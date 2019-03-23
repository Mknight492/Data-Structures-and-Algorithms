using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Addition
{


    class NegativeCycles
    {

        public class WeightedEdge
        {
            public long EndVertex { get; set; }
            public decimal weight { get; set; }
        }

        public class vertex
        {
            public int value { get; set; }
            public bool Explored { get; set; }
        }



        public class Graph
        {
            private long Verticies;
            private List<WeightedEdge>[] Adj;
            private List<WeightedEdge>[] ReversedAdj;

            public Graph(long verticies)
            {
                Verticies = verticies;

                Adj = new List<WeightedEdge>[verticies];
                ReversedAdj = new List<WeightedEdge>[verticies];

                for (long i = 0; i < verticies; i++)
                {
                    Adj[i] = new List<WeightedEdge>();
                    ReversedAdj[i] = new List<WeightedEdge>();
                }
            }

            public void AddDirectedEdge(long source, WeightedEdge data)
            {
                if (!Adj[source].Contains(data))
                    Adj[source].Add(data);

                var reveresedEdge = new WeightedEdge
                {
                    weight = data.weight,
                    EndVertex = source
                };

                if (!ReversedAdj[data.EndVertex].Contains(reveresedEdge))
                    ReversedAdj[data.EndVertex].Add(reveresedEdge);
            }



            public void Explore(vertex[] vertexArray, long currentNode)
            {
                vertexArray[currentNode].Explored = true;
                var accessableNodes = Adj[currentNode];
                for (var i = 0; i < accessableNodes.Count; i++)
                {
                    if (!vertexArray[accessableNodes[i].EndVertex].Explored)
                    {
                        Explore(vertexArray, vertexArray[accessableNodes[i].EndVertex].value);
                    }
                }
            }

            public List<WeightedEdge>[] AccesableNodes(long a)
            {
                var nodesToExplore = new Stack<long>();

                var vertexArray = new vertex[Verticies];

                for (var i = 0; i < Verticies; i++)
                {
                    vertexArray[i] = new vertex
                    {
                        value = i,
                        Explored = false
                    };
                }

                Explore(vertexArray, a);


                var accessableNode = new List<WeightedEdge>[Verticies];
                for (var i = 0; i < Verticies; i++)
                {
                    if (vertexArray[i].Explored)
                    {
                        accessableNode[i] = Adj[i];
                    }
                    else
                    {
                        accessableNode[i] = new List<WeightedEdge>();
                    }
                }
                return accessableNode;
            }

            public long FindRoot()
            {
                var Explored = new bool[Verticies];

                while (true)
                {
                    for (var i = 0; i < Verticies; i++)
                    {
                        if (!Explored[i])
                        {
                            return FindRootRec(i, Explored);
                        }
                    }
                }

            }
            private long FindRootRec(int i, bool[] explored)
            {
                explored[i] = true;

                var accesableNodes = ReversedAdj[i];

                if (accesableNodes.Count == 0)
                {
                    return i;
                }
                else
                {
                    foreach (var node in accesableNodes)
                    {
                        return FindRootRec((int)node.EndVertex, explored);
                    }
                }

                return -1;
            }


            public bool HasNegativeCycle()
            {
                var MinimumDistances = new decimal[Verticies];

                for (var i = 0; i < Verticies; i++)
                {
                    MinimumDistances[i] = decimal.MaxValue;
                }

                //var RootNode = FindRoot(); // this should be a SCC sort.

                bool AdjustmentMade;

                for (var i = 0; i <= Verticies; i++)
                {
                    AdjustmentMade = false;

                    for (var j = 0; j < Verticies; j++)
                    {

                        foreach (var edge in Adj[j])
                        {
                            if (MinimumDistances[j] == decimal.MaxValue)
                            {
                                MinimumDistances[j] = 1;
                            }
                            var currentMinimum = MinimumDistances[edge.EndVertex];
                            var newDistance = MinimumDistances[j] + edge.weight;
                            if (newDistance < currentMinimum)
                            {
                                MinimumDistances[edge.EndVertex] = newDistance;
                                AdjustmentMade = true;
                            }

                        }
                    }
                    if (i == Verticies)
                    {
                        return AdjustmentMade;
                    }
                }


                return false;
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
        //        var WeightedEdge = new WeightedEdge
        //        {
        //            EndVertex = (long)edge[1] - 1,
        //            weight = edge[2]
        //        };
        //        graphInstance.AddDirectedEdge((long)edge[0] - 1, WeightedEdge);
        //    }

        //    var componentCount = graphInstance.HasNegativeCycle();
        //    //sortedGraph.Reverse();
        //    if (componentCount)
        //    {
        //        Console.WriteLine(1);
        //    }
        //    else
        //    {
        //        Console.WriteLine(0);
        //    }

        //}

    }
}




