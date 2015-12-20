using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HilfeUpdater;


namespace JPEG
{
	class Program
	{
		static void Main(string[] args)
		{
			Test test = new Test();
            //var testresult = test.testZickZackbyte();
            //var testresult2 = test.conversionTest();

            //  Test.BitTest();
            //test.HuffmanDepthTest();
            //test.HuffmanMergeBaum();

            //Bitstream bs = new Bitstream(100000);
            //PictureHead.CreateJPGHead(bs, 900, 1600);
            //bs.WriteToFile(@"C:\Users\Lappi\Desktop\BitstreamTest.jpg");

            //DCT Test
            //test.TestDCT();
            //test.schleifentest();
            //test.loopTestArai();
            test.PerformanceTest();
			//test.loopTestAraibetter();
		}
		
	}
}
