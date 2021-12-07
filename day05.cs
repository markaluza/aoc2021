using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
namespace aoc2021
{
    class Day5
    {

        private class Map
        {
            public int[,] positions = new int[1000,1000];

            public long findOverlaping()
            {
                long overlaps = 0;
                for (int x =0; x < 1000; x++)
                {
                    for (int y = 0; y < 1000; y++)
                    {
                        if (positions[x,y] >= 2) overlaps++;
                    }
                }
                return overlaps;
            }

            public void Print(int sz)
            {
                for (int y = 0; y < sz; y++)
                {
                    for (int x = 0; x < sz; x++)
                    {
                        Console.Write(positions[x,y]);        
                    }
                    Console.WriteLine("");
                }
            }
                        
        }

        private class Line
        {
            
            public Line(int x1, int y1, int x2, int y2) 
            { 
                this.x1 = x1; this.x2 = x2;
                this.y1 = y1; this.y2 = y2;
            }

            public int x1, y1;
            public int x2, y2;

            public void DrawIntoMap(Map map, bool straightonly)
            {
                if (x1 == x2)
                {
                    int from = Math.Min(y1, y2);
                    int to = Math.Max(y1, y2);

                    for (int i = from; i <=to; i++)
                    {
                        map.positions[x1, i] += 1;
                    }

                }
                else if (y1 == y2)
                {
                    int from = Math.Min(x1, x2);
                    int to = Math.Max(x1, x2);

                    for (int i = from; i <=to; i++)
                    {
                        map.positions[i, y1] += 1;
                    }
                }
                else
                {
                    if (!straightonly)
                    {

                        int dx = 1;
                        if (x1 > x2)
                            dx = -1;
                        int dy = 1;
                        if (y1 > y2)
                            dy = -1;

                        while(true)
                        {
                            map.positions[x1, y1] += 1;
                            if (x1 == x2) break;
                            x1 += dx;
                            y1 += dy;
                        }

                    }

                }
            }

        }

        static List<Line> ReadInput()
        {
            var ret = new List<Line>();

            var lines = aocIO.GetStringList("day05.txt");
            foreach(var line in lines)
            {
                Match res = Regex.Match(
                    line, 
                    @"(\d+),(\d+).->.(\d+),(\d+)"
                    );

                if (res.Groups.Count != 5) 
                {
                    throw new System.Exception();
                }
                
                ret.Add(new Line(
                    int.Parse(res.Groups[1].ToString()),
                    int.Parse(res.Groups[2].ToString()),
                    int.Parse(res.Groups[3].ToString()),
                    int.Parse(res.Groups[4].ToString())                
                    ));
            }
            return ret;


        }

       public static long Task1()
       {
           var input = ReadInput();
           var map = new Map();

           foreach(var line in input)
           {
               line.DrawIntoMap(map, true);
           }
           return map.findOverlaping();
       }

       public static long Task2()
       {
            var input = ReadInput();
            var map = new Map();

            foreach(var line in input)
            {
                line.DrawIntoMap(map, false);
            }

            map.Print(10);

            return map.findOverlaping();
       }       
    }
}