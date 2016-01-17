using JPEG.Model;
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
        public static SortedList<byte, List<bool>> huffmanTable;
        public List<List<HuffmanNode>> depthList;
        public int maximumDepth;

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

        public SortedList<byte, List<bool>> PrepareEncodingRightsided(MemoryStream stream)
        {
            List<HuffmanNode> allLeafNodes = ReadToSortedList(stream);
            HuffmanNode rootNode = CreateTreeFromSortedList(allLeafNodes);
            rootNode = ReorderTreeToRightSide(rootNode);
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

        //3b
        //builds the HuffmanTree so that the left child of each node is a leaf with the highest remaining frequency (growing to the right hand side)
        //NOITCE: the lowest non-leaf node consists of two leafs with the lowest-frequency-leaf (total) as the right child
        public HuffmanNode ReorderTreeToRightSide(HuffmanNode root)
        {
            depthList = new List<List<HuffmanNode>>();
            maximumDepth = 0;
            GoThroughTree(root, 0);

            for (int i = maximumDepth; i > 0; i--)
            {
                depthList[i].Sort((nodeOne, nodeTwo) => nodeOne.depth.CompareTo(nodeTwo.depth));

                while (depthList[i].Count > 0)
                {
                    HuffmanNode newNode = new HuffmanNode(depthList[i][depthList.Count - 2], depthList[i][depthList.Count - 1]);
                    newNode.depth = newNode.right.depth + 1;
                    depthList[i-1].Add(newNode);

                    depthList[i].RemoveAt(depthList[i].Count-1);
                    depthList[i].RemoveAt(depthList[i].Count-1);
                }
            }

            return depthList[0][0];
        }

        public void GoThroughTree(HuffmanNode node, int currentDepth)
        {
            if (node.isLeaf)
            {
                node.depth = 0;
                depthList[currentDepth].Add(node);

                if (currentDepth > maximumDepth)
                    maximumDepth = currentDepth;
            }
            else
            {
                currentDepth++;
                GoThroughTree(node.left, currentDepth);
                GoThroughTree(node.right, currentDepth);
            }
        }

        //recurses through the entire tree and stacks up the respective code, then adds the respective symbol and code to the HuffmanTable when reaching a leave
        public void ConvertTreeToTable(List<bool> code, HuffmanNode node) //
        {
            if (node == null)
                return;
            if (node.isLeaf == true)
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

        //3d 
        public void EncodeToPackageMerge(List<HuffmanNode> inputlist, int depth)
        {
            //Sortierung nach Frequenz
            inputlist.Sort((nodeOne, nodeTwo) => nodeOne.frequency.CompareTo(nodeTwo.frequency));
            List<HuffmanNode> nodelist = new List<HuffmanNode>(inputlist);
            //Durchlaufe die Liste entsprechend der Tiefe
            for (int i = 2; i <= depth; i++)
            {
                //Bilde Paare und erstelle einen neuen Knoten, alle erstellten Knoten werden in tempList gespeichert
                List<HuffmanNode> tempList = new List<HuffmanNode>();
                for(int c = 1; c<nodelist.Count; c += 2)
                {
                    HuffmanNode mergedNode = new HuffmanNode(nodelist[c-1].frequency + nodelist[c].frequency, nodelist[c-1], nodelist[c]);
                    tempList.Add(mergedNode);
                    
                }

                nodelist.Clear(); //lösche alte liste
                nodelist.AddRange(inputlist); // setzte liste auf die Anfangswerte 
                nodelist.AddRange(tempList); // füge erzeugte knoten der liste hinzu
                tempList.Clear(); //Lösche alte Knoten für einen neuen Schleifendurchlauf

                //Sortiere neue Liste nach frequency
                nodelist.Sort((nodeOne, nodeTwo) => nodeOne.frequency.CompareTo(nodeTwo.frequency));

            }

            //---------------Liste mit Knoten wurde erstellt---------------------------------------------

            var valueTable = new Dictionary<byte, int>();
            for (int i = 0; i<inputlist.Count; i++)
            {
                valueTable[inputlist[i].symbol] = inputlist[i].depth;
            }

            //---------------Liste auf 2n-2 Elemente kürzen-----------------------------------------------

           
            int cutter = inputlist.Count * 2 - 2;
            if(nodelist.Count > cutter)
                nodelist.RemoveRange(cutter, nodelist.Count-cutter);

            //---------------Key/Value Liste für die Kodierungsgewichtung erstellt------------------------

            for (int i = 0; i < nodelist.Count; i++)
            {

                if (nodelist[i].isDepthNode == true)
                {
                    nodelist.Add(nodelist[i].left);
                    nodelist.Add(nodelist[i].right);
                    nodelist.Remove(nodelist[i]);
                    i--;
                }

                else
                {
                    valueTable[nodelist[i].symbol]++;
                }


            }
            Dictionary<Byte, int> wertOutput = new Dictionary<byte, int>();
            foreach (var item in inputlist)
            {
                wertOutput.Add(item.symbol,item.frequency);
            }
            EncodeToPackageMergeList( valueTable, wertOutput);
        }


        public void EncodeToPackageMergeList(Dictionary<Byte, int> input,Dictionary<Byte, int> wertInput)
        {
            List<List<Knoten>> baum = new List<List<Knoten>>();
            int maxValue = input.Values.Max();
            for (int i = 0; i < maxValue; i++)
            {
                baum.Add(new List<Knoten>());
            }
            //huffmanTable
            for (int i = maxValue; i > 0; i--)
            {
                var matches = input.Where(pair => pair.Value == i)
                  .Select(pair => pair.Key);                
                foreach (var item in matches)
                {
                    huffmanTable.Add(item, new List<bool>());
                    baum[i-1].Add(new Knoten(item,wertInput[item]));
                }
            }
            for (int i = maxValue-1; i >= 0; i--)
            {
                baum[i]=baum[i].OrderBy(o => o.wert).ToList();
                bool wert = false;

                for (int ib = 0; ib < baum[i].Count(); ib++)
                {
                    foreach (var itemInter in baum[i][ib].punkte)
                    {
                        huffmanTable[itemInter].Add(wert);
                    }

                    if (wert==true&&i!=0)
                    {
                        baum[i - 1].Add(new Knoten(new List<byte>(baum[i][ib].punkte),
                            new List<byte>(baum[i][ib - 1].punkte), 
                            baum[i][ib].wert + baum[i][ib - 1].wert));
                    }
                    else if(ib == baum[i].Count()-1 && i != 0)
                    {
                        baum[i - 1].Add(new Knoten(new List<byte>(baum[i][ib].punkte), baum[i][ib].wert));
                    }
                    wert= !wert;
                }
            }            
        }
        public void einBitMuster()
        {
            List<bool> einBit = new List<bool>();
            einBit.Add(true);
            byte match = huffmanTable.FirstOrDefault(x => x.Value==einBit).Key;
            einBit.Add(false);
            huffmanTable[match] = einBit;
        }
        
        //========================================================================================================================================================
        //Encoding and Decooding streams using a Huffman Coding Table

        public Bitstream Encode(MemoryStream stream)
        {
            Bitstream encodedOuput = new Bitstream();

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
                for (int i = 0; i < stream.GetLength(); i++)
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

        public SortedList<byte, List<bool>> getHuffmanTable()
        {
            return huffmanTable;
        }

    }
}
