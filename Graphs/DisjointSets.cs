using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Addition
{


    class DisJointSets
    {
        public class DisjointSetNode
        {
            public DisjointSetNode Parent { get; set; }
            public uint rank { get; set; }
            public long totalTables { get; set; }

            public DisjointSetNode(long initalNumberOfTables)
            {
                Parent = this;
                totalTables = initalNumberOfTables;
            }

            public DisjointSetNode Find()
            {
                //path compression
                if (Parent != this) Parent = Parent.Find();
                return Parent;
            }

            public bool IsUnionedWith(DisjointSetNode other)
            {
                return Find() == other.Find();
            }

            public void Union(DisjointSetNode other)
            {
                var root1 = Find();
                var root2 = other.Find();

                if (root1 == root2)
                    return;

                if (root1.rank > root2.rank)
                {
                    root2.Parent = root1;
                    root1.totalTables += root2.totalTables;
                }
                else
                {
                    root1.Parent = root2;
                    root2.totalTables += root1.totalTables;

                    if (root1.rank == root2.rank)
                    {
                        root2.rank++;
                    }
                }

            }
        }


        //static void Main(string[] args)
        //{
        //    var input = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c));
        //    long n = input[0]; //number of tables
        //    var m = input[1]; //number of merge queries

        //    var input2 = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c));


        //    var tables = new DisjointSetNode[n];

        //    long currentMax = 0;
        //    for (var i = 0; i < n; i++)
        //    {
        //        tables[i] = new DisjointSetNode(input2[i]);
        //        if (input2[i] > currentMax)
        //        {
        //            currentMax = input2[i];
        //        }
        //    }



        //    var maxValuesArray = new long[m];

        //    for (var i = 0; i < m; i++)
        //    {
        //        var nextInput = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt32(c));
        //        var tableToJoin1 = nextInput[0] - 1; //-1 as 0 indexed
        //        var tableToJoin2 = nextInput[1] - 1;

        //        tables[tableToJoin1].Union(tables[tableToJoin2]);
        //        var maxOfJoinedTables = tables[tableToJoin1].Find().totalTables;

        //        currentMax = (maxOfJoinedTables > currentMax) ? maxOfJoinedTables : currentMax;
        //        maxValuesArray[i] = currentMax;
        //    }

        //    for (var i = 0; i < m; i++)
        //    {
        //        Console.WriteLine(maxValuesArray[i]);
        //    }

        //}

    }
}




