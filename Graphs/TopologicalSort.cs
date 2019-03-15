using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Addition
{


    class TopologicalSort
    {

        public class Graph
        {
            private long Verticies;
            private List<long>[] Adj;

            public Graph(long verticies)
            {
                Verticies = verticies;
                Adj = new List<long>[verticies];

                for (long i = 0; i < verticies; i++)
                {
                    Adj[i] = new List<long>();
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

            public bool IsCyclic()
            {
                var visted = new bool[Verticies];
                var accessableNodes = new bool[Verticies];

                for (long i = 0; i < Verticies; i++)
                {
                    if (IsCyclicRecursive(i, visted, accessableNodes))
                        return true;

                }
                return false;
            }

            private bool IsCyclicRecursive(long i, bool[] visited, bool[] accessableNodes)
            {
                if (accessableNodes[i])
                    return true;

                if (visited[i])
                    return false;

                accessableNodes[i] = true;
                visited[i] = true;

                var furtherAccessableNodes = Adj[i];

                foreach (var node in furtherAccessableNodes)
                {
                    if (IsCyclicRecursive(node, visited, accessableNodes))
                        return true;
                }

                accessableNodes[i] = false;

                return false;
            }

            public long[] TopologicalSort()
            {
                var visted = new bool[Verticies];
                var sortedNodes = new List<long>();

                for (long i = 0; i < Verticies; i++)
                {
                    if (!visted[i])
                        TopologicalSortRecursive(i, visted, sortedNodes);


                }
                return sortedNodes.ToArray();


            }

            private void TopologicalSortRecursive(long i, bool[] visited, List<long> sortedNodes)
            {
                visited[i] = true;

                var furtherAccessableNodes = Adj[i];

                if (furtherAccessableNodes.Any())
                {
                    foreach (var node in furtherAccessableNodes)
                    {
                        if (!visited[node])
                            TopologicalSortRecursive(node, visited, sortedNodes);

                    }
                }
                sortedNodes.Add(i);
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
        //        graphInstance.AddDirectedEdge(edge[0] - 1, edge[1] - 1);
        //    }

        //    var sortedGraph = graphInstance.TopologicalSort();
        //    //sortedGraph.Reverse();
        //    for (var i = sortedGraph.Length - 1; i >= 0; i--)
        //    {
        //        Console.Write((sortedGraph[i] + 1) + " ");
        //    }
        //}
    }
}




