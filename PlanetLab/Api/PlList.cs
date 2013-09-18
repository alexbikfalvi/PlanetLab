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
using System.IO;
using System.Linq;
using System.Xml.Linq;
using DotNetApi.Concurrent.Generic;
using DotNetApi.Web.XmlRpc;

namespace PlanetLab.Api
{
	public class PlList<T> : ConcurrentList<T> where T : PlObject, new()
	{
		private XmlRpcArray array = null;

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
		/// Clears the current list and copies all elements from the argument list.
		/// </summary>
		/// <param name="list">The list from where to copy.</param>
		public void CopyFrom(PlList<T> list)
		{
			// Validate the argument.
			if (null == list) throw new ArgumentNullException("list");
			// If this object and list are the same instance, do nothing.
			if (this == list) return;
			// Update all elements from the list.
			this.Update(list.array);
		}

		/// <summary>
		/// Updates the list with all PlanetLab object from the specified array.
		/// </summary>
		/// <param name="obj">The XML-RPC array.</param>
		public void Update(XmlRpcArray obj)
		{
			// Save the XML-RPC object.
			this.array = obj;
			// Clear the list.
			this.Clear();
			// If the object is not null.
			if (null != obj)
			{
				// Update the addresses list.
				foreach (XmlRpcValue element in obj.Values)
				{
					XmlRpcStruct str = element.Value as XmlRpcStruct;
					if (null == str) continue;
					
					T item = new T();
					item.Parse(element.Value as XmlRpcStruct);
					this.Add(item);
				}
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
			this.Update(XmlRpcArray.Create(document.Root) as XmlRpcArray);
		}

		/// <summary>
		/// Saves the list of PlanetLab addresses to the specified file.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		public void SaveToFile(string fileName)
		{
			// Create a new XML document for the current XML object.
			XDocument document = new XDocument();
			// If the current XML object is not null.
			if (null != this.array)
			{
				// Add the object.
				document.Add(this.array.GetXml());
				// Save the XML document to the file.
				document.Save(fileName);
			}
		}
	}
}
