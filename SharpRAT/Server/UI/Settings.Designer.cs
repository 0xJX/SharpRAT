namespace Server.UI
{
    partial class Settings
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.mainPage = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.portNumericUD = new System.Windows.Forms.NumericUpDown();
            this.logPage = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.refreshBtn = new System.Windows.Forms.Button();
            this.logAutoUpdate = new System.Windows.Forms.CheckBox();
            this.testWriteBtn = new System.Windows.Forms.Button();
            this.clearLogBtn = new System.Windows.Forms.Button();
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.aboutPage = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.versionLbl = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.imgBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.updateLogTimer = new System.Windows.Forms.Timer(this.components);
            this.formEffectTimer = new System.Windows.Forms.Timer(this.components);
            this.serverCheckbox = new System.Windows.Forms.CheckBox();
            this.tabControl.SuspendLayout();
            this.mainPage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portNumericUD)).BeginInit();
            this.logPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.aboutPage.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.mainPage);
            this.tabControl.Controls.Add(this.logPage);
            this.tabControl.Controls.Add(this.aboutPage);
            this.tabControl.Location = new System.Drawing.Point(12, 72);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(565, 322);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // mainPage
            // 
            this.mainPage.Controls.Add(this.groupBox2);
            this.mainPage.Location = new System.Drawing.Point(4, 24);
            this.mainPage.Name = "mainPage";
            this.mainPage.Padding = new System.Windows.Forms.Padding(3);
            this.mainPage.Size = new System.Drawing.Size(557, 294);
            this.mainPage.TabIndex = 0;
            this.mainPage.Text = "Main";
            this.mainPage.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.serverCheckbox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.portNumericUD);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(255, 74);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Server settings";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "Port:";
            // 
            // portNumericUD
            // 
            this.portNumericUD.Location = new System.Drawing.Point(70, 19);
            this.portNumericUD.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portNumericUD.Minimum = new decimal(new int[] {
            1025,
            0,
            0,
            0});
            this.portNumericUD.Name = "portNumericUD";
            this.portNumericUD.Size = new System.Drawing.Size(179, 23);
            this.portNumericUD.TabIndex = 0;
            this.portNumericUD.Value = new decimal(new int[] {
            1025,
            0,
            0,
            0});
            this.portNumericUD.ValueChanged += new System.EventHandler(this.portNumericUD_ValueChanged);
            // 
            // logPage
            // 
            this.logPage.Controls.Add(this.groupBox1);
            this.logPage.Controls.Add(this.logTextBox);
            this.logPage.Location = new System.Drawing.Point(4, 24);
            this.logPage.Name = "logPage";
            this.logPage.Padding = new System.Windows.Forms.Padding(3);
            this.logPage.Size = new System.Drawing.Size(557, 294);
            this.logPage.TabIndex = 1;
            this.logPage.Text = "Log";
            this.logPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.refreshBtn);
            this.groupBox1.Controls.Add(this.logAutoUpdate);
            this.groupBox1.Controls.Add(this.testWriteBtn);
            this.groupBox1.Controls.Add(this.clearLogBtn);
            this.groupBox1.Location = new System.Drawing.Point(6, 241);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(545, 47);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log control";
            // 
            // refreshBtn
            // 
            this.refreshBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshBtn.Location = new System.Drawing.Point(296, 16);
            this.refreshBtn.Name = "refreshBtn";
            this.refreshBtn.Size = new System.Drawing.Size(77, 25);
            this.refreshBtn.TabIndex = 3;
            this.refreshBtn.Text = "Refresh";
            this.refreshBtn.UseVisualStyleBackColor = true;
            this.refreshBtn.Click += new System.EventHandler(this.refreshBtn_Click);
            // 
            // logAutoUpdate
            // 
            this.logAutoUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.logAutoUpdate.AutoSize = true;
            this.logAutoUpdate.Location = new System.Drawing.Point(204, 20);
            this.logAutoUpdate.Name = "logAutoUpdate";
            this.logAutoUpdate.Size = new System.Drawing.Size(91, 19);
            this.logAutoUpdate.TabIndex = 2;
            this.logAutoUpdate.Text = "Auto refresh";
            this.logAutoUpdate.UseVisualStyleBackColor = true;
            this.logAutoUpdate.CheckedChanged += new System.EventHandler(this.logAutoUpdate_CheckedChanged);
            // 
            // testWriteBtn
            // 
            this.testWriteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testWriteBtn.Location = new System.Drawing.Point(379, 16);
            this.testWriteBtn.Name = "testWriteBtn";
            this.testWriteBtn.Size = new System.Drawing.Size(77, 25);
            this.testWriteBtn.TabIndex = 1;
            this.testWriteBtn.Text = "Test write";
            this.testWriteBtn.UseVisualStyleBackColor = true;
            this.testWriteBtn.Click += new System.EventHandler(this.testWriteBtn_Click);
            // 
            // clearLogBtn
            // 
            this.clearLogBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearLogBtn.Location = new System.Drawing.Point(462, 16);
            this.clearLogBtn.Name = "clearLogBtn";
            this.clearLogBtn.Size = new System.Drawing.Size(77, 25);
            this.clearLogBtn.TabIndex = 0;
            this.clearLogBtn.Text = "Clear log";
            this.clearLogBtn.UseVisualStyleBackColor = true;
            this.clearLogBtn.Click += new System.EventHandler(this.clearLogBtn_Click);
            // 
            // logTextBox
            // 
            this.logTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logTextBox.AutoWordSelection = true;
            this.logTextBox.BackColor = System.Drawing.Color.White;
            this.logTextBox.HideSelection = false;
            this.logTextBox.Location = new System.Drawing.Point(6, 6);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.logTextBox.ShowSelectionMargin = true;
            this.logTextBox.Size = new System.Drawing.Size(545, 232);
            this.logTextBox.TabIndex = 0;
            this.logTextBox.Text = "";
            this.logTextBox.WordWrap = false;
            // 
            // aboutPage
            // 
            this.aboutPage.Controls.Add(this.label5);
            this.aboutPage.Controls.Add(this.versionLbl);
            this.aboutPage.Controls.Add(this.linkLabel1);
            this.aboutPage.Controls.Add(this.panel1);
            this.aboutPage.Location = new System.Drawing.Point(4, 24);
            this.aboutPage.Name = "aboutPage";
            this.aboutPage.Padding = new System.Windows.Forms.Padding(3);
            this.aboutPage.Size = new System.Drawing.Size(557, 294);
            this.aboutPage.TabIndex = 2;
            this.aboutPage.Text = "About";
            this.aboutPage.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(150, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(233, 30);
            this.label5.TabIndex = 8;
            this.label5.Text = "Originally created for \"Product Design and \r\nImplementation\" -UaS course.";
            // 
            // versionLbl
            // 
            this.versionLbl.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.versionLbl.AutoSize = true;
            this.versionLbl.Location = new System.Drawing.Point(150, 160);
            this.versionLbl.Name = "versionLbl";
            this.versionLbl.Size = new System.Drawing.Size(84, 15);
            this.versionLbl.TabIndex = 7;
            this.versionLbl.Text = "Version: 1.0.0.0";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(150, 188);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(192, 15);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://github.com/0xJX/SharpRAT";
            this.linkLabel1.Click += new System.EventHandler(this.linkLabel1_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(137, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(254, 95);
            this.panel1.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::Server.Properties.Resources.SharpRAT_Icon;
            this.pictureBox1.Location = new System.Drawing.Point(2, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(62, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(13, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(220, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "C# Remote Administrator Tool";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(61, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 50);
            this.label2.TabIndex = 3;
            this.label2.Text = "SharpRAT";
            // 
            // imgBox
            // 
            this.imgBox.Image = global::Server.Properties.Resources.SharpRAT_Icon;
            this.imgBox.Location = new System.Drawing.Point(12, 6);
            this.imgBox.Name = "imgBox";
            this.imgBox.Size = new System.Drawing.Size(57, 60);
            this.imgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgBox.TabIndex = 1;
            this.imgBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(70, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 50);
            this.label1.TabIndex = 2;
            this.label1.Text = "Settings";
            // 
            // updateLogTimer
            // 
            this.updateLogTimer.Interval = 500;
            this.updateLogTimer.Tick += new System.EventHandler(this.updateLogTimer_Tick);
            // 
            // formEffectTimer
            // 
            this.formEffectTimer.Enabled = true;
            this.formEffectTimer.Interval = 1;
            this.formEffectTimer.Tick += new System.EventHandler(this.formEffectTimer_Tick);
            // 
            // serverCheckbox
            // 
            this.serverCheckbox.AutoSize = true;
            this.serverCheckbox.Checked = true;
            this.serverCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.serverCheckbox.Location = new System.Drawing.Point(166, 48);
            this.serverCheckbox.Name = "serverCheckbox";
            this.serverCheckbox.Size = new System.Drawing.Size(81, 19);
            this.serverCheckbox.TabIndex = 2;
            this.serverCheckbox.Text = "Run server";
            this.serverCheckbox.UseVisualStyleBackColor = true;
            this.serverCheckbox.CheckedChanged += new System.EventHandler(this.serverCheckbox_CheckedChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(589, 406);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imgBox);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SharpRAT - Settings";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Settings_FormClosed);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.tabControl.ResumeLayout(false);
            this.mainPage.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portNumericUD)).EndInit();
            this.logPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.aboutPage.ResumeLayout(false);
            this.aboutPage.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TabControl tabControl;
        private TabPage mainPage;
        private TabPage logPage;
        private TabPage aboutPage;
        private PictureBox imgBox;
        private Label label1;
        private Label label3;
        private Label label2;
        private PictureBox pictureBox1;
        private Panel panel1;
        private LinkLabel linkLabel1;
        private Label label5;
        private Label versionLbl;
        private RichTextBox logTextBox;
        private GroupBox groupBox1;
        private Button clearLogBtn;
        private System.Windows.Forms.Timer updateLogTimer;
        private Button testWriteBtn;
        private Button refreshBtn;
        private CheckBox logAutoUpdate;
        private GroupBox groupBox2;
        private Label label4;
        private NumericUpDown portNumericUD;
        private System.Windows.Forms.Timer formEffectTimer;
        private CheckBox serverCheckbox;
    }
}