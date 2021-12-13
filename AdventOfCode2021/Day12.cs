using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public static class Day12
    {
        public static int A()
        {
            var lines = File.ReadLines("InputData\\Day12.txt");
            
            var caves = new List<Cave>();

            foreach (var line in lines)
            {
                var path = line.Split('-');
                var cave1 = caves.Where(c => c.Name == path[0]).SingleOrDefault();
                var cave2 = caves.Where(c => c.Name == path[1]).SingleOrDefault();

                bool isCave1New = false;
                if(cave1 == null)
                {
                    cave1 = new Cave
                    {
                        Name = path[0]
                    };
                    isCave1New = true;
                }

                bool isCave2New = false;
                if(cave2 == null)
                {
                    cave2 = new Cave
                    {
                        Name = path[1]
                    };
                    isCave2New = true;
                }

                cave1.AdjacentCaves.Add(cave2);
                cave2.AdjacentCaves.Add(cave1);

                if (isCave1New)
                    caves.Add(cave1);

                if (isCave2New)
                    caves.Add(cave2);
            }

            var startCave = caves.Where(c => c.Name == "start").Single();
            startCave.Visits = 1;
            var allPaths = new List<string>();
            allPaths.Add(startCave.Name);

            allPaths = findPaths(startCave, allPaths, startCave.Name);

            List<string> findPaths(Cave currentCave, List<string> paths, string path)
            {
                if (currentCave.Name != "end")
                {
                    // loop through adjacent caves
                    foreach (var cave in currentCave.AdjacentCaves.Where(c => c.IsValid))
                    {
                        // path should be start, A | start, b
                        // on recursion
                        if (!(cave.Name == cave.Name.ToLower() && path.Contains(cave.Name)))
                        {
                            string newPath = $"{path},{cave.Name}";
                            paths.Add(newPath);
                            paths = findPaths(cave, paths, newPath);
                        }
                    }
                }
                return paths;
            }

            return allPaths.Where(x => x.EndsWith("end")).Count();
        }

        public static int B()
        {
            var lines = File.ReadLines("InputData\\Day12.txt");

            var caves = new List<Cave>();

            foreach (var line in lines)
            {
                var path = line.Split('-');
                var cave1 = caves.Where(c => c.Name == path[0]).SingleOrDefault();
                var cave2 = caves.Where(c => c.Name == path[1]).SingleOrDefault();

                bool isCave1New = false;
                if (cave1 == null)
                {
                    cave1 = new Cave
                    {
                        Name = path[0]
                    };
                    isCave1New = true;
                }

                bool isCave2New = false;
                if (cave2 == null)
                {
                    cave2 = new Cave
                    {
                        Name = path[1]
                    };
                    isCave2New = true;
                }

                cave1.AdjacentCaves.Add(cave2);
                cave2.AdjacentCaves.Add(cave1);

                if (isCave1New)
                    caves.Add(cave1);

                if (isCave2New)
                    caves.Add(cave2);
            }

            var startCave = caves.Where(c => c.Name == "start").Single();
            startCave.Visits = 1;
            var allPaths = new List<string>();
            allPaths.Add(startCave.Name);

            allPaths = findPaths(startCave, allPaths, startCave.Name);

            List<string> findPaths(Cave currentCave, List<string> paths, string path)
            {
                if (currentCave.Name != "end")
                {
                    // loop through adjacent caves
                    foreach (var cave in currentCave.AdjacentCaves.Where(c => c.Name != "start"))
                    {
                        // path should be start, A | start, b
                        // on recursion
                        if (cave.Name.ToUpper() == cave.Name || cave.Name.IsPathValid(path))
                        {
                            string newPath = $"{path},{cave.Name}";
                            paths.Add(newPath);
                            paths = findPaths(cave, paths, newPath);
                        }
                    }
                }
                return paths;
            }

            return allPaths.Where(x => x.EndsWith("end")).Count();
        }

        public static bool IsPathValid(this string caveName, string path)
        {
            bool isValid = true;
            var caveNames = path.Split(',').ToList();
            var twoVisits = "";
            foreach (var name in caveNames.Where(c => c.ToLower() == c && c != "start"))
            {
                var pattern = new Regex(name);
                var count = pattern.Matches(path).Count;
                if (count == 2)
                {
                    twoVisits = name;
                }
            }

            if (twoVisits != "" && (twoVisits == caveName || path.Contains(caveName)))
            {
                isValid = false;
            }

            return isValid;
        }
    }

    public class Cave
    {
        public Cave()
        {
            AdjacentCaves = new List<Cave>();
        }
        public string Name { get; set; }

        public List<Cave> AdjacentCaves { get; set; }

        public int Visits { get; set; }

        public bool IsSmall
        {
            get
            {
                if(Name == Name.ToUpper())
                {
                    return false;
                }
                return true;
            }
        }

        public bool IsValid
        {
            get
            {
                if (IsSmall && Visits > 0)
                    return false;
                return true;
            }
        }
    }

    

    /*
     * var paths = new List<string>();
            int pathAttemepts = 0;
            var random = new Random();

            while (pathAttemepts < 5000000)
            {
                resetCaves(caves);
                var startCave = caves.Where(c => c.Name == "start").Single();
                startCave.Visits = 1;
                var currentCave = startCave;

                string path = currentCave.Name;
                while (currentCave.Name != "end")
                {
                    bool validCave = false;
                    int validCaveAttempts = 0;
                    //break out if we've tried every adjacent cave and haven't found a path
                    while (!validCave && validCaveAttempts < currentCave.AdjacentCaves.Count + 5)
                    {
                        int choices = currentCave.AdjacentCaves.Count;
                        int choice = random.Next(0, choices - 1);

                        if (isValidCave(currentCave.AdjacentCaves[choice]))
                        {
                            validCave = true;
                            currentCave = currentCave.AdjacentCaves[choice];
                            currentCave.Visits++;
                            path = $"{path},{currentCave.Name}";
                        }
                        validCaveAttempts++;
                    }

                    // attempts to find a valid path have failed, double check
                    // to make sure random choice didn't miss any
                    if(validCaveAttempts > 0 && validCave == false)
                    {
                        for (int i = 0; i < currentCave.AdjacentCaves.Count; i++)
                        {
                            if (isValidCave(currentCave.AdjacentCaves[i]))
                            {
                                validCave = true;
                                currentCave = currentCave.AdjacentCaves[i];
                                currentCave.Visits++;
                                path = $"{path},{currentCave.Name}";
                            }
                        }
                    }

                    // after checking all paths we found nothing
                    // mark this cave as a dead end
                    if(!validCave)
                    {
                        Console.WriteLine($"{currentCave.Name} is a deadend");
                        caves.Remove(currentCave);
                        path = "";
                        break;
                    }
                }

                if (paths.Contains(path) || path == "")
                {
                    pathAttemepts++;
                }
                else
                {
                    Console.WriteLine(path);
                    paths.Add(path);
                    pathAttemepts = 0;
                }
            }*/
}
