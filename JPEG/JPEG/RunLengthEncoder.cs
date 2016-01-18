using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using JPEG.Model;
using System.Threading.Tasks;

namespace JPEG
{
     
    class RunLengthEncoder
    {
        List<byte[]> input;
        //struct pairValues { public byte merged; public byte zeros; public byte category; public short value; }
        

        public RunLengthEncoder(List<byte[]> input)
        {
            this.input = input;
}

        public RLEObject encodeACRunLength()
        {
            List<pairValues> pairList = new List<pairValues>();
            MemoryStream stream;
            RLEObject RLEobj = new RLEObject();
          
          
            List<Bitstream> finalStreamList = new List<Bitstream>();
            byte[] streamArray;

            short[] dcDifferenceArray = getDCDifferenceArray(input);
            var dcCategoryArray = dcDifferenceArray.Select(getCategory).ToArray();

            for (int g = 0; g < input.Count; g++)
            {
                byte zeroCounter = 0;
                pairValues tempValue = new pairValues();
                for (int i = 1; i < input[g].Length; i++)
                {
                    if (i == input[g].Length - 1 && input[g][i] == 0)
                    {
                        tempValue.zeros = 0;
                        tempValue.value = 0;
                        pairList.Add(tempValue);
                    }

                    if (zeroCounter == 16)
                    {
                        tempValue.zeros = 15;
                        tempValue.value = 0;
                        pairList.Add(tempValue);

                        zeroCounter = 0;
                    }

                    else if (input[g][i] == 0)
                    {
                        zeroCounter++;
                    }
                    else
                    {
                        tempValue.zeros = zeroCounter;
                        zeroCounter = 0;
                        tempValue.value = input[g][i];
                        pairList.Add(tempValue);

                    }
                }


                pairValues[] pairArray = pairList.ToArray();
                streamArray = new byte[pairArray.Length];
                pairList.Clear();


                for (int i = 0; i < pairArray.Length; i++)
                {
                    pairArray[i].category = getCategory(pairArray[i].value);

                    if (pairArray[i].value < 0)
                    {
                        pairArray[i].value = (short)(~(-pairArray[i].value));

                    }
                    //nur so viele bits von rechts nehmen wie in kategorie sagt
                    pairArray[i].merged = connectBytes(pairArray[i].zeros, pairArray[i].category);
                    streamArray[i] = pairArray[i].merged;
                }

                HuffmanEncoder coder = new HuffmanEncoder();
                SortedList<byte, List<bool>> huffmantable = new SortedList<byte, List<bool>>();
                stream = new MemoryStream(streamArray);
                coder.PrepareEncodingRightsided(stream);
                huffmantable = coder.getHuffmanTable();
                RLEobj.huffmantablesAC.Add(huffmantable);
                List<bool> encodedList = new List<bool>();
                Bitstream outputStream = new Bitstream();


                pairDC pair = new pairDC();
                pair.value = dcDifferenceArray[g];
                MemoryStream hufstream = new MemoryStream(dcCategoryArray);
                HuffmanEncoder coderDC = new HuffmanEncoder();
                SortedList<byte, List<bool>> hufmantable = new SortedList<byte, List<bool>>();
                coderDC.PrepareEncodingRightsided(hufstream);
                hufmantable = coderDC.getHuffmanTable();
                pair.category = getCategory(pair.value); //huffman codieren
                List<bool> encodedListDC = new List<bool>();
                encodedListDC = hufmantable[pair.category];
                RLEobj.huffmantablesDC.Add(hufmantable);

                for (int z = 0; z < encodedListDC.Count; z++)
                    outputStream.AddBit(encodedListDC[z]);

                for (int z = 0; z < pair.category; z++)
                {
                    outputStream.AddBit(getBit(pair.value, z));
                }


                for (int i = 0; i < pairArray.Length; i++)
                {

                    encodedList = huffmantable[pairArray[i].merged];
                    for (int z = 0; z < encodedList.Count; z++)
                        outputStream.AddBit(encodedList[z]);
                    for (int z = 0; z < pairArray[i].category; z++)
                        outputStream.AddBit(getBit(pairArray[i].value, z));

                }

                finalStreamList.Add(outputStream);
            }
            RLEobj.bitstreams = finalStreamList;
            return RLEobj;
        }

        public bool getBit(short s, int bitnum)
        {
            var bit = (s & (1 << bitnum)) != 0;
            return bit;
        }

        public byte getCategory(short input)
        {
            int absValue = Math.Abs(input);

            if (absValue >= 16384)
            {
                return 15;
            }
            else if (absValue >= 8192)
            {
                return 14;
            }
            else if (absValue >= 4096)
            {
                return 13;
            }
            else if (absValue >= 2048)
            {
                return 12;
            }
            else if (absValue >= 1024)
            {
                return 11;
            }
            else if (absValue >= 512)
            {
                return 10;
            }
            else if (absValue >= 256)
            {
                return 9;
            }
            else if (absValue >= 128)
            {
                return 8;
            }
            else if (absValue >= 64)
            {
                return 7;
            }
            else if (absValue >= 32)
            {
                return 6;
            }
            else if (absValue >= 16)
            {
                return 5;
            }
            else if (absValue >= 8)
            {
                return 4;
            }
            else if (absValue >= 4)
            {
                return 3;
            }
            else if (absValue >= 2)
            {
                return 2;
            }
            else if (absValue == 1)
            {
                return 1;
            }
            else if (absValue == 0)
            {
                return 0;
            }
            return 16;
        }

        public byte ByteZusammenfassen(byte zahl1,byte zahl2)
        {
            byte returnbyte;
            returnbyte = swtichsup(zahl1,16,32,64,128);
            returnbyte = (byte)(returnbyte+ swtichsup(zahl2, 1, 2, 4, 8));
            return returnbyte;
            
        }

        public short[] getDCDifferenceArray(List<byte[]> input)
        {
            short[] array = new short[input.Count];
            short lastDC = 0;
            for(int i = 0; i<input.Count;i++)
            {
                short curDC = input[i][0];
                array[i] = (short)(curDC - lastDC);
                lastDC = curDC;
            }
            return array;
        }

        public byte connectBytes(byte zahl1, byte zahl2)
        {
            return (byte)(zahl1 << 4 | (0xf & zahl2));
        }
        private byte swtichsup(byte zahl,byte Rzahl, byte Rzahl2, byte Rzahl3, byte Rzahl4)
        {
            byte returnbyte= 0;
            switch (zahl)
            {
                case (0):
                    {
                        returnbyte = 0;
                        break;
                    }
                case (1):
                    {
                        returnbyte = Rzahl;
                        break;
                    }
                case (2):
                    {
                        returnbyte = Rzahl2;
                        break;
                    }
                case (3):
                    {
                        returnbyte = (byte)(Rzahl + Rzahl2);
                        break;
                    }
                case (4):
                    {
                        returnbyte = Rzahl3;
                        break;
                    }
                case (5):
                    {
                        returnbyte = (byte)(Rzahl + Rzahl3); ;
                        break;
                    }
                case (6):
                    {
                        returnbyte = (byte)(Rzahl2 + Rzahl3); ;
                        break;
                    }
                case (7):
                    {
                        returnbyte = (byte)(Rzahl + Rzahl2 + Rzahl3);
                        break;
                    }
                case (8):
                    {
                        returnbyte = Rzahl4;
                        break;
                    }
                case (9):
                    {
                        returnbyte = (byte)(Rzahl + Rzahl4);
                        break;
                    }
                case (10):
                    {
                        returnbyte = (byte)(Rzahl2 + Rzahl4);
                        break;
                    }
                case (11):
                    {
                        returnbyte = (byte)(Rzahl + Rzahl2 + Rzahl4);
                        break;
                    }
                case (12):
                    {
                        returnbyte = (byte)(Rzahl3 + Rzahl4);
                        break;
                    }
                case (13):
                    {
                        returnbyte = (byte)(Rzahl + Rzahl3 + Rzahl4);
                        break;
                    }
                case (14):
                    {
                        returnbyte = (byte)(Rzahl2 + Rzahl3 + Rzahl4);
                        break;
                    }
                case (15):
                    {
                        returnbyte = (byte)(Rzahl + Rzahl2 + Rzahl3 + Rzahl4);
                        break;
                    }
                default:
                    break;
            }
            return returnbyte;
        }

        //positiv und dann bitweise invertieren

    }
}
