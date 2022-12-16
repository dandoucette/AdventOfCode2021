using AdventOfCode2021.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public static class Day16
    {
        public static int A()
        {
            
            //Console.WriteLine(Convert.ToInt32("00010100", 2));
            //string input = "E0529D18025800ABCA6996534CB22E4C00FB48E233BAEC947A8AA010CE1249DB51A02CC7DB67EF33D4002AE6ACDC40101CF0449AE4D9E4C071802D400F84BD21CAF3C8F2C35295EF3E0A600848F77893360066C200F476841040401C88908A19B001FD35CCF0B40012992AC81E3B980553659366736653A931018027C87332011E2771FFC3CEEC0630A80126007B0152E2005280186004101060C03C0200DA66006B8018200538012C01F3300660401433801A6007380132DD993100A4DC01AB0803B1FE2343500042E24C338B33F5852C3E002749803B0422EC782004221A41A8CE600EC2F8F11FD0037196CF19A67AA926892D2C643675A0C013C00CC0401F82F1BA168803510E3942E969C389C40193CFD27C32E005F271CE4B95906C151003A7BD229300362D1802727056C00556769101921F200AC74015960E97EC3F2D03C2430046C0119A3E9A3F95FD3AFE40132CEC52F4017995D9993A90060729EFCA52D3168021223F2236600ECC874E10CC1F9802F3A71C00964EC46E6580402291FE59E0FCF2B4EC31C9C7A6860094B2C4D2E880592F1AD7782992D204A82C954EA5A52E8030064D02A6C1E4EA852FE83D49CB4AE4020CD80272D3B4AA552D3B4AA5B356F77BF1630056C0119FF16C5192901CEDFB77A200E9E65EAC01693C0BCA76FEBE73487CC64DEC804659274A00CDC401F8B51CE3F8803B05217C2E40041A72E2516A663F119AC72250A00F44A98893C453005E57415A00BCD5F1DD66F3448D2600AC66F005246500C9194039C01986B317CDB10890C94BF68E6DF950C0802B09496E8A3600BCB15CA44425279539B089EB7774DDA33642012DA6B1E15B005C0010C8C917A2B880391160944D30074401D845172180803D1AA3045F00042630C5B866200CC2A9A5091C43BBD964D7F5D8914B46F040";
            string input = "D2FE28";
            var totalBits = "";

            for (int i = 0; i < input.Length; i++)
            {
                var longValue = Convert.ToInt64(input[i].ToString(), 16);
                var bits = Convert.ToString(longValue, 2).PadLeft(4, '0');
                totalBits += bits;
            }

            string remainingBits = totalBits;
            var packets = new List<Packet>();
            while(remainingBits.Length > 11)
            {
                var processed = ProcessPacket(remainingBits, null);
                remainingBits = processed.remainingBits;
                packets.AddRange(processed.packets);
            }
            //var packet = processPacket(totalBits, 0, null);

            return packets.Sum(p => p.Version); //packet.Version;
        }

        public static int B()
        {
            return 0;
        }

        private static (string remainingBits, List<Packet> packets) ProcessPacket(string bits, Packet parentPacket)
        {
            int version = Convert.ToInt32(bits.Substring(0, 3), 2);
            int packetType = Convert.ToInt32(bits.Substring(3, 3), 2);


            if(packetType == 4)
            {
                string bitsToProcess = bits;
                var packets = new List<Packet>();
                packets.Add(parentPacket);
                while (bitsToProcess.Length >= 11)
                {
                    var packet = new LiteralPacket
                    {
                        Version = version,
                        Type = packetType,
                        Parent = parentPacket
                    };

                    bool isLastGroup = false;
                    int i = 6;
                    while (!isLastGroup)
                    {
                        packet.Literals.Add(bitsToProcess.Substring(i, 5));
                        if (packet.Literals.Last()[0] == '0')
                            isLastGroup = true;

                        i += 5;
                    }

                    packets.Add(packet);
                    bitsToProcess = bitsToProcess.Substring(i);
                }
                return (bitsToProcess, packets);
            }
            else
            {
                var packet = new OperatorPacket
                {
                    Version = version,
                    Type = packetType,
                    Parent = parentPacket,
                    LengthTypeId = bits[6]
                };

                var packets = new List<Packet>();
                if (parentPacket != null)
                    packets.Add(parentPacket);

                if (packet.LengthType == LengthType.FiftenBit)
                {
                    packet.SubPacketsSize = bits.Substring(7, 15);
                    var processed = ProcessPacket(bits.Substring(7 + 15, packet.LengthOfSubPackets), packet);
                    packets.AddRange(processed.packets);
                    return (processed.remainingBits, packets);
                }
                else
                {
                    // this is the tough one, we don't know the length so have to parse all subpackets
                    // to know what is left to parse, it can be multiple packets
                    packet.SubPacketsSize = bits.Substring(7, 11);
                    var subPackets = new List<Packet>();
                    string bitsToProcess = bits.Substring(7 + 11);
                    while(subPackets.Count < packet.NumberOfSubPackets)
                    {
                        (string remainingBits, List<Packet> packets) processed = ProcessPacket(bitsToProcess, packet);
                        subPackets.AddRange(processed.packets);
                        bitsToProcess = processed.remainingBits;
                    }
                    packets.AddRange(subPackets);
                    return (bitsToProcess, packets);
                }
            }
        }
    }
}

/*                                                                    
 * process generic packet <--------------------------------------------------
 * read 3 bits -> version                                             |     |
 * read 3 bits -> type                                                |     |
 * literal type - process literal packet                              |     |
 * operator type - process operatpr                                   |     |
 *                                                                    |     |
 * process literal                                                    |     |
 *      loop through bits 5 at a time                                 |     |
 *      until first bit of 5 is zero                                  |     |
 *      return literal packet + remaining bits or current index       |     |
 *                                                                    |     |
 * process operator                                                   |     |
 *      if length of subpackets                                       |     |
 *          process packet, pass in exact amount of bits              |     |
 *          each packet will be a child packet                        |     |
 *          process generic packet for each child ---------------------     |
 *      if number of subpackets                                             |
 *          process x packets passing in all remaining bits                 |
 *          each packet will be a child packet                              |
 *          process generic packet for each child----------------------------
 * 
 * continue until all bits length - index < 11 bits, the least amount that could be a packet
 * 
 * Packet processPacket(string bits, int index, Packet parent)
            {
                int i = index;
                var packet = new Packet
                {
                    VersionIn = bits.Substring(i, 3)
                };
                if (parent != null)
                {
                    packet.Parent = parent;
                }
                Console.WriteLine($"version: {packet.Version}");
                i += 3;

                packet.TypeIn = bits.Substring(i, 3);
                Console.WriteLine($"Packet Type ID: {packet.Type}");
                i += 3;

                if (packet.Type == PacketType.Literal)
                {
                    var literalPacket = new LiteralPacket();
                    literalPacket.CopyFrom(packet);

                    bool isLastGroup = false;
                    while (!isLastGroup)
                    {
                        literalPacket.Literals.Add(bits.Substring(i, 5));
                        if (literalPacket.Literals.Last()[0] == '0')
                            isLastGroup = true;

                        i += 5;
                    }
                    Console.WriteLine($"Literal value: {literalPacket.Value}");
                    Console.WriteLine("packet complete");
                    Console.WriteLine();
                    Console.WriteLine("-----------------------");
                    Console.WriteLine();

                    return literalPacket;
                }
                else
                {
                    var operatorPacket = new OperatorPacket();
                    operatorPacket.CopyFrom(packet);

                    operatorPacket.LengthTypeId = bits[i];
                    i++;

                    operatorPacket.SubPacketsSize = bits.Substring(i, operatorPacket.SubPacketsSizeLength);
                    i += operatorPacket.SubPacketsSizeLength;


                    if (operatorPacket.LengthType == LengthType.FiftenBit)       // Number of bits in sub-packets
                    {
                        Console.WriteLine($"subpackets length: {operatorPacket.LengthOfSubPackets}");

                    }
                    else                                    // Number of packets in sub-packets
                    {
                        // loop through bits until x bits are found
                    }
                }
            }*/
