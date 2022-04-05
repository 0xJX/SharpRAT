using System.Net.Sockets;
using System.Text;

namespace Server.Server
{
    internal class ClientFunctions
    {
        // This needs some work.
        public static string GetRegistryKeyValue(SocketServer socketServer, int iSelectedIndex, string szPath, string szKeyname)
        {
            // Send request.
            string getRegKeyCMD = "<GET-REGKEY>" + szPath + "<SPLIT>" + szKeyname;
            socketServer.Send(socketServer.GetClient(iSelectedIndex).socket, getRegKeyCMD);
            Thread.Sleep(500);
            try
            {
                byte[] buffer = new byte[1024];
                socketServer.GetClient(iSelectedIndex).socket.Receive(buffer);

                if (buffer.Length > 0)
                {
                    string decoded = Encoding.ASCII.GetString(buffer);
                    if (decoded.StartsWith("<GET-REGKEY>"))
                    {
                        decoded = decoded.Replace("<GET-REGKEY>", "");
                        return decoded;
                    }
                }
            }
            catch (SocketException)
            {
                return "0"; // Connection failed.
            }
            return "0";
        }
    }
}
