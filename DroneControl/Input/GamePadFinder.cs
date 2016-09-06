using SharpDX.DirectInput;
using System.Linq;

namespace DroneControl.Input
{
    public class GamePadFinder : IDeviceFinder
    {
        private DirectInput directInput;

        public GamePadFinder()
        {
            directInput = new DirectInput();
        }

        public IInputDevice[] FindDevices()
        {
            return directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices)
                .Select(d => new GamePad(directInput, d))
                .ToArray();
        }
    }
}
