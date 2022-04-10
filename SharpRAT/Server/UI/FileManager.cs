
namespace Server.UI
{
    public partial class FileManager : Form
    {
        private string currentPath = @"C:\";
        private readonly ImageList imageList;
        public FileManager()
        {
            imageList = new ImageList();
            InitializeComponent();
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

        private FileType GetFileType(string path)
        {
            string extension = Path.GetExtension(path);
            switch(extension)
            {
                case ".txt":
                case ".log":
                case ".cfg":
                    return FileType.TEXT_DOCUMENT;
                case ".exe":
                    return FileType.EXECUTABLE;
                case ".dll":
                    return FileType.DLL;
                case ".jpg":
                case ".png":
                case ".jpeg":
                case ".bmp":
                case ".webp":
                case ".gif":
                case ".apng":
                    return FileType.IMAGE;
                case ".mp4":
                case ".mov":
                case ".mkv":
                case ".flv":
                case ".webm":
                case ".wmv":
                case ".avi":
                    return FileType.VIDEO;
                default:
                    return FileType.FILE;
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

        private void LoadDirectories()
        {
            fileListView.Items.Clear();
            pathBox.Text = currentPath;
            try
            {
                AddFileToList("...", "", FileType.FILE_FOLDER, 0);
                foreach (string file in Directory.GetDirectories(currentPath))
                {
                    string finalFileName = file.Replace(currentPath, "");
                    if (finalFileName.StartsWith(@"\"))
                        finalFileName = finalFileName.Remove(0, 1);

                    string lastModified = Directory.GetLastWriteTime(currentPath + @"\" + finalFileName).ToString();
                    AddFileToList(finalFileName, lastModified, FileType.FILE_FOLDER, 0);
                }
                LoadFiles();
            }
            catch (Exception ex)
            {
                Server.Log.Error(ex.Message);
            }
        }

        private void LoadFiles()
        {
            foreach (string file in Directory.GetFiles(currentPath))
            {
                string finalFileName = file.Replace(currentPath, "");
                if (finalFileName.StartsWith(@"\"))
                    finalFileName = finalFileName.Remove(0, 1);

                long fileSize = new FileInfo(currentPath + @"\" + finalFileName).Length;
                string lastModified = File.GetLastWriteTime(currentPath + @"\" + finalFileName).ToString();

                AddFileToList(finalFileName, lastModified, GetFileType(finalFileName), fileSize);
            }
        }

        private void LoadDrives()
        {
            fileListView.Items.Clear();
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                if (d.IsReady == true)
                {
                    string driveName = d.Name;
                    //driveName = driveName.Replace(@"\", "");
                    AddFileToList(driveName, "", FileType.DRIVE, (d.TotalSize - d.AvailableFreeSpace));
                }
            }
        }

        private void FileManager_Load(object sender, EventArgs e)
        {
            LoadDirectories();
        }

        private void fileListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (fileListView.SelectedItems.Count > 0)
            {
                string selectedFile = fileListView.SelectedItems[0].Text;

                if(currentPath.Length <= 3 && fileListView.SelectedItems[0].Text.Equals("..."))
                {
                    currentPath = "";
                    LoadDrives();
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
                                LoadDrives();
                                return;
                            }
                            else
                            {
                                currentPath = pathFolders[0] + @"\";
                                LoadDirectories();
                                return;
                            }
                        }
                    }

                    currentPath = newPath;
                    LoadDirectories();
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
                    LoadDirectories();
                }
            }
        }
    }
}
