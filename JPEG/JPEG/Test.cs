using JPEG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEG
{
    class Test
    {
        public bool testZickZackbyte()
        {
            byte[,] test = new byte[4, 6] { { 1, 2, 6, 7, 14, 15 }, { 3, 5, 8, 13, 16, 21 }, { 4, 9, 12, 17, 20, 22 }, { 10, 11, 18, 19, 23, 24 } };
            byte[,] test2 = new byte[4, 7] { { 1, 2, 6, 7, 14, 15, 22 }, { 3, 5, 8, 13, 16, 21, 23 }, { 4, 9, 12, 17, 20, 24, 27 }, { 10, 11, 18, 19, 25, 26, 28 } };
            byte[,] test3 = new byte[5, 7] { { 1, 2, 6, 7, 15, 16, 25 }, { 3, 5, 8, 14, 17, 24, 26 }, { 4, 9, 13, 18, 23, 27, 32 }, { 10, 12, 19, 22, 28, 31, 33 }, { 11, 20, 21, 29, 30, 34, 35 } };
            byte[,] test4 = new byte[5, 6] { { 1, 2, 6, 7, 15, 16 }, { 3, 5, 8, 14, 17, 24 }, { 4, 9, 13, 18, 23, 25 }, { 10, 12, 19, 22, 26, 29 }, { 11, 20, 21, 27, 28, 30 } };

            byte[,] test5 = new byte[6, 4] { { 1, 2, 6, 7 }, { 3, 5, 8, 14 }, { 4, 9, 13, 15 }, { 10, 12, 16, 21 }, { 11, 17, 20, 22 }, { 18, 19, 23, 24 } };
            byte[,] test6 = new byte[7, 6] { { 1, 2, 6, 7, 15, 16 }, { 3, 5, 8, 14, 17, 27 }, { 4, 9, 13, 18, 26, 28 }, { 10, 12, 19, 25, 29, 36 }, { 11, 20, 24, 30, 35, 37 }, { 21, 23, 31, 34, 38, 41 }, { 22, 32, 33, 39, 40, 42 } };
            byte[,] test7 = new byte[7, 5] { { 1, 2, 6, 7, 15 }, { 3, 5, 8, 14, 16 }, { 4, 9, 13, 17, 25 }, { 10, 12, 18, 24, 26 }, { 11, 19, 23, 27, 32 }, { 20, 22, 28, 31, 33 }, { 21, 29, 30, 34, 35 } };
            byte[,] test8 = new byte[6, 5] { { 1, 2, 6, 7, 15 }, { 3, 5, 8, 14, 16 }, { 4, 9, 13, 17, 24 }, { 10, 12, 18, 23, 25 }, { 11, 19, 22, 26, 29 }, { 20, 21, 27, 28, 30 } };

            Math testmath = new Math();
            byte[] restest = new byte[24];
            byte[] restest2 = new byte[28];
            byte[] restest3 = new byte[35];
            byte[] restest4 = new byte[30];
            for (byte i = 0; i < 24; i++)
            {
                restest[i] = (byte)(i+1);
            }
            for (byte i = 0; i < 28; i++)
            {
                restest2[i] = (byte)(i+1);
            }
            for (byte i = 0; i < 35; i++)
            {
                restest3[i] = (byte)(i+1);
            }
            for (byte i = 0; i < 30; i++)
            {                
                restest4[i] = (byte)(i +1);
            }
            var result1 = false;
            var res = testmath.ZickZackScanByte(test);            
            var res2 = testmath.ZickZackScanByte(test2);
            var res3 = testmath.ZickZackScanByte(test3);
            var res4 = testmath.ZickZackScanByte(test4);
            if (res.SequenceEqual(restest)&& res2.SequenceEqual(restest2) && res3.SequenceEqual(restest3) && res4.SequenceEqual(restest4))
            {
                result1 = true;
            }

            byte[] restest5 = new byte[24];
            byte[] restest6 = new byte[42];
            byte[] restest7 = new byte[35];
            byte[] restest8 = new byte[30];
            for (byte i = 0; i < 24; i++)
            {
                restest5[i] = (byte)(i + 1);
            }
            for (byte i = 0; i < 42; i++)
            {
                restest6[i] = (byte)(i + 1);
            }
            for (byte i = 0; i < 35; i++)
            {
                restest7[i] = (byte)(i + 1);
            }
            for (byte i = 0; i < 30; i++)
            {
                restest8[i] = (byte)(i + 1);
            }
            var result2 = false;
            //passt
            var res5 = testmath.ZickZackScanByte(test5);
            var res6 = testmath.ZickZackScanByte(test6);
            var res7 = testmath.ZickZackScanByte(test7);
            var res8 = testmath.ZickZackScanByte(test8);
            if (res5.SequenceEqual(restest5) && res6.SequenceEqual(restest6) && res7.SequenceEqual(restest7) && res8.SequenceEqual(restest8))
            {
                result2 = true;
            }
            if (result1 && result2)
            {
                return (true);
            }
            return (false);


            //
            Color[] Colortest = new Color[10];
            Color3 col3 = new Color3();
            Color1 col1 = new Color1();

            Colortest[1] = col3;
            Colortest[2] = col1;
        }
    }
}
