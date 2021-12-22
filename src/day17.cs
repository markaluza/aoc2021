using System;
using System.Linq;
using System.Collections.Generic;

namespace aoc2021
{
    class Day17
    {

        static int integrate(int dest) // integrace tehle funkce je obycejny trojuhelnik
        {
            return (dest * dest + dest)/2;
        }
        
       public static long Task1()
       {    

           // x=240..292, y=-90..-57
           // tak to byl zahul :)  
           // nejvyssi rychlosti dosahneme tak, ze od 0 -> -90(dolni hrana obdelniku) je v jednom kroku -> znamena ze predchozi krok byl -90 + 1
           // predchozi krok ma stejnou rychlost jako prvni jen s opacnym znamenkem a voilaaaa :)
           return integrate(-(-90+1));

       }

       public static long Task2()
       {        
            return 0;
       }       
    }
}