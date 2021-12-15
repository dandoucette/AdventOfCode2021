using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public static class Day15
    {
        public static int A()
        {
            var lines = File.ReadLines("InputData\\Day15.txt").ToList();
            int maxX = lines[0].Length;
            int maxY = lines.Count;
            var cave = new int[maxX, maxY];

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    cave[x, y] = int.Parse(lines[y][x].ToString());
                }
            }

            var paths = new List<string>();
            addPath(0, 0, "0");

            void addPath(int x, int y, string currentPath)
            {
                var path = "";
                if (x != maxX - 1)
                {
                    //newPaths.Add(addPath(x+1, y));
                    path = $"{currentPath},{cave[x + 1, y]}";
                    continuePath(x + 1, y, path);
                }
                
                if (y != maxY - 1)
                {
                    //newPaths.Add(addPath(x, y+1));
                    path = $"{currentPath},{cave[x, y + 1]}";
                    continuePath(x, y + 1, path);
                }

                string continuePath(int x1, int y1, string path1)
                {
                    if (!(x1 == maxX - 1 && y1 == maxY - 1))
                    {
                        addPath(x1, y1, path1);
                    }
                    else
                    {
                        paths.Add(path1);
                    }
                    return "";
                }
            }

            int leastRisk = 2000000000;
            foreach (var path in paths)
            {
                var risks = path.Split(',');
                int totalRisk = 0;
                for (int i = 0; i < risks.Length; i++)
                {
                    totalRisk += int.Parse(risks[i]);
                }
                if (totalRisk < leastRisk)
                    leastRisk = totalRisk;
            }

            return leastRisk;

        }

        public static int B()
        {
            return 0;
        }
    }

    public class Path
    {
        public int Risk { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}

/*List<List<Path>> addPaths(int x, int y, List<Path> currentPath)
            {
                var newPaths = new List<List<Path>>();

                if (x != maxX - 1)
                {
                    var path = addPath(x + 1, y);
                    if (path != null && path.Count > 0)
                        newPaths.Add(path);
                }

                if (y != maxY - 1)
                {
                    var path = addPath(x, y + 1);
                    if (path != null && path.Count > 0)
                        newPaths.Add(path);
                }

                //if (x != 0)
                //{
                //    var path = addPath(x - 1, y);
                //    if(path.Count > 0)
                //        newPaths.Add(path);
                //}

                //if(y != 0)
                //{
                //    var path = addPath(x, y - 1);
                //    if(path.Count > 0)
                //        newPaths.Add(path);
                //}

                List<Path> addPath(int x, int y)
                {
                    var newPath = new List<Path>();
                    if (!currentPath.Where(p => p.X == x && p.Y == y).Any())
                    {

                        newPath.AddRange(currentPath);
                        newPath.Add(
                            new Path
                            {
                                X = x,
                                Y = y,
                                Risk = cave[x, y]
                            });

                        if(!(x == maxX - 1 && y == maxY - 1))
                            newPaths.AddRange(addPaths(x, y, newPath));
                    }
                    return newPath;
                }

                return newPaths;
            }*/