using System;

namespace DroneLibrary
{

    /// <summary>
    /// Eine Struktur zum Speichern der Motorwerte aller vier Motoren.
    /// </summary>
    public struct QuadMotorValues
    {
        /// <summary>
        /// Gibt den Wert des vorderen linken Motors zurück.
        /// </summary>
        public ushort FrontLeft { get; set; }

        /// <summary>
        /// Gibt den Wert des vorderen rechten Motors zurück.
        /// </summary>
        public ushort FrontRight { get; set; }

        /// <summary>
        /// Gibt den Wert des hinteren linken Motors zurück.
        /// </summary>
        public ushort BackLeft { get; set; }

        /// <summary>
        /// Gibt den Wert des hintern rechten Motors zurück.
        /// </summary>
        public ushort BackRight { get; set; }

        public QuadMotorValues(ushort all)
        {
            BackLeft = all;
            BackRight = all;
            FrontLeft = all;
            FrontRight = all;
        }

        public QuadMotorValues(PacketBuffer buffer)
        {
            FrontLeft = buffer.ReadUShort();
            FrontRight = buffer.ReadUShort();
            BackLeft = buffer.ReadUShort();
            BackRight = buffer.ReadUShort();
        }

        public QuadMotorValues(ushort fl, ushort fr, ushort bl, ushort br)
        {
            FrontLeft = fl;
            FrontRight = fr;
            BackLeft = bl;
            BackRight = br;
        }
    }
}
