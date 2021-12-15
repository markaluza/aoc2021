using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
namespace aoc2021
{
    class Day6
    {


        private static long TaskDays(int days)
        {
            var input = aocIO.GetIntList_comas("day06.txt");
            var list = new List<long>() {0,0,0,0,0,0,0,0,0};

            foreach(var it in input)
            {
                list[it]++;
            }
            
            for (int iters = 0; iters < days; iters++)
            {
                long zeroes = list[0];
                list.RemoveAt(0);
                list.Add(zeroes);
                list[6]+=zeroes;
            }

            long sum = 0;
            foreach(var it in list)
                sum += it;
           return sum;
        }

       public static long Task1()
       {
            return TaskDays(80);
       }

       public static long Task2()
       {
            return TaskDays(256);
       }       
    }
}