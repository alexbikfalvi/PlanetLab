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
using InetCommon.Events;

namespace PlanetLab
{
	/// <summary>
	/// A class with config evetns.
	/// </summary>
	public sealed class ApplicationEvents
	{
		// Public events.

		/// <summary>
		/// An event raised when selecting a page.
		/// </summary>
		public event PageSelectionEventHandler PageSelected;

		/// <summary>
		/// An event raised when selecting the PlanetLab sites.
		/// </summary>
		public event EventHandler SitesSelected;
		/// <summary>
		/// An event raised when selecting the PlanetLab nodes.
		/// </summary>
		public event EventHandler NodesSelected;
		/// <summary>
		/// An event raised when selecting the PlanetLab slices.
		/// </summary>
		public event EventHandler SlicesSelected;

		/// <summary>
		/// Raises a page selection event.
		/// </summary>
		/// <param name="node">The tree node corresponding to the page.</param>
		public void SelectPage(TreeNode node)
		{
			if (null != this.PageSelected) this.PageSelected(this, new PageSelectionEventArgs(node));
		}

		/// <summary>
		/// Raises a PlanetLab sites selected event.
		/// </summary>
		public void SelectPlanetLabSites()
		{
			if (null != this.SitesSelected) this.SitesSelected(this, EventArgs.Empty);
		}

		/// <summary>
		/// Raises a PlanetLab nodes selected event.
		/// </summary>
		public void SelectPlanetLabNodes()
		{
			if (null != this.NodesSelected) this.NodesSelected(this, EventArgs.Empty);
		}

		/// <summary>
		/// Raises a PlanetLab slices selected event.
		/// </summary>
		public void SelectPlanetLabSlices()
		{
			if (null != this.SlicesSelected) this.SlicesSelected(this, EventArgs.Empty);
		}
	}
}
