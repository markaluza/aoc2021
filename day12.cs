using System.Collections.Generic;

namespace aoc2021
{
    class Day12
    {

        private static Dictionary<string,List<string>> GetInput()
        {
            var dict = new Dictionary<string,List<string>>();
            var lines = aocIO.GetStringList("day12.txt");
            foreach(var line in lines)
            {
                var joints = line.Split("-");
                if (!dict.ContainsKey(joints[0])) dict[joints[0]] = new List<string>();
                if (!dict.ContainsKey(joints[1])) dict[joints[1]] = new List<string>();

                dict[joints[0]].Add(joints[1]);
                dict[joints[1]].Add(joints[0]);

            }
            
            return dict;
        }

        class Path
        {
     

            public List<string> strpath = new List<string>();
                        
            public List<Path> Recurse(Dictionary<string,List<string>> dict) 
            {
                List<Path> ret = new List<Path>();

                var strlist = dict[strpath[strpath.Count -1]];
                foreach(var test in  strlist) 
                {

                    if (test == "end") 
                    {
                        ret.Add(this);
                        continue;
                    }

                    if (char.IsLower(test[0]) && strpath.Contains(test)) continue;

                    Path p = new Path();
                    p.strpath = new List<string>(strpath);
                    p.strpath.Add(test);

                    var rec = p.Recurse(dict);
                    ret.AddRange(rec);

                }

                return ret;
            }
                            
        }

       public static long Task1()
       {
            var input = GetInput();

            var paths = new List<Path>();
            foreach(var path in input["start"])
            {
                Path p = new Path();
                p.strpath.Add("start");
                p.strpath.Add(path);

                paths.AddRange(p.Recurse(input));

            }

            return paths.Count;            

       }

       public static long Task2()
       {                


           return 0;
       }       
    }
}