using System;

namespace DroneLibrary.Data
{
    public struct SensorData
    {
        public bool InCalibration { get; }

        public float Roll { get; }
        public float Pitch { get; }
        public float Yaw { get; }

        public Vector Gyro { get; }
        public Vector Acceleration { get; }
        public Vector Magnet { get; }
        public BaroData Baro { get; }
        public PositionData Position { get; }

        public float[] Temperatures { get; }

        public SensorData(PacketBuffer buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            InCalibration = buffer.ReadBoolean();

            Roll = buffer.ReadFloat();
            Pitch = buffer.ReadFloat();
            Yaw = buffer.ReadFloat();

            Gyro = Vector.ReadFromBuffer(buffer);
            Acceleration = Vector.ReadFromBuffer(buffer);
            Magnet = Vector.ReadFromBuffer(buffer);

            Baro = new BaroData(buffer);
            Position = new PositionData(buffer);

            int tempCount = buffer.ReadByte();
            Temperatures = new float[tempCount];

            for (int i = 0; i < Temperatures.Length; i++)
                Temperatures[i] = buffer.ReadFloat();
        }
    }
}
