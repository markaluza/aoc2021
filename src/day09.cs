using System;
using System.Collections.Generic;

namespace aoc2021
{
    class Day9
    {

        private static bool IsLower_Abs(List<string> input, (int, int) pos, (int, int) pos2)
        {
            try
            {
                return input[pos.Item1][pos.Item2] < input[pos2.Item1][pos2.Item2];
            }
            catch (Exception)
            {
                return true;
            }
        } 

        private static bool IsLower_offset(List<string> input, (int, int) pos, (int, int) offset)
        {
            return IsLower_Abs(input, pos, (pos.Item1 + offset.Item1, pos.Item2 + offset.Item2));
        }

        static private List<(int,int)> GetLowPoints(List<string> input)
        {
            var ret = new List<(int,int)>();
            for (int row =0; row < input.Count; row++)
            {   
                for (int col = 0; col < input[0].Length; col++)
                {
                    if (
                        IsLower_offset(input, (row, col), (0, -1)) &&
                        IsLower_offset(input, (row, col), (+1, 0))&&
                        IsLower_offset(input, (row, col), (0, +1)) &&
                        IsLower_offset(input, (row, col), (-1, 0))
                    ) 
                    {
                        ret.Add((row, col));
                    }              
                }
            }
            return ret;
        }

       public static long Task1()
       {
            var input = aocIO.GetStringList("day09.txt");
            var lowpoints = GetLowPoints(input);

            long retval = 0;
            foreach(var lowpoint in lowpoints)
            {
                retval += (long) (char.GetNumericValue(input[lowpoint.Item1][lowpoint.Item2])) + 1;
            }        

            return retval;

       }

       private static bool IsInBasin(List<string> input, HashSet<(int, int)> basin, (int, int) origpos, (int, int) offset, out (int, int) testpoint)
       {
            testpoint = (origpos.Item1 + offset.Item1, origpos.Item2 + offset.Item2);
            if (basin.Contains(testpoint)) return false;

           try
           {
                if (input[testpoint.Item1][testpoint.Item2] == '9') return false;
           }
           catch (Exception)
           {
               return false;
           }

            return IsLower_Abs(input,  origpos, testpoint);

           
       }

       private static long GetBasinSize(List<string> input, (int, int) pos)
       {
            
            var queuetotest = new Queue<(int, int)>(); queuetotest.Enqueue( pos );
            var basin = new HashSet<(int, int)>() { pos };

            while(queuetotest.Count > 0)
            {
                var testpos = queuetotest.Dequeue();
                var newpoint = (0, 0);

                if (IsInBasin(input, basin, testpos, (-1, 0), out newpoint)) { basin.Add(newpoint); queuetotest.Enqueue(newpoint); }
                if (IsInBasin(input, basin, testpos, (1, 0), out newpoint)) { basin.Add(newpoint); queuetotest.Enqueue(newpoint); }
                if (IsInBasin(input, basin, testpos, (0, -1), out newpoint)) { basin.Add(newpoint); queuetotest.Enqueue(newpoint); }
                if (IsInBasin(input, basin, testpos, (0, 1), out newpoint)) { basin.Add(newpoint); queuetotest.Enqueue(newpoint); }

            }

            return basin.Count;
       }

       public static long Task2()
       {
            var input = aocIO.GetStringList("day09.txt");
            var lowpoints = GetLowPoints(input);

            var basinsizes = new List<long>();
            foreach(var pt in lowpoints)
            {
                basinsizes.Add(GetBasinSize(input, pt));
            }

            basinsizes.Sort();
          

            return basinsizes[basinsizes.Count -1] * basinsizes[basinsizes.Count -2] * basinsizes[basinsizes.Count -3];
       }       
    }
}