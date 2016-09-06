namespace DroneControl.Input
{
    public interface IDeviceFinder
    {
        IInputDevice[] FindDevices();
    }
}
