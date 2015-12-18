
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
