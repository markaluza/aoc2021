using System.Collections.Generic;

namespace aoc2021
{
    class Day1
    {
        
        public static int SumIncreases(List<int> list)
        {
            int increases = 0;
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i] > list[i-1]) increases++;
            }
            return increases;
        }

        public static int Task1()
        {
            var list = aocIO.GetIntInput("day1.txt");
            return SumIncreases(list);
        }

        public static int Task2()
        {
            var tmplist = aocIO.GetIntInput("day1.txt");
            
            var list = new List<int>();
            for (int i = 0; i < tmplist.Count - 2; i++)
            {
                int sum = 0;
                for (int j = 0; j < 3; j++)
                {
                    sum += tmplist[i+j];
                }
                list.Add(sum);
            }

            return SumIncreases(list);

        }
    }
}