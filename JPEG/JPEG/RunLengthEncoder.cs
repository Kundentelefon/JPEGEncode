using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace JPEG
{
     
    class RunLengthEncoder
    {
        byte[] input;
        struct pairValues { public byte merged; public byte zeros; public byte category; public short value; }
        

        public RunLengthEncoder(byte[] input)
        {
            this.input = input;
        }

        public short[] encodeACRunLength()
        {
            List<pairValues> pairList = new List<pairValues>();
            pairValues[] pairArray;
            MemoryStream stream;
            HuffmanEncoder coder = new HuffmanEncoder();
            SortedList<byte, List<bool>> huffmantable;
            byte[] streamArray;
            short[] solutionArray;
            
            byte zeroCounter = 0;
            pairValues tempValue = new pairValues();
            for (int i = 1; i<input.Length; i++)
            {
                if(i == input.Length-1 && input[i] == 0)
                {
                    tempValue.zeros = 0;
                    tempValue.value = 0;
                    pairList.Add(tempValue);
                }

                if(zeroCounter == 16)
                {
                    tempValue.zeros = 15;
                    tempValue.value = 0;
                    pairList.Add( tempValue);
                  
                    zeroCounter = 0;
                }

                else if(input[i] == 0)
                {
                    zeroCounter++;
                }
                else
                {
                    tempValue.zeros = zeroCounter;
                    zeroCounter = 0;
                    tempValue.value = input[i];
                    pairList.Add(tempValue);
                   
                }
            }

            
            int arrayCounter = 0;
            pairArray = pairList.ToArray();
            streamArray = new byte[pairArray.Length];
            pairList.Clear();
            

            for(int i = 0; i<pairArray.Length; i++)
            {
                int absValue = Math.Abs(pairArray[i].value);

                if (absValue >= 16384)
                {
                    pairArray[i].category = 15;
                }
                else if (absValue >= 8192)
                {
                    pairArray[i].category = 14;
                }
                else if (absValue >= 4096 )
                {
                    pairArray[i].category = 13;
                }
                else if (absValue >= 2048 )
                {
                    pairArray[i].category = 12;
                }
                else if (absValue >= 1024 )
                {
                    pairArray[i].category = 11;
                }
                else if (absValue>= 512 )
                {
                    pairArray[i].category = 10;
                }
                else if (absValue >= 256 )
                {
                    pairArray[i].category = 9;
                }
                else if (absValue >= 128 )
                {
                    pairArray[i].category = 8;
                }
                else if (absValue>= 64 )
                {
                    pairArray[i].category = 7;
                }
                else if (absValue>= 32 )
                {
                    pairArray[i].category = 6;
                }
                else if (absValue>= 16 )
                {
                    pairArray[i].category = 5;
                }
                else if (absValue>= 8 )
                {
                    pairArray[i].category = 4;
                }
                else if (absValue >= 4 )
                {
                    pairArray[i].category = 3;
                }
                else if (absValue >= 2)
                { 
                    pairArray[i].category = 2;
                }
                else if (pairArray[i].value == 1 || pairArray[i].value == -1)
                {
                    pairArray[i].category = 1;
                }
                else if (pairArray[i].value == 0)
                {
                    pairArray[i].category = 0;
                }

                if (pairArray[i].value < 0)
                {
                    pairArray[i].value = (short)(~absValue);
                    
                }
                //nur so viele bits von rechts nehmen wie in kategorie sagt
                pairArray[i].merged = ByteZusammenfassen(pairArray[i].zeros, pairArray[i].category);
                streamArray[i] = pairArray[i].merged;
            }


            stream = new MemoryStream(streamArray);
            coder.PrepareEncodingRightsided(stream);
            huffmantable = coder.getHuffmanTable();
            List<bool> encodedList = new List<bool>();
            Bitstream outputStream = new Bitstream();

            for (int i = 0;i < pairArray.Length;i++ )
            {
                
                    encodedList = huffmantable[pairArray[i].merged];
                for (int z = 0; z < encodedList.Count; z++)
                    outputStream.AddBit(encodedList[z]);
            }


            solutionArray = new short[pairArray.Length * 2];

            for(int i = 0; i<pairArray.Length; i++)
            {
                solutionArray[arrayCounter] = pairArray[i].zeros;
                solutionArray[arrayCounter + 1] = pairArray[i].value;
                arrayCounter += 2;
            }

            return solutionArray;
        }

    

        public byte ByteZusammenfassen(byte zahl1,byte zahl2)
        {
            byte returnbyte;
            returnbyte = swtichsup(zahl1,16,32,64,128);
            returnbyte = (byte)(returnbyte+ swtichsup(zahl2, 1, 2, 4, 8));
            return returnbyte;

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
