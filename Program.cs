using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Addition
{


    class Program
    {
        public class Node
        {
            public int Key { get; set; }
            public Node LChild { get; set; }
            public Node RChild { get; set; }
        }

        public static void InOrderTraversal(Node rootNode)
        {
            if(rootNode.LChild != null)
            {
                InOrderTraversal(rootNode.LChild);
            }
            Console.Write(rootNode.Key + " ");
            if(rootNode.RChild != null)
            {
                InOrderTraversal(rootNode.RChild);
            }
            
        }

        public static void PreOrderTraversal(Node rootNode)
        {
            Console.Write(rootNode.Key + " ");
            if (rootNode.LChild != null)
            {
                PreOrderTraversal(rootNode.LChild);
            }
            
            if (rootNode.RChild != null)
            {
                PreOrderTraversal(rootNode.RChild);
            }
        }

        public static void PostOrderTraversal(Node rootNode)
        {
            
            if (rootNode.LChild != null)
            {
                PostOrderTraversal(rootNode.LChild);
            }

            if (rootNode.RChild != null)
            {
                PostOrderTraversal(rootNode.RChild);
            }
            Console.Write(rootNode.Key + " ");
        }

        
        public static void ContemporaneousTraversal(int[][] Tree)
        {
            var inOrderArray = new List<int>();
            var preOrderArray = new List<int>();
            var postOrderArray = new List<int>();
            var traversalStack = new Stack<int>();

            var CurrentIndex = 0;
            traversalStack.Push(CurrentIndex);

            while (traversalStack.Any())
            {
                CurrentIndex = traversalStack.Peek();

                //if chech right child and add this to stack be processed last if it exists
                if (Tree[CurrentIndex][2] != -1)
                {
                    traversalStack.Push(Tree[CurrentIndex][2]);
                }

                if (Tree[CurrentIndex][1] != -1)
                {
                    preOrderArray.Add(Tree[CurrentIndex][1]);
                    traversalStack.Push(Tree[CurrentIndex][1]);
                }

                //if tree has no left but a right branch add it to inOrderTrav
                if (Tree[CurrentIndex][1] == -1 && Tree[CurrentIndex][2] != -1)
                {
                    inOrderArray.Add(CurrentIndex);
                }

                if (Tree[CurrentIndex][1] == -1 && Tree[CurrentIndex][2] == -1)
                {
                    postOrderArray.Add(Tree[CurrentIndex][0]);
                    traversalStack.Pop();
                    if(!traversalStack.Any())
                    CurrentIndex = traversalStack.Pop();

                    //if no left children add the nodes key to inOrderTraversal Array
                    if (Tree[CurrentIndex][2] != -1)
                    {
                        inOrderArray.Add(Tree[CurrentIndex][0]);
                        traversalStack.Push(Tree[CurrentIndex][2]);

                    }

                }
  



                //then put th

            }

            //Array.ConvertAll(inOrderArray.ToArray(), x => Convert.ToString(x)).Join(' ');

            //Console.WriteLine(string.Join(inOrderArray, " ");

            foreach(var node in preOrderArray)
            {
                Console.Write(node + " ");
            }
            Console.WriteLine();
        }

    

        public static void SingleTraversal(int[][]Tree, int Node, List<int>[] OrderArray)
        {
            OrderArray[1].Add(Tree[Node][0]);
            if (Tree[Node][1] != -1)
            {
                SingleTraversal(Tree, Tree[Node][1], OrderArray);
            }
            OrderArray[0].Add(Tree[Node][0]);
            if (Tree[Node][2] != -1)
            {
                SingleTraversal(Tree, Tree[Node][2], OrderArray);

            }
            OrderArray[2].Add(Tree[Node][0]);
        }





        static void Main(string[] args)
        {
            var numberOfQueries = Convert.ToInt32(Console.ReadLine());
            //var nodeArray = new Node[numberOfQueries];

            //for (var i = 0; i < numberOfQueries; i++)
            //{
            //    nodeArray[i] = new Node();
            //}


            var node2Array = new int[numberOfQueries][];

            for (var i = 0; i < numberOfQueries; i++)
            {
                var input = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt32(c));
                //nodeArray[i].Key = input[0];
                node2Array[i] = new int[3];
                node2Array[i][0] = input[0];
                node2Array[i][1] = input[1];
                node2Array[i][2] = input[2];
                //if (input[1] != -1)
                //{
                //    nodeArray[i].LChild = nodeArray[input[1]];
                //}
                //if (input[2] != -1)
                //{
                //    nodeArray[i].RChild = nodeArray[input[2]];
                //}
            }

            var OrderArrray = new List<int>[3];

            for (var i = 0; i < 3; i++)
            {
                OrderArrray[i] = new List<int>();
            }

            //InOrderTraversal(nodeArray[0]);
            //Console.WriteLine();
            //PreOrderTraversal(nodeArray[0]);
            //Console.WriteLine();
            //PostOrderTraversal(nodeArray[0]);

            SingleTraversal(node2Array, 0, OrderArrray);
            foreach(var list in OrderArrray)
            {
                Console.WriteLine(string.Join(" ", list));
            }
            //ContemporaneousTraversal(node2Array);
        }
    }
}




