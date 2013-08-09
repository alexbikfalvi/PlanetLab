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
using System.Collections.Generic;
using System.Xml.Linq;
using DotNetApi.Web.XmlRpc;

namespace PlanetLab.Api
{
	/// <summary>
	/// A class representing a list of PlanetLab persons.
	/// </summary>
	public class PlPersons : List<PlPerson>
	{
		private XmlRpcArray xml = null;

		/// <summary>
		/// Creates an empty PlanetLab persons list.
		/// </summary>
		public PlPersons()
		{
		}

		/// <summary>
		/// Creates a PlanetLab persons list from the specified XML-RPC array.
		/// </summary>
		/// <param name="obj">The XML-RPC array.</param>
		/// <returns>A PlanetLab persons list.</returns>
		public static PlPersons Create(XmlRpcArray obj)
		{
			// Create the object.
			PlPersons persons = new PlPersons();
			// Update the persons object.
			persons.Update(obj);
			// Return the object.
			return persons;
		}

		// Public methods.

		/// <summary>
		/// Updates the list of PlanetLab persons from the specified array.
		/// </summary>
		/// <param name="obj">The XML-RPC array.</param>
		public void Update(XmlRpcArray obj)
		{
			// Save the XML-RPC object.
			this.xml = obj;
			// Clear the persons list.
			this.Clear();
			// If the object is not null.
			if (null != obj)
			{
				// Update the persons list.
				foreach (XmlRpcValue value in obj.Values)
				{
					this.Add(new PlPerson(value.Value as XmlRpcStruct));
				}
			}
		}

		/// <summary>
		/// Loads the list of PlanetLab persons from the specified file.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		public void LoadFromFile(string fileName)
		{
			// Load the XML document from a file.
			XDocument document = XDocument.Load(fileName);
			// Parse the XML into the XML-RPC array object.
			this.Update(XmlRpcArray.Create(document.Root) as XmlRpcArray);
		}

		/// <summary>
		/// Saves the list of PlanetLab persons to the specified file.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		public void SaveToFile(string fileName)
		{
			// Create a new XML document for the current XML object.
			XDocument document = new XDocument();
			// If the current XML object is not null, add the object.
			if (null != this.xml) document.Add(this.xml.GetXml());
			// Save the XML document to the file.
			document.Save(fileName);
		}
	}
}
