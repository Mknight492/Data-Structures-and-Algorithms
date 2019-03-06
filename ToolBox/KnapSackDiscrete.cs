using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addition
{
    class KnapSackDiscrete
    {

        public static long GoldBars(long maxWeight, long[] GoldBarArray)
        {
            //initialise lookuptable with 
            var LookupTable = new long[GoldBarArray.Length + 1, maxWeight + 1];


            for (long goldBar = 1; goldBar <= GoldBarArray.Length; goldBar++)
            {
                //weight of the current gold bar
                long currentWeight = GoldBarArray[goldBar - 1];
                for (long weight = 1; weight <= maxWeight; weight++)
                {

                    if (currentWeight <= weight)
                    //the currentWeight might be able to be used as it wount automatically make the "knapsack too heavy"
                    {
                        //return whichever is higher the prev. calculated maxium 
                        //or the current weight added to the prev maxium that can tolerate the weight i.e. LookupTable[goldBar-1, weight-currentWeight]
                        LookupTable[goldBar, weight] = new long[] {
                            (LookupTable[goldBar-1, weight-currentWeight] + currentWeight),
                            LookupTable[goldBar-1, weight] }
                        .Max();
                    }
                    else
                    {
                        //the current weight can be used yet so return the previous calculated maximum at this weight
                        LookupTable[goldBar, weight] = LookupTable[goldBar - 1, weight];
                    }

                }
            }
            return LookupTable[GoldBarArray.Length, maxWeight];
        }


       // static void Main(string[] args)
        //{

        //    var MaxWeight = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c)).ToArray()[0];
        //    var GoldBarArray = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c));

        //    var res = GoldBars(MaxWeight, GoldBarArray);
        //    Console.WriteLine(res);

        //}
    }
}
