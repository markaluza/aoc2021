using System.Collections.Generic;

namespace aoc2021
{
    class Day2
    {

        private static List<(string, int)> GetPath()
        {

            var ret = new List<(string, int)>(); 

            var lines = aocIO.GetStringList("day02.txt");

            foreach(var line in lines)
            {
                var its = line.Split(" ");
                ret.Add( (its[0], int.Parse(its[1])) );
            }

            return ret;
        }

        public static long Task1()
        {
            int depth = 0;
            int horizontal = 0;

            var path = GetPath();

            foreach(var step in path)
            {
                switch(step.Item1)
                {
                    case "forward": horizontal += step.Item2;    break;
                    case "down": depth += step.Item2;    break;
                    case "up": depth -= step.Item2;    break;
                }
            }

            return depth * horizontal;
            
        }

        public static long Task2()
        {
            int depth = 0;
            int horizontal = 0;
            int aim = 0;

            var path = GetPath();

            foreach(var step in path)
            {
                switch(step.Item1)
                {
                    case "forward": horizontal += step.Item2; depth += aim* step.Item2;   break;
                    case "down":  aim += step.Item2;  break;
                    case "up":  aim -= step.Item2;  break;
                }
            }

            return depth * horizontal;
        }
    }
}