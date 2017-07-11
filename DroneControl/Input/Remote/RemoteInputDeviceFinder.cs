using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneControl.Input.Remote
{
    public class RemoteInputDeviceFinder : IDeviceFinder
    {
        private RemoteInputDevice device;

        public RemoteInputDeviceFinder()
        {
        }

        public void Dispose()
        {
            if (device != null)
                device.Dispose();
        }

        public IInputDevice[] FindDevices()
        {
            try
            {
                if (device == null)
                    device = new RemoteInputDevice("COM6");
                else if (!device.IsConnected)
                    device.Reconnect();

                return new IInputDevice[]
                {
                    device
                };
            }
            catch(Exception)
            {
                return new IInputDevice[0];
            }
        }
    }
}
