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
using System.Xml.Linq;
using System.Xml.Serialization;
using DotNetApi.Concurrent.Generic;
using DotNetApi.Web.XmlRpc;

namespace PlanetLab.Api
{
	/// <summary>
	/// A class representing a PlanetLab list.
	/// </summary>
	/// <typeparam name="T">The list item type.</typeparam>
	public sealed class PlList<T> : ConcurrentList<T> where T : PlObject, new()
	{
		/// <summary>
		/// Creates an empty PlanetLab address list.
		/// </summary>
		public PlList()
		{
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

		// Public methods.

		/// <summary>
		/// Clears the current list and copies all elements from the specified enumerable.
		/// </summary>
		/// <param name="list">The list from where to copy.</param>
		public void CopyFrom(IEnumerable<T> list)
		{
			// Validate the argument.
			if (null == list) throw new ArgumentNullException("list");
			// Clear the list.
			this.Clear();
			// Update the items.
			foreach (T item in list)
			{
				this.Add(item);
			}
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
			// Lock the list.
			list.Lock();
			try
			{
				// Clear the list.
				this.Clear();
				// Update the items.
				foreach (T item in list)
				{
					this.Add(item);
				}
			}
			finally
			{
				list.Unlock();
			}
		}

		/// <summary>
		/// Updates the list with all PlanetLab object from the specified array.
		/// </summary>
		/// <param name="obj">The XML-RPC array.</param>
		public void Update(XmlRpcArray obj)
		{
			// Clear the list.
			this.Clear();
			// If the object is not null.
			if (null != obj)
			{
				// Update the items.
				foreach (XmlRpcValue element in obj.Values)
				{
					XmlRpcStruct str = element.Value as XmlRpcStruct;
					if (null == str) continue;

					T item = new T();
					item.Parse(str);
					this.Add(item);
				}
			}
		}

		/// <summary>
		/// Loads the list of PlanetLab addresses from the specified file.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		public static PlList<T> LoadFromFile(string fileName)
		{
			// Create a new empty list.
			PlList<T> list = new PlList<T>();
			// If the file does not exist, do nothing.
			if (!File.Exists(fileName)) return list;
			// Create a new stream for the specified file.
			using (FileStream stream = new FileStream(fileName, FileMode.Open))
			{
				// Deserialize the stream to the list object.
				stream.Deserialize<PlList<T>>(list);
			}
			// Return the list.
			return list;
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
				// Create a new XML file for the current object.
				using (FileStream stream = new FileStream(fileName, FileMode.Create))
				{
					// Serialize the current object to the stream.
					this.Serialize(stream);
				}
			}
			finally
			{
				this.Unlock();
			}
		}
	}
}