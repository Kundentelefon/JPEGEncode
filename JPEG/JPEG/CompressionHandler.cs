using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPEG.Model;

namespace JPEG
{
    class CompressionHandler
    {
        Color[,] outputArray2D;
        Color3[,] inputArray2D;
        int pixelMaxX;
        int pixelMaxY;
        byte aPattern; // y
        byte bPattern; // cb
        byte cPattern; // cr
        byte ColorPattern;


        //public CompressionHandler(Color[,] inputArray2D, int pixelMaxX, int pixelMaxY)
        //{
        //    this.inputArray2D = inputArray2D;
        //    this.pixelMaxX = pixelMaxX;
        //    this.pixelMaxY = pixelMaxY;
        //}

        // Only if no values are given, default values are set to 0
        public CompressionHandler(Color3[,] inputArray2D, int pixelMaxX, int pixelMaxY, 
            byte aPattern = 0, byte bPattern = 0, byte cPattern = 0, byte colorPattern = 0)
        {
            this.inputArray2D = inputArray2D;
            this.pixelMaxX = pixelMaxX;
            this.pixelMaxY = pixelMaxY;
            this.aPattern= aPattern;
            this.bPattern = bPattern;
            this.cPattern = cPattern;
            this.ColorPattern = colorPattern;

            outputArray2D = new Color[inputArray2D.GetLength(0), inputArray2D.GetLength(1)];
        }


        /// <summary>
        /// fixed sampling scheme 4:2:0, colorPattern not considered
        /// </summary>
        /// <param name="inputCompression"></param>
        /// <returns></returns>
        public Color[,] LocalAveraging(int inputCompression)
        {
            Color[,] outputArray2D;
            outputArray2D = new Color[pixelMaxY, pixelMaxX];
            //alle 3Vector Koordinaten
            for (int y = 0; y < pixelMaxY; y++)
            {
                for (int x = 0; x < pixelMaxX; x++)
                {
                    outputArray2D[y, x] = inputArray2D[y, x].ConvertToColor1();
                }
            }

            //Alle Werte auf der x-Achse werden abhängig der Kompression in den ersten Pixel gespeichert
            Color3[,] tempArray;
            tempArray = new Color3[pixelMaxX, pixelMaxY];
            for(int y = 0; y< pixelMaxY; y++)
            {
                for(int x = 0; x<pixelMaxX; x = x+inputCompression)
                {
                    int count = 0;
                    for (int z = x; z<x+inputCompression; z++)
                    {
                        if (z > pixelMaxX) break;
                        else
                        {
                            tempArray[y, x].Color3AdditionB(inputArray2D[y, z]);
                            //outputArray2D[y, x].b += inputArray2D[y, z].b;
                            tempArray[y, x].Color3AdditionC(inputArray2D[y, z]);
                            // outputArray2D[y, x].c += inputArray2D[y, z].c;
                           
                        }
                        count++;
                    }
                    tempArray[y, x].Color3DivisionB(count);
                    tempArray[y, x].Color3DivisionC(count);
                    //outputArray2D[y, x].c = (byte) (outputArray2D[y, x].c / count);

                }
            }

            //Alle Werte in y-Richtung, abhänig der Kompression werden in den ersten Pixel gespeichert
            for (int y = 0; y<pixelMaxY; y= y+ inputCompression)
            {
                for(int x = 0; x<pixelMaxX; x = x+inputCompression)
                {
                    int count = 0;
                    for(int z = y+1; z< y+inputCompression; z++)
                    {
                        if (z > pixelMaxY) break;
                        else
                        {
                            tempArray[y, x].Color3AdditionB(tempArray[z, x]);
                            tempArray[y, x].Color3AdditionC(tempArray[z, x]);
                            //outputArray2D[y, x].b += outputArray2D[z, x].b;                          
                            //outputArray2D[y, x].c += outputArray2D[z, x].c;

                        }
                        count++;
                    }
                    //outputArray2D[y, x].b = (byte) (outputArray2D[y, x].b / count);
                    //outputArray2D[y, x].c = (byte) (outputArray2D[y, x].c / count);

                    tempArray[y, x].Color3DivisionB(count);
                    tempArray[y, x].Color3DivisionC(count);
                    tempArray[y, x].a = outputArray2D[y, x].a;
                    outputArray2D[y, x] = tempArray[y, x];
                }
            }

            return outputArray2D;
            //alle 1Vector Koordinaten
            //for (int y = 0; y < pixelMaxY;y= y+inputCompression)
            //{
            //    for (int x = 0; x < pixelMaxX; x=x + inputCompression)
            //    {
            //        outputArray2D[y, x].b = inputArray2D[y, x].b;
            //        outputArray2D[y, x].c = inputArray2D[y, x].c;
            //    }
            //    }
            //return outputArray2D;
        }

        // fixed compression 444 to 422
        //public Color[,] LocalAveraging444To422(int inputCompression)
        //{
        //    // Picture are not smaller, because each pixel are saved -> later a ColorChannel solution must be implemented
        //    // PictureColorChannel inputData = new PictureColorChannel(inputArray2D);
            
        //    int rows = inputArray2D.GetLength(0);
        //    int cols = inputArray2D.GetLength(1);

        //    for (int rowIdx = 0; rowIdx < rows; rowIdx++)
        //    {
        //        for (int colIdx = 0; colIdx < cols; colIdx++)
        //        {
        //            Color newPixel = new Color
        //            {
        //                a = inputArray2D[rowIdx, colIdx].a,
        //                // bitwise operation to duplicate only every second column pixel 
        //                b = inputArray2D[rowIdx, colIdx & ~0x1].b,
        //                c = inputArray2D[rowIdx, colIdx & ~0x1].c
        //            };
        //            outputArray2D[rowIdx, colIdx] = newPixel;
        //        }
        //    }

        //    return outputArray2D;
        //}
    }

}
