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
	/// A class representing a PlanetLab person.
	/// </summary>
	public sealed class PlPerson : PlObject
	{
		public enum Fields
		{
			[PlName("last_updated")]
			LastUpdated,
			[PlName("date_created")]
			DateCreated,
			[PlName("first_name")]
			FirstName,
			[PlName("last_name")]
			LastName,
			[PlName("title")]
			Title,
			[PlName("phone")]
			Phone,
			[PlName("email")]
			Email,
			[PlName("url")]
			Url,
			[PlName("bio")]
			Bio,
			[PlName("enabled")]
			IsEnabled,
			[PlName("person_id")]
			PersonId,
			[PlName("peer_id")]
			PeerId,
			[PlName("peer_person_id")]
			PeerPersonId,
			[PlName("role_ids")]
			RoleIds,
			[PlName("key_ids")]
			KeyIds,
			[PlName("slice_ids")]
			SliceIds,
			[PlName("site_ids")]
			SiteIds,
			[PlName("person_tag_ids")]
			PersonTagIds,
			[PlName("roles")]
			Roles
		}

		/// <summary>
		/// Creates a default PlanetLab person object.
		/// </summary>
		public PlPerson()
		{
		}

		/// <summary>
		/// Creates a new PlanetLab person object from the specified object.
		/// </summary>
		/// <param name="obj">The XML-RPC object.</param>
		public PlPerson(XmlRpcStruct obj)
		{
			this.Parse(obj);
		}

		// Public properties.

		public override int? Id { get { return this.PersonId; } }

		public DateTime? LastUpdated { get; private set; }
		public DateTime? DateCreated { get; private set; }

		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public string Title { get; private set; }
		public string Phone { get; private set; }
		public string Email { get; private set; }
		public string Url { get; private set; }
		public string Bio { get; private set; }

		public bool? IsEnabled { get; private set; }

		public int? PersonId { get; private set; }
		public int? PeerId { get; private set; }
		public int? PeerPersonId { get; private set; }

		public int[] RoleIds { get; private set; }
		public int[] KeyIds { get; private set; }
		public int[] SliceIds { get; private set; }
		public int[] SiteIds { get; private set; }
		public int[] PersonTagIds { get; private set; }

		public string[] Roles { get; private set; }

		// Public methods.

		/// <summary>
		/// Copies the data of the current object from the specified object.
		/// </summary>
		/// <param name="obj">The object.</param>
		public override void CopyFrom(PlObject obj)
		{
			// Convert the object.
			PlPerson item = obj as PlPerson;
			// Check the object is not null.
			if (null == item) throw new PlException("The object is null or not of the current type.");

			this.LastUpdated = item.LastUpdated;
			this.DateCreated = item.DateCreated;

			this.FirstName = item.FirstName;
			this.LastName = item.LastName;
			this.Title = item.Title;
			this.Phone = item.Phone;
			this.Email = item.Email;
			this.Url = item.Url;
			this.Bio = item.Bio;

			this.IsEnabled = item.IsEnabled;

			this.PersonId = item.PersonId;
			this.PeerId = item.PeerId;
			this.PeerPersonId = item.PeerPersonId;

			this.RoleIds = item.RoleIds;
			this.KeyIds = item.KeyIds;
			this.SliceIds = item.SliceIds;
			this.SiteIds = item.SiteIds;
			this.PersonTagIds = item.PersonTagIds;

			this.Roles = item.Roles;

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
			int? dateCreated = obj[Fields.DateCreated.GetName()].Value.Value.AsInt;

			this.LastUpdated = lastUpdated.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(lastUpdated.Value) : null;
			this.DateCreated = dateCreated.HasValue ? (DateTime?)PlDateTime.FromUnixTimestamp(dateCreated.Value) : null;

			this.FirstName = obj[Fields.FirstName.GetName()].Value.Value.AsString;
			this.LastName = obj[Fields.LastName.GetName()].Value.Value.AsString;
			this.Title = obj[Fields.Title.GetName()].Value.Value.AsString;
			this.Phone = obj[Fields.Phone.GetName()].Value.Value.AsString;
			this.Email = obj[Fields.Email.GetName()].Value.Value.AsString;
			this.Url = obj[Fields.Url.GetName()].Value.Value.AsString;
			this.Bio = obj[Fields.Bio.GetName()].Value.Value.AsString;

			this.IsEnabled = obj[Fields.IsEnabled.GetName()].Value.Value.AsBoolean;

			this.PersonId = obj[Fields.PersonId.GetName()].Value.Value.AsInt;
			this.PeerId = obj[Fields.PeerId.GetName()].Value.Value.AsInt;
			this.PeerPersonId = obj[Fields.PeerPersonId.GetName()].Value.Value.AsInt;

			this.RoleIds = obj[Fields.RoleIds.GetName()].Value.Value.AsArray<int>();
			this.KeyIds = obj[Fields.KeyIds.GetName()].Value.Value.AsArray<int>();
			this.SliceIds = obj[Fields.SliceIds.GetName()].Value.Value.AsArray<int>();
			this.SiteIds = obj[Fields.SiteIds.GetName()].Value.Value.AsArray<int>();
			this.PersonTagIds = obj[Fields.PersonTagIds.GetName()].Value.Value.AsArray<int>();

			this.Roles = obj[Fields.Roles.GetName()].Value.Value.AsArray<string>();

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
			return obj[Fields.PersonId.GetName()].Value.Value.AsInt;
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
