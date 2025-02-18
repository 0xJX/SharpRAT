namespace Server.UI
{
    partial class FileManager
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
            components = new System.ComponentModel.Container();
            fileListView = new ListView();
            nameHeader = new ColumnHeader();
            dateModHeader = new ColumnHeader();
            typeHeader = new ColumnHeader();
            sizeHeader = new ColumnHeader();
            fileMenuStrip = new ContextMenuStrip(components);
            openFileToolStripMenuItem = new ToolStripMenuItem();
            groupBox1 = new GroupBox();
            pathBox = new TextBox();
            uiUpdateTimer = new System.Windows.Forms.Timer(components);
            refreshDirectoryToolStripMenuItem = new ToolStripMenuItem();
            fileMenuStrip.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // fileListView
            // 
            fileListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            fileListView.Columns.AddRange(new ColumnHeader[] { nameHeader, dateModHeader, typeHeader, sizeHeader });
            fileListView.ContextMenuStrip = fileMenuStrip;
            fileListView.Location = new Point(12, 57);
            fileListView.Name = "fileListView";
            fileListView.Size = new Size(776, 381);
            fileListView.TabIndex = 0;
            fileListView.UseCompatibleStateImageBehavior = false;
            fileListView.Click += fileListView_Click;
            fileListView.MouseDoubleClick += fileListView_MouseDoubleClick;
            // 
            // nameHeader
            // 
            nameHeader.Text = "Name";
            nameHeader.Width = 200;
            // 
            // dateModHeader
            // 
            dateModHeader.Text = "Date modified";
            dateModHeader.Width = 200;
            // 
            // typeHeader
            // 
            typeHeader.Text = "Type";
            typeHeader.Width = 200;
            // 
            // sizeHeader
            // 
            sizeHeader.Text = "Size";
            sizeHeader.Width = 200;
            // 
            // fileMenuStrip
            // 
            fileMenuStrip.Items.AddRange(new ToolStripItem[] { openFileToolStripMenuItem, refreshDirectoryToolStripMenuItem });
            fileMenuStrip.Name = "contextMenuStrip1";
            fileMenuStrip.Size = new Size(181, 70);
            fileMenuStrip.Opening += fileMenuStrip_Opening;
            // 
            // openFileToolStripMenuItem
            // 
            openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            openFileToolStripMenuItem.Size = new Size(180, 22);
            openFileToolStripMenuItem.Text = "Open file";
            openFileToolStripMenuItem.Click += openFileToolStripMenuItem_Click;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(pathBox);
            groupBox1.Location = new Point(12, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(776, 46);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Current path";
            // 
            // pathBox
            // 
            pathBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pathBox.BackColor = Color.White;
            pathBox.BorderStyle = BorderStyle.FixedSingle;
            pathBox.Location = new Point(6, 17);
            pathBox.Name = "pathBox";
            pathBox.ReadOnly = true;
            pathBox.Size = new Size(764, 23);
            pathBox.TabIndex = 0;
            // 
            // uiUpdateTimer
            // 
            uiUpdateTimer.Enabled = true;
            uiUpdateTimer.Interval = 1;
            uiUpdateTimer.Tick += uiUpdateTimer_Tick;
            // 
            // refreshDirectoryToolStripMenuItem
            // 
            refreshDirectoryToolStripMenuItem.Name = "refreshDirectoryToolStripMenuItem";
            refreshDirectoryToolStripMenuItem.Size = new Size(180, 22);
            refreshDirectoryToolStripMenuItem.Text = "Refresh directory";
            refreshDirectoryToolStripMenuItem.Click += refreshDirectoryToolStripMenuItem_Click;
            // 
            // FileManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Controls.Add(fileListView);
            Name = "FileManager";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FileManager";
            Load += FileManager_Load;
            fileMenuStrip.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ListView fileListView;
        private ColumnHeader nameHeader;
        private ColumnHeader sizeHeader;
        private ColumnHeader typeHeader;
        private ColumnHeader dateModHeader;
        private GroupBox groupBox1;
        private TextBox pathBox;
        private System.Windows.Forms.Timer uiUpdateTimer;
        private ContextMenuStrip fileMenuStrip;
        private ToolStripMenuItem openFileToolStripMenuItem;
        private ToolStripMenuItem refreshDirectoryToolStripMenuItem;
    }
}