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
	public class PlSite
	{
		private DateTime lastUpdated;
		private DateTime dateCreated;

		private int siteId;
		private int peerId;
		private int extConsortiumId;
		private int peerSiteId;

		private bool isPublic;
		private bool enabled;
		private int maxSlices;
		private int maxSlivers;
		
		private string abbreviatedName;
		private string name;
		private string url;
		private string loginBase;

		private int[] nodesIds;
		private int[] pcuIds;
		private int[] personIds;
		private int[] sliceIds;
		private int[] addressIds;
		private int[] siteTagIds;

		private double latitude;
		private double longitude;

		/// <summary>
		/// Creates a new PlanetLab site object from the specified object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public PlSite(XmlRpcStruct obj)
		{
			this.lastUpdated = PlDateTime.FromUnixTimestamp((obj["last_updated"].Value.Value as XmlRpcInt).Value);
			this.dateCreated = PlDateTime.FromUnixTimestamp((obj["date_created"].Value.Value as XmlRpcInt).Value);

			this.siteId = (obj["site_id"].Value.Value as XmlRpcInt).Value;
			this.peerId = (obj["peer_id"].Value.Value as XmlRpcInt).Value;
			this.extConsortiumId = (obj["ext_consortium_id"].Value.Value as XmlRpcInt).Value;
			this.peerSiteId = (obj["peer_site_id"].Value.Value as XmlRpcInt).Value;

			this.isPublic = (obj["is_public"].Value.Value as XmlRpcBoolean).Value;
			this.enabled = (obj["enabled"].Value.Value as XmlRpcBoolean).Value;
			this.maxSlices = (obj["max_slices"].Value.Value as XmlRpcInt).Value;
			this.maxSlivers = (obj["max_slivers"].Value.Value as XmlRpcInt).Value;

			this.abbreviatedName = (obj["abbreviated_name"].Value.Value as XmlRpcString).Value;
			this.name = (obj["name"].Value.Value as XmlRpcString).Value;
			this.url = (obj["url"].Value.Value as XmlRpcString).Value;
			this.loginBase = (obj["login_base"].Value.Value as XmlRpcString).Value;

			this.latitude = (obj["latitude"].Value.Value as XmlRpcDouble).Value;
			this.longitude = (obj["longitude"].Value.Value as XmlRpcDouble).Value;
		}
	}
}
