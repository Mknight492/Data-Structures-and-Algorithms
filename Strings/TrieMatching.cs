using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Addition
{


    class Program
    {

        public class Nucleotide
        {
            public Nucleotide A { get; set; }
            public Nucleotide T { get; set; }
            public Nucleotide C { get; set; }
            public Nucleotide G { get; set; }
            public bool Word { get; set; }
            public int ID { get; set; }

            public Nucleotide(int id)
            {
                ID = id;
            }
        }

        public class Trie<T>
        {
            private Nucleotide baseOfTrie = new Nucleotide(0);
            private int count = 0;

            public void Add(char[] WordToAdd)
            {
                var currentElement = baseOfTrie;

                for (var i = 0; i < WordToAdd.Length; i++)
                {
                    switch (WordToAdd[i])
                    {
                        case 'A':
                            if (currentElement.A == null)
                            {
                                count++;
                                currentElement.A = new Nucleotide(count);
                                currentElement = currentElement.A;
                            }
                            else
                            {
                                currentElement = currentElement.A;
                            }
                            break;
                        case 'T':
                            if (currentElement.T == null)
                            {
                                count++;
                                currentElement.T = new Nucleotide(count);
                                currentElement = currentElement.T;
                            }
                            else
                            {
                                currentElement = currentElement.T;
                            }
                            break;
                        case 'C':
                            if (currentElement.C == null)
                            {
                                count++;
                                currentElement.C = new Nucleotide(count);
                                currentElement = currentElement.C;
                            }
                            else
                            {
                                currentElement = currentElement.C;
                            }
                            break;
                        case 'G':
                            if (currentElement.G == null)
                            {
                                count++;
                                currentElement.G = new Nucleotide(count);
                                currentElement = currentElement.G;
                            }
                            else
                            {
                                currentElement = currentElement.G;
                            }
                            break;
                    }
                    if (i == WordToAdd.Length - 1)
                    {
                        currentElement.Word = true;
                    }

                }
            }

            public List<int> CheckWord(char[] Word)
            {

                var Matches = new List<int>();

                for (var i = 0; i < Word.Length; i++)
                {
                    var currentPositionInTrie = baseOfTrie;
                    var currentPositionInWord = i;
                    var AbleToTraverse = true;

                    while (AbleToTraverse)
                    {
                        switch (Word[currentPositionInWord])
                        {
                            case 'A':
                                if (currentPositionInTrie.A == null)
                                    AbleToTraverse = false;
                                else
                                    currentPositionInTrie = currentPositionInTrie.A;
                                break;
                            case 'T':
                                if (currentPositionInTrie.T == null)
                                    AbleToTraverse = false;
                                else
                                    currentPositionInTrie = currentPositionInTrie.T;
                                break;
                            case 'C':
                                if (currentPositionInTrie.C == null)
                                    AbleToTraverse = false;
                                else
                                    currentPositionInTrie = currentPositionInTrie.C;
                                break;
                            case 'G':
                                if (currentPositionInTrie.G == null)
                                    AbleToTraverse = false;
                                else
                                    currentPositionInTrie = currentPositionInTrie.G;
                                break;
                        }


                        if (AbleToTraverse && currentPositionInTrie.Word)
                        {
                            Matches.Add(i);
                        }

                        if (currentPositionInWord + 1 < Word.Length)
                            currentPositionInWord++;
                        else
                            AbleToTraverse = false;
                    }
                }

                return Matches;
            }
        }





        static void Main(string[] args)
        {
            var StringToTest = Console.ReadLine().ToCharArray();

            var numberOfStrings = Convert.ToInt32(Console.ReadLine());

            var trieInstance = new Trie<Nucleotide>();

            for (var i = 0; i < numberOfStrings; i++)
            {
                var Pattern = Console.ReadLine().ToCharArray();
                trieInstance.Add(Pattern);
            }

            var matches = trieInstance.CheckWord(StringToTest);
            //var matchesAsString = matches.ConvertAll(x => x.ToString()).ToArray();
            Console.WriteLine(string.Join(" ", matches));


        }

    }
}




