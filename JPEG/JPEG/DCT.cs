using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEG
{
    class DCT
    {
        // fixed value for 8x8 Matrix
        static int n = 8;

        // return value for Tests 
        public static float[,] DCTdirect(float[,] Matrix8init)
        {
            float[,] Matrix8res = new float[n, n];
            float row;
            float rowP;
            float column;
            float columnP;
            float subtotal;

            float oneSquare = (float) (1.0f / Math.Sqrt(2.0f));
            float doubleN = 2.0f * n;
            float halfN = 2.0f / n;

            //loop for i rows
            for (int i = 0; i < n; i++)
            {
                rowP = i * (float)Math.PI;
                // C(n) condition
                row = i == 0.0f ? oneSquare : 1.0f;

                //loop for j columns
                for (int j = 0; j < n; j++)
                {
                    columnP = j * (float)Math.PI;
                    // C(n) condition
                    column = j == 0.0f ? oneSquare : 1.0f;

                    subtotal = 0f;
                    //loop for x
                    for (int x = 0; x < n; x++)
                    {
                        float doubleX = 2.0f * x;
                        //loop for y
                        for (int y = 0; y < n; y++)
                        {
                            subtotal += (float)(Matrix8init[x, y] * Math.Cos(((doubleX + 1.0f) * rowP) / doubleN) * Math.Cos((((2.0f * y) + 1.0f) * columnP) / doubleN));
                        } //end loop y
                    } // end loop x
                    Matrix8res[i, j] = halfN * row * column * subtotal;
                } // end loop j
            } // end loop i
            return Matrix8res;
        }

        public static float[,] IDCTdirect(float[,] Matrix8init)
        {
            float[,] Matrix8res = new float[n, n];
            float row;
            float rowX;
            float column;
            float columnY;
            float subtotal;

            float oneSquare = (float)(1.0f / Math.Sqrt(2.0f));
            float doubleN = 2.0f * n;
            float halfN = 2.0f / n;

            for (int x = 0; x < n; x++)
            {
                rowX = ((2.0f * x) + 1) / doubleN;

                for (int y = 0; y < n; y++)
                {
                    columnY = ((2.0f * y) + 1) / doubleN;

                    //resets subtotal to 0
                    subtotal = 0f;
                    for (int i = 0; i < n; i++)
                    {
                        // C(n) condition
                        row = i == 0.0f ? oneSquare : 1.0f;

                        for (int j = 0; j < n; j++)
                        {
                            // C(n) condition
                            column = j == 0.0f ? oneSquare : 1.0f;

                            subtotal += (float)(Matrix8init[i, j] * row * column * Math.Cos(rowX * i * Math.PI) * Math.Cos(columnY * j * Math.PI));
                        } //end loop j
                    } // end loop i
                    Matrix8res[x, y] = halfN * subtotal;
                }// end loop y
            } // end loop x
            return Matrix8res;
        }

        public static float[,] DCTseparated(float[,] Matrix8init)
        {
            float[,] Matrix8res = new float[n, n];
            float[,] Matrix8final = new float[n, n];
            float[,] Matrix8A = new float[n, n];
            float temp;

            float oneSquare = (float)(1.0f / Math.Sqrt(2.0f));
            float doubleN = 2.0f * n;
            float halfN = 2.0f / n;

            for (int k = 0; k < n; k++)
            {
                // C(n) condition
                temp = k == 0.0f ? oneSquare : 1.0f;

                for (int nS = 0; nS < n; nS++)
                {
                    //fills row and column
                    Matrix8A[k, nS] = (float)(temp * Math.Sqrt(halfN) * Math.Cos(((2.0f * nS) + 1.0f) * ((k * Math.PI) / doubleN)));
                }
            } // end Matrix8A fill

            // Y = AX: multiplicates the filled A Matrix with the given X Matrix
            // Y = AXAT: multiplicates AX with the transposed second Matrix8AT
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        Matrix8res[i, j] += Matrix8A[i, k] * Matrix8init[k, j];
                    }
                }
            } // end Y = AX

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        //multiplicatin with transposed matrix8A
                        Matrix8final[i, j] += Matrix8res[i, k] * Matrix8A[j, k];
                    }
                }
            } // end Y = AXAT

            return Matrix8final;
        }

        public static float[,] DCTArai(float[,] Matrix8init)
        {
            float[,] Matrix8Arai = new float[n, n];

            float s0 = SMethod(0);
            float s1 = SMethod(1);
            float s2 = SMethod(2);
            float s3 = SMethod(3);
            float s4 = SMethod(4);
            float s5 = SMethod(5);
            float s6 = SMethod(6);
            float s7 = SMethod(7);

            float a1 = CMethod(4);
            float a2 = CMethod(2) - CMethod(6);
            float a3 = CMethod(4);
            float a4 = CMethod(6) + CMethod(2);
            float a5 = CMethod(6);

            float[] phase1 = new float[n];
            float[] phase2 = new float[n];
            float[] phase3 = new float[n];
            float[] phase4 = new float[n];
            float[] phase5 = new float[n];
            float[] phase6 = new float[n];

            for (int pointer = 0; pointer < 8; pointer++)
            {
                //phase 1
                phase1[0] = Matrix8init[pointer, 0] + Matrix8init[pointer, 7];
                phase1[1] = Matrix8init[pointer, 1] + Matrix8init[pointer, 6];
                phase1[2] = Matrix8init[pointer, 2] + Matrix8init[pointer, 5];
                phase1[3] = Matrix8init[pointer, 3] + Matrix8init[pointer, 4];
                phase1[4] = Matrix8init[pointer, 3] - Matrix8init[pointer, 4];
                phase1[5] = Matrix8init[pointer, 2] - Matrix8init[pointer, 5];
                phase1[6] = Matrix8init[pointer, 1] - Matrix8init[pointer, 6];
                phase1[7] = Matrix8init[pointer, 0] - Matrix8init[pointer, 7];

                //phase 2
                phase2[0] = phase1[0] + phase1[3];
                phase2[1] = phase1[1] + phase1[2];
                phase2[2] = phase1[1] - phase1[2];
                phase2[3] = phase1[0] - phase1[3];
                phase2[4] = -(phase1[4] + phase1[5]); //look butterfly diagram
                phase2[5] = phase1[5] + phase1[6];
                phase2[6] = phase1[6] + phase1[7];
                phase2[7] = phase1[7];

                //phase 3
                phase3[0] = phase2[0] + phase2[1];
                phase3[1] = phase2[0] - phase2[1];
                phase3[2] = phase2[2] + phase2[3];
                phase3[3] = phase2[3];
                phase3[4] = phase2[4];
                phase3[5] = phase2[5];
                phase3[6] = phase2[6];
                phase3[7] = phase2[7];

                //phase 4
                phase4[0] = phase3[0];
                phase4[1] = phase3[1];
                phase4[2] = phase3[2] * a1;
                phase4[3] = phase3[3];
                phase4[4] = -(phase3[4] * a2) + -((phase3[4] + phase3[6]) * a5);
                phase4[5] = phase3[5] * a3;
                phase4[6] = phase3[6] * a4 + -((phase3[4] + phase3[6]) * a5);
                phase4[7] = phase3[7];

                //phase 5
                phase5[0] = phase4[0];
                phase5[1] = phase4[1];
                phase5[2] = phase4[2] + phase4[3];
                phase5[3] = phase4[3] - phase4[2];
                phase5[4] = phase4[4];
                phase5[5] = phase4[5] + phase4[7];
                phase5[6] = phase4[6];
                phase5[7] = phase4[7] - phase4[5];

                //phase 6
                phase6[0] = phase5[0] * s0;
                phase6[1] = phase5[1] * s4;
                phase6[2] = phase5[2] * s2;
                phase6[3] = phase5[3] * s6;
                phase6[4] = (phase5[4] + phase5[7]) * s5;
                phase6[5] = (phase5[5] + phase5[6]) * s1;
                phase6[6] = (phase5[5] - phase5[6]) * s7;
                phase6[7] = (phase5[7] - phase5[4]) * s3;

                Matrix8Arai[pointer, 0] = phase6[0];
                Matrix8Arai[pointer, 1] = phase6[5];
                Matrix8Arai[pointer, 2] = phase6[2];
                Matrix8Arai[pointer, 3] = phase6[7];
                Matrix8Arai[pointer, 4] = phase6[1];
                Matrix8Arai[pointer, 5] = phase6[4];
                Matrix8Arai[pointer, 6] = phase6[3];
                Matrix8Arai[pointer, 7] = phase6[6];
            }

            for (int pointer = 0; pointer < 8; pointer++)
            {
                //phase 1
                phase1[0] = Matrix8Arai[0, pointer] + Matrix8Arai[7, pointer];
                phase1[1] = Matrix8Arai[1, pointer] + Matrix8Arai[6, pointer];
                phase1[2] = Matrix8Arai[2, pointer] + Matrix8Arai[5, pointer];
                phase1[3] = Matrix8Arai[3, pointer] + Matrix8Arai[4, pointer];
                phase1[4] = Matrix8Arai[3, pointer] - Matrix8Arai[4, pointer];
                phase1[5] = Matrix8Arai[2, pointer] - Matrix8Arai[5, pointer];
                phase1[6] = Matrix8Arai[1, pointer] - Matrix8Arai[6, pointer];
                phase1[7] = Matrix8Arai[0, pointer] - Matrix8Arai[7, pointer];

                //phase 2
                phase2[0] = phase1[0] + phase1[3];
                phase2[1] = phase1[1] + phase1[2];
                phase2[2] = phase1[1] - phase1[2];
                phase2[3] = phase1[0] - phase1[3];
                phase2[4] = -(phase1[4] + phase1[5]); //look butterfly diagram
                phase2[5] = phase1[5] + phase1[6];
                phase2[6] = phase1[6] + phase1[7];
                phase2[7] = phase1[7];

                //phase 3
                phase3[0] = phase2[0] + phase2[1];
                phase3[1] = phase2[0] - phase2[1];
                phase3[2] = phase2[2] + phase2[3];
                phase3[3] = phase2[3];
                phase3[4] = phase2[4];
                phase3[5] = phase2[5];
                phase3[6] = phase2[6];
                phase3[7] = phase2[7];

                //phase 4
                phase4[0] = phase3[0];
                phase4[1] = phase3[1];
                phase4[2] = phase3[2] * a1;
                phase4[3] = phase3[3];
                phase4[4] = -(phase3[4] * a2) + -((phase3[4] + phase3[6]) * a5);
                phase4[5] = phase3[5] * a3;
                phase4[6] = phase3[6] * a4 + -((phase3[4] + phase3[6]) * a5);
                phase4[7] = phase3[7];

                //phase 5
                phase5[0] = phase4[0];
                phase5[1] = phase4[1];
                phase5[2] = phase4[2] + phase4[3];
                phase5[3] = phase4[3] - phase4[2];
                phase5[4] = phase4[4];
                phase5[5] = phase4[5] + phase4[7];
                phase5[6] = phase4[6];
                phase5[7] = phase4[7] - phase4[5];

                //phase 6
                phase6[0] = phase5[0] * s0;
                phase6[1] = phase5[1] * s4;
                phase6[2] = phase5[2] * s2;
                phase6[3] = phase5[3] * s6;
                phase6[4] = (phase5[4] + phase5[7]) * s5;
                phase6[5] = (phase5[5] + phase5[6]) * s1;
                phase6[6] = (phase5[5] - phase5[6]) * s7;
                phase6[7] = (phase5[7] - phase5[4]) * s3;

                Matrix8Arai[0, pointer] = phase6[0];
                Matrix8Arai[1, pointer] = phase6[5];
                Matrix8Arai[2, pointer] = phase6[2];
                Matrix8Arai[3, pointer] = phase6[7];
                Matrix8Arai[4, pointer] = phase6[1];
                Matrix8Arai[5, pointer] = phase6[4];
                Matrix8Arai[6, pointer] = phase6[3];
                Matrix8Arai[7, pointer] = phase6[6];
            }

            return Matrix8Arai;
        }

        public static float CMethod(int num)
        {
            return (float)Math.Cos(num * Math.PI / 16.0f);
        }

        public static float SMethod(int num)
        {
            if (num == 0)
                return (1 / (2 * (float)Math.Sqrt(2.0f)));
            else
                return (1 / (4 * CMethod(num)));
        }

        public static float[,] DCTAraiOptimized(float[,] Matrix8init)
        {
            float[,] Matrix8Arai = new float[n, n];

            float s0 = 0.3535533f;
            float s1 = SMethodOptimized(1f);
            float s2 = SMethodOptimized(2f);
            float s3 = SMethodOptimized(3f);
            float s4 = SMethodOptimized(4f);
            float s5 = SMethodOptimized(5f);
            float s6 = SMethodOptimized(6f);
            float s7 = SMethodOptimized(7f);

            float a1 = CMethodOptimized(4f);
            float a2 = CMethodOptimized(2f) - CMethodOptimized(6f);
            float a3 = CMethodOptimized(4f);
            float a4 = CMethodOptimized(6f) + CMethodOptimized(2f);
            float a5 = CMethodOptimized(6f);

            float[] phase1 = new float[n];
            float[] phase2 = new float[n];
            float[] phase3 = new float[n];
            float[] phase4 = new float[n];
            float[] phase5 = new float[n];

            for (int pointer = 0; pointer < 8; pointer++)
            {
                //phase 1
                phase1[0] = Matrix8init[pointer, 0] + Matrix8init[pointer, 7];
                phase1[1] = Matrix8init[pointer, 1] + Matrix8init[pointer, 6];
                phase1[2] = Matrix8init[pointer, 2] + Matrix8init[pointer, 5];
                phase1[3] = Matrix8init[pointer, 3] + Matrix8init[pointer, 4];
                phase1[4] = Matrix8init[pointer, 3] - Matrix8init[pointer, 4];
                phase1[5] = Matrix8init[pointer, 2] - Matrix8init[pointer, 5];
                phase1[6] = Matrix8init[pointer, 1] - Matrix8init[pointer, 6];
                phase1[7] = Matrix8init[pointer, 0] - Matrix8init[pointer, 7];

                //phase 2
                phase2[0] = phase1[0] + phase1[3];
                phase2[1] = phase1[1] + phase1[2];
                phase2[2] = phase1[1] - phase1[2];
                phase2[3] = phase1[0] - phase1[3];
                phase2[4] = -(phase1[4] + phase1[5]); //look butterfly diagram
                phase2[5] = phase1[5] + phase1[6];
                phase2[6] = phase1[6] + phase1[7];
                //phase2[7] = phase1[7];

                //phase 3
                phase3[0] = phase2[0] + phase2[1];
                phase3[1] = phase2[0] - phase2[1];
                phase3[2] = phase2[2] + phase2[3];
                //phase3[3] = phase2[3];
                //phase3[4] = phase2[4];
                //phase3[5] = phase2[5];
                //phase3[6] = phase2[6];
                //phase3[7] = phase2[7];

                //phase 4
                //phase4[0] = phase3[0];
                //phase4[1] = phase3[1];
                phase4[2] = phase3[2] * a1;
                phase4[3] = phase2[3];
                phase4[4] = -(phase2[4] * a2) + -((phase2[4] + phase2[6]) * a5);
                phase4[5] = phase2[5] * a3;
                phase4[6] = phase2[6] * a4 + -((phase2[4] + phase2[6]) * a5);
                //phase4[7] = phase3[7];

                //phase 5
                //phase5[0] = phase4[0];
                //phase5[1] = phase4[1];
                phase5[2] = phase4[2] + phase4[3];
                phase5[3] = phase4[3] - phase4[2];
                //phase5[4] = phase4[4];
                phase5[5] = phase4[5] + phase1[7];
                //phase5[6] = phase4[6];
                phase5[7] = phase1[7] - phase4[5];

                Matrix8Arai[pointer, 0] = phase3[0] * s0;
                Matrix8Arai[pointer, 1] = (phase5[5] + phase4[6]) * s1;
                Matrix8Arai[pointer, 2] = phase5[2] * s2;
                Matrix8Arai[pointer, 3] = phase5[3] * s6;
                Matrix8Arai[pointer, 4] = phase3[1] * s4;
                Matrix8Arai[pointer, 5] = (phase4[4] + phase5[7]) * s5;
                Matrix8Arai[pointer, 6] = (phase5[7] - phase4[4]) * s3;
                Matrix8Arai[pointer, 7] = (phase5[5] - phase4[6]) * s7;
            }

            for (int pointer = 0; pointer < 8; pointer++)
            {
                //phase 1
                phase1[0] = Matrix8Arai[0, pointer] + Matrix8Arai[7, pointer];
                phase1[1] = Matrix8Arai[1, pointer] + Matrix8Arai[6, pointer];
                phase1[2] = Matrix8Arai[2, pointer] + Matrix8Arai[5, pointer];
                phase1[3] = Matrix8Arai[3, pointer] + Matrix8Arai[4, pointer];
                phase1[4] = Matrix8Arai[3, pointer] - Matrix8Arai[4, pointer];
                phase1[5] = Matrix8Arai[2, pointer] - Matrix8Arai[5, pointer];
                phase1[6] = Matrix8Arai[1, pointer] - Matrix8Arai[6, pointer];
                phase1[7] = Matrix8Arai[0, pointer] - Matrix8Arai[7, pointer];

                //phase 2
                phase2[0] = phase1[0] + phase1[3];
                phase2[1] = phase1[1] + phase1[2];
                phase2[2] = phase1[1] - phase1[2];
                phase2[3] = phase1[0] - phase1[3];
                phase2[4] = -(phase1[4] + phase1[5]); //look butterfly diagram
                phase2[5] = phase1[5] + phase1[6];
                phase2[6] = phase1[6] + phase1[7];
                //phase2[7] = phase1[7];

                //phase 3
                phase3[0] = phase2[0] + phase2[1];
                phase3[1] = phase2[0] - phase2[1];
                phase3[2] = phase2[2] + phase2[3];
                //phase3[3] = phase2[3];
                //phase3[4] = phase2[4];
                //phase3[5] = phase2[5];
                //phase3[6] = phase2[6];
                //phase3[7] = phase2[7];

                //phase 4
                //phase4[0] = phase3[0];
                //phase4[1] = phase3[1];
                phase4[2] = phase3[2] * a1;
                phase4[3] = phase2[3];
                phase4[4] = -(phase2[4] * a2) + -((phase2[4] + phase2[6]) * a5);
                phase4[5] = phase2[5] * a3;
                phase4[6] = phase2[6] * a4 + -((phase2[4] + phase2[6]) * a5);
                //phase4[7] = phase3[7];

                //phase 5
                //phase5[0] = phase4[0];
                //phase5[1] = phase4[1];
                phase5[2] = phase4[2] + phase4[3];
                phase5[3] = phase4[3] - phase4[2];
                //phase5[4] = phase4[4];
                phase5[5] = phase4[5] + phase1[7];
                //phase5[6] = phase4[6];
                phase5[7] = phase1[7] - phase4[5];

                Matrix8Arai[pointer, 0] = phase3[0] * s0;
                Matrix8Arai[pointer, 1] = (phase5[5] + phase4[6]) * s1;
                Matrix8Arai[pointer, 2] = phase5[2] * s2;
                Matrix8Arai[pointer, 3] = phase5[3] * s6;
                Matrix8Arai[pointer, 4] = phase3[1] * s4;
                Matrix8Arai[pointer, 5] = (phase4[4] + phase5[7]) * s5;
                Matrix8Arai[pointer, 6] = (phase5[7] - phase4[4]) * s3;
                Matrix8Arai[pointer, 7] = (phase5[5] - phase4[6]) * s7;
            }

            return Matrix8Arai;
        }

        public static float CMethodOptimized(float k)
        {
            return (float)Math.Cos(k * 0.1963495f);
        }

        public static float SMethodOptimized(float k)
        {
                return 0.25f * CMethodOptimized(k);
        }

        public static void printMatrix(float[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            String s = "";

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (col != 0) s += " | ";
                    s += string.Format("{0,8:####0.00}", matrix[row, col]);
                }

                s += "\n";
            }

            Console.WriteLine(s);
        }
    }
}
