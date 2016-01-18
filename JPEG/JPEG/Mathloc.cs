
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JPEG
{
    class Mathloc
    {
        public Object[] ZickZackScan(Object[,] InputByteArray)
        {
            var ymax = InputByteArray.GetLength(0);
            var xmax = InputByteArray.GetLength(1);
            var x = 0;
            var y = 0;
            var indexMax = ymax * xmax;
            Object[] returnArray = new Object[indexMax];
            var index = 0;
            var zustand = 0;
            var kpold = 0;
            var kp = 0;

            returnArray[index] = InputByteArray[y,x];
            while (index < indexMax)
            {
                kp = kpold;
                if (zustand == 0)
                {
                    if (y == 0)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        kpold = kp + 1;
                        while (kp >= 0)
                        {
                            x--;
                            y++;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }

                    }
                    else if (x == 0)
                    {
                        y++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        kpold = kp + 1;
                        while (kp >= 0)
                        {
                            x++;
                            y--;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                    }
                    if (ymax == y)
                    {
                        zustand = 1;
                    }
                    else if (xmax == x)
                    {
                        zustand = 1;
                    }
                    else if (x + 1 == xmax && y == 0)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        kpold++;
                        kp = kpold;
                        zustand++;
                        while (kp > 0)
                        {
                            x--;
                            y++;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                    }
                    else if (y + 1 == ymax && x == 0)
                    {
                        y++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        kpold++;
                        kp = kpold;
                        zustand++;
                        while (kp > 0)
                        {
                            y--;
                            x++;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                    }
                }
                else if (zustand == 1 && xmax > ymax)
                {
                    if (xmax == x)
                    {
                        zustand = 2;
                    }
                    else if (x + 1 == xmax && y == 0)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        zustand++;
                    }
                    else if (y + 1 == ymax && x == 0)
                    {
                        y++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        zustand++;
                    }
                    else if (x == 0)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        while (kp > 0)
                        {
                            y--;
                            x++;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                    }
                    else if (y == 0)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        while (kp > 0)
                        {
                            y++;
                            x--;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                    }
                    else if (x == xmax)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        while (kp > 0)
                        {
                            y++;
                            x--;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                    }
                    else if (y == ymax)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        while (kp > 0)
                        {
                            y--;
                            x++;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                    }
                }
                else if (zustand == 1 && xmax < ymax)
                {
                    if (ymax == y)
                    {
                        zustand = 2;
                    }
                    else if (x + 1 == xmax && y == 0)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        zustand++;
                    }
                    else if (y + 1 == ymax && x == 0)
                    {
                        y++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        zustand++;
                    }
                    else if (x == 0)
                    {
                        y++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        while (kp > 0)
                        {
                            y--;
                            x++;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                    }
                    else if (y == 0)
                    {
                        y++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        while (kp > 0)
                        {
                            y++;
                            x--;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                    }
                    else if (x == xmax)
                    {
                        y++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        while (kp > 0)
                        {
                            y++;
                            x--;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                    }
                    else if (y == ymax)
                    {
                        y++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        while (kp > 0)
                        {
                            y--;
                            x++;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                    }
                }
                else if (zustand == 2)
                {
                    if (x == xmax && y == 0 && (xmax % 2) != 1)
                    {
                        y++;
                        returnArray[index] = InputByteArray[y, x];
                        kp--;
                        kpold--;
                        index++;
                    }
                    else if (y == ymax && x == 0 && (ymax % 2) == 1)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        kp--;
                        kpold--;
                    }
                    if (x == xmax)
                    {
                        while (kp > 0)
                        {
                            y++;
                            x--;
                            returnArray[index] = InputByteArray[y, x];
                            kp--;
                            index++;
                        }
                        x++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        kpold = kpold - 1;
                    }
                    else if (y == ymax)
                    {
                        while (kp > 0)
                        {
                            y--;
                            x++;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                        y++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        kpold = kpold - 1;
                    }

                }
            }
            return (returnArray);
        }
        public byte[] ZickZackScanByte(byte[,] InputByteArray)
        {
            var ymax = InputByteArray.GetLength(0)-1;
            var xmax = InputByteArray.GetLength(1)-1;
            var x = 0;
            var y = 0;
            var indexMax = (ymax+1) * (xmax+1);
            byte[] returnArray = new byte[indexMax];
            var index = 0;
            var zustand = 0;
            var kpold = 0;
            var kp = 0;

            returnArray[index] = InputByteArray[y,x];
            index++;
            while (index < indexMax)
            {
                kp = kpold;
                if (zustand == 0)
                {
                    if (y == 0)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        kpold = kp + 1;
                        while (kp >= 0)
                        {
                            x--;
                            y++;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }

                    }                    
                    else if (x == 0)
                    {
                        y++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        kpold = kp + 1;
                        while (kp >= 0)
                        {
                            x++;
                            y--;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                    }
                    if (ymax == y)
                    {
                        zustand = 1;
                    }
                    else if (xmax == x)
                    {
                        zustand = 1;
                    }
                    else if (x + 1 == xmax && y == 0)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        kpold++;
                        kp = kpold; 
                        zustand++;
                        while (kp > 0)
                        {
                            x--;
                            y++;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                    }
                    else if (y + 1 == ymax && x == 0)
                    {
                        y++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        kpold++;
                        kp=kpold;
                        zustand++;
                        while (kp > 0)
                        {
                            y--;
                            x++;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                    }
                }
                else if (zustand == 1&&xmax>ymax)
                {
                    if (xmax == x)
                    {
                        zustand = 2;
                    }
                    else if (x + 1 == xmax && y == 0)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        zustand++;
                    }
                    else if (y + 1 == ymax && x == 0)
                    {
                        y++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        zustand++;
                    }
                    else if (x == 0)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y,x];
                        index++;
                        while (kp > 0)
                        {
                            y--;
                            x++;
                            returnArray[index] = InputByteArray[y,x];
                            index++;
                            kp--;
                        }
                    }
                    else if (y == 0)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y,x];
                        index++;
                        while (kp > 0)
                        {
                            y++;
                            x--;
                            returnArray[index] = InputByteArray[y,x];
                            index++;
                            kp--;
                        }
                    }
                    else if (x == xmax)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        while (kp > 0)
                        {
                            y++;
                            x--;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                    }
                    else if (y == ymax)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        while (kp > 0)
                        {
                            y--;
                            x++;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                    }                    
                }
                else if (zustand == 1 && xmax < ymax)
                {
                    if (ymax == y)
                    {
                        zustand = 2;
                    }
                    else if (x + 1 == xmax && y == 0)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        zustand++;
                    }
                    else if (y + 1 == ymax && x == 0)
                    {
                        y++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        zustand++;
                    }
                    else if (x == 0)
                    {
                        y++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        while (kp > 0)
                        {
                            y--;
                            x++;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                    }
                    else if (y == 0)
                    {
                        y++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        while (kp > 0)
                        {
                            y++;
                            x--;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                    }
                    else if (x == xmax)
                    {
                        y++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        while (kp > 0)
                        {
                            y++;
                            x--;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                    }
                    else if (y == ymax)
                    {
                        y++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        while (kp > 0)
                        {
                            y--;
                            x++;
                            returnArray[index] = InputByteArray[y, x];
                            index++;
                            kp--;
                        }
                    }
                }
                else if (zustand == 2)
                {
                    if (x == xmax && y == 0 && (xmax % 2) != 1)
                    {
                        y++;
                        returnArray[index] = InputByteArray[y, x];
                        kp--;
                        kpold--;
                        index++;
                    }
                    else if (y == ymax && x == 0 && (ymax % 2) == 1)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        kp--;
                        kpold--;
                    }
                    if (x == xmax)
                    {
                        while (kp > 0)
                        {
                            y++;
                            x--;
                            returnArray[index] = InputByteArray[y, x];                            
                            kp--;
                            index++;
                        }
                        x++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        kpold = kpold - 1;
                    }
                    else if (y == ymax)
                    {
                        while (kp > 0)
                        {
                            y--;
                            x++;
                            returnArray[index] = InputByteArray[y,x];
                            index++;
                            kp--;
                        }
                        y++;
                        returnArray[index] = InputByteArray[y,x];
                        index++;
                        kpold = kpold - 1;
                    }

                }                
            }
            return (returnArray);
        }


        public byte[] Zickzackdre(byte[,] InputByteArray)
        {
            byte[] returnarray = { 64 };
            returnarray[0]= InputByteArray[0,0];
            returnarray[1] = InputByteArray[1, 0];
            returnarray[2] = InputByteArray[0, 1];
            returnarray[3] = InputByteArray[0, 2];
            returnarray[4] = InputByteArray[1, 1];
            returnarray[5] = InputByteArray[2, 0];
            returnarray[6] = InputByteArray[3, 0];
            returnarray[7] = InputByteArray[2, 1];
            returnarray[8] = InputByteArray[1, 2];
            returnarray[9] = InputByteArray[0, 3];
            returnarray[10] = InputByteArray[0, 4];
            returnarray[11] = InputByteArray[1, 3];
            returnarray[12] = InputByteArray[2, 2];
            returnarray[13] = InputByteArray[3, 1];
            returnarray[14] = InputByteArray[4, 0];
            returnarray[15] = InputByteArray[5, 0];
            returnarray[16] = InputByteArray[4, 1];
            returnarray[17] = InputByteArray[3, 2];
            returnarray[18] = InputByteArray[2, 3];
            returnarray[19] = InputByteArray[1, 4];
            returnarray[20] = InputByteArray[0, 5];
            returnarray[21] = InputByteArray[0, 6];
            returnarray[22] = InputByteArray[1, 5];
            returnarray[23] = InputByteArray[2, 4];
            returnarray[24] = InputByteArray[3, 3];
            returnarray[25] = InputByteArray[4, 2];
            returnarray[26] = InputByteArray[5, 1];
            returnarray[27] = InputByteArray[6, 0];
            returnarray[28] = InputByteArray[7, 0];
            returnarray[29] = InputByteArray[6, 1];
            returnarray[30] = InputByteArray[5, 2];
            returnarray[31] = InputByteArray[4, 3];
            returnarray[32] = InputByteArray[3, 4];
            returnarray[33] = InputByteArray[2, 5];
            returnarray[34] = InputByteArray[1, 6];
            returnarray[35] = InputByteArray[0, 7];
            returnarray[36] = InputByteArray[1, 7];
            returnarray[37] = InputByteArray[2, 6];
            returnarray[38] = InputByteArray[3, 5];
            returnarray[39] = InputByteArray[4, 4];
            returnarray[40] = InputByteArray[5, 3];
            returnarray[41] = InputByteArray[6, 2];
            returnarray[42] = InputByteArray[7, 1];
            returnarray[43] = InputByteArray[7, 2];
            returnarray[44] = InputByteArray[6, 3];
            returnarray[45] = InputByteArray[5, 4];
            returnarray[46] = InputByteArray[4, 5];
            returnarray[47] = InputByteArray[3, 6];
            returnarray[48] = InputByteArray[2, 7];
            returnarray[49] = InputByteArray[3, 7];
            returnarray[50] = InputByteArray[4, 6];
            returnarray[51] = InputByteArray[5, 5];
            returnarray[52] = InputByteArray[6, 4];
            returnarray[53] = InputByteArray[7, 3];
            returnarray[54] = InputByteArray[7, 4];
            returnarray[55] = InputByteArray[6, 5];
            returnarray[56] = InputByteArray[5, 6];
            returnarray[57] = InputByteArray[4 ,7];
            returnarray[58] = InputByteArray[5, 7];
            returnarray[59] = InputByteArray[6, 6];
            returnarray[60] = InputByteArray[7, 5];
            returnarray[61] = InputByteArray[7, 6];
            returnarray[62] = InputByteArray[6, 7];
            returnarray[63] = InputByteArray[7, 7];
            return returnarray;
        }
        /// <summary>
        /// nimmt input byte arry schreibt es in ein double array um und führt die umrechnung von RGB nach YUV aus
        /// daraufhin wird die das double array in ein byte Array umgewandelt und zurückgegeben 
        /// </summary>
        /// <param name="inputvector"></param>
        /// <returns></returns>
        public byte[] RGBToYUV(byte[] inputvector)
        {
            double[] vector = Array.ConvertAll(inputvector, b => Convert.ToDouble(b));
            double[,] RGBToYUVFullRangeMa = { { 0.299, 0.587, 0.114 }, { -0.169, -0.331, 0.500 }, { 0.500, -0.419, -0.081 } };
            double[] RGBToYUVFullRangeVec = { 0.0, 128.0, 128.0 };
            var m = Matrix<double>.Build.DenseOfArray(RGBToYUVFullRangeMa);
            var v = Vector<double>.Build.DenseOfArray(RGBToYUVFullRangeVec);
            var v2 = Vector<double>.Build.DenseOfArray(vector);

            var RGBTOYUVFullRange = v + m * v2;
            var resconrgbtoyuv = Array.ConvertAll(RGBTOYUVFullRange.ToArray(), b => Convert.ToByte(roundmaxColorRange(b)));
            return (resconrgbtoyuv);
        }


        /// <summary>
        /// nimmt input byte arry schreibt es in ein double array um und führt die umrechnung von YUV nach RGB aus
        /// daraufhin wird die das double array in ein byte Array umgewandelt und zurückgegeben 
        /// </summary>
        /// <param name="inputvector"></param>
        /// <returns></returns>
        public byte[] YUVToRGB(byte[] inputvector)
        {
            double[] vector = Array.ConvertAll(inputvector, b => Convert.ToDouble(b));
            double[,] FullRangeYUVToRGBMa = { { 1, 0, 1.4 }, { 1, -0.343, -0.711 }, { 1, 1.765, 0 } };
            double[] FullRangeYUVToRGBVec = { 0, -128, -128 };
            var v = Vector<double>.Build.DenseOfArray(vector);
            var m = Matrix<double>.Build.DenseOfArray(FullRangeYUVToRGBMa);
            var v2 = Vector<double>.Build.DenseOfArray(FullRangeYUVToRGBVec);
            v = v + v2;
            var yuvfullrangetorgb = m * v;
            var resconyuvtorgb = Array.ConvertAll(yuvfullrangetorgb.ToArray(), b => Convert.ToByte(roundmaxColorRange(b)));
            return (resconyuvtorgb);
        }

        public double roundmaxColorRange(double input)
        {
            if (input>255)
            {
                return (255.0);
            }
            return (input);
        }
    }
}
