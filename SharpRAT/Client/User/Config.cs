
namespace Client.User
{
    public static class Config
    {
        public static int
            iPortNumber = 11111,
            iSocketBuffer = 1048576;
        public static string szIPAddress = "0";
        public static bool bRunServer = true;

        public static string GetConfigPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SharpRAT\\Client.cfg";
        }

        private static int Parse(string line, string varName, int iValue)
        {
            string varStructure = "-" + varName + ":";
            if (!line.StartsWith(varStructure))
                return iValue;

            return int.Parse(line.Split(varStructure)[1]);
        }

        private static string Parse(string line, string varName, string szValue)
        {
            string varStructure = "-" + varName + ":";
            if (!line.StartsWith(varStructure))
                return szValue;

            return line.Split(varStructure)[1];
        }

        private static bool Parse(string line, string varName, bool bValue)
        {
            string varStructure = "-" + varName + ":";
            if (!line.StartsWith(varStructure))
                return bValue;

            return line.Split(varStructure)[1].StartsWith("True");
        }

        public static void Read()
        {
            try
            {
                string line;
                using (StreamReader sr = new(GetConfigPath(), false))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        iPortNumber = Parse(line, "iPortNumber", iPortNumber);
                        szIPAddress = Parse(line, "szIPAddress", szIPAddress);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Config read failed: " + ex.Message);
            }
        }
    }
}
