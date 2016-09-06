using System;

namespace DroneLibrary.Protocol
{
    public struct PacketReset : IPacket{

        public PacketType Type => PacketType.Reset;

        public void Write(PacketBuffer packet) {
            if(packet == null)
                throw new ArgumentNullException(nameof(packet));
        }

    }
}
