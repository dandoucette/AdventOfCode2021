using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public static class Day17
    {
        public static long A()
        {
            (int x1, int x2) = (155, 182);
            (int y1, int y2) = (-117, -67);

            //(int x1, int x2) = (20, 30);
            //(int y1, int y2) = (-10, -5);

            
            int xVelocity = (int)Math.Sqrt(x2 + x2);
            int yVelocity = (int)Math.Sqrt(Math.Abs(y2 + y2));

            int highestYPosition = 0;
            int initialYVelocity = yVelocity;
            int initialXVelocity = xVelocity;
            
            int maxYVelocity = yVelocity * yVelocity * 2;

            for (int j = initialXVelocity; j >= 0; j--)
            {
                bool probeFailed = false;
                for (int i = initialYVelocity; i < maxYVelocity; i++)
                {
                    xVelocity = j;
                    yVelocity = i;
                    int x = 0;
                    int y = 0;
                    int highestY = 0;

                    while (!(x >= x1 && x <= x2 && y >= y1 && y <= y2))
                    {
                        x += xVelocity;
                        y += yVelocity;

                        xVelocity = xVelocity - 1 < 0 ? 0 : xVelocity - 1;
                        yVelocity -= 1;

                        if (y > highestY)
                            highestY = y;

                        if (x > x2 || y < y1)
                        {
                            probeFailed = true;
                            Console.WriteLine($"Probe failed with initial Y velocity of {i}");
                            break;
                        }
                    }

                    if (probeFailed)
                        break;
                    else
                    {
                        if(highestY > highestYPosition)
                            highestYPosition = highestY;
                    }
                }
            }
            return highestYPosition;
        }

        public static long B()
        {
            return 0;
        }
    }
}
