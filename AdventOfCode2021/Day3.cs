using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public static class Day3
    {
        public static int A()
        {
            var lines = File.ReadLines("InputData\\Day3.txt");
            var count = lines.Count();
            var binary = new List<char[]>();
            int bitCount = 0;

            foreach (var line in lines)
            {
                var bits = line.Trim().ToCharArray();
                bitCount = bits.Length;
                binary.Add(bits);
            }

            string gamma = "";
            string epsilon = "";

            for (int i = 0; i < bitCount; i++)
            {
                var ones = binary.Where(x => x[i] == '1').Count();

                if (ones > (count/2))
                {
                    gamma = $"{gamma}1";
                    epsilon = $"{epsilon}0";
                } 
                else
                {
                    gamma = $"{gamma}0";
                    epsilon = $"{epsilon}1";
                }
            }

            return Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
        }

        public static int B()
        {
            var lines = File.ReadLines("InputData\\Day3.txt");
            var half = lines.Count() / 2;
            var binary = new List<char[]>();
            int bitCount = 0;

            foreach (var line in lines)
            {
                var bits = line.Trim().ToCharArray();
                bitCount = bits.Length;
                binary.Add(bits);
            }

            string oxygen = "";
            string CO2 = "";

            var o2Bits = binary;
            var co2Bits = binary;
            decimal o2Half = half;
            decimal co2Half = half;

            for (int i = 0; i < bitCount; i++)
            {
                var o2Ones = o2Bits.Where(x => x[i] == '1').Count();
                var co2Zeroes = co2Bits.Where(x => x[i] == '0').Count();
                
                decimal o2Count = o2Bits.Count;
                decimal co2Count = co2Bits.Count;

                o2Half = o2Count / 2.0m;
                co2Half = co2Count / 2.0m;

                if(o2Bits.Count > 1)
                    o2Bits = o2Bits.Where(x => x[i] == (o2Ones >= o2Half ? '1' : '0')).ToList();

                if(co2Bits.Count > 1)
                    co2Bits = co2Bits.Where(x => x[i] == (co2Zeroes <= co2Half ? '0' : '1')).ToList();
            }

            if(o2Bits.Count == 1 && co2Bits.Count == 1)
            {
                oxygen = new String(o2Bits.First());
                CO2 = new String(co2Bits.First());
            }

            int o2Decimal = Convert.ToInt32(oxygen, 2);
            int co2Decimal = Convert.ToInt32(CO2, 2);

            return o2Decimal * co2Decimal;
        }
    }
}
