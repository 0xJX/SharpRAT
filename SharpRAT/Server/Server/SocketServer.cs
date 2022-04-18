using System.Text;
using System.Net;
using System.Net.Sockets;
using Server.UI;

namespace Server.Server
{
    public partial class SocketServer
    {
        private static int iSocketBuffer = 1048576;
        private Thread serverThread, checkClientsThread;
        private readonly static List<Client> clients = new();
        const int
            iMaxConnectedUsers = 100;

        public class Client
        {
            public Socket socket;
            public byte[] buffer = new byte[iSocketBuffer];
            // Received data string.
            public StringBuilder dataStringBuilder = new();
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
            while (User.Config.bRunServer)
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
                            if (ReadClient(GetClient(i).socket))
                                continue;
                        }

                        // Invalid connection, remove from the list
                        clients.Remove(GetClient(i));
                        Main.uiRequests.Request(i.ToString(), RequestUI.RequestType.UI_REMOVE_USER);
                    }
                }
                catch (SocketException e)
                {
                    // Normal stuff.
                }
                catch (Exception e)
                {
                    Log.Error("Server: " + e.Message);
                }
                Thread.Sleep(100);
            }
        }

        bool IsBufferEmpty(byte[] buf, int size)
        {
            for (int i = 0; i < size; i++)
                if (buf[i] != 0) 
                    return false;

            return true;
        }

        public bool ReadClient(Socket clientSocket)
        {
            byte[] buffer = new byte[iSocketBuffer];
            try
            {
                clientSocket.Receive(buffer);
            }
            catch (SocketException)
            {
                return false; // Connection failed.
            }

            if (buffer.Length > 0)
            {
                if (IsBufferEmpty(buffer, iSocketBuffer))
                    return false;

                string tempString = Encoding.ASCII.GetString(buffer);
                tempString = tempString.Split("<EOF>")[0];

                ParseData(tempString);

                return true; // Ping succeeded.
            }

            return true;
        }

        public static void ParseData(string data)
        {
            if (data.StartsWith("<FILE>"))
            {
                data = data.Replace("<FILE>", "");
                FileManager.ReceiveData(data);
            }

            if (data.StartsWith("<PRINTSCREEN>"))
            {
                ScreenViewer.StartDataReceiveThread();
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
                    // Parse first initial received data
                    string tempString = data.Replace("<EOF>", "");

                    if (tempString.StartsWith("<CMD=NAME>"))
                    {
                        tempString = tempString.Replace("<CMD=NAME>", "");

                        // Add received name, ip:port to our UI.
                        tempString += "<SPLIT>" + ((IPEndPoint)clientSocket.RemoteEndPoint).Address.ToString()
                        + "<SPLIT>" + ((IPEndPoint)clientSocket.RemoteEndPoint).Port.ToString();

                        Main.uiRequests.Request(tempString, RequestUI.RequestType.UI_ADD_USER);
                    }
                }
                else
                {
                    // Not all data received. Get more.
                    clientSocket.BeginReceive(client.buffer, 0, iSocketBuffer, 0, new AsyncCallback(ReadCallback), client);
                }
            }
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            clients.Add(new Client());
            clients.Last().socket = listener.EndAccept(ar);
            clients.Last().socket.BeginReceive(clients.Last().buffer, 0, iSocketBuffer, 0, new AsyncCallback(ReadCallback), clients.Last());
            Main.uiRequests.Request("Connection accepted.", RequestUI.RequestType.UI_UPDATE_STATUS);
        }

        public IAsyncResult Send(Socket handler, string data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data + "<EOF>");
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
                Log.Error("SendCallback: " + e.Message);
            }
        }

        public void ExecuteServer()
        {
            Log.Info("Started SocketServer.");

            while (User.Config.bRunServer)
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

                    while (User.Config.bRunServer)
                    {
                        listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                        Thread.Sleep(100); // Sleep, so the thread doesn't hog all the memory.
                    }
                }
                catch (Exception e)
                {
                    Log.Error("Execute Server: " + e.Message);
                }
                Thread.Sleep(100);
            }
        }

        public void StartServer()
        {
            // Start the server on it's own thread, so it does not hog all the UI resources.
            serverThread = new Thread(ExecuteServer);
            if (serverThread.ThreadState != ThreadState.Running)
            {
                serverThread.IsBackground = true;
                serverThread.Start();
                Main.uiRequests.Request("Server started", RequestUI.RequestType.UI_UPDATE_STATUS);
            }

            // Ping clients on background thread.
            checkClientsThread = new Thread(CheckClientsThread);
            if (checkClientsThread.ThreadState != ThreadState.Running)
            {
                checkClientsThread.IsBackground = true;
                checkClientsThread.Start();
            }
        }

        public void SuspendServer()
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
                        GetClient(i).socket.Disconnect(false);
                        clients.Remove(GetClient(i));
                        Main.uiRequests.Request(i.ToString(), RequestUI.RequestType.UI_REMOVE_USER);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }
    }
}
