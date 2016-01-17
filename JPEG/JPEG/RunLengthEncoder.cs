using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEG
{
     
    class RunLengthEncoder
    {
        byte[] input;
        struct pairValues {public byte zeros; public byte category; public short value; }
        public RunLengthEncoder(byte[] input)
        {
            this.input = input;
        }

        public short[] encodeACRunLength()
        {
            List<pairValues> pairList = new List<pairValues>();
            pairValues[] pairArray;
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

        //positiv und dann bitweise invertieren

    }
}
