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
using PlanetLab.Api;

namespace PlanetLab
{
	/// <summary>
	/// A delegate representing a PlanetLab object event handler.
	/// </summary>
	/// <typeparam name="T">The PlanetLab object type.</typeparam>
	/// <param name="sender">The sender object.</param>
	/// <param name="e">The event arguments.</param>
	public delegate void PlObjectEventHandler(object sender, PlObjectEventArgs e);

	/// <summary>
	/// A delegate representing a PlanetLab object event handler.
	/// </summary>
	/// <typeparam name="T">The PlanetLab object type.</typeparam>
	/// <param name="sender">The sender object.</param>
	/// <param name="e">The event arguments.</param>
	public delegate void PlObjectEventHandler<T>(object sender, PlObjectEventArgs<T> e) where T : PlObject;

	/// <summary>
	/// A class representing a database object selected event argument.
	/// </summary>
	public sealed class PlObjectEventArgs : EventArgs
	{
		/// <summary>
		/// Creates a new event instance.
		/// </summary>
		/// <param name="obj">The selected result.</param>
		public PlObjectEventArgs(PlObject obj)
		{
			this.Object = obj;
		}

		// Public properties.

		/// <summary>
		/// The PlanetLab event object.
		/// </summary>
		public PlObject Object { get; private set; }
	}

	/// <summary>
	/// A class representing a database object selected event argument.
	/// </summary>
	/// <typeparam name="T">The PlanetLab object type.</typeparam>
	public sealed class PlObjectEventArgs<T> : EventArgs where T : PlObject
	{
		/// <summary>
		/// Creates a new event instance.
		/// </summary>
		/// <param name="obj">The selected result.</param>
		public PlObjectEventArgs(T obj)
		{
			this.Object = obj;
		}

		// Public properties.

		/// <summary>
		/// The PlanetLab event object.
		/// </summary>
		public T Object { get; private set; }
	}
}
