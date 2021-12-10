using System.Linq;
using System.Collections.Generic;

namespace aoc2021
{
    class Day10
    {

       public static long Task1()
       {
            var input = aocIO.GetStringList("day10.txt");

            long totscore = 0;
            var match = new Dictionary<char, char>() { {')', '('}, {'>', '<'}, {'}', '{'}, {']', '['}  };
            var score = new Dictionary<char, long>() { {')', 3}, {'>', 25137}, {'}', 1197}, {']', 57}  };    
            foreach(var line in input)
            {
                var stack = new Stack<char>(); 

                for(int i =0; i <line.Length; i++)
                {
                    char chr = line[i];

                    if (match.ContainsValue(chr))
                    {
                        stack.Push(chr);
                        continue;
                    }

                    if (match[chr] == stack.Peek())
                    {
                        stack.Pop();
                        continue;
                    }

                    totscore += score[chr];

                    break;
                }

            }        

            return totscore;

       }

       public static long Task2()
       {
            var input = aocIO.GetStringList("day10.txt");

            var allscores = new List<long>();
            var match = new Dictionary<char, char>() { {')', '('}, {'>', '<'}, {'}', '{'}, {']', '['}  }; 
            foreach(var line in input)
            {
                var stack = new Stack<char>(); 
                bool bErr = false;
                for(int i =0; i <line.Length; i++)
                {
                    char chr = line[i];

                    if (match.ContainsValue(chr))
                    {
                        stack.Push(chr);
                        continue;
                    }

                    if (match[chr] == stack.Peek())
                    {
                        stack.Pop();
                        continue;
                    }
                    bErr = true;
                    break;
                }

                // vynecham prazdne a dobre zpravy
                if (bErr || stack.Count <= 0) continue;
                var score = new Dictionary<char, long>() { {'(', 1}, {'<', 4}, {'{', 3}, {'[', 2}  };   

                long totscore = 0;
                while(stack.Count > 0)
                {
                    totscore *= 5;
                    totscore += score[stack.Pop()];
                }

                allscores.Add(totscore);

            }        

            allscores.Sort();
            return allscores[allscores.Count /2];
       }       
    }
}