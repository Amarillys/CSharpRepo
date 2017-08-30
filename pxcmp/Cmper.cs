using System.IO;

namespace pxcmp_cs
{
    class Cmper
    {
        public static void Cmp(byte[] px0, byte[] px1)
        {
            int len = 255;
            int[] x = new int[len];
            int[] y = new int[len];
            for (int r = 255 - len; r < len; ++r)
                for(int i = 0; i < px0.Length / 4; ++i)
                {
                    if (px0[i * 4 + 1] == r)
                    {
                        x[r + len - 255] = px0[i * 4 + 1];
                        y[r + len - 255] = px1[i * 4 + 1];
                    }
                }

            using (var fs = new FileStream("r.txt", FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    for (int i = 0; i < len; ++i)
                        sw.WriteLine("{0},{1}", x[i], y[i]);
                }
            }
        }
    }
}
