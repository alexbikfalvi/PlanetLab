namespace PlanetLabConfig
{
	partial class FormMain
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageBase64 = new System.Windows.Forms.TabPage();
			this.labelRegistryValue = new System.Windows.Forms.Label();
			this.textBoxRegistryValue = new System.Windows.Forms.TextBox();
			this.textBoxBase64 = new System.Windows.Forms.TextBox();
			this.buttonBase64 = new System.Windows.Forms.Button();
			this.textBoxRegistryKey = new System.Windows.Forms.TextBox();
			this.labelRegistryKey = new System.Windows.Forms.Label();
			this.tabPageEncrypt = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxIv = new System.Windows.Forms.TextBox();
			this.textBoxKey = new System.Windows.Forms.TextBox();
			this.buttonEncrypt = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.tabPageExpires = new System.Windows.Forms.TabPage();
			this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.textBoxDate = new System.Windows.Forms.TextBox();
			this.tabControl.SuspendLayout();
			this.tabPageBase64.SuspendLayout();
			this.tabPageEncrypt.SuspendLayout();
			this.tabPageExpires.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageBase64);
			this.tabControl.Controls.Add(this.tabPageEncrypt);
			this.tabControl.Controls.Add(this.tabPageExpires);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(584, 362);
			this.tabControl.TabIndex = 0;
			// 
			// tabPageBase64
			// 
			this.tabPageBase64.Controls.Add(this.labelRegistryValue);
			this.tabPageBase64.Controls.Add(this.textBoxRegistryValue);
			this.tabPageBase64.Controls.Add(this.textBoxBase64);
			this.tabPageBase64.Controls.Add(this.buttonBase64);
			this.tabPageBase64.Controls.Add(this.textBoxRegistryKey);
			this.tabPageBase64.Controls.Add(this.labelRegistryKey);
			this.tabPageBase64.Location = new System.Drawing.Point(4, 22);
			this.tabPageBase64.Name = "tabPageBase64";
			this.tabPageBase64.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageBase64.Size = new System.Drawing.Size(576, 336);
			this.tabPageBase64.TabIndex = 0;
			this.tabPageBase64.Text = "Base64";
			this.tabPageBase64.UseVisualStyleBackColor = true;
			// 
			// labelRegistryValue
			// 
			this.labelRegistryValue.AutoSize = true;
			this.labelRegistryValue.Location = new System.Drawing.Point(8, 35);
			this.labelRegistryValue.Name = "labelRegistryValue";
			this.labelRegistryValue.Size = new System.Drawing.Size(77, 13);
			this.labelRegistryValue.TabIndex = 2;
			this.labelRegistryValue.Text = "Registry value:";
			// 
			// textBoxRegistryValue
			// 
			this.textBoxRegistryValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxRegistryValue.Location = new System.Drawing.Point(91, 32);
			this.textBoxRegistryValue.Name = "textBoxRegistryValue";
			this.textBoxRegistryValue.Size = new System.Drawing.Size(477, 20);
			this.textBoxRegistryValue.TabIndex = 3;
			// 
			// textBoxBase64
			// 
			this.textBoxBase64.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxBase64.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxBase64.Location = new System.Drawing.Point(91, 87);
			this.textBoxBase64.Multiline = true;
			this.textBoxBase64.Name = "textBoxBase64";
			this.textBoxBase64.ReadOnly = true;
			this.textBoxBase64.Size = new System.Drawing.Size(477, 241);
			this.textBoxBase64.TabIndex = 5;
			// 
			// buttonBase64
			// 
			this.buttonBase64.Location = new System.Drawing.Point(91, 58);
			this.buttonBase64.Name = "buttonBase64";
			this.buttonBase64.Size = new System.Drawing.Size(75, 23);
			this.buttonBase64.TabIndex = 4;
			this.buttonBase64.Text = "Base64";
			this.buttonBase64.UseVisualStyleBackColor = true;
			this.buttonBase64.Click += new System.EventHandler(this.OnBase64);
			// 
			// textBoxRegistryKey
			// 
			this.textBoxRegistryKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxRegistryKey.Location = new System.Drawing.Point(91, 6);
			this.textBoxRegistryKey.Name = "textBoxRegistryKey";
			this.textBoxRegistryKey.Size = new System.Drawing.Size(477, 20);
			this.textBoxRegistryKey.TabIndex = 1;
			// 
			// labelRegistryKey
			// 
			this.labelRegistryKey.AutoSize = true;
			this.labelRegistryKey.Location = new System.Drawing.Point(8, 9);
			this.labelRegistryKey.Name = "labelRegistryKey";
			this.labelRegistryKey.Size = new System.Drawing.Size(68, 13);
			this.labelRegistryKey.TabIndex = 0;
			this.labelRegistryKey.Text = "Registry key:";
			// 
			// tabPageEncrypt
			// 
			this.tabPageEncrypt.Controls.Add(this.label2);
			this.tabPageEncrypt.Controls.Add(this.label1);
			this.tabPageEncrypt.Controls.Add(this.textBoxIv);
			this.tabPageEncrypt.Controls.Add(this.textBoxKey);
			this.tabPageEncrypt.Controls.Add(this.buttonEncrypt);
			this.tabPageEncrypt.Location = new System.Drawing.Point(4, 22);
			this.tabPageEncrypt.Name = "tabPageEncrypt";
			this.tabPageEncrypt.Size = new System.Drawing.Size(576, 336);
			this.tabPageEncrypt.TabIndex = 1;
			this.tabPageEncrypt.Text = "Encrypt";
			this.tabPageEncrypt.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(20, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "&IV:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(28, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Key:";
			// 
			// textBoxIv
			// 
			this.textBoxIv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxIv.Location = new System.Drawing.Point(91, 32);
			this.textBoxIv.Name = "textBoxIv";
			this.textBoxIv.Size = new System.Drawing.Size(477, 20);
			this.textBoxIv.TabIndex = 4;
			this.textBoxIv.Text = "0x62, 0x36, 0x61, 0x5B, 0xA0, 0xA4, 0xF0, 0x4E, 0xFE, 0x81, 0x2C, 0xA4, 0xD3, 0x1" +
    "6, 0x26, 0xEE";
			// 
			// textBoxKey
			// 
			this.textBoxKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxKey.Location = new System.Drawing.Point(91, 6);
			this.textBoxKey.Name = "textBoxKey";
			this.textBoxKey.Size = new System.Drawing.Size(477, 20);
			this.textBoxKey.TabIndex = 2;
			this.textBoxKey.Text = "0x7E, 0x31, 0xC9, 0xD9, 0x0F, 0x01, 0x28, 0x11, 0xD4, 0xB3, 0xC4, 0x12, 0x44, 0x0" +
    "D, 0x4B, 0x8C, 0xEE, 0xDB, 0xF1, 0x4D, 0xCF, 0x01, 0xF8, 0x00, 0xE4, 0x3E, 0x33," +
    " 0xF5, 0x02, 0x48, 0xD4, 0x8D";
			// 
			// buttonEncrypt
			// 
			this.buttonEncrypt.Location = new System.Drawing.Point(91, 58);
			this.buttonEncrypt.Name = "buttonEncrypt";
			this.buttonEncrypt.Size = new System.Drawing.Size(75, 23);
			this.buttonEncrypt.TabIndex = 0;
			this.buttonEncrypt.Text = "Encrypt";
			this.buttonEncrypt.UseVisualStyleBackColor = true;
			this.buttonEncrypt.Click += new System.EventHandler(this.OnEncrypt);
			// 
			// openFileDialog
			// 
			this.openFileDialog.Title = "Open File to Encrypt";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Title = "Select Encrypted File Name";
			// 
			// tabPageExpires
			// 
			this.tabPageExpires.Controls.Add(this.textBoxDate);
			this.tabPageExpires.Controls.Add(this.dateTimePicker);
			this.tabPageExpires.Location = new System.Drawing.Point(4, 22);
			this.tabPageExpires.Name = "tabPageExpires";
			this.tabPageExpires.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageExpires.Size = new System.Drawing.Size(576, 336);
			this.tabPageExpires.TabIndex = 2;
			this.tabPageExpires.Text = "Expires";
			this.tabPageExpires.UseVisualStyleBackColor = true;
			// 
			// dateTimePicker
			// 
			this.dateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dateTimePicker.Location = new System.Drawing.Point(8, 6);
			this.dateTimePicker.Name = "dateTimePicker";
			this.dateTimePicker.Size = new System.Drawing.Size(560, 20);
			this.dateTimePicker.TabIndex = 0;
			this.dateTimePicker.ValueChanged += new System.EventHandler(this.OnDateChanged);
			// 
			// textBoxDate
			// 
			this.textBoxDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxDate.Location = new System.Drawing.Point(8, 32);
			this.textBoxDate.Name = "textBoxDate";
			this.textBoxDate.ReadOnly = true;
			this.textBoxDate.Size = new System.Drawing.Size(560, 20);
			this.textBoxDate.TabIndex = 1;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 362);
			this.Controls.Add(this.tabControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormMain";
			this.Text = "PlanetLab Configuration Editor";
			this.tabControl.ResumeLayout(false);
			this.tabPageBase64.ResumeLayout(false);
			this.tabPageBase64.PerformLayout();
			this.tabPageEncrypt.ResumeLayout(false);
			this.tabPageEncrypt.PerformLayout();
			this.tabPageExpires.ResumeLayout(false);
			this.tabPageExpires.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageBase64;
		private System.Windows.Forms.TextBox textBoxRegistryKey;
		private System.Windows.Forms.Label labelRegistryKey;
		private System.Windows.Forms.Button buttonBase64;
		private System.Windows.Forms.TextBox textBoxBase64;
		private System.Windows.Forms.Label labelRegistryValue;
		private System.Windows.Forms.TextBox textBoxRegistryValue;
		private System.Windows.Forms.TabPage tabPageEncrypt;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxIv;
		private System.Windows.Forms.TextBox textBoxKey;
		private System.Windows.Forms.Button buttonEncrypt;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.TabPage tabPageExpires;
		private System.Windows.Forms.DateTimePicker dateTimePicker;
		private System.Windows.Forms.TextBox textBoxDate;
	}
}

