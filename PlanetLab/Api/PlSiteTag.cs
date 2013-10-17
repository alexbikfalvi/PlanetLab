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
	/// A class representing a PlanetLab site tag.
	/// </summary>
	public sealed class PlSiteTag : PlObject
	{
		public enum Fields
		{
			// Standard fields.
			[PlName("tagname"), PlDisplayName("Tag name")]
			TagName,
			[PlName("description"), PlDisplayName("Description")]
			Description,
			[PlName("category"), PlDisplayName("Category")]
			Category,
			[PlName("value"), PlDisplayName("Value")]
			Value,
			[PlName("tag_type_id"), PlDisplayName("Tag type ID")]
			TagTypeId,
			// Custom fields.
			[PlName("login_base"), PlDisplayName("Login base")]
			LoginBase,
			[PlName("site_tag_id"), PlDisplayName("Site tag ID")]
			SiteTagId,
			[PlName("site_id"), PlDisplayName("Site ID")]
			SiteId
		}

		/// <summary>
		/// Creates a default PlanetLab site tag object.
		/// </summary>
		public PlSiteTag()
		{
		}

		/// <summary>
		/// Creates a new PlanetLab site tag object from the specified object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public PlSiteTag(XmlRpcStruct obj)
		{
			this.Parse(obj);
		}

		// Public properties.

		public override int? Id { get { return this.SiteTagId; } }

		public string TagName { get; private set; }
		public string Description { get; private set; }
		public string Category { get; private set; }
		public string Value { get; private set; }

		public int? TagTypeId { get; private set; }

		public string LoginBase { get; private set; }

		public int? SiteTagId { get; private set; }
		public int? SiteId { get; private set; }

		// Public methods.

		/// <summary>
		/// Copies the data of the current object from the specified object.
		/// </summary>
		/// <param name="obj">The object.</param>
		public override void CopyFrom(PlObject obj)
		{
			// Convert the object.
			PlSiteTag item = obj as PlSiteTag;
			// Check the object is not null.
			if (null == item) throw new PlException("The object is null or not of the current type.");

			// Standard fields.
			this.TagName = item.TagName;
			this.Description = item.Description;
			this.Category = item.Category;
			this.Value = item.Value;

			this.TagTypeId = item.TagTypeId;

			// Custom fields.
			this.LoginBase = item.LoginBase;

			this.SiteTagId = item.SiteTagId;
			this.SiteId = item.SiteId;

			// Raise the changed event.
			base.OnChanged();
		}

		/// <summary>
		/// Parses the current PlanetLab object from the specified XML-RPC object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public override void Parse(XmlRpcStruct obj)
		{
			// Standard fields.
			this.TagName = obj[Fields.TagName.GetName()].Value.Value.AsString;
			this.Description = obj[Fields.Description.GetName()].Value.Value.AsString;
			this.Category = obj[Fields.Category.GetName()].Value.Value.AsString;
			this.Value = obj[Fields.Value.GetName()].Value.Value.AsString;

			this.TagTypeId = obj[Fields.TagTypeId.GetName()].Value.Value.AsInt;

			// Custom fields.
			this.LoginBase = obj[Fields.LoginBase.GetName()].Value.Value.AsString;

			this.SiteTagId = obj[Fields.SiteTagId.GetName()].Value.Value.AsInt;
			this.SiteId = obj[Fields.SiteId.GetName()].Value.Value.AsInt;

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
			return obj[Fields.SiteTagId.GetName()].Value.Value.AsInt;
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
