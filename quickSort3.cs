using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addition
{
    class quickSort3
    {

        public class Indexes
        {
            public int Lindex { get; set; }
            public int Rindex { get; set; }
        }

        public static Indexes Partiton(long[] inputArray, int LIndex, int Rindex)
        {

            var Random = new Random();
            var Middle = Random.Next(LIndex, Rindex);


            long temp = 0;
            long middleElement = inputArray[Middle];
            //move the middle to the start of the Array
            inputArray[Middle] = inputArray[LIndex];
            inputArray[LIndex] = middleElement;


            //current index of the end of each section
            var currentL = LIndex;
            var currentM = LIndex + 1;
            var currentR = LIndex + 1;
            //sort by the middle element, starting from the element to the right of element(s) to go in the middle
            for (var i = LIndex + 1; i <= Rindex; i++)
            {
                if (inputArray[i] > middleElement)
                {
                    currentR++;
                }
                else if (inputArray[i] == middleElement && i <= Rindex)
                {
                    inputArray[i] = inputArray[currentM];
                    inputArray[currentM] = middleElement;
                    currentM++;
                    currentR++;


                }
                else
                {
                    //temp = inputArray[currentL]; //first M element;

                    inputArray[currentL] = inputArray[i];

                    if (currentR > currentM)
                    {
                        inputArray[i] = inputArray[currentM];

                    }
                    else
                    {
                        inputArray[i] = middleElement;
                    }

                    inputArray[currentM] = middleElement;
                    currentR++;
                    currentM++;
                    currentL++;

                }
            }
            return new Indexes
            {
                Lindex = currentL,
                Rindex = currentM
            };
        }


        public static long[] QuickSort3(long[] inputArray, int LIndex, int Rindex)
        {
            var Indexes = Partiton(inputArray, LIndex, Rindex);




            if ((Indexes.Lindex) > LIndex)
            {
                QuickSort3(inputArray, LIndex, Indexes.Lindex);
            }
            if (Indexes.Rindex < Rindex)
                QuickSort3(inputArray, Indexes.Rindex, Rindex);

            return inputArray;

        }

        public static long[] QS3(long[] inputArray)
        {
            if (inputArray.Length == 0)
            {
                return inputArray;
            }
            var LIndex = 0;
            var RIndex = inputArray.Length - 1;
            return QuickSort3(inputArray, LIndex, RIndex);



        }

        public static Boolean ArraysEqual(long[] mySortedArray, long[] correctlySortedArray)
        {
            if (mySortedArray.Length != correctlySortedArray.Length)
            {
                return false;
            }

            for (var i = 0; i < mySortedArray.Length; i++)
            {
                if (mySortedArray[i] != correctlySortedArray[i])
                {
                    var myStoredi = mySortedArray[i];
                    var correctSortedi = correctlySortedArray[i];
                    return false;
                }

            }
            return true;
        }

        //static void Main(string[] args)
        //{
        //    var valuesToFind = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c)).Skip(1).ToArray();
        //    var inputArray = Array.ConvertAll(Console.ReadLine().Split(' '), c => Convert.ToInt64(c)).ToArray();

        //    var count = 0;
        //    //while (true)
        //    //{
        //    //    var random = new Random();
        //    //    var ArrLength = random.Next(0, 1000000);
        //    //    var newArray = new long[ArrLength];
        //    //    var newArray2 = new long[ArrLength];
        //    //    var baseLine = new long[ArrLength];
        //    //    for (var i=0; i< ArrLength;i++)
        //    //    {
        //    //        newArray[i] = random.Next(0, 100000000);
        //    //        newArray2[i] = newArray[i];
        //    //        baseLine[i] = newArray[i];
        //    //    }
        //    //    QS3(newArray);
        //    //    newArray2 = newArray2.OrderBy(x => x).ToArray();
        //    //    ArraysEqual(newArray, newArray2);
        //    //    count++;

        //    //}

        //    var res = QS3(inputArray);
        //    Console.WriteLine(string.Join(" ", res));


        //}
    }
}
