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
	/// A class representing a PlanetLab PCU.
	/// </summary>
	public sealed class PlPcu : PlObject
	{
		public enum Fields
		{
			[PlName("last_updated"), PlDisplayName("Last updated")]
			LastUpdated,
			[PlName("hostname"), PlDisplayName("Hostname")]
			Hostname,
			[PlName("model"), PlDisplayName("Model")]
			Model,
			[PlName("username"), PlDisplayName("Username")]
			Username,
			[PlName("password"), PlDisplayName("Password")]
			Password,
			[PlName("protocol"), PlDisplayName("Protocol")]
			Protocol,
			[PlName("ip"), PlDisplayName("IP")]
			Ip,
			[PlName("notes"), PlDisplayName("Notes")]
			Notes,
			[PlName("pcu_id"), PlDisplayName("PCU ID")]
			PcuId,
			[PlName("site_id"), PlDisplayName("Site ID")]
			SiteId,
			[PlName("ports"), PlDisplayName("Ports")]
			Ports,
			[PlName("node_ids"), PlDisplayName("Node IDs")]
			NodeIds
		};

		/// <summary>
		/// Creates a default PlanetLab PCU object.
		/// </summary>
		public PlPcu()
		{
		}

		/// <summary>
		/// Creates a new PlanetLab PCU object from the specified object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public PlPcu(XmlRpcStruct obj)
		{
			this.Parse(obj);
		}

		//  Public properties.

		public override int? Id { get { return this.PcuId; } }

		public DateTime? LastUpdated { get; private set; }

		public string Hostname { get; private set; }
		public string Model { get; private set; }
		public string Username { get; private set; }
		public string Password { get; private set; }
		public string Protocol { get; private set; }
		public string Ip { get; private set; }
		public string Notes { get; private set; }

		public int? PcuId { get; private set; }
		public int? SiteId { get; private set; }

		public int[] Ports { get; private set; }

		public int[] NodeIds { get; private set; }

		// Public methods.

		/// <summary>
		/// Copies the data of the current object from the specified object.
		/// </summary>
		/// <param name="obj">The object.</param>
		public override void CopyFrom(PlObject obj)
		{
			// Convert the object.
			PlPcu item = obj as PlPcu;
			// Check the object is not null.
			if (null == item) throw new PlException("The object is null or not of the current type.");

			this.LastUpdated = item.LastUpdated;

			this.Hostname = item.Hostname;
			this.Model = item.Model;
			this.Username = item.Username;
			this.Password = item.Password;
			this.Protocol = item.Protocol;
			this.Ip = item.Ip;
			this.Notes = item.Notes;

			this.PcuId = item.PcuId;
			this.SiteId = item.SiteId;

			this.Ports = item.Ports;

			this.NodeIds = item.NodeIds;

			// Raise the changed event.
			base.OnChanged();
		}

		/// <summary>
		/// Parses the current PlanetLab object from the specified XML-RPC object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public override void Parse(XmlRpcStruct obj)
		{
			int? lastUpdated = obj[Fields.Hostname.GetName()].Value.Value.AsInt;

			this.LastUpdated = lastUpdated.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(lastUpdated.Value) : null;

			this.Hostname = obj[Fields.Hostname.GetName()].Value.Value.AsString;
			this.Model = obj[Fields.Model.GetName()].Value.Value.AsString;
			this.Username = obj[Fields.Username.GetName()].Value.Value.AsString;
			this.Password = obj[Fields.Password.GetName()].Value.Value.AsString;
			this.Protocol = obj[Fields.Protocol.GetName()].Value.Value.AsString;
			this.Ip = obj[Fields.Ip.GetName()].Value.Value.AsString;
			this.Notes = obj[Fields.Notes.GetName()].Value.Value.AsString;

			this.PcuId = obj[Fields.PcuId.GetName()].Value.Value.AsInt;
			this.SiteId = obj[Fields.SiteId.GetName()].Value.Value.AsInt;

			this.Ports = obj[Fields.Ports.GetName()].Value.Value.AsArray<int>();

			this.NodeIds = obj[Fields.NodeIds.GetName()].Value.Value.AsArray<int>();

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
			return obj[Fields.PcuId.GetName()].Value.Value.AsInt;
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
