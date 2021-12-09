using System;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //try
            //{
                long answer = 0;

                answer = Day8.B();

                Console.WriteLine();
                Console.WriteLine("============================");
                Console.WriteLine();
                Console.WriteLine("Answer is");
                Console.WriteLine(answer.ToString());
                Console.WriteLine();
                Console.WriteLine("============================");
                Console.WriteLine();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            //Console.WriteLine();
            //Console.WriteLine("Press a key");
            //Console.ReadKey();
        }
    }
}
