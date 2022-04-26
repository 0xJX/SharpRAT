using Server.Server;

namespace Server
{
    public partial class MessageboxCreator : Form
    {
        private static Client client;
        private MessageBoxIcon selectedIcon = MessageBoxIcon.Information;

        public MessageboxCreator(Client selectedClient)
        {
            InitializeComponent();
            client = selectedClient;
            Text += " - " + client.GetUsername();
        }

        private void MessageboxCreator_Load(object sender, EventArgs e)
        {
            msgTypeBox.SelectedIndex = 1;
        }

        private void sendMessageBoxBtn_Click(object sender, EventArgs e)
        {
            string messageBoxStr
                = "<SEND-MSGBOX>" + textBox.Text + "<SPLIT>" + titleBox.Text + "<SPLIT>" +
                 msgTypeBox.SelectedIndex.ToString() + "<EOF>";
            SocketServer.Send(client.socket, messageBoxStr);
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
                    msgIconBox.Image = WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Warning);
                    break;
                case 1:
                    selectedIcon = MessageBoxIcon.Information;
                    msgIconBox.Image = WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Info);
                    break;
                case 2:
                    selectedIcon = MessageBoxIcon.Error;
                    msgIconBox.Image = WinIcons.GetImageFromIcon("comres.dll", (int)WinIcons.ComresID.Error);
                    break;
                case 3:
                    selectedIcon = MessageBoxIcon.Question;
                    msgIconBox.Image = WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Question);
                    break;
            }
        }
    }
}
