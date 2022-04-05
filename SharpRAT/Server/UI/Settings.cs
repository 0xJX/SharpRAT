using System.Diagnostics;

namespace Server.UI
{
    public partial class Settings : Form
    {
        private readonly ImageList tabImages;
        public Settings()
        {
            tabImages = new ImageList();
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            tabImages.Images.Add(WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Config_ICO, false));
            tabImages.Images.Add(WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Info_ICO, false));
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
    }
}
