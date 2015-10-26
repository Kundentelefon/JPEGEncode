using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace JPEG
{
    class PlainFormatReader
    {
        //variables and constructors
        private StreamReader reader;

        private int height;
        private int width;

        public PlainFormatReader(String filepath)
        {
            reader = new StreamReader(new FileStream(filepath, FileMode.Open));
        }

        //actual workflow
        public Picture ReadPicture()
        {
            Picture output = new Picture();
            output.Head = ReadPictureHead();
            output.Data = ReadPictureData();
            return output;
        }

        //functional methods
        PictureHead ReadPictureHead()
        {
            PictureHead head = new PictureHead();

            if (reader.ReadLine() != "P3")
                return null;

            String line = reader.ReadLine();
            if (line.StartsWith("#"))
                line = reader.ReadLine();

            string[] xy = Regex.Split(line, " ");
            width = int.Parse(xy[0]);
            height = int.Parse(xy[1]);

            //not used but  must be read so only the body remains in reader
            string maxColor = reader.ReadLine();

            head.pixelMaxY = height;
            head.pixelMaxX = width;

            return head;
        }

        //reads the remaining lines in the reader and parses it into color3 values for each pixel
        PictureData ReadPictureData()
        {
            PictureData output = new PictureData();
            output.PictureYX = new Color[height, width];
            String[] values = PrepareValues();

            int yCount = 0;
            int xCount = 0;

            for (int i = 0; i < values.Length; i += 3)
            {
                output.PictureYX[yCount, xCount] = ReadPixelColor(values, i);
                xCount++;
                if (xCount == width)
                {
                    xCount = 0;
                    yCount++;
                }
            }

            return output;
        }

        //reads out the lines and splits them
        String[] PrepareValues()
        {
            String output = "";
            while (!reader.EndOfStream)
            {
                output = output + " " + reader.ReadLine();
            }
            reader.Close();
            return Regex.Split(output, "()+");
        }

        //pareses the String-value of the respective element in values into byte, to represent a Color3
        byte ReadColorChannel(String[] values, int index)
        {
            String output = values.ElementAt(index);
            return byte.Parse(output);
        }

        //calls the ReadColorChannel() method 3 times to collect all color information for one pixel
        Color3 ReadPixelColor(String[] values, int index)
        {
            Color3 output = new Color3();

            output.a = ReadColorChannel(values, index);
            output.b = ReadColorChannel(values, index + 1);
            output.c = ReadColorChannel(values, index + 2);

            return output;
        }

    }
}
