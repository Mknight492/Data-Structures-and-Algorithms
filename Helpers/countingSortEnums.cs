using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addition.Helpers
{
    class countingSortEnums
    {
        public enum NucleoTide { A, C, G, T }

        public static char[] CountingSort(char[] InputStr)
        {
            var enumCount = Enum.GetValues(typeof(NucleoTide)).Length;

            var countArr = new int[enumCount];

            for (var i = 0; i < InputStr.Length - 1; i++)
            {
                //NucleoTide myNucleoTide;
                var res = Enum.TryParse(InputStr[i].ToString(), out NucleoTide myNucleotide);

                if (!res)
                {
                    throw new Exception("invalid Character in string");
                }

                var myNucleoTideInt = (int)myNucleotide;
                countArr[myNucleoTideInt]++;
            }

            var sortArr = new char[InputStr.Length];
            sortArr[0] = '$';

            var currentLetterCount = 1;
            for (var i = 0; i < countArr.Length; i++)
            {
                while (countArr[i] > 0)
                {
                    NucleoTide currentLetter = (NucleoTide)i;
                    var nucAsChar = currentLetter.ToString()[0];
                    sortArr[currentLetterCount] = nucAsChar;
                    currentLetterCount++;
                    countArr[i]--;
                }
            }


            return sortArr;
        }

        //NB both CalcOrder and Sorting could be done in One Step and require one less interation through the input Str
        //These havve been calculated separately for clarity sake however could be refactored
        //the Length of each Occurance Array would then subsitute the Count vars.

        public static int[] CalcOrder(char[] unsortedStr)
        {

            var enumCount = Enum.GetValues(typeof(NucleoTide)).Length;

            var PositionQueues = new Queue<int>[enumCount];
            for (var i = 0; i < enumCount; i++)
            {
                PositionQueues[i] = new Queue<int>();
            }


            //for SortStr.Length-1 as las char will be $
            for (var i = 0; i < unsortedStr.Length - 1; i++)
            {
                var res = Enum.TryParse(unsortedStr[i].ToString(), out NucleoTide myNucleotide);

                if (!res)
                {
                    throw new Exception("invalid Character in string");
                }

                var myNucleoTideInt = (int)myNucleotide;
                PositionQueues[myNucleoTideInt].Enqueue(i);
            }

            var OrderArray = new int[unsortedStr.Length];
            //first letter will be the $ from the end of the string
            OrderArray[0] = unsortedStr.Length - 1;
            var CurrentLetterCount = 1;
            for (var i = 0; i < PositionQueues.Length; i++)
            {
                while (PositionQueues[i].Any())
                {
                    OrderArray[CurrentLetterCount] = PositionQueues[i].Dequeue();
                    CurrentLetterCount++;
                }

            }
            return OrderArray;
        }
    }
}
