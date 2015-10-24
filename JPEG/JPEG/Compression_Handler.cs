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

        public Color[,] compressLine2D(int input_compression)
        {
            compression = input_compression;


            return output_array_2D;
        }

    }
}
