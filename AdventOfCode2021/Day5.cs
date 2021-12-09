using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public static class Day5
    {
        public static int A()
        {
            var lines = File.ReadLines("InputData\\Day5.txt");
            var grid = new int[1000, 1000];

            foreach (var line in lines)
            {
                var pair = line.Split(" -> ");
                var start = pair[0].Split(',');
                var end = pair[1].Split(',');

                int x1 = int.Parse(start[0]);
                int y1 = int.Parse(start[1]);

                int x2 = int.Parse(end[0]);
                int y2 = int.Parse(end[1]);

                if(x1 == x2)
                {
                    int a = y2 > y1 ? y1 : y2;
                    int b = y2 > y1 ? y2 : y1;

                    for (int i = a; i <= b; i++)
                    {
                        grid[x1, i]++;
                    }
                }
                else if (y1 == y2)
                {
                    int a = x2 > x1 ? x1 : x2;
                    int b = x2 > x1 ? x2 : x1;

                    for (int i = a; i <= b; i++)
                    {
                        grid[i, y1]++;
                    }
                }
            }

            int count = 0;
            for (int x = 0; x < 1000; x++)
            {
                for (int y = 0; y < 1000; y++)
                {
                    if (grid[x, y] > 1)
                        count++;
                }
            }

            return count;
        }

        public static int B()
        {
            int size = 1000;
            var lines = File.ReadLines("InputData\\Day5.txt");
            var grid = new int[size, size];

            foreach (var line in lines)
            {
                var pair = line.Split(" -> ");
                var start = pair[0].Split(',');
                var end = pair[1].Split(',');

                int x1 = int.Parse(start[0]);
                int y1 = int.Parse(start[1]);

                int x2 = int.Parse(end[0]);
                int y2 = int.Parse(end[1]);

                if (x1 == x2)
                {
                    int a = y2 > y1 ? y1 : y2;
                    int b = y2 > y1 ? y2 : y1;

                    for (int i = a; i <= b; i++)
                    {
                        grid[i, x1]++;
                    }
                }
                else if (y1 == y2)
                {
                    int a = x2 > x1 ? x1 : x2;
                    int b = x2 > x1 ? x2 : x1;

                    for (int i = a; i <= b; i++)
                    {
                        grid[y1, i]++;
                    }
                }
                else if (Math.Abs(x1 - x2) == Math.Abs(y1 - y2))
                {
                    //diagonal
                    // 8,0 -> 0,8
                    // 6,4 -> 2,0
                    //Console.WriteLine($"Diagonal - {line}");

                    int y = y1;

                    if (x1 < x2)
                    {
                        for (int x = x1; x <= x2; x++)
                        {
                            
                            grid[y, x]++;
                            
                            if(y1 < y2)
                            {
                                y++;
                            }
                            else
                            {
                                y--;
                            }
                        }
                    }
                    else
                    {
                        for (int x = x1; x >= x2; x--)
                        {
                            grid[y, x]++;

                            if (y1 < y2)
                            {
                                y++;
                            }
                            else
                            {
                                y--;
                            }
                        }
                    }

                }
            }

            int count = 0;
            for (int x = 0; x < size; x++)
            {
                string line = "";
                for (int y = 0; y < size; y++)
                {
                    line += grid[x, y] == 0 ? "." : $"{grid[x, y]}";
                    if (grid[x, y] > 1)
                        count++;
                }
                //Console.WriteLine(line);
            }

            return count;
        }
    }
}
