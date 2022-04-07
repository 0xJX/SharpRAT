using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.User
{
    public static class Config
    {
        public static int iPortNumber = 11111;
        public static string GetConfigPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SharpRAT\\Server.cfg";
        }

        private static int Parse(string line, string varName, int iValue)
        {
            string varStructure = "-" + varName + ":";
            if (!line.StartsWith(varStructure))
                return iValue;

            return int.Parse(line.Split(varStructure)[1]);
        }

        private static bool Parse(string line, string varName, bool bValue)
        {
            string varStructure = "-" + varName + ":";
            if (!line.StartsWith(varStructure))
                return bValue;

            return Convert.ToBoolean(int.Parse(line.Split(varStructure)[1]));
        }

        private static void WriteVariable(StreamWriter sw, string varName, string varWrite)
        {
            sw.Write("-" + varName + ":" + varWrite);
        }

        public static void Read()
        {
            try
            {
                string line;
                using (StreamReader sr = new StreamReader(GetConfigPath(), false))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        iPortNumber = Parse(line, "iPortNumber", iPortNumber);
                    }
                }
            }
            catch(Exception ex)
            {
                Server.Log.Error("Config read failed: " + ex.Message);
            }
        }

        public static void Write()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(GetConfigPath(), false))
                {
                    WriteVariable(sw, "iPortNumber", iPortNumber.ToString());
                }
            }
            catch (Exception ex)
            {
                Server.Log.Error("Config write failed: " + ex.Message);
            }
        }
    }
}
