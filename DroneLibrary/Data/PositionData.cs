using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary.Data
{
    public struct PositionData
    {
        public float Latitude;
        public float Longitude;
        public float Altitude;
        public float Velocity;

        public PositionData(PacketBuffer buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            Latitude = buffer.ReadFloat();
            Longitude = buffer.ReadFloat();
            Altitude = buffer.ReadFloat();
            Velocity = buffer.ReadFloat();
        }
    }
}
