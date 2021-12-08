using System.Collections.Generic;
using System.Linq;

namespace aoc2021
{
    class Day8
    {

        private static List<(List<string>, List<string>)> GetInput()
        {

            var input = aocIO.GetStringList("day08.txt");

            var ret = new List<(List<string>, List<string>)>();
            
            foreach(var line in input)
            {
                var l = line.Split(" | ");
                ret.Add((l[0].Split(" ").ToList(), l[1].Split(" ").ToList()));
            }

            return ret;


        }

       public static long Task1()
       {
            var input = GetInput();

            long cntr = 0;
            foreach(var line in input)
            {
                foreach(var output in line.Item2)
                {
                    if (output.Length == 2 || 
                        output.Length == 3 ||
                        output.Length == 4 ||
                        output.Length == 7
                    ) cntr++;
                }
            }

            return cntr;

       }

        private static List<HashSet<char>> FindOutStringWithLen((List<string>, List<string>) input, int rqlen)
        {
            List<HashSet<char>> ret = new List<HashSet<char>>();

            foreach(var output in input.Item1)
            {
                if (output.Length == rqlen)
                {
                    ret.Add(output.ToHashSet());
                }
            }            

            return ret;
        }

       public static long Task2()
       {
           
            var input = GetInput();

            var dict = new Dictionary<int, HashSet<char>> ();

            long totsum = 0;

            foreach( var line in input)
            {
                dict.Clear();

                dict.Add(1,FindOutStringWithLen(line, 2)[0]);
                dict.Add(4,FindOutStringWithLen(line, 4)[0]);
                dict.Add(7, FindOutStringWithLen(line, 3)[0]);
                dict.Add(8, FindOutStringWithLen(line, 7)[0]);

                var zerosixnine = FindOutStringWithLen(line, 6);
                var twothreefive = FindOutStringWithLen(line, 5);

                // sestak je jedny z 069 ktery nema jeden znak z 1
                char topright = ' ';
                char bottomright = ' ';
                var dif41 = new HashSet<char>(dict[4]); 
                dif41.ExceptWith(dict[1]);
                for(int i =0; i < zerosixnine.Count; i++)
                {
                    var set = zerosixnine[i];
                    if (!dict[1].IsSubsetOf(set))
                    {
                        dict.Add(6, set);
                        topright = dict[1].Except(set).First();
                        bottomright = dict[1].Except( new List<char> { topright } ).First();
                    }
                    else
                    {
                        // 9 obsahuje casti 4 ky
                        if (dif41.IsSubsetOf(set))
                        {
                            dict.Add(9, set);
                        }
                        else
                        {
                        // 0 je nema
                             dict.Add(0, set);
                        }
                    }
                }
                
                // 3 obsahuje celou jednicku
                foreach (var set in twothreefive)
                {
                    if (dict[1].IsSubsetOf(set))
                    {
                        dict.Add(3, set);
                    }
                    else
                    {
                        // 2 ma hroni pravy
                        if (set.Contains(topright))
                        {
                            dict.Add(2, set);
                        }
                        else 
                        // 5 dolni pravy
                        {
                            dict.Add(5, set);
                        }
                    }
                }

                long result = 0;
                foreach(var val in line.Item2)
                {
                    foreach (var nmb in dict)
                    {
                        if (nmb.Value.SetEquals(val))
                        {
                            result *=10;
                            result += nmb.Key;
                            break;
                        }
                    }
                }

                totsum += result;
                
            }

            return totsum;

       }       
    }
}