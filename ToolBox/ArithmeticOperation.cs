using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Addition
{
    class ArithmeticOperation
    {

        public static long Calc(long a, long b, char op)
        {
            switch (op)
            {
                case ('-'):
                    return (a - b);
                case ('+'):
                    return (a + b);
                case ('*'):
                    return (a * b);
            }
            return -1;

        }





        public static long ArithmeticExpressionMax(long[] numbers, char[] operators)
        {

            var LookupMax = new long[numbers.Length, numbers.Length];
            var LookupMin = new long[numbers.Length, numbers.Length];




            for (long i = 0; i < numbers.Length; i++)
            {
                //initialise diagonal as the numbers themselves
                LookupMax[i, i] = numbers[i];
                LookupMin[i, i] = numbers[i];
            }


            for (long i = 0; i < numbers.Length - 1; i++)
            {

                for (long j = 0; j + i + 1 < numbers.Length; j++)
                {

                    var options = new List<long>();
                    var currenti = j;
                    var currentj = i + 1 + j;
                    for (var k = 0; k <= i; k++)
                    {
                        options.Add(Calc(LookupMax[currenti, currentj - 1 - k], LookupMax[currentj - k, currentj], operators[currentj - 1 - k]));
                        options.Add(Calc(LookupMax[currenti, currentj - 1 - k], LookupMin[currentj - k, currentj], operators[currentj - 1 - k]));
                        options.Add(Calc(LookupMin[currenti, currentj - 1 - k], LookupMax[currentj - k, currentj], operators[currentj - 1 - k]));
                        options.Add(Calc(LookupMin[currenti, currentj - 1 - k], LookupMin[currentj - k, currentj], operators[currentj - 1 - k]));
                    }
                    LookupMax[j, i + 1 + j] = options.Max();
                    LookupMin[j, i + 1 + j] = options.Min();
                }
            }


            //debugging logging
            //for (long i = 0; i < numbers.Length; i++)
            //{

            //    for (long j = 0; j < numbers.Length; j++)
            //    {
            //        Console.Write(LookupMax[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}
            //for (long i = 0; i < numbers.Length; i++)
            //{

            //    for (long j = 0; j < numbers.Length; j++)
            //    {
            //        Console.Write(LookupMin[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}

            //return statement
            return LookupMax[0, numbers.Length - 1];
        }



        //static void Main(string[] args)
        //{


        //    var ArithmiticExpression = Console.ReadLine();
        //    var numbers = Regex.Matches(ArithmiticExpression, @"([0-9]*)");
        //    var expressions = Regex.Matches(ArithmiticExpression, @"[+-/*]");

        //    var numbersList = new List<long>();
        //    var expressionList = new List<char>();
        //    foreach (var number in numbers)
        //    {
        //        if (number.ToString() != "")
        //        {
        //            numbersList.Add(Convert.ToInt64(number.ToString()));
        //        }
        //    }
        //    foreach (var expression in expressions)
        //    {
        //        if (expression.ToString() != "")
        //        {
        //            expressionList.Add(Convert.ToChar(expression.ToString()));
        //        }
        //    }


        //    var res = ArithmeticExpressionMax(numbersList.ToArray(), expressionList.ToArray());
        //    Console.WriteLine(res);


        //}
    }
}
