using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addition.Helpers
{
    class countingSortMisc
    {
        public static char[] NucCountingSort(char[] InputStr)
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
    }
}
