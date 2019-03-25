using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Addition
{


    class KMP
    {

        public static int[] BorderCalc(char[] Pattern)
        {
            var BorderArray = new int[Pattern.Length];

            BorderArray[0] = 0;

            var CurrentBorderLength = 0;

            for (var i = 1; i < Pattern.Length; i++)
            {
                if (Pattern[CurrentBorderLength] == Pattern[i])
                {
                    CurrentBorderLength++;

                }
                else
                {
                    while (CurrentBorderLength > 0 && Pattern[CurrentBorderLength] != Pattern[i])
                    {
                        CurrentBorderLength = BorderArray[CurrentBorderLength - 1];
                    }
                    if (Pattern[i] == Pattern[CurrentBorderLength])
                        CurrentBorderLength++;
                }


                BorderArray[i] = CurrentBorderLength;

            }

            return BorderArray;
        }



        public static List<int> KnuthMorrisPratt(char[] Pattern, char[] StringToCheck)
        {
            var Breaker = new char[1] { '$' };
            var CombinedArray = Pattern.Concat(Breaker).Concat(StringToCheck).ToArray();

            var borderArray = BorderCalc(CombinedArray);

            var Matches = new List<int>();


            for (var i = Pattern.Length + 1; i < CombinedArray.Length; i++)
            {
                if (borderArray[i] != 0 && borderArray[i] % Pattern.Length == 0)
                {
                    Matches.Add(i - (Pattern.Length * 2));
                }
            }

            return Matches;
        }



        //static void Main(string[] args)
        //{

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

        //    //    var matchesViaKMPImplimentation = KnuthMorrisPratt(patternArray, letterArray);

        //    //    var matchesViaNaiveAlg = NaiveMatching( patternArray, letterArray);


        //    //    if(matchesViaNaiveAlg.Count != matchesViaKMPImplimentation.Count)
        //    //    {

        //    //        var incorrectMatches = KnuthMorrisPratt(patternArray, letterArray);
        //    //        cycles = 0;
        //    //    }

        //    // for(var i=0; i < matchesViaKMPImplimentation.Count; i++)
        //    //    { 
        //    //        if(matchesViaKMPImplimentation[i] != matchesViaNaiveAlg[i])
        //    //        cycles = 0;
        //    //        var incorrectMatches = KnuthMorrisPratt(patternArray, letterArray);
        //    //    }
        //    //    cycles++;

        //    //}

        //    var Pattern = Console.ReadLine().ToCharArray();

        //    var InputString = Console.ReadLine().ToCharArray();

        //    var BorderArray = BorderCalc(Pattern);


        //    var Occurrences = KnuthMorrisPratt(Pattern, InputString);


        //    Console.WriteLine(string.Join(" ", Occurrences));
        //}


        public static List<int> NaiveMatching(char[] Pattern, char[] letterArrray)
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




