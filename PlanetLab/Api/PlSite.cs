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
		/// <summary>
		/// Creates a new PlanetLab site object from the specified object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public PlSite(XmlRpcStruct obj)
		{
			int? lastUpdated = obj["last_updated"].Value.Value.AsInt;
			int? dateCreated = obj["date_created"].Value.Value.AsInt;

			this.LastUpdated = lastUpdated.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(lastUpdated.Value) : null;
			this.DateCreated = dateCreated.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(dateCreated.Value) : null;

			this.SiteId = obj["site_id"].Value.Value.AsInt;
			this.PeerId = obj["peer_id"].Value.Value.AsInt;
			this.ExtConsortiumId = obj["ext_consortium_id"].Value.Value.AsInt;
			this.PeerSiteId = obj["peer_site_id"].Value.Value.AsInt;

			this.IsPublic = obj["is_public"].Value.Value.AsBoolean;
			this.IsEnabled = obj["enabled"].Value.Value.AsBoolean;
			this.MaxSlices = obj["max_slices"].Value.Value.AsInt;
			this.MaxSlivers = obj["max_slivers"].Value.Value.AsInt;

			this.AbbreviatedName = obj["abbreviated_name"].Value.Value.AsString;
			this.Name = obj["name"].Value.Value.AsString;
			this.Url = obj["url"].Value.Value.AsString;
			this.LoginBase = obj["login_base"].Value.Value.AsString;

			this.Latitude = obj["latitude"].Value.Value.AsDouble;
			this.Longitude = obj["longitude"].Value.Value.AsDouble;

			this.NodeIds = obj["node_ids"].Value.Value.AsArray<int>();
			this.PcuIds = obj["pcu_ids"].Value.Value.AsArray<int>();
			this.PersonIds = obj["person_ids"].Value.Value.AsArray<int>();
			this.SliceIds = obj["slice_ids"].Value.Value.AsArray<int>();
			this.AddressIds = obj["address_ids"].Value.Value.AsArray<int>();
			this.SiteTagIds = obj["site_tag_ids"].Value.Value.AsArray<int>();
		}

		/// <summary>
		/// The date when the site entry was last updated.
		/// </summary>
		public DateTime? LastUpdated { get; private set; }
		/// <summary>
		/// The date when the site was created.
		/// </summary>
		public DateTime? DateCreated { get; private set; }

		/// <summary>
		/// The site identifier.
		/// </summary>
		public int? SiteId { get; private set; }
		/// <summary>
		/// The peer identifier.
		/// </summary>
		public int? PeerId { get; private set; }
		/// <summary>
		/// The external consortium identifier.
		/// </summary>
		public int? ExtConsortiumId { get; private set; }
		/// <summary>
		/// The peer site identifier.
		/// </summary>
		public int? PeerSiteId { get; private set; }

		/// <summary>
		/// Publicly viewable site.
		/// </summary>
		public bool? IsPublic { get; private set; }
		/// <summary>
		/// The site has been enabled.
		/// </summary>
		public bool? IsEnabled { get; private set; }
		/// <summary>
		/// The maximum number of slices the site can create.
		/// </summary>
		public int? MaxSlices { get; private set; }
		/// <summary>
		/// The maximum number of slivers the site can create.
		/// </summary>
		public int? MaxSlivers { get; private set; }

		/// <summary>
		/// The site abbreviated name.
		/// </summary>
		public string AbbreviatedName { get; private set; }
		/// <summary>
		/// The site name.
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// The site URL.
		/// </summary>
		public string Url { get; private set; }
		/// <summary>
		/// The site slice prefix.
		/// </summary>
		public string LoginBase { get; private set; }

		/// <summary>
		/// The list of site node identifiers.
		/// </summary>
		public int[] NodeIds { get; private set; }
		/// <summary>
		/// The list of site PCU identifiers.
		/// </summary>
		public int[] PcuIds { get; private set; }
		/// <summary>
		/// The list of site person identifiers.
		/// </summary>
		public int[] PersonIds { get; private set; }
		/// <summary>
		/// The list of site slice identifiers.
		/// </summary>
		public int[] SliceIds { get; private set; }
		/// <summary>
		/// The list of site address identifiers.
		/// </summary>
		public int[] AddressIds { get; private set; }
		/// <summary>
		/// The list of site tag identifiers.
		/// </summary>
		public int[] SiteTagIds { get; private set; }

		/// <summary>
		/// The site latitude.
		/// </summary>
		public double? Latitude { get; private set; }
		/// <summary>
		/// The site longitude.
		/// </summary>
		public double? Longitude { get; private set; }
	}
}
