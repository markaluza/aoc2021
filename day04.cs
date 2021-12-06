using System.Collections.Generic;
using System.Linq;
using System;

namespace aoc2021
{
    class Day4
    {

        class Board
        {
            int[,] number = new int[5,5];
            bool[,] used = new bool[5,5];

            public int boardno = 0;
            public Board(int b) { boardno = b; }

            public void SetNmb(int x, int y, int nmb)
            {
                number[x,y] = nmb;
                used[x,y] = false;
            }

            public bool Turn(int nmb)
            {
                // vraci win = true/ not yet false
                for (int x =0; x < 5; x++)
                {
                    bool colok = true;
                    for (int y = 0; y < 5; y++)
                    {
                        if (number[x,y] == nmb)
                        {
                            used[x,y] = true;
                        }
                        colok = colok & used[x,y];
                    }
                    if (colok) 
                    {
                        return true;
                    }
                }

                for (int y =0; y < 5; y++)
                {
                    bool brok = true;
                    for (int x = 0; x < 5; x++)
                    {
                        if (used[x,y]) continue;
                        brok = false;
                        break;
                    }
                   if (brok) return true;
                }

                return false;

            }

            public long SumNotUsed()
            {
                long sum = 0;
                for (int x =0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        if (!used[x,y])
                        {
                            sum += number[x,y];
                        }
                    }
                }
                return sum;
            }

        }

        class Input
        {
            public List<Board> Boards = new List<Board>();
            
            public List<int> Moves = new List<int>();

            public Input()
            {
                var lines = aocIO.GetStringList("day04.txt");

                Moves = lines[0].Split(',').Select(int.Parse).ToList();

                for (int b =0;  ;b++)
                {
                   int firstline = b * 6 + 2;
                   if (firstline >= lines.Count) break;

                    var board = new Board(b+1);

                    for (int l = 0; l < 5; l++)
                    {
                        for (int n = 0; n < 5; n++)
                        {
                            int nmb = int.Parse(lines[firstline + l].Substring(n *3, 2));
                            board.SetNmb(n, l, nmb);
                        }

                    }

                    Boards.Add(board);

                }

            }

            public long Game1()
            {
                for (int move =0; move < Moves.Count; move++)
                {
                    foreach(var b in Boards)
                    {
                        if (b.Turn(Moves[move]))
                        {
                            return b.SumNotUsed() * Moves[move];
                        }
                    }
                }
                return -1;
            }

            public long Game2()
            {

                int lastmove = 0;
                Board lastboard = null;
                for (int move =0; move < Moves.Count; move++)
                {
                    for (int b = 0; b < Boards.Count; b++)
                    {
                        if (Boards[b].Turn(Moves[move]))
                        {
                            lastboard = Boards[b];
                            Boards.RemoveAt(b);
                            b--;
                        }
                    }

                    lastmove = Moves[move];
                    
                    if (Boards.Count == 0) 
                    {
                        break;
                    }

                }

                return lastboard.SumNotUsed() * lastmove;
            }


        }

        // bingo
       public static long Task1()
       {
           var input = new Input();
           return input.Game1();
       }

       public static long Task2()
       {
        
           var input = new Input();
           return input.Game2();
       }       
    }
}