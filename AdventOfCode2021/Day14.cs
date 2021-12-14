using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    public static class Day14
    {
        public static int A()
        {
            var lines = File.ReadLines("InputData\\Day14.txt").ToList();
            string template = lines[0];
            var pairInsertions = new Dictionary<string, string>();
            char lastCharacter = template[template.Length - 1];

            for (int i = 2; i < lines.Count; i++)
            {
                var data = lines[i].Split(" -> ");
                pairInsertions.Add(data[0], data[1]);
            }

            for (int i = 0; i < 10; i++)
            {
                string newPolymer = "";
                for (int j = 1; j < template.Length; j++)
                {
                    string pair = $"{template[j - 1]}{template[j]}";
                    newPolymer += $"{template[j - 1]}{pairInsertions[pair]}";
                }
                template = $"{newPolymer}{lastCharacter}";
            }

            string distinctCharacters = new string(template.Distinct().ToArray());
            var charCounts = new List<(string character, int count)>();

            for (int i = 0; i < distinctCharacters.Length; i++)
            {
                var letter = new Regex(distinctCharacters[i].ToString());
                charCounts.Add((distinctCharacters[i].ToString(), letter.Matches(template).Count));
            }

            charCounts = charCounts.OrderBy(c => c.count).ToList();

            return charCounts.Last().count - charCounts.First().count;
        }

        public static long B()
        {
            var lines = File.ReadLines("InputData\\Day14.txt").ToList();
            string template = lines[0];
            string firstAndLastLetters = $"{template[0]}{template[template.Length - 1]}";
            var pairInsertions = new Dictionary<string, string>();
            var letters = "";

            for (int i = 2; i < lines.Count; i++)
            {
                var data = lines[i].Split(" -> ");
                pairInsertions.Add(data[0], data[1]);
                letters += data[1];
            }

            // map the pairs to the pairs they will create
            // CH -> B so a CH will spawn a CB and BH (CBH)
            var pairMultipliers = new List<(string sourcePair, string createPair1, string createPair2)>();
            
            // we will count how many pairs we create since we can't store 20TB of data
            // (40 iterations) is estimated to create a 20TB string lol
            var pairCounts = new Dictionary<string, long>();
            

            foreach (var pair in pairInsertions)
            {
                string pair1 = $"{pair.Key[0]}{pair.Value}";
                string pair2 = $"{pair.Value}{pair.Key[1]}";
                pairMultipliers.Add((pair.Key, pair1, pair2));
                pairCounts.Add(pair.Key, 0);
            }

            // go through template and grab initial count of pairs
            for (int i = 1; i < template.Length; i++)
            {
                string pair = $"{template[i - 1]}{template[i]}";
                pairCounts[pair]++;
            }
            
            // now we will run out iterations and count the pairs it spawns
            for (int i = 0; i < 40; i++)
            {
                // check the number of each pair 1 CH will create 1 CB and 1 BH
                // We can't add it to the official count until the end
                var stepCount = createStepCount();
                foreach (var pair in pairCounts)
                {
                    var multiplier = pairMultipliers.Where(p => p.sourcePair == pair.Key).Single();
                    stepCount[multiplier.createPair1] += pair.Value;
                    stepCount[multiplier.createPair2] += pair.Value;
                }

                pairCounts = stepCount;
            }

            var letterCount = new Dictionary<string, long>();
            
            foreach (var pair in pairCounts)
            {
                for (int i = 0; i < pair.Key.Length; i++)
                {
                    string letter = pair.Key[i].ToString();
                    long count = pair.Value;  // 2 + (firstAndLastLetters.Contains(letter) ? 1 : 0);

                    if (letterCount.ContainsKey(letter))
                    {
                        letterCount[letter] += count;
                    }
                    else
                    {
                        letterCount.Add(letter, count);
                    }
                }
            }

            foreach (var letter in letterCount)
            {
                letterCount[letter.Key] = letter.Value / 2 + (firstAndLastLetters.Contains(letter.Key) ? 1 : 0);
            }

            return letterCount.Max(l => l.Value) - letterCount.Min(l => l.Value);

            Dictionary<string, long> createStepCount()
            {
                var pairs = new Dictionary<string, long>();

                foreach (var pair in pairInsertions)
                {
                    pairs.Add(pair.Key, 0);
                }

                return pairs;
            }
        }
    }
}

/*for (int i = 0; i < 12; i++)
            {
                string newPolymer = "";
                for (int j = 1; j < template.Length; j++)
                {
                    string pair = $"{template[j - 1]}{template[j]}";
                    newPolymer += $"{template[j - 1]}{pairInsertions[pair]}";
                }
                template = $"{newPolymer}{lastCharacter}";
                printCharacterCounts(template, i==0);
            }

            List<(string character, int count)> getCharacterCounts(string template)
                {
                string distinctCharacters = new string(template.Distinct().ToArray());
                var charCounts = new List<(string character, int count)>();

                for (int i = 0; i < distinctCharacters.Length; i++)
                {
                    var letter = new Regex(distinctCharacters[i].ToString());
                    charCounts.Add((distinctCharacters[i].ToString(), letter.Matches(template).Count));
                }

                return charCounts;
            }
            var charCounts = getCharacterCounts(template).OrderBy(c => c.count).ToList();

            return charCounts.Last().count - charCounts.First().count;

            void printCharacterCounts(string template, bool printHeader)
            {
                var counts = getCharacterCounts(template).OrderBy(c => c.character);
                string line = "";
                string line2 = "";

                foreach (var count in counts)
                {
                    line += $"{count.character},";
                    line2 += $"{count.count},";
                }

                //Console.WriteLine();
                if(printHeader)
                    Console.WriteLine(line.TrimEnd(','));

                Console.WriteLine(line2.TrimEnd(','));
                //Console.WriteLine();
            }*/
