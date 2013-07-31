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
	/// A class representing a PlanetLab node.
	/// </summary>
	public class PlNode
	{
		/// <summary>
		/// Creates a new PlanetLab node object from the specified object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public PlNode(XmlRpcStruct obj)
		{
			int? lastUpdated = obj["last_updated"].Value.Value.AsInt;
			int? lastBoot = obj["last_boot"].Value.Value.AsInt;
			int? lastPcuReboot = obj["last_pcu_reboot"].Value.Value.AsInt;
			int? lastContact = obj["last_contact"].Value.Value.AsInt;
			int? lastPcuConfirmation = obj["last_pcu_confirmation"].Value.Value.AsInt;
			int? lastDownload = obj["last_download"].Value.Value.AsInt;
			int? dateCreated = obj["date_created"].Value.Value.AsInt;

			this.LastUpdated = lastUpdated.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(lastUpdated.Value) : null;
			this.LastBoot = lastBoot.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(lastBoot.Value) : null;
			this.LastPcuReboot = lastPcuReboot.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(lastPcuReboot.Value) : null;
			this.LastContact = lastContact.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(lastContact.Value) : null;
			this.LastPcuConfirmation = lastPcuConfirmation.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(lastPcuConfirmation.Value) : null;
			this.LastDownload = lastDownload.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(lastDownload.Value) : null;
			this.DateCreated = dateCreated.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(dateCreated.Value) : null;

			this.BootState = obj["boot_state"].Value.Value.AsString;
			this.NodeType = obj["node_type"].Value.Value.AsString;
			this.RunLevel = obj["run_level"].Value.Value.AsString;
			this.SshRsaKey = obj["ssh_rsa_key"].Value.Value.AsString;
			this.Hostname = obj["hostname"].Value.Value.AsString;
			this.Version = obj["version"].Value.Value.AsString;
			this.Model = obj["model"].Value.Value.AsString;

			this.SiteId = obj["site_id"].Value.Value.AsInt;
			this.NodeId = obj["node_id"].Value.Value.AsInt;
			this.PeerId = obj["peer_id"].Value.Value.AsInt;
			this.PeerNodeId = obj["peer_node_id"].Value.Value.AsInt;

			this.LastTimeSpentOffline = obj["last_time_spent_offline"].Value.Value.AsInt;
			this.LastTimeSpentOnline = obj["last_time_spent_online"].Value.Value.AsInt;

			this.Verified = obj["verified"].Value.Value.AsBoolean;

			this.Ports = obj["ports"].Value.Value.AsArray<int>();

			this.PcuIds = obj["pcu_ids"].Value.Value.AsArray<int>();
			this.InterfaceIds = obj["interface_ids"].Value.Value.AsArray<int>();
			this.SliceIds = obj["slice_ids"].Value.Value.AsArray<int>();
			this.NodeTagIds = obj["node_tag_ids"].Value.Value.AsArray<int>();
			this.NodeGroupIds = obj["nodegroup_ids"].Value.Value.AsArray<int>();
			this.SliceIdsWhitelist = obj["slice_ids_whitelist"].Value.Value.AsArray<int>();
			this.ConfigurationFileIds = obj["conf_file_ids"].Value.Value.AsArray<int>();
		}

		public DateTime? LastUpdated { get; private set; }
		public DateTime? LastBoot { get; private set; }
		public DateTime? LastPcuReboot { get; private set; }
		public DateTime? LastContact { get; private set; }
		public DateTime? LastPcuConfirmation { get; private set; }
		public DateTime? LastDownload { get; private set; }
		public DateTime? DateCreated { get; private set; }
		
		public string BootState { get; private set; }
		public string NodeType { get; private set; }
		public string RunLevel { get; private set; }
		public string SshRsaKey { get; private set; }
		public string Hostname { get; private set; }
		public string Version { get; private set; }
		public string Model { get; private set; }
		
		public int? SiteId { get; private set; }
		public int? NodeId { get; private set; }
		public int? PeerId { get; private set; }
		public int? PeerNodeId { get; private set; }

		public int? LastTimeSpentOffline { get; private set; }
		public int? LastTimeSpentOnline { get; private set; }

		public bool? Verified { get; private set; }

		public int[] Ports { get; private set; }
		
		public int[] PcuIds { get; private set; }
		public int[] InterfaceIds { get; private set; }
		public int[] SliceIds { get; private set; }
		public int[] NodeTagIds { get; private set; }
		public int[] NodeGroupIds { get; private set; }
		public int[] SliceIdsWhitelist { get; private set; }
		public int[] ConfigurationFileIds { get; private set; }
	}
}
