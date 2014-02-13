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
using System.Windows.Forms;
using PlanetLab;
using PlanetLab.Api;
using PlanetLab.Database;
using PlanetLab.Requests;
using DotNetApi;
using DotNetApi.Web.XmlRpc;

namespace PlanetLab.Controls
{
	/// <summary>
	/// A control that receives user input data to add a new PlanetLab slice.
	/// </summary>
	public sealed partial class ControlRenewSlice : ControlRequest
	{
		private readonly PlRequest requestRefresh = new PlRequest(PlRequest.RequestMethod.GetSlices);
		private readonly PlRequest requestRenew = new PlRequest(PlRequest.RequestMethod.UpdateSlice);

		private PlDatabase<PlSlice> slices = null;
		private PlSlice slice = null;

		private readonly RequestState refreshRequestState;
		
		/// <summary>
		/// Creates a new control instance.
		/// </summary>
		public ControlRenewSlice()
		{
			// Initialize the component.
			this.InitializeComponent();
			// Create the refresh request state.
			this.refreshRequestState = new RequestState(
				this.OnRefreshStarted,
				this.OnRefreshResult,
				this.OnRefreshCanceled,
				this.OnRefreshException,
				null);
		}

		// Public events.

		/// <summary>
		/// An event raised when a PlanetLab operation has started.
		/// </summary>
		public event EventHandler RequestStarted;
		/// <summary>
		/// An event raised when a PlanetLab operation has finished.
		/// </summary>
		public event EventHandler RequestFinished;
		/// <summary>
		/// An event raised when user closes the selection.
		/// </summary>
		public event EventHandler Closed;

		// Public methods.

		/// <summary>
		/// Refreshes the current slice information.
		/// </summary>
		/// <param name="config">The configuration.</param>
		/// <param name="slice">The current slice.</param>
		public void Refresh(Config config, PlSlice slice)
		{
			// Set the slices database.
			this.slices = config.DbSlices;
			// Set the slice.
			this.slice = slice;

			// Clear the buttons state.
			this.buttonRefresh.Enabled = true;
			this.buttonCancel.Enabled = false;
			this.buttonRenew.Enabled = false;
			this.buttonClose.Enabled = true;

			// Clear the status.
			this.labelStatus.Text = "Ready.";

			// Update the slice information.
			this.OnUpdateSlice();
		}

		// Protected methods.

		/// <summary>
		/// An event handler called when the current request starts, and the notification box is displayed.
		/// </summary>
		/// <param name="state">The request state.</param>
		protected override void OnRequestStarted(RequestState state)
		{
			// Change the controls enabled state.
			this.buttonRefresh.Enabled = false;
			this.buttonCancel.Enabled = true;
			this.buttonRenew.Enabled = false;
			this.buttonClose.Enabled = false;
			this.comboBoxRenew.Enabled = false;
			// Raise the PlanetLab request started event.
			if (this.RequestStarted != null) this.RequestStarted(this, EventArgs.Empty);
			// Call the base class method.
			base.OnRequestStarted(state);
		}

		/// <summary>
		/// An event handler called when the current request ends, and the notification box is hidden.
		/// </summary>
		/// <param name="state">The request state.</param>
		protected override void OnRequestFinished(RequestState state)
		{
			// Change the controls enanled state..
			this.buttonRefresh.Enabled = true;
			this.buttonCancel.Enabled = false;
			this.buttonRenew.Enabled = this.slice.Expires.HasValue;
			this.buttonClose.Enabled = true;
			this.comboBoxRenew.Enabled = true;
			// Raise the PlanetLab request finished event.
			if (this.RequestFinished != null) this.RequestFinished(this, EventArgs.Empty);
			// Call the base class method.
			base.OnRequestFinished(state);
		}

		// Private methods.

		/// <summary>
		/// An event handler called when the user refreshes the list of items.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnRefreshSlice(object sender, EventArgs e)
		{
			// Begin the PlanetLab request.
			this.BeginRequest(
				this.requestRefresh,
				Config.Static.Username,
				Config.Static.Password,
				PlSlice.GetFilter(PlSlice.Fields.SliceId, this.slice.Id),
				this.refreshRequestState);
		}

		/// <summary>
		/// An event handler called when the refreshing the current slice.
		/// </summary>
		/// <param name="state">The request state.</param>
		private void OnRefreshStarted(RequestState state)
		{
			// Update the status.
			this.labelStatus.Text = "Refreshing slice...";
		}

		/// <summary>
		/// An event handler called when the control completes an asynchronous request for refreshing the slice.
		/// </summary>
		/// <param name="response">The XML-RPC response.</param>
		/// <param name="state">The request state.</param>
		private void OnRefreshResult(XmlRpcResponse response, RequestState state)
		{
			// If the request has not failed.
			if ((null == response.Fault) && (null != response.Value))
			{
				try
				{
					// Get the response slices.
					XmlRpcArray slices = response.Value as XmlRpcArray;
					// Parse the slice.
					this.slice.Parse(slices.Values[0].Value as XmlRpcStruct);
					// Update the slice information.
					this.OnUpdateSlice();
					// Update the status.
					this.labelStatus.Text = "Ready.";
				}
				catch
				{
					// Update the status.
					this.labelStatus.Text = "Refresh failed.";
				}
			}
			else
			{
				// Update the status.
				this.labelStatus.Text = "Refresh failed. {0}".FormatWith(response.Fault);
			}
		}

		/// <summary>
		/// An event handler called when an asynchronous request for refershing the slice was canceled.
		/// </summary>
		/// <param name="state">The request state.</param>
		private void OnRefreshCanceled(RequestState state)
		{
			// Update the status.
			this.labelStatus.Text = "Refresh canceled.";
		}

		/// <summary>
		/// An event handler called when the current request for refreshing the slice throws an exception.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="state">The request state.</param>
		private void OnRefreshException(Exception exception, RequestState state)
		{
			// Update the status.
			this.labelStatus.Text = "Refresh failed.";
		}

		/// <summary>
		/// An event handler called when the slice is being renewed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnRenew(object sender, EventArgs e)
		{
			// Show an error dialog.
			MessageBox.Show("Cannot renew the expiration date of the selected nodes. This option is not available in PlanetLab Manager Student Edition.", "Option Not Available", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		/// <summary>
		/// An event handler called when the user selects the close button.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnClose(object sender, EventArgs e)
		{
			// Raise the close event.
			if (this.Closed != null) this.Closed(sender, e);
		}

		/// <summary>
		/// An event handler called when the user cancel a current database command.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnCancel(object sender, EventArgs e)
		{
			// Disable the cancel button.
			this.buttonCancel.Enabled = false;
			// Cancel the current request.
			this.CancelRequest();
		}

		/// <summary>
		/// An event handler called when the renewal interval has changed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnRenewChanged(object sender, EventArgs e)
		{
			this.buttonRenew.Enabled = this.comboBoxRenew.SelectedIndex >= 0;
		}

		/// <summary>
		/// Updates the PlanetLab slice information.
		/// </summary>
		private void OnUpdateSlice()
		{
			// Set the slice name.
			this.textBoxName.Text = this.slice.Name;

			// Clear the renewal items.
			this.comboBoxRenew.Items.Clear();

			// Set the slice expires.
			if (this.slice.Expires.HasValue)
			{
				this.textBoxExpires.Text = this.slice.Expires.Value.ToString();
				this.comboBoxRenew.Enabled = true;

				this.comboBoxRenew.Items.Add("One week ({0})".FormatWith(this.slice.Expires.Value.AddDays(7.0)));
				this.comboBoxRenew.Items.Add("Two weeks ({0})".FormatWith(this.slice.Expires.Value.AddDays(14.0)));
				this.comboBoxRenew.Items.Add("Three weeks ({0})".FormatWith(this.slice.Expires.Value.AddDays(21.0)));
				this.comboBoxRenew.Items.Add("Four weeks ({0})".FormatWith(this.slice.Expires.Value.AddDays(28.0)));

				this.comboBoxRenew.SelectedIndex = 0;
				this.buttonRenew.Enabled = true;
			}
			else
			{
				this.textBoxExpires.Clear();
				this.comboBoxRenew.Enabled = false;
			}
		}
	}
}
