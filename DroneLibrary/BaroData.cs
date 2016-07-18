using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.Pressure = buffer.ReadFloat();
            this.Humidity = buffer.ReadFloat();
            this.Temperature = buffer.ReadFloat();
            this.Altitude = buffer.ReadFloat();
        }
    }
}
