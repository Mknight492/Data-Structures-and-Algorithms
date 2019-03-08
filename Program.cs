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
        
        public class ProcessingTask
        {
            public long processor { get; set; }
            public UInt64 task { get; set; }
        }


        public class MinHeap
        {
            private ProcessingTask[] heap;
            private long count;

            public MinHeap(long arrLength)
            {
                heap = new ProcessingTask[arrLength];
                //MinHeapSort(heap);
                count = 0;
            }

            public bool IsEmpty()
            {
                if (count > 0) return false;
                return true;
            }

            public bool IsFull()
            {
                if (count != heap.Length) return false;
                return true;
            }

            public ProcessingTask getMin()
            {
                if (!IsEmpty())
                {
                    var minValue= heap[1];
                    heap[1] = heap[count];
                    DownShift(heap[1], 1, heap);
                    count--;
                    return minValue;
                }
                else
                {
                    throw new Exception("heap is empty");
                }
                
            }
            public ProcessingTask peekMin()
            {
                if (!IsEmpty())
                {
                    return heap[1];
                }
                else
                {
                    throw new Exception("heap is empty");
                }
            }

            public List<ProcessingTask> getAllMin()
            {
                if (IsEmpty())
                {
                    throw new Exception("heap is empty");
                }
                var currentMin = peekMin().task;
                var AllMin = new List<ProcessingTask>();
                while (!IsEmpty() && peekMin().task == currentMin)
                {
                    AllMin.Add(getMin());
                }
                return AllMin.OrderBy(x => x.processor).ToList();
            }

            public void AddElement(ProcessingTask element)
            {
                //attach at the bottom of heap and sift up;
                if (count >= heap.Length){
                    throw new Exception("heap is full");
                }
                count++;
                heap[count] = element;
                UpShift(element, count, heap);
                
                

            }
        }

        public static void UpShift(ProcessingTask nodeValue, long index, ProcessingTask[] arrpointer)
        {
            if (arrpointer.Length == 1 || index == 1) return;
            var parentIndex = (long)Math.Floor(index / (double)2);

            while(index != 1 && arrpointer[index].task < arrpointer[parentIndex].task)
            {
                var temp = arrpointer[parentIndex];
                arrpointer[parentIndex] = nodeValue;
                arrpointer[index] = temp;
                index = parentIndex;
                parentIndex =(long)Math.Floor(index / (double)2);

            }
            
        }





        public static void DownShift(ProcessingTask nodeValue, long index, ProcessingTask[] arrpointer)
        {
            if (arrpointer.Length == 1) return;
            var currentPosition = index;
            var notChecked = true;
            //check current position has two leaf nodes
           while (currentPosition*2 +1 < arrpointer.Length && notChecked){
                //check left child
                var LChild = arrpointer[currentPosition * 2];
                var RChild = arrpointer[currentPosition * 2 +1];
                //is Lchild the smallest
                if (LChild.task <= RChild.task && LChild.task < nodeValue.task)
                {
                    arrpointer[currentPosition] = arrpointer[currentPosition * 2];
                    arrpointer[currentPosition * 2] = nodeValue;
                    currentPosition = currentPosition * 2;
                }
                //is RChild the smallest
                else if(RChild.task <= LChild.task && RChild.task < nodeValue.task)
                {
                    arrpointer[currentPosition] = arrpointer[currentPosition * 2 +1];
                    arrpointer[currentPosition * 2 +1] = nodeValue;
                    currentPosition = currentPosition * 2 +1;
                }
                if (LChild.task >= nodeValue.task && RChild.task >= nodeValue.task)
                {
                    notChecked = false;
                }
            }
           //check if their is a single leaf node
            if(currentPosition * 2  < arrpointer.Length   && arrpointer[currentPosition * 2].task < nodeValue.task)
            {
                var temp = nodeValue;
                arrpointer[currentPosition] = arrpointer[currentPosition * 2];
                arrpointer[currentPosition * 2] = temp;
                currentPosition = currentPosition * 2;
            }

        }



        public static ProcessingTask[] MinHeapSort(ProcessingTask[] arr)
        {
            //will use array indexing of 1
            var n= (long)Math.Floor((arr.Length -1)/ (double)(2));
 
            for(var i = n; i>=1; i--)
            {
                var node = arr[i];
                DownShift(node, i, arr);
            }
            return arr;
        }


        public static void ParallelProcessing(long processors, UInt64[] tasks)
        {
            //initial the heap with the a number of processors
            var myMinHeap = new MinHeap(processors +1);

            var processorCount = 0;
            var initialProcessedCount = 0;
            for (var i =0; processorCount < processors && i< tasks.Length; i++)
            {
                var nextTask =new ProcessingTask
                {
                    processor = processorCount,
                    task = tasks[i]
                };
                if(nextTask.task != 0)
                {
                    myMinHeap.AddElement(nextTask);
                    processorCount++;
                }
                initialProcessedCount++;
                Console.WriteLine($"{nextTask.processor} 0");
                
            }

            

            //initialise the heap and sort it.
            var currentTask = initialProcessedCount;
            var finishedAllTasks = (initialProcessedCount >= tasks.Length);
            ulong currentTime = 0;
            while (!finishedAllTasks)
            {
                if (!myMinHeap.IsEmpty())
                {
                    //get the all the current tasks the are finishing
                    var currentFinishingTasks = myMinHeap.getAllMin();
                    currentTime = currentFinishingTasks[0].task;
                    //then iterate over these, logging the processor and the time the current task took to finish
                    //then shedule new taks to the  minHeap (add the current time taken to the new task

                    var freeProcessors = currentFinishingTasks.Count();
                    processorCount = 0;
                    while (processorCount < freeProcessors && !finishedAllTasks)
                    {
                        var nextTask = new ProcessingTask
                        {
                            processor = currentFinishingTasks[processorCount].processor,
                            task = tasks[currentTask] + currentTime
                        };
                        if (nextTask.task != currentTime)
                        {
                            myMinHeap.AddElement(nextTask);
                            processorCount++;
                        }
                        currentTask++;

                        Console.WriteLine($"{nextTask.processor} {currentTime}");
                        if (currentTask == tasks.Length) finishedAllTasks = true;

                    }

                }
            }
            //find the shortest task(s)

            //add their time to the next test(s)
            //continue while the heap is not empty

        }



        static void Main(string[] args)
        {
            //generate the array with the size of the heap at index 0
            var numberProcessors = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c))[0];
            UInt64[] tasks = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToUInt64(c));








            ParallelProcessing(numberProcessors, tasks);
            //Console.WriteLine(res);
        }
    }
}




