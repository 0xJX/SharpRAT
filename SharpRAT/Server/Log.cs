
namespace Server.Server
{
    internal class Log
    {
        const string szDefaultLogName = "log";

        public static string GetFilePath(string logName = szDefaultLogName)
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $"\\SharpRAT\\{logName}.log";
        }

        public static void DeleteFile(string logName = szDefaultLogName)
        {
            string filePath = GetFilePath(logName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public static string ReadAll(string logName = szDefaultLogName)
        {
            string tempString = "Empty file.";

            if (File.Exists(GetFilePath(logName)))
            {
                // Read file to string.
                tempString = File.ReadAllText(GetFilePath(logName));
            }

            return tempString;
        }

        public static bool WriteRaw(string strMessage, string logName)
        {
            try
            {
                string formatedMessage = $"{DateTime.Now}: {strMessage}";

                Console.WriteLine(formatedMessage);
                using (StreamWriter sw = new StreamWriter(GetFilePath(logName), true))
                {
                    sw.WriteLine(formatedMessage);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static void Info(string text, string logName = szDefaultLogName)
        {
            WriteRaw($"[INFO] {text}", logName);
        }

        public static void Error(string text, string logName = szDefaultLogName)
        {
            WriteRaw($"[ERROR] {text}", logName);
        }

        public static void Action(string text, string logName = szDefaultLogName)
        {
            WriteRaw($"[ACTION] {text}", logName);
        }
    }
}
