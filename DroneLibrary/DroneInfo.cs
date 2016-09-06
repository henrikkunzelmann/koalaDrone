﻿using System.ComponentModel;

namespace DroneLibrary
{
    /// <summary>
    /// Stellt verschiedene Informationen über das Drone bereit.
    /// </summary>
    public struct DroneInfo
    {
        /// <summary>
        /// Gibt den benutzerfreundlichen Namen der Drone zurück.
        /// </summary>
        [Category("Drone")]
        public string Name { get; private set; }

        /// <summary>
        /// Gibt den Model Namen der Drone zurück.
        /// </summary>
        [Category("Drone")]
        public string ModelName { get; private set; }

        /// <summary>
        /// Gibt die Seriennummer der Drone zurück.
        /// </summary>
        [Category("Drone")]
        public string SerialCode { get; private set; }

        /// <summary>
        /// Gibt den Buildnamen der Drone zurück.
        /// </summary>
        [Category("Build")]
        public string BuildName { get; private set; }

        /// <summary>
        /// Gibt die Build-Version der Firmware der Drone zurück.
        /// </summary>
        [Category("Build")]
        public byte BuildVersion { get; private set; }

        /// <summary>
        /// Gibt die größte Revision der Drone zurück.
        /// </summary>
        [Category("Debug")]
        public int HighestRevision { get; private set; }

        [Category("Debug")]
        public ResetReason ResetReason { get; private set; }

        [Category("Debug")]
        public ResetException ResetException { get; private set; }

        [Category("Debug")]
        public StopReason StopReason { get; private set; }

        [Category("Drone")]
        public string GyroSensor { get; private set; }

        [Category("Drone")]
        public string Magnetometer { get; private set; }

        [Category("Drone")]
        public string BaroSensor { get; private set; }

        public DroneInfo(PacketBuffer buffer)
        {
            Name = buffer.ReadString();
            ModelName = buffer.ReadString();
            SerialCode = buffer.ReadString();

            BuildName = buffer.ReadString().Trim().Replace(' ', '_');
            BuildVersion = buffer.ReadByte();

            HighestRevision = buffer.ReadInt();
            ResetReason = (ResetReason)buffer.ReadByte();
            ResetException = (ResetException)buffer.ReadByte();

            if (ResetReason != ResetReason.Exception)
                ResetException = ResetException.None;

            StopReason = (StopReason)buffer.ReadByte();

            GyroSensor = buffer.ReadString();
            Magnetometer = buffer.ReadString();
            BaroSensor = buffer.ReadString();
        }
    }
}
