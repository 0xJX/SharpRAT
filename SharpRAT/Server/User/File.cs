namespace Server.User
{
    public enum FileType
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

    public class ClientFile
    {
        public string name;
        public long size;
        public string dateModified;
        public FileType type;

        public string GetFileSizeString()
        {
            if (size == 0)
                return "";

            string ending = "bytes";
            long tempBytes = size;

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

        public string GetFileTypeString()
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
                case FileType.AUDIO:
                    return "Audio";
                case FileType.VIDEO:
                    return "Video";
                case FileType.TEXT_DOCUMENT:
                    return "Text Document";
                default:
                    return "???";
            }
        }

        public ClientFile(string name, long size, string dateModified, FileType type)
        {
            this.name = name;
            this.size = size;
            this.dateModified = dateModified;
            this.type = type;
        }
    }
}
