namespace Server
{
    partial class MessageboxCreator
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
            this.sendMessageBoxBtn = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.msgTypeBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.msgIconBox = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.titleBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.testBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.msgIconBox)).BeginInit();
            this.SuspendLayout();
            // 
            // sendMessageBoxBtn
            // 
            this.sendMessageBoxBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendMessageBoxBtn.Location = new System.Drawing.Point(388, 290);
            this.sendMessageBoxBtn.Name = "sendMessageBoxBtn";
            this.sendMessageBoxBtn.Size = new System.Drawing.Size(103, 32);
            this.sendMessageBoxBtn.TabIndex = 0;
            this.sendMessageBoxBtn.Text = "Send";
            this.sendMessageBoxBtn.UseVisualStyleBackColor = true;
            this.sendMessageBoxBtn.Click += new System.EventHandler(this.sendMessageBoxBtn_Click);
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Location = new System.Drawing.Point(6, 139);
            this.textBox.MaxLength = 800;
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox.Size = new System.Drawing.Size(467, 128);
            this.textBox.TabIndex = 1;
            // 
            // msgTypeBox
            // 
            this.msgTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.msgTypeBox.FormattingEnabled = true;
            this.msgTypeBox.Items.AddRange(new object[] {
            "Exclamation",
            "Info",
            "Error",
            "Question"});
            this.msgTypeBox.Location = new System.Drawing.Point(53, 42);
            this.msgTypeBox.Name = "msgTypeBox";
            this.msgTypeBox.Size = new System.Drawing.Size(207, 21);
            this.msgTypeBox.TabIndex = 2;
            this.msgTypeBox.SelectedIndexChanged += new System.EventHandler(this.msgTypeBox_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.msgIconBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.titleBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox);
            this.groupBox1.Controls.Add(this.msgTypeBox);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 273);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MessageBox";
            // 
            // msgIconBox
            // 
            this.msgIconBox.BackColor = System.Drawing.Color.Transparent;
            this.msgIconBox.Location = new System.Drawing.Point(8, 24);
            this.msgIconBox.Name = "msgIconBox";
            this.msgIconBox.Size = new System.Drawing.Size(42, 42);
            this.msgIconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.msgIconBox.TabIndex = 7;
            this.msgIconBox.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Title:";
            // 
            // titleBox
            // 
            this.titleBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleBox.Location = new System.Drawing.Point(6, 94);
            this.titleBox.MaxLength = 100;
            this.titleBox.Name = "titleBox";
            this.titleBox.Size = new System.Drawing.Size(467, 21);
            this.titleBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Text:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Message Icon:";
            // 
            // testBtn
            // 
            this.testBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.testBtn.Location = new System.Drawing.Point(279, 290);
            this.testBtn.Name = "testBtn";
            this.testBtn.Size = new System.Drawing.Size(103, 32);
            this.testBtn.TabIndex = 4;
            this.testBtn.Text = "Test";
            this.testBtn.UseVisualStyleBackColor = true;
            this.testBtn.Click += new System.EventHandler(this.testBtn_Click);
            // 
            // MessageboxCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 326);
            this.Controls.Add(this.testBtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.sendMessageBoxBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "MessageboxCreator";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Send MessageBox";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MessageboxCreator_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.msgIconBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button sendMessageBoxBtn;
        private TextBox textBox;
        private ComboBox msgTypeBox;
        private GroupBox groupBox1;
        private PictureBox msgIconBox;
        private Label label3;
        private TextBox titleBox;
        private Label label2;
        private Label label1;
        private Button testBtn;
    }
}