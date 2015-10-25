using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace JPEG
{
	class Program
	{
		static void Main(string[] args)
		{
            // Test test = new Test();
            // var testresult=test.testZickZackbyte();

            TestChannelResolutionReduction("inputfile", "outputfile");
		}

	    private static void TestChannelResolutionReduction(string inputFile, string outputFile)
	    {
            // load Bitmap .png
            Bitmap bitmap = new Bitmap(inputFile);
            Color[,] picture = new Color[bitmap.Height, bitmap.Width];
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    var bitmapPixel = bitmap.GetPixel(x, y);
                    // gets RGB from each pixel
                    Color newPixel = new Color
                    {
                        a = bitmapPixel.R,
                        b = bitmapPixel.G,
                        c = bitmapPixel.B
                    };
                    picture[y, x] = newPixel;
                }
            }

            CompressionHandler compressionHandler = new CompressionHandler(picture, bitmap.Width, bitmap.Height);

            // compressionhandler dont nead parameter yet
            Color[,] result = compressionHandler.LocalAveraging444To422(0xDEAD);

            Bitmap resultImage = new Bitmap(bitmap.Width, bitmap.Height);
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    // draws image
                    System.Drawing.Color color = System.Drawing.Color.FromArgb(result[y, x].a, result[y, x].b, result[y, x].c);
                    resultImage.SetPixel(x, y, color);
                }
            }
            resultImage.Save(outputFile);
        }
	}
}

//        using MathNet.Numerics.LinearAlgebra;
//using MathNet.Numerics.LinearAlgebra.Double;

//Matrix<double> A = DenseMatrix.OfArray(new double[,] {
//        {1,1,1,1},
//        {1,2,3,4},
//        {4,3,2,1}});
//    Vector<double>[] nullspace = A.Kernel();