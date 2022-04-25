
using Server.Server;
using System.Net.Sockets;

namespace Server.UI
{
    public partial class FileManager : Form
    {
        private static List<string> fileStringList = new();
        private string currentPath = "";
        private readonly ImageList imageList;
        private readonly Client client;

        public FileManager(Client selectedClient)
        {
            imageList = new ImageList();
            InitializeComponent();
            client = selectedClient;
        }

        private enum FileType
        {
            MY_COMPUTER,
            DRIVE,
            FILE_FOLDER,
            FILE,
            EXECUTABLE,
            DLL,
            IMAGE,
            VIDEO,
            TEXT_DOCUMENT
        }

        private string GetFileSizeString(long bytes)
        {
            if (bytes == 0)
                return "";

            string ending = "bytes";
            long tempBytes = bytes;

            if (tempBytes >= 1000000000)
            {
                tempBytes /= 1000000000;
                ending = "gigabytes";
            }
            else if (tempBytes >= 1000000)
            {
                tempBytes /= 1000000;
                ending = "megabytes";
            }
            else if (tempBytes >= 1000)
            {
                tempBytes /= 1000;
                ending = "kilobytes";
            }

            return tempBytes.ToString() + " " + ending;
        }

        private string GetFileTypeString(FileType type)
        {
            switch (type)
            {
                case FileType.MY_COMPUTER:
                    return "My Computer";
                case FileType.DRIVE:
                    return "Drive";
                case FileType.FILE_FOLDER:
                    return "File Folder";
                case FileType.FILE:
                    return "File";
                case FileType.EXECUTABLE:
                    return "Executable";
                case FileType.DLL:
                    return "DLL";
                case FileType.IMAGE:
                    return "Image";
                case FileType.VIDEO:
                    return "Video";
                case FileType.TEXT_DOCUMENT:
                    return "Text Document";
                default:
                    return "???";
            }
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
                case FileType.VIDEO:
                    return WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.Video);
                case FileType.TEXT_DOCUMENT:
                    return WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.TextFile);
                default:
                    return WinIcons.GetImageFromIcon("shell32.dll", (int)WinIcons.ShellID.MyComputer);
            }
        }

        private void AddFileToList(string name, string dateModified, FileType type, long size)
        {
            imageList.Images.Add(GetFileIcon(type));
            imageList.ColorDepth = ColorDepth.Depth32Bit; // Improves quality of the image.
            fileListView.SmallImageList = imageList;
            fileListView.View = View.Details;
            ListViewItem fileViewItem = new ListViewItem { ImageIndex = imageList.Images.Count - 1, Text = name };
            fileViewItem.SubItems.Add(dateModified);
            fileViewItem.SubItems.Add(GetFileTypeString(type));
            fileViewItem.SubItems.Add(GetFileSizeString(size));
            fileListView.Items.Add(fileViewItem);
        }

        private void FileManager_Load(object sender, EventArgs e)
        {
            currentPath = "";
            Icon = WinIcons.Extract("shell32.dll", (int)WinIcons.ShellID.FilledFolder, false);
            RequestDrives();
        }

        private void RequestDrives()
        {
            fileListView.Enabled = false;
            fileListView.Items.Clear();
            SocketServer.Send(client.socket, "<REQUEST-DRIVES>");
        }

        private void RequestDirectories()
        {
            fileListView.Enabled = false;
            fileListView.Items.Clear();
            SocketServer.Send(client.socket, "<REQUEST-DIRS>" + currentPath);
            pathBox.Text = currentPath;
        }

        public static void ReceiveData(string fileData)
        {
            string[] fileDatas = fileData.Split("<FILE-END>");

            if (fileDatas == null)
                return;

            for (int i = 0; i < fileDatas.Count() - 1; i++)
                fileStringList.Add(fileDatas[i]);

            Main.uiRequests.Request(RequestUI.RequestType.UI_UPDATE_FILES);
        }

        private void fileListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (fileListView.SelectedItems.Count > 0)
            {
                string selectedFile = fileListView.SelectedItems[0].Text;

                if(currentPath.Length <= 3 && fileListView.SelectedItems[0].Text.Equals("..."))
                {
                    currentPath = "";
                    RequestDrives();
                    return;
                }
                else if(fileListView.SelectedItems[0].Index == 0 && fileListView.SelectedItems[0].Text.Equals("..."))
                {
                    string newPath = string.Empty;
                    string[] pathFolders = currentPath.Split(@"\");

                    // Build path string without the last folder.
                    for (int i = 0; i < (pathFolders.Count() - 1); i++)
                    {
                        newPath += pathFolders[i];
                        if(i != (pathFolders.Count() - 2))
                            newPath += @"\";

                        if(!newPath.Contains(@"\"))
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
                else if(Path.GetExtension(currentPath + selectedFile).Length > 0)
                {
                    // Do nothing.
                }
                else if (!string.IsNullOrEmpty(selectedFile))
                {
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

            AddFileToList(name, dateModified, type, size);
        }

        private void uiUpdateTimer_Tick(object sender, EventArgs e)
        {
            int requestID = Main.uiRequests.RequestReceived();
            if (requestID != 0) // Another thread requested UI to update requsted item.
            {
                switch (Main.uiRequests.GetRequestType(requestID)) // Check request type and call the correct function.
                {
                    case RequestUI.RequestType.UI_UPDATE_FILES:
                        foreach (string file in fileStringList)
                            AddDataToView(file);

                        fileListView.Enabled = true;
                        fileStringList.Clear();
                        break;
                }
            }
        }
    }
}
