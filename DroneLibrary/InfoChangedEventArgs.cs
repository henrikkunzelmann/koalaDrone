using System;

namespace DroneLibrary
{
    public class InfoChangedEventArgs : EventArgs
    {
        public DroneInfo Info { get; private set; }

        public InfoChangedEventArgs(DroneInfo info)
        {
            this.Info = info;
        }
    }
}
