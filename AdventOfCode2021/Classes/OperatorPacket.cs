using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Classes
{
    public class OperatorPacket : Packet
    {
        private char _lengthTypeId;
        public char LengthTypeId { set { _lengthTypeId = value; } }

        public LengthType LengthType => _lengthTypeId == '0' ? LengthType.FiftenBit : LengthType.ElevenBit;
        public int SubPacketsSizeLength => LengthType == LengthType.FiftenBit ? 15 : 11;

        private string _packetSize;
        public string SubPacketsSize { set { _packetSize = value; } }

        public int NumberOfSubPackets => Type == PacketType.Operator
                                    && LengthType == LengthType.ElevenBit
                                    ? Convert.ToInt32(_packetSize, 2) : 0;
        public int LengthOfSubPackets => Type == PacketType.Operator
                                    && LengthType == LengthType.FiftenBit
                                    ? Convert.ToInt32(_packetSize, 2) : 0;
    }
}
