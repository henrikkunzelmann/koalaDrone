using DroneLibrary;
using DroneLibrary.Data;
using DroneLibrary.Protocol;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DroneControl
{
    public partial class SensorControl : UserControl
    {
        public Drone Drone { get; private set; }

        public SensorControl()
        {
            InitializeComponent();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (Drone != null)
                Drone.OnDataChange -= Drone_OnDataChange;

            base.OnHandleDestroyed(e);
        }

        public void Init(Drone drone)
        {
            if (drone == null)
                throw new ArgumentNullException(nameof(drone));

            this.Drone = drone;
            this.Drone.OnDataChange += Drone_OnDataChange;
            UpdateData(drone.Data);
        }

        private void Drone_OnDataChange(object sender, DataChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<DataChangedEventArgs>(Drone_OnDataChange), sender, e);
                return;
            }

            UpdateData(e.Data);
        }

        private void calibrateGyroButton_Click(object sender, EventArgs e)
        {
            Drone.SendPacket(new PacketCalibrateGyro(false), true);
        }

        private void calibrateMagnetButton_Click(object sender, EventArgs e)
        {
            Drone.SendPacket(new PacketCalibrateGyro(true), true);
        }

        private void UpdateData(DroneData data)
        {
            SuspendLayout();

            if (!float.IsNaN(data.Sensor.Pitch) && !float.IsNaN(data.Sensor.Roll))
                artificialHorizon.SetAttitudeIndicatorParameters(data.Sensor.Pitch, -data.Sensor.Roll);
            if (!float.IsNaN(data.Sensor.Yaw))
                headingIndicator.SetHeadingIndicatorParameters((int)data.Sensor.Yaw);

            calibrateGyroButton.Enabled = !data.State.AreMotorsRunning();
            calibrationRunningText.Visible = data.Sensor.InCalibration;

            orientationLabel.Text = string.Format("Roll: {0} Pitch: {1} Yaw: {2}",
                Formatting.FormatDecimal(data.Sensor.Roll, 2),
                Formatting.FormatDecimal(data.Sensor.Pitch, 2),
                Formatting.FormatDecimal(data.Sensor.Yaw, 2));

            rotationLabel.Text = string.Format("Rotation x: {0} y: {1} z: {2}",
                Formatting.FormatDecimal(data.Sensor.Gyro.X, 2),
                Formatting.FormatDecimal(data.Sensor.Gyro.Y, 2),
                Formatting.FormatDecimal(data.Sensor.Gyro.Z, 2));

            accelerationLabel.Text = string.Format("Acceleration x: {0} y: {1} z: {2} len: {3} g",
                Formatting.FormatDecimal(data.Sensor.Acceleration.X, 2),
                Formatting.FormatDecimal(data.Sensor.Acceleration.Y, 2),
                Formatting.FormatDecimal(data.Sensor.Acceleration.Z, 2),
                Formatting.FormatDecimal(data.Sensor.Acceleration.Length, 2));

            magnetLabel.Text = string.Format("Magnet x: {0} y: {1} z: {2}{3}Magnet strengh: {4} µT",
                Formatting.FormatDecimal(data.Sensor.Magnet.X, 2),
                Formatting.FormatDecimal(data.Sensor.Magnet.Y, 2),
                Formatting.FormatDecimal(data.Sensor.Magnet.Z, 2),
                Environment.NewLine,
                Formatting.FormatDecimal(data.Sensor.Magnet.Length, 2));

            batteryVoltageLabel.Text = string.Format("Battery voltage: {0} V",
                Formatting.FormatDecimal(data.BatteryVoltage, 2));

            pressureLabel.Text = string.Format("Pressure: {0} hPa",
                Formatting.FormatDecimal(data.Sensor.Baro.Pressure, 2, 4));

            humidityLabel.Text = string.Format("Humidity: {0} %RH",
                Formatting.FormatDecimal(data.Sensor.Baro.Humidity, 2, 3));

            altitudeLabel.Text = string.Format("Altitude: {0} m",
                Formatting.FormatDecimal(data.Sensor.Baro.Altitude, 2, 4));

            if (data.Sensor.Temperatures == null || data.Sensor.Temperatures.Length == 0)
                temperatureLabel.Text = "Temperatures °C: n/a";
            else
            {
                temperatureLabel.Text = string.Format("Temperatures °C: {0}",
                    string.Join("  ", data.Sensor.Temperatures.Select(t => Formatting.FormatDecimal(t, 2))));
            }

            ResumeLayout();
            Invalidate(true);
        }
    }
}
