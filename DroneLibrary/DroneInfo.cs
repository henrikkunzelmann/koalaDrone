using System.ComponentModel;

namespace DroneLibrary
{
    /// <summary>
    /// Stellt verschiedene Informationen über die Drohne bereit.
    /// </summary>
    public struct DroneInfo
    {
        /// <summary>
        /// Gibt den benutzerfreundlichen Namen der Drohne zurück.
        /// </summary>
        [Category("Drone")]
        public string Name { get; private set; }

        /// <summary>
        /// Gibt den Model Namen der Drohne zurück.
        /// </summary>
        [Category("Drone")]
        public string ModelName { get; private set; }

        /// <summary>
        /// Gibt die Seriennummer der Drohne zurück.
        /// </summary>
        [Category("Drone")]
        public string SerialCode { get; private set; }

        /// <summary>
        /// Gibt den Buildnamen der Drohne zurück.
        /// </summary>
        [Category("Build")]
        public string BuildName { get; private set; }

        /// <summary>
        /// Gibt die Build-Version der Firmware der Drohne zurück.
        /// </summary>
        [Category("Build")]
        public byte BuildVersion { get; private set; }

        /// <summary>
        /// Gibt die größte Revision der Drohne zurück.
        /// </summary>
        [Category("Debug")]
        public int HighestRevision { get; private set; }

        /// <summary>
        /// Gibt den Grund des letzten Resets der Drohne zurück.
        /// </summary>
        [Category("Debug")]
        public ResetReason ResetReason { get; private set; }

        /// <summary>
        /// Gibt die Exceptions des letzten Resets der Drohne zurück.
        /// </summary>
        [Category("Debug")]
        public ResetException ResetException { get; private set; }

        [Category("Debug")]
        public uint ResetEpc1 { get; private set; }

        [Category("Debug")]
        public uint ResetEpc2 { get; private set; }

        [Category("Debug")]
        public uint ResetEpc3 { get; private set; }

        [Category("Debug")]
        public uint ResetExcvaddr { get; private set; }

        [Category("Debug")]
        public uint ResetDepc { get; private set; }

        /// <summary>
        /// Gibt den letzten Stop Grund der Drohne zurück.
        /// </summary>
        [Category("Debug")]
        public StopReason StopReason { get; private set; }

        /// <summary>
        /// Gibt den Namen des Gyrosensors zurück.
        /// </summary>
        [Category("Drone")]
        public string GyroSensor { get; private set; }

        /// <summary>
        /// Gibt den Namen des Magnetometers zurück.
        /// </summary>
        [Category("Drone")]
        public string Magnetometer { get; private set; }

        /// <summary>
        /// Gibt den Namen des Barometers zurück.
        /// </summary>
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
            ResetEpc1 = buffer.ReadUInt();
            ResetEpc2 = buffer.ReadUInt();
            ResetEpc3 = buffer.ReadUInt();
            ResetExcvaddr = buffer.ReadUInt();
            ResetDepc = buffer.ReadUInt();

            if (ResetReason != ResetReason.Exception)
                ResetException = ResetException.None;

            StopReason = (StopReason)buffer.ReadByte();

            GyroSensor = buffer.ReadString();
            Magnetometer = buffer.ReadString();
            BaroSensor = buffer.ReadString();
        }
    }
}
