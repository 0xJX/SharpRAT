using System.Net.Sockets;
using System.Text;

namespace Server.Server
{
    public class Client
    {
        public Socket socket;
        public byte[] buffer = new byte[User.Config.iSocketBuffer];
        // Received data string.
        public StringBuilder dataStringBuilder = new();

        private string szUsername = "Unknown";
        public bool bTaskmgrDisabled = false;
        public int iScreenCount = 0;

        public string GetUsername()
        {
            return szUsername;
        }

        public void SetUsername(string username)
        {
            szUsername = username;
        }
    }
}
