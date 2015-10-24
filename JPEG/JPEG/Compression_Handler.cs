using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEG
{
    class Compression_Handler
    {
        Color[] output_array;
        Color[,] output_array_2D;
        Color[] input_array;
        Color[ , ] input_array_2D;
        int pixelX;
        int pixelY;
        int compression;
        public Compression_Handler(Color[] color_array)
        {
            input_array = color_array;
            
        }

        public Compression_Handler(Color [ , ] color_array, int inputX, int inputY)
        {
            input_array_2D = color_array;
            pixelX = inputX;
            pixelY = inputY;
        }

        public Color[,] compressLine_schemaC(int input_compression)
        {
            compression = input_compression;
            Color tempColor = new Color();
   
            for (int y = 0; y < pixelY; y++)
            {
                for (int x = 0; x < pixelX; x++)
                {
                    tempColor = input_array[y, x];
                    output_array_2D[y, x].a = tempColor.a;
                }
            }


            for (int y = 0; y<pixelY; y+ input_compression)
            {
                for(int x = 0; x<pixelX; x+input_compression)
                {
                    tempColor = input_array[y, x];
                    output_array_2D[y, x].b = tempColor.b;
                    output_array_2D[y, x].c = tempColor.c;
                }
            }


            return output_array_2D;
        }

    }
}
