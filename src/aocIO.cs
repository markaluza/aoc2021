using System.Collections.Generic;
using System.Linq;
using System;

namespace aoc2021
{
    class aocIO
    {
        public static List<int> GetIntList(string day)
        {
            return System.IO.File.ReadAllLines(@"./inputs/" + day).Select(int.Parse).ToList();
        }

        public static List<int> GetIntList_comas(string day)
        {
            return System.IO.File.ReadAllLines(@"./inputs/" + day)[0].Split(",").Select(int.Parse).ToList();
        }

        public static List<string> GetStringList(string day)
        {
            return new List<string>(System.IO.File.ReadAllLines(@"./inputs/" + day));
        }  

        public static void PrintStringList(List<string> list)
        {
            Console.WriteLine(String.Join(", ", list));
        }

        public static int [,] GetByteMap(string file)
        {
            var lines = aocIO.GetStringList(file);
            var map = new int [lines[0].Length, lines.Count];

            for (int y =0; y < lines.Count; y++)
            {
                for (int x= 0; x < lines[y].Length; x++)
                    map[x,y] = lines[y][x] - '0';
            }
            return map;
        }
        

    }
}