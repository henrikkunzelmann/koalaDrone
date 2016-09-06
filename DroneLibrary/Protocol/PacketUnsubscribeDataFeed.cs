using System;

namespace DroneLibrary.Protocol
{
    public struct PacketUnsubscribeDataFeed : IPacket {
        public PacketType Type => PacketType.UnsubscribeDataFeed;

        public void Write(PacketBuffer packet) {
            if(packet == null)
                throw new ArgumentNullException(nameof(packet));
        }
    }
}
