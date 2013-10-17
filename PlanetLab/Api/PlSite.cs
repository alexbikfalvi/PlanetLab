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
			[PlName("last_updated"), PlDisplayName("Last updated")]
			LastUpdated,
			[PlName("date_created"), PlDisplayName("Date created")]
			DateCreated,
			[PlName("site_id"), PlDisplayName("Site ID")]
			SiteId,
			[PlName("peer_id"), PlDisplayName("Peer ID")]
			PeerId,
			[PlName("ext_consortium_id"), PlDisplayName("Ext consortium ID")]
			ExtConsortiumId,
			[PlName("peer_site_id"), PlDisplayName("Peer site ID")]
			PeerSiteId,
			[PlName("is_public"), PlDisplayName("Is public")]
			IsPublic,
			[PlName("enabled"), PlDisplayName("Enabled")]
			IsEnabled,
			[PlName("max_slices"), PlDisplayName("Maximum slices")]
			MaxSlices,
			[PlName("max_slivers"), PlDisplayName("Maximum slivers")]
			MaxSlivers,
			[PlName("abbreviated_name"), PlDisplayName("Abbreviated name")]
			AbbreviatedName,
			[PlName("name"), PlDisplayName("Name")]
			Name,
			[PlName("url"), PlDisplayName("URL")]
			Url,
			[PlName("login_base"), PlDisplayName("Login base")]
			LoginBase,
			[PlName("latitude"), PlDisplayName("Latitude")]
			Latitude,
			[PlName("longitude"), PlDisplayName("Longitude")]
			Longitude,
			[PlName("node_ids"), PlDisplayName("Node IDs")]
			NodeIds,
			[PlName("pcu_ids"), PlDisplayName("PCU IDs")]
			PcuIds,
			[PlName("person_ids"), PlDisplayName("Person IDs")]
			PersonIds,
			[PlName("slice_ids"), PlDisplayName("Slice IDs")]
			SliceIds,
			[PlName("address_ids"), PlDisplayName("Address IDs")]
			AddressIds,
			[PlName("site_tag_ids"), PlDisplayName("Site tag IDs")]
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
		/// Copies the data of the current object from the specified object.
		/// </summary>
		/// <param name="obj">The object.</param>
		public override void CopyFrom(PlObject obj)
		{
			// Convert the object.
			PlSite item = obj as PlSite;
			// Check the object is not null.
			if (null == item) throw new PlException("The object is null or not of the current type.");

			this.LastUpdated = item.LastUpdated;
			this.DateCreated = item.DateCreated;

			this.SiteId = item.SiteId;
			this.PeerId = item.PeerId;
			this.ExtConsortiumId = item.ExtConsortiumId;
			this.PeerSiteId = item.PeerSiteId;

			this.IsPublic = item.IsPublic;
			this.IsEnabled = item.IsEnabled;
			this.MaxSlices = item.MaxSlices;
			this.MaxSlivers = item.MaxSlivers;

			this.AbbreviatedName = item.AbbreviatedName;
			this.Name = item.Name;
			this.Url = item.Url;
			this.LoginBase = item.LoginBase;

			this.Latitude = item.Latitude;
			this.Longitude = item.Longitude;

			this.NodeIds = item.NodeIds;
			this.PcuIds = item.PcuIds;
			this.PersonIds = item.PersonIds;
			this.SliceIds = item.SliceIds;
			this.AddressIds = item.AddressIds;
			this.SiteTagIds = item.SiteTagIds;

			// Raise the changed event.
			base.OnChanged();
		}

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
