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

            List<HuffmanNode> testList = new List<HuffmanNode> {node1, node5, node3, node6, node2, node4};
            HuffmanEncoder encoder = new HuffmanEncoder();
            Dictionary<byte, int> testDict = new Dictionary<byte, int>();
            //testDict = encoder.EncodeToPackageMerge(testList, 4);

        }

        public bool testZickZackbyte()
        {
            byte[,] test = new byte[4, 6]
            {{1, 2, 6, 7, 14, 15}, {3, 5, 8, 13, 16, 21}, {4, 9, 12, 17, 20, 22}, {10, 11, 18, 19, 23, 24}};
            byte[,] test2 = new byte[4, 7]
            {
                {1, 2, 6, 7, 14, 15, 22}, {3, 5, 8, 13, 16, 21, 23}, {4, 9, 12, 17, 20, 24, 27},
                {10, 11, 18, 19, 25, 26, 28}
            };
            byte[,] test3 = new byte[5, 7]
            {
                {1, 2, 6, 7, 15, 16, 25}, {3, 5, 8, 14, 17, 24, 26}, {4, 9, 13, 18, 23, 27, 32},
                {10, 12, 19, 22, 28, 31, 33}, {11, 20, 21, 29, 30, 34, 35}
            };
            byte[,] test4 = new byte[5, 6]
            {
                {1, 2, 6, 7, 15, 16}, {3, 5, 8, 14, 17, 24}, {4, 9, 13, 18, 23, 25}, {10, 12, 19, 22, 26, 29},
                {11, 20, 21, 27, 28, 30}
            };

            byte[,] test5 = new byte[6, 4]
            {{1, 2, 6, 7}, {3, 5, 8, 14}, {4, 9, 13, 15}, {10, 12, 16, 21}, {11, 17, 20, 22}, {18, 19, 23, 24}};
            byte[,] test6 = new byte[7, 6]
            {
                {1, 2, 6, 7, 15, 16}, {3, 5, 8, 14, 17, 27}, {4, 9, 13, 18, 26, 28}, {10, 12, 19, 25, 29, 36},
                {11, 20, 24, 30, 35, 37}, {21, 23, 31, 34, 38, 41}, {22, 32, 33, 39, 40, 42}
            };
            byte[,] test7 = new byte[7, 5]
            {
                {1, 2, 6, 7, 15}, {3, 5, 8, 14, 16}, {4, 9, 13, 17, 25}, {10, 12, 18, 24, 26}, {11, 19, 23, 27, 32},
                {20, 22, 28, 31, 33}, {21, 29, 30, 34, 35}
            };
            byte[,] test8 = new byte[6, 5]
            {
                {1, 2, 6, 7, 15}, {3, 5, 8, 14, 16}, {4, 9, 13, 17, 24}, {10, 12, 18, 23, 25}, {11, 19, 22, 26, 29},
                {20, 21, 27, 28, 30}
            };


            byte[] restest = new byte[24];
            byte[] restest2 = new byte[28];
            byte[] restest3 = new byte[35];
            byte[] restest4 = new byte[30];
            for (byte i = 0; i < 24; i++)
            {
                restest[i] = (byte) (i + 1);
            }
            for (byte i = 0; i < 28; i++)
            {
                restest2[i] = (byte) (i + 1);
            }
            for (byte i = 0; i < 35; i++)
            {
                restest3[i] = (byte) (i + 1);
            }
            for (byte i = 0; i < 30; i++)
            {
                restest4[i] = (byte) (i + 1);
            }
            var result1 = false;
            var res = testmath.ZickZackScanByte(test);
            var res2 = testmath.ZickZackScanByte(test2);
            var res3 = testmath.ZickZackScanByte(test3);
            var res4 = testmath.ZickZackScanByte(test4);
            if (res.SequenceEqual(restest) && res2.SequenceEqual(restest2) && res3.SequenceEqual(restest3) &&
                res4.SequenceEqual(restest4))
            {
                result1 = true;
            }

            byte[] restest5 = new byte[24];
            byte[] restest6 = new byte[42];
            byte[] restest7 = new byte[35];
            byte[] restest8 = new byte[30];
            for (byte i = 0; i < 24; i++)
            {
                restest5[i] = (byte) (i + 1);
            }
            for (byte i = 0; i < 42; i++)
            {
                restest6[i] = (byte) (i + 1);
            }
            for (byte i = 0; i < 35; i++)
            {
                restest7[i] = (byte) (i + 1);
            }
            for (byte i = 0; i < 30; i++)
            {
                restest8[i] = (byte) (i + 1);
            }
            var result2 = false;
            //passt
            var res5 = testmath.ZickZackScanByte(test5);
            var res6 = testmath.ZickZackScanByte(test6);
            var res7 = testmath.ZickZackScanByte(test7);
            var res8 = testmath.ZickZackScanByte(test8);
            if (res5.SequenceEqual(restest5) && res6.SequenceEqual(restest6) && res7.SequenceEqual(restest7) &&
                res8.SequenceEqual(restest8))
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

            byte[] testvect = {255, 200, 0};
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
            float[,] testMat81 = new float[8, 8]
            {
                {16, 11, 10, 16, 24, 40, 51, 61},
                {12, 12, 14, 19, 26, 58, 60, 55},
                {14, 13, 16, 24, 40, 57, 69, 56},
                {14, 17, 22, 29, 51, 87, 80, 62},
                {18, 22, 37, 56, 68, 109, 103, 77},
                {24, 35, 55, 64, 81, 194, 113, 92},
                {49, 64, 78, 87, 103, 121, 120, 101},
                {72, 92, 95, 98, 121, 100, 103, 99}
            };

            float[,] testMat82 = new float[8, 8]
            {
                {92, 3, -9, -7, 3, -1, 0, 2},
                {-39, -58, 12, 17, -2, 2, 4, 2},
                {-84, 62, 1, -18, 3, 4, -5, 5},
                {-52, -36, -10, 14, -10, 4, -2, 0},
                {-86, -40, 49, -7, 17, -6, -2, 5},
                {-62, 65, -12, -2, 3, -8, -2, 0},
                {-17, 14, -36, 17, -11, 3, 3, -1},
                {-54, 32, -9, -9, 22, 0, 1, 3}
            };

            float[,] testMat83 = new float[8, 8]
            {
                {-76, -73, -67, -62, -58, -67, -64, -55},
                {-65, -69, -73, -38, -19, -43, -59, -56},
                {-66, -69, -60, -15, 16, -24, -62, -55},
                {-65, -70, -57, -6, 26, -22, -58, -59},
                {-61, -67, -60, -24, -2, -40, -60, -58},
                {-49, -63, -68, -58, -51, -60, -70, -53},
                {-43, -57, -64, -69, -73, -67, -63, -45},
                {-41, -49, -59, -60, -63, -52, -50, -34}
            };

            Stopwatch sw = new Stopwatch();
            Stopwatch sw1 = new Stopwatch();
            Stopwatch sw2 = new Stopwatch();

            Console.WriteLine("Ausgangsmatrix:");
            DCT.printMatrix(testMat83);
            Console.WriteLine("Matrix direct:");
            sw.Start();
            DCT.printMatrix(DCT.DCTdirect(testMat83));
            sw.Stop();
            Console.WriteLine("Matrix direct: Elapsed={0} \n", sw.Elapsed);
            Console.WriteLine("Matrix direct inverse:");
            DCT.printMatrix(DCT.IDCTdirect(DCT.DCTdirect(testMat83)));
            Console.WriteLine("Matrix separated:");
            sw1.Start();
            DCT.printMatrix(DCT.DCTseparated(testMat83));
            sw1.Stop();
            Console.WriteLine("Matrix separated: Elapsed={0} \n", sw1.Elapsed);
            Console.WriteLine("Matrix Arai:");
            sw2.Start();
            DCT.printMatrix(DCT.DCTArai(testMat83));
            sw2.Stop();
            Console.WriteLine("Matrix Arai: Elapsed={0} \n", sw2.Elapsed);

            //Console.ReadKey();
        }

        public void schleifentest()
        {
            float[,] testMat81 = new float[8, 8]
            {
                {16, 11, 10, 16, 24, 40, 51, 61},
                {12, 12, 14, 19, 26, 58, 60, 55},
                {14, 13, 16, 24, 40, 57, 69, 56},
                {14, 17, 22, 29, 51, 87, 80, 62},
                {18, 22, 37, 56, 68, 109, 103, 77},
                {24, 35, 55, 64, 81, 194, 113, 92},
                {49, 64, 78, 87, 103, 121, 120, 101},
                {72, 92, 95, 98, 121, 100, 103, 99}
            };

            float[,] testMat82 = new float[8, 8]
            {
                {92, 3, -9, -7, 3, -1, 0, 2},
                {-39, -58, 12, 17, -2, 2, 4, 2},
                {-84, 62, 1, -18, 3, 4, -5, 5},
                {-52, -36, -10, 14, -10, 4, -2, 0},
                {-86, -40, 49, -7, 17, -6, -2, 5},
                {-62, 65, -12, -2, 3, -8, -2, 0},
                {-17, 14, -36, 17, -11, 3, 3, -1},
                {-54, 32, -9, -9, 22, 0, 1, 3}
            };

            float[,] testMat83 = new float[8, 8]
            {
                {-76, -73, -67, -62, -58, -67, -64, -55},
                {-65, -69, -73, -38, -19, -43, -59, -56},
                {-66, -69, -60, -15, 16, -24, -62, -55},
                {-65, -70, -57, -6, 26, -22, -58, -59},
                {-61, -67, -60, -24, -2, -40, -60, -58},
                {-49, -63, -68, -58, -51, -60, -70, -53},
                {-43, -57, -64, -69, -73, -67, -63, -45},
                {-41, -49, -59, -60, -63, -52, -50, -34}
            };

            Stopwatch sw = new Stopwatch();
            Stopwatch sw1 = new Stopwatch();
            Stopwatch sw2 = new Stopwatch();

            sw.Start();
            for (int i = 0; i < 5000; i++)
            {
                DCT.DCTdirect(testMat83);
            }
            sw.Stop();

            DCT.IDCTdirect(DCT.DCTdirect(testMat83));

            sw1.Start();
            for (int i = 0; i < 5000; i++)
            {
                DCT.DCTseparated(testMat83);
            }
            sw1.Stop();

            sw2.Start();
            for (int i = 0; i < 5000; i++)
            {
                DCT.DCTArai(testMat83);
            }
            sw2.Stop();

            Console.WriteLine($"/{sw.Elapsed} /{sw1.Elapsed} /{sw2.Elapsed}");
            //Console.ReadKey();
        }

        public void loopTestArai()
        {
            float[,] testMatArai = new float[8, 8]
            {
                {-76, -73, -67, -62, -58, -67, -64, -55},
                {-65, -69, -73, -38, -19, -43, -59, -56},
                {-66, -69, -60, -15, 16, -24, -62, -55},
                {-65, -70, -57, -6, 26, -22, -58, -59},
                {-61, -67, -60, -24, -2, -40, -60, -58},
                {-49, -63, -68, -58, -51, -60, -70, -53},
                {-43, -57, -64, -69, -73, -67, -63, -45},
                {-41, -49, -59, -60, -63, -52, -50, -34}
            };

            Stopwatch sw = new Stopwatch();
            //sw.Start();
            //for (int i = 0; i < 5000000; i++)
            //{
            
            //    DCT.DCTAraiOptimized(testMatArai);
            //}
            sw.Stop();
            Console.WriteLine("Matrix Arai Optimized: Elapsed={0} \n", sw.Elapsed);

            Console.WriteLine("Matrix AraiOpimized endresult:");
            DCT.printMatrix(DCT.DCTAraiOptimized(testMatArai));
            //Console.ReadKey();

        }
        public void loopTestAraibetter()
        {
            float[] testMatArai = new float[64]
            {
                -76, -73, -67, -62, -58, -67, -64, -55,
                -65, -69, -73, -38, -19, -43, -59, -56,
                -66, -69, -60, -15, 16, -24, -62, -55,
                -65, -70, -57, -6, 26, -22, -58, -59,
                -61, -67, -60, -24, -2, -40, -60, -58,
                -49, -63, -68, -58, -51, -60, -70, -53,
                -43, -57, -64, -69, -73, -67, -63, -45,
                -41, -49, -59, -60, -63, -52, -50, -34
            };

            //int count = 10;
            //float[][] testArie = new float[65536][];
            //for (int i = 0; i < 65536; i++)
            //{
            //    testArie[i] = testMatArai;
            //}
            //Stopwatch sw = new Stopwatch();
            //for (int i = 0; i < count; i++)
            //{
            //sw.Start();
            //    DCT.araiAranger(testArie);
            //sw.Stop();
            //}
            //var time=mittelwertZeit(sw.Elapsed,count);
            //Console.WriteLine($"Matrix Arai Optimized:{time} ");

            //for (int i = 10; i < 10000; i=i*10)
            //{
            //    araitimer(testArie, i, count);
            //}
            //Stopwatch sw2 = new Stopwatch();        

            DCT.printArray(DCT.DCTAraiOptimizedrly2(testMatArai));

            Console.ReadKey();
            }
        public void araitimer(float[][] testArie, int tasks, int count)
        {
            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < count; i++)
            {               
                sw.Start();
                DCT.taskSeperater(testArie, tasks);
            sw.Stop();
            }
            var time = mittelwertZeit(sw.Elapsed, count);
            Console.WriteLine($"Matrix Arai Optimized:{ time} Count {tasks}");

            Stopwatch sw2 = new Stopwatch();
        }

        public TimeSpan mittelwertZeit(TimeSpan input, int count)
            {
            var time = input.Ticks / count;
            return new TimeSpan(time);
            }
            
        public float[][] Bilderaufteilen(float[] input, int maxx, int maxy)
        {
            float[][] returnarray = new float[(maxx * maxy) / 64][];
            for (int i = 0; i < returnarray.Length; i++)
            {
                returnarray[i] = new float[64];
            }
            int xcount = 0;
            int ycount = 0;
            int reihe = maxx/8;
            int count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                returnarray[(count * reihe) + xcount][i % 8+(ycount*8)] = input[i];
                if (i % 8 == 7&&i!=0)
                {
                    xcount++;
                }
                if (i % maxx == 255 && i != 1)
                {
                    ycount++;
                    xcount = 0;
                }
                if (ycount == 8)
                {
                    ycount = 0;
                    count++;
                }

            }
            return returnarray;
        }

        public float[] CombineBlocksToPicture(float[][] input, int maxX, int maxY)
        {
            float[] output = new float[maxX * maxY];
            int numberOfBlocks = input.Length;
            int blocksPerRow = maxX / 8;
            
            for (int i = 0; i < numberOfBlocks; i++)
            {
                for (int c = 0; c < 64; c++)
                {
                    output[(i * 8) + ((i / blocksPerRow) * blocksPerRow * 64) + ((c / 8) * maxX) + (c % 8)] = input[i][c];
                }
            }

            return output;
        }

        public float[,] DCTBench()
        {
            float mx = 256;
            float my = 256;

            float pixels = mx*my;
            float[,] testmatrix = new float[256, 256];

            // loops for (mx + my*8) % 256;
            for (int i = 0; i < mx; i += 8)
            {
                for (int j = 0; j < 256; j+= (int)mx * 8)
                {
                    testmatrix[i, j] = 0;
                }
            }

            return testmatrix;
        }

        public void PerformanceTest()
        {
            //Generate test picture as onedimensional array and fill it with the correct values
            float[] testValues = new float[65536];

            for (int i = 0; i < 65536; i++)
            {
                testValues[i] = (i % 256 + (i / 256) * 8) % 256;
            }

            Stopwatch watch = new Stopwatch();
            Stopwatch recordTime = new Stopwatch();
            long recordArai = 100000000;
            long recordDCT = 100000000;

            //Arai test
            watch.Start();
            while (watch.ElapsedMilliseconds < 10000)
            {
                recordTime.Start();

                float[][] listOfBlocks = Bilderaufteilen(testValues, 256, 256);

                listOfBlocks = DCT.taskSeperater(listOfBlocks, 100);

                recordTime.Stop();

                if (recordTime.ElapsedTicks < recordArai)
                {
                    recordArai = recordTime.ElapsedTicks;
                }

                recordTime.Reset();
            }
            watch.Reset();

            //DCT test
            watch.Start();
            while (watch.ElapsedMilliseconds < 10000)
            {
                recordTime.Start();

                float[][] listOfBlocks = Bilderaufteilen(testValues, 256, 256);
                
                //TODO: INSERT READING ONEDIMENSIONAL ARRAY INTO 8X8 BLOCKS

                //TODO: INSERT OPTMIZED LOGIC

                recordTime.Stop();

                if (recordTime.ElapsedTicks < recordDCT)
                {
                    recordDCT = recordTime.ElapsedTicks;
                }

                recordTime.Reset();
            }
            watch.Reset();

            Console.WriteLine("Arai Record Time: ={0} \n", recordArai);
            Console.WriteLine("DCT Record Time: ={0} \n", recordDCT);
            Console.WriteLine("Values represented in Ticks (100 Nanoseconds)");

        }
    }





}
