using System;

namespace DroneControl.Input
{
    public interface IDeviceFinder : IDisposable
    {
        IInputDevice[] FindDevices();
    }
}
