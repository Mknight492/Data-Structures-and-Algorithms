using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Addition
{


    class GroupsClass
    {

        public class vertex
        {
            public int value { get; set; }
            public int Group { get; set; }
        }


        public static void Explore(List<long>[] AdjList, vertex[] vertexArray, long currentNode, int currentCount)
        {
            vertexArray[currentNode].Group = currentCount;
            var accessableNodes = AdjList[currentNode];
            for (var i = 0; i < accessableNodes.Count; i++)
            {
                if (vertexArray[accessableNodes[i]].Group != 0)
                {
                    Explore(AdjList, vertexArray, vertexArray[accessableNodes[i]].value, currentCount);
                }
            }
        }

        public static int Groups(List<long>[] AdjList, long verticesCount)
        {
            var connected = false;
            var nodesToExplore = new Stack<long>();

            var vertexArray = new vertex[verticesCount + 1];
            for (var i = 0; i < verticesCount + 1; i++)
            {
                vertexArray[i] = new vertex
                {
                    value = i,
                    Group = 0
                };
            }


            var currentCount = 0;

            for (var i = 0; i < AdjList.Length; i++)
            {
                if (vertexArray[i].Group == 0)
                {
                    for (var j = 0; j < AdjList[i].Count; j++)
                    {
                        if (vertexArray[i].Group != 0)
                        {
                            currentCount++;
                            Explore(AdjList, vertexArray, i, currentCount);
                        }
                    }
                }
            }




            return currentCount;
        }






        //static void Main(string[] args)
        //{
        //    var input = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c));
        //    var n = input[0]; //vertices
        //    var m = input[1]; //edges
        //    //var input2 = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c));
        //    //var a = input2[0] - 1;
        //    //var b = input2[1] - 1;

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




        //    var conneteced = Groups(AdjList, n - 1);

        //    Console.WriteLine(conneteced);

        //}
    }
}




