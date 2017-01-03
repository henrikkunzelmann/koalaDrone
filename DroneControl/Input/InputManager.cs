using DroneLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DroneControl.Input
{
    public class InputManager : IDisposable
    {
        private Drone drone;
        private List<IInputDevice> devices = new List<IInputDevice>();

        private bool lastConnected;
        private BatteryInfo lastBattery;

        /// <summary>
        /// Gibt das aktuelle Eingabegerät zurück oder setzt dieses. 
        /// </summary>
        public IInputDevice CurrentDevice { get; set; }

        public float RollExp { get; set; } = 1;
        public float PitchExp { get; set; } = 1;
        public float YawExp { get; set; } = 1;

        /// <summary>
        /// Gibt aktuelle Ziel Daten zurück.
        /// </summary>
        public TargetData TargetData { get; set; }

        public TargetData RawTargetData { get; set; }

        public bool DeadZone { get; set; } = true;

        public event EventHandler OnDeviceInfoChanged;
        public event EventHandler OnTargetDataChanged;

        private IDeviceFinder[] deviceFinders;

        public InputManager(Drone drone)
        {
            if (drone == null)
                throw new ArgumentNullException(nameof(drone));
            this.drone = drone;

            CreateDeviceFinders();
        }

        private void CreateDeviceFinders()
        {
            try
            {
                // alle IDeviceFinder Types in diesem Code suchen
                var finderTypes = Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => t.GetInterfaces().Contains(typeof(IDeviceFinder)))
                    .Where(t => t.IsClass);

                // IDeviceFinder Instanzen erzeugen
                deviceFinders = finderTypes.Select(t => (IDeviceFinder)Activator.CreateInstance(t)).ToArray();
            }
            catch(Exception e)
            {
                Log.Error(e);
                deviceFinders = new IDeviceFinder[0];
            }
        }

        public void Dispose()
        {
            foreach (IInputDevice device in devices)
                device.Dispose();
            foreach (IDeviceFinder finder in deviceFinders)
                finder.Dispose();
        }

        /// <summary>
        /// Sucht nach alle IInputDevices die angeschlossen sind oder einmal angeschlossen waren.
        /// </summary>
        /// <returns>Array mit allen IInputDevices die gefunden wurden.</returns>
        public IInputDevice[] FindDevices(out bool changed)
        {
            int lastDeviceCount = devices.Count;

            // Geräte suchen
            foreach (IDeviceFinder finder in deviceFinders)
                foreach (IInputDevice device in finder.FindDevices())
                    if (!devices.Contains(device))
                        devices.Add(device);


            // hat sich verändert, wenn Geräte dazugekommen sind
            changed = devices.Count > lastDeviceCount;
            return devices.ToArray();
        }

        public void Update()
        {
            if (CurrentDevice != null)
            {
                CurrentDevice.Update(this);
                if (!CurrentDevice.IsConnected)
                {
                    SendTargetData(new TargetData(0, 0, 0, 0));

                    if (drone.Data.State.AreMotorsRunning())
                        StopDrone();
                }

                // schauen ob sich Informationen vom Gerät geändert haben
                bool dirty = CurrentDevice.IsConnected != lastConnected || !CurrentDevice.Battery.Equals(lastBattery);
                if (dirty)
                {
                    OnDeviceInfoChanged?.Invoke(this, EventArgs.Empty);

                    lastConnected = CurrentDevice.IsConnected;
                    lastBattery = CurrentDevice.Battery;
                }
            }
        }

        /// <summary>
        /// Sendet rohe IInputDevice Daten an die Drohne.
        /// </summary>
        /// <param name="data"></param>
        public void SendTargetData(TargetData data)
        {
            RawTargetData = data;

            data.Roll = (float)(Math.Pow(Math.Abs(data.Roll), RollExp) * Math.Sign(data.Roll));
            data.Pitch = (float)(Math.Pow(Math.Abs(data.Pitch), PitchExp) * Math.Sign(data.Pitch));
            data.Yaw = (float)(Math.Pow(Math.Abs(data.Yaw), YawExp) * Math.Sign(data.Yaw));

            data.Roll *= 500;
            data.Pitch *= 500;
            data.Yaw *= 200;
            data.Thrust *= 400;

            // Daten setzen und senden
            TargetData = data;
            OnTargetDataChanged?.Invoke(this, EventArgs.Empty);

            if (drone.Data.State.AreMotorsRunning())
                drone.SendMovementData((short)data.Roll, (short)data.Pitch, (short)data.Yaw, (short)data.Thrust);
        }

        public void ToogleArmStatus()
        {
            if (drone.Data.State == DroneState.Idle)
                ArmDrone();
            else if (drone.Data.State.AreMotorsRunning())
                DisarmDrone();
        }

        public void ArmDrone()
        {
            if (drone.Data.State == DroneState.Idle)
                drone.SendArm();
        }

        public void DisarmDrone()
        {
            if (drone.Data.State.AreMotorsRunning())
                drone.SendDisarm();
        }

        public void StopDrone()
        {
            drone.SendStop();
        }

        public void SendClear()
        {
            drone.SendClearStatus();
        }
    }
}
