using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public static class Day2
    {
        public static int A()
        {
            var lines = File.ReadLines("InputData\\Day2.txt");
            int depth = 0;
            int forward = 0;

            foreach (var line in lines)
            {
                var commands = line.Split(' ');
                int amount = int.Parse(commands[1]);

                switch(commands[0])
                {
                    case "forward":
                        forward += amount;
                        break;

                    case "down":
                        depth += amount;
                        break;

                    case "up":
                        depth -= amount;
                        break;
                }
            }

            return depth * forward;
        }

        public static int B()
        {
            var lines = File.ReadLines("InputData\\Day2.txt");
            int depth = 0;
            int forward = 0;
            int aim = 0;

            foreach (var line in lines)
            {
                var commands = line.Split(' ');
                int amount = int.Parse(commands[1]);

                switch (commands[0])
                {
                    case "forward":
                        forward += amount;
                        depth += (amount * aim);
                        break;

                    case "down":
                        aim += amount;
                        break;

                    case "up":
                        aim -= amount;
                        break;
                }
            }

            return depth * forward;
        }
    }
}
