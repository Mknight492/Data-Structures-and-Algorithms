using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addition
{
    class minSubString
    {





        public static int MinimumEdit(string wordi, string wordj)
        {
            //var maximumLength = Math.Max(word1.Length, word2.Length) + 1;

            var wordLookupTable = new int[wordi.Length + 1, wordj.Length + 1];

            //turn the words into a char array with a _ first so they 
            //correspond to the same charachter in the wordLookuptable with padding
            var wordiCharArray = ('_' + wordi).ToCharArray();
            var wordjCharArray = ('_' + wordj).ToCharArray();


            for (var i = 0; i < wordiCharArray.Length; i++)
            {
                for (var j = 0; j < wordjCharArray.Length; j++)
                {
                    //initialise the top and leftmost rows with zeros
                    if (i == 0)
                    {
                        wordLookupTable[i, j] = j;
                    }
                    else if (j == 0)
                    {
                        wordLookupTable[i, j] = i;
                    }
                    else if (wordiCharArray[i] == wordjCharArray[j])
                    {
                        wordLookupTable[i, j] = new int[] { (wordLookupTable[i - 1, j] + 1), (wordLookupTable[i, j - 1] + 1), wordLookupTable[i - 1, j - 1] }.Min();
                    }
                    else
                    {
                        wordLookupTable[i, j] = new int[] { wordLookupTable[i - 1, j] + 1, wordLookupTable[i, j - 1] + 1, wordLookupTable[i - 1, j - 1] + 1 }.Min();
                    }

                    //Console.Write(wordLookupTable[i, j]);
                }
                //Console.WriteLine();
            }



            return wordLookupTable[wordi.Length, wordj.Length];
        }




        //static void Main(string[] args)
        //{

        //    var word1 = Console.ReadLine();
        //    var word2 = Console.ReadLine();

        //    var res = MinimumEdit(word1, word2);
        //    Console.WriteLine(res);


        //}
    }
}
