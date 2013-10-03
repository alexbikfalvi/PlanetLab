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
using DotNetApi;
using DotNetApi.Concurrent;
using DotNetApi.Web.XmlRpc;

namespace PlanetLab.Api
{
	/// <summary>
	/// A class representing a PlanetLab list.
	/// </summary>
	/// <typeparam name="T">The list item type.</typeparam>
	public class PlList<T> : ConcurrentBase, IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable where T : PlObject, new()
	{
		private readonly List<T> list = new List<T>();
		private readonly Dictionary<int, T> dictionary = new Dictionary<int, T>();
		private readonly object syncRoot = new object();

		/// <summary>
		/// Creates an empty PlanetLab address list.
		/// </summary>
		public PlList()
		{
		}

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
		/// Gets a value indicating whether the list is synchronized. The property is always <b>true</b>.
		/// </summary>
		public bool IsSynchronized { get { return true; } }
		/// <summary>
		/// Gets a value indicating whether the list is read-only. The property is always <b>false</b>.
		/// </summary>
		public bool IsReadOnly { get { return false; } }
		/// <summary>
		/// Gets a value indicating whether the list is fixed in size. The property is always <b>false</b>.
		/// </summary>
		public bool IsFixedSize { get { return false; } }
		/// <summary>
		/// Gets the object used for synchronizing list operations.
		/// </summary>
		public object SyncRoot { get { return this.syncRoot; } }
		/// <summary>
		/// Gets or sets the element at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The element.</returns>
		object IList.this[int index]
		{
			get { return this[index]; }
			set { this[index] = value as T; }
		}
		/// <summary>
		/// Gets or sets the element at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The element.</returns>
		public T this[int index]
		{
			get
			{
				// Acquire a reader lock.
				LockInfo info = this.AcquireReaderLock();
				try
				{
					// Return the list item.
					return this.list[index];
				}
				finally
				{
					// Release the reader lock.
					this.ReleaseReaderLock(info);
				}
			}
			set
			{
				// Check the item is not null.
				if (null == value) throw new ArgumentNullException("item");
				// If the item does not have a valid identifier, throw an exception.
				if (!value.Id.HasValue) throw new PlException("The item cannot have a null identifier.");
				// Acquire a writer lock.
				LockInfo info = this.AcquireWriterLock();
				try
				{
					// Get the current item.
					T item = this.list[index];
					// If the current item and the new item are different objects.
					if (!object.ReferenceEquals(value, item))
					{
						// Remove the current item from the dictionary.
						this.dictionary.Remove(item.Id.Value);
						// Check the item does not exist in the dictionary.
						if (this.dictionary.ContainsKey(value.Id.Value)) throw new PlException("An item with the identifier {0} already exists.".FormatWith(value.Id.Value));
						// Set the list item.
						this.list[index] = value;
						// Add the new item to the dictionary.
						this.dictionary.Add(value.Id.Value, value);
					}
				}
				finally
				{
					// Release the writer lock.
					this.ReleaseWriterLock(info);
				}
			}
		}

		// Public methods.

		/// <summary>
		/// Adds an item to the list.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns>The position to which the item was added.</returns>
		public int Add(object item)
		{
			// The item position.
			int position;
			// Add the object to the list.
			this.Add(item as T, out position);
			// Return the item position.
			return position;
		}

		/// <summary>
		/// Adds an item to the list.
		/// </summary>
		/// <param name="item">The item.</param>
		public void Add(T item)
		{
			// The item position.
			int position;
			// Add the object to the list.
			this.Add(item as T, out position);
		}

		/// <summary>
		/// Adds an item to the list.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="position">The position.</param>
		public void Add(T item, out int position)
		{
			// Check the item is not null.
			if (null == item) throw new ArgumentNullException("value");
			// If the item does not have a valid identifier, throw an exception.
			if (!item.Id.HasValue) throw new PlException("The item cannot have a null identifier.");
			// Acquire a writer lock.
			LockInfo info = this.AcquireWriterLock();
			try
			{
				// Check the item does not exist in the dictionary.
				if (this.dictionary.ContainsKey(item.Id.Value)) throw new PlException("An item with the identifier {0} already exists.".FormatWith(item.Id.Value));
				// Add the item to the list.
				this.list.Add(item);
				// Add the item to the dictionary.
				this.dictionary.Add(item.Id.Value, item);
				// Set the item position.
				position = this.list.Count - 1;
			}
			finally
			{
				// Release the writer lock.
				this.ReleaseWriterLock(info);
			}
		}

		/// <summary>
		/// Clears the list.
		/// </summary>
		public void Clear()
		{
			// Acquire a writer lock.
			LockInfo info = this.AcquireWriterLock();
			try
			{
				// Clear the list.
				this.list.Clear();
				// Clear the dictionary.
				this.dictionary.Clear();
			}
			finally
			{
				// Release the writer lock.
				this.ReleaseWriterLock(info);
			}
		}

		/// <summary>
		/// Indicates whether the list contains the specified item.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns><b>True</b> if the list contains the item, <b>false</b> otherwise.</returns>
		public bool Contains(object item)
		{
			return this.Contains(item as T);
		}

		/// <summary>
		/// Indicates whether the list contains the specified item.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns><b>True</b> if the list contains the item, <b>false</b> otherwise.</returns>
		public bool Contains(T item)
		{
			// Check the item is not null.
			if (null == item) throw new ArgumentNullException("value");
			// If the item does not have a valid identifier, throw an exception.
			if (!item.Id.HasValue) throw new PlException("The item cannot have a null identifier.");
			// Acquire a reader lock.
			LockInfo info = this.AcquireReaderLock();
			try
			{
				return this.dictionary.ContainsKey(item.Id.Value);
			}
			finally
			{
				// Release the reader lock.
				this.ReleaseReaderLock(info);
			}
		}

		/// <summary>
		/// Copies all elements to the specified array starting from the index.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <param name="index">The index.</param>
		public void CopyTo(Array array, int index)
		{
			this.CopyTo(array as T[], index);
		}

		/// <summary>
		/// Copies all elements to the specified array starting from the index.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <param name="index">The index.</param>
		public void CopyTo(T[] array, int index)
		{
			// Check the array is not null.
			if (null == array) throw new ArgumentNullException("array");
			// Acquire a reader lock.
			LockInfo info = this.AcquireReaderLock();
			try
			{
				// Copy the list to the array.
				this.list.CopyTo(array);
			}
			finally
			{
				// Release the reader lock.
				this.ReleaseReaderLock(info);
			}
		}

		/// <summary>
		/// Returns the enumerator for the current list.
		/// </summary>
		/// <returns>The enumerator.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>
		/// Returns the enumerator for the current list.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator<T> GetEnumerator()
		{
			// The thread must first acquire a lock before retrieving the enumerator.
			if (!this.HasLock()) throw new SynchronizationLockException();
			// Return the enumerator.
			return this.list.GetEnumerator();
		}

		/// <summary>
		/// Determines the index of a specific item in the list.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns>The index, or -1 if the item is not found.</returns>
		public int IndexOf(object item)
		{
			return this.IndexOf(item as T);
		}

		/// <summary>
		/// Determines the index of a specific item in the list.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns>The index, or -1 if the item is not found.</returns>
		public int IndexOf(T item)
		{
			// Check the item is not null.
			if (null == item) throw new ArgumentNullException("value");
			// Acquire a reader lock.
			LockInfo info = this.AcquireReaderLock();
			try
			{
				// Return the index.
				return this.list.IndexOf(item);
			}
			finally
			{
				// Release the reader lock.
				this.ReleaseReaderLock(info);
			}
		}

		/// <summary>
		/// Inserts the specified item in the list.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="item">The item.</param>
		public void Insert(int index, object item)
		{
			this.Insert(index, item as T);
		}

		/// <summary>
		/// Inserts the specified item in the list.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="item">The item.</param>
		public void Insert(int index, T item)
		{
			// Check the item is not null.
			if (null == item) throw new ArgumentNullException("value");
			// If the item does not have a valid identifier, throw an exception.
			if (!item.Id.HasValue) throw new PlException("The item cannot have a null identifier.");
			// Acquire a writer lock.
			LockInfo info = this.AcquireWriterLock();
			try
			{
				// Check the item does not exist in the dictionary.
				if (this.dictionary.ContainsKey(item.Id.Value)) throw new PlException("An item with the identifier {0} already exists.".FormatWith(item.Id.Value));
				// Insert the item.
				this.list.Insert(index, item);
				// Add the new item to the dictionary.
				this.dictionary.Add(item.Id.Value, item);
			}
			finally
			{
				// Release the writer lock.
				this.ReleaseWriterLock(info);
			}
		}

		/// <summary>
		/// Removes the first occurrence of the specified item from the collection.
		/// </summary>
		/// <param name="item">The item.</param>
		public void Remove(object item)
		{
		}

		/// <summary>
		/// Removes the first occurrence of the specified item from the collection.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns><b>True</b> if the item was successfully removed,<b>false</b> otherwise.</returns>
		public bool Remove(T item)
		{
			// Check the item is not null.
			if (null == item) throw new ArgumentNullException("value");
			// If the item does not have a valid identifier, throw an exception.
			if (!item.Id.HasValue) throw new PlException("The item cannot have a null identifier.");
			// Acquire a writer lock.
			LockInfo info = this.AcquireWriterLock();
			try
			{
				// Remove the item from the dictionary.
				this.dictionary.Remove(item.Id.Value);
				// Remove the item from the list.
				return this.list.Remove(item);
			}
			finally
			{
				// Release the writer lock.
				this.ReleaseWriterLock(info);
			}
		}

		/// <summary>
		/// Removes the item at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		public void RemoveAt(int index)
		{
			// Acquire a writer lock.
			LockInfo info = this.AcquireWriterLock();
			try
			{
				// Get the item at the specified index.
				T item = this.list[index];
				// Remove the item from the list.
				this.list.RemoveAt(index);
				// Remove the item from the dictionary
				this.dictionary.Remove(item.Id.Value);
			}
			finally
			{
				// Release the writer lock.
				this.ReleaseWriterLock(info);
			}
		}

		/// <summary>
		/// Creates a PlanetLab address list from the specified XML-RPC array.
		/// </summary>
		/// <param name="obj">The XML-RPC array.</param>
		/// <returns>A PlanetLab slice list.</returns>
		public static PlList<T> Create(XmlRpcArray obj)
		{
			// Create the object.
			PlList<T> list = new PlList<T>();
			// Update the list.
			list.Update(obj);
			// Return the object.
			return list;
		}

		/// <summary>
		/// Clears the current list and copies all elements from the argument list.
		/// </summary>
		/// <param name="list">The list from where to copy.</param>
		public void CopyFrom(PlList<T> list)
		{
			// Validate the argument.
			if (null == list) throw new ArgumentNullException("list");
			// If this object and list are the same instance, do nothing.
			if (object.ReferenceEquals(this, list)) return;

			// Acquire a writer lock.
			LockInfo infoThis = this.AcquireWriterLock();
			try
			{
				// Clear the list and the dictionary.
				this.list.Clear();
				this.dictionary.Clear();

				// Acquire a reader lock on the list.
				LockInfo infoList = list.AcquireReaderLock();
				try
				{
					// Update the items.
					foreach (T item in list)
					{
						// Ignore items that do not have an identifier.
						if (!item.Id.HasValue) continue;
						// Ignore items that are already in the dictionary.
						if (this.dictionary.ContainsKey(item.Id.Value)) continue;

						// Add the item to the list.
						this.list.Add(item);
						// Add the item to the dictionary
						this.dictionary.Add(item.Id.Value, item);
					}
				}
				finally
				{
					// Release the reader lock.
					list.ReleaseReaderLock(infoList);
				}
			}
			finally
			{
				// Release the writer lock.
				this.ReleaseWriterLock(infoThis);
			}
		}

		/// <summary>
		/// Updates the list with all PlanetLab object from the specified array.
		/// </summary>
		/// <param name="obj">The XML-RPC array.</param>
		public void Update(XmlRpcArray obj)
		{
			// Acquire a writer lock.
			LockInfo info = this.AcquireWriterLock();
			try
			{
				// Clear the list and the dictionary.
				this.list.Clear();
				this.dictionary.Clear();

				// If the object is not null.
				if (null != obj)
				{
					// Update the items.
					foreach (XmlRpcValue element in obj.Values)
					{
						// Get the element as a structure.
						XmlRpcStruct str = element.Value as XmlRpcStruct;
						// Ignore non-structure elements.
						if (null == str) continue;
					
						// Create a new item.
						T item = new T();
						item.Parse(element.Value as XmlRpcStruct);
						
						// Ignore items that do not have an identifier.
						if (!item.Id.HasValue) continue;
						// Ignore items that are already in the dictionary.
						if (this.dictionary.ContainsKey(item.Id.Value)) continue;

						// Add the item to the list.
						this.list.Add(item);
						// Add the item to the dictionary
						this.dictionary.Add(item.Id.Value, item);
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
		/// Loads the list of PlanetLab addresses from the specified file.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		public void LoadFromFile(string fileName)
		{
			// If the file does not exist, do nothing.
			if (!File.Exists(fileName)) return;
			// Load the XML document from a file.
			XDocument document = XDocument.Load(fileName);
			// Parse the XML into the XML-RPC array object.
			this.Clear();
			document.Root.Deserialize<PlList<T>>(this);
		}

		/// <summary>
		/// Saves the list of PlanetLab addresses to the specified file.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		public void SaveToFile(string fileName)
		{
			// Lock the list.
			this.Lock();
			try
			{
				// Create a new XML document for the current serialized XML object.
				XDocument document = new XDocument(this.Serialize("List"));
				// Save the XML document to the file.
				document.Save(fileName);
			}
			finally
			{
				this.Unlock();
			}
		}
	}
}
