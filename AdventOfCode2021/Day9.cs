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
            var lines = File.ReadLines("InputData\\Day9.txt").ToList();
            int x = lines[0].Length;
            int y = lines.Count;
            var map = new int[y, x];
            string usedCoordinates = "";

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    map[i, j] = int.Parse(lines[i][j].ToString());
                }
            }

            var basinSizes = new List<int>();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    bool isLowestPoint = true;
                    int point = map[i, j];

                    //9 can't be a lowest point
                    if (point == 9)
                        continue;

                    //up
                    if (i != 0)
                    {
                        if (point >= map[i - 1, j])
                            isLowestPoint = false;
                    }

                    //left
                    if (j != 0)
                    {
                        if (point >= map[i, j - 1])
                            isLowestPoint = false;
                    }

                    //down
                    if (i != map.GetLength(0) - 1)
                    {
                        if (point >= map[i + 1, j])
                            isLowestPoint = false;
                    }

                    //right
                    if (j != map.GetLength(1) - 1)
                    {
                        if (point >= map[i, j + 1])
                            isLowestPoint = false;
                    }

                    if (isLowestPoint)
                    {
                        //find all parts of the basin
                        //usedCoordinates = "";
                        int basinSize = getAdjacentPoints(i, j);
                        basinSizes.Add(basinSize);
                    }
                }
            }

            int getAdjacentPoints(int x, int y)
            {
                int point = map[x, y];
                int up = 0;
                int down = 0;
                int left = 0;
                int right = 0;
                int count = 0;

                int markPoint(int x, int y)
                {
                    string coordinates = $"~{x}{y}";
                    if (!usedCoordinates.Contains(coordinates))
                    {
                        usedCoordinates += coordinates;
                        return 1 + getAdjacentPoints(x, y);
                    }

                    return 0;
                }

                //up
                if (x != 0)
                {
                    up = map[x - 1, y];
                    if (up != 9)// && up - point == 1)
                    {
                        count += markPoint(x - 1, y);
                    }
                }

                //left
                if (y != 0)
                {
                    left = map[x, y - 1];
                    if (left != 9)// && left - point == 1)
                    {
                        count += markPoint(x, y - 1);
                    }
                }

                //down
                if (x != map.GetLength(0) - 1)
                {
                    down = map[x + 1, y];
                    if (down != 9 )//&& down - point == 1)
                    {
                        count += markPoint(x + 1, y);
                    }
                }

                //right
                if (y != map.GetLength(1) - 1)
                {
                    right = map[x, y + 1];
                    if (right != 9 )//&& right - point == 1)
                    {
                        count += markPoint(x, y + 1);
                    }
                }

                return count;
            }


            var basins = basinSizes.OrderByDescending(x => x).Take(3).ToList();

            
            return basins[0] * basins[1] * basins[2];
        }
    }
}
