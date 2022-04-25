using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Win32;

namespace Client.Client
{
    public class SocketClient
    {
        private static int iSocketBuffer = 1048576;
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
                    // Connect to server, currently localhost and port 11111.
                    IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                    IPAddress ipAddress = ipHost.AddressList[0];
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
                            byte[] messageReceived = new byte[iSocketBuffer];
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
