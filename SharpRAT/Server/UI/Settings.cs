using System.Diagnostics;

namespace Server.UI
{
    public partial class Settings : Form
    {
        enum FormEffect
        {
            LAUNCH,
            CLOSING
        }

        private FormEffect formEffect = FormEffect.LAUNCH;
        private readonly ImageList tabImages;
        public Settings()
        {
            tabImages = new ImageList();
            InitializeComponent();
        }

        private void SetWordColor(string selectedWord, Color color, int startIndex = 0)
        {
            if (logTextBox.Text.Contains(selectedWord))
            {
                int 
                    index = -1,
                    iSelectStart = logTextBox.SelectionStart;

                while ((index = logTextBox.Text.IndexOf(selectedWord, (index + 1))) != -1)
                {
                    logTextBox.Select((index + startIndex), selectedWord.Length);
                    logTextBox.SelectionColor = color;
                    logTextBox.Select(iSelectStart, 0);
                }
            }
        }

        private void RefreshLogBox()
        {
            logTextBox.Text = Server.Log.ReadAll();
            logTextBox.SelectionStart = logTextBox.Text.Length;

            SetWordColor("[INFO]", Color.SteelBlue);
            SetWordColor("[ERROR]", Color.Red);
            SetWordColor("[ACTION]", Color.Orange);
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            Width = 0;
            Height = 0;
            portNumericUD.Value = User.Config.iPortNumber;
            serverCheckbox.Checked = User.Config.bRunServer;
            tabImages.Images.Add(WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Config, false));
            tabImages.Images.Add(WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.FileSearch, false));
            tabImages.Images.Add(WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Info, false));
            tabImages.ColorDepth = ColorDepth.Depth32Bit; // Improves quality of the image.
            tabControl.ImageList = tabImages;

            for(int i = 0; i < tabImages.Images.Count; i++)
                tabControl.TabPages[i].ImageIndex = i;

#if DEBUG
            string configuration = "Debug";
#else
            string configuration = "Release";
#endif
            versionLbl.Text = "Version: " + Application.ProductVersion + " - " + configuration + " build";
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/0xJX/SharpRAT") { UseShellExecute = true });
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl.SelectedIndex == 1 /* Log Page */)
            {
                RefreshLogBox();
            }
        }

        private void clearLogBtn_Click(object sender, EventArgs e)
        {
            if(Server.Log.DeleteFile())
                RefreshLogBox();
        }

        private void updateLogTimer_Tick(object sender, EventArgs e)
        {
            RefreshLogBox();
        }

        private void testWriteBtn_Click(object sender, EventArgs e)
        {
            Server.Log.Info("Test info.");
            Server.Log.Action("Test action.");
            Server.Log.Error("Test error.");
            RefreshLogBox();
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            RefreshLogBox();
        }

        private void logAutoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            updateLogTimer.Enabled = logAutoUpdate.Checked;
        }

        private void portNumericUD_ValueChanged(object sender, EventArgs e)
        {
            User.Config.iPortNumber = (int)portNumericUD.Value;
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            User.Config.Write();
        }

        private void formEffectTimer_Tick(object sender, EventArgs e)
        {
            switch(formEffect)
            {
                case FormEffect.LAUNCH:
                    if (Width < 605)
                        Width += 25;

                    if (Height < 445)
                        Height += 25;

                    if (Height >= 445 && Width >= 605)
                        formEffectTimer.Stop();
                    break;
                case FormEffect.CLOSING:
                    if (Width != 0)
                        Width -= 25;

                    if(Height != 0)
                        Height -= 25;

                    if (Height < 45 && Width < 45)
                        Close();
                    break;
            }
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((formEffect == FormEffect.LAUNCH) && WindowState != FormWindowState.Maximized)
                e.Cancel = true;

            formEffect = FormEffect.CLOSING;
            formEffectTimer.Start();
        }

        private void serverCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            User.Config.bRunServer = serverCheckbox.Checked;
            Thread.Sleep(100);
            if (User.Config.bRunServer)
                Main.socketServer.StartServer();
            else
                Main.socketServer.SuspendServer();
        }
    }
}
