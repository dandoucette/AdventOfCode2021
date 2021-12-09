using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public static class Day6
    {
        public static int A()
        {
            var input = File.ReadAllText("InputData\\Day6.txt");
            var stringArray = input.Split(',');
            var fishes = new List<int>();

            foreach (var fish in stringArray)
            {
                fishes.Add(int.Parse(fish));
            }

            for (int i = 0; i < 80; i++)
            {
                int fishesToAdd = 0;
                for (int j = 0; j < fishes.Count; j++)
                {
                    if(fishes[j] == 0)
                    {
                        fishes[j] = 6;
                        fishesToAdd++;
                    }
                    else
                    {
                        fishes[j]--;
                    }
                }

                for (int x = 0; x < fishesToAdd; x++)
                {
                    fishes.Add(8);
                }
            }

            return fishes.Count;
        }

        public static long B()
        {
            var input = File.ReadAllText("InputData\\Day6.txt");
            var additions = new List<long>();

            long total = 0;

            for (int i = 1; i < 10; i++)
            {
                var fishes = input.Split(',');
                
                if (total == 0)
                    total = fishes.Length;

                int fishesToAdd = 0;
                for (int j = 0; j < fishes.Length; j++)
                {
                    if (int.Parse(fishes[j]) == 0)
                    {
                        fishes[j] = "6";
                        fishesToAdd++;
                    }
                    else
                    {
                        fishes[j] = (int.Parse(fishes[j]) - 1).ToString();
                    }
                }

                input = string.Join(',', fishes);
                if(fishesToAdd > 0)
                    input = $"{input},{string.Join(',', Enumerable.Repeat("8", fishesToAdd))}";

                additions.Add(fishesToAdd);
            }

            for (int i = 10; i <= 256; i++)
            {
                additions.Add(additions[i - 10] + additions[i - 8]);
            }

            total += additions.Sum(x => x);

            return total;
        }
    }
}
