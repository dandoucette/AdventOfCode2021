using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public static class Day7
    {
        public static int A()
        {
            var input = File.ReadAllText("InputData\\Day7.txt");
            var positions = input.Split(',');
            var crabs = new List<int>();

            foreach (var pos in positions)
            {
                crabs.Add(int.Parse(pos));
            }

            int avg = (int)crabs.Average(x => x);
            int min = crabs.Min(x => x);
            int max = crabs.Max(x => x);

            var fuelCosts = new List<FuelCost>();
            int increaseCount = 0;
            
            for (int i = avg; i > min; i--)
            {
                int fuel = 0;
                for (int j = 0; j < crabs.Count; j++)
                {
                    fuel += Math.Abs(i - crabs[j]);
                }

                if (fuelCosts.Count > 0)
                {
                    if (fuel > fuelCosts.Last().FuelNeeded)
                    {
                        increaseCount++;
                    }
                    else
                    {
                        increaseCount = 0;
                    }
                }

                if (increaseCount >= 10)
                    break;

                fuelCosts.Add(new FuelCost
                {
                    Position = i,
                    FuelNeeded = fuel
                });
            }

            increaseCount = 0;

            for (int i = avg; i < max; i++)
            {
                int fuel = 0;
                for (int j = 0; j < crabs.Count; j++)
                {
                    fuel += Math.Abs(i - crabs[j]);
                }

                if (fuel > fuelCosts.Last().FuelNeeded)
                {
                    increaseCount++;
                }
                else
                {
                    increaseCount = 0;
                }

                if (increaseCount >= 10)
                    break;

                fuelCosts.Add(new FuelCost
                {
                    Position = i,
                    FuelNeeded = fuel
                });
            }

            return fuelCosts.Min(x => x.FuelNeeded);
        }

        public static int B()
        {
            int calcFuel(int moves)
            {
                int fuel = 0;
                for (int i = 1; i <= moves; i++)
                {
                    fuel += i;
                }
                return fuel;
            }

            var input = File.ReadAllText("InputData\\Day7.txt");
            var positions = input.Split(',');
            var crabs = new List<int>();

            foreach (var pos in positions)
            {
                crabs.Add(int.Parse(pos));
            }

            int bestFuelCost = 1000000000;
            int bestPosition = -1;
            int min = crabs.Min(x => x);
            int max = crabs.Max(x => x);

            for (int i = min; i <= max; i++)
            {
                int fuelNeeded = 0;
                foreach (var crab in crabs)
                {
                    int moves = Math.Abs(i - crab);
                    fuelNeeded += calcFuel(moves);
                }

                if(fuelNeeded < bestFuelCost)
                {
                    bestFuelCost = fuelNeeded;
                    bestPosition = i;
                }
            }

            return bestFuelCost;
        }
    }

    public class FuelCost
    {
        public int Position { get; set; }
        public int FuelNeeded { get; set; }
    }
}
