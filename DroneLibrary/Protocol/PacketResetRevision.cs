using System;

namespace DroneLibrary.Protocol
{
    public struct PacketResetRevision : IPacket
    {

        public PacketType Type => PacketType.ResetRevision;

        public void Write(PacketBuffer packet)
        {
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));
        }
    }
}