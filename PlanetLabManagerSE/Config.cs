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
using System.IO;
using System.Security;
using System.Xml.Linq;
using Microsoft.Win32;
using DotNetApi;
using DotNetApi.Security;
using DotNetApi.Web;
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
			public string Username { get; internal set; }
			public SecureString Password { get; internal set; }
			public int PersonId { get; internal set; }
		}

		internal static readonly byte[] cryptoKey = { 0x7E, 0x31, 0xC9, 0xD9, 0x0F, 0x01, 0x28, 0x11, 0xD4, 0xB3, 0xC4, 0x12, 0x44, 0x0D, 0x4B, 0x8C, 0xEE, 0xDB, 0xF1, 0x4D, 0xCF, 0x01, 0xF8, 0x00, 0xE4, 0x3E, 0x33, 0xF5, 0x02, 0x48, 0xD4, 0x8D };
		internal static readonly byte[] cryptoIV = { 0x62, 0x36, 0x61, 0x5B, 0xA0, 0xA4, 0xF0, 0x4E, 0xFE, 0x81, 0x2C, 0xA4, 0xD3, 0x16, 0x26, 0xEE };

		private readonly RegistryKey key;
		private readonly string root;

		private bool loaded = false;

		private readonly PlDatabase<PlSite> dbSites = new PlDatabase<PlSite>();
		private readonly PlDatabase<PlNode> dbNodes = new PlDatabase<PlNode>();
		private readonly PlDatabase<PlPerson> dbPersons = new PlDatabase<PlPerson>();
		private readonly PlDatabase<PlSlice> dbSlices = new PlDatabase<PlSlice>();

		private readonly AsyncWebRequest request = new AsyncWebRequest();

		private string username;
		private SecureString password;
		private int personId;
		private readonly Dictionary<int, byte[]> slices = new Dictionary<int, byte[]>();
		private readonly HashSet<string> commands = new HashSet<string>();

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
		/// <param name="rootPath">The registry key path.</param>
		public Config(RegistryKey rootKey, string rootPath)
		{
			// Open the PlanetLab configuration key.
			if (null == (this.key = rootKey.OpenSubKey(rootPath, RegistryKeyPermissionCheck.ReadWriteSubTree)))
			{
				this.key = rootKey.CreateSubKey(rootPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
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

			// Initialize the static configuration.
			Config.Static.ConsoleMessageCloseDelay = this.ConsoleMessageCloseDelay;
			Config.Static.ConsoleSideMenuVisibleItems = this.ConsoleSideMenuVisibleItems;
			Config.Static.ConsoleSideMenuSelectedItem = this.ConsoleSideMenuSelectedItem;
			Config.Static.ConsoleSideMenuSelectedNode = this.ConsoleSideMenuSelectedNode;
		}

		#region Static properties

		/// <summary>
		/// Gets the static configuration.
		/// </summary>
		public static StaticConfig Static { get; private set; }

		#endregion

		#region Public properties

		/// <summary>
		/// Gets whether the configuration has been loaded.
		/// </summary>
		public bool Loaded { get { return this.loaded; } }
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
		/// Gets the PlanetLab username.
		/// </summary>
		public string Username
		{
			get { return this.username; }
			private set
			{
				this.username = value;
				Config.Static.Username = value;
			}
		}
		/// <summary>
		/// Gets the PlanetLab password.
		/// </summary>
		public SecureString Password
		{
			get { return this.password; }
			private set
			{
				this.password = value;
				Config.Static.Password = value;
			}
		}
		/// <summary>
		/// Gets the PlanetLab person identifier.
		/// </summary>
		public int PersonId
		{
			get { return this.personId; }
			private set
			{
				this.personId = value;
				Config.Static.PersonId = value;
			}
		}
		/// <summary>
		/// Gets the date when the configuration expires.
		/// </summary>
		public DateTime Expires { get; private set; }
		/// <summary>
		/// Gets the set of allowed commands.
		/// </summary>
		public ISet<string> Commands { get { return this.commands; } }
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
		/// Loads the current configuration.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="callback">A callback method.</param>
		public void Load(Uri url, Action<bool, Exception> callback)
		{
			// Load the configuration.
			this.request.Begin(url, (AsyncWebResult asyncResult) =>
				{
					try
					{
						string xml;
						// Complete the request.
						this.request.End(asyncResult, out xml);

						// Parse the encrypted XML file.
						this.ParseEncryptedConfig(xml);

						// Call the user callback method.
						if (null != callback) callback(true, null);
					}
					catch (Exception exception)
					{
						// Call the user callback method.
						if (null != callback) callback(false, exception);
					}
				}, null);
		}

		/// <summary>
		/// Returns whether the specified slice is allowed.
		/// </summary>
		/// <param name="slice">The slice.</param>
		/// <returns><b>True</b> if the slice is allowed, <b>false</b> otherwise.</returns>
		public bool IsSliceAllowed(PlSlice slice)
		{
			return this.slices.ContainsKey(slice.Id.Value);
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
				// If the configuration does not exist, check whether the user is allowed to create the configuration.
				if (this.slices.ContainsKey(slice.Id.Value))
				{
					// Create the configuration.
					configSlice = new ConfigSlice(slice, this.slices[slice.Id.Value]);
					// Add the configuration.
					this.configSlices.Add(slice.Id.Value, configSlice);
				}
				else
				{
					// Throw an exception.
					throw new ConfigException("You do not have permissions to add the slice {0}.".FormatWith(slice.Id.Value));
				}
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

				// Dispose the slices configuration.
				foreach (ConfigSlice configSlice in this.configSlices.Values)
				{
					configSlice.Dispose();
				}

				// Close the registry key.
				this.key.Close();
			}
		}

		/// <summary>
		/// Parses the specified encrypted configuration.
		/// </summary>
		/// <param name="xml">The XML file.</param>
		private void ParseEncryptedConfig(string xml)
		{
			// Parse the XML document.
			XDocument document = XDocument.Parse(xml);

			// Validate the XML element.
			if (document.Root.Name != "EncryptedPlanetLabConfig") throw new ConfigException("The downloaded encrypted PlanetLab configuration configuration does not have a valid format.");

			try
			{
				// Decode and decrypt the root element.
				byte[] data = Convert.FromBase64String(document.Root.Value).DecryptAes(Config.cryptoKey, Config.cryptoIV);
				// Create a memory stream for the decrypted data.
				using (MemoryStream stream = new MemoryStream(data))
				{
					// Parse the configuration.
					this.ParseConfig(stream);
				}
			}
			catch
			{
				throw new ConfigException("The downloaded PlanetLab configuration configuration does not have a valid format.");
			}
		}

		/// <summary>
		/// Parses the specified stream configuration.
		/// </summary>
		/// <param name="stream">The stream.</param>
		private void ParseConfig(Stream stream)
		{
			// Parse the XML document.
			XDocument document = XDocument.Load(stream);

			// Validate the XML element.
			if (document.Root.Name != "PlanetLabManager") throw new ConfigException("The downloaded PlanetLab configuration configuration does not have a valid format.");

			// Get the Expires field.
			this.Expires = DateTime.Parse(document.Root.Element("Expires").Value);

			// If the configuration has expired, do nothing.
			if (this.Expires < DateTime.Now) return;

			// Get the PlanetLab configuration.
			this.Username = document.Root.Element("UserName").Value;
			this.Password = Convert.FromBase64String(document.Root.Element("Password").Value).DecryptSecureStringAes(Config.cryptoKey, Config.cryptoIV);
			this.PersonId = int.Parse(document.Root.Element("PersonId").Value);

			// Add the allowed slices.
			this.slices.Clear();
			foreach (XElement slice in document.Root.Element("Slices").Elements("Slice"))
			{
				this.slices.Add(int.Parse(slice.Attribute("Id").Value), Convert.FromBase64String(slice.Element("Key").Value));
			}

			// Add the allowed commands.
			this.commands.Clear();
			foreach (XElement command in document.Root.Element("Commands").Elements("Command"))
			{
				this.commands.Add(command.Value.ToLowerInvariant());
			}

			// Set the configuration as loaded if did not expire.
			this.loaded = true;
		}

		#endregion
	}
}
