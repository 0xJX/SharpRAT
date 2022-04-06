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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.versionLbl = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.imgBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPage2.SuspendLayout();
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
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(12, 72);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(565, 322);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(557, 294);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.versionLbl);
            this.tabPage2.Controls.Add(this.linkLabel1);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(557, 294);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "About";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            this.Load += new System.EventHandler(this.Settings_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private PictureBox imgBox;
        private Label label1;
        private Label label3;
        private Label label2;
        private PictureBox pictureBox1;
        private Panel panel1;
        private LinkLabel linkLabel1;
        private Label label5;
        private Label versionLbl;
    }
}