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

        public class Node
        {
            public int value { get; set; }
            public List<Node> Nodes = new List<Node>();
        }

        public class Swap
        {
            public int start { get; set; }
            public int end { get; set; }
        }


        public static void CheckHeap(long[] arr, int index)
        {
            if (index * 2 + 1 < arr.Length)
            {
                //check left child
                var RChild = arr[index * 2 +1];
                
                //is Lchild the smallest
                if (RChild < arr[index])
                {
                    Console.WriteLine("not sorted");
                }
                else if (RChild > arr[index])
                {
                    CheckHeap(arr, index * 2 +1);
                }
            }
            if (index * 2 < arr.Length)
            {
                //check left child
                var LChild = arr[index * 2];

                //is Lchild the smallest
                if (LChild < arr[index])
                {
                    Console.WriteLine("not sorted");
                }
                else if (LChild > arr[index])
                {
                    CheckHeap(arr, index * 2);
                }
            }

        }




        public static void DownShift(long nodeValue, int index, long[] arrpointer, List<Swap> swaps)
        {
            var currentPosition = index;
            var notChecked = true;
            //check current position has two leaf nodes
           while (currentPosition*2 +1 < arrpointer.Length && notChecked){
                //check left child
                var LChild = arrpointer[currentPosition * 2];
                var RChild = arrpointer[currentPosition * 2 +1];
                //is Lchild the smallest
                if (LChild <= RChild && LChild < nodeValue)
                {
                    arrpointer[currentPosition] = arrpointer[currentPosition * 2];
                    arrpointer[currentPosition * 2] = nodeValue;
                    swaps.Add(new Swap
                    {
                        start = currentPosition,
                        end = currentPosition * 2
                    });
                    currentPosition = currentPosition * 2;
                }
                //is RChild the smallest
                else if(RChild <= LChild && RChild < nodeValue)
                {
                    arrpointer[currentPosition] = arrpointer[currentPosition * 2 +1];
                    arrpointer[currentPosition * 2 +1] = nodeValue;
                    swaps.Add(new Swap
                    {
                        start = currentPosition,
                        end = currentPosition * 2 +1
                    });
                    currentPosition = currentPosition * 2 +1;
                }
                if (LChild >= nodeValue && RChild >= nodeValue)
                {
                    notChecked = false;
                }
            }
           //check if their is a single leaf node
            if(currentPosition * 2  < arrpointer.Length   && arrpointer[currentPosition * 2] < nodeValue)
            {
                var temp = nodeValue;
                arrpointer[currentPosition] = arrpointer[currentPosition * 2];
                arrpointer[currentPosition * 2] = temp;
                swaps.Add(new Swap
                {
                    start = currentPosition,
                    end = currentPosition * 2
                });
                currentPosition = currentPosition * 2;
            }
            //check right child
        }



        public static long[] MinHeapSort(long[] arr)
        {
            //will use array indexing of 1
            var n= (int)Math.Floor((arr.Length -1)/ (double)(2));
            var swaps = new List<Swap>();
            for(var i = n; i>=1; i--)
            {
                var node = arr[i];
                DownShift(node, i, arr, swaps );
            }
            //DownShift(arr[1], 1, arr, swaps);

            Console.WriteLine(swaps.Count());
            foreach(var swap in swaps)
            {
                Console.WriteLine($"{swap.start - 1} {swap.end - 1}");
            }
            if (arr.Length >= 1 && (swaps.Count > (arr.Length - 1) * 4))
            {
                Console.WriteLine("too many swaps");
            }
            return arr;
        }




        static void Main(string[] args)
        {
            //generate the array with the size of the heap at index 0
            var number = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c));
            var arr = number.Concat(Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c))).ToArray();


            //while (true)
            //{
            //    var random = new Random();
            //    var ArrLength = random.Next(0, 1000000);
            //    var newArray = new long[ArrLength];
            //    var notSorted = new long[ArrLength];
            //    for (var i = 0; i < ArrLength; i++)
            //    {
            //        newArray[i] = random.Next(0, 1000000);
            //        notSorted[i] = newArray[i];
            //    }
            //    MinHeapSort(newArray);

            //    CheckHeap(newArray, 1);
            //}


            var res = MinHeapSort(arr);
            //Console.WriteLine(res);
        }
    }
}




