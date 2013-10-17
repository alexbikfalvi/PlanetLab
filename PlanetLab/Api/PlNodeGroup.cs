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
	/// A class representing a PlanetLab node group.
	/// </summary>
	public sealed class PlNodeGroup : PlObject
	{
		public enum Fields
		{
			[PlName("groupname"), PlDisplayName("Group name")]
			GroupName,
			[PlName("value"), PlDisplayName("Value")]
			Value,
			[PlName("tagname"), PlDisplayName("Tag name")]
			TagName,
			[PlName("nodegroup_id"), PlDisplayName("Node group ID")]
			NodeGroupId,
			[PlName("tag_type_id"), PlDisplayName("Tag type ID")]
			TagTypeId,
			[PlName("node_ids"), PlDisplayName("Node IDs")]
			NodeIds,
			[PlName("conf_file_ids"), PlDisplayName("Configuration file IDs")]
			ConfigurationFileIds
		}

		/// <summary>
		/// Creates a default PlanetLab node group object.
		/// </summary>
		public PlNodeGroup()
		{
		}

		/// <summary>
		/// Creates a new PlanetLab node group object from the specified object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public PlNodeGroup(XmlRpcStruct obj)
		{
			this.Parse(obj);
		}

		// Public properties.

		public override int? Id { get { return this.NodeGroupId; } }

		public string GroupName { get; private set; }
		public string Value { get; private set; }
		public string TagName { get; private set; }
		
		public int? NodeGroupId { get; private set; }
		public int? TagTypeId { get; private set; }
			
		public int[] NodeIds { get; private set; }
		public int[] ConfigurationFileIds { get; private set; }

		// Public methods.

		/// <summary>
		/// Copies the data of the current object from the specified object.
		/// </summary>
		/// <param name="obj">The object.</param>
		public override void CopyFrom(PlObject obj)
		{
			// Convert the object.
			PlNodeGroup item = obj as PlNodeGroup;
			// Check the object is not null.
			if (null == item) throw new PlException("The object is null or not of the current type.");

			this.GroupName = item.GroupName;
			this.Value = item.Value;
			this.TagName = item.TagName;
			this.NodeGroupId = item.NodeGroupId;
			this.TagTypeId = item.TagTypeId;
			this.NodeIds = item.NodeIds;
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
			this.GroupName = obj[Fields.GroupName.GetName()].Value.Value.AsString;
			this.Value = obj[Fields.Value.GetName()].Value.Value.AsString;
			this.TagName = obj[Fields.TagName.GetName()].Value.Value.AsString;
			this.NodeGroupId = obj[Fields.NodeGroupId.GetName()].Value.Value.AsInt;
			this.TagTypeId = obj[Fields.TagTypeId.GetName()].Value.Value.AsInt;
			this.NodeIds = obj[Fields.NodeIds.GetName()].Value.Value.AsArray<int>();
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
			return obj[Fields.NodeGroupId.GetName()].Value.Value.AsInt;
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
