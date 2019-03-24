using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Addition
{


    class ConstructingSuffixTree
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

            public void GenerateSuffixTree(char[] Word)
            {

                for (var i = 0; i < Word.Length; i++)
                {
                    for (var j = 0; j <= i; j++)
                    {

                    }
                }
            }


        }





        //static void Main(string[] args)
        //{
        //    var InputString = Console.ReadLine().ToCharArray();


        //    var trieInstance = new Trie<Nucleotide>();

        //}

    }
}




