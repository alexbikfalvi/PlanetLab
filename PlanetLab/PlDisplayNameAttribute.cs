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

namespace PlanetLab
{
	/// <summary>
	/// A class representing a PlanetLab display name attribute.
	/// </summary>
	public class PlDisplayNameAttribute : Attribute
	{
		/// <summary>
		/// Creates a new attribute instance.
		/// </summary>
		/// <param name="name">The PlanetLab display name.</param>
		public PlDisplayNameAttribute(string name)
		{
			this.Name = name;
		}

		/// <summary>
		/// Gets the PlanetLab display name.
		/// </summary>
		public string Name { get; private set; }
	}
}
