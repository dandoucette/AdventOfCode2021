using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public static class Day11
    {
        public static int A()
        {
            var lines = File.ReadLines("InputData\\Day11Test.txt").ToList();
            int horizontalMax = lines[0].Length;
            int verticalMax = lines.Count;
            var octopodes = new int[horizontalMax, verticalMax];

            for (int y = 0; y < verticalMax; y++)
            {
                for (int x = 0; x < horizontalMax; x++)
                {
                    octopodes[y, x] = int.Parse(lines[y][x].ToString());
                }
            }


            int flashCount = 0; ;
            for (int step = 0; step < 5; step++)
            {
                var flashes = new List<(int y, int x)>();

                Console.WriteLine($"Start of step {step+1}: {octopodes[9, 8]}");
                for (int y = 0; y < verticalMax; y++)
                {
                    for (int x = 0; x < horizontalMax; x++)
                    {
                        octopodes[y, x]++;
                        if(octopodes[y, x] == 10)
                        {
                            flashes.Add((y, x));
                        }
                    }
                }
                Console.WriteLine($"After increase: {octopodes[9, 8]}");

                while (flashes.Count > 0)
                {
                    Console.WriteLine($"Start of adjacent: {octopodes[9, 8]}");
                    var newFlashes = new List<(int y, int x)>();
                    foreach (var (y, x) in flashes)
                    {
                        flashAdjacents(y, x, ref newFlashes);
                    }
                    flashes.Clear();
                    flashes.AddRange(newFlashes);
                    Console.WriteLine($"After adjacent: {octopodes[9, 8]}");
                }

                Console.WriteLine($"Before count: {octopodes[9, 8]}");
                int stepCount = 0;
                for (int y = 0; y < verticalMax; y++)
                {
                    for (int x = 0; x < horizontalMax; x++)
                    {
                        if(octopodes[y, x] > 9)
                        {
                            octopodes[y, x] = 0;
                            flashCount++;
                            stepCount++;
                        }
                    }
                }
                Console.WriteLine($"End of step {step+1}: {octopodes[9, 8]}");
                //printOctopodes(step);
                //Console.WriteLine($"Step {step + 1} count: {stepCount}");
                //Console.WriteLine();
            }

           
            void flashAdjacents(int y, int x, ref List<(int y, int x)> flashes)
            {
                
                for (int j = (y - 1 < 0 ? 0 : y - 1); j <= (y + 1 > 9 ? 9 : y + 1); j++)
                {
                    for (int i = (x - 1 < 0 ? 0 : x - 1); i <= (x + 1 > 9 ? 9 : x + 1); i++)
                    {
                        if (i == x && j == y)
                            continue;

                        octopodes[j, i]++;

                        if (octopodes[j, i] == 10 && flashes.Where(f => f.y == y && f.x == i).Count() == 0)
                            flashes.Add((j, i));
                    }
                }
                
            }

            return flashCount;


            void printOctopodes(int step)
            {
                Console.WriteLine($"Step {step + 1}");
                for (int y = 0; y < verticalMax; y++)
                {
                    string line = "";
                    for (int x = 0; x < horizontalMax; x++)
                    {
                        line += octopodes[y, x];
                    }
                    Console.WriteLine(line);
                }
                Console.WriteLine();
            }
        }

        public static int B()
        {
            return 0;
        }
    }
}
