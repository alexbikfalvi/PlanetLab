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
using System.Reflection;

namespace PlanetLab
{
	/// <summary>
	/// A class of useful methods for PlanetLab.
	/// </summary>
	public static class PlUtil
	{
		/// <summary>
		/// Returns the PlanetLab name for the specified enumration value.
		/// </summary>
		/// <param name="value">The enumeration value.</param>
		/// <returns>The PlanetLab name.</returns>
		public static string GetName(this Enum value)
		{
			Type type = value.GetType();
			MemberInfo[] member = type.GetMember(value.ToString());
			object[] attributes = member[0].GetCustomAttributes(typeof(PlNameAttribute), false);
			return (attributes[0] as PlNameAttribute).Name;
		}
	}
}
