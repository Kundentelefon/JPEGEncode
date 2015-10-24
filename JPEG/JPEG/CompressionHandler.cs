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
        byte aAbtastschema;
        byte bAbtastschema;
        byte cAbtastschema;
        byte Farbschema;
        public CompressionHandler(Color[,] inputArray2D, int pixelMaxX, int pixelMaxY)
        {
            this.inputArray2D = inputArray2D;
            this.pixelMaxX = pixelMaxX;
            this.pixelMaxY = pixelMaxY;
        }
        public CompressionHandler(Color[,] inputArray2D, int pixelMaxX, int pixelMaxY, byte aAbtastschema, byte bAbtastschema,
            byte cAbtastschema, byte Farbschema)
        {
            this.inputArray2D = inputArray2D;
            this.pixelMaxX = pixelMaxX;
            this.pixelMaxY = pixelMaxY;
            this.aAbtastschema= aAbtastschema;
            this.bAbtastschema = bAbtastschema;
            this.cAbtastschema = cAbtastschema;
            this.Farbschema = Farbschema;
        }
        /// <summary>
        /// festes Abtastschema 4:2:0, Farbschema egal
        /// </summary>
        /// <param name="input_compression"></param>
        /// <returns></returns>
        public Color[,] lokaleMittelung(int input_compression)
        {
            Color[,] outputArray2D;
            outputArray2D = new Color[2, 2];
            //alle 3Vector Koordinaten
            for (int y = 0; y < pixelMaxX; y++)
            {
                for (int x = 0; x < pixelMaxX; x++)
                {
                    outputArray2D[y, x].a = inputArray2D[y, x].a;
                }
            }
            //alle 1Vector Koordinaten
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
