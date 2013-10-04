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
		private readonly List<T> list = new List<T>();

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
		/// An event raised when the database list has changed.
		/// </summary>
		public event PlDatabaseListEventHandler<T> Changed;

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
			if (!this.HasLock()) throw new SynchronizationLockException();
			return this.list.GetEnumerator();
		}

		/// <summary>
		/// Clears the current database list.
		/// </summary>
		public void Clear()
		{
			// Acquire a writer lock.
			LockInfo info = this.AcquireWriterLock();
			try
			{
				this.list.Clear();
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
		public void Add(T obj)
		{
			// Check the object is not null.
			if (null == obj) throw new ArgumentNullException("obj");

			// Acquire a writer lock.
			LockInfo info = this.AcquireWriterLock();
			try
			{
				// Add the object.
				this.list.Add(this.database.Add(obj));
			}
			finally
			{
				// Release the writer lock.
				this.ReleaseWriterLock(info);
			}
		}

		/// <summary>
		/// Removes the specified object from the database list.
		/// </summary>
		/// <param name="obj">The object to remove.</param>
		public void Remove(T obj)
		{
			// Check the object is not null.
			if (null == obj) throw new ArgumentNullException("obj");

			// Acquire a writer lock.
			LockInfo info = this.AcquireWriterLock();
			try
			{
				// Remove the item.
				this.list.Remove(this.database[obj]);
			}
			finally
			{
				// Release the writer lock.
				this.ReleaseWriterLock(info);
			}
		}

		/// <summary>
		/// Updates the database list with all PlanetLab object from the specified array.
		/// </summary>
		/// <param name="obj">The XML-RPC array.</param>
		public void CopyFrom(XmlRpcArray obj)
		{
			// Check the object is not null.
			if (null == obj) throw new ArgumentNullException("obj");

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
						this.list.Add(this.database.Add(element.Value as XmlRpcStruct));
					}
				}
			}
			finally
			{
				// Release the writer lock.
				this.ReleaseWriterLock(info);
			}
		}

		/// <summary>
		/// Copies the current database list from the specified list.
		/// </summary>
		/// <param name="list">The list.</param>
		public void CopyFrom(PlList<T> list)
		{
			// Check the list is not null.
			if (null == list) throw new ArgumentNullException("list");

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
						this.list.Add(this.database.Add(item));
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
				list.CopyFrom(this.list);
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
	}
}
