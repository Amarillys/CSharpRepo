using System;

namespace pxcmp_cs
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage pxcmp_cs img0.png img1.png");
                Console.ReadKey();
                return;
            }
            
            byte[] px0 = Convertor.png2byte(args[0]);
            byte[] px1 = Convertor.png2byte(args[1]);

            Cmper.Cmp(px0, px1);
        }
    }
}
