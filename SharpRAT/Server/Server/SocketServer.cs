using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server.Server
{
    public partial class SocketServer
    {
        private static List<Client> clients = new List<Client>();
        const int
            iMaxConnectedUsers = 100,
            iPingWaitTime = 1000;

        public class Client
        {
            public Socket socket;
            public byte[] buffer = new byte[1024];
            // Received data string.
            public StringBuilder dataStringBuilder = new StringBuilder();
        }

        public static Client GetClient(int index)
        {
            try
            {
                return clients[index];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void CheckClientsThread()
        {
            while (true)
            {
                try
                {
                    for (int i = 0; i < clients.Count; i++)
                    {
                        // No clients.
                        if (clients.Count == 0)
                            break;

                        // Socket was valid, continue on the list.
                        if (GetClient(i).socket.Connected)
                        {
                            // Ping the client just to be sure.
                            if (Ping(GetClient(i).socket))
                                continue;
                        }

                        // Invalid connection, remove from the list
                        clients.Remove(GetClient(i));
                        Main.uiRequests.Request(i.ToString(), RequestUI.RequestType.UI_REMOVE_USER);
                    }
                }catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Thread.Sleep(100);
            }
        }

        public bool Ping(Socket clientSocket)
        {
            byte[] buffer = new byte[1024];
            try
            {
                Send(clientSocket, "<PING>");
                Thread.Sleep(iPingWaitTime);
                clientSocket.Receive(buffer);
            }
            catch(SocketException)
            {
                return false; // Connection failed.
            }

            if(buffer.Length > 0)
            {
                string decoded = Encoding.ASCII.GetString(buffer);
                if (decoded.StartsWith("<PING>"))
                    return true; // Ping succeeded.
            }

            return false;
        }

        public static void ParseData(Socket clientSocket, string data)
        {
            string tempString = data.Replace("<EOF>", "");

            if(tempString.StartsWith("<CMD=NAME>"))
            {
                tempString = tempString.Replace("<CMD=NAME>", "");

                // Add received name, ip:port to our UI.
                tempString += "<SPLIT>" + ((IPEndPoint)clientSocket.RemoteEndPoint).Address.ToString() 
                + "<SPLIT>" + ((IPEndPoint)clientSocket.RemoteEndPoint).Port.ToString();

                Main.uiRequests.Request(tempString, RequestUI.RequestType.UI_ADD_USER);
            }
        }

        public void ReadCallback(IAsyncResult ar)
        {
            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            Client client = (Client)ar.AsyncState;
            Socket clientSocket = client.socket;

            // Read data from the client socket. 
            int bytesRead = clientSocket.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.
                client.dataStringBuilder.Append(Encoding.ASCII.GetString(client.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read more data.
                string data = client.dataStringBuilder.ToString();
                if (data.IndexOf("<EOF>") > -1)
                {
                    // Parse received data
                    ParseData(clientSocket, data);
                    // Echo the data back to the client.
                    Send(clientSocket, data);
                }
                else
                {
                    // Not all data received. Get more.
                    clientSocket.BeginReceive(client.buffer, 0, 1024, 0, new AsyncCallback(ReadCallback), client);
                }
            }
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            clients.Add(new Client());
            clients.Last().socket = handler;
            handler.BeginReceive(clients.Last().buffer, 0, 1024, 0, new AsyncCallback(ReadCallback), clients.Last());
            Main.uiRequests.Request("Connection accepted.", RequestUI.RequestType.UI_UPDATE_STATUS);
        }

        public IAsyncResult Send(Socket handler, string data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            // Begin sending the data to the remote device.
            return handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                handler.EndSend(ar);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        public void ExecuteServer()
        {
            Log.Info("Started SocketServer.");
            // Ping clients on background thread.
            Thread checkClientsThread = new Thread(CheckClientsThread);
            checkClientsThread.IsBackground = true;
            checkClientsThread.Start();

            while (true)
            {
                // Start the server at localhost, port 11111
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, User.Config.iPortNumber);

                // Creation TCP/IP Socket using
                // Socket Class Constructor
                Socket listener = new(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    listener.Bind(localEndPoint);
                    listener.Listen(iMaxConnectedUsers);
                    Main.uiRequests.Request("Waiting for connections...", RequestUI.RequestType.UI_UPDATE_STATUS);

                    while (true)
                    {
                        listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                        Thread.Sleep(100); // Sleep, so the thread doesn't hog all the memory.
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }
                Thread.Sleep(100);
            }
        }
    }
}
