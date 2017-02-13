using System;

namespace DroneLibrary
{
    public struct ProfilerData
    {
        /// <summary>
        /// The freep heap on the hardware in bytes.
        /// </summary>
        public readonly ulong FreeHeapBytes;
        /// <summary>
        /// All profiler entries that were profiled.
        /// </summary>
        public readonly Entry[] Entries;

        public ProfilerData(PacketBuffer buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            FreeHeapBytes = buffer.ReadULong();

            uint count = buffer.ReadUInt();
            Entries = new Entry[count];
            for (int i = 0; i < count; i++)
                Entries[i] = new Entry(buffer);
        }

        public struct Entry
        {
            public readonly string Name;
            public readonly uint TimeMicros;
            public readonly uint TimeMaxMicros;

            public TimeSpan Time
            {
                get { return new TimeSpan(TimeMicros * 10); }
            }

            public TimeSpan TimeMax
            {
                get { return new TimeSpan(TimeMaxMicros * 10); }
            }

            public Entry(PacketBuffer buffer)
            {
                if (buffer == null)
                    throw new ArgumentNullException(nameof(buffer));

                Name = buffer.ReadString();
                TimeMicros = buffer.ReadUInt();
                TimeMaxMicros = buffer.ReadUInt();
            }
        }
    }
}
