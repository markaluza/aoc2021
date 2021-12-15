using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace aoc2021
{
    class Day13
    {

        private static (HashSet<(int,int)>, List<(char, int)>) GetInput()
        {
            var coords = new HashSet<(int,int)>();
            var fold = new List<(char, int)>();

            var lines = aocIO.GetStringList("day13.txt");

            foreach(var line in lines)
            {
                Match res = Regex.Match(line, @"(\d+),(\d+)");

                if (res.Success)
                {
                    coords.Add( (int.Parse(res.Groups[1].ToString()),  int.Parse(res.Groups[2].ToString())));
                    continue;
                }

                res = Regex.Match(line, @"fold along ([x|y])=(\d+)");
                if (res.Success)
                {
                    fold.Add( (res.Groups[1].Value[0],  int.Parse(res.Groups[2].ToString())));
                    continue;
                }         

            }
            
            return (coords, fold);
        }

        public static void Print(HashSet<(int,int)> map, int mx, int my)
        {
            for (int y = 0; y < my; y++)
            {
                for (int x =0; x < mx; x++)
                {
                    if (map.Contains((x,y)))
                        Console.Write('#');
                    else 
                        Console.Write('.');
                }
                Console.WriteLine();
            }
        }

        public static void Fold(ref HashSet<(int,int)> map, (char, int) fold)
        {
            var newcoords = new HashSet<(int,int)>();
            if (fold.Item1 == 'x')
            {
                foreach(var coord in map)
                {
                    var newcoord = coord;
                    if (coord.Item1 > fold.Item2) newcoord.Item1 =  fold.Item2 * 2 - coord.Item1;
                    if (!newcoords.Contains(newcoord)) newcoords.Add(newcoord);
                }

            }
            else
            {
                foreach(var coord in map)
                {
                    var newcoord = coord;
                    if (coord.Item2 > fold.Item2) newcoord.Item2 = fold.Item2 * 2 - newcoord.Item2;
                    if (!newcoords.Contains(newcoord)) newcoords.Add(newcoord);
                }
                
            }

            map = newcoords;
        }

       public static long Task1()
       {
            var input = GetInput();

            Fold(ref input.Item1 ,input.Item2[0] );
            return input.Item1.Count;
       }

       public static long Task2()
       {                
            var input = GetInput();
            foreach(var fold in input.Item2 )
            {
                Fold(ref input.Item1, fold);
            }

           Print(input.Item1, 50, 10);

           return 0;
       }       
    }
}