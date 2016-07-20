﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary.Protocol
{
    public struct PacketCalibrateGyro : IPacket
    {
        public PacketType Type => PacketType.CalibrateGyro;

        public readonly bool CalibrateMagnet;

        public PacketCalibrateGyro(bool calibrateMagnet)
        {
            this.CalibrateMagnet = calibrateMagnet;
        }

        public void Write(PacketBuffer packet)
        {
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));

            packet.Write(CalibrateMagnet);
        }
    }
}
