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
using DotNetApi.Web;
using DotNetApi.Web.XmlRpc;
using PlanetLab.Api.Auth;

namespace PlanetLab.Requests
{
	/// <summary>
	/// A class representing an asynchronous request for PlanetLab data.
	/// </summary>
	public class PlRequestGetSites : PlRequest
	{
		private static string method = "GetSites";

		/// <summary>
		/// Creates an asynchronous PlanetLab request with the specified settings.
		/// </summary>
		public PlRequestGetSites() { }

		// Public methods.

		/// <summary>
		/// Begins an asynchronous request for a PlanetLab method.
		/// </summary>
		/// <param name="username">The PlanetLab username.</param>
		/// <param name="password">The PlanetLab password.</param>
		/// <param name="filter">The nodes filter.</param>
		/// <param name="callback">The callback funcion.</param>
		/// <param name="state">The user state.</param>
		/// <returns>The result of the asynchronous operation.</returns>
		public IAsyncResult Begin(string username, string password, string filter, AsyncWebRequestCallback callback, object state = null)
		{
			// Create the parameters.
			object[] parameters = new object[filter != string.Empty ? 2 : 1];

			parameters[0] = new PlAuthentication(username, password);
			if (filter != string.Empty) parameters[1] = filter;

			// Call the base class method.
			return base.Begin(PlRequestGetSites.method, parameters, callback, state);
		}
	}
}
