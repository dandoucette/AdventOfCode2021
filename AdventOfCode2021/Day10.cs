using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public static class Day10
    {
        public static int A()
        {
            int score = 0;
            var lines = File.ReadLines("InputData\\Day10.txt");
            var points = new Dictionary<string, int>
            {
                {")", 3 },
                {"]", 57 },
                {"}", 1197 },
                {">", 25137 }
            };

            foreach (var line in lines)
            {
                //int parenthesesCount = 0;   // ()
                //int bracketsCount = 0;      // []
                //int bracesCount = 0;        // {}
                //int chevronsCount = 0;      // <>
                // level[0] = "{";
                // level[1] = "(";
                // level[2] = "[";
                // level[3] = "(";
                // level[4] = "<";
                // level[5] = "{}"; //pair has completed, remove
                // level[5] = "["
                // level[6] = "<>"; //pair has complete
                // level[6] = "[]"; //complete
                // } opposite should match last non-complete level
                
                var levels = new List<string>();
                const string closingBrackets = ")]}>";
                const string validPairs = "~()~[]~{}~<>~";
                
                for (int i = 0; i < line.Length; i++)
                {
                    string bracket = line[i].ToString();

                    if(closingBrackets.Contains(bracket))
                    {
                        levels[^1] += bracket;
                        if(validPairs.Contains(levels[^1]))
                        {
                            levels.Remove(levels.Last());
                        }
                        else
                        {
                            //invalid pair
                            Console.WriteLine($"expected {levels[^1][0]} but got {bracket}");
                            score += points[bracket];
                            break;
                        }
                    }
                    else
                    {
                        levels.Add(bracket);
                    }
                }
            }

            return score;
        }

        public static long B()
        {
            var scores = new List<long>();
            var lines = File.ReadLines("InputData\\Day10.txt");
            var points = new Dictionary<string, long>
            {
                {"(", 1 },
                {"[", 2 },
                {"{", 3 },
                {"<", 4 }
            };

            foreach (var line in lines)
            {
                long lineScore = 0;
                var levels = new List<string>();
                const string closingBrackets = ")]}>";
                const string validPairs = "~()~[]~{}~<>~";
                bool isInvalidLine = false;

                for (int i = 0; i < line.Length; i++)
                {
                    string bracket = line[i].ToString();

                    if (closingBrackets.Contains(bracket))
                    {
                        levels[^1] += bracket;
                        if (validPairs.Contains(levels[^1]))
                        {
                            levels.Remove(levels.Last());
                        }
                        else
                        {
                            //invalid pair
                            //Console.WriteLine($"expected {levels[^1][0]} but got {bracket}");
                            isInvalidLine = true;
                            break;
                        }
                    }
                    else
                    {
                        levels.Add(bracket);
                    }
                }

                if (!isInvalidLine)
                {
                    for (int i = levels.Count - 1; i >= 0; i--)
                    {
                        lineScore = (lineScore * 5) + points[levels[i]];
                    }
                    Console.WriteLine(lineScore);
                    scores.Add(lineScore);
                }
            }

            scores = scores.OrderBy(x => x).ToList();

            return scores[scores.Count / 2];
        }
    }
}
