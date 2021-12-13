using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public static class Day13
    {
        public static int A()
        {
            const string foldAlong = "fold along ";
            var lines = File.ReadLines("InputData\\Day13.txt");

            var coordinates = new List<Coordinate>();
            var folds = new List<Fold>();
            
            foreach (var line in lines)
            {
                if(line.Length > 0)
                {
                    if(line.Contains(foldAlong))
                    {
                        var input = line.Replace(foldAlong, "");
                        var data = input.Split('=');
                        folds.Add(new Fold
                        {
                            Direction = data[0],
                            Location = int.Parse(data[1])
                        });
                    }
                    else
                    {
                        var data = line.Split(',');
                        int x = int.Parse(data[0]);
                        int y = int.Parse(data[1]);
                        coordinates.Add(new Coordinate
                        {
                            X = x,
                            Y = y
                        });
                    }
                }
            }
            //printPaper(coordinates);

            foreach (var fold in folds)
            {
                if(fold.Direction == "y")
                {
                    foreach (var coord in coordinates.Where(c => c.Y > fold.Location))
                    {
                        var difference = coord.Y - fold.Location;
                        int newY = fold.Location - difference;
                        if(!coordinates.Where(c => c.X == coord.X && c.Y == newY).Any())
                            coord.Y = newY;
                    }

                    coordinates.RemoveAll(c => c.Y > fold.Location);
                }
                else if (fold.Direction == "x")
                {
                    foreach (var coord in coordinates.Where(c => c.X > fold.Location))
                    {
                        var difference = coord.X - fold.Location;
                        int newX = fold.Location - difference;

                        if (!coordinates.Where(c => c.X == newX && c.Y == coord.Y).Any())
                            coord.X = newX;
                    }

                    coordinates.RemoveAll(c => c.X > fold.Location);
                }

                //printPaper(coordinates);
            }

            printPaper(coordinates);
            return 0;

            void printPaper(List<Coordinate> coords)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine();

                int maxX = coordinates.Max(c => c.X);
                int maxY = coordinates.Max(c => c.Y);
                var display = new string[maxX + 1, maxY + 1];

                foreach (var coord in coordinates)
                {
                    display[coord.X, coord.Y] = "#";
                }


                for (int j = 0; j <= maxY; j++)
                {
                    var line = "";
                    for (int i = 0; i <= maxX; i++)
                    {
                        line += display[i, j] == "#" ? "#" : ".";
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

    public class Fold
    {
        public string Direction { get; set; }
        public int Location { get; set; }
    }

    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

    }
}
