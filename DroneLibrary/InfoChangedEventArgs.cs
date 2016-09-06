using System;

namespace DroneLibrary
{
    public class InfoChangedEventArgs : EventArgs
    {
        public DroneInfo Info { get; private set; }

        public InfoChangedEventArgs(Drone drone)
        {
            this.Info = drone.Info;
        }
    }
}
