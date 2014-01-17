﻿namespace PlanetLab.Forms
{
	partial class FormExportNodes
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.control = new PlanetLab.Controls.ControlExportNodes();
			this.SuspendLayout();
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
			this.saveFileDialog.Title = "Select File Name";
			// 
			// control
			// 
			this.control.Dock = System.Windows.Forms.DockStyle.Fill;
			this.control.Location = new System.Drawing.Point(0, 0);
			this.control.Name = "control";
			this.control.Size = new System.Drawing.Size(434, 212);
			this.control.TabIndex = 0;
			this.control.Closed += new System.EventHandler(this.OnClosed);
			// 
			// FormExportNodes
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(434, 212);
			this.Controls.Add(this.control);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(450, 250);
			this.Name = "FormExportNodes";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Export to CSV File";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private Controls.ControlExportNodes control;
	}
}