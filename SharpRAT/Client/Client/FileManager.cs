
using System.Text;

namespace Client.Client
{
    internal class FileManager
    {
        private static List<string> fileStringList = new();
        private static string currentPath = @"C:\";

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

        private static FileType GetFileType(string path)
        {
            string extension = Path.GetExtension(path);
            switch (extension)
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

        private static void AddFileToList(string name, string dateModified, FileType type, long size)
        {
            fileStringList.Add(name + "<SPLIT>" + dateModified + "<SPLIT>" + ((int)type).ToString() + "<SPLIT>" + size.ToString() + "<FILE-END>");
        }

        public static bool LoadDirectories(string newPath)
        {
            fileStringList.Clear();
            currentPath = newPath;
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
                bool status = LoadFiles();
                SendFileList();
                return status;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public static bool LoadFiles()
        {
            try
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
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public static bool LoadDrives()
        {
            fileStringList.Clear();
            currentPath = "";
            try
            {
                foreach (DriveInfo d in DriveInfo.GetDrives())
                {
                    if (d.IsReady == true)
                    {
                        AddFileToList(d.Name, "-", FileType.DRIVE, (d.TotalSize - d.AvailableFreeSpace));
                    }
                }
                SendFileList();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public static void SendFileList()
        {
            string filesString = "";
            foreach (string file in fileStringList)
                filesString += file;

            Main.socketClient.GetServerSocket().Send(Encoding.ASCII.GetBytes("<FILE>" + filesString + "<EOF>"));
        }
    }
}
