using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Addition
{


    class NucleotideTrie
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

            public void Log()
            {

                if (baseOfTrie.A != null)
                {
                    Console.WriteLine(baseOfTrie.ID + "->" + baseOfTrie.A.ID + ":A");
                    LogR(baseOfTrie.A);
                }
                if (baseOfTrie.T != null)
                {
                    Console.WriteLine(baseOfTrie.ID + "->" + baseOfTrie.T.ID + ":T");
                    LogR(baseOfTrie.T);
                }
                if (baseOfTrie.G != null)
                {
                    Console.WriteLine(baseOfTrie.ID + "->" + baseOfTrie.G.ID + ":G");
                    LogR(baseOfTrie.G);
                }
                if (baseOfTrie.C != null)
                {
                    Console.WriteLine(baseOfTrie.ID + "->" + baseOfTrie.C.ID + ":C");
                    LogR(baseOfTrie.C);
                }

            }
            private void LogR(Nucleotide nucleotideToLog)
            {
                if (nucleotideToLog.A != null)
                {
                    Console.WriteLine(nucleotideToLog.ID + "->" + nucleotideToLog.A.ID + ":A");
                    LogR(nucleotideToLog.A);
                }
                if (nucleotideToLog.T != null)
                {
                    Console.WriteLine(nucleotideToLog.ID + "->" + nucleotideToLog.T.ID + ":T");
                    LogR(nucleotideToLog.T);
                }
                if (nucleotideToLog.G != null)
                {
                    Console.WriteLine(nucleotideToLog.ID + "->" + nucleotideToLog.G.ID + ":G");
                    LogR(nucleotideToLog.G);
                }
                if (nucleotideToLog.C != null)
                {
                    Console.WriteLine(nucleotideToLog.ID + "->" + nucleotideToLog.C.ID + ":C");
                    LogR(nucleotideToLog.C);
                }
            }
        }





        //static void Main(string[] args)
        //{
        //    var numberOfStrings = Convert.ToInt32(Console.ReadLine());

        //    var trieInstance = new Trie<Nucleotide>();

        //    for (var i = 0; i < numberOfStrings; i++)
        //    {
        //        var NextWord = Console.ReadLine().ToCharArray();
        //        trieInstance.Add(NextWord);
        //    }
        //    trieInstance.Log();

        //}

    }
}




