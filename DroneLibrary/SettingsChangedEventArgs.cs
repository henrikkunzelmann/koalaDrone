using System;

namespace DroneLibrary
{
    public class SettingsChangedEventArgs : EventArgs
    {
        public DroneSettings Settings { get; private set; }

        public SettingsChangedEventArgs(Drone drone)
        {
            if (drone == null)
                throw new ArgumentNullException(nameof(drone));

            this.Settings = drone.Settings;
        }
    }
}
