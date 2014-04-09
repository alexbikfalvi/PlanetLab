/* 
 * Copyright (C) 2012-2013 Alex Bikfalvi
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
using System.Security;
using Microsoft.Win32;
using DotNetApi;
using DotNetApi.Security;
using InetCommon.Net;
using InetCommon.Status;
using PlanetLab.Api;
using PlanetLab.Database;

namespace PlanetLab
{
	/// <summary>
	/// A class representing the PlanetLab console configuration.
	/// </summary>
	public class Config : IDisposable
	{
		/// <summary>
		/// A class representing the static configuration.
		/// </summary>
		public sealed class StaticConfig
		{
			public string ApplicationFolder { get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Alex Bikfalvi\PlanetLab Manager"; } }
			public TimeSpan ConsoleMessageCloseDelay { get; internal set; }
			public int ConsoleSideMenuVisibleItems { get; internal set; }
			public int ConsoleSideMenuSelectedItem { get; internal set; }
			public int[] ConsoleSideMenuSelectedNode { get; internal set; }
			public string PlanetLabUsername { get; internal set; }
			public SecureString PlanetLabPassword { get; internal set; }
			public int PlanetLabPersonId { get; internal set; }
			public string PlanetLabSitesFileName { get; internal set; }
			public string PlanetLabNodesFileName { get; internal set; }
			public string PlanetLabSlicesFileName { get; internal set; }
			public string PlanetLabLocalPersonsFileName { get; internal set; }
			public string PlanetLabLocalSlicesFileName { get; internal set; }
			public string PlanetLabSlicesFolder { get; internal set; }
			public string PlanetLabSlicesLogFileName { get; internal set; }
		}

		internal static readonly byte[] cryptoKey = { 0x7E, 0x31, 0xC9, 0xD9, 0x0F, 0x01, 0x28, 0x11, 0xD4, 0xB3, 0xC4, 0x12, 0x44, 0x0D, 0x4B, 0x8C, 0xEE, 0xDB, 0xF1, 0x4D, 0xCF, 0x01, 0xF8, 0x00, 0xE4, 0x3E, 0x33, 0xF5, 0x02, 0x48, 0xD4, 0x8D };
		internal static readonly byte[] cryptoIV = { 0x62, 0x36, 0x61, 0x5B, 0xA0, 0xA4, 0xF0, 0x4E, 0xFE, 0x81, 0x2C, 0xA4, 0xD3, 0x16, 0x26, 0xEE };

		private RegistryKey key;
		private RegistryKey keySlices;

		private string root;

		private readonly PlDatabase<PlSite> dbSites = new PlDatabase<PlSite>();
		private readonly PlDatabase<PlNode> dbNodes = new PlDatabase<PlNode>();
		private readonly PlDatabase<PlPerson> dbPersons = new PlDatabase<PlPerson>();
		private readonly PlDatabase<PlSlice> dbSlices = new PlDatabase<PlSlice>();

		private readonly PlDatabaseList<PlSite> listSites;
		private readonly PlDatabaseList<PlNode> listNodes;
		private readonly PlDatabaseList<PlSlice> listSlices;
		private readonly PlDatabaseList<PlPerson> listLocalPersons;
		private readonly PlDatabaseList<PlSlice> listLocalSlices;

		private readonly Dictionary<int, ConfigSlice> configSlices = new Dictionary<int, ConfigSlice>();

		private readonly ApplicationStatus status;
		private readonly ApplicationEvents events;

		/// <summary>
		/// Static constructor.
		/// </summary>
		static Config()
		{
			// Create the static configuration.
			Config.Static = new StaticConfig();
		}

		/// <summary>
		/// Creates a new crawer global object, based on a configuration from the specified root registry key.
		/// </summary>
		/// <param name="rootKey">The root registry key.</param>
		/// <param name="rootPat">The registry key path.</param>
		public Config(RegistryKey rootKey, string rootPath)
		{
			// Open the PlanetLab configuration key.
			if (null == (this.key = rootKey.OpenSubKey(rootPath, RegistryKeyPermissionCheck.ReadWriteSubTree)))
			{
				this.key = rootKey.CreateSubKey(rootPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
			}
			// Open the PlanetLab slices configuration key.
			if (null == (this.keySlices = this.key.OpenSubKey("Slices", RegistryKeyPermissionCheck.ReadWriteSubTree)))
			{
				this.keySlices = this.key.CreateSubKey("Slices", RegistryKeyPermissionCheck.ReadWriteSubTree);
			}

			// Set the root path.
			this.root = @"{0}\{1}".FormatWith(rootKey.Name, rootPath);

			// Create the status.
			this.status = new ApplicationStatus();

			// Create the events.
			this.events = new ApplicationEvents();

			// Create the PlanetLab lists.
			this.listSites = new PlDatabaseList<PlSite>(this.dbSites);
			this.listNodes = new PlDatabaseList<PlNode>(this.dbNodes);
			this.listSlices = new PlDatabaseList<PlSlice>(this.dbSlices);
			this.listLocalPersons = new PlDatabaseList<PlPerson>(this.dbPersons);
			this.listLocalSlices = new PlDatabaseList<PlSlice>(this.dbSlices);

			// Set the lists event handlers.
			this.listLocalSlices.Cleared += this.OnSlicesCleared;
			this.listLocalSlices.Updated += this.OnSlicesUpdated;
			this.listLocalSlices.Added += this.OnSlicesAdded;
			this.listLocalSlices.Removed += this.OnSlicesRemoved;

			// Initialize the static configuration.
			Config.Static.PlanetLabUsername = this.Username;
			Config.Static.PlanetLabPassword = this.Password;
			Config.Static.PlanetLabPersonId = this.PersonId;
			Config.Static.PlanetLabSitesFileName = this.SitesFileName;
			Config.Static.PlanetLabNodesFileName = this.NodesFileName;
			Config.Static.PlanetLabLocalPersonsFileName = this.LocalPersonsFileName;
			Config.Static.PlanetLabLocalSlicesFileName = this.LocalSlicesFileName;
			Config.Static.PlanetLabSlicesFolder = this.SlicesFolder;
			Config.Static.PlanetLabSlicesLogFileName = this.SlicesLogFileName;
			Config.Static.ConsoleMessageCloseDelay = this.ConsoleMessageCloseDelay;
			Config.Static.ConsoleSideMenuVisibleItems = this.ConsoleSideMenuVisibleItems;
			Config.Static.ConsoleSideMenuSelectedItem = this.ConsoleSideMenuSelectedItem;
			Config.Static.ConsoleSideMenuSelectedNode = this.ConsoleSideMenuSelectedNode;

			// Load the PlanetLab sites configuration.
			try { this.listSites.LoadFromFile(this.SitesFileName); }
			catch { }

			// Load the PlanetLab nodes configuration.
			try { this.listNodes.LoadFromFile(this.NodesFileName); }
			catch { }

			// Load the PlanetLab slices configuration.
			try { this.listSlices.LoadFromFile(this.SlicesFileName); }
			catch { }

			// Load the PlanetLab local persons configuration.
			try { this.listLocalPersons.LoadFromFile(this.LocalPersonsFileName); }
			catch { }

			// Load the PlanetLab local slices configuration.
			try { this.listLocalSlices.LoadFromFile(this.LocalSlicesFileName); }
			catch { }
		}

		#region Static properties

		/// <summary>
		/// Gets the static configuration.
		/// </summary>
		public static StaticConfig Static { get; private set; }

		#endregion

		#region Public properties

		/// <summary>
		/// Returns the application status.
		/// </summary>
		public ApplicationStatus Status { get { return this.status; } }
		/// <summary>
		/// Gets the application events.
		/// </summary>
		public ApplicationEvents Events { get { return this.events; } }
		/// <summary>
		/// Gets or sets the delay to display a user message, after the operation generating the message has completed.
		/// </summary>
		public TimeSpan ConsoleMessageCloseDelay
		{
			get
			{
				return DotNetApi.Windows.RegistryExtensions.GetTimeSpan(this.root + @"\Console", "MessageCloseDelay", TimeSpan.FromMilliseconds(1000));
			}
			set
			{
				DotNetApi.Windows.RegistryExtensions.SetTimeSpan(this.root + @"\Console", "MessageCloseDelay", value);
				Config.Static.ConsoleMessageCloseDelay = value;
			}
		}
		/// <summary>
		/// Gets or sets the number of side menu visible items.
		/// </summary>
		public int ConsoleSideMenuVisibleItems
		{
			get
			{
				return DotNetApi.Windows.RegistryExtensions.GetInteger(this.root + @"\Console", "SideMenuVisibleItems", 4);
			}
			set
			{
				DotNetApi.Windows.RegistryExtensions.SetInteger(this.root + @"\Console", "SideMenuVisibleItems", value);
				Config.Static.ConsoleSideMenuVisibleItems = value;
			}
		}
		/// <summary>
		/// Gets or sets the number of side menu selected item.
		/// </summary>
		public int ConsoleSideMenuSelectedItem
		{
			get
			{
				return DotNetApi.Windows.RegistryExtensions.GetInteger(this.root + @"\Console", "SideMenuSelectedItem", 0);
			}
			set
			{
				DotNetApi.Windows.RegistryExtensions.SetInteger(this.root + @"\Console", "SideMenuSelectedItem", value);
				Config.Static.ConsoleSideMenuSelectedItem = value;
			}
		}
		/// <summary>
		/// Gets or sets the indices of side menu selected node.
		/// </summary>
		public int[] ConsoleSideMenuSelectedNode
		{
			get
			{
				return DotNetApi.Windows.RegistryExtensions.GetInt32Array(this.root + @"\Console", "SideMenuSelectedNode", null);
			}
			set
			{
				if (null == value) return;
				DotNetApi.Windows.RegistryExtensions.SetInt32Array(this.root + @"\Console", "SideMenuSelectedNode", value);
				Config.Static.ConsoleSideMenuSelectedNode = value;
			}
		}
		/// <summary>
		/// Gets or sets the PlanetLab account name.
		/// </summary>
		public string Username
		{
			get
			{
				return DotNetApi.Windows.RegistryExtensions.GetString(this.root, "UserName", string.Empty);
			}
		}
		/// <summary>
		/// Gets or sets the PlanetLab account password.
		/// </summary>
		public SecureString Password
		{
			get
			{
				return DotNetApi.Windows.RegistryExtensions.GetSecureString(this.root, "Password", SecureStringExtensions.Empty, Config.cryptoKey, Config.cryptoIV);
			}
		}
		/// <summary>
		/// Gets or sets the PlanetLab default person ID.
		/// </summary>
		public int PersonId
		{
			get
			{
				return DotNetApi.Windows.RegistryExtensions.GetInteger(this.root, "PersonId", -1);
			}
		}
		/// <summary>
		/// Gets or sets the PlanetLab sites file name.
		/// </summary>
		public string SitesFileName
		{
			get
			{
				return DotNetApi.Windows.RegistryExtensions.GetString(this.root, "SitesFileName", Config.Static.ApplicationFolder + @"\PlanetLab\Sites.xml");
			}
			set
			{
				DotNetApi.Windows.RegistryExtensions.SetString(this.root, "SitesFileName", value);
				Config.Static.PlanetLabSitesFileName = value;
			}
		}
		/// <summary>
		/// Gets or sets the PlanetLab nodes file name.
		/// </summary>
		public string NodesFileName
		{
			get
			{
				return DotNetApi.Windows.RegistryExtensions.GetString(this.root, "NodesFileName", Config.Static.ApplicationFolder + @"\PlanetLab\Nodes.xml");
			}
			set
			{
				DotNetApi.Windows.RegistryExtensions.SetString(this.root, "NodesFileName", value);
				Config.Static.PlanetLabNodesFileName = value;
			}
		}
		/// <summary>
		/// Gets or sets the PlanetLab slices file name.
		/// </summary>
		public string SlicesFileName
		{
			get
			{
				return DotNetApi.Windows.RegistryExtensions.GetString(this.root, "SlicesFileName", Config.Static.ApplicationFolder + @"\PlanetLab\Slices.xml");
			}
			set
			{
				DotNetApi.Windows.RegistryExtensions.SetString(this.root, "SlicesFileName", value);
				Config.Static.PlanetLabSlicesFileName = value;
			}
		}
		/// <summary>
		/// Gets or sets the local PlanetLab persons file name.
		/// </summary>
		public string LocalPersonsFileName
		{
			get
			{
				return DotNetApi.Windows.RegistryExtensions.GetString(this.root, "LocalPersonsFileName", Config.Static.ApplicationFolder + @"\PlanetLab\LocalPersons.xml");
			}
			set
			{
				DotNetApi.Windows.RegistryExtensions.SetString(this.root, "LocalPersonsFileName", value);
				Config.Static.PlanetLabLocalPersonsFileName = value;
			}
		}
		/// <summary>
		/// Gets or sets the local PlanetLab slices file name.
		/// </summary>
		public string LocalSlicesFileName
		{
			get
			{
				return DotNetApi.Windows.RegistryExtensions.GetString(this.root, "LocalSlicesFileName", Config.Static.ApplicationFolder + @"\PlanetLab\LocalSlices.xml");
			}
			set
			{
				DotNetApi.Windows.RegistryExtensions.SetString(this.root, "LocalSlicesFileName", value);
				Config.Static.PlanetLabLocalSlicesFileName = value;
			}
		}
		/// <summary>
		/// Gets or sets the PlanetLab slices folder.
		/// </summary>
		public string SlicesFolder
		{
			get
			{
				return DotNetApi.Windows.RegistryExtensions.GetString(this.root, "SlicesFolder", Config.Static.ApplicationFolder + @"\PlanetLab\Slices");
			}
			set
			{
				DotNetApi.Windows.RegistryExtensions.SetString(this.root, "SlicesFolder", value);
				Config.Static.PlanetLabSlicesFolder = value;
			}
		}
		/// <summary>
		/// Gets or sets the PlanetLab slices log file name.
		/// </summary>
		public string SlicesLogFileName
		{
			get
			{
				return DotNetApi.Windows.RegistryExtensions.GetString(this.root, "SlicesLogFileName", Config.Static.ApplicationFolder + @"\PlanetLab\Slices\Log-{0}-{1}-{2}-{3}.xml");
			}
			set
			{
				DotNetApi.Windows.RegistryExtensions.SetString(this.root, "SlicesLogFileName", value);
				Config.Static.PlanetLabSlicesLogFileName = value;
			}
		}
		/// <summary>
		/// Gets the sites database.
		/// </summary>
		public PlDatabase<PlSite> DbSites { get { return this.dbSites; } }
		/// <summary>
		/// Gets the nodes database.
		/// </summary>
		public PlDatabase<PlNode> DbNodes { get { return this.dbNodes; } }
		/// <summary>
		/// Gets the slices database.
		/// </summary>
		public PlDatabase<PlSlice> DbSlices { get { return this.dbSlices; } }
		/// <summary>
		/// Gets the persons database.
		/// </summary>
		public PlDatabase<PlPerson> DbPersons { get { return this.dbPersons; } }
		/// <summary>
		/// Gets the collection of PlanetLab sites.
		/// </summary>
		public PlDatabaseList<PlSite> Sites { get { return this.listSites; } }
		/// <summary>
		/// Gets the collection of PlanetLab nodes.
		/// </summary>
		public PlDatabaseList<PlNode> Nodes { get { return this.listNodes; } }
		/// <summary>
		/// Gets the collection of PlanetLab slices.
		/// </summary>
		public PlDatabaseList<PlSlice> Slices { get { return this.listSlices; } }
		/// <summary>
		/// Gets the collection of PlanetLab persons.
		/// </summary>
		public PlDatabaseList<PlPerson> LocalPersons { get { return this.listLocalPersons; } }
		/// <summary>
		/// Gets the collection of PlanetLab slices.
		/// </summary>
		public PlDatabaseList<PlSlice> LocalSlices { get { return this.listLocalSlices; } }

		#endregion

		#region Public methods

		/// <summary>
		/// A method called when the object is disposed.
		/// </summary>
		public void Dispose()
		{
			// Call the dispose event handler.
			this.Dispose(true);
			// Suppress the finalizer.
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Saves the PlanetLab credentials.
		/// </summary>
		/// <param name="username">The PlanetLab username.</param>
		/// <param name="password">The PlanetLab persons.</param>
		/// <param name="persons">The list of person accounts associated with the previous credentials.</param>
		/// <param name="person">The default person account ID.</param>
		public void SaveCredentials(string username, SecureString password, PlList<PlPerson> persons, int person)
		{
			// Save the username.
			DotNetApi.Windows.RegistryExtensions.SetString(this.root, "UserName", username);
			Config.Static.PlanetLabUsername = username;
			// Save the password.
			DotNetApi.Windows.RegistryExtensions.SetSecureString(this.root, "Password", password, Config.cryptoKey, Config.cryptoIV);
			Config.Static.PlanetLabPassword = password;
			// Save the persons.
			persons.Lock();
			try { this.LocalPersons.CopyFrom(persons); }
			finally { persons.Unlock(); }
			try { this.LocalPersons.SaveToFile(this.LocalPersonsFileName); }
			catch { }
			// Save the person.
			DotNetApi.Windows.RegistryExtensions.SetInteger(this.root, "PersonId", person);
			Config.Static.PlanetLabPersonId = person;
		}

		/// <summary>
		/// Returns the configuration for the specified slice.
		/// </summary>
		/// <param name="slice">The slice.</param>
		/// <returns>The slice configuration.</returns>
		public ConfigSlice GetSliceConfiguration(PlSlice slice)
		{
			// Validate the arguments.
			if (null == slice) throw new ArgumentNullException("slice");
			// If the slice does not have a valid identifier, throw an exception.
			if (!slice.Id.HasValue) throw new ConfigException("The slice does not have a valid identifier.");

			// Return the configuration.
			ConfigSlice configSlice;
			// Try and get the configuration.
			if (!this.configSlices.TryGetValue(slice.Id.Value, out configSlice))
			{
				// If the configuration does not exist, throw an exception.
				throw new ConfigException("The specified slice does not have a configuration.");
			}

			return configSlice;
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Disposes the current object.
		/// </summary>
		/// <param name="disposing">If <b>true</b>, clean both managed and native resources. If <b>false</b>, clean only native resources.</param>
		private void Dispose(bool disposing)
		{
			// Dispose the current objects.
			if (disposing)
			{
				// Close the status.
				this.status.Dispose();
				
				// Save the PlanetLab sites.
				if (this.Sites.IsDirty)
				{
					try { this.Sites.SaveToFile(this.SitesFileName); }
					catch { }
				}
				// Save the PlanetLab nodes.
				if (this.Nodes.IsDirty)
				{
					try { this.Nodes.SaveToFile(this.NodesFileName); }
					catch { }
				}
				// Save the PlanetLab slices.
				if (this.Slices.IsDirty)
				{
					try { this.Slices.SaveToFile(this.SlicesFileName); }
					catch { }
				}
				// Save the PlanetLab local slices.
				if (this.LocalSlices.IsDirty)
				{
					try { this.LocalSlices.SaveToFile(this.LocalSlicesFileName); }
					catch { }
				}
				
				// Dispose the slices configuration.
				foreach (ConfigSlice configSlice in this.configSlices.Values)
				{
					configSlice.Dispose();
				}
				
				// Close the registry key.
				this.keySlices.Close();
				this.key.Close();
			}
		}

		/// <summary>
		/// An event handler called when the list of slices is cleared.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnSlicesCleared(object sender, EventArgs e)
		{
			// For all slices.
			foreach (ConfigSlice configSlice in this.configSlices.Values)
			{
				// Dispose the configuration.
				configSlice.Dispose();
				// Delete the configuration key.
				ConfigSlice.Delete(configSlice, this.keySlices);
			}
			// Clear the slices configurations.
			this.configSlices.Clear();
		}

		/// <summary>
		/// An event handler called when the list of slices is updated.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnSlicesUpdated(object sender, EventArgs e)
		{
			// Lock the slices list.
			this.listLocalSlices.Lock();
			try
			{
				// For each slice.
				foreach (PlSlice slice in this.listLocalSlices)
				{
					// If the slice has a valid identifier.
					if (slice.Id.HasValue)
					{
						// Create a new slice configuration.
						ConfigSlice configSlice = new ConfigSlice(slice, this.keySlices);
						// Add the configuration to the dictionary.
						this.configSlices.Add(slice.Id.Value, configSlice);
					}
				}
			}
			finally
			{
				// Unlock the slices list.
				this.listLocalSlices.Unlock();
			}
		}

		/// <summary>
		/// An event handler called when a slice is added.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnSlicesAdded(object sender, PlObjectEventArgs<PlSlice> e)
		{
			// If the slice has a valid identifier.
			if (e.Object.Id.HasValue)
			{
				// Create a new slice configuration.
				ConfigSlice configSlice = new ConfigSlice(e.Object, this.keySlices);
				// Add the configuration to the dictionary.
				this.configSlices.Add(e.Object.Id.Value, configSlice);
			}
		}

		/// <summary>
		/// An event handler called when a slice is removed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnSlicesRemoved(object sender, PlObjectEventArgs<PlSlice> e)
		{
			// If the slice has a valid identifier.
			if (e.Object.Id.HasValue)
			{
				// Get the current configuration.
				ConfigSlice configSlice;
				if (this.configSlices.TryGetValue(e.Object.Id.Value, out configSlice))
				{
					// Remove the configuration.
					this.configSlices.Remove(e.Object.Id.Value);
					// Dispose the configuration.
					configSlice.Dispose();
					// Delete the configuration.
					ConfigSlice.Delete(configSlice, this.keySlices);
				}
			}
		}

		#endregion
	}
}
