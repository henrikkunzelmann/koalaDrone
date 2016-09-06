using System;

namespace DroneLibrary
{
    public class DataChangedEventArgs : EventArgs
    {
        public DroneData Data { get; private set; }

        public DataChangedEventArgs(Drone drone)
        {
            this.Data = drone.Data;
        }
    }
}
