using System;

namespace DroneLibrary
{
    public struct GyroData
    {
        public bool InCalibration { get; }

        public float Roll { get; }
        public float Pitch { get; }
        public float Yaw { get; }

        public float GyroX { get; }
        public float GyroY { get; }
        public float GyroZ { get; }

        public float AccelerationX { get; }
        public float AccelerationY { get; }
        public float AccelerationZ { get; }

        public float MagnetX { get; }
        public float MagnetY { get; }
        public float MagnetZ { get; }

        public float Temperature { get; }

        public GyroData(PacketBuffer buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            InCalibration = buffer.ReadBoolean();

            Roll = buffer.ReadFloat();
            Pitch = buffer.ReadFloat();
            Yaw = buffer.ReadFloat();

            GyroX = buffer.ReadFloat();
            GyroY = buffer.ReadFloat();
            GyroZ = buffer.ReadFloat();

            AccelerationX = buffer.ReadFloat();
            AccelerationY = buffer.ReadFloat();
            AccelerationZ = buffer.ReadFloat();

            MagnetX = buffer.ReadFloat();
            MagnetY = buffer.ReadFloat();
            MagnetZ = buffer.ReadFloat();

            Temperature = buffer.ReadFloat();
        }
    }
}
