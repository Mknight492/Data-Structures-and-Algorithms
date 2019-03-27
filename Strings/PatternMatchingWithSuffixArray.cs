using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Addition
{


    class PatternMatchingWithSuffixArray
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
            for (var i = 0; i < sortedStr.Length; i++)
            {
                //find the position of the current letter in the unsorted string using the orderArray;
                var Position = Order[i];

                //move through the sorted string, each time the letter changes increase the currentClass count;
                //and update the current Letter
                if (CurrentLetter != sortedStr[i])
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
            for (var i = 0; i < WrdLngth; i++)
            {
                CountArr[Class[i]]++;
            }

            //compute partial sums of count arrays
            for (var i = 1; i < WrdLngth; i++)
            {
                CountArr[i] += CountArr[i - 1];
            }

            for (var i = WrdLngth - 1; i >= 0; i--)
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

            for (var i = 1; i < WrdLng; i++)
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


        public static char[] SuffixArrToBWTStr(int[] SuffixArray, char[] InputStr)
        {
            var WrdLng = InputStr.Length;

            var BWTString = new char[WrdLng];

            for (var i = 0; i < WrdLng; i++)
            {
                var indexInStr = (SuffixArray[i] + WrdLng - 1) % WrdLng;
                BWTString[i] = InputStr[indexInStr];
            }

            return BWTString;
        }

        public static List<int> BWTMatching(char[] Word, char[][] Patterns, int[] SuffixArray)
        {
            //initalisse counting sort arrays
            var AcountArray = new int[Word.Length];
            var CcountArray = new int[Word.Length];
            var GcountArray = new int[Word.Length];
            var TcountArray = new int[Word.Length];

            var AIndexArray = new List<int>();
            var CIndexArray = new List<int>();
            var GIndexArray = new List<int>();
            var TIndexArray = new List<int>();

            for (var i = 0; i < Word.Length; i++)
            {
                AcountArray[i] = -1;
                CcountArray[i] = -1;
                GcountArray[i] = -1;
                TcountArray[i] = -1;
            }

            var ACount = 0;
            var CCount = 0;
            var GCount = 0;
            var TCount = 0;

            //perform counting sort
            for (var i = 0; i < Word.Length; i++)
            {
                if (i > 0)
                {
                    AcountArray[i] = AcountArray[i - 1];
                    CcountArray[i] = CcountArray[i - 1];
                    GcountArray[i] = GcountArray[i - 1];
                    TcountArray[i] = TcountArray[i - 1];
                }

                if (Word[i] == 'A')
                {
                    AcountArray[i]++;
                    ACount++;
                    AIndexArray.Add(i);
                }
                else if (Word[i] == 'C')
                {
                    CcountArray[i]++;
                    CCount++;
                    CIndexArray.Add(i);
                }
                else if (Word[i] == 'G')
                {
                    GcountArray[i]++;
                    GCount++;
                    GIndexArray.Add(i);
                }
                else if (Word[i] == 'T')
                {
                    TcountArray[i]++;
                    TCount++;
                    TIndexArray.Add(i);
                }
            }

            var firstA = 1;
            var firstC = firstA + ACount;
            var firstG = firstC + CCount;
            var firstT = firstG + GCount;

            var setOfMatches = new HashSet<int>();

            foreach (var Pattern in Patterns)
            {
                var currentLetter = Pattern[0];
                var currentMin = 0;
                int currentMax = Word.Length - 1;

                var NoMatches = false;
                if (Pattern.Length == 1)
                {
                    if (currentLetter == 'A')
                        foreach (var index in AIndexArray)
                            setOfMatches.Add(SuffixArray[index] - 1);
                    else if (currentLetter == 'C')
                        foreach (var index in CIndexArray)
                            setOfMatches.Add(SuffixArray[index] - 1);
                    else if (currentLetter == 'G')
                        foreach (var index in GIndexArray)
                            setOfMatches.Add(SuffixArray[index] - 1);
                    else if (currentLetter == 'T')
                        foreach (var index in TIndexArray)
                            setOfMatches.Add(SuffixArray[index] - 1);
                }
                else
                {



                    for (var i = Pattern.Length - 1; i >= 0; i--)
                    {
                        var nextLetter = Pattern[i];
                        if (nextLetter == 'A')
                        {
                            var FirstOccurance = -1;
                            for (var j = currentMin; (j <= currentMax && FirstOccurance == -1); j++)
                            {
                                if (j == 0 && AcountArray[0] == 0) FirstOccurance = j;
                                else if (j > 0 && AcountArray[j] > AcountArray[j - 1]) FirstOccurance = j;
                            }
                            if (FirstOccurance != -1)
                            {
                                var nextMin = AcountArray[FirstOccurance];
                                if (nextMin == -1) nextMin = 0;
                                currentMin = firstA + nextMin;
                                currentMax = firstA + AcountArray[currentMax];
                            }
                            else
                            {
                                i = 0;
                                NoMatches = true;
                            }
                        }
                        else if (nextLetter == 'C')
                        {
                            var FirstOccurance = -1; ;
                            for (var j = currentMin; (j <= currentMax && FirstOccurance == -1); j++)
                            {
                                if (j == 0 && CcountArray[0] == 0) FirstOccurance = j;
                                else if (j > 0 && CcountArray[j] > CcountArray[j - 1]) FirstOccurance = j;
                            }
                            if (FirstOccurance != -1)
                            {
                                var nextMin = CcountArray[FirstOccurance];
                                if (nextMin == -1) nextMin = 0;
                                currentMin = firstC + nextMin;
                                currentMax = firstC + CcountArray[currentMax];
                            }
                            else
                            {
                                i = 0;
                                NoMatches = true;
                            }
                        }
                        else if (nextLetter == 'G')
                        {
                            var FirstOccurance = -1;
                            for (var j = currentMin; (j <= currentMax && FirstOccurance == -1); j++)
                            {
                                if (j == 0 && GcountArray[0] == 0) FirstOccurance = j;
                                else if (j > 0 && GcountArray[j] > GcountArray[j - 1]) FirstOccurance = j;
                            }
                            if (FirstOccurance != -1)
                            {
                                var nextMin = GcountArray[FirstOccurance];
                                if (nextMin == -1) nextMin = 0;
                                currentMin = firstG + nextMin;
                                currentMax = firstG + GcountArray[currentMax];
                            }
                            else
                            {
                                i = 0;
                                NoMatches = true;
                            }

                        }
                        else if (nextLetter == 'T')
                        {
                            var FirstOccurance = -1;
                            for (var j = currentMin; (j <= currentMax && FirstOccurance == -1); j++)
                            {
                                if (j == 0 && TcountArray[0] == 0) FirstOccurance = j;
                                else if (j > 0 && TcountArray[j] > TcountArray[j - 1]) FirstOccurance = j;
                            }
                            if (FirstOccurance != -1)
                            {
                                var nextMin = TcountArray[FirstOccurance];
                                if (nextMin == -1) nextMin = 0;
                                currentMin = firstT + nextMin;
                                currentMax = firstT + TcountArray[currentMax];
                            }
                            else
                            {
                                i = 0;
                                NoMatches = true;
                            }
                        }

                        if (i == 0 && !NoMatches)
                        {
                            for (var k = currentMin; k <= currentMax; k++)
                                setOfMatches.Add(SuffixArray[k]);
                        }
                    }
                }

            }

            return setOfMatches.ToList();


        }




        //static void Main(string[] args)
        //{
        //    var InputString = (Console.ReadLine() + "$").ToCharArray();

        //    var SuffixArray = StrToSuffixArr(InputString);

        //    var BWTString = SuffixArrToBWTStr(SuffixArray, InputString);

        //    var numberOfPatterns = Convert.ToInt32(Console.ReadLine());

        //    var Patterns = new char[numberOfPatterns][];


        //    var ArrayOfPattern = Console.ReadLine().Split(' ');
        //    for (var i = 0; i < numberOfPatterns; i++)
        //    {
        //        Patterns[i] = ArrayOfPattern[i].ToCharArray();
        //    }

        //    var matches = BWTMatching(BWTString, Patterns, SuffixArray);
        //    Console.WriteLine(string.Join(" ", matches));
        //}
    }
}




