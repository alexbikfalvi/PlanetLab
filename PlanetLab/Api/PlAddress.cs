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
using DotNetApi.Web.XmlRpc;

namespace PlanetLab.Api
{
	/// <summary>
	/// A class representing a PlanetLab address.
	/// </summary>
	public sealed class PlAddress : PlObject
	{
		public enum Fields
		{
			[PlName("line1"), PlDisplayName("Line 1")]
			Line1,
			[PlName("line2"), PlDisplayName("Line 2")]
			Line2,
			[PlName("line3"), PlDisplayName("Line 3")]
			Line3,
			[PlName("postalcode"), PlDisplayName("Postal code")]
			PostalCode,
			[PlName("city"), PlDisplayName("City")]
			City,
			[PlName("state"), PlDisplayName("State")]
			State,
			[PlName("country"), PlDisplayName("Country")]
			Country,
			[PlName("address_id"), PlDisplayName("Address ID")]
			AddressId,
			[PlName("address_type_ids"), PlDisplayName("Address type IDs")]
			AddressTypeIds,
			[PlName("address_types"), PlDisplayName("Address types")]
			AddressTypes
		}

		/// <summary>
		/// Creates a default PlanetLab address object.
		/// </summary>
		public PlAddress()
		{
		}

		/// <summary>
		/// Creates a new PlanetLab address object from the specified object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public PlAddress(XmlRpcStruct obj)
		{
			this.Parse(obj);
		}

		// Public properties.

		public override int? Id { get { return this.AddressId; } }

		public string Line1 { get; private set; }
		public string Line2 { get; private set; }
		public string Line3 { get; private set; }
		public string PostalCode { get; private set; }
		public string City { get; private set; }
		public string State { get; private set; }
		public string Country { get; private set; }

		public int? AddressId { get; private set; }

		public int[] AddressTypeIds { get; private set; }
		
		public string[] AddressTypes { get; private set; }

		// Public methods.

		/// <summary>
		/// Copies the data of the current object from the specified object.
		/// </summary>
		/// <param name="obj">The object.</param>
		public override void CopyFrom(PlObject obj)
		{
			// Convert the object.
			PlAddress item = obj as PlAddress;
			// Check the object is not null.
			if (null == item) throw new PlException("The object is null or not of the current type.");

			this.Line1 = item.Line1;
			this.Line2 = item.Line2;
			this.Line3 = item.Line3;
			this.PostalCode = item.PostalCode;
			this.City = item.City;
			this.State = item.State;
			this.Country = item.Country;

			this.AddressId = item.AddressId;

			this.AddressTypeIds = item.AddressTypeIds;

			this.AddressTypes = item.AddressTypes;

			// Raise the changed event.
			base.OnChanged();
		}

		/// <summary>
		/// Parses the current PlanetLab object from the specified XML-RPC object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public override void Parse(XmlRpcStruct obj)
		{
			this.Line1 = obj[Fields.Line1.GetName()].Value.Value.AsString;
			this.Line2 = obj[Fields.Line2.GetName()].Value.Value.AsString;
			this.Line3 = obj[Fields.Line3.GetName()].Value.Value.AsString;
			this.PostalCode = obj[Fields.PostalCode.GetName()].Value.Value.AsString;
			this.City = obj[Fields.City.GetName()].Value.Value.AsString;
			this.State = obj[Fields.State.GetName()].Value.Value.AsString;
			this.Country = obj[Fields.Country.GetName()].Value.Value.AsString;

			this.AddressId = obj[Fields.AddressId.GetName()].Value.Value.AsInt;

			this.AddressTypeIds = obj[Fields.AddressTypeIds.GetName()].Value.Value.AsArray<int>();

			this.AddressTypes = obj[Fields.AddressTypes.GetName()].Value.Value.AsArray<string>();

			// Raise the changed event.
			base.OnChanged();
		}

		/// <summary>
		/// Parses the object identifier from the specified XML-RPC object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		/// <returns>The object identifier.</returns>
		public override int? ParseId(XmlRpcStruct obj)
		{
			return obj[Fields.AddressId.GetName()].Value.Value.AsInt;
		}

		/// <summary>
		/// Create a filter for the specified field.
		/// </summary>
		/// <param name="field">The field.</param>
		/// <param name="value">The field value.</param>
		/// <returns>The filter object.</returns>
		public static XmlRpcObject GetFilter(Fields field, object value)
		{
			return new XmlRpcStruct(field.GetName(), value);
		}
	}
}
