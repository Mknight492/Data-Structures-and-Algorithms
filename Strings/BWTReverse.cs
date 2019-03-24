using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Addition
{


    class BWTInverseClass
    {

        public static string BWTInverse(char[] Word)
        {
            var CurrentCountByLetter = new int[Word.Length];

            var ACount = 0;
            var CCount = 0;
            var GCount = 0;
            var TCount = 0;

            //counting sort for each of the possible letters
            for (var i = 0; i < Word.Length; i++)
            {
                if (Word[i] == 'A')
                {
                    CurrentCountByLetter[i] = ACount;
                    ACount++;
                }
                else if (Word[i] == 'C')
                {
                    CurrentCountByLetter[i] = CCount;
                    CCount++;
                }
                else if (Word[i] == 'G')
                {
                    CurrentCountByLetter[i] = GCount;
                    GCount++;
                }
                else if (Word[i] == 'T')
                {
                    CurrentCountByLetter[i] = TCount;
                    TCount++;
                }

            }
            //add the preceeding letters to each counting sort
            //this determines their first location in the Array.

            var firstA = 1;
            var firstC = ACount + 1;
            var firstG = ACount + CCount + 1;
            var firstT = ACount + CCount + GCount + 1;

            var BWTStringMatrix = new string[Word.Length];

            var CharArrayToRet = new char[Word.Length];

            CharArrayToRet[0] = Word[0];

            var nextLetter = Word[0];
            var nextLetterCount = 0;
            var letterCount = 1;

            while (letterCount < Word.Length)
            {

                if (nextLetter == 'A')
                {
                    nextLetter = Word[firstA + nextLetterCount];
                    nextLetterCount = CurrentCountByLetter[firstA + nextLetterCount];
                }
                else if (nextLetter == 'C')
                {
                    nextLetter = Word[firstC + nextLetterCount];
                    nextLetterCount = CurrentCountByLetter[firstC + nextLetterCount];
                }
                else if (nextLetter == 'G')
                {
                    nextLetter = Word[firstG + nextLetterCount];
                    nextLetterCount = CurrentCountByLetter[firstG + nextLetterCount];
                }
                else if (nextLetter == 'T')
                {
                    nextLetter = Word[firstT + nextLetterCount];
                    nextLetterCount = CurrentCountByLetter[firstT + nextLetterCount];
                }
                CharArrayToRet[letterCount] = nextLetter;
                letterCount++;
            }

            var StringToReturn = new string(CharArrayToRet.Reverse().ToArray().Skip(1).ToArray());
            return StringToReturn;
        }




        //static void Main(string[] args)
        //{
        //    var InputString = Console.ReadLine().ToCharArray();

        //    Console.WriteLine(BWTInverse(InputString) + "$");
        //}

    }
}




