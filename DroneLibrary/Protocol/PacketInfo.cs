using System;

namespace DroneLibrary.Protocol
{
    public struct PacketInfo : IPacket
    {
        public PacketType Type => PacketType.Info;

        public void Write(PacketBuffer packet)
        {
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));
        }
    }
}
