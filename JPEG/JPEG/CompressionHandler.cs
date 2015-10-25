using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEG
{
    class CompressionHandler
    {
        Color[,] outputArray2D;
        Color[,] inputArray2D;
        int pixelMaxX;
        int pixelMaxY;
        byte aAbtastschema; // y
        byte bAbtastschema; // cb
        byte cAbtastschema; // cr
        byte Farbschema;


        //public CompressionHandler(Color[,] inputArray2D, int pixelMaxX, int pixelMaxY)
        //{
        //    this.inputArray2D = inputArray2D;
        //    this.pixelMaxX = pixelMaxX;
        //    this.pixelMaxY = pixelMaxY;
        //}

        // Only if no values are given, default values are set to 0
        public CompressionHandler(Color[,] inputArray2D, int pixelMaxX, int pixelMaxY, 
            byte aAbtastschema = 0, byte bAbtastschema = 0, byte cAbtastschema = 0, byte Farbschema = 0)
        {
            this.inputArray2D = inputArray2D;
            this.pixelMaxX = pixelMaxX;
            this.pixelMaxY = pixelMaxY;
            this.aAbtastschema= aAbtastschema;
            this.bAbtastschema = bAbtastschema;
            this.cAbtastschema = cAbtastschema;
            this.Farbschema = Farbschema;

            outputArray2D = new Color[inputArray2D.GetLength(0), inputArray2D.GetLength(1)];
        }


        /// <summary>
        /// festes Abtastschema 4:2:0, Farbschema egal
        /// </summary>
        /// <param name="inputCompression"></param>
        /// <returns></returns>
        public Color[,] LocalAveraging(int inputCompression)
        {
            Color[,] outputArray2D;
            outputArray2D = new Color[pixelMaxY, pixelMaxX];
            // alle 3Vector Koordinaten
            for (int y = 0; y < pixelMaxX; y++)
            {
                for (int x = 0; x < pixelMaxX; x++)
                {
                    outputArray2D[y, x].a = inputArray2D[y, x].a;
                }
            }
            // alle 1Vector Koordinaten
            for (int y = 0; y < pixelMaxY;y= y+ inputCompression)
            {
                for (int x = 0; x < pixelMaxX; x=x + inputCompression)
                {
                    outputArray2D[y, x].b = inputArray2D[y, x].b;
                    outputArray2D[y, x].c = inputArray2D[y, x].c;
                }
            }
            return outputArray2D;
        }

        // Kunde solution
        public Color[,] LocalAveraging444To422(int inputCompression)
        {
            // Picture are not smaller, because each pixel are saved -> later a ColorChannel solution must be implemented
            // PictureColorChannel inputData = new PictureColorChannel(inputArray2D);
            
            int rows = inputArray2D.GetLength(0);
            int cols = inputArray2D.GetLength(1);

            for (int rowIdx = 0; rowIdx < rows; rowIdx++)
            {
                for (int colIdx = 0; colIdx < cols; colIdx++)
                {
                    Color newPixel = new Color
                    {
                        a = inputArray2D[rowIdx, colIdx].a,
                        // bitwise operation to duplicate only every second column pixel 
                        b = inputArray2D[rowIdx, colIdx & ~0x1].b,
                        c = inputArray2D[rowIdx, colIdx & ~0x1].c
                    };
                    outputArray2D[rowIdx, colIdx] = newPixel;
                }
            }

            return outputArray2D;
        }
    }

}
