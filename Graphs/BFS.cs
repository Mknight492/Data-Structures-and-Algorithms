using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Addition
{


    class PBFS
    {

        public class vertex
        {
            public int value { get; set; }
            public int Distance { get; set; }
        }



        public class Graph
        {
            private long Verticies;
            private List<long>[] Adj;
            private List<long>[] ReversedAdj;

            public Graph(long verticies)
            {
                Verticies = verticies;

                Adj = new List<long>[verticies];
                ReversedAdj = new List<long>[verticies];

                for (long i = 0; i < verticies; i++)
                {
                    Adj[i] = new List<long>();
                    ReversedAdj[i] = new List<long>();
                }
            }

            public void AddDirectedEdge(long source, long data)
            {
                if (!Adj[source].Contains(data))
                    Adj[source].Add(data);

            }

            public void AddUndirectedEdge(long source, long data)
            {
                if (!Adj[source].Contains(data))
                    Adj[source].Add(data);

                if (!Adj[data].Contains(source))
                    Adj[data].Add(source);
            }



            public long ShortestPath(long a, long b)
            {
                //array which keeps track of the current shortest path for the corresponding vertex i
                var DistanceArray = new long[Verticies];
                var prevVertexArray = new long[Verticies];

                for (var i = 0; i < Verticies; i++)
                {
                    DistanceArray[i] = long.MaxValue;
                }

                var NodesToProcess = new Queue<long>();

                NodesToProcess.Enqueue(a);
                DistanceArray[a] = 0;

                while (NodesToProcess.Any())
                {
                    var nextMinNode = NodesToProcess.Dequeue();
                    var nextAccesableNodes = Adj[nextMinNode];

                    foreach (var node in nextAccesableNodes)
                    {
                        var currentMinDistance = DistanceArray[node];
                        var distanceFromNextNode = DistanceArray[nextMinNode] + 1;
                        if (DistanceArray[nextMinNode] != long.MaxValue && distanceFromNextNode < currentMinDistance)
                        {
                            NodesToProcess.Enqueue(node);
                            DistanceArray[node] = distanceFromNextNode;
                            prevVertexArray[node] = nextMinNode;
                        }
                    }
                }


                if (DistanceArray[b] == long.MaxValue)
                {
                    return -1;
                }
                return DistanceArray[b];


            }

        }




        //static void Main(string[] args)
        //{
        //    var input = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c));
        //    long n = input[0]; //vertices
        //    var m = input[1]; //edges

        //    var graphInstance = new Graph(n);

        //    for (var i = 0; i < m; i++)
        //    {
        //        var edge = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c));
        //        graphInstance.AddUndirectedEdge(edge[0] - 1, edge[1] - 1);
        //    }

        //    var input2 = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c));
        //    var a = input2[0] - 1;
        //    var b = input2[1] - 1;
        //    var componentCount = graphInstance.ShortestPath(a, b);
        //    //sortedGraph.Reverse();

        //    Console.WriteLine(componentCount);


        //}
    }
}




