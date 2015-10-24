using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEG
{
    class Math
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
                    if (x == 0)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y,x];
                        index++;
                        kpold = kp + 1;
                        while (kp <= 0)
                        {
                            y++;
                            x--;
                            returnArray[index] = InputByteArray[y,x];
                            index++;
                        }
                    }
                    else if (y == 0)
                    {
                        y++;
                        returnArray[index] = InputByteArray[y,x];
                        index++;
                        kpold = kp + 1;
                        while (kp <= 0)
                        {
                            y--;
                            x++;
                            returnArray[index] = InputByteArray[y,x];
                            index++;
                        }

                    }
                    if (ymax == y)
                    {
                        zustand = 1;
                    }
                }
                else if (zustand == 1)
                {
                    if (x == 0)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y,x];
                        index++;
                        while (kp <= 0)
                        {
                            y--;
                            x++;
                            returnArray[index] = InputByteArray[y,x];
                            index++;
                        }
                    }
                    else if (y == 0)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y,x];
                        index++;
                        while (kp <= 0)
                        {
                            y++;
                            x--;
                            returnArray[index] = InputByteArray[y,x];
                            index++;
                        }
                    }
                    if (xmax == x)
                    {
                        zustand = 2;
                    }

                }
                else if (zustand == 2)
                {
                    if (x == xmax)
                    {
                        while (kp <= 0)
                        {
                            y++;
                            x--;
                            returnArray[index] = InputByteArray[y,x];
                            index++;
                        }
                        x++;
                        returnArray[index] = InputByteArray[y,x];
                        index++;
                        kpold = kp - 1;
                    }
                    if (y == ymax)
                    {
                        while (kp <= 0)
                        {
                            y--;
                            x++;
                            returnArray[index] = InputByteArray[y,x];
                            index++;
                        }
                        y++;
                        returnArray[index] = InputByteArray[y,x];
                        index++;
                        kpold = kp - 1;
                    }

                }
                if (x + 1 == xmax && y == 0)
                {
                    x++;
                    returnArray[index] = InputByteArray[y,x];
                    index++;
                }
                else if (y + 1 == xmax && x == 0)
                {
                    y++;
                    returnArray[index] = InputByteArray[y,x];
                    index++;
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
                else if (zustand == 2)
                {                    
                    if (x == xmax && y == 0 && (xmax%2)!=1)
                    {
                        y++;
                        returnArray[index] = InputByteArray[y, x];
                        kp--;
                        kpold--;
                        index++;
                    }
                    else if (y == ymax && x == 0 && (xmax % 2) != 1)
                    {
                        x++;
                        returnArray[index] = InputByteArray[y, x];
                        index++;
                        kp--;
                        kpold--;
                        zustand++;
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
                    //else if (y == 0)
                    //{
                    //    y++;
                    //    returnArray[index] = InputByteArray[y, x];
                    //    index++;
                    //    kpold = kpold - 1;
                    //    while (kp > 0)
                    //    {
                    //        y++;
                    //        x--;
                    //        returnArray[index] = InputByteArray[y, x];
                    //        index++;
                    //        kp--;
                    //    }
                    //}                    
                    //else if (x == 0)
                    //{
                    //    x++;
                    //    returnArray[index] = InputByteArray[y, x];
                    //    index++;
                    //    kpold = kpold - 1;
                    //    while (kp > 0)
                    //    {
                    //        y--;
                    //        x++;
                    //        returnArray[index] = InputByteArray[y, x];
                    //        index++;
                    //        kp--;
                    //    }
                    //}
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
    }
}
