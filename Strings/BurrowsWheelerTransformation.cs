using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Addition
{


    class BurrowsWheelerTransformation
    {

        public static string BWT(char[] Word)
        {
            var BWTMatrix = new char[Word.Length][];

            for (var i = 0; i < Word.Length; i++)
            {
                BWTMatrix[i] = new char[Word.Length];
                for (var j = 0; j < Word.Length; j++)
                {
                    BWTMatrix[i][j] = Word[(i + j) % Word.Length];
                }

            }
            var BWTStringMatrix = new string[Word.Length];

            for (var i = 0; i < Word.Length; i++)
            {
                BWTStringMatrix[i] = new string(BWTMatrix[i]);
            }

            BWTStringMatrix = BWTStringMatrix.OrderBy(x => x).ToArray();

            var CharArrayToRet = new char[Word.Length];
            for (var i = 0; i < Word.Length; i++)
            {
                CharArrayToRet[i] = BWTStringMatrix[i][Word.Length - 1];
                //Console.WriteLine(BWTStringMatrix[i].ToString());
            }
            return new string(CharArrayToRet);
        }




        //static void Main(string[] args)
        //{
        //    var InputString = Console.ReadLine().ToCharArray();

        //    Console.WriteLine(BWT(InputString));
        //}

    }
}




