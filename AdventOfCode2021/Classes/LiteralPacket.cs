using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Classes
{
    public class LiteralPacket : Packet
    {
        public List<string> Literals { get; set; }
        public int Value
        {
            get
            {
                string valueBits = "";
                foreach (var literal in Literals)
                {
                    valueBits += literal.Substring(1);
                }
                return Convert.ToInt32(valueBits, 2);
            }
        }

        public LiteralPacket()
        {
            Literals = new List<string>();
        }
    }
}
