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
using System.Windows.Forms;
using DotNetApi.Windows.Controls;

namespace PlanetLab.Controls
{
	/// <summary>
	/// A control that displays the PlanetLab introduction and configuration.
	/// </summary>
	public partial class ControlInfo : ThemeControl
	{
		/// <summary>
		/// Creates a new control instance.
		/// </summary>
		public ControlInfo()
		{
			// Initialize component.
			InitializeComponent();
			// Set the default control properties.
			this.Visible = false;
			this.Dock = DockStyle.Fill;
		}

		// Public methods.

		/// <summary>
		/// Initializes the control with the specified config object.
		/// </summary>
		/// <param name="config">The config.</param>
		public void Initialize(Config config)
		{
			// Set the settings configuration.
			this.settings.Config = config;
		}

		// Private methods.

		/// <summary>
		/// An event handler called when the user selects the sites link.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnSitesClick(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.settings.Config.Events.SelectPlanetLabSites();
		}

		/// <summary>
		/// An event handler called when the user selects the nodes link.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnNodesClick(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.settings.Config.Events.SelectPlanetLabNodes();
		}

		/// <summary>
		/// An event handler called when the user selects the slices link.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnSlicesClick(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.settings.Config.Events.SelectPlanetLabSlices();
		}
	}
}
