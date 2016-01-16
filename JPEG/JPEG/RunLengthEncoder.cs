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
        struct pairValues {public byte zeros; public byte value; }
        public RunLengthEncoder(byte[] input)
        {
            this.input = input;
        }

        public byte[] encodeACRunLength()
        {
            List<pairValues> pairList = new List<pairValues>();
            pairValues[] pairArray;
            byte[] solutionArray;
            
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
            solutionArray = new byte[pairArray.Length * 2];

            for(int i = 0; i<pairArray.Length; i++)
            {
                solutionArray[arrayCounter] = pairArray[i].zeros;
                solutionArray[arrayCounter + 1] = pairArray[i].value;
                arrayCounter += 2;
            }

            return solutionArray;
        }



    }
}
