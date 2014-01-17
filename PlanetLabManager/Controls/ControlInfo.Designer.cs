﻿namespace PlanetLab.Controls
{
	partial class ControlInfo
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
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
			this.linkLabelSites = new System.Windows.Forms.LinkLabel();
			this.labelDescription = new System.Windows.Forms.Label();
			this.labelTitle = new System.Windows.Forms.Label();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.labelSites = new System.Windows.Forms.Label();
			this.labelConfiguration = new System.Windows.Forms.Label();
			this.labelSlices = new System.Windows.Forms.Label();
			this.labelNodes = new System.Windows.Forms.Label();
			this.linkLabelSlices = new System.Windows.Forms.LinkLabel();
			this.linkLabelNodes = new System.Windows.Forms.LinkLabel();
			this.settings = new PlanetLab.Controls.ControlSettings();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
			this.tableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBoxIcon
			// 
			this.pictureBoxIcon.Image = global::PlanetLab.Resources.GlobeLab_48;
			this.pictureBoxIcon.Location = new System.Drawing.Point(20, 41);
			this.pictureBoxIcon.Name = "pictureBoxIcon";
			this.pictureBoxIcon.Size = new System.Drawing.Size(48, 48);
			this.pictureBoxIcon.TabIndex = 0;
			this.pictureBoxIcon.TabStop = false;
			// 
			// linkLabelSites
			// 
			this.linkLabelSites.AutoSize = true;
			this.linkLabelSites.Location = new System.Drawing.Point(3, 49);
			this.linkLabelSites.Margin = new System.Windows.Forms.Padding(3);
			this.linkLabelSites.Name = "linkLabelSites";
			this.linkLabelSites.Size = new System.Drawing.Size(30, 13);
			this.linkLabelSites.TabIndex = 5;
			this.linkLabelSites.TabStop = true;
			this.linkLabelSites.Text = "Sites";
			this.linkLabelSites.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnSitesClick);
			// 
			// labelDescription
			// 
			this.labelDescription.AutoSize = true;
			this.tableLayoutPanel.SetColumnSpan(this.labelDescription, 2);
			this.labelDescription.Location = new System.Drawing.Point(3, 30);
			this.labelDescription.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(385, 13);
			this.labelDescription.TabIndex = 4;
			this.labelDescription.Text = "You can use the PlanetLab project to perform distributed Internet measurements.";
			// 
			// labelTitle
			// 
			this.labelTitle.AutoSize = true;
			this.tableLayoutPanel.SetColumnSpan(this.labelTitle, 2);
			this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTitle.Location = new System.Drawing.Point(3, 0);
			this.labelTitle.Name = "labelTitle";
			this.labelTitle.Size = new System.Drawing.Size(90, 20);
			this.labelTitle.TabIndex = 1;
			this.labelTitle.Text = "PlanetLab";
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Controls.Add(this.labelTitle, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.linkLabelSites, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.labelDescription, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.labelSites, 1, 2);
			this.tableLayoutPanel.Controls.Add(this.labelConfiguration, 0, 5);
			this.tableLayoutPanel.Controls.Add(this.labelSlices, 1, 4);
			this.tableLayoutPanel.Controls.Add(this.labelNodes, 1, 3);
			this.tableLayoutPanel.Controls.Add(this.linkLabelSlices, 0, 4);
			this.tableLayoutPanel.Controls.Add(this.linkLabelNodes, 0, 3);
			this.tableLayoutPanel.Controls.Add(this.settings, 0, 6);
			this.tableLayoutPanel.Location = new System.Drawing.Point(75, 42);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 7;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(721, 554);
			this.tableLayoutPanel.TabIndex = 5;
			// 
			// labelSites
			// 
			this.labelSites.AutoSize = true;
			this.labelSites.Location = new System.Drawing.Point(47, 49);
			this.labelSites.Margin = new System.Windows.Forms.Padding(3);
			this.labelSites.Name = "labelSites";
			this.labelSites.Size = new System.Drawing.Size(307, 13);
			this.labelSites.TabIndex = 6;
			this.labelSites.Text = "A site is a physical location where PlanetLab nodes are located.";
			// 
			// labelConfiguration
			// 
			this.labelConfiguration.AutoSize = true;
			this.tableLayoutPanel.SetColumnSpan(this.labelConfiguration, 2);
			this.labelConfiguration.Location = new System.Drawing.Point(3, 143);
			this.labelConfiguration.Margin = new System.Windows.Forms.Padding(3, 40, 3, 3);
			this.labelConfiguration.Name = "labelConfiguration";
			this.labelConfiguration.Size = new System.Drawing.Size(231, 13);
			this.labelConfiguration.TabIndex = 11;
			this.labelConfiguration.Text = "The PlanetLab uses the following configuration.";
			// 
			// labelSlices
			// 
			this.labelSlices.AutoSize = true;
			this.labelSlices.Location = new System.Drawing.Point(47, 87);
			this.labelSlices.Margin = new System.Windows.Forms.Padding(3);
			this.labelSlices.Name = "labelSlices";
			this.labelSlices.Size = new System.Drawing.Size(390, 13);
			this.labelSlices.TabIndex = 7;
			this.labelSlices.Text = "A slice is a set of allocated resources distributed across multiple PlanetLab nod" +
    "es.";
			// 
			// labelNodes
			// 
			this.labelNodes.AutoSize = true;
			this.labelNodes.Location = new System.Drawing.Point(47, 68);
			this.labelNodes.Margin = new System.Windows.Forms.Padding(3);
			this.labelNodes.Name = "labelNodes";
			this.labelNodes.Size = new System.Drawing.Size(355, 13);
			this.labelNodes.TabIndex = 13;
			this.labelNodes.Text = "A node is a dedicated server that runs components of PlanetLab services.";
			// 
			// linkLabelSlices
			// 
			this.linkLabelSlices.AutoSize = true;
			this.linkLabelSlices.Location = new System.Drawing.Point(3, 87);
			this.linkLabelSlices.Margin = new System.Windows.Forms.Padding(3);
			this.linkLabelSlices.Name = "linkLabelSlices";
			this.linkLabelSlices.Size = new System.Drawing.Size(35, 13);
			this.linkLabelSlices.TabIndex = 9;
			this.linkLabelSlices.TabStop = true;
			this.linkLabelSlices.Text = "Slices";
			this.linkLabelSlices.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnSlicesClick);
			// 
			// linkLabelNodes
			// 
			this.linkLabelNodes.AutoSize = true;
			this.linkLabelNodes.Location = new System.Drawing.Point(3, 68);
			this.linkLabelNodes.Margin = new System.Windows.Forms.Padding(3);
			this.linkLabelNodes.Name = "linkLabelNodes";
			this.linkLabelNodes.Size = new System.Drawing.Size(38, 13);
			this.linkLabelNodes.TabIndex = 10;
			this.linkLabelNodes.TabStop = true;
			this.linkLabelNodes.Text = "Nodes";
			this.linkLabelNodes.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnNodesClick);
			// 
			// settings
			// 
			this.settings.AutoScroll = true;
			this.tableLayoutPanel.SetColumnSpan(this.settings, 2);
			this.settings.Config = null;
			this.settings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.settings.Location = new System.Drawing.Point(3, 162);
			this.settings.Name = "settings";
			this.settings.Size = new System.Drawing.Size(715, 389);
			this.settings.TabIndex = 14;
			// 
			// ControlInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel);
			this.Controls.Add(this.pictureBoxIcon);
			this.Name = "ControlInfo";
			this.ShowBorder = true;
			this.ShowTitle = true;
			this.Size = new System.Drawing.Size(800, 600);
			this.Title = "Settings";
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBoxIcon;
		private System.Windows.Forms.LinkLabel linkLabelSites;
		private System.Windows.Forms.Label labelDescription;
		private System.Windows.Forms.Label labelTitle;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label labelSites;
		private System.Windows.Forms.LinkLabel linkLabelSlices;
		private System.Windows.Forms.Label labelSlices;
		private System.Windows.Forms.LinkLabel linkLabelNodes;
		private System.Windows.Forms.Label labelConfiguration;
		private System.Windows.Forms.Label labelNodes;
		private ControlSettings settings;
	}
}
