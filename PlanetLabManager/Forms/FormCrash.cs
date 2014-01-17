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
using System.Windows.Forms;
using DotNetApi.Windows;
using DotNetApi.Windows.Forms;

namespace PlanetLab.Forms
{
	/// <summary>
	/// A form showing an exception.
	/// </summary>
	public sealed partial class FormCrash : ThreadSafeForm
	{
		private Exception exception;

		/// <summary>
		/// Creates a new form instance.
		/// </summary>
		public FormCrash()
		{
			// Initialize the component.
			this.InitializeComponent();
			// Set the font.
			Window.SetFont(this);
		}

		// Public properties.

		/// <summary>
		/// Gets or sets the current exception.
		/// </summary>
		public Exception Exception
		{
			get { return this.exception; }
			set { this.OnExceptionSet(value); }
		}

		// Public methods.

		/// <summary>
		/// Shows the crash form as a modal dialog.
		/// </summary>
		/// <param name="exception">The exception to display.</param>
		public void ShowDialog(Exception exception)
		{
			this.Exception = exception;
			base.ShowDialog();
		}

		// Private methods.

		/// <summary>
		/// Shows the form.
		/// </summary>
		private new void Show()
		{
			base.Show();
		}

		/// <summary>
		/// Shows the form.
		/// </summary>
		/// <param name="owner">The owner.</param>
		private new void Show(IWin32Window owner)
		{
			base.Show(owner);
		}

		/// <summary>
		/// Shows the dialog.
		/// </summary>
		/// <returns>The dialog result.</returns>
		private new DialogResult ShowDialog()
		{
			return base.ShowDialog();
		}

		/// <summary>
		/// Shows the dialog.
		/// </summary>
		/// <param name="owner">The owner.</param>
		/// <returns>The dialog result.</returns>
		private new DialogResult ShowDialog(IWin32Window owner)
		{
			return base.ShowDialog(owner);
		}

		/// <summary>
		/// An event handler called when setting a new exception for this form.
		/// </summary>
		/// <param name="exception">The exception.</param>
		private void OnExceptionSet(Exception exception)
		{
			this.exception = exception;
			this.textBoxType.Text = exception.GetType().ToString();
			this.textBoxMessage.Text = exception.Message;
			this.textBoxSource.Text = exception.Source;
			this.textBoxTarget.Text = exception.TargetSite != null ? exception.TargetSite.ToString() : string.Empty;
			this.textBoxStack.Text = exception.StackTrace;
		}
	}
}
