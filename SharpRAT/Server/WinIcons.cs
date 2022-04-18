using System.Runtime.InteropServices;

namespace Server
{
    public class WinIcons
    {
        /*
         Easy way to get ID:s for the .DLL images is, by creating an folder and changing the icon of it.
         Then opening the folder with VSCode and you can see the hidden desktop.ini file that contains part
         "IconResource=C:\Windows\System32\SHELL32.dll,7" where the 7 is the selected icon id. :)
        */

        //https://www.digitalcitizen.life/where-find-most-windows-10s-native-icons/
        public enum ShellID // https://help4windows.com/windows_8_shell32_dll.shtml
        {
            UnknownFile = 0,
            Executable = 2,
            Folder = 3,
            Drive = 7,
            MyComputer = 15,
            Shutdown = 27,
            FileSearch = 55,
            Config = 69,
            TextFile = 70,
            Keychain = 104,
            FilledFolder = 126,
            Question = 154,
            PcKeyboard = 173,
            NotAllowed = 219,
            Warning = 233,
            Desktop = 240,
            Error = 244,
            Info = 277,
            Image = 311,
            Sound = 312,
            Video = 313,
            Settings = 314
        }
        public enum ComresID
        {
            Error = 10
        }

        public enum Imageres
        {
            Admin = 73,
            ResourceMgr = 145
        }

        public enum Dsuiext
        {
            User = 3
        }

        public enum Wmploc
        {
            Settings = 17
        }

        [DllImport("shell32", CharSet = CharSet.Unicode)]
        private static extern int ExtractIconEx(string lpszFile, int nIconIndex, out IntPtr phiconLarge, IntPtr phiconSmall, int nIcons);

        [DllImport("shell32", CharSet = CharSet.Unicode)]
        private static extern int ExtractIconEx(string lpszFile, int nIconIndex, IntPtr phiconLarge, out IntPtr phiconSmall, int nIcons);

        public static Bitmap GetImageFromIcon(string filePath, int index, bool largeIcon = true)
        {
            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            Bitmap img = Bitmap.FromHicon(Extract(filePath, index, largeIcon).Handle);
            return img;
        }
        public static Icon Extract(string filePath, int index, bool largeIcon = true)
        {
            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            IntPtr hIcon;
            if (largeIcon)
            {
                ExtractIconEx(filePath, index, out hIcon, IntPtr.Zero, 1);
            }
            else
            {
                ExtractIconEx(filePath, index, IntPtr.Zero, out hIcon, 1);
            }

            return hIcon != IntPtr.Zero ? Icon.FromHandle(hIcon) : null;
        }
    }
}
