using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JPEG
{
    // TODO: Not working yet. Prototype for PPM reader
    class PPMReader
    {
        // TODO: optimize int(parse)
        public static Bitmap ReadPpmBitmap(string file)
        {
            int widths, heights;
            int RGBcount = 3;
            int R;
            int G;
            int B;
            var reader = new StreamReader(new FileStream(file, FileMode.Open));
            string text = "dsf";
            while (text != null)
            {
                text = reader.ReadLine();
                if (text != "P3")
                    return null;
                if (text == "#")
                    reader.ReadLine();

                text = reader.ReadLine();
                string[] xy = Regex.Split(text, " ");
                widths = int.Parse(xy[0]);
                heights = int.Parse(xy[1]);

                string maxColor = reader.ReadLine();

                while (reader.ReadLine() != null)
                {
                    for (int i = 0; i < (widths*heights); i++)
                    {
                        for (int j = 0; j < RGBcount; j++)
                        {
                            string[] colorLines = Regex.Split(reader.ReadLine(), " ");
                            if (j == 0)
                                R = int.Parse(colorLines[j]);
                            if (j == 1)
                                G = int.Parse(colorLines[j]);
                            if (j == 2)
                                B = int.Parse(colorLines[j]);
                        }
                    }
                }
            }
            return null;
        }

    }
}
