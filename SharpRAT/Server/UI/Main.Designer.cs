namespace Server
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.userView = new System.Windows.Forms.ListView();
            this.userHeader = new System.Windows.Forms.ColumnHeader();
            this.ipHeader = new System.Windows.Forms.ColumnHeader();
            this.portHeader = new System.Windows.Forms.ColumnHeader();
            this.userMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sendMessageBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableTaskmanagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shutdownClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uiUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.statusLbl = new System.Windows.Forms.Label();
            this.userMenuStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // userView
            // 
            this.userView.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.userView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.userHeader,
            this.ipHeader,
            this.portHeader});
            this.userView.ContextMenuStrip = this.userMenuStrip;
            this.userView.Location = new System.Drawing.Point(12, 27);
            this.userView.Name = "userView";
            this.userView.Size = new System.Drawing.Size(776, 401);
            this.userView.TabIndex = 8;
            this.userView.UseCompatibleStateImageBehavior = false;
            // 
            // userHeader
            // 
            this.userHeader.Text = "User";
            this.userHeader.Width = 120;
            // 
            // ipHeader
            // 
            this.ipHeader.Text = "IP Address";
            this.ipHeader.Width = 200;
            // 
            // portHeader
            // 
            this.portHeader.Text = "Port";
            this.portHeader.Width = 120;
            // 
            // userMenuStrip
            // 
            this.userMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendMessageBoxToolStripMenuItem,
            this.userControlToolStripMenuItem});
            this.userMenuStrip.Name = "contextMenuStrip1";
            this.userMenuStrip.Size = new System.Drawing.Size(170, 48);
            this.userMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.userMenuStrip_Opening);
            // 
            // sendMessageBoxToolStripMenuItem
            // 
            this.sendMessageBoxToolStripMenuItem.Name = "sendMessageBoxToolStripMenuItem";
            this.sendMessageBoxToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.sendMessageBoxToolStripMenuItem.Text = "Send MessageBox";
            this.sendMessageBoxToolStripMenuItem.Click += new System.EventHandler(this.sendMessageBoxToolStripMenuItem_Click);
            // 
            // userControlToolStripMenuItem
            // 
            this.userControlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disableTaskmanagerToolStripMenuItem,
            this.shutdownClientToolStripMenuItem});
            this.userControlToolStripMenuItem.Name = "userControlToolStripMenuItem";
            this.userControlToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.userControlToolStripMenuItem.Text = "User Control";
            // 
            // disableTaskmanagerToolStripMenuItem
            // 
            this.disableTaskmanagerToolStripMenuItem.CheckOnClick = true;
            this.disableTaskmanagerToolStripMenuItem.Name = "disableTaskmanagerToolStripMenuItem";
            this.disableTaskmanagerToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.disableTaskmanagerToolStripMenuItem.Text = "Disable Taskmanager";
            this.disableTaskmanagerToolStripMenuItem.CheckedChanged += new System.EventHandler(this.disableTaskmanagerToolStripMenuItem_CheckedChanged);
            // 
            // shutdownClientToolStripMenuItem
            // 
            this.shutdownClientToolStripMenuItem.Name = "shutdownClientToolStripMenuItem";
            this.shutdownClientToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.shutdownClientToolStripMenuItem.Text = "Shutdown SharpRAT client";
            this.shutdownClientToolStripMenuItem.Click += new System.EventHandler(this.shutdownClientToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.fileToolStripMenuItem.Text = "Menu";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // uiUpdateTimer
            // 
            this.uiUpdateTimer.Enabled = true;
            this.uiUpdateTimer.Tick += new System.EventHandler(this.uiUpdateTimer_Tick);
            // 
            // statusLbl
            // 
            this.statusLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusLbl.AutoSize = true;
            this.statusLbl.Location = new System.Drawing.Point(9, 431);
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(50, 15);
            this.statusLbl.TabIndex = 9;
            this.statusLbl.Text = "Status: -";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusLbl);
            this.Controls.Add(this.userView);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(450, 250);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SharpRAT";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Main_Load);
            this.userMenuStrip.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView userView;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ColumnHeader userHeader;
        private System.Windows.Forms.Timer uiUpdateTimer;
        private ContextMenuStrip userMenuStrip;
        private ToolStripMenuItem sendMessageBoxToolStripMenuItem;
        private Label statusLbl;
        private ColumnHeader ipHeader;
        private ColumnHeader portHeader;
        private ToolStripMenuItem userControlToolStripMenuItem;
        private ToolStripMenuItem disableTaskmanagerToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem shutdownClientToolStripMenuItem;
    }
}