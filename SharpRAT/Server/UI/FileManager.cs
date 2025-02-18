
using Server.Server;
using Server.User;
using System.Net.Sockets;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Server.UI
{
    public partial class FileManager : Form
    {
        private static List<string> fileStringList = new();
        private static List<ClientFile> fileObjectList = new();
        private string currentPath = "";
        private readonly ImageList imageList;
        private readonly Client client;

        public FileManager(Client selectedClient)
        {
            imageList = new ImageList();
            InitializeComponent();
            client = selectedClient;
            Text += " - " + client.GetUsername();
        }

        private Bitmap GetFileIcon(FileType type)
        {
            switch (type)
            {
                case FileType.MY_COMPUTER:
                    return WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.MyComputer);
                case FileType.DRIVE:
                    return WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Drive);
                case FileType.FILE_FOLDER:
                    return WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Folder);
                case FileType.FILE:
                    return WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.UnknownFile);
                case FileType.EXECUTABLE:
                    return WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Executable);
                case FileType.DLL:
                    return WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Config, false);
                case FileType.IMAGE:
                    return WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Image);
                case FileType.AUDIO:
                    return WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Sound);
                case FileType.VIDEO:
                    return WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Video);
                case FileType.TEXT_DOCUMENT:
                    return WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.TextFile);
                default:
                    return WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.MyComputer);
            }
        }

        private void AddFileToList(ClientFile file)
        {
            imageList.Images.Add(GetFileIcon(file.type));
            imageList.ColorDepth = ColorDepth.Depth32Bit; // Improves quality of the image.
            fileListView.SmallImageList = imageList;
            fileListView.View = View.Details;
            ListViewItem fileViewItem = new ListViewItem { ImageIndex = imageList.Images.Count - 1, Text = file.name };
            fileViewItem.SubItems.Add(file.dateModified);
            fileViewItem.SubItems.Add(file.GetFileTypeString());
            fileViewItem.SubItems.Add(file.GetFileSizeString());
            fileListView.Items.Add(fileViewItem);
            fileObjectList.Add(file);
        }

        private void LoadImagesForUI()
        {
            openFileToolStripMenuItem.Image = WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.RunFile);
            refreshDirectoryToolStripMenuItem.Image = WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Refresh);
        }

        private void FileManager_Load(object sender, EventArgs e)
        {
            currentPath = "";
            Icon = WinIcons.Extract("shell32.dll", (int)WinIcons.ShellID.FilledFolder, false);
            RequestDrives();
            LoadImagesForUI();
        }

        private void RequestOpenFile(ClientFile file)
        {
            SocketServer.Send(client.socket, "<REQUEST-OPENFILE>" + currentPath + "\\" + file.name);
        }

        private void RequestDrives()
        {
            fileListView.Items.Clear();
            fileObjectList.Clear();
            SocketServer.Send(client.socket, "<REQUEST-DRIVES>");
            pathBox.Text = currentPath;
        }

        private void RequestDirectories()
        {
            fileListView.Items.Clear();
            fileObjectList.Clear();
            SocketServer.Send(client.socket, "<REQUEST-DIRS>" + currentPath);
            pathBox.Text = currentPath;
        }

        public static void ReceiveData(string fileData)
        {
            string[] fileDatas = fileData.Split("<FILE-END>");

            if (fileDatas == null)
                return;

            for (int i = 0; i < fileDatas.Count() - 1; ++i)
                fileStringList.Add(fileDatas[i]);

            Main.uiRequests.Request(RequestUI.RequestType.UI_UPDATE_FILES);
        }

        private void fileListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (fileListView.SelectedItems.Count > 0)
            {
                string selectedFile = fileListView.SelectedItems[0].Text;

                if (currentPath.Length <= 3 && fileListView.SelectedItems[0].Text.Equals("..."))
                {
                    currentPath = "";
                    RequestDrives();
                    return;
                }
                else if (fileListView.SelectedItems[0].Index == 0 && fileListView.SelectedItems[0].Text.Equals("..."))
                {
                    string newPath = string.Empty;
                    string[] pathFolders = currentPath.Split(@"\");

                    // Build path string without the last folder.
                    for (int i = 0; i < (pathFolders.Count() - 1); ++i)
                    {
                        newPath += pathFolders[i];
                        if (i != (pathFolders.Count() - 2))
                            newPath += @"\";

                        if (!newPath.Contains(@"\"))
                        {
                            if (currentPath.Length <= 3)
                            {
                                currentPath = "";
                                RequestDrives();
                                return;
                            }
                            else
                            {
                                currentPath = pathFolders[0] + @"\";
                                RequestDirectories();
                                return;
                            }
                        }
                    }

                    currentPath = newPath;
                    RequestDirectories();
                }
                else if (Path.GetExtension(currentPath + selectedFile).Length > 0)
                {
                    // Do nothing.
                }
                else if (!string.IsNullOrEmpty(selectedFile))
                {
                    if (fileObjectList[fileListView.SelectedItems[0].Index].type != FileType.FILE_FOLDER && fileObjectList[fileListView.SelectedItems[0].Index].type != FileType.DRIVE)
                        return;

                    if (!selectedFile.StartsWith(@"\") && currentPath.Length >= 2)
                        selectedFile = @"\" + selectedFile;
                    currentPath = currentPath + selectedFile;
                    if (currentPath.Contains(@":\\"))
                        currentPath = currentPath.Replace(@":\\", @":\");
                    RequestDirectories();
                }
            }
        }

        private void AddDataToView(string fileData)
        {
            string[] splitData = fileData.Split("<SPLIT>");
            string
                name = splitData[0],
                dateModified = splitData[1];
            FileType type = (FileType)int.Parse(splitData[2]);
            long size = long.Parse(splitData[3]);

            ClientFile file = new(name, size, dateModified, type);
            AddFileToList(file);
        }

        private void uiUpdateTimer_Tick(object sender, EventArgs e)
        {
            int requestID = Main.uiRequests.RequestReceived();
            if (requestID != -1) // Another thread requested UI to update requsted item.
            {
                switch (Main.uiRequests.GetRequestType(requestID)) // Check request type and call the correct function.
                {
                    case RequestUI.RequestType.UI_UPDATE_FILES:

                        // Clear old data when receiving new data.
                        fileListView.Items.Clear();
                        fileObjectList.Clear();

                        foreach (string file in fileStringList)
                            AddDataToView(file);

                        if (fileListView.Items.Count != 0)
                        {
                            fileStringList.Clear();
                            Main.uiRequests.GetRequests().RemoveAt(requestID);
                        }
                        break;
                }
            }
        }

        private int GetSelectedFileIndex()
        {
            return fileListView.SelectedItems[0].Index;
        }


        private void fileMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (fileListView.SelectedItems.Count == 0)
            {
                openFileToolStripMenuItem.Visible = false;
                return;
            }

            ClientFile currentFile = fileObjectList[GetSelectedFileIndex()];
            if (currentFile.type == FileType.DRIVE || currentFile.type == FileType.FILE_FOLDER)
            {
                openFileToolStripMenuItem.Visible = false;
                return;
            }

            openFileToolStripMenuItem.Visible = true;
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientFile currentFile = fileObjectList[GetSelectedFileIndex()];
            if (currentFile == null)
                return;
            RequestOpenFile(fileObjectList[GetSelectedFileIndex()]);
        }

        private void fileListView_Click(object sender, EventArgs e)
        {
            ClientFile currentFile = fileObjectList[GetSelectedFileIndex()];
            if (currentFile == null)
            {
                pathBox.Text = currentPath;
                return;
            }

            string slashStr = !currentPath.EndsWith("\\") ? "\\" : "";
            pathBox.Text = $"{currentPath}{slashStr}{currentFile.name}";
        }

        private void refreshDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentPath == "")
                RequestDrives();
            else
                RequestDirectories();
        }
    }
}
