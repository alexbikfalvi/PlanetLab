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
using System.Threading;
using Microsoft.Win32;
using DotNetApi.Windows;
using DotNetApi.Windows.Forms;

namespace PlanetLab.Forms
{
	/// <summary>
	/// A form used for downloading the configuration.
	/// </summary>
	public partial class FormConfig : ThreadSafeForm
	{
		private readonly Config config;

		/// <summary>
		/// Creates a new form instance.
		/// </summary>
		public FormConfig()
		{
			// Initialize the component.
			this.InitializeComponent();

			// Set the font.
			Window.SetFont(this);

			// Create the configuration.
			this.config = new Config(Registry.CurrentUser, Resources.ConfigRootPath, "http://alex.bikfalvi.com/projects/inetanalytics/planetlab/config.xml", () =>
				{
					this.Invoke(() =>
						{
							// Close the form.
							this.Close();
						});
				});
		}

		// Public properties.

		/// <summary>
		/// Gets the configuration.
		/// </summary>
		public Config Configuration { get { return this.config; } }
	}
}
