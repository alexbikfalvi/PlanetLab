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
using DotNetApi.Web.XmlRpc;

namespace PlanetLab.Api
{
	/// <summary>
	/// A class representing a PlanetLab key.
	/// </summary>
	public sealed class PlKey : PlObject
	{
		public enum Fields
		{
			[PlName("key"), PlDisplayName("Key")]
			Key,
			[PlName("key_type"), PlDisplayName("Key type")]
			KeyType,
			[PlName("key_id"), PlDisplayName("Key ID")]
			KeyId,
			[PlName("peer_id"), PlDisplayName("Peer ID")]
			PeerId,
			[PlName("person_id"), PlDisplayName("Person ID")]
			PersonId,
			[PlName("peer_key_id"), PlDisplayName("Peer key ID")]
			PeerKeyId
		}

		/// <summary>
		/// Creates a default PlanetLab key object.
		/// </summary>
		public PlKey()
		{
		}

		/// <summary>
		/// Creates a new PlanetLab key object from the specified object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public PlKey(XmlRpcStruct obj)
		{
			this.Parse(obj);
		}

		// Public properties.

		public override int? Id { get { return this.KeyId; } }

		public string Key { get; private set; }
		public string KeyType { get; private set; }

		public int? KeyId { get; private set; }
		public int? PeerId { get; private set; }
		public int? PersonId { get; private set; }
		public int? PeerKeyId { get; private set; }

		// Public methods.

		/// <summary>
		/// Copies the data of the current object from the specified object.
		/// </summary>
		/// <param name="obj">The object.</param>
		public override void CopyFrom(PlObject obj)
		{
			// Convert the object.
			PlKey item = obj as PlKey;
			// Check the object is not null.
			if (null == item) throw new PlException("The object is null or not of the current type.");

			this.Key = item.Key;
			this.KeyType = item.KeyType;

			this.KeyId = item.KeyId;
			this.PeerId = item.PeerId;
			this.PersonId = item.PersonId;
			this.PeerKeyId = item.PeerKeyId;

			// Raise the changed event.
			base.OnChanged();
		}

		/// <summary>
		/// Parses the current PlanetLab object from the specified XML-RPC object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public override void Parse(XmlRpcStruct obj)
		{
			this.Key = obj[Fields.Key.GetName()].Value.Value.AsString;
			this.KeyType = obj[Fields.KeyType.GetName()].Value.Value.AsString;

			this.KeyId = obj[Fields.KeyId.GetName()].Value.Value.AsInt;
			this.PeerId = obj[Fields.PeerId.GetName()].Value.Value.AsInt;
			this.PersonId = obj[Fields.PersonId.GetName()].Value.Value.AsInt;
			this.PeerKeyId = obj[Fields.PeerKeyId.GetName()].Value.Value.AsInt;

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
			return obj[Fields.KeyId.GetName()].Value.Value.AsInt;
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
