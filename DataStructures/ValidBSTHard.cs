using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Addition
{


    class ValidBSTHard
    {
        public class Node
        {
            public int Key { get; set; }
            public Node LChild { get; set; }
            public Node RChild { get; set; }
        }

        public class MaxMinPair
        {
            public long Min { get; set; }
            public long Max { get; set; }
            public bool Valid { get; set; }
        }


        public static MaxMinPair MaxValidSubtree(long[][] Tree, long index)
        {
            if (Tree.Length == 0)
            {
                return new MaxMinPair
                {
                    Valid = true
                };
            }
            long currentNodeValue = Tree[index][0];


            //if this tree is a root node return the value of the node
            if (Tree[index][1] == -1 && Tree[index][2] == -1)
            {
                return new MaxMinPair
                {
                    Min = currentNodeValue,
                    Max = currentNodeValue,
                    Valid = true
                };
            }

            MaxMinPair MaxMinLeftChild = null;
            MaxMinPair MaxMinRightChild = null;

            //otherwise find the max/min pairs of its children.
            if (Tree[index][1] != -1)
            {
                MaxMinLeftChild = MaxValidSubtree(Tree, Tree[index][1]);
            }
            if (Tree[index][2] != -1)
            {
                MaxMinRightChild = MaxValidSubtree(Tree, Tree[index][2]);
            }

            //if either of the subtrees are invalid this node is invalid
            if (MaxMinLeftChild != null && !MaxMinLeftChild.Valid
              || MaxMinRightChild != null && !MaxMinRightChild.Valid)
            {
                return new MaxMinPair
                {
                    Valid = false
                };
            }

            //else return the MaxMinpair of the subtree
            if (MaxMinRightChild != null && MaxMinLeftChild != null)
            {
                if (MaxMinLeftChild.Max < currentNodeValue
                  && MaxMinRightChild.Min >= currentNodeValue)
                {

                    return new MaxMinPair
                    {
                        Min = MaxMinLeftChild.Min,
                        Max = MaxMinRightChild.Max,
                        Valid = true
                    };
                }

            }
            else if (MaxMinRightChild != null
               && MaxMinRightChild.Min >= currentNodeValue)
            {
                return new MaxMinPair
                {
                    Max = MaxMinRightChild.Max,
                    Min = Math.Min(MaxMinRightChild.Min, currentNodeValue),
                    Valid = true
                };
            }
            else if (MaxMinLeftChild != null
                && MaxMinLeftChild.Max < currentNodeValue)
            {
                return new MaxMinPair
                {
                    Max = Math.Max(MaxMinLeftChild.Max, currentNodeValue),
                    Min = MaxMinLeftChild.Min,
                    Valid = true
                };

            }




            //else something has gone wrong
            return new MaxMinPair
            {
                Valid = false
            };



        }






        //static void Main(string[] args)
        //{
        //    var numberOfQueries = Convert.ToInt32(Console.ReadLine());

        //    var node2Array = new long[numberOfQueries][];

        //    for (var i = 0; i < numberOfQueries; i++)
        //    {
        //        var input = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c));
        //        node2Array[i] = new long[3];
        //        node2Array[i][0] = input[0];
        //        node2Array[i][1] = input[1];
        //        node2Array[i][2] = input[2];
        //    }

        //    var valid = MaxValidSubtree(node2Array, 0).Valid;

        //    if (valid)
        //    {
        //        Console.WriteLine("CORRECT");
        //    }
        //    else
        //    {
        //        Console.WriteLine("INCORRECT");
        //    }

        //}
    }
}




