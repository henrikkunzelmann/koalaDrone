using DroneLibrary.Diagnostics;
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

        private const int maxDataCount = 6;
        private List<int[]> dataSets = new List<int[]>();

        public bool IsConnected
        {
            get { return controller.IsConnected; }
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

        public bool HasError { get; private set; }

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

        public void Reconnect()
        {
            if (controller != null)
                controller.Reconnect();
        }

        public void Update(InputManager manager)
        {
            try
            {
                if (!IsConnected)
                    return;

                if (!controller.IsOK)
                {
                    if (!HasError)
                        Log.Error("RemoteInputDevice data is not ok");
                    HasError = true;
                    return;
                }

                int[] data = controller.Data;

                if (dataSets.Count >= maxDataCount)
                    dataSets.RemoveAt(0);
                dataSets.Add(data);

                const int aux1 = 4;
                const int aux2 = 5;

                if (CheckButtonPressed(aux1, ButtonState.High, true))
                    manager.ArmDrone();
                else if (CheckButtonPressed(aux1, ButtonState.Low, true))
                    manager.DisarmDrone();

                if (CheckButtonPressed(aux2, ButtonState.Low, true))
                    manager.SendClear();
                else if (CheckButtonPressed(aux2, ButtonState.High, true))
                    manager.StopDrone();

                float deadZone = 0.075f;
                if (!manager.DeadZone)
                    deadZone = 0;

                TargetData target = new TargetData();
                target.Thrust = MapValueThrust(data, 0);
                target.Roll = DeadZone.Compute(MapValue(data, 1), deadZone);
                target.Pitch = -DeadZone.Compute(MapValue(data, 2), deadZone);
                target.Yaw = DeadZone.Compute(MapValue(data, 3), deadZone);

                manager.SendTargetData(target);
                HasError = false;
            }
            catch(Exception e)
            {
                HasError = true;
                Log.Error(e);
            }
        }

        private float MapValue(int[] data, int index)
        {
            return (data[index] - 1500) / 500.0f;
        }

        private float MapValueThrust(int[] data, int index)
        {
            return (data[index] - 1000) / 1000.0f;
        }

        private bool IsButtonPressed(int[] data, int index, ButtonState state, bool triState)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (index < 0 || index >= data.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (state == ButtonState.Middle && !triState)
                throw new ArgumentException("Middle is used without triState flag", nameof(triState));

            const int middleSize = 200;

            switch (state)
            {
                case ButtonState.Low:
                    if (triState)
                        return data[index] < 1500 - middleSize;
                    else
                        return data[index] < 1500;
                case ButtonState.Middle:
                    return Math.Abs(1500 - data[index]) <= middleSize;
                case ButtonState.High:
                    if (triState)
                        return data[index] > 1500 + middleSize;
                    else
                        return data[index] >= 1500;
            }
            return false;
        }

        private bool CheckButtonPressed(int index, ButtonState state, bool triState)
        {
            if (dataSets.Count != maxDataCount)
                return false;

            int mid = dataSets.Count / 2;

            bool last = false;
            for (int i = 0; i < mid; i++)
                if (IsButtonPressed(dataSets[i], index, state, triState))
                    last = true;

            bool current = true;
            for (int i = mid; i < dataSets.Count; i++)
                if (!IsButtonPressed(dataSets[i], index, state, triState))
                    current = false;

            return current && !last;
        }

        private enum ButtonState
        {
            Low,
            Middle,
            High
        }
    }
}
