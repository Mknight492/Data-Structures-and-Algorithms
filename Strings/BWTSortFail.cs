using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Addition
{


    class BWTSortClass
    {


        public static List<int> BWTMatching(char[] Word, char[][] Patterns)
        {
            //initalisse counting sort arrays
            var AcountArray = new int[Word.Length];
            var CcountArray = new int[Word.Length];
            var GcountArray = new int[Word.Length];
            var TcountArray = new int[Word.Length];

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
                }
                else if (Word[i] == 'C')
                {
                    CcountArray[i]++;
                    CCount++;
                }
                else if (Word[i] == 'G')
                {
                    GcountArray[i]++;
                    GCount++;
                }
                else if (Word[i] == 'T')
                {
                    TcountArray[i]++;
                    TCount++;
                }
            }

            var firstA = 1;
            var firstC = firstA + ACount;
            var firstG = firstC + CCount;
            var firstT = firstG + GCount;

            var numberOfMatchesPerPattern = new List<int>();

            foreach (var Pattern in Patterns)
            {
                var currentLetter = Pattern[0];
                var currentMin = 0;
                int currentMax = Word.Length - 1;

                var NoMatches = false;
                if (Pattern.Length == 1)
                {
                    if (currentLetter == 'A')
                        numberOfMatchesPerPattern.Add(ACount);
                    else if (currentLetter == 'C')
                        numberOfMatchesPerPattern.Add(CCount);
                    else if (currentLetter == 'G')
                        numberOfMatchesPerPattern.Add(GCount);
                    else if (currentLetter == 'T')
                        numberOfMatchesPerPattern.Add(TCount);
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
                            numberOfMatchesPerPattern.Add(currentMax - currentMin + 1);
                        }
                        else if (i == 0 && NoMatches)
                        {
                            numberOfMatchesPerPattern.Add(0);
                        }
                    }
                }

            }

            return numberOfMatchesPerPattern;


        }


        //static void Main(string[] args)
        //{
        //    var InputString = Console.ReadLine().ToCharArray();

        //    var numberOfPatterns = Convert.ToInt32(Console.ReadLine());

        //    var ListOfPatterns = new List<char[]>();

        //    var ArrayOfAllPatterns = Array.ConvertAll(Console.ReadLine().Split(' '), x => x.ToCharArray());



        //    var res = BWTMatching(InputString, ArrayOfAllPatterns);

        //    Console.WriteLine(string.Join(" ", res));

        //    //var cycles = 0;
        //    //while (true)
        //    //{
        //    //    var randInst = new Random();
        //    //    var numberOfLetters = randInst.Next(1, 500);
        //    //    var letterArray = new char[numberOfLetters];

        //    //    var patternArray = new char[randInst.Next(1, numberOfLetters)];

        //    //    for (var i = 0; i < letterArray.Length; i++)
        //    //    {
        //    //        var nextLetter = randInst.Next(0, 3);
        //    //        if (nextLetter == 0)
        //    //            letterArray[i] = 'A';
        //    //        if (nextLetter == 1)
        //    //            letterArray[i] = 'C';
        //    //        if (nextLetter == 2)
        //    //            letterArray[i] = 'G';
        //    //        if (nextLetter == 3)
        //    //            letterArray[i] = 'T';
        //    //    }

        //    //    for (var i = 0; i < patternArray.Length; i++)
        //    //    {
        //    //        var nextLetter = randInst.Next(0, 3);
        //    //        if (nextLetter == 0)
        //    //            patternArray[i] = 'A';
        //    //        if (nextLetter == 1)
        //    //            patternArray[i] = 'C';
        //    //        if (nextLetter == 2)
        //    //            patternArray[i] = 'G';
        //    //        if (nextLetter == 3)
        //    //            patternArray[i] = 'T';
        //    //    }
        //    //    var PatternArrayList = new char[1][];
        //    //    PatternArrayList[0] = patternArray;

        //    //    var BWTstring = BurrowsWheelerTransformation.BWT((new string(letterArray) + "$").ToCharArray()).ToCharArray();

        //    //    var matchesViaBWTImplimentation = BWTMatching(BWTstring, PatternArrayList)[0];

        //    //    var matchesViaNaiveAlg = NaiveMatching(letterArray, PatternArrayList[0]);


        //    //    if (matchesViaBWTImplimentation != matchesViaNaiveAlg)
        //    //    {

        //    //        var incorrectMatches = BWTMatching(BWTstring, PatternArrayList);
        //    //        cycles = 0;
        //    //    }
        //    //    cycles++;

        //    //}
        //}

        public static int NaiveMatching(char[] letterArrray, char[] Pattern)
        {
            var numberOfMatches = 0;

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
                        numberOfMatches++;
                }
            }


            return numberOfMatches;
        }

    }
}




