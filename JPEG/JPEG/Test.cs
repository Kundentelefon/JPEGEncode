using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEG
{
    class Test
    {
        public void testZickZackbyte()
        {
            byte[,] test = new byte[4, 6] { { 1, 2, 6, 7, 14, 15 }, { 3, 5, 8, 13, 16, 21 }, { 4, 9, 12, 17, 20, 22 }, { 10, 11, 18, 19, 23, 24 } };
            byte[,] test2 = new byte[4, 7] { { 0, 1, 5, 6, 13, 14, 21 }, { 2, 4, 7, 12, 15, 20, 22 }, { 3, 8, 11, 16, 19, 23, 26 }, { 9, 10, 17, 18, 24, 25, 27 } };
            byte[,] test3 = new byte[5, 7] { { 0, 1, 5, 6, 14, 15, 24 }, { 2, 4, 7, 13, 16, 23, 25 }, { 3, 8, 12, 17, 22, 26, 31 }, { 9, 11, 18, 21, 27, 30, 32 }, { 10, 19, 20, 28, 29, 33, 34 } };
            byte[,] test4 = new byte[5, 6] { { 1, 2, 6, 7, 15, 16 }, { 3, 5, 8, 14, 17, 24 }, { 4, 9, 13, 18, 23, 25 }, { 10, 12, 19, 22, 26, 29 }, { 11, 20, 21, 27, 28, 30 } };

            byte[,] test5 = new byte[6, 4] { { 1, 2, 6, 7 }, { 3, 5, 8, 14 }, { 4, 9, 13, 15 }, { 10, 12, 16, 21 }, { 11, 17, 20, 22 }, { 18, 19, 23, 24 } };
            byte[,] test6 = new byte[7, 6] { { 1, 2, 6, 7, 15, 16 }, { 3, 5, 8, 14, 17, 27 }, { 4, 9, 13, 18, 26, 28 }, { 10, 12, 19, 25, 29, 36 }, { 11, 20, 24, 30, 35, 37 }, { 21, 23, 31, 34, 38, 41 }, { 22, 32, 33, 39, 40, 42 } };
            byte[,] test7 = new byte[7, 5] { { 1, 2, 6, 7, 15 }, { 3, 5, 8, 14, 16 }, { 4, 9, 13, 17, 25 }, { 10, 12, 18, 24, 26 }, { 11, 19, 23, 27, 32 }, { 20, 22, 28, 31, 33 }, { 21, 29, 30, 34, 35 } };
            byte[,] test8 = new byte[6, 5] { { 1, 2, 6, 7, 15 }, { 3, 5, 8, 14, 16 }, { 4, 9, 13, 17, 24 }, { 10, 12, 18, 23, 25 }, { 11, 19, 22, 26, 29 }, { 20, 21, 27, 28, 30 } };
            //var test2= test.GetLength(0);
            //var test3 = test.GetLength(1);
            Math testmath = new Math();
            //var res = testmath.ZickZackScanByte(test);
            //var res2 = testmath.ZickZackScanByte(test2);
            //var res3 = testmath.ZickZackScanByte(test3);
            //var res4 = testmath.ZickZackScanByte(test4);
            //passt
            var res5 = testmath.ZickZackScanByte(test5);
            var res6 = testmath.ZickZackScanByte(test6);
            var res7 = testmath.ZickZackScanByte(test7);
            var res8 = testmath.ZickZackScanByte(test8);
        }
    }
}
