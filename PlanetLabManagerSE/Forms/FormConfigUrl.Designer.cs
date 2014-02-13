namespace PlanetLab.Forms
{
	partial class FormConfigUrl
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
			this.labelMessage = new System.Windows.Forms.Label();
			this.textBoxUrl = new System.Windows.Forms.TextBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonContinue = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelMessage
			// 
			this.labelMessage.AutoSize = true;
			this.labelMessage.Location = new System.Drawing.Point(12, 9);
			this.labelMessage.Name = "labelMessage";
			this.labelMessage.Size = new System.Drawing.Size(226, 13);
			this.labelMessage.TabIndex = 0;
			this.labelMessage.Text = "Enter the &web address of the configuration file:";
			// 
			// textBoxUrl
			// 
			this.textBoxUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxUrl.Location = new System.Drawing.Point(12, 25);
			this.textBoxUrl.Name = "textBoxUrl";
			this.textBoxUrl.Size = new System.Drawing.Size(360, 20);
			this.textBoxUrl.TabIndex = 1;
			this.textBoxUrl.TextChanged += new System.EventHandler(this.OnChanged);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(297, 77);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonContinue
			// 
			this.buttonContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonContinue.Location = new System.Drawing.Point(216, 77);
			this.buttonContinue.Name = "buttonContinue";
			this.buttonContinue.Size = new System.Drawing.Size(75, 23);
			this.buttonContinue.TabIndex = 3;
			this.buttonContinue.Text = "Continue";
			this.buttonContinue.UseVisualStyleBackColor = true;
			// 
			// FormConfigUrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 112);
			this.ControlBox = false;
			this.Controls.Add(this.buttonContinue);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.textBoxUrl);
			this.Controls.Add(this.labelMessage);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormConfigUrl";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Select Configuration File";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelMessage;
		private System.Windows.Forms.TextBox textBoxUrl;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonContinue;
	}
}