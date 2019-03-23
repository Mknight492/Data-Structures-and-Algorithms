using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Addition
{


    class Kuskals
    {

        //


        public class DisjointSetNode
        {
            public DisjointSetNode Parent { get; set; }
            public uint rank { get; set; }

            public DisjointSetNode()
            {
                Parent = this;
            }

            public DisjointSetNode Find()
            {
                //path compression
                if (Parent != this) Parent = Parent.Find();
                return Parent;
            }

            public bool IsUnionedWith(DisjointSetNode other)
            {
                return Find() == other.Find();
            }

            public void Union(DisjointSetNode other)
            {
                var root1 = Find();
                var root2 = other.Find();

                if (root1 == root2)
                    return;

                if (root1.rank > root2.rank)
                {
                    root2.Parent = root1;
                }
                else
                {
                    root1.Parent = root2;

                    if (root1.rank == root2.rank)
                    {
                        root2.rank++;
                    }
                }

            }
        }








        public class WeightedEdge
        {
            public long EndVertex { get; set; }
            public ulong weight { get; set; }
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

            }

            public void AddUndirectedEdge(long source, WeightedEdge data)
            {
                if (!Adj[source].Contains(data))
                    Adj[source].Add(data);
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




        }














        public static double ShortestSpanningPath(List<Edge> ListOfEdges, int numberOfPoints)
        {
            var SetArray = new DisjointSetNode[numberOfPoints];

            for (var i = 0; i < numberOfPoints; i++)
            {
                SetArray[i] = new DisjointSetNode();
            }

            double total = 0;

            for (var i = 0; i < ListOfEdges.Count(); i++)
            {
                var currentEdge = ListOfEdges[i];
                var LSidedSet = SetArray[currentEdge.StartingV].Find();
                var RSidedSet = SetArray[currentEdge.FinishingV].Find();

                if (LSidedSet != RSidedSet)
                {
                    total += currentEdge.Weight;
                    LSidedSet.Union(RSidedSet);
                }
            }
            return total;
        }








        public class Point
        {
            public int PointA { get; set; }
            public int PointB { get; set; }
        }

        public class Edge
        {
            public int StartingV { get; set; }
            public int FinishingV { get; set; }
            public double Weight { get; set; }
        }

        //static void Main(string[] args)
        //{
        //    //determine the number of points
        //    var numberOfPoints = Convert.ToInt32(Console.ReadLine());

        //    //make an array of points and read in the points from the console
        //    var pointsArray = new Point[numberOfPoints];

        //    for (var i = 0; i < numberOfPoints; i++)
        //    {
        //        var pointInput = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt32(c));
        //        pointsArray[i] = new Point
        //        {
        //            PointA = pointInput[0],
        //            PointB = pointInput[1]
        //        };
        //    }

        //    //make a graph with the number of points

        //    var graphInstance = new Graph(numberOfPoints);

        //    //generate all possible Edges

        //    var ListOfEdges = new List<Edge>();
        //    for (var i = 0; i < numberOfPoints; i++)
        //    {
        //        for (var j = i + 1; j < numberOfPoints; j++)
        //        {

        //            var xSquared = Math.Pow(pointsArray[i].PointA - pointsArray[j].PointA, 2);
        //            var ySquared = Math.Pow(pointsArray[i].PointB - pointsArray[j].PointB, 2);
        //            var EdgeWeight = Math.Sqrt(xSquared + ySquared);

        //            ListOfEdges.Add(new Edge
        //            {
        //                StartingV = i,
        //                FinishingV = j,
        //                Weight = EdgeWeight
        //            });
        //        }
        //    }

        //    ListOfEdges = ListOfEdges.OrderBy(x => x.Weight).ToList();


        //    var Path = ShortestSpanningPath(ListOfEdges, numberOfPoints);
        //    Console.WriteLine(Math.Round(Path, 9));

        //}

    }
}




