using System;

namespace DroneLibrary
{
    public struct BaroData
    {
        public float Pressure;
        public float Humidity;
        public float Temperature;
        public float Altitude;

        public BaroData(PacketBuffer buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            Pressure = buffer.ReadFloat();
            Humidity = buffer.ReadFloat();
            Temperature = buffer.ReadFloat();
            Altitude = buffer.ReadFloat();
        }
    }
}
