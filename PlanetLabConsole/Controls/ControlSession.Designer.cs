namespace PlanetLab.Controls
{
	partial class ControlSession
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				// Remove the slice configuration event handlers.
				this.sliceConfig.Changed -= this.OnConfigurationChanged;
				this.sliceConfig.Disposed -= this.OnConfigurationChanged;

				// Remove the node event handlers.
				this.node.Changed -= this.OnConfigurationChanged;

				// Dispose the components.
				if (null != this.components)
				{
					this.components.Dispose();
				}
			}
			// Call the base class method.
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlSession));
			this.panelConsole = new DotNetApi.Windows.Controls.ThemeControl();
			this.console = new PlanetLab.Controls.ControlConsole();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.labelHostname = new System.Windows.Forms.ToolStripLabel();
			this.separator1 = new System.Windows.Forms.ToolStripSeparator();
			this.buttonConnect = new System.Windows.Forms.ToolStripButton();
			this.buttonDisconnect = new System.Windows.Forms.ToolStripButton();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.panelConsole.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelConsole
			// 
			this.panelConsole.Controls.Add(this.console);
			this.panelConsole.Controls.Add(this.toolStrip);
			this.panelConsole.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelConsole.Location = new System.Drawing.Point(0, 0);
			this.panelConsole.Name = "panelConsole";
			this.panelConsole.Padding = new System.Windows.Forms.Padding(1, 23, 1, 1);
			this.panelConsole.ShowBorder = true;
			this.panelConsole.ShowTitle = true;
			this.panelConsole.Size = new System.Drawing.Size(600, 400);
			this.panelConsole.TabIndex = 2;
			this.panelConsole.Title = "Secure Shell";
			// 
			// console
			// 
			this.console.ButtonImage = ((System.Drawing.Image)(resources.GetObject("console.ButtonImage")));
			this.console.Dock = System.Windows.Forms.DockStyle.Fill;
			this.console.Location = new System.Drawing.Point(1, 48);
			this.console.Name = "console";
			this.console.Size = new System.Drawing.Size(598, 351);
			this.console.TabIndex = 1;
			this.console.Execute += new System.EventHandler(this.OnExecuteCommand);
			this.console.Cancel += new System.EventHandler(this.OnCancelCommand);
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelHostname,
            this.separator1,
            this.buttonConnect,
            this.buttonDisconnect});
			this.toolStrip.Location = new System.Drawing.Point(1, 23);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(598, 25);
			this.toolStrip.TabIndex = 0;
			this.toolStrip.Text = "toolStrip1";
			// 
			// labelHostname
			// 
			this.labelHostname.Name = "labelHostname";
			this.labelHostname.Size = new System.Drawing.Size(62, 22);
			this.labelHostname.Text = "Hostname";
			// 
			// separator1
			// 
			this.separator1.Name = "separator1";
			this.separator1.Size = new System.Drawing.Size(6, 25);
			// 
			// buttonConnect
			// 
			this.buttonConnect.Image = global::PlanetLab.Resources.Connect_16;
			this.buttonConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.Size = new System.Drawing.Size(72, 22);
			this.buttonConnect.Text = "&Connect";
			this.buttonConnect.Click += new System.EventHandler(this.OnConnect);
			// 
			// buttonDisconnect
			// 
			this.buttonDisconnect.Enabled = false;
			this.buttonDisconnect.Image = global::PlanetLab.Resources.Disconnect_16;
			this.buttonDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonDisconnect.Name = "buttonDisconnect";
			this.buttonDisconnect.Size = new System.Drawing.Size(86, 22);
			this.buttonDisconnect.Text = "&Disconnect";
			this.buttonDisconnect.Click += new System.EventHandler(this.OnDisconnect);
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "Header_16.png");
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Title = "Export Settings";
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "XML files (*.xml)|*.xml";
			this.openFileDialog.Title = "Import Settings";
			// 
			// ControlSession
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelConsole);
			this.Enabled = false;
			this.Name = "ControlSession";
			this.Size = new System.Drawing.Size(600, 400);
			this.Controls.SetChildIndex(this.panelConsole, 0);
			this.panelConsole.ResumeLayout(false);
			this.panelConsole.PerformLayout();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private Controls.ControlConsole console;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton buttonConnect;
		private System.Windows.Forms.ToolStripButton buttonDisconnect;
		private System.Windows.Forms.ToolStripLabel labelHostname;
		private System.Windows.Forms.ToolStripSeparator separator1;
		private DotNetApi.Windows.Controls.ThemeControl panelConsole;

	}
}
