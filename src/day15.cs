using System;
using System.Collections.Generic;

namespace aoc2021
{
    class Day15
    {

        class MapItem
        {
            public long alt = 0;
            public long risklevel = long.MaxValue;
            public int x = 0;
            public int y = 0;
            public MapItem(int x, int y, long alt) { this.x = x; this.y = y; this.alt = alt; }

        }


        private static long GetLowestRiskLevel(int[,] map)
        {

            MapItem [,] imap = new MapItem[map.GetLength(0), map.GetLength(1)];
            for (int x = 0; x < map.GetLength(0); x++) for (int y = 0; y < map.GetLength(1); y++) imap[x,y] = new MapItem(x, y, map[x,y]);
            imap[0,0].risklevel = 0;

            List<MapItem> itemstosearch = new List<MapItem>() { imap[0,0] };
            MapItem lastitem = imap[imap.GetLength(0)-1, imap.GetLength(1)-1];

            while(true)
            {
                MapItem lowest = itemstosearch[0];
                

                foreach(var srch in new List<(int, int)>() { (-1, 0), (0, -1), (+1, 0), (0, +1) })
                {
                    int x = lowest.x + srch.Item1;
                    int y = lowest.y + srch.Item2; // vyradim mimo mapu a uz ohodnocene
                    if (x < 0 || x >= imap.GetLength(0) || y < 0 || y >= imap.GetLength(1) || imap[x,y].risklevel != long.MaxValue) continue;

                    var it = imap[x,y];
                    it.risklevel = lowest.risklevel + it.alt;
                    
                    Console.WriteLine("x {0},  y{1}", x, y);
                    for (int fy = 0; fy < map.GetLength(1); fy++)
                    {
                        for (int fx = 0; fx < map.GetLength(0); fx++) 
                        {
                            if (imap[fx,fy].risklevel == long.MaxValue)
                                Console.Write(" - ");
                            else
                                Console.Write("{0, 3}", imap[fx,fy].risklevel);
                        }
                        Console.WriteLine();
                    }

                    if (it == lastitem) 
                    {
                        return it.risklevel;
                    }

                    itemstosearch.Add(it);
                    
                }
            }

        }

       public static long Task1()
       {
           var map = aocIO.GetByteMap("tst.txt");
           return GetLowestRiskLevel(map);
       }

       public static long Task2()
       {                
            return 0;
       }       
    }
}