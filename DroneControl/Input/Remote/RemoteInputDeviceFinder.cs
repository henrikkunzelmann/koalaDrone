using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneControl.Input.Remote
{
    public class RemoteInputDeviceFinder : IDeviceFinder
    {
        public RemoteInputDeviceFinder()
        {
        }

        public void Dispose()
        {
        }

        public IInputDevice[] FindDevices()
        {
            try
            {
                return new IInputDevice[]
                {
                    new RemoteInputDevice("COM5")
                };
            }
            catch(Exception)
            {
                return new IInputDevice[0];
            }
        }
    }
}
