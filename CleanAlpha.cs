using System;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;

namespace GetAlphaOfPng
{
    class MaikazeImage
    {
        public int w, h;
        public int bitDepth, alpha;
        public string fileName;
        public BitmapSource bmpSrc;
        public byte[] px;
        public int pos;
        public MaikazeImage()
        {
            bitDepth = 32;
            alpha = 255;
            pos = -1;
        }

        public void LoadImage(string dst_fileName)
        {   
            fileName = dst_fileName;
            Uri fileUri = new Uri(dst_fileName);
            BitmapDecoder decoder = BitmapDecoder.Create(fileUri, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
            bmpSrc = decoder.Frames[0];
            w = (int)bmpSrc.PixelWidth;
            h = (int)bmpSrc.PixelHeight;
            px = new byte[w * h * bitDepth / 8];
            bmpSrc.CopyPixels(px, w * bitDepth / 8, 0);
        }

        public void RefreshImageFromPixels()
        {
            PixelFormat newFmt = PixelFormats.Bgra32;
            bmpSrc = BitmapSource.Create(w, h, 0, 0, newFmt, null, px, w * bitDepth / 8);
        }
        
        public void SaveImage()
        {
            FileStream saveimg = new FileStream(fileName, FileMode.Create);
            PngBitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Interlace = PngInterlaceOption.Off;
            pngEncoder.Frames.Add(BitmapFrame.Create(bmpSrc));
            pngEncoder.Save(saveimg);
            saveimg.Close();
        }

        public void Clean()
        {
            int t = 0;
            for (int i = 0; i < w * h - 1; ++i)
            {
                t = i * 4;
                if (px[t + 3] == 0)
                {
                    px[t + 0] = 0;
                    px[t + 1] = 0;
                    px[t + 2] = 0;
                }
            }
            RefreshImageFromPixels();
            SaveImage();
        }
    }
}
