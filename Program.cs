using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Addition
{


    class Program
    {

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

                for(long i =0; i< verticies; i++)
                {
                    Adj[i] = new List<long>();
                    ReversedAdj[i] = new List<long>();
                }
            }

            public void AddDirectedEdge(long source, long data)
            {
                if (!Adj[source].Contains(data))
                    Adj[source].Add(data);

                if (!ReversedAdj[data].Contains(source))
                    ReversedAdj[data].Add(source);

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

                for(long i = 0; i< Verticies; i++)
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

                foreach(var node in furtherAccessableNodes)
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
                var sortedNodes = new Stack<long>();

                for (long i = 0; i < Verticies; i++)
                {
                    if (!visted[i])
                        TopologicalSortRecursive(i, visted, sortedNodes);
                      

                }

                var sortedArray = new long[Verticies];
                var count = 0;
                while (sortedNodes.Any())
                {
                    sortedArray[count] = sortedNodes.Pop();
                    count++;
                }
                return sortedArray;


            }

            private void TopologicalSortRecursive(long i, bool[] visited, Stack<long> sortedNodes)
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
                    sortedNodes.Push(i);       
            }

            public int StronglyConnectedComponents()
            {
                //should acutally run TopologocialSort on AdjReversed but as we're just counting the number of SCC it doesn't matter.
                var topoSortedArray = TopologicalSort();


                var count = 0;
                var visited = new bool[Verticies];

                for(var i =0; i< topoSortedArray.Length; i++)
                {
                    if (!visited[topoSortedArray[i]])
                    {
                        SCCRecursive(topoSortedArray[i], visited);
                        count++;

                    }
                }

                return count;
            }

            private void SCCRecursive(long i, bool[] visited)
            {
                visited[i] = true;
                var furtherAccessableNodes = ReversedAdj[i];

                for(var j =0; j<furtherAccessableNodes.Count; j++)
                {
                    if(!visited[furtherAccessableNodes[j]])
                        SCCRecursive(furtherAccessableNodes[j], visited);
                }
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
                graphInstance.AddDirectedEdge(edge[0] - 1,edge[1] - 1);
            }

            var componentCount = graphInstance.StronglyConnectedComponents();
            //sortedGraph.Reverse();
            Console.WriteLine(componentCount);
        }
    }
}




