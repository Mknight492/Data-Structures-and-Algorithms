using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Addition
{


    class Program
    {
        public static bool solveDietProblem(decimal[][] GMat, int NoPivotRows)
        {
            var arrL = GMat[0].Length;
            
            // Write your code here
            for (var i =0; i< GMat.Length; i++)
            {
                for (var j = 0; j < arrL - 1; j++)
                {
                    if(GMat[j][i] != 0)
                    {
                        return solveDietProblemRec(GMat, 0, j,i);
                    }
                }
            }


            return false;
        }

        public static bool solveDietProblemRec(decimal[][] GMat, int NoPivotRows, int curRow,int curCol)
        {
            var ValueToSolve = GMat[curRow][curCol];
            var rowLeng = GMat[0].Length;

            //swap this row to highest position available i.e. just below number of pivot Rows
            var temp = GMat[NoPivotRows];
            GMat[NoPivotRows] = GMat[curRow];
            GMat[curRow] = temp;

            //make current row propotional to ValueToSolve 
            for(var i =0; i < rowLeng; i++)
            {
                GMat[NoPivotRows][i] /= ValueToSolve;
            }


            //also need shift rows which don't have a value for ValueToSolve to the bottom
            var numberOfRowsWithoutVTS = 0;
            for (var i =NoPivotRows+1; i <GMat.Length - numberOfRowsWithoutVTS; i++)
            {
                while(GMat[i][curCol] == 0 && i <GMat.Length - numberOfRowsWithoutVTS)
                {
                    temp = GMat[GMat.Length - numberOfRowsWithoutVTS -1];
                    GMat[GMat.Length - numberOfRowsWithoutVTS-1] = GMat[i];
                    GMat[i] = temp;
                    numberOfRowsWithoutVTS++;
                }
            }


            //for each row which contains a value of ValueToSolve
            //make this ==0 and adjust all the other values acordingly

            for(var i= 0; i < GMat.Length- numberOfRowsWithoutVTS; i++)
            {
                if(i != NoPivotRows)
                {
                    var FactorToChangeBy = GMat[i][curCol];
                    for(var j =0; j< rowLeng; j++)
                    {
                        if(FactorToChangeBy != 0)
                        GMat[i][j] -= (FactorToChangeBy * GMat[NoPivotRows][j]);
                    }
                }
            }

            //reverse i&&J in both formulas
            for(var i = curCol +1; i < rowLeng-1; i++)
            {
                for(var j = NoPivotRows + 1; j < GMat.Length; j++)
                {
                    if (GMat[j][i] != 0) {
                        return solveDietProblemRec(GMat, NoPivotRows + 1, j, i);
                        
                    }    
                }
            }

            return false;

        }

        static void Main(string[] args)
        {
            var Input = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));
            var n = Input[0]; // number of dishes
            var m = Input[0]; // number of ingredients

            var GauseeanMat = new decimal[n][];
            for(var i =0; i < n; i++)
            {
                GauseeanMat[i] = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToDecimal(x));
            }

            var uncess = solveDietProblem(GauseeanMat, 0);

            for(var i =0; i<GauseeanMat.Length; i++)
            {
                var Value = GauseeanMat[i][GauseeanMat[0].Length - 1];
                Console.Write(String.Format("{0:0.000000}", Value) + " ");
            }
            
        }
    }
}


