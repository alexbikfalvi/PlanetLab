namespace PlanetLab.Forms
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
			if (disposing)
			{
				// Dispose the forms.
				this.formAbout.Dispose();
				// Dispose the components.
				if (components != null)
				{
					components.Dispose();
				}
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statusLabelLeft = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusLabelMiddle = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusLabelRight = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusLabelRun = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusLabelConnection = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolSplitContainer = new DotNetApi.Windows.Controls.ToolSplitContainer();
			this.sideMenu = new DotNetApi.Windows.Controls.SideMenu();
			this.sideTreePlanetLab = new DotNetApi.Windows.Controls.SideTreeView();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.sideMenuPlanetLab = new DotNetApi.Windows.Controls.SideMenuItem();
			this.labelNotAvailable = new System.Windows.Forms.Label();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.toolTipNetworkStatus = new PlanetLab.Controls.NetworkStatusToolTip(this.components);
			this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer.ContentPanel.SuspendLayout();
			this.toolStripContainer.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer.SuspendLayout();
			this.statusStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.toolSplitContainer)).BeginInit();
			this.toolSplitContainer.Panel1.SuspendLayout();
			this.toolSplitContainer.Panel2.SuspendLayout();
			this.toolSplitContainer.SuspendLayout();
			this.sideMenu.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer
			// 
			// 
			// toolStripContainer.BottomToolStripPanel
			// 
			this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip);
			// 
			// toolStripContainer.ContentPanel
			// 
			this.toolStripContainer.ContentPanel.Controls.Add(this.toolSplitContainer);
			this.toolStripContainer.ContentPanel.Padding = new System.Windows.Forms.Padding(5);
			this.toolStripContainer.ContentPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
			this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(784, 516);
			this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer.Name = "toolStripContainer";
			this.toolStripContainer.Size = new System.Drawing.Size(784, 562);
			this.toolStripContainer.TabIndex = 0;
			this.toolStripContainer.Text = "toolStripContainer1";
			// 
			// toolStripContainer.TopToolStripPanel
			// 
			this.toolStripContainer.TopToolStripPanel.Controls.Add(this.menuStrip);
			// 
			// statusStrip
			// 
			this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelLeft,
            this.statusLabelMiddle,
            this.statusLabelRight,
            this.statusLabelRun,
            this.statusLabelConnection});
			this.statusStrip.Location = new System.Drawing.Point(0, 0);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.ShowItemToolTips = true;
			this.statusStrip.Size = new System.Drawing.Size(784, 22);
			this.statusStrip.TabIndex = 1;
			// 
			// statusLabelLeft
			// 
			this.statusLabelLeft.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.statusLabelLeft.Name = "statusLabelLeft";
			this.statusLabelLeft.Size = new System.Drawing.Size(39, 17);
			this.statusLabelLeft.Text = "Ready";
			// 
			// statusLabelMiddle
			// 
			this.statusLabelMiddle.Name = "statusLabelMiddle";
			this.statusLabelMiddle.Size = new System.Drawing.Size(514, 17);
			this.statusLabelMiddle.Spring = true;
			// 
			// statusLabelRight
			// 
			this.statusLabelRight.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.statusLabelRight.Name = "statusLabelRight";
			this.statusLabelRight.Size = new System.Drawing.Size(0, 17);
			// 
			// statusLabelRun
			// 
			this.statusLabelRun.Image = global::PlanetLab.Resources.RunConcurrentStop_16;
			this.statusLabelRun.Name = "statusLabelRun";
			this.statusLabelRun.Size = new System.Drawing.Size(135, 17);
			this.statusLabelRun.Text = "No background tasks";
			// 
			// statusLabelConnection
			// 
			this.statusLabelConnection.Image = global::PlanetLab.Resources.ConnectionSuccess_16;
			this.statusLabelConnection.Name = "statusLabelConnection";
			this.statusLabelConnection.Size = new System.Drawing.Size(81, 17);
			this.statusLabelConnection.Text = "Connected";
			this.statusLabelConnection.MouseEnter += new System.EventHandler(this.OnNetworkStatusEnter);
			this.statusLabelConnection.MouseLeave += new System.EventHandler(this.OnNetworkStatusLeave);
			// 
			// toolSplitContainer
			// 
			this.toolSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.toolSplitContainer.Location = new System.Drawing.Point(5, 5);
			this.toolSplitContainer.Name = "toolSplitContainer";
			// 
			// toolSplitContainer.Panel1
			// 
			this.toolSplitContainer.Panel1.Controls.Add(this.sideMenu);
			this.toolSplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(1);
			// 
			// toolSplitContainer.Panel2
			// 
			this.toolSplitContainer.Panel2.Controls.Add(this.labelNotAvailable);
			this.toolSplitContainer.Panel2Border = false;
			this.toolSplitContainer.Size = new System.Drawing.Size(774, 506);
			this.toolSplitContainer.SplitterDistance = 250;
			this.toolSplitContainer.SplitterWidth = 5;
			this.toolSplitContainer.TabIndex = 0;
			// 
			// sideMenu
			// 
			this.sideMenu.Controls.Add(this.sideTreePlanetLab);
			this.sideMenu.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sideMenu.ItemHeight = 48;
			this.sideMenu.Items.AddRange(new DotNetApi.Windows.Controls.SideMenuItem[] {
            this.sideMenuPlanetLab});
			this.sideMenu.Location = new System.Drawing.Point(1, 1);
			this.sideMenu.MinimizedItemWidth = 25;
			this.sideMenu.MinimumPanelHeight = 50;
			this.sideMenu.Name = "sideMenu";
			this.sideMenu.Padding = new System.Windows.Forms.Padding(0, 22, 0, 104);
			this.sideMenu.SelectedIndex = 0;
			this.sideMenu.SelectedItem = this.sideMenuPlanetLab;
			this.sideMenu.Size = new System.Drawing.Size(248, 504);
			this.sideMenu.TabIndex = 0;
			this.sideMenu.Title = "";
			this.sideMenu.VisibleItems = 1;
			// 
			// sideTreePlanetLab
			// 
			this.sideTreePlanetLab.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.sideTreePlanetLab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sideTreePlanetLab.FullRowSelect = true;
			this.sideTreePlanetLab.HideSelection = false;
			this.sideTreePlanetLab.ImageIndex = 0;
			this.sideTreePlanetLab.ImageList = this.imageList;
			this.sideTreePlanetLab.ItemHeight = 20;
			this.sideTreePlanetLab.Location = new System.Drawing.Point(0, 22);
			this.sideTreePlanetLab.Name = "sideTreePlanetLab";
			this.sideTreePlanetLab.SelectedImageIndex = 0;
			this.sideTreePlanetLab.ShowLines = false;
			this.sideTreePlanetLab.ShowRootLines = false;
			this.sideTreePlanetLab.Size = new System.Drawing.Size(248, 378);
			this.sideTreePlanetLab.TabIndex = 0;
			this.sideTreePlanetLab.Visible = false;
			this.sideTreePlanetLab.ControlChanged += new DotNetApi.Windows.Controls.ControlChangedEventHandler(this.OnControlChanged);
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "ServerBrowse");
			this.imageList.Images.SetKeyName(1, "ServerDatabase");
			this.imageList.Images.SetKeyName(2, "ServersDatabase");
			this.imageList.Images.SetKeyName(3, "ServerCube");
			this.imageList.Images.SetKeyName(4, "ServerTask");
			this.imageList.Images.SetKeyName(5, "ServersCube");
			this.imageList.Images.SetKeyName(6, "ServersGlobe");
			this.imageList.Images.SetKeyName(7, "ServerToolbox");
			this.imageList.Images.SetKeyName(8, "File");
			this.imageList.Images.SetKeyName(9, "FileXml");
			this.imageList.Images.SetKeyName(10, "FileVideo");
			this.imageList.Images.SetKeyName(11, "FileUser");
			this.imageList.Images.SetKeyName(12, "FileComment");
			this.imageList.Images.SetKeyName(13, "FileGraphLine");
			this.imageList.Images.SetKeyName(14, "Categories");
			this.imageList.Images.SetKeyName(15, "Comments");
			this.imageList.Images.SetKeyName(16, "CommentVideo");
			this.imageList.Images.SetKeyName(17, "CommentUser");
			this.imageList.Images.SetKeyName(18, "CommentPlay");
			this.imageList.Images.SetKeyName(19, "Settings");
			this.imageList.Images.SetKeyName(20, "ServerDown");
			this.imageList.Images.SetKeyName(21, "ServerUp");
			this.imageList.Images.SetKeyName(22, "ServerWarning");
			this.imageList.Images.SetKeyName(23, "ServerBusy");
			this.imageList.Images.SetKeyName(24, "Log");
			this.imageList.Images.SetKeyName(25, "QueryDatabase");
			this.imageList.Images.SetKeyName(26, "Cube");
			this.imageList.Images.SetKeyName(27, "Cubes");
			this.imageList.Images.SetKeyName(28, "GlobeBrowse");
			this.imageList.Images.SetKeyName(29, "GlobeSettings");
			this.imageList.Images.SetKeyName(30, "GlobeSchema");
			this.imageList.Images.SetKeyName(31, "GlobeObject");
			this.imageList.Images.SetKeyName(32, "GlobeConsole");
			this.imageList.Images.SetKeyName(33, "GlobeNode");
			this.imageList.Images.SetKeyName(34, "GlobeTask");
			this.imageList.Images.SetKeyName(35, "GlobeScript");
			this.imageList.Images.SetKeyName(36, "GlobePlayStart");
			this.imageList.Images.SetKeyName(37, "GlobePlayStop");
			this.imageList.Images.SetKeyName(38, "GlobeSuccess");
			this.imageList.Images.SetKeyName(39, "GlobeWarning");
			this.imageList.Images.SetKeyName(40, "GlobeError");
			this.imageList.Images.SetKeyName(41, "GlobeCanceled");
			this.imageList.Images.SetKeyName(42, "TestGlobeGoto");
			this.imageList.Images.SetKeyName(43, "TestConnectGoto");
			this.imageList.Images.SetKeyName(44, "FolderClosed");
			this.imageList.Images.SetKeyName(45, "FolderClosedClock");
			this.imageList.Images.SetKeyName(46, "FolderClosedComment");
			this.imageList.Images.SetKeyName(47, "FolderClosedGlobe");
			this.imageList.Images.SetKeyName(48, "FolderClosedPlayBlue");
			this.imageList.Images.SetKeyName(49, "FolderClosedPlayGreen");
			this.imageList.Images.SetKeyName(50, "FolderClosedTask");
			this.imageList.Images.SetKeyName(51, "FolderClosedUser");
			this.imageList.Images.SetKeyName(52, "FolderClosedVideo");
			this.imageList.Images.SetKeyName(53, "FolderClosedXml");
			this.imageList.Images.SetKeyName(54, "FolderOpen");
			this.imageList.Images.SetKeyName(55, "FolderOpenClock");
			this.imageList.Images.SetKeyName(56, "FolderOpenComment");
			this.imageList.Images.SetKeyName(57, "FolderOpenGlobe");
			this.imageList.Images.SetKeyName(58, "FolderOpenPlayBlue");
			this.imageList.Images.SetKeyName(59, "FolderOpenPlayGreen");
			this.imageList.Images.SetKeyName(60, "FolderOpenTask");
			this.imageList.Images.SetKeyName(61, "FolderOpenUser");
			this.imageList.Images.SetKeyName(62, "FolderOpenVideo");
			this.imageList.Images.SetKeyName(63, "FolderOpenXml");
			this.imageList.Images.SetKeyName(64, "ToolboxPickAxe");
			// 
			// sideMenuPlanetLab
			// 
			this.sideMenuPlanetLab.Control = this.sideTreePlanetLab;
			this.sideMenuPlanetLab.ImageLarge = global::PlanetLab.Resources.GlobeLab_32;
			this.sideMenuPlanetLab.ImageSmall = global::PlanetLab.Resources.GlobeLab_16;
			this.sideMenuPlanetLab.Index = -1;
			this.sideMenuPlanetLab.Text = "PlanetLab";
			// 
			// labelNotAvailable
			// 
			this.labelNotAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelNotAvailable.Location = new System.Drawing.Point(0, 0);
			this.labelNotAvailable.Name = "labelNotAvailable";
			this.labelNotAvailable.Size = new System.Drawing.Size(519, 506);
			this.labelNotAvailable.TabIndex = 1;
			this.labelNotAvailable.Text = "Feature not available";
			this.labelNotAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// menuStrip
			// 
			this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemHelp});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(784, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip1";
			// 
			// menuItemFile
			// 
			this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemExit});
			this.menuItemFile.Name = "menuItemFile";
			this.menuItemFile.Size = new System.Drawing.Size(37, 20);
			this.menuItemFile.Text = "&File";
			// 
			// menuItemExit
			// 
			this.menuItemExit.Name = "menuItemExit";
			this.menuItemExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.menuItemExit.Size = new System.Drawing.Size(134, 22);
			this.menuItemExit.Text = "&Exit";
			this.menuItemExit.Click += new System.EventHandler(this.OnExit);
			// 
			// menuItemHelp
			// 
			this.menuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAbout});
			this.menuItemHelp.Name = "menuItemHelp";
			this.menuItemHelp.Size = new System.Drawing.Size(44, 20);
			this.menuItemHelp.Text = "&Help";
			// 
			// menuItemAbout
			// 
			this.menuItemAbout.Name = "menuItemAbout";
			this.menuItemAbout.Size = new System.Drawing.Size(116, 22);
			this.menuItemAbout.Text = "&About...";
			this.menuItemAbout.Click += new System.EventHandler(this.OnAbout);
			// 
			// toolTipNetworkStatus
			// 
			this.toolTipNetworkStatus.OwnerDraw = true;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Controls.Add(this.toolStripContainer);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip;
			this.Name = "FormMain";
			this.Text = "PlanetLab Manager (Student Edition)";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer.ContentPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.PerformLayout();
			this.toolStripContainer.ResumeLayout(false);
			this.toolStripContainer.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.toolSplitContainer.Panel1.ResumeLayout(false);
			this.toolSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.toolSplitContainer)).EndInit();
			this.toolSplitContainer.ResumeLayout(false);
			this.sideMenu.ResumeLayout(false);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem menuItemFile;
		private System.Windows.Forms.ToolStripMenuItem menuItemExit;
		private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
		private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
		private DotNetApi.Windows.Controls.ToolSplitContainer toolSplitContainer;
		private DotNetApi.Windows.Controls.SideMenu sideMenu;
		private DotNetApi.Windows.Controls.SideMenuItem sideMenuPlanetLab;
		private DotNetApi.Windows.Controls.SideTreeView sideTreePlanetLab;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel statusLabelLeft;
		private System.Windows.Forms.ToolStripStatusLabel statusLabelMiddle;
		private System.Windows.Forms.ToolStripStatusLabel statusLabelRight;
		private System.Windows.Forms.ToolStripStatusLabel statusLabelRun;
		private System.Windows.Forms.ToolStripStatusLabel statusLabelConnection;
		private System.Windows.Forms.Label labelNotAvailable;
		private System.Windows.Forms.ImageList imageList;
		private Controls.NetworkStatusToolTip toolTipNetworkStatus;
	}
}

