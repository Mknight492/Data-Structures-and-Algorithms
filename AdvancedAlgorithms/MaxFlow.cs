using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Addition
{


    class FindMaxFlow
    {
        public class Edge
        {
            private long Capacity;

            public int Start { get; set; }
            public int End { get; set; }
            public long Flow { get; set; }

            public bool ResEdge { get; set; }

            public Edge Corresponding { get; set; }

            public Edge(int capacity, int start, int end)
            {
                Capacity = capacity;
                End = end;
                Start = start;
            }

            public long ResFlow()
            {
                return Capacity - Flow;
            }
        }

        public class Graph
        {
            private List<Edge>[] Adj;
            private int VCount;

            public Graph(int NumberVerticies)
            {
                VCount = NumberVerticies;
                Adj = new List<Edge>[NumberVerticies];

                for (var i = 0; i < NumberVerticies; i++)
                    Adj[i] = new List<Edge>();
            }

            public void AddEdge(int Start, int End, int Capacity)
            {
                var newEdge = new Edge(Capacity, Start, End);
                newEdge.ResEdge = false;
                Adj[Start].Add(newEdge);

                var newResEdge = new Edge(Capacity, End, Start);
                newResEdge.Flow = Capacity;
                newResEdge.ResEdge = true;
                Adj[End].Add(newResEdge);

                newEdge.Corresponding = newResEdge;
                newResEdge.Corresponding = newEdge;
            }

            private bool IncreaseFlow(int a, int b)
            {
                //array to keep track of which nodes have previously been visited
                var visited = new bool[VCount];

                //array which keeps track of the current shortest path for the corresponding vertex i
                var prevEdgeArray = new Edge[VCount];

                //array which keeps trach of the max flow through any given node
                var MaxFlow = new long[VCount];
                for (var i = 0; i < VCount; i++)
                {
                    MaxFlow[i] = long.MaxValue;
                }

                var currentMinFlow = long.MaxValue;


                var NodesToProcess = new Queue<long>();

                NodesToProcess.Enqueue(a);
                visited[a] = true;
                //MaxFlow[a] = 

                while (NodesToProcess.Any())
                {
                    var currentNode = NodesToProcess.Dequeue();
                    var nextAccesableNodes = Adj[currentNode];

                    foreach (var nextEdge in nextAccesableNodes)
                    {
                        var prevMaxFlow = MaxFlow[currentNode];
                        var MaxFlowViaNode = nextEdge.ResFlow();

                        if (!visited[nextEdge.End] && MaxFlowViaNode > 0)
                        {
                            //keep track of the shortest path to each node



                            MaxFlow[nextEdge.End] = Math.Min(prevMaxFlow, MaxFlowViaNode);
                            //at the final node aka the capital
                            visited[nextEdge.End] = true;
                            prevEdgeArray[nextEdge.End] = nextEdge;

                            if (nextEdge.End == b)
                            {

                                var Current = nextEdge;

                                while (Current != null)
                                {
                                    //need to increase flow in edge but also decrese flow in res network

                                    Current.Flow += Math.Min(prevMaxFlow, MaxFlowViaNode);
                                    Current.Corresponding.Flow -= Math.Min(prevMaxFlow, MaxFlowViaNode);

                                    Current = prevEdgeArray[Current.Start];
                                }


                                return true;
                            }
                            NodesToProcess.Enqueue(nextEdge.End);


                        }
                    }
                }

                return false;


            }

            public long FindMaxFlow()
            {
                while (IncreaseFlow(0, VCount - 1)) { };
                long totalFlowOut = 0;

                var EdgesOutOfSource = Adj[0];


                foreach (var EdgeList in Adj)
                {
                    foreach (var Edge in EdgeList)
                    {
                        if (!Edge.ResEdge && Edge.End == VCount - 1 && Edge.Start != VCount - 1)
                            totalFlowOut += Edge.Flow;
                    }

                }


                return totalFlowOut;
            }


        }


        //static void Main(string[] args)
        //{
        //    var Input = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));
        //    var VCount = Input[0];
        //    var ECount = Input[1];

        //    var GraphInst = new Graph(VCount);

        //    for (var i = 0; i < ECount; i++)
        //    {
        //        var EdgeValues = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));
        //        GraphInst.AddEdge(EdgeValues[0] - 1, EdgeValues[1] - 1, EdgeValues[2]);
        //    }


        //    Console.WriteLine(GraphInst.FindMaxFlow());
        //}
    }
}




