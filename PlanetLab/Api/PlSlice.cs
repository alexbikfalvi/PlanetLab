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
	/// A class representing a PlanetLab slice.
	/// </summary>
	public class PlSlice
	{
		public enum Fields
		{
			[PlName("name")]
			Name,
			[PlName("description")]
			Description,
			[PlName("instantiation")]
			Instantiation,
			[PlName("url")]
			Url,
			[PlName("created")]
			Created,
			[PlName("expires")]
			Expires,
			[PlName("max_nodes")]
			MaxNodes,
			[PlName("slice_id")]
			SliceId,
			[PlName("site_id")]
			SiteId,
			[PlName("peer_id")]
			PeerId,
			[PlName("peer_slice_id")]
			PeerSliceId,
			[PlName("creator_person_id")]
			CreatorPersonId,
			[PlName("node_ids")]
			NodeIds,
			[PlName("person_ids")]
			PersonIds
		}

		/// <summary>
		/// Creates a new PlanetLab slice object from the specified object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public PlSlice(XmlRpcStruct obj)
		{
			int? created = obj[Fields.Created.GetName()].Value.Value.AsInt;
			int? expires = obj[Fields.Expires.GetName()].Value.Value.AsInt;

			this.Created = created.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(created.Value) : null;
			this.Expires = expires.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(expires.Value) : null;

			this.Name = obj[Fields.Name.GetName()].Value.Value.AsString;
			this.Description = obj[Fields.Description.GetName()].Value.Value.AsString;
			this.Instantiation = obj[Fields.Instantiation.GetName()].Value.Value.AsString;
			this.Url = obj[Fields.Url.GetName()].Value.Value.AsString;

			this.MaxNodes = obj[Fields.MaxNodes.GetName()].Value.Value.AsInt;

			this.SliceId = obj[Fields.SliceId.GetName()].Value.Value.AsInt;
			this.SiteId = obj[Fields.SiteId.GetName()].Value.Value.AsInt;
			this.PeerId = obj[Fields.PeerId.GetName()].Value.Value.AsInt;
			this.PeerSliceId = obj[Fields.PeerSliceId.GetName()].Value.Value.AsInt;
			this.CreatorPersonId = obj[Fields.CreatorPersonId.GetName()].Value.Value.AsInt;

			this.NodeIds = obj[Fields.NodeIds.GetName()].Value.Value.AsArray<int>();
			this.PersonIds = obj[Fields.PersonIds.GetName()].Value.Value.AsArray<int>();
		}

		// Public properties.

		public DateTime? Created { get; private set; }
		public DateTime? Expires { get; private set; }

		public string Name { get; private set; }
		public string Description { get; private set; }
		public string Instantiation { get; private set; }
		public string Url { get; private set; }

		public int? MaxNodes { get; private set; }

		public int? SliceId { get; private set; }
		public int? SiteId { get; private set; }
		public int? PeerId { get; private set; }
		public int? PeerSliceId { get; private set; }
		public int? CreatorPersonId { get; private set; }
		
		public int[] NodeIds { get; private set; }
		public int[] PersonIds { get; private set; }

		// Public methods.

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
