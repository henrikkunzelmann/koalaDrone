using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary.Data
{
    public struct OutputData
    {
        public readonly float PitchOutput;
        public readonly float RollOutput;
        public readonly float YawOutput;

        public OutputData(PacketBuffer buffer)
        {
            PitchOutput = buffer.ReadFloat();
            RollOutput = buffer.ReadFloat();
            YawOutput = buffer.ReadFloat();
        }
    }
}
