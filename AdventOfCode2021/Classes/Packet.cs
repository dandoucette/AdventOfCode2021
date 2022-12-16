using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Classes
{
    public class Packet
    {
        public int Version { get; set; }
        public int Type { get; set; }
        public PacketType TypeDisplay => Type == 4 ? PacketType.Literal : PacketType.Operator;

        public Packet Parent { get; set; }
    }

    public enum PacketType
    {
        Literal,
        Operator
    }

    public enum LengthType
    {
        ElevenBit,
        FiftenBit
    }
}
