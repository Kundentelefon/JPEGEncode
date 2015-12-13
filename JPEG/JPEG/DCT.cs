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
        int n = 8;

        // return value for Tests 
        public float[,] DCTdirect(float[,] Matrix8init)
        {
            float[,] Matrix8res = new float[n,n];
            float row;
            float rowP;
            float column;
            float columnP;
            float subtotal;

            //loop for i rows
            for (int i = 0; i < n; i++)
            {
                rowP = i * (float)Math.PI;
                // C(n) condition
                if (i == 0)
                    row = (float) (1.0f / Math.Sqrt(2.0f));
                else
                    row = 1.0f;

                //loop for j columns
                for (int j = 0; j < n; j++)
                {
                    columnP = j * (float)Math.PI;
                    // C(n) condition
                    if (j == 0)
                        column = (float) (1.0f / Math.Sqrt(2.0f));
                    else
                        column = 1.0f;

                    //resets subtotal to 0
                    subtotal = 0f;

                    //loop for x
                    for (int x = 0; x < n; x++)
                    {
                        //loop for y
                        for (int y = 0; y < n; y++)
                        {
                            subtotal += (float) (Matrix8init[x,y] * Math.Cos(( ((2.0f * x) + 1.0f) * rowP) / (2.0f * n) ) * Math.Cos( ((2.0f * y) + 1.0f) * columnP) / (2.0f * n) );
                        } //end loop y
                    } // end loop x
                   Matrix8res[i, j] = 2.0f / n * row * column * subtotal;                   
                } // end loop j
            } // end loop i
            return Matrix8res;
        }

        public float[,] IDCTdirect(float[,] Matrix8init)
        {
            float[,] Matrix8res = new float[n, n];
            float row;
            float rowX;
            float column;
            float columnY;
            float subtotal;

            for (int x = 0; x < n; x++)
            {
                rowX = ((2.0f * x) + 1) / (2.0f * n);

                for(int y = 0; y < n; y++)
                {
                    columnY = ((2.0f * y) + 1) / (2.0f * n);
                    
                    //resets subtotal to 0
                    subtotal = 0f;

                    for (int i = 0; i < n; i++)
                    {
                        // C(n) condition
                        if (i == 0)
                            row = (float)(1.0f / Math.Sqrt(2.0f));
                        else
                            row = 1.0f;

                        for (int j = 0; j < n; j++)
                        {
                            // C(n) condition
                            if (i == 0)
                                column = (float)(1.0f / Math.Sqrt(2.0f));
                            else
                                column = 1.0f;

                            subtotal += (float)(Matrix8init[x, y] * row * column * Math.Cos(rowX* i* Math.PI) * Math.Cos(columnY* j* Math.PI) );
                        } //end loop j
                    } // end loop i
                    Matrix8res[x, y] = (2.0f / n) * subtotal;
                }// end loop y
            } // end loop x
            return Matrix8res;
        }

        public float[,] DCTseperated(float[,] Matrix8init)
        {
            float[,] Matrix8res = new float[n, n];
            float[,] Matrix8A = new float[n, n];
            //second Matrix for the transformation in AT matrix
            float[,] Matrix8AT = new float[n, n];
            float temp;
            float subtotal;

            for (int k = 0; k < n; k++)
            {
                // C(n) condition
                if (k == 0)
                    temp = (float) (1.0f / Math.Sqrt(2.0f));
                else
                    temp = 1.0f;

                for (int nS = 0; nS < n; nS++)
                {
                    //fills row and column
                    Matrix8A[k,nS] = (float) (temp * Math.Sqrt(2.0f / n) * Math.Cos( (2.0f * nS) + 1.0f) * ( (k * Math.PI) / (2.0f * n) ) );
                }
            } // end Matrix8A fill

            // Y = AX: multiplicates the filled A Matrix with the given X Matrix
            // Y = AXAT: multiplicates AX with the transformed second Matrix8AT
            for (int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    //resets the result place with 0
                    Matrix8res[i, j] = 0.0f;
                    for(int k = 0; k < n; k++)
                    {
                        //TODO: performance optimization?
                        Matrix8res[i, j] += Matrix8A[i, k] * Matrix8init[k, j];
                        //fills the second Matrix with the same result
                        Matrix8AT[i, j] = Matrix8res[i, j];
                        Matrix8res[i, j] += Matrix8A[j, k] * Matrix8AT[i, j];
                    }
                }
            } // end Y = AXAT
            return Matrix8res;
        }

        public float[,] DCTArai(float[,] Matrix8init)
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
            float a2 = CMethod(2) - CMethod(2);
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
                phase1[2] = Matrix8init[pointer, 2] + Matrix8init[pointer, 7];
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
                phase4[4] = (-phase3[4] * a2) + (phase3[4] + phase3[6]) * a5; // überpüfen
                phase4[5] = phase3[5] * a3;
                phase4[6] = phase3[6] * a4 + (phase3[4] + phase3[6]) * a5;
                phase4[7] = phase3[7];

                //phase 5
                phase5[0] = phase4[0];
                phase5[1] = phase4[1];
                phase5[2] = phase4[2] + phase4[3];
                phase5[3] = phase4[3] - phase4[2]; // überprüfen dangerous
                phase5[4] = phase4[4];
                phase5[5] = phase4[5] + phase4[7];
                phase5[6] = phase4[6];
                phase5[7] = phase4[7] - phase4[5];

                //phase 6
                phase6[0] = phase5[0] * s0;
                phase6[1] = phase5[1] * s4;
                phase6[2] = phase5[2] * s2;
                phase6[3] = phase5[3] * s6;
                phase6[4] = phase5[4] * s5;
                phase6[5] = (phase5[5] + phase5[6]) * s1;
                phase6[6] = (phase5[5] - phase5[6]) * s7;
                phase6[7] = (phase5[7] - phase5[4]) * s3;

                Matrix8Arai[pointer, 0] = phase6[0];
                Matrix8Arai[pointer, 1] = phase6[1];
                Matrix8Arai[pointer, 2] = phase6[2];
                Matrix8Arai[pointer, 3] = phase6[3];
                Matrix8Arai[pointer, 4] = phase6[4];
                Matrix8Arai[pointer, 5] = phase6[5];
                Matrix8Arai[pointer, 6] = phase6[6];
                Matrix8Arai[pointer, 7] = phase6[7];
            }
            return Matrix8Arai;
        }

        public float CMethod(int num)
        {
            return (float)Math.Cos(num * Math.PI / 16.0f);
        }

        public float SMethod(int num)
        {
            if (num == 0)
                return (1 / (2 * (float)Math.Sqrt(2.0f)));
            else
                return (1 / (4 * CMethod(num)));
        }

        public void printMatrix(float[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            String s = "";

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    s += matrix[row,col] + "\t";
                }

                s += "\n";
            }

            Console.WriteLine(s);
        }    
    }
}
