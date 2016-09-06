using System;

namespace DroneLibrary
{
    public class DroneListChangedEventArgs : EventArgs
    {
        public DroneEntry[] Entries { get; private set; }

        public DroneListChangedEventArgs(DroneEntry[] entries)
        {
            this.Entries = entries;
        }
    }
}
