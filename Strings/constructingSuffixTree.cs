using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Addition
{


    class ConstructingSuffixTree
    {
        public static char[] CountingSort(char[] InputStr)
        {
            var ACount = 0;
            var CCount = 0;
            var GCount = 0;
            var TCount = 0;

            var CountingSortArr = new int[4];
            //perform counting sort
            for (var i = 0; i < InputStr.Length; i++)
            {

                if (InputStr[i] == 'A')
                {
                    ACount++;
                    CountingSortArr[0]++;
                }
                else if (InputStr[i] == 'C')
                {
                    CCount++;
                    CountingSortArr[1]++;
                }
                else if (InputStr[i] == 'G')
                {
                    GCount++;
                    CountingSortArr[2]++;
                }
                else if (InputStr[i] == 'T')
                {
                    TCount++;
                    CountingSortArr[3]++;
                }
            }

            var SortedStr = new char[InputStr.Length];
            SortedStr[0] = '$';
            for (var i = 1; i < InputStr.Length; i++)
            {
                while (ACount > 0)
                {
                    SortedStr[i] = 'A';
                    ACount--;
                    i++;
                }
                while (CCount > 0)
                {
                    SortedStr[i] = 'C';
                    CCount--;
                    i++;
                }
                while (GCount > 0)
                {
                    SortedStr[i] = 'G';
                    GCount--;
                    i++;
                }
                while (TCount > 0)
                {
                    SortedStr[i] = 'T';
                    TCount--;
                    i++;
                }
                if (i < InputStr.Length - 1)
                {
                    throw new Exception("non ACGT chars in input String");
                }
            }

            return SortedStr;
        }

        //NB both CalcOrder and Sorting could be done in One Step and require one less interation through the input Str
        //These havve been calculated separately for clarity sake however could be refactored
        //the Length of each Occurance Array would then subsitute the Count vars.
        public static int[] CalcOrder(char[] unsortedStr)
        {

            var AOccurances = new Queue<int>();
            var COccurances = new Queue<int>();
            var GOccurances = new Queue<int>();
            var TOccurances = new Queue<int>();

            //for SortStr.Length-1 as las char will be $
            for (var i = 0; i < unsortedStr.Length - 1; i++)
            {
                if (unsortedStr[i] == 'A')
                    AOccurances.Enqueue(i);
                else if (unsortedStr[i] == 'C')
                    COccurances.Enqueue(i);
                else if (unsortedStr[i] == 'G')
                    GOccurances.Enqueue(i);
                else
                    TOccurances.Enqueue(i);
            }

            var OrderArray = new int[unsortedStr.Length];
            //first letter will be the $ from the end of the string
            OrderArray[0] = unsortedStr.Length - 1;

            for (var i = 1; i < unsortedStr.Length; i++)
            {
                while (AOccurances.Any())
                {
                    OrderArray[i] = AOccurances.Dequeue();
                    i++;
                }
                while (COccurances.Any())
                {
                    OrderArray[i] = COccurances.Dequeue();
                    i++;
                }
                while (GOccurances.Any())
                {
                    OrderArray[i] = GOccurances.Dequeue();
                    i++;
                }
                while (TOccurances.Any())
                {
                    OrderArray[i] = TOccurances.Dequeue();
                    i++;
                }
                if (i < unsortedStr.Length - 1)
                {
                    throw new Exception("non ACGT chars in input String");
                }
            }
            return OrderArray;
        }




        public static int[] CalulateClass(char[] sortedStr, int[] Order)
        {
            var ClassArr = new int[sortedStr.Length];


            char CurrentLetter = '$';
            var currentClass = 0;
            for (var i = 0; i < sortedStr.Length; i++)
            {
                //find the position of the current letter in the unsorted string using the orderArray;
                var Position = Order[i];

                //move through the sorted string, each time the letter changes increase the currentClass count;
                //and update the current Letter
                if (CurrentLetter != sortedStr[i])
                {
                    CurrentLetter = sortedStr[i];
                    currentClass++;
                }

                ClassArr[Position] = currentClass;

            }

            return ClassArr;
        }


        public static int[] SortDoubled(char[] unsortedStr, int L, int[] Order, int[] Class)
        {
            var WrdLngth = unsortedStr.Length;

            var CountArr = new int[WrdLngth];

            var newOrder = new int[WrdLngth];

            //perform a counting sort of the classes
            for (var i = 0; i < WrdLngth; i++)
            {
                CountArr[Class[i]]++;
            }

            //compute partial sums of count arrays
            for (var i = 1; i < WrdLngth; i++)
            {
                CountArr[i] += CountArr[i - 1];
            }

            for (var i = WrdLngth - 1; i >= 0; i--)
            {
                var start = (Order[i] - L + WrdLngth) % WrdLngth;

                var curClass = Class[start];

                CountArr[curClass]--;

                newOrder[CountArr[curClass]] = start;
            }

            return newOrder;
        }

        public static int[] UpdateClasses(int[] newOrder, int[] Class, int L)
        {
            var WrdLng = newOrder.Length;

            var newClass = new int[WrdLng];

            var startingPos = newOrder[0];

            newClass[startingPos] = 0;

            for (var i = 1; i < WrdLng; i++)
            {
                var cur = newOrder[i];
                var prev = newOrder[i - 1];

                var mid = (cur + L) % WrdLng;
                var midPrev = (prev + L) % WrdLng;

                if (Class[cur] != Class[prev] || Class[mid] != Class[midPrev])
                    newClass[cur] = newClass[prev] + 1;
                else
                    newClass[cur] = newClass[prev];
            }
            return newClass;
        }


        public static int[] StrToSuffixArr(char[] InputStr)
        {
            var wrdLng = InputStr.Length;
            var SortedStr = CountingSort(InputStr);
            var OrderArr = CalcOrder(InputStr);
            var ClassArr = CalulateClass(SortedStr, OrderArr);
            var L = 1;

            while (L < wrdLng)
            {
                OrderArr = SortDoubled(InputStr, L, OrderArr, ClassArr);
                ClassArr = UpdateClasses(OrderArr, ClassArr, L);
                L *= 2;
            }



            return OrderArr;
        }

        public static int[] InvertArray(int[] ArrayToInvert)
        {
            var InvertedArray = new int[ArrayToInvert.Length];

            for (var i = 0; i < ArrayToInvert.Length; i++)
            {
                InvertedArray[ArrayToInvert[i]] = i;
            }

            return InvertedArray;
        }



        public static int GetNextLCP(char[] InputStr, int a, int b, int KnownPrefixLength)
        {
            var wrdLng = InputStr.Length;

            var count = (KnownPrefixLength > 0) ? KnownPrefixLength : 0;

            while (a + count < wrdLng && b + count < wrdLng)
            {
                if (InputStr[a + count] == InputStr[b + count])
                {
                    count++;
                }
                else break;

            }
            return count;
        }


        public static int[] GetLCPArray(int[] SuffixArray, char[] InputStr)
        {
            var WrdLng = InputStr.Length;

            var LCPArray = new int[WrdLng - 1];

            var PosInOrder = InvertArray(SuffixArray);

            var Suffix = PosInOrder[0];

            var LeftIndex = 0;
            var RightIndex = 1;
            var currentLCP = GetNextLCP(InputStr, LeftIndex, RightIndex, 0);


            for (var i = 0; i < WrdLng; i++)
            {
                //LCPArray[i] = currentLCP;
                //LeftIndex = SuffixArray[LeftIndex + 1];
                //RightIndex = SuffixArray[RightIndex + 1];
                //var nextLCP = GetNextLCP(InputStr, LeftIndex, RightIndex, currentLCP - 1);

                var OrderIndex = PosInOrder[Suffix];
                if (OrderIndex == WrdLng - 1)
                {
                    currentLCP = 0;
                    Suffix = (Suffix + 1) % WrdLng;
                    continue;
                }
                var nextSuffix = SuffixArray[OrderIndex + 1];
                currentLCP = GetNextLCP(InputStr, Suffix, nextSuffix, currentLCP - 1);
                LCPArray[OrderIndex] = currentLCP;
                Suffix = (Suffix + 1) % WrdLng;
            }
            return LCPArray;
        }

        public class SuffixTreeNode
        {
            public SuffixTreeNode Parent { get; set; }
            public Dictionary<int, SuffixTreeNode> Children { get; set; }
            public int StringDepth { get; set; }
            public int EdgeStart { get; set; }
            public int EdgeEnd { get; set; }
        }


        public class SuffixTree
        {
            public SuffixTreeNode Root { get; set; }

            public SuffixTree(char[] InputStr, int[] SuffixArr, int[] LCPArray)
            {
                Root = STFromSA(InputStr, SuffixArr, LCPArray);
            }

            private SuffixTreeNode BreakEdge(SuffixTreeNode currentNode, char[] inputString, int EdgeStart, int Offset)
            {
                var StartChar = inputString[EdgeStart];
                var MidChar = inputString[EdgeStart + Offset];

                var midNode = new SuffixTreeNode
                {
                    Children = new Dictionary<int, SuffixTreeNode>(),
                    Parent = currentNode,
                    StringDepth = currentNode.StringDepth + Offset,
                    EdgeStart = EdgeStart,
                    EdgeEnd = EdgeStart + Offset - 1
                };
                midNode.Children[MidChar] = currentNode.Children[StartChar];

                currentNode.Children[StartChar].Parent = midNode;
                currentNode.Children[StartChar].EdgeStart += Offset;
                currentNode.Children[StartChar] = midNode;

                return midNode;
            }

            private SuffixTreeNode CreateNewLeaf(SuffixTreeNode currentNode, char[] inputString, int suffix)
            {
                var NewLeaf = new SuffixTreeNode
                {
                    Children = new Dictionary<int, SuffixTreeNode>(),
                    Parent = currentNode,
                    StringDepth = inputString.Length - suffix,
                    EdgeStart = suffix + currentNode.StringDepth,
                    EdgeEnd = inputString.Length - 1
                };

                currentNode.Children[inputString[NewLeaf.EdgeStart]] = NewLeaf;

                return NewLeaf;
            }

            private SuffixTreeNode STFromSA(char[] InputStr, int[] SuffixArr, int[] LCPArray)
            {
                var WrdLng = InputStr.Length;

                var root = new SuffixTreeNode
                {
                    Children = new Dictionary<int, SuffixTreeNode>(),
                    StringDepth = 0,
                    EdgeStart = -1,
                    EdgeEnd = -1
                };

                var LCPPrev = 0;
                var CurrentNode = root;

                for (var i = 0; i < WrdLng; i++)
                {
                    var CurrentSuffix = SuffixArr[i];
                    while (CurrentNode.StringDepth > LCPPrev)
                        CurrentNode = CurrentNode.Parent;
                    if (CurrentNode.StringDepth == LCPPrev)
                        CurrentNode = CreateNewLeaf(CurrentNode, InputStr, CurrentSuffix);
                    else
                    {
                        var EdgeStart = SuffixArr[i - 1] + CurrentNode.StringDepth;
                        var Offset = LCPPrev - CurrentNode.StringDepth;
                        var MidNode = BreakEdge(CurrentNode, InputStr, EdgeStart, Offset);
                        var CurNode = CreateNewLeaf(MidNode, InputStr, CurrentSuffix);
                    }

                    if (i < WrdLng - 1)
                        LCPPrev = LCPArray[i];
                }


                return root;
            }
        }

        public static void OutputEdges(SuffixTreeNode current)
        {
            var edges = current.Children.ToList();
            foreach (var edge in edges)
            {
                Console.WriteLine(edge.Value.EdgeStart + " " + (edge.Value.EdgeEnd + 1));
                OutputEdges(edge.Value);
            };

        }

        //static void Main(string[] args)
        //{
        //    var InputString = (Console.ReadLine()).ToCharArray();

        //    var SuffixArray = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));

        //    var LCPArray = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));

        //    var Treeinst = new SuffixTree(InputString, SuffixArray, LCPArray);

        //    var root = Treeinst.Root;

        //    Console.WriteLine(InputString);
        //    OutputEdges(root);


        //    //ArrayList<String> result = new ArrayList<String>();
        //    // Output the edges of the suffix tree in the required order.
        //    // Note that we use here the contract that the root of the tree
        //    // will have node ID = 0 and that each vector of outgoing edges
        //    // will be sorted by the first character of the corresponding edge label.
        //    //
        //    // The following code avoids recursion to avoid stack overflow issues.
        //    // It uses two stacks to convert recursive function to a while loop.
        //    // This code is an equivalent of 
        //    //
        //    //    OutputEdges(tree, 0);
        //    //
        //    // for the following _recursive_ function OutputEdges:
        //    //
        //    // public void OutputEdges(Map<Integer, List<Edge>> tree, int nodeId) {
        //    //     List<Edge> edges = tree.get(nodeId);
        //    //     for (Edge edge : edges) {
        //    //         System.out.println(edge.start + " " + edge.end);
        //    //         OutputEdges(tree, edge.node);
        //    //     }
        //    // }
        //    //

        //    //depth first travevrsal
        //    var Result = new List<string>();

        //    //    OutputEdges(tree, 0);

        //    //for the following _recursive_ function OutputEdges:





        //    //int[] nodeStack = new int[InputString.Length];
        //    //    int[] edgeIndexStack = new int[InputString.Length];
        //    //    nodeStack[0] = 0;
        //    //    edgeIndexStack[0] = 0;
        //    //    int stackSize = 1;
        //    //    while (stackSize > 0)
        //    //    {
        //    //        int node = nodeStack[stackSize - 1];
        //    //        int edgeIndex = edgeIndexStack[stackSize - 1];
        //    //        stackSize -= 1;
        //    //        if (!root.Children.Any())
        //    //        {
        //    //            continue;
        //    //        }
        //    //        if (edgeIndex + 1 < root.Children[node].Children.Count())
        //    //        {
        //    //            nodeStack[stackSize] = node;
        //    //            edgeIndexStack[stackSize] = edgeIndex + 1;
        //    //            stackSize += 1;
        //    //        }
        //    //        Result.Add(root.Children[node].get(edgeIndex).start + " " + root.get(node).get(edgeIndex).end);
        //    //        nodeStack[stackSize] = root.get(node).get(edgeIndex).node;
        //    //        edgeIndexStack[stackSize] = 0;
        //    //        stackSize += 1;
        //    //    }
        //    //    print(result);
        //    //
        //}
    }
}




