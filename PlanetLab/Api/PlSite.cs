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
	public class PlSite
	{
		private DateTime? lastUpdated;
		private DateTime? dateCreated;

		private int? siteId;
		private int? peerId;
		private int? extConsortiumId;
		private int? peerSiteId;

		private bool? isPublic;
		private bool? isEnabled;
		private int? maxSlices;
		private int? maxSlivers;
		
		private string abbreviatedName;
		private string name;
		private string url;
		private string loginBase;

		private int[] nodeIds;
		private int[] pcuIds;
		private int[] personIds;
		private int[] sliceIds;
		private int[] addressIds;
		private int[] siteTagIds;

		private double? latitude;
		private double? longitude;

		/// <summary>
		/// Creates a new PlanetLab site object from the specified object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public PlSite(XmlRpcStruct obj)
		{
			int? lastUpdated = obj["last_updated"].Value.Value.AsInt;
			int? dateCreated = obj["date_created"].Value.Value.AsInt;

			this.lastUpdated = lastUpdated.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(lastUpdated.Value) : null;
			this.dateCreated = dateCreated.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(dateCreated.Value) : null;

			this.siteId = obj["site_id"].Value.Value.AsInt;
			this.peerId = obj["peer_id"].Value.Value.AsInt;
			this.extConsortiumId = obj["ext_consortium_id"].Value.Value.AsInt;
			this.peerSiteId = obj["peer_site_id"].Value.Value.AsInt;

			this.isPublic = obj["is_public"].Value.Value.AsBoolean;
			this.isEnabled = obj["enabled"].Value.Value.AsBoolean;
			this.maxSlices = obj["max_slices"].Value.Value.AsInt;
			this.maxSlivers = obj["max_slivers"].Value.Value.AsInt;

			this.abbreviatedName = obj["abbreviated_name"].Value.Value.AsString;
			this.name = obj["name"].Value.Value.AsString;
			this.url = obj["url"].Value.Value.AsString;
			this.loginBase = obj["login_base"].Value.Value.AsString;

			this.latitude = obj["latitude"].Value.Value.AsDouble;
			this.longitude = obj["longitude"].Value.Value.AsDouble;

			this.nodeIds = (obj["node_ids"].Value.Value as XmlRpcArray).GetArray<int>();
			this.pcuIds = (obj["pcu_ids"].Value.Value as XmlRpcArray).GetArray<int>();
			this.personIds = (obj["person_ids"].Value.Value as XmlRpcArray).GetArray<int>();
			this.sliceIds = (obj["slice_ids"].Value.Value as XmlRpcArray).GetArray<int>();
			this.addressIds = (obj["address_ids"].Value.Value as XmlRpcArray).GetArray<int>();
			this.siteTagIds = (obj["site_tag_ids"].Value.Value as XmlRpcArray).GetArray<int>();
		}

		/// <summary>
		/// The date when the site entry was last updated.
		/// </summary>
		public DateTime? LastUpdated { get { return this.lastUpdated; } }
		/// <summary>
		/// The date when the site was created.
		/// </summary>
		public DateTime? DateCreated { get { return this.dateCreated; } }

		/// <summary>
		/// The site identifier.
		/// </summary>
		public int? SiteId { get { return this.siteId; } }
		/// <summary>
		/// The peer identifier.
		/// </summary>
		public int? PeerId { get { return this.peerId; } }
		/// <summary>
		/// The external consortium identifier.
		/// </summary>
		public int? ExtConsortiumId { get { return this.extConsortiumId; } }
		/// <summary>
		/// The peer site identifier.
		/// </summary>
		public int? PeerSiteId { get { return this.peerSiteId; } }

		/// <summary>
		/// Publicly viewable site.
		/// </summary>
		public bool? IsPublic { get { return this.isPublic; } }
		/// <summary>
		/// The site has been enabled.
		/// </summary>
		public bool? IsEnabled { get { return this.isEnabled; } }
		/// <summary>
		/// The maximum number of slices the site can create.
		/// </summary>
		public int? MaxSlices { get { return this.maxSlices; } }
		/// <summary>
		/// The maximum number of slivers the site can create.
		/// </summary>
		public int? MaxSlivers { get { return this.maxSlivers; } }

		/// <summary>
		/// The site abbreviated name.
		/// </summary>
		public string AbbreviatedName { get { return this.abbreviatedName; } }
		/// <summary>
		/// The site name.
		/// </summary>
		public string Name { get { return this.name; } }
		/// <summary>
		/// The site URL.
		/// </summary>
		public string Url { get { return this.url; } }
		/// <summary>
		/// The site slice prefix.
		/// </summary>
		public string LoginBase { get { return this.loginBase; } }

		/// <summary>
		/// The list of site node identifiers.
		/// </summary>
		public int[] NodeIds { get { return this.nodeIds; } }
		/// <summary>
		/// The list of site PCU identifiers.
		/// </summary>
		public int[] PcuIds { get { return this.pcuIds; } }
		/// <summary>
		/// The list of site person identifiers.
		/// </summary>
		public int[] PersonIds { get { return this.personIds; } }
		/// <summary>
		/// The list of site slice identifiers.
		/// </summary>
		public int[] SliceIds { get { return this.sliceIds; } }
		/// <summary>
		/// The list of site address identifiers.
		/// </summary>
		public int[] AddressIds { get { return this.addressIds; } }
		/// <summary>
		/// The list of site tag identifiers.
		/// </summary>
		public int[] SiteTagIds { get { return this.siteTagIds; } }

		/// <summary>
		/// The site latitude.
		/// </summary>
		public double? Latitude { get { return this.latitude; } }
		/// <summary>
		/// The site longitude.
		/// </summary>
		public double? Longitude { get { return this.longitude; } }
	}
}
