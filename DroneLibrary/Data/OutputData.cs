using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary.Data
{
    public struct OutputData
    {
        public readonly float RollOutput;
        public readonly float PitchOutput;
        public readonly float YawOutput;

        public readonly float AngleRollOutput;
        public readonly float AnglePitchOutput;
        public readonly float AngleYawOutput;

        public OutputData(PacketBuffer buffer)
        {
            RollOutput = buffer.ReadFloat();
            PitchOutput = buffer.ReadFloat();
            YawOutput = buffer.ReadFloat();

            AngleRollOutput = buffer.ReadFloat();
            AnglePitchOutput = buffer.ReadFloat();
            AngleYawOutput = buffer.ReadFloat();
        }
    }
}
