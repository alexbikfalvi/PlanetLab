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
using System.Security;
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
		/// <param name="callback">The callback funcion.</param>
		/// <param name="state">The user state.</param>
		/// <returns>The result of the asynchronous operation.</returns>
		public override IAsyncResult Begin(string username, SecureString password, AsyncWebRequestCallback callback, object state = null)
		{
			// Create the parameters.
			object[] parameters = new object[1];

			parameters[0] = new PlAuthentication(username, password);

			// Call the base class method.
			return base.Begin(PlRequestGetSites.method, parameters, callback, state);
		}

		/// <summary>
		/// Begins an asynchronous request for a PlanetLab method.
		/// </summary>
		/// <param name="username">The PlanetLab username.</param>
		/// <param name="password">The PlanetLab password.</param>
		/// <param name="id">The PlanetLab site identifier.</param>
		/// <param name="callback">The callback funcion.</param>
		/// <param name="state">The user state.</param>
		/// <returns>The result of the asynchronous operation.</returns>
		public override IAsyncResult Begin(string username, SecureString password, int id, AsyncWebRequestCallback callback, object state = null)
		{
			// Create the parameters.
			object[] parameters = new object[2];

			parameters[0] = new PlAuthentication(username, password);
			parameters[1] = id;

			// Call the base class method.
			return base.Begin(PlRequestGetSites.method, parameters, callback, state);
		}
	}
}
