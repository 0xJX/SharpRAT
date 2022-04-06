using Server.Server;

namespace Server
{
    public partial class MessageboxCreator : Form
    {
        int iSelectedIndex = -1;
        private SocketServer socketServer;
        private MessageBoxIcon selectedIcon = MessageBoxIcon.Information;

        public MessageboxCreator(SocketServer socketServer, int iClientIndex)
        {
            InitializeComponent();
            iSelectedIndex = iClientIndex;
            this.socketServer = socketServer;
        }

        private void MessageboxCreator_Load(object sender, EventArgs e)
        {
            msgTypeBox.SelectedIndex = 1;
        }

        private void sendMessageBoxBtn_Click(object sender, EventArgs e)
        {
            string messageBoxStr
                = "<CMD=MSGBOX>" + textBox.Text + "<SPLIT>" + titleBox.Text + "<SPLIT>" +
                 msgTypeBox.SelectedIndex.ToString() + "<EOF>";
            socketServer.Send(SocketServer.GetClient(iSelectedIndex).socket, messageBoxStr);
        }

        private void testBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show(textBox.Text, titleBox.Text, MessageBoxButtons.OK, selectedIcon);
        }

        private void msgTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(msgTypeBox.SelectedIndex)
            {
                case 0:
                    selectedIcon = MessageBoxIcon.Exclamation;
                    msgIconBox.Image = WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Warning_ICO);
                    break;
                case 1:
                    selectedIcon = MessageBoxIcon.Information;
                    msgIconBox.Image = WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Info_ICO);
                    break;
                case 2:
                    selectedIcon = MessageBoxIcon.Error;
                    msgIconBox.Image = WinIcons.GetImageFromIcon("comres.dll", (int)WinIcons.ComresID.Error_ICO);
                    break;
                case 3:
                    selectedIcon = MessageBoxIcon.Question;
                    msgIconBox.Image = WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Question_ICO);
                    break;
            }
        }
    }
}
