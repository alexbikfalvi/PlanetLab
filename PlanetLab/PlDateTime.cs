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

namespace PlanetLab
{
	/// <summary>
	/// A PlanetLab date time.
	/// </summary>
	public struct PlDateTime
	{
		private static DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		/// <summary>
		/// Converts the specified UNIX timestamp into a date-time.
		/// </summary>
		/// <param name="dateTime">The UNIX timestamp.</param>
		/// <returns>The date-time.</returns>
		public static DateTime FromUnixTimestamp(Int64 dateTime)
		{
			return PlDateTime.unixEpoch.AddSeconds(dateTime).ToLocalTime();
		}

		/// <summary>
		/// Converts the specified date-time into the UNIX timestamp.
		/// </summary>
		/// <param name="dateTime">The date-time.</param>
		/// <returns>The UNIX timestamp.</returns>
		public static Int64 ToUnixTimestamp(DateTime dateTime)
		{
			return (Int64)dateTime.ToUniversalTime().Subtract(PlDateTime.unixEpoch).TotalSeconds;
		}
	}
}
