using System;

namespace DroneLibrary
{
    public class SettingsChangedEventArgs : EventArgs
    {
        public DroneSettings Settings { get; private set; }

        public SettingsChangedEventArgs(Drone drone)
        {
            this.Settings = drone.Settings;
        }
    }
}
