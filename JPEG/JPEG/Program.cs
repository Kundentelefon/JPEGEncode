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
			var testresult = test.testZickZackbyte();
			var testresult2 = test.conversionTest();

            Bitstream bs = new Bitstream(100);
            
            PictureHead.CreateJPGHead(bs, 900, 1600);

            bs.WriteToFile(@"C:\Users\Maxwell\Desktop\BitstreamTest.jpg");

            //TestChannelResolutionReduction("inputfile", "outputfile");
            Picture pic = new Picture();
            ReaderWriter rW = new ReaderWriter();
            PictureHead pH = new PictureHead();
            //pic.Head = pH.CreateJPGHead();

            //rW.writePicture("location", pic);
        }
		
	}
}
