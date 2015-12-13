using JPEG.Model;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JPEG
{
    class Test
    {
        Mathloc testmath = new Mathloc();

        public void HuffmanDepthTest()
        {
            HuffmanNode node1 = new HuffmanNode(1, 1);
            HuffmanNode node2 = new HuffmanNode(2, 1);
            HuffmanNode node3 = new HuffmanNode(5, 5);
            HuffmanNode node4 = new HuffmanNode(7, 7);
            HuffmanNode node5 = new HuffmanNode(10, 10);
            HuffmanNode node6 = new HuffmanNode(14, 14);

            List<HuffmanNode> testList = new List<HuffmanNode> { node1, node5, node3, node6, node2, node4 };
            HuffmanEncoder encoder = new HuffmanEncoder();
            Dictionary<byte, int> testDict = new Dictionary<byte, int>();
            //testDict = encoder.EncodeToPackageMerge(testList, 4);

        }
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


            byte[] restest = new byte[24];
            byte[] restest2 = new byte[28];
            byte[] restest3 = new byte[35];
            byte[] restest4 = new byte[30];
            for (byte i = 0; i < 24; i++)
            {
                restest[i] = (byte)(i + 1);
            }
            for (byte i = 0; i < 28; i++)
            {
                restest2[i] = (byte)(i + 1);
            }
            for (byte i = 0; i < 35; i++)
            {
                restest3[i] = (byte)(i + 1);
            }
            for (byte i = 0; i < 30; i++)
            {
                restest4[i] = (byte)(i + 1);
            }
            var result1 = false;
            var res = testmath.ZickZackScanByte(test);
            var res2 = testmath.ZickZackScanByte(test2);
            var res3 = testmath.ZickZackScanByte(test3);
            var res4 = testmath.ZickZackScanByte(test4);
            if (res.SequenceEqual(restest) && res2.SequenceEqual(restest2) && res3.SequenceEqual(restest3) && res4.SequenceEqual(restest4))
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


            //Color example
            Color[] Colortest = new Color[10];
            Color3 col3 = new Color3();
            Color1 col1 = new Color1();

            Colortest[1] = col3;
            Colortest[2] = col1;

        }
        public bool conversionTest()
        {

            byte[] testvect = { 255, 200, 0 };
            var temp = testmath.RGBToYUV(testvect);
            var temp2 = testmath.YUVToRGB(temp);


            return (false);
        }

        //Bitmap test
        private static void TestChannelResolutionReduction(string inputFile, string outputFile)
        {
            // load Bitmap .png
            Bitmap bitmap = new Bitmap(inputFile);
            Color[,] picture = new Color[bitmap.Height, bitmap.Width];
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    var bitmapPixel = bitmap.GetPixel(x, y);
                    // sets RGB for each pixel
                    Color3 newPixel = new Color3
                    {
                        a = bitmapPixel.R,
                        b = bitmapPixel.G,
                        c = bitmapPixel.B
                    };
                    picture[y, x] = newPixel;
                }
            }

            //    CompressionHandler compressionHandler = new CompressionHandler(picture, bitmap.Width, bitmap.Height);

            //    // compressionhandler dont neednead parameter yet
            //    Color[,] result = compressionHandler.LocalAveraging444To422(0xDEAD);

            //    Bitmap resultImage = new Bitmap(bitmap.Width, bitmap.Height);
            //    for (int y = 0; y < bitmap.Height; y++)
            //    {
            //        for (int x = 0; x < bitmap.Width; x++)
            //        {
            //            // draws image
            //            System.Drawing.Color color = System.Drawing.Color.FromArgb(result[y, x].a, result[y, x].b, result[y, x].c);
            //            resultImage.SetPixel(x, y, color);
            //        }
            //    }
            //    resultImage.Save(outputFile);
        }

        public static void BitTest()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            // test with 10 Mio 1 Bits
            Bitstream bs = new Bitstream(10000000);
            bs.AddBits(true, 10000000);
            sw.Stop();
            Console.WriteLine("BitWrite Elapsed={0} ", sw.Elapsed);
            Console.ReadKey();
        }
        public void HuffmanMergeBaum()
        {
            HuffmanEncoder huff = new HuffmanEncoder();
            Dictionary<byte, int> test = new Dictionary<byte, int>();
            Dictionary<byte, int> test2 = new Dictionary<byte, int>();
            test.Add(0, 4);
            test.Add(1, 4);
            test.Add(2, 3);
            test.Add(3, 2);
            test.Add(4, 2);
            test.Add(5, 2);
            test2.Add(0, 3);
            test2.Add(1, 3);
            test2.Add(2, 5);
            test2.Add(3, 7);
            test2.Add(4, 14);
            test2.Add(5, 14);
            huff.EncodeToPackageMergeList(test, test2);
        }

        public void TestDCT()
        {
            float[,] testMat8 = new float[8, 8] {
                { 16, 11, 10, 16, 24, 40, 51, 61 },
                { 12, 12, 14, 19, 26, 58, 60, 55 },
                { 14, 13, 16, 24, 40, 57, 69, 56 },
                { 14, 17, 22, 29, 51, 87, 80, 62 },
                { 18, 22, 37, 56, 68, 109, 103, 77 },
                { 24, 35, 55, 64, 81, 194, 113, 92 },
                { 49, 64, 78, 87, 103, 121, 120, 101 },
                { 72, 92, 95, 98, 121, 100, 103, 99 } };

            DCT testDCT1 = new DCT();
            //test DCT direct
            testDCT1.printMatrix( testDCT1.DCTdirect(testMat8) );
            //test DCT direct inverse after DCT direct
            testDCT1.printMatrix( testDCT1.IDCTdirect( testDCT1.DCTdirect(testMat8) ) );

            //test DCT seperated
            //testDCT1.printMatrix(testDCT1.DCTseperated(testMat8));

            //test DCT Arai
            //testDCT1.printMatrix(testDCT1.DCTArai(testMat8));

            Console.ReadKey();

            //TODO: Test if Math.PI needs (float) for performance
        }
    }
}
