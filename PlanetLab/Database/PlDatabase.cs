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
using System.Collections.Generic;
using DotNetApi.Concurrent;
using DotNetApi.Web.XmlRpc;
using PlanetLab.Api;

namespace PlanetLab.Database
{
	/// <summary>
	/// A class representing a PlanetLab database.
	/// </summary>
	/// <typeparam name="T">The PlanetLab object type.</typeparam>
	public sealed class PlDatabase<T> : ConcurrentBase where T : PlObject, new()
	{
		private readonly Dictionary<int, T> database = new Dictionary<int, T>();
		private readonly T reference = new T();

		/// <summary>
		/// Creates a new database instance.
		/// </summary>
		public PlDatabase()
		{
		}

		// Internal properties.

		/// <summary>
		/// Returns the PlanetLab object from the database corresponding to the specified object.
		/// </summary>
		/// <param name="obj">The PlantLab object.</param>
		/// <returns>The corresponding PlanetLab object.</returns>
		internal T this[T obj]
		{
			get
			{
				// Check the object is not null.
				if (null == obj) throw new ArgumentNullException("obj");
				// Check the object has a valid identifier.
				if (!obj.Id.HasValue) throw new PlException("The object identifier cannot be null.");

				// Acquire a reader lock.
				LockInfo info = this.AcquireReaderLock();
				try
				{
					return this.database[obj.Id.Value];
				}
				finally
				{
					// Release the reader lock.
					this.ReleaseReaderLock(info);
				}
			}
		}

		// Public methods.

		/// <summary>
		/// Adds the specified PlanetLab object to the current database. If the object does not exist, the database will add the specified instance.
		/// If the object exists, the database will copy to the existing instance the information of the specified object.
		/// </summary>
		/// <param name="obj">The PlanetLab object to add.</param>
		/// <returns>The object instance that was added/updated to/in the database.</returns>
		public T Add(T obj)
		{
			// Check the object is not null.
			if (null == obj) throw new ArgumentNullException("obj");
			// Check the object has a valid identifier.
			if (!obj.Id.HasValue) throw new PlException("The object identifier cannot be null.");
			
			// Acquire a writer lock.
			LockInfo info = this.AcquireWriterLock();
			try
			{
				// The database item.
				T item;
				// Get the current database item for the specified identifier.
				if (this.database.TryGetValue(obj.Id.Value, out item))
				{
					// If the object exists, update the infomation.
					item.CopyFrom(obj);
					// Return the item.
					return item;
				}
				else
				{
					// Else, add the new object to the database.
					this.database.Add(obj.Id.Value, obj);
					// Return the object.
					return obj;
				}
			}
			finally
			{
				// Release the writer lock.
				this.ReleaseWriterLock(info);
			}
		}

		/// <summary>
		/// Parses and adds the specified PlanetLab object to the current database. If the object does not exist, the database will add the specified instance.
		/// If the object exists, the database will copy to the existing instance the information of the specified object.
		/// </summary>
		/// <param name="obj">The XML-RPC structure to parse.</param>
		/// <returns>The object instance that was added/updated to/in the database.</returns>
		public T Add(XmlRpcStruct obj)
		{
			// Check the object is not null.
			if (null == obj) throw new ArgumentNullException("obj");

			// Get the object identifier using the reference.
			int? id = this.reference.ParseId(obj);

			// Check the object has a valid identifier.
			if (!id.HasValue) throw new PlException("The object identifier cannot be null.");

			// Acquire a writer lock.
			LockInfo info = this.AcquireWriterLock();
			try
			{
				// The database item.
				T item;
				// Get the current database item for the specified identifier.
				if (!this.database.TryGetValue(id.Value, out item))
				{
					// If the object does not exist in the database, create a new object.
					item = new T();
					// Add the new object to the database.
					this.database.Add(id.Value, item);
				}
				// Parse the object.
				item.Parse(obj);
				// Return the object.
				return item;
			}
			finally
			{
				// Release the writer lock.
				this.ReleaseWriterLock(info);
			}
		}

		/// <summary>
		/// Finds the PlanetLab object with the specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>The object instance, or <b>null</b> if the object is not found.</returns>
		public T Find(int id)
		{
			// Acquire a reader lock.
			LockInfo info = this.AcquireReaderLock();
			try
			{
				T obj;
				// Return the object.
				if (this.database.TryGetValue(id, out obj)) return obj;
				else return null;
			}
			finally
			{
				// Release the reader lock.
				this.ReleaseReaderLock(info);
			}
		}
	}
}
