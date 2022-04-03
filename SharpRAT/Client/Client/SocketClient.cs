using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.Client
{
    internal class SocketClient
    {
        string szTempName = "Client1";
        bool nameSent = false;
        public void ExecuteClient()
        {
            while (true)
            {
                try
                {
                    // Connect to server, currently localhost and port 11111.
                    IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                    IPAddress ipAddress = ipHost.AddressList[0];
                    IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11111);

                    // TCP/IP Socket.
                    Socket sender = new(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                    try
                    {
                        // Connect to server.
                        sender.Connect(localEndPoint);

                        // Send name message to server
                        if (!nameSent)
                        {
                            sender.Send(Encoding.ASCII.GetBytes(szTempName + "<EOF>"));
                            nameSent = true;
                        }

                        // Data buffer
                        byte[] messageReceived = new byte[1024];

                        int byteRecv = sender.Receive(messageReceived);
                        Console.WriteLine("Message from Server -> {0}", Encoding.ASCII.GetString(messageReceived, 0, byteRecv));

                        sender.Shutdown(SocketShutdown.Both);
                        sender.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}
