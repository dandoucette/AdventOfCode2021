using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public static class Day8
    {
        public static int A()
        {
            var lines = File.ReadLines("InputData\\Day8.txt");

            int segments1478count = 0;

            foreach (var line in lines)
            {
                var inputOutput = line.Split(" | ");
                var output = inputOutput[1];
                var segments = output.Split(' ');
                

                foreach (var segment in segments)
                {
                    if (segment.Length == 2 || segment.Length == 3 || segment.Length == 4 || segment.Length == 7)
                        segments1478count++;
                }
            }
            return segments1478count;
        }

        public static int B()
        {
            var lines = File.ReadLines("InputData\\Day8.txt");
            
            //1110111 = 0
            //0010010 = 1
            //1011101 = 2
            //1011011 = 3
            //0111010 = 4
            //1101011 = 5
            //1101111 = 6
            //1010010 = 7
            //1111111 = 8
            //1111011 = 9
            var validNumbers = new List<string> {
                    "1110111",
                    "0010010",
                    "1011101",
                    "1011011",
                    "0111010",
                    "1101011",
                    "1101111",
                    "1010010",
                    "1111111",
                    "1111011"
                };

            var letters = new Dictionary<string, string>()
                {
                    { "a", "1234567" },
                    { "b", "1234567" },
                    { "c", "1234567" },
                    { "d", "1234567" },
                    { "e", "1234567" },
                    { "f", "1234567" },
                    { "g", "1234567" }
                };

            int total = 0;

            foreach (var line in lines)
            {
                var inputOutput = line.Split(" | ");
                var input = inputOutput[0];
                var inputSegments = input.Split(' ');
                var output = inputOutput[1];
                var outputSegments = output.Split(' ');

                
                var lettersUsed = "";
                var oneLetters = "";

                foreach (var segment in inputSegments.OrderBy(x => x.Length))
                {
                    if(segment.Length == 2)
                    {
                        letters[segment[0].ToString()] = "36";
                        letters[segment[1].ToString()] = "36";
                        lettersUsed = segment;
                        oneLetters = segment;
                    }
                    else if (segment.Length == 3)
                    {
                        var seven = segment.RemoveChars(lettersUsed);
                        foreach (var l in letters)
                        {
                            if (l.Value != "36")
                            {
                                letters[l.Key] = "2457";
                            }
                        }
                        letters[seven] = "1";
                        lettersUsed += seven;
                    }
                    else if (segment.Length == 4)
                    {
                        var four = segment.RemoveChars(lettersUsed);

                        foreach (var l in letters)
                        {
                            if(l.Value == "2457")
                            {
                                if(four.Contains(l.Key))
                                {
                                    letters[l.Key] = "24";
                                }
                                else
                                {
                                    letters[l.Key] = "57";
                                }
                            }
                        }
                    }
                    else if (segment.Length == 5 && segment.Contains(oneLetters[0]) && segment.Contains(oneLetters[1]))
                    {
                        // 3 is the only 5 segment number that uses both segments shared with one
                        var three = segment.RemoveChars(lettersUsed);
                        for (int i = 0; i < three.Length; i++)
                        {
                            if (letters[three[i].ToString()] == "24")
                            {
                                letters[three[i].ToString()] = "4";
                                letters[letters.Where(x => x.Value == "24").Single().Key] = "2";
                            }

                            if (letters[three[i].ToString()] == "57")
                            {
                                letters[three[i].ToString()] = "7";
                                letters[letters.Where(x => x.Value == "57").Single().Key] = "5";
                            }
                        }
                        
                    }
                    else if (segment.Length == 6 && (segment.Contains(oneLetters[0]) ^ segment.Contains(oneLetters[1])))
                    {
                        //0,6,9 use 6 sections, only 6 is missing a section shared with 1, this will resolve right side
                        if (segment.Contains(oneLetters[0]))
                        {
                            letters[oneLetters[0].ToString()] = "6";
                            letters[oneLetters[1].ToString()] = "3";
                        }
                        else
                        {
                            letters[oneLetters[0].ToString()] = "3";
                            letters[oneLetters[1].ToString()] = "6";
                        }
                    }
                }

                string number = "";
                foreach (var segment in outputSegments)
                {    
                    if (segment.Length == 2)
                    {
                        number = $"{number}1";
                    }
                    else if (segment.Length == 3)
                    {
                        number = $"{number}7";
                    }
                    else if (segment.Length == 4)
                    {
                        number = $"{number}4";
                    }
                    else if (segment.Length == 7)
                    {
                        number = $"{number}8";
                    }
                    else
                    {
                        string segmentValue = "";
                        foreach (var l in letters.OrderBy(x => x.Value))
                        {
                            if (segment.Contains(l.Key))
                            {
                                segmentValue = $"{segmentValue}1";
                            }
                            else
                            {
                                segmentValue = $"{segmentValue}0";
                            }
                        }
                        number = $"{number}{validNumbers.IndexOf(segmentValue)}";
                    }
                }

                total += int.Parse(number);
            }

            return total;
        }

        public static string RemoveChars(this string value, string charsToRemove)
        {
            string original = value;
            for (int i = 0; i < charsToRemove.Length; i++)
            {
                original = original.Replace(charsToRemove[i].ToString(), "");
            }
            return original;
        }

        /*
         * if (segment.Length != 7)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                var attempt = new string[7] { "0", "0", "0", "0", "0", "0", "0" };

                                for (int j = 0; j < segment.Length; j++)
                                {
                                    var positions = letters[segment[j].ToString()];
                                    if(positions.Length == 1)
                                    {
                                        attempt[int.Parse(positions) - 1] = "1";
                                    }
                                    else
                                    {
                                        var pair = string.Join("", letters.Where(x => x.Value == positions).Select(x => x.Key));
                                        var otherLetter = pair.RemoveChars(segment[j].ToString());
                                        if(segment.Contains(otherLetter))
                                        {

                                        }
                                        else
                                        {
                                            attempt[int.Parse(positions[0].ToString())] = "1";
                                        }
                                    }
                                }

                                var number = string.Join("", attempt);
                            }
                        }
         * */
    }
}
