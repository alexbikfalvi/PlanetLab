﻿/* 
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
using System.Drawing;
using System.Security;
using System.Windows.Forms;
using DotNetApi;
using DotNetApi.Web.XmlRpc;
using DotNetApi.Windows.Controls;
using PlanetLab;
using PlanetLab.Api;
using PlanetLab.Forms;
using PlanetLab.Requests;

namespace PlanetLab.Controls
{
	/// <summary>
	/// A control that displays the information of a PlanetLab interface.
	/// </summary>
	public partial class ControlInterfaceProperties : ControlObjectProperties
	{
		private readonly PlRequest request = new PlRequest(PlRequest.RequestMethod.GetInterfaces);

		/// <summary>
		/// Creates a new control instance.
		/// </summary>
		public ControlInterfaceProperties()
		{
			InitializeComponent();
		}

		// Protected methods.

		/// <summary>
		/// An event handler called when a new PlanetLab object is set.
		/// </summary>
		/// <param name="obj">The PlanetLab object.</param>
		protected override void OnObjectSet(PlObject obj)
		{
			// Get the interface.
			PlInterface iface = obj as PlInterface;

			// Change the display information for the new node.
			if (null == iface)
			{
				this.ObjectTitle = "Interface unknown";
				this.Message = "The interface information is not available.";
				this.Icon = Resources.GlobeWarning_32;
				this.tabControl.Visible = false;
			}
			else
			{
				// General and additional.

				this.ObjectTitle = iface.Ip;
				this.Message = string.Empty;
				this.Icon = Resources.GlobeObject_32;

				this.textBoxLastUpdated.Text = iface.LastUpdated.HasValue ? iface.LastUpdated.Value.ToString() : ControlObjectProperties.notAvailable;

				this.textBoxType.Text = iface.Type;
				this.textBoxHostname.Text = iface.Hostname;
				this.textBoxMac.Text = iface.Mac;
				this.textBoxIp.Text = iface.Ip;
				this.textBoxNetmask.Text = iface.Netmask;
				this.textBoxBroadcast.Text = iface.Broadcast;
				this.textBoxGateway.Text = iface.Gateway;
				this.textBoxBandwidthLimit.Text = iface.BandwidthLimit.HasValue ? iface.BandwidthLimit.Value.ToString() : ControlObjectProperties.notAvailable;
				this.textBoxDns1.Text = iface.Dns1;
				this.textBoxDns2.Text = iface.Dns2;
				this.textBoxMethod.Text = iface.Method;
				
				this.checkBoxIsPrimary.CheckState = iface.IsPrimary.HasValue ? iface.IsPrimary.Value ? CheckState.Checked : CheckState.Unchecked : CheckState.Indeterminate;

				// Identifiers.

				this.textBoxInterfaceId.Text = iface.InterfaceId.HasValue ? iface.InterfaceId.Value.ToString() : ControlObjectProperties.notAvailable;
				this.textBoxNodeId.Text = iface.NodeId.HasValue ? iface.NodeId.Value.ToString() : ControlObjectProperties.notAvailable;

				// Interface tags.
				this.listViewInterfaceTags.Items.Clear();
				foreach (int id in iface.InterfaceTagIds)
				{
					ListViewItem item = new ListViewItem(id.ToString(), 0);
					item.Tag = id;
					this.listViewInterfaceTags.Items.Add(item);
				}

				// Disable the buttons.
				this.buttonInterfaceTag.Enabled = false;

				this.tabControl.Visible = true;
			}

			this.tabControl.SelectedTab = this.tabPageGeneral;
			if (this.Focused)
			{
				this.textBoxHostname.Select();
				this.textBoxHostname.SelectionStart = 0;
				this.textBoxHostname.SelectionLength = 0;
			}
		}

		/// <summary>
		/// An event handler called when updating the control with a PlanetLab object of the specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		protected override void OnUpdate(int id)
		{
			// Hide the current information.
			this.Icon = Resources.GlobeClock_32;
			this.ObjectTitle = "Updating...";
			this.Message = "Updating the information for interface {0}...".FormatWith(id);
			this.tabControl.Visible = false;

			// Begin a new nodes request for the specified node.
			this.BeginRequest(this.request, Config.Static.Username, Config.Static.Password, PlInterface.GetFilter(PlInterface.Fields.InterfaceId, id));
		}

		/// <summary>
		/// An event handler called when the request completes.
		/// </summary>
		/// <param name="response">The XML-RPC response.</param>
		/// <param name="state">The request state.</param>
		protected override void OnRequestResult(XmlRpcResponse response, RequestState state)
		{
			// Call the base class method.
			base.OnRequestResult(response, state);
			// If the request has not failed.
			if ((null == response.Fault) && (null != response.Value))
			{
				PlList<PlInterface> iface = null;
				// Create a PlanetLab interfaces list for the given response.
				try { iface = PlList<PlInterface>.Create(response.Value as XmlRpcArray); }
				catch { }
				// If the interfaces count is greater than zero.
				if ((null != iface) && (iface.Count > 0))
				{
					// Display the information for the first interface.
					this.Object = iface[0];
				}
				else
				{
					// Set the node to null.
					this.Object = null;
				}
			}
		}

		/// <summary>
		/// An event handler called when the current request throws an exception.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="state">The request state.</param>
		protected override void OnRequestException(Exception exception, RequestState state)
		{
			// Catch all exceptions.
			this.Icon = Resources.GlobeError_32;
			this.ObjectTitle = "Interface unknown";
			this.Message = "An error occurred while requesting the interface information. {0}{1}{2}".FormatWith(
				Environment.NewLine,
				Environment.NewLine,
				exception.Message);
		}

		// Private methods.

		/// <summary>
		/// An event handler called when the interface tag selection has changed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnInterfaceTagSelectionChanged(object sender, EventArgs e)
		{
			this.buttonInterfaceTag.Enabled = this.listViewInterfaceTags.SelectedItems.Count > 0;
		}

		/// <summary>
		/// An event handler called when the user selects the properties of a node tag.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnInterfaceTagProperties(object sender, EventArgs e)
		{
			// If there are no selected tag, do nothing.
			if (this.listViewInterfaceTags.SelectedItems.Count == 0) return;
			// Get the selected address ID.
			int id = (int)this.listViewInterfaceTags.SelectedItems[0].Tag;
			using (FormObjectProperties<ControlInterfaceTagProperties> form = new FormObjectProperties<ControlInterfaceTagProperties>())
			{
				form.ShowDialog(this, "Interface Tag", id);
			}
		}
	}
}
