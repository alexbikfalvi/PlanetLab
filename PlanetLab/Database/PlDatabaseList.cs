/* 
 * Copyright (C) 2013 Alex Bikfalvi
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or (at
 * your option) any later version.
 *
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using System.Xml.Serialization;
using DotNetApi.Concurrent;
using DotNetApi.Web.XmlRpc;
using PlanetLab;
using PlanetLab.Api;

namespace PlanetLab.Database
{
	/// <summary>
	/// A class representing a PlanetLab database list.
	/// </summary>
	/// <typeparam name="T">The PlanetLab object type.</typeparam>
	public sealed class PlDatabaseList<T> : ConcurrentBase, IEnumerable<T> where T : PlObject, new()
	{
		private readonly PlDatabase<T> database;
		private readonly Dictionary<int, T> list = new Dictionary<int, T>();

		private DateTime lastSaved = DateTime.MinValue;
		private DateTime lastUpdated = DateTime.Now;

		/// <summary>
		/// Creates a new PlanetLab list using the specified database.
		/// </summary>
		/// <param name="database">The PlanetLab database.</param>
		public PlDatabaseList(PlDatabase<T> database)
		{
			this.database = database;
		}

		// Public events.

		/// <summary>
		/// An event raised before the database list has been cleared.
		/// </summary>
		public event EventHandler Cleared;
		/// <summary>
		/// An event raised after the database list has been updated.
		/// </summary>
		public event EventHandler Updated;
		/// <summary>
		/// An event raised when an object has been added to the list.
		/// </summary>
		public event PlObjectEventHandler<T> Added;
		/// <summary>
		/// An event raised when an object has been removed from the list.
		/// </summary>
		public event PlObjectEventHandler<T> Removed;

		// Public properties.

		/// <summary>
		/// Gets the number of elements contained in the list.
		/// </summary>
		public int Count
		{
			get
			{
				// Acquire a reader lock.
				LockInfo info = this.AcquireReaderLock();
				try
				{
					// Return the list count.
					return this.list.Count;
				}
				finally
				{
					// Release the reader lock.
					this.ReleaseReaderLock(info);
				}
			}
		}
		/// <summary>
		/// Gets the date-time when the list was last saved.
		/// </summary>
		public DateTime LastSaved { get { return this.lastSaved; } }
		/// <summary>
		/// Gets the date-time when the list was last updated.
		/// </summary>
		public DateTime LastUpdated { get { return this.lastUpdated; } }

		// Public methods.

		/// <summary>
		/// Returns the enumeator for the current list.
		/// </summary>
		/// <returns>The enumerator.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>
		/// Returns the enumeator for the current list.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator<T> GetEnumerator()
		{
			// The thread must first acquire a lock before retrieving the enumerator.
			if (!this.HasLock()) throw new InvalidOperationException("Thread must own a lock on this concurrent collection.");
			return this.list.Values.GetEnumerator();
		}

		/// <summary>
		/// Clears the current database list.
		/// </summary>
		public void Clear()
		{
			// Call the cleared event handler.
			this.OnCleared();
			// Acquire a writer lock.
			LockInfo info = this.AcquireWriterLock();
			try
			{
				// Remove the changed event handlers.
				foreach (T obj in this.list.Values)
				{
					obj.Changed -= this.OnObjectChanged;
				}
				// Clear the list.
				this.list.Clear();
				// Update the timestamp.
				this.lastUpdated = DateTime.Now;
			}
			finally
			{
				// Release the writer lock.
				this.ReleaseWriterLock(info);
			}
		}

		/// <summary>
		/// Adds the specified PlanetLab object to the list.
		/// </summary>
		/// <param name="obj">The object to add.</param>
		/// <returns><b>True</b> if the object was added, <b>false</b> otherwise.</returns>
		public bool Add(T obj)
		{
			// Check the object is not null.
			if (null == obj) throw new ArgumentNullException("obj");

			bool added = false;

			// Acquire a writer lock.
			LockInfo info = this.AcquireWriterLock();
			try
			{
				// Add the object.
				added = this.OnAddInternal(this.database.Add(obj));
			}
			finally
			{
				// Release the writer lock.
				this.ReleaseWriterLock(info);
			}
			// Call the added event handler.
			if (added) this.OnAdded(obj);
			// Return added.
			return added;
		}

		/// <summary>
		/// Adds the specified PlanetLab object to the list.
		/// </summary>
		/// <param name="obj">The object to add.</param>
		/// <returns><b>True</b> if the object was added, <b>false</b> otherwise.</returns>
		public T Add(XmlRpcStruct obj)
		{
			// Check the object is not null.
			if (null == obj) throw new ArgumentNullException("obj");

			T o = null;
			bool added = false;

			// Acquire a writer lock.
			LockInfo info = this.AcquireWriterLock();
			try
			{
				// Add the object to the database.
				o = this.database.Add(obj);
				// Add the object to the list.
				added = this.OnAddInternal(o);
			}
			finally
			{
				// Release the writer lock.
				this.ReleaseWriterLock(info);
			}
			// Call the added event handler.
			if (added) this.OnAdded(o);
			// Return added.
			return o;
		}

		/// <summary>
		/// Removes the specified object from the database list.
		/// </summary>
		/// <param name="obj">The object to remove.</param>
		/// <returns><b>True</b> if the object was removed, <b>false</b> otherwise.</returns>
		public bool Remove(T obj)
		{
			// Check the object is not null.
			if (null == obj) throw new ArgumentNullException("obj");

			bool removed = false;

			// Acquire a writer lock.
			LockInfo info = this.AcquireWriterLock();
			try
			{
				// Remove the object event handler.
				obj.Changed -= this.OnObjectChanged;
				// Remove the object.
				removed = this.list.Remove(this.database[obj].Id.Value);
				// Update the timestamp.
				this.lastUpdated = DateTime.Now;
			}
			finally
			{
				// Release the writer lock.
				this.ReleaseWriterLock(info);
			}
			// Call the removed event handler.
			if (removed) this.OnRemoved(obj);
			// Returned removed.
			return removed;
		}

		/// <summary>
		/// Updates the database list with all PlanetLab object from the specified array.
		/// </summary>
		/// <param name="obj">The XML-RPC array.</param>
		public void CopyFrom(XmlRpcArray obj)
		{
			// Check the object is not null.
			if (null == obj) throw new ArgumentNullException("obj");

			// Call the cleared event handler.
			this.OnCleared();
			// Acquire a writer lock.
			LockInfo info = this.AcquireWriterLock();
			try
			{
				// Clear the list.
				this.list.Clear();
				// If the object is not null.
				if (null != obj)
				{
					// Update the items.
					foreach (XmlRpcValue element in obj.Values)
					{
						XmlRpcStruct str = element.Value as XmlRpcStruct;
						if (null == str) continue;
						this.OnAddInternal(this.database.Add(str));
					}
				}
			}
			finally
			{
				// Release the writer lock.
				this.ReleaseWriterLock(info);
			}
			// Call the updated event handler.
			this.OnUpdated();
		}

		/// <summary>
		/// Copies the current database list from the specified list.
		/// </summary>
		/// <param name="list">The list.</param>
		public void CopyFrom(PlList<T> list)
		{
			// Check the list is not null.
			if (null == list) throw new ArgumentNullException("list");

			// Call the cleared event handler.
			this.OnCleared();
			// Acquire a writer lock.
			LockInfo info = this.AcquireWriterLock();
			try
			{
				// Clear the current list.
				this.list.Clear();
				// Acquire a lock on the list.
				list.Lock();
				try
				{
					// Add the item from the list.
					foreach (T item in list)
					{
						this.OnAddInternal(this.database.Add(item));
					}
				}
				finally
				{
					list.Unlock();
				}
			}
			finally
			{
				// Release the writer lock.
				this.ReleaseWriterLock(info);
			}
			// Call the updated event handler.
			this.OnUpdated();
		}
		
		/// <summary>
		/// Copies the current database list to the specified list.
		/// </summary>
		/// <param name="list">The list.</param>
		public void CopyTo(PlList<T> list)
		{
			// Check the list is not null.
			if (null == list) throw new ArgumentNullException("list");

			// Acquire a reader lock.
			LockInfo info = this.AcquireReaderLock();
			try
			{
				// Copy the elements to the list.
				list.CopyFrom(this.list.Values);
			}
			finally
			{
				// Release the reader lock.
				this.ReleaseReaderLock(info);
			}
		}

		/// <summary>
		/// Loads the list of PlanetLab addresses from the specified file.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		public void LoadFromFile(string fileName)
		{
			// Load the list of object from the specified file.
			PlList<T> list = PlList<T>.LoadFromFile(fileName);
			// Copy the elements from the list to the current database list.
			this.CopyFrom(list);
			// Set the timestamp.
			this.lastUpdated = 
		}

		/// <summary>
		/// Saves the list of PlanetLab addresses to the specified file.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		public void SaveToFile(string fileName)
		{
			// Acquire a reader lock.
			LockInfo info = this.AcquireReaderLock();
			try
			{
				// Create a new XML document for the current serialized XML object.
				XDocument document = new XDocument(this.Serialize("List"));
				// Save the XML document to the file.
				document.Save(fileName);
			}
			finally
			{
				// Release the reader lock.
				this.ReleaseReaderLock(info);
			}
		}

		// Private methods.

		/// <summary>
		/// Adds the specified object to the list.
		/// </summary>
		/// <param name="obj">The PlanetLab object.</param>
		/// <returns><b>True</b> if the object was added to the list, <b>false</b> if the object already existed.</returns>
		private bool OnAddInternal(T obj)
		{
			// The current object.
			T curr;
			// If the object does not exist.
			if (!this.list.TryGetValue(obj.Id.Value, out curr))
			{
				// Add the object.
				this.list.Add(obj.Id.Value, obj);
				// Return true.
				return true;
			}
			else if (!object.ReferenceEquals(curr, obj))
			{
				throw new PlException("Cannot add object to the list because a different reference already exists.");
			}
			// Else, return false.
			return false;
		}

		/// <summary>
		/// An event handler called before the list has been cleared.
		/// </summary>
		private void OnCleared()
		{
			// Raise the event.
			if (null != this.Cleared) this.Cleared(this, EventArgs.Empty);
		}

		/// <summary>
		/// An event handler called after the list has been updated.
		/// </summary>
		private void OnUpdated()
		{
			// Raise the event.
			if (null != this.Updated) this.Updated(this, EventArgs.Empty);
		}

		/// <summary>
		/// An event handler called after an item has been added.
		/// </summary>
		/// <param name="obj">The object.</param>
		private void OnAdded(T obj)
		{
			// Raise the event.
			if (null != this.Added) this.Added(this, new PlObjectEventArgs<T>(obj));
		}

		/// <summary>
		/// An event handler called after an item has been removed.
		/// </summary>
		/// <param name="obj">The object.</param>
		private void OnRemoved(T obj)
		{
			// Raise the event.
			if (null != this.Removed) this.Removed(this, new PlObjectEventArgs<T>(obj));
		}

		/// <summary>
		/// An event handler called when a list object has changed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnObjectChanged(object sender, PlObjectEventArgs e)
		{
			// Update the list.
			this.lastUpdated = DateTime.Now;
		}
	}
}
