using System;

namespace DroneLibrary
{
    public class PingChangedEventArgs : EventArgs
    {
        public bool IsConnected { get; private set; }
        public int Ping { get; private set; }

        public PingChangedEventArgs(Drone drone)
        {
            this.IsConnected = drone.IsConnected;
            this.Ping = drone.Ping;
        }
    }
}
