using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary.Data
{
    public struct Vector
    {
        public float X;
        public float Y;
        public float Z;

        public float LengthSquared
        {
            get { return X * X + Y * Y + Z * Z; }
        }

        public float Length
        {
            get { return (float)Math.Sqrt(LengthSquared); }
        }

        public Vector(float value)
        {
            this.X = value;
            this.Y = value;
            this.Z = value;
        }

        public Vector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector ReadFromBuffer(PacketBuffer buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            float x = buffer.ReadFloat();
            float y = buffer.ReadFloat();
            float z = buffer.ReadFloat();
            return new Vector(x, y, z);
        }
    }
}
