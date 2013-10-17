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
	public sealed class PlNode : PlObject
	{
		public enum Fields
		{
			[PlName("last_updated"), PlDisplayName("Last updated")]
			LastUpdated,
			[PlName("last_boot"), PlDisplayName("Last boot")]
			LastBoot,
			[PlName("last_pcu_reboot"), PlDisplayName("Last PCU reboot")]
			LastPcuReboot,
			[PlName("last_contact"), PlDisplayName("Last contact")]
			LastContact,
			[PlName("last_pcu_confirmation"), PlDisplayName("Last PCU confirmation")]
			LastPcuConfirmation,
			[PlName("last_download"), PlDisplayName("Last download")]
			LastDownload,
			[PlName("date_created"), PlDisplayName("Date created")]
			DateCreated,
			[PlName("boot_state"), PlDisplayName("Boot state")]
			BootState,
			[PlName("node_type"), PlDisplayName("Node type")]
			NodeType,
			[PlName("run_level"), PlDisplayName("Run level")]
			RunLevel,
			[PlName("ssh_rsa_key"), PlDisplayName("SSH RSA key")]
			SshRsaKey,
			[PlName("hostname"), PlDisplayName("Hostname")]
			Hostname,
			[PlName("version"), PlDisplayName("Version")]
			Version,
			[PlName("model"), PlDisplayName("Model")]
			Model,
			[PlName("site_id"), PlDisplayName("Site ID")]
			SiteId,
			[PlName("node_id"), PlDisplayName("Node ID")]
			NodeId,
			[PlName("peer_id"), PlDisplayName("Peer ID")]
			PeerId,
			[PlName("peer_node_id"), PlDisplayName("Peer node ID")]
			PeerNodeId,
			[PlName("last_time_spent_offline"), PlDisplayName("Last time spent offline")]
			LastTimeSpentOffline,
			[PlName("last_time_spent_online"), PlDisplayName("Last time spent online")]
			LastTimeSpentOnline,
			[PlName("verified"), PlDisplayName("Verified")]
			Verified,
			[PlName("ports"), PlDisplayName("Ports")]
			Ports,
			[PlName("pcu_ids"), PlDisplayName("PCU IDs")]
			PcuIds,
			[PlName("interface_ids"), PlDisplayName("Interface IDs")]
			InterfaceIds,
			[PlName("slice_ids"), PlDisplayName("Slice IDs")]
			SliceIds,
			[PlName("node_tag_ids"), PlDisplayName("Node tag IDs")]
			NodeTagIds,
			[PlName("nodegroup_ids"), PlDisplayName("Node group IDs")]
			NodeGroupIds,
			[PlName("slice_ids_whitelist"), PlDisplayName("Slice IDs")]
			SliceIdsWhitelist,
			[PlName("conf_file_ids"), PlDisplayName("Configuration file IDs")]
			ConfigurationFileIds
		}

		/// <summary>
		/// Creates a default PlanetLab node object.
		/// </summary>
		public PlNode()
		{
		}

		/// <summary>
		/// Creates a new PlanetLab node object from the specified object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public PlNode(XmlRpcStruct obj)
		{
			this.Parse(obj);
		}

		// Public properties.

		public override int? Id { get { return this.NodeId; } }

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

		public TimeSpan? LastTimeSpentOffline { get; private set; }
		public TimeSpan? LastTimeSpentOnline { get; private set; }

		public bool? Verified { get; private set; }

		public int[] Ports { get; private set; }
		
		public int[] PcuIds { get; private set; }
		public int[] InterfaceIds { get; private set; }
		public int[] SliceIds { get; private set; }
		public int[] NodeTagIds { get; private set; }
		public int[] NodeGroupIds { get; private set; }
		public int[] SliceIdsWhitelist { get; private set; }
		public int[] ConfigurationFileIds { get; private set; }

		// Public methods.

		/// <summary>
		/// Copies the data of the current object from the specified object.
		/// </summary>
		/// <param name="obj">The object.</param>
		public override void CopyFrom(PlObject obj)
		{
			// Convert the object.
			PlNode item = obj as PlNode;
			// Check the object is not null.
			if (null == item) throw new PlException("The object is null or not of the current type.");

			this.LastUpdated = item.LastUpdated;
			this.LastBoot = item.LastBoot;
			this.LastPcuReboot = item.LastPcuReboot;
			this.LastContact = item.LastContact;
			this.LastPcuConfirmation = item.LastPcuConfirmation;
			this.LastDownload = item.LastDownload;
			this.DateCreated = item.DateCreated;

			this.BootState = item.BootState;
			this.NodeType = item.NodeType;
			this.RunLevel = item.RunLevel;
			this.SshRsaKey = item.SshRsaKey;
			this.Hostname = item.Hostname;
			this.Version = item.Version;
			this.Model = item.Model;

			this.SiteId = item.SiteId;
			this.NodeId = item.NodeId;
			this.PeerId = item.PeerId;
			this.PeerNodeId = item.PeerNodeId;

			this.LastTimeSpentOffline = item.LastTimeSpentOffline;
			this.LastTimeSpentOnline = item.LastTimeSpentOnline;

			this.Verified = item.Verified;

			this.Ports = item.Ports;

			this.PcuIds = item.PcuIds;
			this.InterfaceIds = item.InterfaceIds;
			this.SliceIds = item.SliceIds;
			this.NodeTagIds = item.NodeTagIds;
			this.NodeGroupIds = item.NodeGroupIds;
			this.SliceIdsWhitelist = item.SliceIdsWhitelist;
			this.ConfigurationFileIds = item.ConfigurationFileIds;

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
			int? lastBoot = obj[Fields.LastBoot.GetName()].Value.Value.AsInt;
			int? lastPcuReboot = obj[Fields.LastPcuReboot.GetName()].Value.Value.AsInt;
			int? lastContact = obj[Fields.LastContact.GetName()].Value.Value.AsInt;
			int? lastPcuConfirmation = obj[Fields.LastPcuConfirmation.GetName()].Value.Value.AsInt;
			int? lastDownload = obj[Fields.LastDownload.GetName()].Value.Value.AsInt;
			int? dateCreated = obj[Fields.DateCreated.GetName()].Value.Value.AsInt;

			int? lastTimeSpentOffline = obj[Fields.LastTimeSpentOffline.GetName()].Value.Value.AsInt;
			int? lastTimeSpentOnline = obj[Fields.LastTimeSpentOnline.GetName()].Value.Value.AsInt;

			this.LastUpdated = lastUpdated.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(lastUpdated.Value) : null;
			this.LastBoot = lastBoot.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(lastBoot.Value) : null;
			this.LastPcuReboot = lastPcuReboot.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(lastPcuReboot.Value) : null;
			this.LastContact = lastContact.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(lastContact.Value) : null;
			this.LastPcuConfirmation = lastPcuConfirmation.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(lastPcuConfirmation.Value) : null;
			this.LastDownload = lastDownload.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(lastDownload.Value) : null;
			this.DateCreated = dateCreated.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(dateCreated.Value) : null;

			this.BootState = obj[Fields.BootState.GetName()].Value.Value.AsString;
			this.NodeType = obj[Fields.NodeType.GetName()].Value.Value.AsString;
			this.RunLevel = obj[Fields.RunLevel.GetName()].Value.Value.AsString;
			this.SshRsaKey = obj[Fields.SshRsaKey.GetName()].Value.Value.AsString;
			this.Hostname = obj[Fields.Hostname.GetName()].Value.Value.AsString;
			this.Version = obj[Fields.Version.GetName()].Value.Value.AsString;
			this.Model = obj[Fields.Model.GetName()].Value.Value.AsString;

			this.SiteId = obj[Fields.SiteId.GetName()].Value.Value.AsInt;
			this.NodeId = obj[Fields.NodeId.GetName()].Value.Value.AsInt;
			this.PeerId = obj[Fields.PeerId.GetName()].Value.Value.AsInt;
			this.PeerNodeId = obj[Fields.PeerNodeId.GetName()].Value.Value.AsInt;

			this.LastTimeSpentOffline = lastTimeSpentOffline.HasValue ? TimeSpan.FromMinutes(lastTimeSpentOffline.Value) as TimeSpan? : null;
			this.LastTimeSpentOnline = lastTimeSpentOnline.HasValue ? TimeSpan.FromMinutes(lastTimeSpentOnline.Value) as TimeSpan? : null;

			this.Verified = obj[Fields.Verified.GetName()].Value.Value.AsBoolean;

			this.Ports = obj[Fields.Ports.GetName()].Value.Value.AsArray<int>();

			this.PcuIds = obj[Fields.PcuIds.GetName()].Value.Value.AsArray<int>();
			this.InterfaceIds = obj[Fields.InterfaceIds.GetName()].Value.Value.AsArray<int>();
			this.SliceIds = obj[Fields.SliceIds.GetName()].Value.Value.AsArray<int>();
			this.NodeTagIds = obj[Fields.NodeTagIds.GetName()].Value.Value.AsArray<int>();
			this.NodeGroupIds = obj[Fields.NodeGroupIds.GetName()].Value.Value.AsArray<int>();
			this.SliceIdsWhitelist = obj[Fields.SliceIdsWhitelist.GetName()].Value.Value.AsArray<int>();
			this.ConfigurationFileIds = obj[Fields.ConfigurationFileIds.GetName()].Value.Value.AsArray<int>();

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
			return obj[Fields.NodeId.GetName()].Value.Value.AsInt;
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
