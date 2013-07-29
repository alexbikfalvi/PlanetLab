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
using DotNetApi.Security;
using DotNetApi.Web.XmlRpc;

namespace PlanetLab.Api.Auth
{
	/// <summary>
	/// A PlanetLab authentication structure.
	/// </summary>
	public class PlAuthentication : XmlRpcStruct
	{
		/// <summary>
		/// Creates a new authentication structure using the specified session key.
		/// </summary>
		/// <param name="key">The session key.</param>
		public PlAuthentication(string key)
		{
			this.Add("AuthMethod", "session");
			this.Add("session", key);
		}

		/// <summary>
		/// Creates a new authentication structure using the specified username and password.
		/// </summary>
		/// <param name="username">The user name.</param>
		/// <param name="password">The password.</param>
		public PlAuthentication(string username, SecureString password)
		{
			this.Add("AuthMethod", "password");
			this.Add("Username", username);
			this.Add("AuthString", password.ConvertToUnsecureString());
		}

		/// <summary>
		/// Creates a new authentication structure for anonymous authentication.
		/// </summary>
		public PlAuthentication()
		{
			this.Add("AuthMethod", "anonymous");
		}
	}
}
