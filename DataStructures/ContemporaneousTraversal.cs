using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Addition
{


    class ContemporaneousTravesal
    {
        public class Node
        {
            public int Key { get; set; }
            public Node LChild { get; set; }
            public Node RChild { get; set; }
        }



        public static void SingleTraversal(int[][] Tree, int Node, List<int>[] OrderArray)
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


        //static void Main(string[] args)
        //{
        //    var numberOfQueries = Convert.ToInt32(Console.ReadLine());

        //    var node2Array = new int[numberOfQueries][];

        //    for (var i = 0; i < numberOfQueries; i++)
        //    {
        //        var input = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt32(c));
        //        node2Array[i] = new int[3];
        //        node2Array[i][0] = input[0];
        //        node2Array[i][1] = input[1];
        //        node2Array[i][2] = input[2];
        //    }

        //    var OrderArrray = new List<int>[3];

        //    for (var i = 0; i < 3; i++)
        //    {
        //        OrderArrray[i] = new List<int>();
        //    }

        //    SingleTraversal(node2Array, 0, OrderArrray);
        //    foreach (var list in OrderArrray)
        //    {
        //        Console.WriteLine(string.Join(" ", list));
        //    }

        //}
    }
}




