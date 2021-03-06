using System;
using System.Collections.Generic;

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

        private static void split(List<string> lines, int position, out List<string> zeroes, out List<string> ones)
        {
            zeroes = new  List<string>();
            ones = new  List<string>();
            foreach(var line in lines)
            {
                if (line[position] == '1') ones.Add(line);
                else zeroes.Add(line);
            }
        }

        public static long Task2()
        {
            var lines = aocIO.GetStringList("day03.txt");

            var zeroes = new List<string>();
            var ones = new List<string>();

            split(lines, 0, out zeroes, out ones);

            List<string> ox  = null;
            List<string> co  = null;

            if (zeroes.Count < ones.Count)
            {
                ox = ones;
                co = zeroes;
            }
            else
            {
                ox = zeroes;
                co = ones;
            }

            for (int i = 1; i < lines[0].Length && ox.Count > 1; i++)
            {
                split(ox, i, out zeroes, out ones);
                if (zeroes.Count <= ones.Count)
                {
                    ox = ones;
                }
                else
                {
                    ox = zeroes;
                }
            }

            for (int i = 1; i < lines[0].Length && co.Count > 1; i++)
            {
                split(co, i, out zeroes, out ones);
                if (ones.Count < zeroes.Count)
                {
                    co = ones;
                }
                else
                {
                    co = zeroes;
                }
            }


            long nox = Convert.ToUInt32(ox[0], 2);
            long nco = Convert.ToUInt32(co[0], 2);
            

            return nox * nco;
                                           
        
        }
    }
}