using System;

namespace DroneLibrary.Data
{
    public class DataChangedEventArgs : EventArgs
    {
        public DroneData Data { get; private set; }

        public DataChangedEventArgs(Drone drone)
        {
            if (drone == null)
                throw new ArgumentNullException(nameof(drone));

            this.Data = drone.Data;
        }
    }
}
