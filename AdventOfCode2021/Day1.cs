using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public static class Day1
    {
        public static int A()
        {
            var lines = File.ReadLines("InputData\\Day1.txt");
            int previousDepth = 1000000;
            int depthIncreaseCount = 0;

            foreach (var line in lines)
            {
                int depth = int.Parse(line);
                if (depth > previousDepth)
                    depthIncreaseCount++;

                previousDepth = depth;
            }

            return depthIncreaseCount;
        }

        public static int B()
        {
            var lines = File.ReadLines("InputData\\Day1.txt").ToList();
            int previousDepth = 1000000;
            int depthIncreaseCount = 0;

            for(int i = 2; i < lines.Count; i++)
            {
                int depth = int.Parse(lines[i]) + int.Parse(lines[i - 1]) + int.Parse(lines[i - 2]);
                if (depth > previousDepth)
                    depthIncreaseCount++;

                previousDepth = depth;
            }

            return depthIncreaseCount;
        }
    }
}
