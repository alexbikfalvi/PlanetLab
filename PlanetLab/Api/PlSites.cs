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
using DotNetApi.Web.XmlRpc;

namespace PlanetLab.Api
{
	public class PlSites
	{
		private List<PlSite> sites = new List<PlSite>();

		/// <summary>
		/// Creates an empty PlanetLab sites list.
		/// </summary>
		public PlSites()
		{
		}

		// Public properties.
		
		/// <summary>
		/// Gets the collection of PlanetLab sites.
		/// </summary>
		public ICollection<PlSite> Sites
		{
			get { return this.sites; }
		}

		// Public methods.

		/// <summary>
		/// Updates the PlanetLab sites list from the specified array.
		/// </summary>
		/// <param name="obj">The XML-RPC array.</param>
		public void Update(XmlRpcArray obj)
		{
			this.sites.Clear();
			foreach (XmlRpcValue value in obj.Values)
			{
				this.sites.Add(new PlSite(value.Value as XmlRpcStruct));
			}
		}
	}
}
