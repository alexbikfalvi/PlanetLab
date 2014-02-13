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
using System.Windows.Forms;
using Microsoft.Win32;
using DotNetApi;
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
		private string url;

		private readonly FormConfigUrl formUrl = new FormConfigUrl();

		/// <summary>
		/// Creates a new form instance.
		/// </summary>
		/// <param name="config">The PlanetLab configuration.</param>
		/// <param name="defaultUrl">The default URL.</param>
		public FormConfig(Config config, string defaultUrl)
		{
			// Initialize the component.
			this.InitializeComponent();

			// Set the configuration.
			this.config = config;
			// Set the default URL.
			this.url = defaultUrl;

			// Set the font.
			Window.SetFont(this);
		}

		/// <summary>
		/// An event handler called when the forms loads.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnLoad(object sender, EventArgs e)
		{
			// Start downloading the configuration.
			this.labelMessage.Text = "Downloading the PlanetLab configuration...";

			// Execute on the thread pool.
			ThreadPool.QueueUserWorkItem((object status) =>
				{
					try
					{
						// Load the configuration.
						this.config.Load(new Uri(this.url), (bool result, Exception exception) =>
						{
							this.Invoke(() =>
							{
								// If the download was successful.
								if (result)
								{
									// Check whether the configuration did not expire.
									if (this.config.Expires < DateTime.Now)
									{
										// Show an error dialog.
										MessageBox.Show(
											this,
											"The specified configuration file has expired at {0}. Select a different configuration file and try again.".FormatWith(this.config.Expires),
											"Configuration Expired",
											MessageBoxButtons.OK,
											MessageBoxIcon.Error);
										// Retry.
										this.OnRetry(sender, e);
									}
									else
									{
										// Close the form.
										this.Close();
									}
								}
								else
								{
									// Show an error dialog.
									MessageBox.Show(
										this,
										"Downloading the PlanetLab configuration failed.{0}{1}Technical information: {2}".FormatWith(Environment.NewLine, Environment.NewLine, exception.Message),
										"Configuration Error",
										MessageBoxButtons.OK,
										MessageBoxIcon.Error);
									// Retry.
									this.OnRetry(sender, e);
								}
							});
						});
					}
					catch (Exception exception)
					{
						this.Invoke(() =>
						{
							// Show an error dialog.
							MessageBox.Show(
								this,
								"Downloading the PlanetLab configuration failed.{0}{1}Technical information: {2}".FormatWith(Environment.NewLine, Environment.NewLine, exception.Message),
								"Configuration Error",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
							// Retry.
							this.OnRetry(sender, e);
						});
					}
				});
		}

		/// <summary>
		/// A method called to ask the user for the configuration file.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnRetry(object sender, EventArgs e)
		{
			// Show the select URL dialog.
			if (this.formUrl.ShowDialog(this) == DialogResult.OK)
			{
				// Set the URL.
				this.url = this.formUrl.Url;

				// Load the configuration.
				this.OnLoad(sender, e);
			}
			else
			{
				// Close the form.
				this.Close();
			}
		}
	}
}
