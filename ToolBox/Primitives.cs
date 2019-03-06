using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addition
{
    class Primatives
    {
        public static long MoneyChange(long coins)
        {
            var MemoArray = new long[coins + 1];
            MemoArray[0] = 0;
            for (var i = 1; i <= coins; i++)
            {
                var Using1Coin = MemoArray[i - 1];
                if (i >= 3)
                {
                    Using1Coin = Math.Min(Using1Coin, MemoArray[i - 3]);
                }
                if (i >= 4)
                {
                    Using1Coin = Math.Min(Using1Coin, MemoArray[i - 4]);
                }

                MemoArray[i] = Using1Coin + 1;
            }
            return MemoArray[coins];
        }

        public static long[] Calculations(long numberToGet)
        {


            var MemoArray = new long[numberToGet + 1];
            MemoArray[0] = 1;
            var MemoPointers = new long[numberToGet + 1];

            for (var i = 2; i <= numberToGet; i++)
            {
                MemoArray[i] = MemoArray[i - 1] + 1;
                MemoPointers[i] = i - 1;
                if (i % 2 == 0 && MemoArray[i / 2] < MemoArray[i])
                {

                    MemoArray[i] = MemoArray[i / 2] + 1;
                    MemoPointers[i] = i / 2;

                }
                if (i % 3 == 0 && MemoArray[i / 3] < MemoArray[i])
                {
                    MemoArray[i] = MemoArray[i / 3] + 1;
                    MemoPointers[i] = i / 3;

                }
            }
            var OptimalSeq = new List<long>();

            while (numberToGet > 0)
            {
                OptimalSeq.Add(numberToGet);
                numberToGet = MemoPointers[numberToGet];
            }

            return OptimalSeq.AsEnumerable().Reverse().ToArray();
        }



        //static void Main(string[] args)
        //{
        //    var coins = Convert.ToInt64(Console.ReadLine());

        //    var res = Calculations(coins);
        //    Console.WriteLine(res.Length - 1);
        //    Console.WriteLine(string.Join(" ", res));


        //}
    }
}
