using System.Runtime.InteropServices;

namespace Server
{
    public class WinIcons
    {
        //https://www.digitalcitizen.life/where-find-most-windows-10s-native-icons/
        public enum ShellID // https://help4windows.com/windows_8_shell32_dll.shtml
        {
            Shutdown_ICO = 27,
            Config_ICO = 69,
            Keychain_ICO = 104,
            Question_ICO = 154,
            PcKeyboard_ICO = 173,
            NotAllowed_ICO = 219,
            Warning_ICO = 233,
            Info_ICO = 277,
            Error_ICO = 244
        }
        public enum ComresID
        {
            Error_ICO = 10
        }

        public enum Imageres
        {
            ResourceMgr_ICO = 145
        }

        public enum Dsuiext
        {
            User_ICO = 3
        }

        public enum Wmploc
        {
            Settings_ICO = 17
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
