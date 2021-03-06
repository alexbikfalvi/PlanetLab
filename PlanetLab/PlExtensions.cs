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
using DotNetApi;
using PlanetLab.Api;

namespace PlanetLab
{
	/// <summary>
	/// An enumeration representing the boot state.
	/// </summary>
	public enum PlBootState
	{
		[PlDisplayName("Unknown")]
		Unknown = 0,
		[PlDisplayName("Boot")]
		Boot = 1,
		[PlDisplayName("Safe boot")]
		SafeBoot = 2,
		[PlDisplayName("Disabled")]
		Disabled = 3,
		[PlDisplayName("Reinstall")]
		Reinstall = 4
	}

	/// <summary>
	/// A class of useful methods for PlanetLab.
	/// </summary>
	public static class PlExtensions
	{
		/// <summary>
		/// Returns the PlanetLab name for the specified enumration value.
		/// </summary>
		/// <param name="value">The enumeration value.</param>
		/// <returns>The PlanetLab name.</returns>
		public static string GetName(this Enum value)
		{
			return value.GetAttribute<PlNameAttribute>().Name;
		}

		/// <summary>
		/// Returns the PlanetLab display name for the specified enumration value.
		/// </summary>
		/// <param name="value">The enumeration value.</param>
		/// <returns>The PlanetLab display name.</returns>
		public static string GetDisplayName(this Enum value)
		{
			return value.GetAttribute<PlDisplayNameAttribute>().Name;
		}

		/// <summary>
		/// Returns the boot state for the specified PlanetLab node.
		/// </summary>
		/// <param name="node">The PlanetLab node.</param>
		/// <returns>The boot state.</returns>
		public static PlBootState GetBootState(this PlNode node)
		{
			// If the boot state is null, return unknown.
			if (node.BootState == null) return PlBootState.Unknown;
			
			// Get the lower-case version.
			string bootState = node.BootState.ToLower();

			// Return the corresponding the boot state.
			if (bootState.Equals("boot")) return PlBootState.Boot;
			else if (bootState.Equals("safeboot")) return PlBootState.SafeBoot;
			else if (bootState.Equals("disabled")) return PlBootState.Disabled;
			else if (bootState.Equals("reinstall")) return PlBootState.Reinstall;
			else return PlBootState.Unknown;
		}
	}
}
