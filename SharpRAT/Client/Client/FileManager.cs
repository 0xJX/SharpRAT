
using System.Diagnostics;
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
            AUDIO,
            VIDEO,
            TEXT_DOCUMENT
        }

        private static FileType GetFileType(string path)
        {
            string extension = Path.GetExtension(path);
            switch (extension)
            {
                case ".txt":
                case ".ini":
                case ".log":
                case ".cfg":
                case ".srt":
                case ".dat":
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
                case ".mp3":
                case ".wav":
                case ".wma":
                case ".aac":
                case ".flac":
                case ".m4a":
                case ".m4b":
                case ".ogg":
                    return FileType.AUDIO;
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
            catch (UnauthorizedAccessException)
            {
                AddFileToList("No access to load content!", "", FileType.FILE, 0);
                SendFileList();
            }
            catch (DirectoryNotFoundException)
            {
                AddFileToList("Directory not found!", "", FileType.FILE, 0);
                SendFileList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("LoadDirectories: " + ex.Message);
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
                Console.WriteLine("LoadFiles: " + ex.Message);
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
                        AddFileToList(d.Name, "-", FileType.DRIVE, d.TotalSize);
                    }
                }
                SendFileList();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("LoadDrives: " + ex.Message);
            }
            return false;
        }

        public static void SendFileList()
        {
            string filesString = "";
            foreach (string file in fileStringList)
                filesString += file;

            Console.WriteLine("Sending filelist to server that contains: " + fileStringList.Count() + " files.");
            Main.socketClient.GetServerSocket().Send(Encoding.UTF8.GetBytes("<FILE>" + filesString + "<EOF>"));
            fileStringList.Clear();
        }

        public static void OpenFile(string filePath)
        {
            Console.WriteLine("OpenFile called with path: " + filePath);
            if (File.Exists(filePath))
            {
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = filePath
                };
                process.Start();
            }
        }
    }
}
