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
            var cave = new List<string>();
            foreach (var line in lines)
            {
                cave.Add(line);
            }

            var start = new Tile
            {
                X = 0,
                Y = 0,
                Risk = 0
            };

            var finish = new Tile
            {
                X = lines.First().Length - 1,
                Y = lines.Count - 1
            };

            start.SetDistances(finish.X, finish.Y);
            var activeTiles = new List<Tile>();
            activeTiles.Add(start);
            var visitedTiles = new List<Tile>();
            var completedPaths = new List<Tile>();

            while(activeTiles.Any())
            {
                var checkTile = activeTiles.OrderBy(t => t.ManhattanCost).ThenBy(t => t.Risk).First();
                if (checkTile.X == finish.X && checkTile.Y == finish.Y)
                {
                    finish = checkTile;
                    break;
                }
                
                visitedTiles.Add(checkTile);
                activeTiles.Remove(checkTile);
                

                foreach (var neighbourTile in GetValidNeighbours(cave, checkTile, finish, 1))
                {
                    if (visitedTiles.Any(tile => tile.X == neighbourTile.X && tile.Y == neighbourTile.Y))
                        continue;

                    if (activeTiles.Any(tile => tile.X == neighbourTile.X && tile.Y == neighbourTile.Y))
                    {
                        var existingTile = activeTiles.First(tile => tile.X == neighbourTile.X && tile.Y == neighbourTile.Y);
                        if (existingTile.ManhattanCost > checkTile.ManhattanCost)
                        {
                            activeTiles.Remove(existingTile);
                            activeTiles.Remove(neighbourTile);
                        }
                    }
                    else
                    {
                        activeTiles.Add(neighbourTile);
                    }
                }
            }

            Tile lastTile = finish;
            string path = lastTile.Risk.ToString();
            while(lastTile.Parent != null)
            {
                path = $"{lastTile.Parent.Risk},{path}";
                lastTile = lastTile.Parent;
            }
            Console.WriteLine(path);
            
            return finish.TotalRisk;

        }

        public static int B()
        {
            return 0;
        }

        private static List<Tile> GetValidNeighbours(List<string> map, Tile currentTile, Tile targetTile, int stepsToLookAhead)
        {

            var validTiles = GetNeighbours(currentTile);            
            
            validTiles.ForEach(tile => tile.SetDistances(targetTile.X, targetTile.Y));

            //store all tiles we know about
            var allTiles = new List<Tile>
            {
                currentTile
            };
            allTiles.AddRange(validTiles);

            //store only tiles we need to explore
            var stepTiles = validTiles;

            //new tiles will show the last set of tiles we explored
            var newTilesOnly = new List<Tile>();

            for (int i = 0; i < stepsToLookAhead; i++)
            {
                newTilesOnly.Clear();
                foreach (var tile in stepTiles)
                {
                    var neighbours = GetNeighbours(tile);
                    allTiles.AddRange(neighbours.Where(t => !allTiles.Any(t2 => t2.X == t.X && t2.Y == t.Y)));
                    newTilesOnly.AddRange(neighbours);
                }
                newTilesOnly.RemoveAll(t => t.X == currentTile.X && t.Y == currentTile.Y);
                stepTiles = newTilesOnly;
            }

            foreach (var tile in validTiles)
            {
                tile.LookAheadRisk = newTilesOnly.Where(t => t.Parents.Any(t2 => t2.X == tile.X && t2.Y == tile.Y)).Min(t => t.TotalRisk);
            }
            
            return validTiles;

            List<Tile> GetNeighbours(Tile sourceTile)
            {
                var possiblities = new List<Tile>()
                {
                    new Tile { X = sourceTile.X, Y = sourceTile.Y - 1, Parent = sourceTile },
                    new Tile { X = sourceTile.X, Y = sourceTile.Y + 1, Parent = sourceTile },
                    new Tile { X = sourceTile.X - 1, Y = sourceTile.Y, Parent = sourceTile },
                    new Tile { X = sourceTile.X + 1, Y = sourceTile.Y, Parent = sourceTile },
                };

                int maxX = map.First().Length - 1;
                int maxY = map.Count - 1;

                var validTiles =  possiblities.Where(t => t.X >= 0 && t.X <= maxX
                                   && t.Y >= 0 && t.Y <= maxY).ToList();
                validTiles.ForEach(tile => tile.Risk = int.Parse(map[tile.Y][tile.X].ToString()));

                return validTiles;
            }
        }
    }

    public class Tile
    {
        public int Risk { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int ManhattanDistance { get; set; }
        public double EuclideanDistance { get; set; }
        public int ManhattanCost => (Risk + (LookAheadRisk - TotalRisk)) + ManhattanDistance;
        public double EuclideanCost => LookAheadRisk + EuclideanDistance;
        public Tile Parent { get; set; }
        public int LookAheadRisk { get; set; }

        public int TotalRisk
        {
            get
            {
                return Parents.Sum(t => t.Risk);
            }
        }

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

        public void SetDistances(int targetX, int targetY)
        {
            this.ManhattanDistance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
            this.EuclideanDistance = Math.Sqrt(Math.Pow(Math.Abs(targetX - X), 2) + Math.Pow(Math.Abs(targetY - Y), 2));
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public (int X, int Y) SubtractPoint(Point point)
        {
            int x = Math.Abs(point.X - X);
            int y = Math.Abs(point.Y - Y);
            return (x, y);
        }

        public bool Equals(Point point)
        {
            return point.X == X && point.Y == Y;
        }
    }
}