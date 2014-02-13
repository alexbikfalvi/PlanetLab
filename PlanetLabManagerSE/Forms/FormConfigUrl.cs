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
using System.Windows.Forms;
using DotNetApi.Windows;
using DotNetApi.Windows.Forms;

namespace PlanetLab.Forms
{
	/// <summary>
	/// A form allowing the user to select the configuration URL.
	/// </summary>
	public partial class FormConfigUrl : ThreadSafeForm
	{
		/// <summary>
		/// Creates a new form instance.
		/// </summary>
		public FormConfigUrl()
		{
			// Initialize the component.
			this.InitializeComponent();

			// Set the font.
			Window.SetFont(this);
		}

		// Public properties.

		/// <summary>
		/// Gets the selected URL.
		/// </summary>
		public string Url { get { return this.textBoxUrl.Text; } }

		// Public methods.

		/// <summary>
		/// Shows the dialog.
		/// </summary>
		/// <param name="owner">The owner window.</param>
		/// <returns>The dialog result.</returns>
		public new DialogResult ShowDialog(IWin32Window owner)
		{
			this.textBoxUrl.Clear();
			this.buttonContinue.Enabled = false;

			return base.ShowDialog(owner);
		}

		// Private methods.

		/// <summary>
		/// An event handler called when the text has changed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnChanged(object sender, EventArgs e)
		{
			this.buttonContinue.Enabled = !string.IsNullOrWhiteSpace(this.textBoxUrl.Text);
		}
	}
}
