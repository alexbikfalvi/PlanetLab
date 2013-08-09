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

		/// <summary>
		/// Converts the specified longitude to a string.
		/// </summary>
		/// <param name="longitude">The longitude.</param>
		/// <returns>The display string.</returns>
		public static string LongitudeToString(this double longitude)
		{
			double absoluteLongitude = Math.Abs(longitude);
			
			double degrees = Math.Floor(absoluteLongitude);
			double minutes = (absoluteLongitude - degrees) * 60.0;
			double seconds = (minutes - Math.Floor(minutes)) * 60.0;
			double tenths = (seconds - Math.Floor(seconds)) * 10.0;

			minutes = Math.Floor(minutes);
			seconds = Math.Floor(seconds);
			tenths = Math.Floor(tenths);

			return string.Format("{0}° {1}ʹ {2}.{3}ʺ {4}", degrees, minutes, seconds, tenths, Math.Sign(longitude) >= 0 ? 'E' : 'W');
		}

		/// <summary>
		/// Converts the specified latitude to a string.
		/// </summary>
		/// <param name="latitude">The latitude.</param>
		/// <returns>The display string.</returns>
		public static string LatitudeToString(this double latitude)
		{
			double absoluteLatitude = Math.Abs(latitude);

			double degrees = Math.Floor(absoluteLatitude);
			double minutes = (absoluteLatitude - degrees) * 60.0;
			double seconds = (minutes - Math.Floor(minutes)) * 60.0;
			double tenths = (seconds - Math.Floor(seconds)) * 10.0;

			minutes = Math.Floor(minutes);
			seconds = Math.Floor(seconds);
			tenths = Math.Floor(tenths);

			return string.Format("{0}° {1}ʹ {2}.{3}ʺ {4}", degrees, minutes, seconds, tenths, Math.Sign(latitude) >= 0 ? 'N' : 'S');
		}
	}
}
