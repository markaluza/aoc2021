using System.Collections.Generic;
using System.Linq;

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

    }
}