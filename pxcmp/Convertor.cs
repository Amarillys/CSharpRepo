using System.Drawing;
using System.IO;

namespace pxcmp_cs
{
    class Convertor
    {
        public static byte[] png2byte(string img)
        {
            Bitmap bmp = new Bitmap(img);
            int w = bmp.Width;
            int h = bmp.Height;
            int d = GetBitDepth(img) / 8;
            byte[] px = new byte[w * h * d];
            Color pixel = new Color();
            for (int i = 0; i < px.Length / d; ++i)
            {
                pixel = bmp.GetPixel(i % w, i / w);
                px[i * d + 0] = pixel.R;
                px[i * d + 1] = pixel.G;
                px[i * d + 2] = pixel.B;
                px[i * d + 3] = pixel.A;
            }
            return px;
        }

        //get the bit depth of the image by reading its header
        private static int GetBitDepth(string img)
        {
            int depth = -1;
            FileStream imgFile = new FileStream(img, FileMode.Open, FileAccess.Read, FileShare.Read);
            imgFile.Seek(0, SeekOrigin.Begin);
            if (imgFile.ReadByte() == 0x89)
                if (imgFile.ReadByte() == 0x50)
                    if (imgFile.ReadByte() == 0x4e)
                    {
                        imgFile.Seek(25, SeekOrigin.Begin);
                        int src_bit = imgFile.ReadByte();
                        if (src_bit == 0x06)
                            depth = 32;
                        else
                            depth = 24;
                    }
            imgFile.Close();
            return depth;
        }
    }
}
