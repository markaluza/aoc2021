using System;
using System.Collections.Generic;

namespace aoc2021
{
    class Day15
    {

        private static long GetLowestRiskLevel(int[,] map)
        {
            //  djikstra
            long [,] risk = new long[map.GetLength(0), map.GetLength(1)];
            for (int x = 0; x < risk.GetLength(0); x++) for (int y = 0; y < risk.GetLength(1); y++) risk[x,y] = long.MaxValue;
            risk[0,0] = 0;

            var itemstosearch = new List<(int, int)>() { (0,0) };
            var lastitem = (map.GetLength(0)-1, map.GetLength(1)-1);

            while(true)
            {
                var lowest = itemstosearch[0];
                foreach(var it in itemstosearch) if (risk[it.Item1, it.Item2] < risk[lowest.Item1, lowest.Item2]) lowest = it;
                itemstosearch.Remove(lowest);

                foreach(var srch in new List<(int, int)>() { (-1, 0), (0, -1), (+1, 0), (0, +1) })
                {
                    int x = lowest.Item1 + srch.Item1;
                    int y = lowest.Item2 + srch.Item2; // vyradim mimo mapu a uz ohodnocene
                    if (x < 0 || x >= risk.GetLength(0) || y < 0 || y >= risk.GetLength(1) || risk[x,y] != long.MaxValue) continue;

                    risk[x,y] = risk[lowest.Item1, lowest.Item2] + map[x,y];
                    
                    if ((x,y) == lastitem) 
                    {
                        return risk[x,y];
                    }

                    itemstosearch.Add((x,y));
                }
            }
        }

       public static long Task1()
       {
           var map = aocIO.GetByteMap("day15.txt");
           return GetLowestRiskLevel(map);
       }

       public static long Task2()
       {        
            var map = aocIO.GetByteMap("day15.txt"); 
            var map2 = new int[map.GetLength(0)*5,map.GetLength(1)*5];

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    int offset = row + col;
                    int sx = map.GetLength(0) * col;
                    int sy = map.GetLength(1) * row;
                    for (int y = 0; y < map.GetLength(1); y++)
                    {
                        for (int x = 0; x < map.GetLength(0); x++)
                        {
                            map2[sx + x, sy +y] = map[x,y] + offset;
                            if (map2[sx + x, sy +y] > 9) map2[sx + x, sy +y] -=9;
                        }
                    }
                }
            }

           return GetLowestRiskLevel(map2);
       }       
    }
}