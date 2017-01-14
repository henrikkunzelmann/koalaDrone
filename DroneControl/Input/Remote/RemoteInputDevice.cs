using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneControl.Input.Remote
{
    public class RemoteInputDevice : IInputDevice
    {
        private FlightController controller;
        private int[] lastData;

        public bool IsConnected
        {
            get { return controller.IsConnected && controller.IsOK; }
        }

        public string Name
        {
            get { return string.Format("RemoteInputDevice [{0}]", controller.ComPort); }
        }

        public BatteryInfo Battery
        {
            get
            {
                return new BatteryInfo();
            }
        }

        public bool CanCalibrate
        {
            get { return false; }
        }

        public RemoteInputDevice(string comPort)
        {
            this.controller = new FlightController(comPort);
        }

        public void Calibrate()
        {
        }

        public void Dispose()
        {
            if (controller != null)
                controller.Dispose();
        }

        public override bool Equals(object other)
        {
            if (other is RemoteInputDevice)
                return Equals((RemoteInputDevice)other);
            return false;
        }

        public bool Equals(IInputDevice other)
        {
            return Equals((object)other);
        }

        public bool Equals(RemoteInputDevice other)
        {
            return object.ReferenceEquals(this, other);
        }

        public override int GetHashCode()
        {
            return controller.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

        public void Update(InputManager manager)
        {
            if (!IsConnected)
                return;

            int[] data = controller.Data;

            const int aux1 = 4;
            const int aux2 = 5;

            if (CheckButtonPressed(data, aux1, true))
                manager.ArmDrone();
            else if (CheckButtonPressed(data, aux1, false))
                manager.DisarmDrone();

            if (CheckButtonPressed(data, aux2, true))
                manager.SendClear();
            else if (CheckButtonPressed(data, aux2, false))
                manager.StopDrone();

            TargetData target = new TargetData();
            target.Thrust = MapValueThrust(data, 0);
            target.Roll = MapValue(data, 1);
            target.Pitch = -MapValue(data, 2);
            target.Yaw = MapValue(data, 3);

            manager.SendTargetData(target);

            if (lastData == null)
                lastData = new int[data.Length];
            data.CopyTo(lastData, 0);
        }

        private float MapValue(int[] data, int index)
        {
            return (data[index] - 1500) / 500.0f;
        }

        private float MapValueThrust(int[] data, int index)
        {
            return (data[index] - 1000) / 1000.0f;
        }

        private bool CheckButtonPressed(int[] data, int index, bool isHighPressed)
        {
            if (lastData == null)
                return false;

            bool current; 
            bool last;

            if (isHighPressed)
            {
                current = data[index] > 1500;
                last = lastData[index] > 1500;
            }
            else
            {
                current = data[index] < 1500;
                last = lastData[index] < 1500;
            }
            return current && !last;
        }
    }
}
