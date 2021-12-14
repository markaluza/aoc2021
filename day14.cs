using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2021
{
    class Day14
    {

        private static string chrs2str(char ch1, char ch2)
        {
            char  [] chrs = { ch1, ch2 };
            return new string( chrs );
        }

        private static (Dictionary<string, long>, Dictionary<string, List<string>>, Dictionary<char, long>) GetInput()
        {
            var input = new Dictionary<string, long>();
            var rules = new Dictionary<string, List<string>>();
            var sum = new Dictionary<char, long>();

            var lines =aocIO.GetStringList("day14.txt");

            for (int i =2; i < lines.Count; i++)
            {
                var match = Regex.Match(lines[i], "(..) -> (.)");

                var r = match.Groups[1].ToString();
                var d = match.Groups[2].ToString();

                rules.Add(r, new List<string>() { chrs2str(r[0], d[0]),chrs2str(d[0], r[1])  });
                input.Add(r, 0);

                if (!sum.ContainsKey(d[0]))
                    sum.Add(d[0], 0);

            }  

            for (int i =0; i < lines[0].Length -1; i++)
            {
                input[chrs2str( lines[0][i], lines[0][i+1] ) ] +=  1 ;
            }

            foreach(var chr in lines[0])
            {
                sum[chr] += 1;
            }

            return (input, rules, sum); 
        }

        private static long GetDiff(long steps)
        {
             var input = GetInput();

            var res = input.Item1;
            var sum = input.Item3;            
            
            for (int i =0; i < steps; i++)
            {
                var step =  new Dictionary<string, long>(); // vyplnim vsema klicema at nemusim testovat zda tam jsou...
                foreach(var r in res) step.Add(r.Key, 0);

                foreach(var it in res)
                {
                    var k1 = input.Item2[it.Key][0];
                    var k2 = input.Item2[it.Key][1];

                    step[k1] +=  it.Value;
                    step[k2] +=  it.Value;

                    sum[k1[1]] += it.Value;

                }

                res = step;
            }

            var minkey = sum.First();
            var maxkey = minkey;

            foreach(var it  in sum)
            {
                if (it.Value > maxkey.Value) maxkey = it;
                if (it.Value < minkey.Value) minkey = it;
            }

            return maxkey.Value - minkey.Value;
        }

       public static long Task1()
       {
           return GetDiff(10);
       }

       public static long Task2()
       {                
            return GetDiff(40);  
       }       
    }
}