using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEG.Model
{
    class Knoten
    {
        public List<byte> punkte= new List<byte>();
        public int wert;
        public Knoten(byte inByte,int inInt)
        {
            punkte.Add(inByte);
            wert = inInt;
        }
        public Knoten(List<byte> inByte, int inInt)
        {
            punkte = inByte;
            wert = inInt;
        }
        public Knoten(List<byte> inByte, List<byte> inByte2,int inInt)
        {
            punkte = inByte;
            foreach (var item in inByte2)
            {
                punkte.Add(item);
            }
            wert = inInt;
        }
    }
}
