﻿/* 
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
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Linq;
using System.Windows.Forms;
using DotNetApi;
using DotNetApi.IO;
using DotNetApi.Security;
using InetCommon.Status;
using PlanetLab;
using PlanetLab.Api;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace PlanetLab.Controls
{
	/// <summary>
	/// A control class for testing secure shell.
	/// </summary>
	public sealed partial class ControlSession : ControlSsh
	{
		// Private variables.

		private Config config = null;
		private ConfigSlice sliceConfig = null;
		private PlNode node = null;

		private ApplicationStatusHandler status = null;

		// Public declarations

		/// <summary>
		/// Creates a new control instance.
		/// </summary>
		public ControlSession()
		{
			// Initialize component.
			InitializeComponent();

			// Set the default control properties.
			this.Visible = false;
			this.Dock = DockStyle.Fill;
		}

		// Public events.

		/// <summary>
		/// An event raised when connecting to a PlanetLab node.
		/// </summary>
		public event PlObjectEventHandler<PlNode> Connecting;
		/// <summary>
		/// An event raised when connecting to a PlanetLab node succeeded.
		/// </summary>
		public event PlObjectEventHandler<PlNode> ConnectSucceeded;
		/// <summary>
		/// An event raised when connecting to a PlanetLab node failed.
		/// </summary>
		public event PlExceptionEventHandler<PlNode> ConnectFailed;
		/// <summary>
		/// An event raised when disconnecting from a PlanetLab node.
		/// </summary>
		public event PlObjectEventHandler<PlNode> Disconnecting;
		/// <summary>
		/// An event raised when disconnected from a PlanetLab node.
		/// </summary>
		public event PlObjectEventHandler<PlNode> Disconnected;

		// Public methods.

		/// <summary>
		/// Initialized the control with the specified config.
		/// </summary>
		/// <param name="config">The config.</param>
		/// <param name="sliceConfig">The slice configuration.</param>
		/// <param name="node">The PlanetLab node.</param>
		public void Initialize(Config config, ConfigSlice sliceConfig, PlNode node)
		{
			// Set the config.
			this.config = config;

			// Set the slice configuration.
			this.sliceConfig = sliceConfig;
			
			// Set the node.
			this.node = node;

			// Set the title.
			this.panelConsole.Title = "Secure Shell Connection to {0}".FormatWith(node.Hostname);

			// Set the slice configuration event handlers.
			this.sliceConfig.Changed += this.OnConfigurationChanged;
			this.sliceConfig.Disposed += this.OnConfigurationChanged;

			// Set the node event handlers.
			this.node.Changed += this.OnConfigurationChanged;

			// Set the node hostname.
			this.labelHostname.Text = this.node.Hostname;

			// Get the config status.
			this.status = this.config.Status.GetHandler(this);
			this.status.Send(ApplicationStatus.StatusType.Normal, "Disconnected.", Resources.Server_16);

			// Enable the control.
			this.Enabled = true;
		}

		// Public methods.

		/// <summary>
		/// Connects the console to the current SSH server.
		/// </summary>
		public void Connect()
		{
			// If the private key is null, show a message and return.
			if (null == this.sliceConfig.Key)
			{
				// Show a message.
				MessageBox.Show("The private key for the selected PlanetLab slice is not set.", "Cannot Connect", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				// Raise the connect failed event.
				if (this.ConnectFailed != null) this.ConnectFailed(this, new PlExceptionEventArgs<PlNode>(this.node, new Exception("The private key for the selected PlanetLab slice is not set.")));
				return;
			}

			try
			{
				// The connection info.
				ConnectionInfo connectionInfo = null;
				// Create a memory stream with the key data.
				using (MemoryStream memoryStream = new MemoryStream(this.sliceConfig.Key))
				{
					// Create the private key file.
					using (PrivateKeyFile keyFile = new PrivateKeyFile(memoryStream))
					{
						// Create a key connection info.
						connectionInfo = new PrivateKeyConnectionInfo(this.node.Hostname, this.sliceConfig.Name, keyFile);
					}
				}
				// Connect to the PlanetLab node.
				base.Connect(connectionInfo);
			}
			catch (Exception exception)
			{
				// Show an error dialog if an exception is thrown.
				MessageBox.Show("Cannot connect to the PlanetLab node. {0}".FormatWith(exception.Message), "Cannot Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
				// Raise the connect failed event.
				if (this.ConnectFailed != null) this.ConnectFailed(this, new PlExceptionEventArgs<PlNode>(this.node, exception));
			}
		}

		/// <summary>
		/// Disconnects the console from the current SSH server.
		/// </summary>
		/// <param name="wait">The event wait handle.</param>
		public void Disconnect(EventWaitHandle wait = null)
		{
			try
			{
				// Disconnect from the PlanetLab node.
				base.Disconnect(wait);
			}
			catch (SshException)
			{
				// Raise the disconnected event.
				if (this.Disconnected != null) this.Disconnected(this, new PlObjectEventArgs<PlNode>(this.node));
			}
		}

		// Protected methods.

		/// <summary>
		/// An event handler called when connecting to an PlanetLab node.
		/// </summary>
		/// <param name="info">The connection info.</param>
		protected override void OnConnecting(ConnectionInfo info)
		{
			// Change the buttons enabled state.
			this.buttonConnect.Enabled = false;
			// Update the status bar.
			this.status.Send(ApplicationStatus.StatusType.Busy, "Connecting to the PlanetLab node \'{0}\'...".FormatWith(info.Host), Resources.ServerBusy_16);
			this.status.Lock();
			// Raise the event.
			if (null != this.Connecting) this.Connecting(this, new PlObjectEventArgs<PlNode>(this.node));
		}

		/// <summary>
		/// An event handler called when connecting to an PlanetLab node succeeded.
		/// </summary>
		/// <param name="info">The connection info.</param>
		protected override void OnConnectSucceeded(ConnectionInfo info)
		{
			// Change the buttons enabled state.
			this.buttonDisconnect.Enabled = true;
			// Update the status bar.
			this.status.Send(ApplicationStatus.StatusType.Busy, "Connected to the PlanetLab node \'{0}\'.".FormatWith(info.Host), Resources.ServerSuccess_16);
			// Enable the console.
			this.OnEnableConsole();
			// Raise the event.
			if (null != this.ConnectSucceeded) this.ConnectSucceeded(this, new PlObjectEventArgs<PlNode>(this.node));
		}

		/// <summary>
		/// An event handler called when connecting to an PlanetLab node failed.
		/// </summary>
		/// <param name="info">The connection info.</param>
		/// <param name="exception">The exception.</param>
		protected override void OnConnectFailed(ConnectionInfo info, Exception exception)
		{
			// Change the buttons enabled state.
			this.buttonConnect.Enabled = true;
			// Update the status bar.
			this.status.Send(ApplicationStatus.StatusType.Normal, "Connecting to the PlanetLab node \'{0}\' failed.".FormatWith(info.Host), Resources.ServerError_16);
			// Raise the connect failed event.
			if (null != this.ConnectFailed) this.ConnectFailed(this, new PlExceptionEventArgs<PlNode>(this.node, exception));
		}

		/// <summary>
		/// An event handler called when disconnecting from an PlanetLab node.
		/// </summary>
		/// <param name="info">The connection info.</param>
		protected override void OnDisconnecting(ConnectionInfo info)
		{
			// Change the buttons enabled state.
			this.buttonDisconnect.Enabled = false;
			// Update the status bar.
			this.status.Send(ApplicationStatus.StatusType.Busy, "Disconnecting from the PlanetLab node \'{0}\'...".FormatWith(info.Host), Resources.ServerBusy_16);
			// Raise the event.
			if (null != this.Disconnecting) this.Disconnecting(this, new PlObjectEventArgs<PlNode>(this.node));
		}

		/// <summary>
		/// An event handler called when disconnected from an PlanetLab node.
		/// </summary>
		/// <param name="info">The connection info.</param>
		protected override void OnDisconnected(ConnectionInfo info)
		{
			// Update the status bar.
			this.status.Send(ApplicationStatus.StatusType.Normal, "Disconnected.", Resources.Server_16);
			this.status.Unlock();

			// Change the buttons enabled state.
			this.buttonConnect.Enabled = true;
			// Disable the console.
			this.OnDisableConsole();
			// Raise the disconnected event.
			if (null != this.Disconnected) this.Disconnected(this, new PlObjectEventArgs<PlNode>(this.node));
		}

		/// <summary>
		/// An event handler called when an error occurres on an PlanetLab node connection.
		/// </summary>
		/// <param name="info">The connection info.</param>
		/// <param name="exception">The error exception.</param>
		protected override void OnErrorOccurred(ConnectionInfo info, Exception exception)
		{
			// Update the status bar.
			this.status.Send(ApplicationStatus.StatusType.Busy, "An error occurred on the connection to {0}. {1}".FormatWith(info.Host, exception.Message), Resources.ServerError_16);
		}

		/// <summary>
		/// An event handler called when receiving a key from the remote host.
		/// </summary>
		/// <param name="info">The connection info.</param>
		/// <param name="args">The event arguments.</param>
		protected override void OnHostKeyReceived(ConnectionInfo info, HostKeyEventArgs args)
		{
			// Update the status bar.
			this.status.Send(ApplicationStatus.StatusType.Busy, "Received host key from {0} of {1} bits.".FormatWith(info.Host, args.KeyLength), Resources.ServerBusy_16);
		}

		/// <summary>
		/// An event handler called when the client begins executing a command.
		/// </summary>
		/// <param name="command">The command.</param>
		protected override void OnCommandBegin(SshCommand command)
		{
			// Begin the command.
			this.console.BeginCommand(command.CommandText);
		}

		/// <summary>
		/// An event handler called when the client receives data for an executing command.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="data">The received data.</param>
		protected override void OnCommandData(SshCommand command, string data)
		{
			// Set the exception message as a result argument.
			this.console.AppendText(data, Color.LightGray);
		}

		/// <summary>
		/// An event handler called when a client command completed successfully.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="result">The result.</param>
		protected override void OnCommandSucceeded(SshCommand command, string result)
		{
			// Set the exception message as a result argument.
			this.console.AppendText(result, Color.LightGray);
			this.console.AppendText("SUCCESS", Color.Lime);
			this.console.AppendText(" Code: {0}.{1}", command.ExitStatus, Environment.NewLine);
			// Call the command complete event handler.
			this.console.EndCommand();
		}

		/// <summary>
		/// An event handler called when a client command has failed.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="error">The error.</param>
		protected override void OnCommandFailed(SshCommand command, string error)
		{
			// Set the exception message as a result argument.
			if (!string.IsNullOrWhiteSpace(error))
			{
				this.console.AppendText(error, Color.LightGray);
			} 
			this.console.AppendText("FAIL", Color.Red);
			this.console.AppendText(" Code: {0}.", command.ExitStatus);
			this.console.AppendText(Environment.NewLine);
			// Call the command complete event handler.
			this.console.EndCommand();
		}

		// Private methods.

		/// <summary>
		/// An event handler called when connecting to the PlanetLab node.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnConnect(object sender, EventArgs e)
		{
			// Call the connect method.
			this.Connect();
		}

		/// <summary>
		/// An event handler called when disconnecting from the PlanetLab node.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnDisconnect(object sender, EventArgs e)
		{
			// Call the disconnect method.
			this.Disconnect();
		}

		/// <summary>
		/// An event handler called when the configuration of the current session (slice or node) has changed.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnConfigurationChanged(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
		}

		/// <summary>
		/// Enables the console controls.
		/// </summary>
		private void OnEnableConsole()
		{
			this.console.Enable("{0}@{1}>".FormatWith(this.Info.Username, this.Info.Host));
		}

		/// <summary>
		/// Disables the console controls.
		/// </summary>
		private void OnDisableConsole()
		{
			this.console.Disable();
		}

		/// <summary>
		/// An event handler called when the user clicks on the execute command button.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnExecuteCommand(object sender, EventArgs e)
		{
			// Lock the list of commands.
			this.Commands.Lock();
			try
			{
				// If the number of executing commands is greater than zero, do nothing.
				if (this.Commands.Count > 0) return;
			}
			finally
			{
				this.Commands.Unlock();
			}

			// Else, get the command text.
			string text = this.console.Command;
			try
			{
				// Begin the command.
				this.BeginCommand(text);
			}
			catch (Exception exception)
			{
				// Show the error.
				this.console.AppendText("{0}@{1}> {2}{3}Command failed.{4}",
					this.Info.Username,
					this.Info.Host,
					text,
					Environment.NewLine,
					exception.Message);
			}
		}

		/// <summary>
		/// An event handler called when the user cancels a command.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void OnCancelCommand(object sender, EventArgs e)
		{
			// Lock the list of commands.
			this.Commands.Lock();
			try
			{
				// If the number of executing commands is greater than zero.
				if (this.Commands.Count > 0)
				{
					// Cancel the first command.
					this.Commands.First().CancelAsync();
				}
			}
			catch { }
			finally
			{
				this.Commands.Unlock();
			}
		}
	}
}
