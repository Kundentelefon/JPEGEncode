using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEG
{
    class CompressionHandler
    {
        
        Color[] outputArray;
        Color[,] outputArray2D;
        Color[] inputArray;
        Color[,] inputArray2D;
        int pixelMaxX;
        int pixelMaxY;
        public CompressionHandler(Color[] color_array)
        {
            inputArray = color_array;

        }

        public CompressionHandler(Color[,] colorArray, int inputMaxX, int inputMaxY)
        {
            inputArray2D = colorArray;
            pixelMaxX = inputMaxX;
            pixelMaxY = inputMaxY;
        }
        /// <summary>
        /// festes Abtastschema 4:2:0
        /// </summary>
        /// <param name="input_compression"></param>
        /// <returns></returns>
        public Color[,] compressLine_schemaC(int input_compression)
        {
            outputArray2D = new Color[2, 2];

            for (int y = 0; y < pixelMaxX; y++)
            {
                for (int x = 0; x < pixelMaxX; x++)
                {
                    outputArray2D[y, x].a = inputArray2D[y, x].a;
                }
            }


            for (int y = 0; y < pixelMaxY;y= y+input_compression)
            {
                for (int x = 0; x < pixelMaxX; x=x + input_compression)
                {
                    outputArray2D[y, x].b = inputArray2D[y, x].b;
                    outputArray2D[y, x].c = inputArray2D[y, x].c;
                }
            }
            return outputArray2D;
        }

    }

}
