using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Classes
{
    public class Packet
    {
        private string _version;
        public string VersionIn { set { _version = value; } }
        public int Version => Convert.ToInt32(_version, 2);

        private string _type;
        public string TypeIn { set { _type = value; } }
        public PacketType Type => Convert.ToInt32(_type, 2) == 4 ? PacketType.Literal : PacketType.Operator;

        public Packet Parent { get; set; }

        public void CopyFrom(Packet packet)
        {
            this._version = packet._version;
            this._type = packet._type;
            this.Parent = packet.Parent;
        }
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
