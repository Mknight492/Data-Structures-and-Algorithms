using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Addition
{


    class Program
    {
        public class HeapNode
        {
            public long Vertex { get; set; }
            public ulong Distance { get; set; }
        }

        public class MinHeap
        {
            private HeapNode[] heap;
            private int[] indexes;
            private long count;

            public MinHeap(HeapNode[] Heap, int[] Indexes)
            {
                heap = new HeapNode[1].Concat(Heap).ToArray();
                indexes = Indexes;
                count = Indexes.Length;
            }

            public bool IsEmpty()
            {
                return (count == 0);
            }


            public HeapNode getMin()
            {
                if (!IsEmpty())
                {
                    var minValue = heap[1];
                    var lastNode = heap[count];
                    //for debuggin
                    indexes[heap[1].Vertex] = -1;
                    heap[1] = lastNode;

                    indexes[lastNode.Vertex] = 1;


                    DownShift(1);
                    //for debugging
                    heap[count] = null;

                    count--;
                    return minValue;
                }
                else
                {
                    throw new Exception("heap is empty");
                }

            }


            public void UpShift(long index)
            {
                if (heap.Length == 1 || index == 1) return;
                var parentIndex = (long)Math.Floor(index / (double)2);

                while (index != 1 && heap[index].Distance < heap[parentIndex].Distance)
                {

                    //need to swap the indexs of the heaps array and the refArray
                    var currentNode = heap[index];
                    var parentNode = heap[parentIndex];

                    indexes[currentNode.Vertex] = (int)parentIndex;
                    indexes[parentNode.Vertex] = (int)index;

                    swap((int)index, (int)parentIndex);

                    index = parentIndex;
                    parentIndex = (long)Math.Floor(index / (double)2);

                }

            }





            public void DownShift(long index)
            {
                if (heap.Length == 1) return;
                var currentPosition = index;
                var Checked = false;
                //check current position has two leaf nodes
                while (currentPosition * 2 < count && !Checked)
                {
                    //check left child
                    var LChildIndex = currentPosition * 2;
                    var RChildIndex = currentPosition * 2 + 1;

                    var smallestIndex = currentPosition;
                    var LChildIsSmallest = false;
                    //is Lchild the smallest
                    if (LChildIndex <= count && heap[LChildIndex].Distance < heap[smallestIndex].Distance)
                    {
                        smallestIndex = LChildIndex;
                        LChildIsSmallest = true;
                    }
                    //is RChild the smallest
                    if (RChildIndex <= count && heap[RChildIndex].Distance < heap[smallestIndex].Distance)
                    {
                        smallestIndex = RChildIndex;
                        LChildIsSmallest = false;
                    }
                    if (heap[LChildIndex].Distance >= heap[currentPosition].Distance && (RChildIndex > count || heap[RChildIndex].Distance >= heap[currentPosition].Distance))
                    {
                        Checked = true;
                    }
                    if (smallestIndex != currentPosition)
                    {
                        var smallestNode = heap[smallestIndex];
                        var currentNode = heap[currentPosition];

                        var temp = indexes[smallestNode.Vertex];
                        indexes[smallestNode.Vertex] = indexes[currentNode.Vertex];
                        indexes[currentNode.Vertex] = temp;

                        swap((int)currentPosition, (int)smallestIndex);
                        if (LChildIsSmallest)
                        {
                            currentPosition = currentPosition * 2;
                        }
                        else
                        {
                            currentPosition = currentPosition * 2 + 1;
                        }
                    }

                }


            }

            private void swap(int indexA, int indexB)
            {
                var tempNode = heap[indexA];
                heap[indexA] = heap[indexB];
                heap[indexB] = tempNode;
            }

            public void updateDistance(ulong newDistance, int index)
            {
                int indexInHeap = indexes[index];

                var nodeToUpdate = heap[indexInHeap];

                nodeToUpdate.Distance = newDistance;

                UpShift(indexInHeap);
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




            public ulong Djikstras(long a, long b)
            {
                //array which keeps track of the current shortest path for the corresponding vertex i
                var DistanceArray = new ulong[Verticies];

                //array which keeps track of which  vertex points  has the shortest path to the corresponding vertex i
                var PreviousVerticies = new long[Verticies];

                //array which points the reference array, is sorted as a min heap
                var indexes = new int[Verticies];

                //array which sort the current distance to each node
                //also points to it's location in the minHeap so that anywhere in the heap can be accessed in 0(1) time
                //this means when the distance changes the minDistance also changes
                var referenceArray = new HeapNode[Verticies];

                //shortest path checker
                var SPT = new bool[Verticies];

                for (var i = 0; i < Verticies; i++)
                {
                    DistanceArray[i] = ulong.MaxValue; // -1 is the proxy for infinity here

                    referenceArray[i] = (new HeapNode
                    {
                        Vertex = i,
                        Distance = long.MaxValue,
                    });

                    indexes[i] = i + 1;
                }

                //make the starting point distance 0
                DistanceArray[a] = 0;
                referenceArray[a].Distance = 0;

                //swap minHeap[a] and minHeap[0] so it's sorted

                var temp = referenceArray[0];
                referenceArray[0] = referenceArray[a];
                referenceArray[a] = temp;

                var temp2 = indexes[0];
                indexes[0] = indexes[a];
                indexes[a] = temp2;

                //make minHeap
                var minHeap = new MinHeap(referenceArray, indexes);


                //need to determine which nodes are reachable first from point a and only calculate from there!
                var accesableNodes = AccesableNodes(a);
                //ideally should check if node is accesable before cocntinuing

                while (!minHeap.IsEmpty())
                {
                    var nextMinNode = minHeap.getMin();
                    var nextAccesableNodes = accesableNodes[nextMinNode.Vertex];
                    SPT[nextMinNode.Vertex] = true;
                    foreach (var node in nextAccesableNodes)
                    {
                        if (SPT[node.EndVertex] != true)
                        {
                            var currentMinDistance = DistanceArray[node.EndVertex];
                            var distanceFromNextNode = DistanceArray[nextMinNode.Vertex] + node.weight;
                            if (distanceFromNextNode < currentMinDistance)
                            {
                                //update the distance array
                                DistanceArray[node.EndVertex] = distanceFromNextNode;

                                //update the minHeap.
                                minHeap.updateDistance(distanceFromNextNode, (int)node.EndVertex);
                                //minHeap.DownShift(editedNode);
                            }
                        }
                    }
                }

                //if(DistanceArray[b] != long.MaxValue)
                //{
                return DistanceArray[b];
                //}
                //else
                //{
                //    return -1;
                //}

            }

        }

        public static void NavieAlgorithm()
        {

        }






        static void Main(string[] args)
        {
            var input = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c));
            long n = input[0]; //vertices
            var m = input[1]; //edges

            var graphInstance = new Graph(n);

            for (var i = 0; i < m; i++)
            {
                var edge = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToUInt64(c));
                var WeightedEdge = new WeightedEdge
                {
                    EndVertex = (long)edge[1] - 1,
                    weight = edge[2]
                };
                graphInstance.AddDirectedEdge((long)edge[0] - 1, WeightedEdge);
            }

            var input2 = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c));
            var a = input2[0] - 1;
            var b = input2[1] - 1;
            var componentCount = graphInstance.Djikstras(a, b);
            //sortedGraph.Reverse();
            if (componentCount != ulong.MaxValue)
            {
                Console.WriteLine(componentCount);
            }
            else
            {
                Console.WriteLine(-1);
            }

        }
    }
}




