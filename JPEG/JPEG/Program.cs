using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			Test test = new Test();
			var testresult = test.testZickZackbyte();
			var testresult2 = test.conversionTest();

            Bitstream bs = new Bitstream();
            bs.Write(1, 100);

            //int[] bl = new int[10000000];
            //      Stopwatch sw = new Stopwatch();
            //      sw.Start();
            //for (int i = 0; i < 10000000; i++)
            //{
            //          bs.WriteBits(1, bl);
            //      }
            //      sw.Stop();
            //      Console.WriteLine("BitWrite Elapsed={0} ", sw.Elapsed);
            //      Console.ReadKey();
            //TestChannelResolutionReduction("inputfile", "outputfile");

            JPGHeader jpgh = new JPGHeader();
            jpgh.CreateJPGHead();
        }
		
	}
}
