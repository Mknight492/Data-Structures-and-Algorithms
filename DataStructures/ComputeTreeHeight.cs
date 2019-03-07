using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addition.DataStructures
{
    class ComputeTreeHeight
    {
        public class Node
        {
            public int value { get; set; }
            public List<Node> Nodes = new List<Node>();
        }


        public static long MaxDepth(Node node)
        {
            var counter = 0;


            while (node.Nodes.Count == 1)
            {
                counter++;
                node = node.Nodes[0];
            }
            if (node.Nodes.Count == 0)
            {
                return counter;
            }
            else
            {
                var lengths = new long[node.Nodes.Count];
                for (var i = 0; i < node.Nodes.Count; i++)
                {
                    lengths[i] = MaxDepth(node.Nodes[i]);
                }
                return 1 + counter + lengths.Max();
            }
        }

        public static long MaxTreeDepth(long[] nodes)
        {
            var NodeArray = new Node[nodes.Length];
            Node Root = new Node();
            //initialise the tree
            for (var i = 0; i < nodes.Length; i++)
            {
                NodeArray[i] = new Node();
            }
            for (var i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] == -1)
                {
                    Root = NodeArray[i];
                }
                else
                {
                    var parent = nodes[i]; //-1 as input not 0 indexed
                    NodeArray[parent].Nodes.Add(NodeArray[i]);
                }
            }
            return MaxDepth(Root);
        }


        //static void Main(string[] args)
        //{
        //    var number = Console.ReadLine();
        //    var nodes = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c));

        //    var res = MaxTreeDepth(nodes);
        //    Console.WriteLine(res + 1);
        //}
    }
}
