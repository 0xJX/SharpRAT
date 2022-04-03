using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server.Server
{
    internal class SocketServer
    {
        public void ExecuteServer()
        {
            while (true)
            {
                // Start the server at localhost, port 11111
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11111);

                // Creation TCP/IP Socket using
                // Socket Class Constructor
                Socket listener = new(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    listener.Bind(localEndPoint);
                    listener.Listen(10);
                    Main.uiRequests.Request("Waiting for connections...", RequestUI.RequestType.UI_UPDATE_STATUS);
                    while (true)
                    {

                        Socket clientSocket = listener.Accept();
                        Main.uiRequests.Request("Connection accepted.", RequestUI.RequestType.UI_UPDATE_STATUS);

                        // Data buffer
                        byte[] bytes = new Byte[1024];
                        string data = "";

                        while (true)
                        {
                            int numByte = clientSocket.Receive(bytes);
                            data += Encoding.ASCII.GetString(bytes, 0, numByte);
                            if (data.IndexOf("<EOF>") > -1) // EOF found, stop receiving.
                                break;
                        }

                        // Add received name to our UI.
                        Main.uiRequests.Request(data, RequestUI.RequestType.UI_ADD_USER);

                        // Send test msg back to client.
                        clientSocket.Send(Encoding.ASCII.GetBytes("Hello client"));
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Close();
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
