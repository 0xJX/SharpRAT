
using System.Drawing.Imaging;
using System.Text;

namespace Client.Client
{
    internal class ScreenViewer
    {
        public static void TakePrintScreen(int screenIndex)
        {
            Bitmap bitmap = new(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            Rectangle captureRectangle = Screen.AllScreens[screenIndex].Bounds;
            Graphics captureGraphics = Graphics.FromImage(bitmap);
            captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);

            byte[] imageStream;
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);
                imageStream = stream.ToArray();
            }

            if(imageStream != null)
            {
                Main.socketClient.GetServerSocket().Send(Encoding.ASCII.GetBytes("<PRINTSCREEN>"));
                SendImageToServer(imageStream);
            }
        }

        public static void SendImageToServer(byte[] imageData)
        {
            Main.socketClient.GetServerSocket().Send(imageData);
            Main.socketClient.GetServerSocket().Send(Encoding.ASCII.GetBytes("<EOF>"));
        }
    }
}
