using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace aoc2021
{
    class Day5
    {

        public class Line
        {
            
            public Line(int x1, int y1, int x2, int y2) 
            { 
                this.x1 = x1; this.x2 = x2;
                this.y1 = y1; this.y2 = y2;
            }

            int x1, y1;
            int x2, y2;
        }

        static List<Line> ReadInput()
        {
            var ret = new List<Line>();

            var lines = aocIO.GetStringList("day05.txt");
            foreach(var line in lines)
            {
                Match res = Regex.Match(
                    line, 
                    @"(\d+),(\d+).->.(\d+),(\d)"
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

        // bingo
       public static long Task1()
       {
           var input = ReadInput();
           return 0;
       }

       // tbd
       public static long Task2()
       {
           return 0;
       }       
    }
}