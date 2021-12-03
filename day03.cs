using System;

namespace aoc2021
{
    class Day3
    {
        
        private static long arr2long(int [] arr)
        {
            long pown = 1;
            long res = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                res += arr[arr.Length-1-i] * pown;
                pown*=2;
            }
            return res;
        }

        public static long Task1()
        {
            var lines = aocIO.GetStringList("day03.txt");

            int [] ones = new int[lines[0].Length];
            int [] zeroes = new int[lines[0].Length];

            foreach(var line in lines)
            {
                for (int i =0; i < line.Length; i++)
                {
                    if (line[i] == '1') ones[i]++;
                }
            }

            for (int i =0; i < ones.Length; i++)
            {
                if (ones[i] > lines.Count /2) 
                {
                    ones[i] = 1;
                    zeroes[i] = 0;
                }
                else
                {
                    ones[i] = 0;
                    zeroes[i] = 1;
                }
            }

            long gama = arr2long(ones);
            long epsilon = arr2long(zeroes);
            
            return gama * epsilon;

        }
    }
}