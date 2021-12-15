using System.Collections.Generic;

namespace aoc2021
{
    class Day1
    {
        
        private static int SumIncreases(List<int> list)
        {
            int increases = 0;
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i] > list[i-1]) increases++;
            }
            return increases;
        }

        public static long Task1()
        {
            var list = aocIO.GetIntList("day01.txt");
            return SumIncreases(list);
        }

        public static long Task2()
        {
            var tmplist = aocIO.GetIntList("day01.txt");
            
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