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
	/// A class representing a PlanetLab configuration file.
	/// </summary>
	public sealed class PlConfigurationFile : PlObject
	{
		public enum Fields
		{
			[PlName("source")]
			Source,
			[PlName("dest")]
			Destination,
			[PlName("file_permissions")]
			FilePermissions,
			[PlName("file_group")]
			FileGroup,
			[PlName("file_owner")]
			FileOwner,
			[PlName("enabled")]
			Enabled,
			[PlName("always_update")]
			AlwaysUpdate,
			[PlName("ignore_cmd_errors")]
			IgnoreCommandErrors,
			[PlName("preinstall_cmd")]
			PreinstallCommand,
			[PlName("postinstall_cmd")]
			PostinstallCommand,
			[PlName("error_cmd")]
			ErrorCommand,
			[PlName("conf_file_id")]
			ConfigurationFileId,
			[PlName("node_ids")]
			NodeIds,
			[PlName("nodegroup_ids")]
			NodeGroupIds
	}

		/// <summary>
		/// Creates a default PlanetLab configuration file object.
		/// </summary>
		public PlConfigurationFile()
		{
		}

		/// <summary>
		/// Creates a new PlanetLab configuration file object from the specified object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public PlConfigurationFile(XmlRpcStruct obj)
		{
			this.Parse(obj);
		}

		// Public properties.

		public override int? Id { get { return this.ConfigurationFileId; } }

		public string Source { get; private set; }
		public string Destination { get; private set; }
		public string FilePermissions { get; private set; }
		public string FileGroup { get; private set; }
		public string FileOwner { get; private set; }
		
		public bool? Enabled { get; private set; }
		public bool? AlwaysUpdate { get; private set; }
		public bool? IgnoreCommandErrors { get; private set; }
		
		public string PreinstallCommand { get; private set; }
		public string PostinstallCommand { get; private set; }
		public string ErrorCommand { get; private set; }
		
		public int? ConfigurationFileId { get; private set; }
		
		public int[] NodeIds { get; private set; }
		public int[] NodeGroupIds { get; private set; }

		// Public methods.

		/// <summary>
		/// Copies the data of the current object from the specified object.
		/// </summary>
		/// <param name="obj">The object.</param>
		public override void CopyFrom(PlObject obj)
		{
			// Convert the object.
			PlConfigurationFile item = obj as PlConfigurationFile;
			// Check the object is not null.
			if (null == item) throw new PlException("The object is null or not of the current type.");

			this.Source = item.Source;
			this.Destination = item.Destination;
			this.FilePermissions = item.FilePermissions;
			this.FileGroup = item.FileGroup;
			this.FileOwner = item.FileOwner;

			this.Enabled = item.Enabled;
			this.AlwaysUpdate = item.AlwaysUpdate;
			this.IgnoreCommandErrors = item.IgnoreCommandErrors;

			this.PreinstallCommand = item.PreinstallCommand;
			this.PostinstallCommand = item.PostinstallCommand;
			this.ErrorCommand = item.ErrorCommand;

			this.ConfigurationFileId = item.ConfigurationFileId;

			this.NodeIds = item.NodeIds;
			this.NodeGroupIds = item.NodeGroupIds;

			// Raise the changed event.
			base.OnChanged();
		}

		/// <summary>
		/// Parses the current PlanetLab object from the specified XML-RPC object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public override void Parse(XmlRpcStruct obj)
		{
			this.Source = obj[Fields.Source.GetName()].Value.Value.AsString;
			this.Destination = obj[Fields.Destination.GetName()].Value.Value.AsString;
			this.FilePermissions = obj[Fields.FilePermissions.GetName()].Value.Value.AsString;
			this.FileGroup = obj[Fields.FileGroup.GetName()].Value.Value.AsString;
			this.FileOwner = obj[Fields.FileOwner.GetName()].Value.Value.AsString;

			this.Enabled = obj[Fields.Enabled.GetName()].Value.Value.AsBoolean;
			this.AlwaysUpdate = obj[Fields.AlwaysUpdate.GetName()].Value.Value.AsBoolean;
			this.IgnoreCommandErrors = obj[Fields.IgnoreCommandErrors.GetName()].Value.Value.AsBoolean;

			this.PreinstallCommand = obj[Fields.PreinstallCommand.GetName()].Value.Value.AsString;
			this.PostinstallCommand = obj[Fields.PostinstallCommand.GetName()].Value.Value.AsString;
			this.ErrorCommand = obj[Fields.ErrorCommand.GetName()].Value.Value.AsString;

			this.ConfigurationFileId = obj[Fields.ConfigurationFileId.GetName()].Value.Value.AsInt;
			
			this.NodeIds = obj[Fields.NodeIds.GetName()].Value.Value.AsArray<int>();
			this.NodeGroupIds = obj[Fields.NodeGroupIds.GetName()].Value.Value.AsArray<int>();

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
			return obj[Fields.ConfigurationFileId.GetName()].Value.Value.AsInt;
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
