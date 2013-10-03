﻿/* 
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
using PlanetLab;
using DotNetApi.Web.XmlRpc;

namespace PlanetLab.Api
{
	/// <summary>
	/// A class representing the base class for all PlanetLab objects.
	/// </summary>
	public abstract class PlObject
	{
		// Public properties.

		public abstract int? Id { get; }

		// Public events.

		/// <summary>
		/// An event raised when the PlanetLab object has changed.
		/// </summary>
		public event PlEventHandler Changed;

		// Public methods.

		/// <summary>
		/// Parses the current PlanetLab object from the specified XML-RPC object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public abstract void Parse(XmlRpcStruct obj);

		/// <summary>
		/// Parses the object identifier from the specified XML-RPC object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		/// <returns>The object identifier.</returns>
		public abstract int? ParseId(XmlRpcStruct obj);

		// Protected methods.

		/// <summary>
		/// An event handler called when the PlanetLab object has changed.
		/// </summary>
		protected virtual void OnChanged()
		{
			// Raise the event.
			if (null != this.Changed) this.Changed(this, new PlEventArgs(this));
		}
	}
}
