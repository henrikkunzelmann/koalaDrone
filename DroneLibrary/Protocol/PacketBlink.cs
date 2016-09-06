using System;

namespace DroneLibrary.Protocol
{
    public struct PacketBlink : IPacket
    {
        public PacketType Type => PacketType.Blink;

        public void Write(PacketBuffer packet)
        {
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));
        }
    }
}
