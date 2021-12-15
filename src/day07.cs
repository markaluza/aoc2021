using System;
using System.Linq;

namespace aoc2021
{
    class Day7
    {

       public static long Task1()
       {
            var input = aocIO.GetIntList_comas("day07.txt");

            int min = input.Min();
            int max = input.Max();

            long mindist = long.MaxValue;
            int mindist_iter = 0;

            for(int i = min-1; i <= max+1; i++)
            {
                long dist = 0;
                foreach(var pos in input)
                {

                    dist += Math.Abs( pos - i);
                }

                if (dist < mindist)
                {
                    mindist_iter = i;
                    mindist = dist;
                }

            }

            return mindist;

       }

       public static long Task2()
       {
            var input = aocIO.GetIntList_comas("day07.txt");

            int min = input.Min();
            int max = input.Max();

            long mindist = long.MaxValue;
            int mindist_iter = 0;

            for(int i = min-1; i <= max+1; i++)
            {
                long dist = 0;
                foreach(var pos in input)
                {
                    long n = Math.Abs( pos - i);

                    dist += (n *n +n)/2;
                }

                if (dist < mindist)
                {
                    mindist_iter = i;
                    mindist = dist;
                }

            }

            return mindist;
       }       
    }
}