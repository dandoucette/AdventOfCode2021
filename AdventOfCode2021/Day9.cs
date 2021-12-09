using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public static class Day9
    {
        public static int A()
        {
            var lines = File.ReadLines("InputData\\Day9.txt").ToList();
            int x = lines[0].Length;
            int y = lines.Count;
            var map = new int[x, y];

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    map[i, j] = int.Parse(lines[i][j].ToString());
                }
            }

            var lowPoints = new List<int>();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    bool isLowestPoint = true;
                    int point = map[i, j];

                    //up
                    if(i != 0)
                    {
                        if (point >= map[i - 1, j])
                            isLowestPoint = false;
                    }

                    //left
                    if(j != 0)
                    {
                        if (point >= map[i, j - 1])
                            isLowestPoint = false;
                    }

                    //down
                    if(i != map.GetLength(0) - 1)
                    {
                        if (point >= map[i + 1, j])
                            isLowestPoint = false;
                    }

                    //right
                    if(j != map.GetLength(1) - 1)
                    {
                        if (point >= map[i, j + 1])
                            isLowestPoint = false;
                    }

                    if (isLowestPoint)
                        lowPoints.Add(point + 1);
                }
            }
            return lowPoints.Sum(x => x);
        }

        public static int B()
        {
            return 0;
        }
    }
}
