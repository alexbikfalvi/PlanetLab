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
	/// A class representing a PlanetLab site.
	/// </summary>
	public sealed class PlSite : PlObject
	{
		public enum Fields
		{
			[PlName("last_updated")]
			LastUpdated,
			[PlName("date_created")]
			DateCreated,
			[PlName("site_id")]
			SiteId,
			[PlName("peer_id")]
			PeerId,
			[PlName("ext_consortium_id")]
			ExtConsortiumId,
			[PlName("peer_site_id")]
			PeerSiteId,
			[PlName("is_public")]
			IsPublic,
			[PlName("enabled")]
			IsEnabled,
			[PlName("max_slices")]
			MaxSlices,
			[PlName("max_slivers")]
			MaxSlivers,
			[PlName("abbreviated_name")]
			AbbreviatedName,
			[PlName("name")]
			Name,
			[PlName("url")]
			Url,
			[PlName("login_base")]
			LoginBase,
			[PlName("latitude")]
			Latitude,
			[PlName("longitude")]
			Longitude,
			[PlName("node_ids")]
			NodeIds,
			[PlName("pcu_ids")]
			PcuIds,
			[PlName("person_ids")]
			PersonIds,
			[PlName("slice_ids")]
			SliceIds,
			[PlName("address_ids")]
			AddressIds,
			[PlName("site_tag_ids")]
			SiteTagIds
		}

		/// <summary>
		/// Creates a default PlanetLab site object.
		/// </summary>
		public PlSite()
		{
		}

		/// <summary>
		/// Creates a new PlanetLab site object from the specified object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public PlSite(XmlRpcStruct obj)
		{
			this.Parse(obj);
		}

		// Public properties.

		public override int? Id { get { return this.SiteId; } }

		public DateTime? LastUpdated { get; private set; }
		public DateTime? DateCreated { get; private set; }

		public int? SiteId { get; private set; }
		public int? PeerId { get; private set; }
		public int? ExtConsortiumId { get; private set; }
		public int? PeerSiteId { get; private set; }

		public bool? IsPublic { get; private set; }
		public bool? IsEnabled { get; private set; }
		public int? MaxSlices { get; private set; }
		public int? MaxSlivers { get; private set; }

		public string AbbreviatedName { get; private set; }
		public string Name { get; private set; }
		public string Url { get; private set; }
		public string LoginBase { get; private set; }

		public int[] NodeIds { get; private set; }
		public int[] PcuIds { get; private set; }
		public int[] PersonIds { get; private set; }
		public int[] SliceIds { get; private set; }
		public int[] AddressIds { get; private set; }
		public int[] SiteTagIds { get; private set; }

		public double? Latitude { get; private set; }
		public double? Longitude { get; private set; }

		// Public methods.

		/// <summary>
		/// Parses the current PlanetLab object from the specified XML-RPC object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public override void Parse(XmlRpcStruct obj)
		{
			int? lastUpdated = obj[Fields.LastUpdated.GetName()].Value.Value.AsInt;
			int? dateCreated = obj[Fields.DateCreated.GetName()].Value.Value.AsInt;

			this.LastUpdated = lastUpdated.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(lastUpdated.Value) : null;
			this.DateCreated = dateCreated.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(dateCreated.Value) : null;

			this.SiteId = obj[Fields.SiteId.GetName()].Value.Value.AsInt;
			this.PeerId = obj[Fields.PeerId.GetName()].Value.Value.AsInt;
			this.ExtConsortiumId = obj[Fields.ExtConsortiumId.GetName()].Value.Value.AsInt;
			this.PeerSiteId = obj[Fields.PeerSiteId.GetName()].Value.Value.AsInt;

			this.IsPublic = obj[Fields.IsPublic.GetName()].Value.Value.AsBoolean;
			this.IsEnabled = obj[Fields.IsEnabled.GetName()].Value.Value.AsBoolean;
			this.MaxSlices = obj[Fields.MaxSlices.GetName()].Value.Value.AsInt;
			this.MaxSlivers = obj[Fields.MaxSlivers.GetName()].Value.Value.AsInt;

			this.AbbreviatedName = obj[Fields.AbbreviatedName.GetName()].Value.Value.AsString;
			this.Name = obj[Fields.Name.GetName()].Value.Value.AsString;
			this.Url = obj[Fields.Url.GetName()].Value.Value.AsString;
			this.LoginBase = obj[Fields.LoginBase.GetName()].Value.Value.AsString;

			this.Latitude = obj[Fields.Latitude.GetName()].Value.Value.AsDouble;
			this.Longitude = obj[Fields.Longitude.GetName()].Value.Value.AsDouble;

			this.NodeIds = obj[Fields.NodeIds.GetName()].Value.Value.AsArray<int>();
			this.PcuIds = obj[Fields.PcuIds.GetName()].Value.Value.AsArray<int>();
			this.PersonIds = obj[Fields.PersonIds.GetName()].Value.Value.AsArray<int>();
			this.SliceIds = obj[Fields.SliceIds.GetName()].Value.Value.AsArray<int>();
			this.AddressIds = obj[Fields.AddressIds.GetName()].Value.Value.AsArray<int>();
			this.SiteTagIds = obj[Fields.SiteTagIds.GetName()].Value.Value.AsArray<int>();

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
			return obj[Fields.SiteId.GetName()].Value.Value.AsInt;
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
