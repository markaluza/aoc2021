using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace aoc2021
{
    class Day11
    {

        private static void Increase(ref int [,] map, int x, int y, ref HashSet<(int, int)> flashed)
        {

            if (x < 0 || x >= map.GetLength(0) ||
                y < 0 || y >= map.GetLength(1)) return;

            map[x,y]++;

            if (map[x,y] >9)
            {
                if (flashed.Contains((x,y))) return;
                flashed.Add((x,y));

                foreach (var tuple in new List<(int, int)> {(-1, -1), (0, -1), (+1, -1), (+1, 0), (+1, +1), (0, +1), (-1, +1), (-1, 0)})
                    Increase(ref map, x+tuple.Item1, y+tuple.Item2, ref flashed);

            };
        }
        private static int Step(ref int [,] map)
        {
            var flashes = new HashSet<(int, int)>();
            for (int y =0; y < map.GetLength(0); y++)
            {
                for (int x =0; x < map.GetLength(1); x++)
                {
                   Increase(ref map, x,y, ref flashes);
                }
            }

            for (int y =0; y < map.GetLength(0); y++)
            {
                for (int x =0; x < map.GetLength(1); x++)
                {
                   if (map[x,y] > 9) map[x,y] = 0;
                }
            }

            return flashes.Count;
        }

       public static long Task1()
       {
            var map = aocIO.GetByteMap("day11.txt");

            long flashes = 0;
            for (int i =0; i < 100; i++)
            {
                flashes+= Step(ref map);
            }

            return flashes;
       }   

       public static long Task2()
       {                
            var map = aocIO.GetByteMap("day11.txt");

            for (long i =1; ; i++)
            {
                if (Step(ref map) == 100) return i;
            }
            
       }       
    }
}