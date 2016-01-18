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

            //not used but must be read so only the body remains in reader
            string maxColor = reader.ReadLine();

            head.pixelMaxY = height;
            head.pixelMaxX = width;

            return head;
        }

        //reads the remaining lines in the reader and parses it into color3 values for each pixel
        PictureData ReadPictureData()
        {
            PictureData output = new PictureData();
            String[] values = PrepareValues();

            output.PictureYX = new Color3[height, width];

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


            if (height % 8 != 0 || width % 8 != 0)
            {
                PictureData filledOutput = new PictureData();
                int targetHeight;
                int targetWidth;
                int fillright = 0;
                int fillbottom = 0;

                if (height % 8 != 0)
                    fillbottom = 8 - height % 8;
                targetHeight = height + fillbottom;
                
                if (width % 8 != 0)
                    fillright = 8 - width % 8;
                targetWidth = width + fillright;

                filledOutput.PictureYX = new Color3[targetHeight, targetWidth];

                for (int h = 0; h < targetHeight; h++)
                {
                    for (int w = 0; w < targetWidth; w++)
                    {
                        if (w >= width)
                            filledOutput.PictureYX[h, w] = filledOutput.PictureYX[h, width - 1];
                        else if (h >= height)
                            filledOutput.PictureYX[h, w] = filledOutput.PictureYX[height - 1, w];
                        else
                            filledOutput.PictureYX[h, w] = output.PictureYX[h, w];
                    }
                }
                return filledOutput;

            }

            return output;
        }

        //reads out the lines and splits them
        String[] PrepareValues()
        {
            String output = "";
            while (!reader.EndOfStream)
            {
                output = output + " " +  reader.ReadLine();
            }
            reader.Close();
            //output = output.Replace(" ", String.Empty);
            Regex rgx = new Regex("\\s+");
            output = rgx.Replace(output, " ");

            //deletes first empty char
            if (Char.IsWhiteSpace(output[0]))
            {
                output=output.Remove(0, 1);
            }

            //deletes last empty char
            if (Char.IsWhiteSpace(output[output.Length-1]))
            {
                output = output.Remove(output.Length - 1, 1);
            }
            return Regex.Split(output, " ");
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

        public int GetHeight()
        {
            return height;
        }

        public int GetWidth()
        {
            return width;
        }

    }
}
