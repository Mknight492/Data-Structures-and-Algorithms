using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Addition
{


    class Program
    {

        public enum Status
        {
            undiscovered,
            black,
            white
        }

        public class vertex
        {
            public int value { get; set; }
            public Status Status { get; set; }
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



            public bool IsBipartite()
            {
                //array which keeps track of the current shortest path for the corresponding vertex i
                var DistanceArray = new Status[Verticies];

                for (var i = 0; i < Verticies; i++)
                {
                    DistanceArray[i] = Status.undiscovered;
                }

                var NodesToProcess = new Queue<long>();

                NodesToProcess.Enqueue(0);
                DistanceArray[0] = Status.black;

                var valid = true;

                while (NodesToProcess.Any())
                {
                    var nextMinNode = NodesToProcess.Dequeue();
                    var nextAccesableNodes = Adj[nextMinNode];

                    foreach (var node in nextAccesableNodes)
                    {
                        if (DistanceArray[node] == Status.undiscovered)
                        {
                            DistanceArray[node] = (DistanceArray[nextMinNode] == Status.black) ? Status.white : Status.black;
                            NodesToProcess.Enqueue(node);
                        }

                        else if (
                            (DistanceArray[node] == Status.black && DistanceArray[nextMinNode] == Status.black)
                            || (DistanceArray[node] == Status.white && DistanceArray[nextMinNode] == Status.white))
                        {

                            valid = false;
                            break;
                        }
                    }
                }
                return valid;
            }
        }




        static void Main(string[] args)
        {
            var input = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c));
            long n = input[0]; //vertices
            var m = input[1]; //edges

            var graphInstance = new Graph(n);

            for (var i = 0; i < m; i++)
            {
                var edge = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c));
                graphInstance.AddUndirectedEdge(edge[0] - 1, edge[1] - 1);
            }

            //var input2 = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c));
            //var a = input2[0] - 1;
            //var b = input2[1] - 1;
            var componentCount = graphInstance.IsBipartite();
            //sortedGraph.Reverse();
            if (componentCount)
            {
                Console.WriteLine(1);
            }
            else
            {
                Console.WriteLine(0);
            }

        }


    }
}




