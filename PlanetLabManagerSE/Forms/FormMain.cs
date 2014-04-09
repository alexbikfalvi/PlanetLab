/* 
 * Copyright (C) 2014 Alex Bikfalvi
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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DotNetApi;
using DotNetApi.Windows;
using DotNetApi.Windows.Controls;
using DotNetApi.Windows.Forms;
using DotNetApi.Windows.Themes;
using InetCommon.Events;
using InetCommon.Net;
using InetCommon.Status;
using PlanetLab.Controls;

namespace PlanetLab.Forms
{
	/// <summary>
	/// A class representing the main form of 
	/// </summary>
	public partial class FormMain : ThreadSafeForm
	{
		// Configuration.
		private readonly Config config;

		// Theme.
		private readonly ThemeSettings themeSettings;

		// Forms.
		private readonly FormAbout formAbout = new FormAbout();

		// Delegates.
		private readonly EventHandler actionNetworkStatusChanged;
		private readonly EventHandler actionNetworkStatusChecked;

		// Tree nodes.
		private TreeNode treeNodeInfo;
		private TreeNode treeNodeSites;
		private TreeNode treeNodeNodes;
		private TreeNode treeNodeSlices;

		// Controls.
		private readonly ControlInfo controlInfo = new ControlInfo();
		private readonly ControlSites controlSites = new ControlSites();
		private readonly ControlNodes controlNodes = new ControlNodes();
		private readonly ControlSlices controlSlices = new ControlSlices();

		// Panel control.
		private Control controlPanel = null;

		// Status.
		private ApplicationStatus.StatusType status = ApplicationStatus.StatusType.Unknown;


		/// <summary>
		/// Creates a new form instance.
		/// </summary>
		/// <param name="config">The configuration.</param>
		public FormMain(Config config)
		{
			// Initialize the component.
			this.InitializeComponent();

			// Set the configuration.
			this.config = config;

			// Get the theme settings.
			this.themeSettings = ToolStripManager.Renderer is ThemeRenderer ? (ToolStripManager.Renderer as ThemeRenderer).Settings : ThemeSettings.Default;

			// Create the tree view items.
			this.treeNodeSites = new TreeNode("Sites",
				this.imageList.Images.IndexOfKey("FolderClosedGlobe"),
				this.imageList.Images.IndexOfKey("FolderOpenGlobe"));
			this.treeNodeNodes = new TreeNode("Nodes",
				this.imageList.Images.IndexOfKey("FolderClosedGlobe"),
				this.imageList.Images.IndexOfKey("FolderOpenGlobe"));
			this.treeNodeSlices = new TreeNode("Slices",
				this.imageList.Images.IndexOfKey("FolderClosedGlobe"),
				this.imageList.Images.IndexOfKey("FolderOpenGlobe"));
			this.treeNodeInfo = new TreeNode("PlanetLab",
				this.imageList.Images.IndexOfKey("ServersGlobe"),
				this.imageList.Images.IndexOfKey("ServersGlobe"),
				new TreeNode[] {
					this.treeNodeSites,
					this.treeNodeNodes,
					this.treeNodeSlices
				});

			// Add the panel controls to the split container.
			this.toolSplitContainer.Panel2.Controls.Add(this.controlInfo);
			this.toolSplitContainer.Panel2.Controls.Add(this.controlSites);
			this.toolSplitContainer.Panel2.Controls.Add(this.controlNodes);
			this.toolSplitContainer.Panel2.Controls.Add(this.controlSlices);

			// Add the panel controls as tags.
			this.treeNodeInfo.Tag = this.controlInfo;
			this.treeNodeSites.Tag = this.controlSites;
			this.treeNodeNodes.Tag = this.controlNodes;
			this.treeNodeSlices.Tag = this.controlSlices;

			// Add the tree nodes to the side panel tree views.
			this.sideTreePlanetLab.Nodes.Add(this.treeNodeInfo);

			// Initialize the controls.
			this.controlInfo.Initialize(this.config);
			this.controlSites.Initialize(this.config);
			this.controlNodes.Initialize(this.config);
			this.controlSlices.Initialize(this.config, this.treeNodeSlices, this.toolSplitContainer.Panel2.Controls);

			// Set the selected control.
			this.controlPanel = this.labelNotAvailable;

			// Initialize the side controls.
			this.sideTreePlanetLab.Initialize();

			// Set the event handlers.
			this.config.Events.PageSelected += this.OnPageSelected;

			this.config.Events.SitesSelected += this.OnSitesSelected;
			this.config.Events.NodesSelected += this.OnNodesSelected;
			this.config.Events.SlicesSelected += this.OnSlicesSelected;

			// Set the status event handler.
			this.config.Status.MessageChanged += this.OnStatusMessageChanged;
			this.config.Status.LockChanged += this.OnStatusLockChanged;

			// Set the status.
			this.OnStatusMessageChanged(this, null);

			// Configure the side menu with the last saved configuration.
			this.sideMenu.VisibleItems = this.config.ConsoleSideMenuVisibleItems;
			this.sideMenu.SelectedIndex = this.config.ConsoleSideMenuSelectedItem;
			if (this.sideMenu.SelectedItem.HasControl && this.sideMenu.SelectedItem.Control.HasSelected())
			{
				this.sideMenu.SelectedItem.Control.SetSelected(this.config.ConsoleSideMenuSelectedNode);
			}

			// Create the network status event handler.
			this.actionNetworkStatusChanged = new EventHandler(this.OnNetworkStatusChanged);
			this.actionNetworkStatusChecked = new EventHandler(this.OnNetworkStatusChecked);
			// Set the network availability event handler.
			NetworkStatus.NetworkChanged += this.actionNetworkStatusChanged;
			NetworkStatus.NetworkChecked += this.actionNetworkStatusChecked;
			// Update the network status.
			this.OnNetworkStatusChanged(this, EventArgs.Empty);

			// Set the font.
			Window.SetFont(this);
		}

		// Protected methods.

		/// <summary>
		/// An event handler called when the form is being closed.
		/// </summary>
		/// <param name="e">The event arguments.</param>
		protected override void OnClosing(CancelEventArgs e)
		{
			// Check the status is locked.
			if (this.config.Status.IsLocked)
			{
				// Show a message.
				MessageBox.Show(
					this,
					"The PlanetLab Manager is running one or more background operations. You must stop them before closing the program.",
					"PlanetLab Manager Background",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
				// Cancel the closing request.
				e.Cancel = true;
				// Return.
				return;
			}
			// Save the configuration.
			this.config.ConsoleSideMenuVisibleItems = this.sideMenu.VisibleItems;
			this.config.ConsoleSideMenuSelectedItem = this.sideMenu.SelectedIndex ?? 0;
			if (this.sideMenu.SelectedItem.HasControl)
			{
				this.config.ConsoleSideMenuSelectedNode = this.sideMenu.SelectedItem.Control.GetSelected();
			}
			// Call the base class event handler.
			base.OnClosing(e);
		}

		// Private methods.

		/// <summary>
		/// An event handler called when the user clicks on the exit button.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnExit(object sender, EventArgs e)
		{
			// Close the form.
			this.Close();
		}

		/// <summary>
		/// An event handler called when the user clicks on the about button.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnAbout(object sender, EventArgs e)
		{
			this.formAbout.ShowDialog(this);
		}

		/// <summary>
		/// An event handler called when the right panel control selection has changed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnControlChanged(object sender, ControlChangedEventArgs e)
		{
			// If the selected control has not changed, do nothing.
			if (e.Control == this.controlPanel) return;

			// If there exists a current control.
			if (null != this.controlPanel)
			{
				// Hide the current control.
				this.controlPanel.Hide();
			}

			// If the control is not null.
			if (null != e.Control)
			{
				// Show the control.
				e.Control.Show();
				// Activate the control status.
				this.config.Status.Activate(e.Control);
				// Set the selected control.
				this.controlPanel = e.Control;
			}
			else
			{
				// Display the default message.
				this.labelNotAvailable.Show();
				// Set the selected control.
				this.controlPanel = this.labelNotAvailable;
			}
		}

		/// <summary>
		/// An event handler called when a page is selected.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnPageSelected(object sender, PageSelectionEventArgs e)
		{
			if (e.Node.TreeView != null)
			{
				e.Node.TreeView.SelectedNode = e.Node;
			}
		}

		/// <summary>
		/// An event handler called when the user selects the PlanetLab sites page.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnSitesSelected(object sender, EventArgs e)
		{
			this.sideMenu.SelectedItem = this.sideMenuPlanetLab;
			this.sideTreePlanetLab.SelectedNode = this.treeNodeSites;
		}

		/// <summary>
		/// An event handler called when the user selects the PlanetLab nodes page.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnNodesSelected(object sender, EventArgs e)
		{
			this.sideMenu.SelectedItem = this.sideMenuPlanetLab;
			this.sideTreePlanetLab.SelectedNode = this.treeNodeNodes;
		}

		/// <summary>
		/// An event handler called when the user selects the PlanetLab slices page.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnSlicesSelected(object sender, EventArgs e)
		{
			this.sideMenu.SelectedItem = this.sideMenuPlanetLab;
			this.sideTreePlanetLab.SelectedNode = this.treeNodeSlices;
		}

		/// <summary>
		/// An event handler called when changing the status message.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnStatusMessageChanged(object sender, ApplicationStatusMessageEventArgs e)
		{
			// Call the code on the UI thread.
			this.Invoke(() =>
			{
				if ((null != e) && e.Message.HasValue)
				{
					// If the status type has changed.
					if (e.Message.Value.Type != this.status)
					{
						// Update the status.
						switch (e.Message.Value.Type)
						{
							case ApplicationStatus.StatusType.Ready:
								this.statusStrip.BackColor = this.themeSettings.ColorTable.StatusStripReadyBackground;
								this.statusLabelLeft.ForeColor = this.themeSettings.ColorTable.StatusStripReadyText;
								this.statusLabelRight.ForeColor = this.themeSettings.ColorTable.StatusStripReadyText;
								this.statusLabelConnection.ForeColor = this.themeSettings.ColorTable.StatusStripReadyText;
								this.statusLabelRun.ForeColor = this.themeSettings.ColorTable.StatusStripReadyText;
								break;
							case ApplicationStatus.StatusType.Normal:
								this.statusStrip.BackColor = this.themeSettings.ColorTable.StatusStripNormalBackground;
								this.statusLabelLeft.ForeColor = this.themeSettings.ColorTable.StatusStripNormalText;
								this.statusLabelRight.ForeColor = this.themeSettings.ColorTable.StatusStripNormalText;
								this.statusLabelConnection.ForeColor = this.themeSettings.ColorTable.StatusStripNormalText;
								this.statusLabelRun.ForeColor = this.themeSettings.ColorTable.StatusStripNormalText;
								break;
							case ApplicationStatus.StatusType.Busy:
								this.statusStrip.BackColor = this.themeSettings.ColorTable.StatusStripBusyBackground;
								this.statusLabelLeft.ForeColor = this.themeSettings.ColorTable.StatusStripBusyText;
								this.statusLabelRight.ForeColor = this.themeSettings.ColorTable.StatusStripBusyText;
								this.statusLabelConnection.ForeColor = this.themeSettings.ColorTable.StatusStripBusyText;
								this.statusLabelRun.ForeColor = this.themeSettings.ColorTable.StatusStripBusyText;
								break;
						}
						// Set the new status.
						this.status = e.Message.Value.Type;
					}
					this.statusLabelLeft.Image = e.Message.Value.LeftImage;
					this.statusLabelLeft.Text = e.Message.Value.LeftText;
					this.statusLabelRight.Image = e.Message.Value.RightImage;
					this.statusLabelRight.Text = e.Message.Value.RightText;
					return;
				}

				this.statusStrip.BackColor = this.themeSettings.ColorTable.StatusStripReadyBackground;
				this.statusLabelLeft.ForeColor = this.themeSettings.ColorTable.StatusStripReadyText;
				this.statusLabelRight.ForeColor = this.themeSettings.ColorTable.StatusStripReadyText;
				this.statusLabelConnection.ForeColor = this.themeSettings.ColorTable.StatusStripReadyText;
				this.statusLabelRun.ForeColor = this.themeSettings.ColorTable.StatusStripReadyText;
				this.statusLabelLeft.Image = Resources.Information_16;
				this.statusLabelLeft.Text = "Ready.";
				this.statusLabelRight.Image = null;
				this.statusLabelRight.Text = null;
				this.status = ApplicationStatus.StatusType.Ready;
			});
		}

		/// <summary>
		/// An event handler called when the status lock has changed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnStatusLockChanged(object sender, EventArgs e)
		{
			this.Invoke(() =>
			{
				// Get the number of locks.
				int count = this.config.Status.LockCount;
				// Update the lock information.
				if (count > 0)
				{
					this.statusLabelRun.Text = "{0} background task{1}".FormatWith(count, count.PluralSuffix());
					this.statusLabelRun.Image = Resources.RunConcurrentStart_16;
				}
				else
				{
					this.statusLabelRun.Text = "No background tasks";
					this.statusLabelRun.Image = Resources.RunConcurrentStop_16;
				}
			});
		}

		/// <summary>
		/// An event handler called when the network status has changed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnNetworkStatusChanged(object sender, EventArgs e)
		{
			// Call the method on the UI thread.
			if (this.InvokeRequired) this.Invoke(this.actionNetworkStatusChanged, new object[] { sender, e });
			else
			{
				// Update the connecton status label.
				switch (NetworkStatus.IsInternetAvailable)
				{
					case NetworkStatus.AvailabilityStatus.Unknown:
						this.statusLabelConnection.Image = Resources.ConnectionQuestion_16;
						this.statusLabelConnection.Text = "Connectivity unknown";
						break;
					case NetworkStatus.AvailabilityStatus.Success:
						this.statusLabelConnection.Image = Resources.ConnectionSuccess_16;
						this.statusLabelConnection.Text = "Connected to Internet";
						break;
					case NetworkStatus.AvailabilityStatus.Warning:
						this.statusLabelConnection.Image = Resources.ConnectionWarning_16;
						this.statusLabelConnection.Text = "Connected to local network";
						break;
					case NetworkStatus.AvailabilityStatus.Fail:
						this.statusLabelConnection.Image = Resources.ConnectionError_16;
						this.statusLabelConnection.Text = "Not connected";
						break;
				}
			}
		}


		/// <summary>
		/// An event handler called when the network status has been checked.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnNetworkStatusChecked(object sender, EventArgs e)
		{
			// Call the method on the UI thread.
			if (this.InvokeRequired) this.Invoke(this.actionNetworkStatusChecked, new object[] { sender, e });
			else
			{
				// If the tooltip is visible.
				if (this.toolTipNetworkStatus.Visible)
				{
					// Hide the tooltip.
					this.OnNetworkStatusLeave(sender, e);
					// Show the tooltip.
					this.OnNetworkStatusEnter(sender, e);
				}
			}
		}

		/// <summary>
		/// Shows the network status tooltip.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnNetworkStatusEnter(object sender, EventArgs e)
		{
			// Compute the tooltip location.
			Point location = new Point(this.ClientRectangle.Right, this.ClientRectangle.Bottom);
			// Show the tooltip.
			this.toolTipNetworkStatus.Show(this, location, ContentAlignment.BottomRight);
		}

		/// <summary>
		/// Hides the network status tooltip.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnNetworkStatusLeave(object sender, EventArgs e)
		{
			// Hide the tooltip.
			this.toolTipNetworkStatus.Hide(this);
		}
	}
}
