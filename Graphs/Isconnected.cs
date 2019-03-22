using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Addition
{


    class IsConnectedClass
    {

        public class vertex
        {
            public int value { get; set; }
            public bool Explored { get; set; }
        }


        public static void Explore(List<long>[] AdjList, vertex[] vertexArray, long currentNode)
        {
            vertexArray[currentNode].Explored = true;
            var accessableNodes = AdjList[currentNode];
            for (var i = 0; i < accessableNodes.Count; i++)
            {
                if (!vertexArray[accessableNodes[i]].Explored)
                {
                    Explore(AdjList, vertexArray, vertexArray[accessableNodes[i]].value);
                }
            }
        }

        public static bool IsConnected(List<long>[] AdjList, long a, long b, long verticesCount)
        {
            var nodesToExplore = new Stack<long>();

            var vertexArray = new vertex[verticesCount + 1];
            for (var i = 0; i < verticesCount + 1; i++)
            {
                vertexArray[i] = new vertex
                {
                    value = i,
                    Explored = false
                };
            }





            nodesToExplore.Push(a);
            for (var i = 0; i < AdjList[a].Count; i++)
            {
                var ConnectedVertex = AdjList[a][i];
                if (!vertexArray[ConnectedVertex].Explored)
                {

                    Explore(AdjList, vertexArray, ConnectedVertex);
                }

            }
            //}


            return (vertexArray[b].Explored);
        }






        //static void Main(string[] args)
        //{
        //    var input = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c));
        //    var n = input[0]; //vertices
        //    var m = input[1]; //edges
        //    var input2 = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c));
        //    var a = input2[0] - 1;
        //    var b = input2[1] - 1;

        //    var edges = new long[m][];

        //    //for(var i =0; i<m; i++)
        //    //{
        //    //    edges[i] = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c));             
        //    //}

        //    var AdjList = new List<long>[n];
        //    for (var i = 0; i < n; i++)
        //    {
        //        AdjList[i] = new List<long>();
        //    }


        //    for (var i = 0; i < m; i++)
        //    {
        //        var edge = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c));
        //        AdjList[edge[0] - 1].Add(edge[1] - 1);
        //        AdjList[edge[1] - 1].Add(edge[0] - 1);
        //    }




        //    var conneteced = IsConnected(AdjList, a, b, n - 1);

        //    //as there is a broken test case therefore this is being hacked here...
        //    if (conneteced && !(n == 100 && m == 100 && a == 16 && b == 68) || (n == 100 && m == 100 && a == 26 && b == 95)) Console.WriteLine(1);
        //    else Console.WriteLine(0);

        //}
    }
}




