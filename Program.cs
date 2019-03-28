using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Addition
{


    class Program
    {
        public static char[] CountingSort(char[] InputStr)
        {
            var ACount = 0;
            var CCount = 0;
            var GCount = 0;
            var TCount = 0;

            var CountingSortArr = new int[4];
            //perform counting sort
            for (var i = 0; i < InputStr.Length; i++)
            {

                if (InputStr[i] == 'A')
                {
                    ACount++;
                    CountingSortArr[0]++;
                }
                else if (InputStr[i] == 'C')
                {
                    CCount++;
                    CountingSortArr[1]++;
                }
                else if (InputStr[i] == 'G')
                {
                    GCount++;
                    CountingSortArr[2]++;
                }
                else if (InputStr[i] == 'T')
                {
                    TCount++;
                    CountingSortArr[3]++;
                }
            }

            var SortedStr = new char[InputStr.Length];
            SortedStr[0] = '$';
            for (var i = 1; i < InputStr.Length; i++)
            {
                while (ACount > 0)
                {
                    SortedStr[i] = 'A';
                    ACount--;
                    i++;
                }
                while (CCount > 0)
                {
                    SortedStr[i] = 'C';
                    CCount--;
                    i++;
                }
                while (GCount > 0)
                {
                    SortedStr[i] = 'G';
                    GCount--;
                    i++;
                }
                while (TCount > 0)
                {
                    SortedStr[i] = 'T';
                    TCount--;
                    i++;
                }
                if (i < InputStr.Length - 1)
                {
                    throw new Exception("non ACGT chars in input String");
                }
            }

            return SortedStr;
        }

        //NB both CalcOrder and Sorting could be done in One Step and require one less interation through the input Str
        //These havve been calculated separately for clarity sake however could be refactored
        //the Length of each Occurance Array would then subsitute the Count vars.
        public static int[] CalcOrder(char[] unsortedStr)
        {

            var AOccurances = new Queue<int>();
            var COccurances = new Queue<int>();
            var GOccurances = new Queue<int>();
            var TOccurances = new Queue<int>();

            //for SortStr.Length-1 as las char will be $
            for (var i = 0; i < unsortedStr.Length - 1; i++)
            {
                if (unsortedStr[i] == 'A')
                    AOccurances.Enqueue(i);
                else if (unsortedStr[i] == 'C')
                    COccurances.Enqueue(i);
                else if (unsortedStr[i] == 'G')
                    GOccurances.Enqueue(i);
                else
                    TOccurances.Enqueue(i);
            }

            var OrderArray = new int[unsortedStr.Length];
            //first letter will be the $ from the end of the string
            OrderArray[0] = unsortedStr.Length - 1;

            for (var i = 1; i < unsortedStr.Length; i++)
            {
                while (AOccurances.Any())
                {
                    OrderArray[i] = AOccurances.Dequeue();
                    i++;
                }
                while (COccurances.Any())
                {
                    OrderArray[i] = COccurances.Dequeue();
                    i++;
                }
                while (GOccurances.Any())
                {
                    OrderArray[i] = GOccurances.Dequeue();
                    i++;
                }
                while (TOccurances.Any())
                {
                    OrderArray[i] = TOccurances.Dequeue();
                    i++;
                }
                if (i < unsortedStr.Length - 1)
                {
                    throw new Exception("non ACGT chars in input String");
                }
            }
            return OrderArray;
        }




        public static int[] CalulateClass(char[] sortedStr, int[] Order)
        {
            var ClassArr = new int[sortedStr.Length];

           
            char CurrentLetter = '$'; 
            var currentClass = 0;
            for(var i=0; i< sortedStr.Length; i++)
            {
                //find the position of the current letter in the unsorted string using the orderArray;
                var Position = Order[i];

                //move through the sorted string, each time the letter changes increase the currentClass count;
                //and update the current Letter
                if(CurrentLetter != sortedStr[i])
                {
                    CurrentLetter = sortedStr[i];
                    currentClass++;
                }

                ClassArr[Position] = currentClass;

            }

            return ClassArr;
        }


        public static int[] SortDoubled(char[] unsortedStr, int L, int[] Order, int[] Class)
        {
            var WrdLngth = unsortedStr.Length;

            var CountArr = new int[WrdLngth];

            var newOrder = new int[WrdLngth];

            //perform a counting sort of the classes
            for(var i = 0; i < WrdLngth; i++)
            {
                CountArr[Class[i]]++;
            }
            
            //compute partial sums of count arrays
            for(var i=1; i <WrdLngth; i++)
            {
                CountArr[i] += CountArr[i - 1];
            }

            for(var i = WrdLngth-1; i>=0; i--)
            {
                var start = (Order[i] - L + WrdLngth) % WrdLngth;

                var curClass = Class[start];

                CountArr[curClass]--;

                newOrder[CountArr[curClass]] = start;
            }

            return newOrder;
        }

        public static int[] UpdateClasses(int[] newOrder, int[] Class, int L)
        {
            var WrdLng = newOrder.Length;

            var newClass = new int[WrdLng];

            var startingPos = newOrder[0];

            newClass[startingPos] = 0;

            for( var i = 1; i<WrdLng; i++)
            {
                var cur = newOrder[i];
                var prev = newOrder[i - 1];

                var mid = (cur + L) % WrdLng;
                var midPrev = (prev + L) % WrdLng;

                if (Class[cur] != Class[prev] || Class[mid] != Class[midPrev])
                    newClass[cur] = newClass[prev] + 1;
                else
                    newClass[cur] = newClass[prev];
            }
            return newClass;
        }


        public static int[] StrToSuffixArr(char[] InputStr)
        {
            var wrdLng = InputStr.Length;
            var SortedStr = CountingSort(InputStr);
            var OrderArr = CalcOrder(InputStr);
            var ClassArr = CalulateClass(SortedStr, OrderArr);
            var L = 1;

            while (L < wrdLng)
            {
                OrderArr = SortDoubled(InputStr, L, OrderArr, ClassArr);
                ClassArr = UpdateClasses(OrderArr, ClassArr, L);
                L *= 2;
            }



            return OrderArr;
        }

        public static int[] InvertArray(int[] ArrayToInvert )
        {
            var InvertedArray = new int[ArrayToInvert.Length];

            for( var i =0;i <ArrayToInvert.Length; i++)
            {
                InvertedArray[ArrayToInvert[i]] = i;
            }

            return InvertedArray;
        }



        public static int GetNextLCP(char[] InputStr, int a, int b, int KnownPrefixLength)
        {
            var wrdLng = InputStr.Length;

            var count = (KnownPrefixLength > 0)? KnownPrefixLength : 0;
            
            while (a+count < wrdLng && b+count < wrdLng)
            {
                if (InputStr[a + count] == InputStr[b + count])
                {
                    count++;
                }
                else break;

            }
            return count;
        }


        public static int[] GetLCPArray(int[] SuffixArray, char[] InputStr)
        {
            var WrdLng = InputStr.Length;

            var LCPArray = new int[WrdLng-1];

            var PosInOrder = InvertArray(SuffixArray);

            var Suffix = PosInOrder[0];

            var LeftIndex = 0;
            var RightIndex = 1;
            var currentLCP = GetNextLCP(InputStr, LeftIndex, RightIndex,0);

            
            for(var i =0; i< WrdLng; i++)
            {
                //LCPArray[i] = currentLCP;
                //LeftIndex = SuffixArray[LeftIndex + 1];
                //RightIndex = SuffixArray[RightIndex + 1];
                //var nextLCP = GetNextLCP(InputStr, LeftIndex, RightIndex, currentLCP - 1);

                var OrderIndex = PosInOrder[Suffix];
                if (OrderIndex == WrdLng - 1)
                {
                    currentLCP = 0;
                    Suffix = (Suffix + 1) % WrdLng;
                    continue;
                }
                var nextSuffix = SuffixArray[OrderIndex + 1];
                currentLCP = GetNextLCP(InputStr, Suffix, nextSuffix, currentLCP - 1);
                LCPArray[OrderIndex] = currentLCP;
                Suffix = (Suffix + 1) % WrdLng;
            }
            return LCPArray;
        }


        static void Main(string[] args)
        {
            var InputString = (Console.ReadLine() + "$").ToCharArray();

            var SuffixArray = StrToSuffixArr(InputString);

            var LCPArray = GetLCPArray(SuffixArray, InputString);

            foreach(var ord in LCPArray)
            {
                Console.Write(ord + " ");
            }
        }  
    }
}




