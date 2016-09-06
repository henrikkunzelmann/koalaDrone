using System;

namespace DroneLibrary.Protocol
{
    public struct PacketSetRawValues : IPacket
    {
        public readonly QuadMotorValues Values;

        public PacketType Type => PacketType.SetRawValues;

        public PacketSetRawValues(ushort fl, ushort fr, ushort bl, ushort br)
        {
            this.Values = new QuadMotorValues(fl, fr, bl, br);
        }

        public PacketSetRawValues(QuadMotorValues values)
        {
            this.Values = values;
        }

        public void Write(PacketBuffer packet)
        {
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));

            packet.Write(Values.FrontLeft);
            packet.Write(Values.FrontRight);
            packet.Write(Values.BackLeft);
            packet.Write(Values.BackRight);
        }
    }
}