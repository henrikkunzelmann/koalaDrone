using System;

namespace DroneLibrary.Protocol
{
    public struct PacketSetMovement : IPacket
    {
        public readonly short Roll;
        public readonly short Pitch;
        public readonly short Yaw;
        public readonly short Thrust;

        public PacketType Type => PacketType.Movement;

        public PacketSetMovement(short roll, short pitch, short yaw, short thrust)
        {
            Roll = roll;
            Pitch = pitch;
            Yaw = yaw;
            Thrust = thrust;
        }

        public void Write(PacketBuffer packet)
        {
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));

            packet.Write(Roll);
            packet.Write(Pitch);
            packet.Write(Yaw);
            packet.Write(Thrust);
        }
    }
}
