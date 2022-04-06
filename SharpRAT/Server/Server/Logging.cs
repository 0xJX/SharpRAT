using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Server
{
    internal class Logging
    {
        public static bool AddToLog(string strMessage, string userName)
        {
            try
            {
                string logPathx = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $"\\SharpRAT\\{userName}.log";
                using (StreamWriter sw = new StreamWriter(logPathx, true))
                {
                    sw.WriteLine($"{DateTime.Now}: { strMessage}");
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static void Info(string text, string userName)
        {
            var textX = $"[INFO] {text}";
            AddToLog(textX, userName);
        }
        public static void Error(string text, string userName)
        {
            var textX = $"[ERROR] {text}";
            AddToLog(textX, userName);
        }
        public static void Action(string text, string userName)
        {
            var textX = $"[ACTION] {text}";
            AddToLog(textX, userName);
        }
    }
}
