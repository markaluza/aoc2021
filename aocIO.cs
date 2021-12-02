using System.Collections.Generic;
using System.Linq;

namespace aoc2021
{
    class aocIO
    {
        public static List<int> GetIntInput(string day)
        {
            return System.IO.File.ReadAllLines(@"./inputs/" + day).Select(int.Parse).ToList();
        }

    }
}