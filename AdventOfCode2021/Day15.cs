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
            Tile[][] cave;
            cave = File.ReadAllLines("InputData\\Day15.txt")
                .Select((row, y) => row
                    .Select((number, x) => new Tile
                    {
                        Risk = Convert.ToInt32(number.ToString()),
                        Y = y,
                        X = x
                    }).ToArray())
                .ToArray();


            var activeTiles = new List<Tile>();
            cave[0][0].Distance = 0;
            activeTiles.Add(cave[0][0]);

            while (activeTiles.Any())
            {
                var checkTile = activeTiles.OrderBy(t => t.Distance).First();
                activeTiles.Remove(checkTile);
                checkTile.NotInQueue = true;

                foreach (var neighbourTile in GetValidNeighbours(cave, checkTile.Y, checkTile.X))
                {
                    var cost = checkTile.Distance + neighbourTile.Risk;
                    if (cost >= neighbourTile.Distance) continue;

                    neighbourTile.Distance = cost;
                    neighbourTile.Parent = checkTile;
                    activeTiles.Add(neighbourTile);
                }
            }


            return cave[^1][^1].Distance;

        }

        public static int B()
        {
            Tile[][] initialCave;
            initialCave = File.ReadAllLines("InputData\\Day15.txt")
                .Select((row, y) => row
                    .Select((number, x) => new Tile
                    {
                        Risk = Convert.ToInt32(number.ToString()),
                        Y = y,
                        X = x
                    }).ToArray())
                .ToArray();

            int offsetY = initialCave.Length;
            int offsetX = initialCave[0].Length;

            var cave = new Tile[offsetY * 5][];

            for (int y = 0; y < initialCave.Length; y++)
            {
                cave[y] = new Tile[offsetX * 5];
                for (int x = 0; x < initialCave[y].Length; x++)
                {
                    int initialValue = initialCave[y][x].Risk;
                    //cave[y][x] = initialCave[y][x];
                    for (int i = 0; i < 5; i++)
                    {
                        if(cave[y + (offsetY * i)] == null)
                        {
                            cave[y + (offsetY * i)] = new Tile[offsetX * 5];
                        }

                        cave[y + (offsetY * i)][x] = new Tile
                        {
                            Y = y + (offsetY * i),
                            X = x,
                            Risk = initialValue.Add(i)
                        };
                        
                        for (int j = 0; j < 5; j++)
                        {
                            cave[y + (offsetY * i)][x + (offsetX * j)] = new Tile
                            {
                                Y = y + (offsetY * i),
                                X = x + (offsetX * j),
                                Risk = initialValue.Add(i + j)
                            };
                        }
                    }
                }
            }

            void PrintCave()
            {
                for (int y = 0; y < cave.Length; y++)
                {
                    string line = "";
                    for (int x = 0; x < cave[y].Length; x++)
                    {
                        line = $"{line}{cave[y][x].Risk}";
                    }
                    Console.WriteLine(line);

                }
            }

            var activeTiles = new List<Tile>();
            cave[0][0].Distance = 0;
            activeTiles.Add(cave[0][0]);

            while (activeTiles.Any())
            {
                var checkTile = activeTiles.OrderBy(t => t.Distance).First();
                activeTiles.Remove(checkTile);
                checkTile.NotInQueue = true;

                foreach (var neighbourTile in GetValidNeighbours(cave, checkTile.Y, checkTile.X))
                {
                    var cost = checkTile.Distance + neighbourTile.Risk;
                    if (cost >= neighbourTile.Distance) continue;

                    neighbourTile.Distance = cost;
                    neighbourTile.Parent = checkTile;
                    activeTiles.Add(neighbourTile);
                }
            }


            return cave[^1][^1].Distance;
        }

        private static List<Tile> GetValidNeighbours(Tile[][] cave, int y, int x)
        {
            var validNeighbours = new List<Tile>();

            if (y > 0 && !cave[y - 1][x].NotInQueue)
                validNeighbours.Add(cave[y - 1][x]);     // Up

            if (y < cave.Length - 1 && !cave[y + 1][x].NotInQueue)
                validNeighbours.Add(cave[y + 1][x]);     // Down

            if (x > 0 && !cave[y][x - 1].NotInQueue)
                validNeighbours.Add(cave[y][x - 1]);     // Left

            if (x < cave[y].Length - 1 && !cave[y][x + 1].NotInQueue)
                validNeighbours.Add(cave[y][x + 1]);     // Right

            return validNeighbours;
        }

        internal class Tile
        {
            public int Risk { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public int Distance { get; set; } = int.MaxValue;  // default value
            public bool NotInQueue { get; set; }
            public Tile Parent { get; set; }
            public int TotalRisk => Parents.Sum(t => t.Risk);
            public List<Tile> Parents
            {
                get
                {
                    var parents = new List<Tile>();
                    Tile tile = this;
                    while (tile.Parent != null)
                    {
                        parents.Add(tile);
                        tile = tile.Parent;
                    }
                    return parents;
                }
            }
        }

        public static int Add(this int value, int amount)
        {
            if(value + amount > 9)
            {
                return value + amount - 9;
            }
            else
            {
                return value + amount;
            }
        }
    }
}