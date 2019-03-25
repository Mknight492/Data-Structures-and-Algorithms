using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Addition
{


    class Program
    {
        public enum NucleoTide {A,C,G,T}

        public static char[]  CountingSort(char[] InputStr)
        {
            var enumCount = Enum.GetValues(typeof(NucleoTide)).Length;

            var countArr = new int[enumCount];

            for(var i = 0; i< InputStr.Length-1; i++)
            {
                //NucleoTide myNucleoTide;
                var res= Enum.TryParse(InputStr[i].ToString(), out NucleoTide myNucleotide);

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
            for(var i = 0; i< countArr.Length; i++)
            {
                while (countArr[i] > 0)
                {
                    NucleoTide currentLetter = (NucleoTide)i;
                    var nucAsChar =currentLetter.ToString()[0];
                    sortArr[currentLetterCount] = nucAsChar;
                    currentLetterCount++;
                    countArr[i]--;
                }
            }


            return sortArr ;
        }

        //NB both CalcOrder and Sorting could be done in One Step and require one less interation through the input Str
        //These havve been calculated separately for clarity sake however could be refactored
        //the Length of each Occurance Array would then subsitute the Count vars.

        public static int[] CalcOrder(char[] unsortedStr)
        {

            var enumCount = Enum.GetValues(typeof(NucleoTide)).Length;

            var PositionQueues = new Queue<int>[enumCount];
            for(var i =0; i< enumCount; i++)
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
                    OrderArray[CurrentLetterCount] =PositionQueues[i].Dequeue();
                    CurrentLetterCount++;
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



        public static int[] StrToSuffixArr(char[] InputStr)
        {
            return new int[0];
        }



        static void Main(string[] args)
        {
            var str = new Char[] { 'A', 'C', 'A', 'C','A', 'A', '$' };
            var SortedStr2 = CountingSort(str);
            var OrderArr2 = CalcOrder(str);
            var ClassArr = CalulateClass(SortedStr2, OrderArr2);
            char G = 'G';
            //var cycles = 0;
            //while (true)
            //{
            //    var randInst = new Random();
            //    var numberOfLetters = randInst.Next(1, 500);
            //    var letterArray = new char[numberOfLetters];

            //    var patternArray = new char[randInst.Next(1, numberOfLetters)];

            //    for (var i = 0; i < letterArray.Length; i++)
            //    {
            //        var nextLetter = randInst.Next(0, 3);
            //        if (nextLetter == 0)
            //            letterArray[i] = 'A';
            //        if (nextLetter == 1)
            //            letterArray[i] = 'C';
            //        if (nextLetter == 2)
            //            letterArray[i] = 'G';
            //        if (nextLetter == 3)
            //            letterArray[i] = 'T';
            //    }


            //    if(matchesViaNaiveAlg.Count != matchesViaKMPImplimentation.Count)
            //    {
                    
            //        var incorrectMatches = KnuthMorrisPratt(patternArray, letterArray);
            //        cycles = 0;
            //    }

            // for(var i=0; i < matchesViaKMPImplimentation.Count; i++)
            //    { 
            //        if(matchesViaKMPImplimentation[i] != matchesViaNaiveAlg[i])
            //        cycles = 0;
            //        var incorrectMatches = KnuthMorrisPratt(patternArray, letterArray);
            //    }
            //    cycles++;

            //}

            var InputString = Console.ReadLine().ToCharArray();

            var SuffixArray = StrToSuffixArr(InputString);


            Console.WriteLine(string.Join(" ", SuffixArray));
        }


        public static List<int> NaiveMatching( char[] Pattern, char[] letterArrray)
        {
            var MatchLocations = new List<int>();

            for (var i = 0; i <= letterArrray.Length - Pattern.Length; i++)
            {
                if (letterArrray[i] == Pattern[0])
                {
                    var match = true;
                    var count = 0;
                    for (var j = i; (j < letterArrray.Length && count < Pattern.Length); j++)
                    {

                        if (letterArrray[j] != Pattern[count])
                        {

                            match = false;
                        }
                        count++;
                    }
                    if (match)
                        MatchLocations.Add(i);
                }
            }


            return MatchLocations;
        }
    }
}




