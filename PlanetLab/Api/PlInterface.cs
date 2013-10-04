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
	/// A class representing a PlanetLab interface.
	/// </summary>
	public sealed class PlInterface : PlObject
	{
		public enum Fields
		{
			[PlName("is_primary")]
			IsPrimary,
			[PlName("last_updated")]
			LastUpdated,
			[PlName("type")]
			Type,
			[PlName("hostname")]
			Hostname,
			[PlName("mac")]
			Mac,
			[PlName("ip")]
			Ip,
			[PlName("netmask")]
			Netmask,
			[PlName("network")]
			Network,
			[PlName("broadcast")]
			Broadcast,
			[PlName("gateway")]
			Gateway,
			[PlName("bwlimit")]
			BandwidthLimit,
			[PlName("dns1")]
			Dns1,
			[PlName("dns2")]
			Dns2,
			[PlName("method")]
			Method,
			[PlName("interface_id")]
			InterfaceId,
			[PlName("node_id")]
			NodeId,
			[PlName("interface_tag_ids")]
			InterfaceTagIds
		}

		/// <summary>
		/// Creates a default PlanetLab interface object.
		/// </summary>
		public PlInterface()
		{
		}

		/// <summary>
		/// Creates a new PlanetLab interface object from the specified object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public PlInterface(XmlRpcStruct obj)
		{
			this.Parse(obj);
		}

		// Public properties.

		public override int? Id { get { return this.InterfaceId; } }

		public DateTime? LastUpdated { get; private set; }

		public bool? IsPrimary { get; private set; }

		public string Type { get; private set; }
		public string Hostname { get; private set; }
		public string Mac { get; private set; }
		public string Ip { get; private set; }
		public string Netmask { get; private set; }
		public string Network { get; private set; }
		public string Broadcast { get; private set; }
		public string Gateway { get; private set; }
		public int? BandwidthLimit { get; private set; }
		public string Dns1 { get; private set; }
		public string Dns2 { get; private set; }
		public string Method { get; private set; }

		public int? InterfaceId { get; private set; }
		public int? NodeId { get; private set; }

		public int[] InterfaceTagIds { get; private set; }

		// Public methods.

		/// <summary>
		/// Copies the data of the current object from the specified object.
		/// </summary>
		/// <param name="obj">The object.</param>
		public override void CopyFrom(PlObject obj)
		{
			// Convert the object.
			PlInterface item = obj as PlInterface;
			// Check the object is not null.
			if (null == item) throw new PlException("The object is null or not of the current type.");

			this.LastUpdated = item.LastUpdated;

			this.IsPrimary = item.IsPrimary;

			this.Type = item.Type;
			this.Hostname = item.Hostname;
			this.Mac = item.Mac;
			this.Ip = item.Ip;
			this.Netmask = item.Netmask;
			this.Network = item.Network;
			this.Broadcast = item.Broadcast;
			this.Gateway = item.Gateway;
			this.BandwidthLimit = item.BandwidthLimit;
			this.Dns1 = item.Dns1;
			this.Dns2 = item.Dns2;
			this.Method = item.Method;

			this.InterfaceId = item.InterfaceId;
			this.NodeId = item.NodeId;

			this.InterfaceTagIds = item.InterfaceTagIds;

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

			this.LastUpdated = lastUpdated.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(lastUpdated.Value) : null;

			this.IsPrimary = obj[Fields.IsPrimary.GetName()].Value.Value.AsBoolean;

			this.Type = obj[Fields.Type.GetName()].Value.Value.AsString;
			this.Hostname = obj[Fields.Hostname.GetName()].Value.Value.AsString;
			this.Mac = obj[Fields.Mac.GetName()].Value.Value.AsString;
			this.Ip = obj[Fields.Ip.GetName()].Value.Value.AsString;
			this.Netmask = obj[Fields.Netmask.GetName()].Value.Value.AsString;
			this.Network = obj[Fields.Network.GetName()].Value.Value.AsString;
			this.Broadcast = obj[Fields.Broadcast.GetName()].Value.Value.AsString;
			this.Gateway = obj[Fields.Gateway.GetName()].Value.Value.AsString;
			this.BandwidthLimit = obj[Fields.BandwidthLimit.GetName()].Value.Value.AsInt;
			this.Dns1 = obj[Fields.Dns1.GetName()].Value.Value.AsString;
			this.Dns2 = obj[Fields.Dns2.GetName()].Value.Value.AsString;
			this.Method = obj[Fields.Method.GetName()].Value.Value.AsString;

			this.InterfaceId = obj[Fields.InterfaceId.GetName()].Value.Value.AsInt;
			this.NodeId = obj[Fields.NodeId.GetName()].Value.Value.AsInt;

			this.InterfaceTagIds = obj[Fields.InterfaceTagIds.GetName()].Value.Value.AsArray<int>();

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
			return obj[Fields.InterfaceId.GetName()].Value.Value.AsInt;
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
