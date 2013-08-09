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
	/// A class representing a list of PlanetLab PCUs.
	/// </summary>
	public class PlPcus : List<PlPcu>
	{
		private XmlRpcArray xml = null;

		/// <summary>
		/// Creates an empty PlanetLab PCUs list.
		/// </summary>
		public PlPcus()
		{
		}

		/// <summary>
		/// Creates a PlanetLab PCUs list from the specified XML-RPC array.
		/// </summary>
		/// <param name="obj">The XML-RPC array.</param>
		/// <returns>A PlanetLab PCUs list.</returns>
		public static PlPcus Create(XmlRpcArray obj)
		{
			// Create the object.
			PlPcus pcus = new PlPcus();
			// Update the PCUs object.
			pcus.Update(obj);
			// Return the object.
			return pcus;
		}

		// Public methods.

		/// <summary>
		/// Updates the list of PlanetLab PCUs from the specified array.
		/// </summary>
		/// <param name="obj">The XML-RPC array.</param>
		public void Update(XmlRpcArray obj)
		{
			// Save the XML-RPC object.
			this.xml = obj;
			// Clear the PCUs list.
			this.Clear();
			// If the object is not null.
			if (null != obj)
			{
				// Update the PCUs list.
				foreach (XmlRpcValue value in obj.Values)
				{
					this.Add(new PlPcu(value.Value as XmlRpcStruct));
				}
			}
		}

		/// <summary>
		/// Loads the list of PlanetLab PCUs from the specified file.
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
		/// Saves the list of PlanetLab PCUs to the specified file.
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
