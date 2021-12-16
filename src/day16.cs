using System;
using System.Linq;
using System.Collections.Generic;

namespace aoc2021
{
    class Day16
    {

        class BitDecoder
        {
            private Queue<char> InputLine = null;
            private string BitBuffer = "";
            public  long BitsRead = 0;
            public  BitDecoder(string file) 
            {
                InputLine = new Queue<char>(aocIO.GetStringList(file)[0].ToList());
                BitsRead = 0;
            }
            
            public string GetBitsCnt(int cnt)
            {
                while (BitBuffer.Length < cnt)
                {
                    var topchar =  InputLine.Dequeue(); string topstr = ""; topstr += topchar;
                    BitBuffer += Convert.ToString(Convert.ToInt32(topstr, 16), 2).PadLeft(4, '0');
                }

                var ret = BitBuffer.Substring(0, cnt);
                BitBuffer = BitBuffer.Remove(0, cnt);
                BitsRead+=cnt;
                //Console.Write(ret);
                return ret;
            }
            public uint GetBitUint(int BitCount)
            {
                return Convert.ToUInt32(GetBitsCnt(BitCount), 2);
            }
            public char GetBit()
            {
                return GetBitsCnt(1)[0];
            }
        }

        abstract class Packet
        {
            public uint Version = 0;
            abstract public void Read(BitDecoder decoder);
            public static Packet GetPacket(BitDecoder decoder)
            {
                var ver = decoder.GetBitUint(3);
                var typeid = decoder.GetBitUint(3);
                           
                Packet packet = null;
                switch(typeid)
                {
                    case SumPacket.TypeId:  packet = new SumPacket(); break;
                    case ProductPacket.TypeId:  packet = new ProductPacket(); break;
                    case MinPacket.TypeId:  packet = new MinPacket(); break;
                    case MaxPacket.TypeId:  packet = new MaxPacket(); break;
                    case Literal.TypeId: packet = new Literal(); break;
                    case LowerThanPacket.TypeId: packet = new LowerThanPacket(); break;
                    case GreatherThanPacket.TypeId: packet = new GreatherThanPacket(); break;
                    case EqualToPacket.TypeId: packet = new EqualToPacket(); break;
                    

                    default: throw new Exception();
                }

                packet.Version = ver;
                packet.Read(decoder);

                return packet;

            }

            abstract public long SumVersions();

            abstract public long GetValue();

        }

        class Literal : Packet
        {
            public const uint TypeId = 4;

            public long Value = 0;

            override public void Read(BitDecoder bits)
            {
                string strvalue = "";
                bool bLast = false;
                while(!bLast)
                {
                    bLast = bits.GetBit() == '0';
                    strvalue += bits.GetBitsCnt(4);
                }
                Value = Convert.ToInt64(strvalue, 2);

            }

            override public long SumVersions()
            {
                return Version;
            }

            override public long GetValue()
            {
                return Value;
            }

        }

        abstract class Operator : Packet
        {

            public Operator() {}

            protected List<Packet> subpackets = new List<Packet>();

            override public void Read(BitDecoder bits)
            {
                switch (bits.GetBit())
                {
                    case '0':
                    var len = bits.GetBitUint(15);
                    var start =  bits.BitsRead;
                    while( bits.BitsRead -start   < len)
                    {
                        subpackets.Add(GetPacket(bits));
                    }
                    break;

                    case '1':
                    var count = bits.GetBitUint(11);
                    for (int i =0; i < count; i++)
                    {
                        subpackets.Add(GetPacket(bits));
                    }
                    break;

                    default:
                        throw new Exception();
                }
            }  

            override public long SumVersions()
            {
                long sum = Version;
                foreach(var packet in subpackets)
                    sum += packet.SumVersions();
                return sum;
            }
        }

        class SumPacket : Operator
        {
            public const uint TypeId = 0;

            override public long GetValue()
            {
                long sum = 0;
                foreach(var packet in subpackets)
                    sum += packet.GetValue();
                return sum;
            }
        } 
        class ProductPacket : Operator
        {
            public const uint TypeId = 1;
            override public long GetValue()
            {
                long mult = 1;
                foreach(var packet in subpackets)
                    mult *= packet.GetValue();
                return mult;
            }
        } 

        class MinPacket : Operator
        {
            public const uint TypeId = 2;
            override public long GetValue()
            {
                long min = long.MaxValue;
                foreach(var packet in subpackets)
                    min  = Math.Min(min, packet.GetValue());
                return min;
            }
        } 
        class MaxPacket : Operator
        {
            public const uint TypeId = 3;
            override public long GetValue()
            {
                long max = long.MinValue;
                foreach(var packet in subpackets)
                    max  = Math.Max(max, packet.GetValue());
                return max;
            }
        } 

        class GreatherThanPacket : Operator
        {
            public const uint TypeId = 5;
            override public long GetValue()
            {
                return subpackets[0].GetValue() > subpackets[1].GetValue() ? 1 : 0;
            }
        } 

        class LowerThanPacket : Operator
        {
            public const uint TypeId = 6;
            override public long GetValue()
            {
                return subpackets[0].GetValue() < subpackets[1].GetValue() ? 1 : 0;
            }
        } 

        class EqualToPacket : Operator
        {
            public const uint TypeId = 7;
            override public long GetValue()
            {
                return subpackets[0].GetValue() == subpackets[1].GetValue() ? 1 : 0;
            }
        }         


       public static long Task1()
       {
            var program = new BitDecoder("day16.txt");
            var packet = Packet.GetPacket(program);
            return packet.SumVersions();
       }

       public static long Task2()
       {        
            var program = new BitDecoder("day16.txt");
            var packet = Packet.GetPacket(program);
            return packet.GetValue();
       }       
    }
}