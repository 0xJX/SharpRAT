namespace Server
{
    public partial class Main : Form
    {
        private readonly ImageList imageList;
        private readonly Server.SocketServer socketServer;
        #pragma warning disable CS8618
            public static RequestUI uiRequests;
        #pragma warning restore CS8618

        public Main()
        {
            socketServer = new Server.SocketServer();
            uiRequests = new RequestUI();
            imageList = new ImageList();
            InitializeComponent();
        }

        public void AddUserToViewList(string text)
        {
            Bitmap image;
            image = Properties.Resources.user;
            imageList.Images.Add(image);
            userView.SmallImageList = imageList;
            userView.View = View.Details;
            userView.Items.Add(new ListViewItem { ImageIndex = imageList.Images.Count - 1, Text = text });
        }

        public void UpdateStatus(string statusText)
        {
            statusLbl.Text = "Status: " + statusText;
        }

        private void Main_Load(object sender, EventArgs e)
        {
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
                        AddUserToViewList(uiRequests.GetRequestText(requestID));
                        break;
                    case RequestUI.RequestType.UI_UPDATE_STATUS:
                        UpdateStatus(uiRequests.GetRequestText(requestID));
                        break;
                }
            }
        }

        private void sendMessageBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // todo.
        }
    }
}