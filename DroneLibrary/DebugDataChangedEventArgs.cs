using System;

namespace DroneLibrary
{
    public class DebugDataChangedEventArgs : EventArgs
    {
        public DebugData DebugData { get; private set; }

        public DebugDataChangedEventArgs(Drone drone)
        {
            this.DebugData = drone.DebugData;
        }
    }
}
