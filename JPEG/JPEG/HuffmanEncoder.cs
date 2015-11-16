using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEG
{
    class HuffmanEncoder
    {
        //Variables & Constructors
        public SortedList<byte, List<bool>> huffmanTable;

        public HuffmanEncoder()
        {
            huffmanTable = new SortedList<byte, List<bool>>();
        }

        //===================================================================================================================================================
        //Creating a Huffman Coding Table and Tree from a stream

        public SortedList<byte, List<bool>> PrepareEncoding(MemoryStream stream)
        {
            List<HuffmanNode> allLeafNodes = ReadToSortedList(stream);
            HuffmanNode rootNode = CreateTreeFromSortedList(allLeafNodes);
            ConvertTreeToTable(new List<bool>(), rootNode);

            return huffmanTable;
        }

        //Reads the stream, creates a Leaf node for every symbol with its respective frequency in it and places them in a sorted List<>
        public List<HuffmanNode> ReadToSortedList(MemoryStream stream) 
        {
            List<HuffmanNode> nodeList = new List<HuffmanNode>();

            try
            {
                for (int i = 0; i < stream.Length; i++)
                {
                    byte read = Convert.ToByte(stream.ReadByte());
                    if (nodeList.Exists(x => x.symbol == read))
                        nodeList.Find(y => y.symbol == read).FrequencyIncrease();
                    else
                        nodeList.Add(new HuffmanNode(read));
                }
                nodeList.Sort();
                return nodeList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //removes the rarest nodes from the list and combines them to a tree until only 1 node is left
        public HuffmanNode CreateTreeFromSortedList(List<HuffmanNode> nodeList) 
        {
            while (nodeList.Count > 1)
            {
                int index = nodeList.Count - 1;
                HuffmanNode nodeLower = nodeList[index];
                nodeList.RemoveAt(index);
                HuffmanNode nodeHigher = nodeList[index-1];
                nodeList.RemoveAt(index-1);
                nodeList.Add(new HuffmanNode(nodeLower, nodeHigher));
                nodeList.Sort();
            }
            return nodeList[0];
        }

        //recurses through the entire tree and stacks up the respective code, then adds the respective symbol and code to the HuffmanTable when reaching a leave
        public void ConvertTreeToTable(List<bool> code, HuffmanNode node) //
        {
            if (node == null)
                return;
            if (node.isLeaf = true)
            {
                huffmanTable.Add(node.symbol, code);
                return;
            }

            code.Add(false);
            ConvertTreeToTable(code, node.left);
            code.RemoveAt(code.Count - 1);
            code.Add(true);
            ConvertTreeToTable(code, node.right);
        }

        //========================================================================================================================================================
        //Encoding and Decooding streams using a Huffman Coding Table

       public Bitstream Encode(MemoryStream stream)
        {
            Bitstream encodedOuput = new Bitstream(10000);

            try
            {
                for (int i = 0; i < stream.Length; i++)
                {
                    byte read = Convert.ToByte(stream.ReadByte());
                    List<bool> output = huffmanTable.Values[huffmanTable.IndexOfKey(read)];

                    for (int j = 0; j < output.Count; j++)
                    {
                        encodedOuput.AddBit(output[j]);
                    }
                }

                return encodedOuput;
            }
            catch (Exception)
            {
                return encodedOuput;
            }
        }

        public MemoryStream Decode(Bitstream stream)
        {
            MemoryStream encodedOuput = new MemoryStream();
            List<bool> codeWord = new List<bool>();

            try
            {
                for (long i = 0; i < stream.GetLength(); i++)
                {
                    codeWord.Add(stream.GetBit(i));
                    if (huffmanTable.ContainsValue(codeWord))
                    {
                        encodedOuput.WriteByte(huffmanTable.Keys[huffmanTable.IndexOfValue(codeWord)]);
                        codeWord.Clear();
                    }
                }

                return encodedOuput;
            }
            catch (Exception)
            {
                return encodedOuput;
            }
        }

    }
}
