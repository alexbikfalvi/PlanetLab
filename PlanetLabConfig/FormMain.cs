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
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Win32;
using DotNetApi;
using DotNetApi.Security;

namespace PlanetLabConfig
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Converts a loaded registry key to Base64.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnBase64(object sender, EventArgs e)
		{
			try
			{
				// Read the registry value.
				byte[] data = Registry.GetValue(this.textBoxRegistryKey.Text, this.textBoxRegistryValue.Text, null) as byte[];
				// If the data is not null.
				if (null != data)
				{
					this.textBoxBase64.Text = Convert.ToBase64String(data);
				}
			}
			catch (Exception exception)
			{
				// Show a message.
				MessageBox.Show(this, "Converting the binary registry value to Base64 failed. {0}".FormatWith(exception.Message), "Conversion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Encrypts a file.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnEncrypt(object sender, EventArgs e)
		{
			// Read the key.
			byte[] key = FormMain.HexStringToByteArray(this.textBoxKey.Text);
			byte[] iv = FormMain.HexStringToByteArray(this.textBoxIv.Text);

			// Show the open file dialog.
			if (this.openFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				// Read the binary data from the file.
				byte[] input = File.ReadAllBytes(this.openFileDialog.FileName);
				// Encrypt the data.
				byte[] output = input.EncryptAes(key, iv);

				// Save the data to a file.
				if (this.saveFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					// Create a configuration XML document.
					XDocument document = new XDocument(new XElement("EncryptedPlanetLabConfig", Convert.ToBase64String(output)));
					// Save the XML document to the file.
					using (XmlTextWriter writer = new XmlTextWriter(this.saveFileDialog.FileName, new UTF8Encoding(false)))
					{
						document.Save(writer);
					}
				}
			}
		}

		/// <summary>
		/// Converts a hexadecimal string to a byte array.
		/// </summary>
		/// <param name="value">The string.</param>
		/// <returns>The byte array.</returns>
		private static byte[] HexStringToByteArray(String value)
		{
			String[] tokens = value.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
			byte[] data = new byte[tokens.Length];
			for (int index = 0; index < data.Length; index++)
			{
				data[index] = Convert.ToByte(tokens[index], 16);
			}
			return data;
		}

		/// <summary>
		/// An event handler called when the date has changed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnDateChanged(object sender, EventArgs e)
		{
			this.textBoxDate.Text = this.dateTimePicker.Value.ToString("o");
		}
	}
}
