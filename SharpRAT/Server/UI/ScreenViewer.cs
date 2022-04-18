using Server.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server.UI
{
    public partial class ScreenViewer : Form
    {
        private static Socket client;
        private static byte[] imageStream;
        private static Image printScreenImage;
        private static Thread dataReceiveThread;

        public ScreenViewer(int clientIndex)
        {
            InitializeComponent();
            client = SocketServer.GetClient(clientIndex).socket;
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
                    client.Receive(buffer);
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
            Main.socketServer.Send(client, "<PRINTSCREEN>");
        }

        private void uiUpdateTimer_Tick(object sender, EventArgs e)
        {
            int requestID = Main.uiRequests.RequestReceived();
            if (requestID != 0) // Another thread requested UI to update requsted item.
            {
                switch (Main.uiRequests.GetRequestType(requestID)) // Check request type and call the correct function.
                {
                    case RequestUI.RequestType.UI_UPDATE_SCREENSHOT:
                        pictureBox1.Image = printScreenImage;
                        imageStream = Array.Empty<byte>();
                        break;
                }
            }
        }

        private void ScreenViewer_Load(object sender, EventArgs e)
        {
            Icon = WinIcons.Extract("shell32.dll", (int)WinIcons.ShellID.Desktop, false);
        }
    }
}
