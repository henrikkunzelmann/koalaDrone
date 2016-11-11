using DroneLibrary;
using DroneLibrary.Protocol;
using System;
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
            if (this.Drone != null)
                this.Drone.OnDataChange -= Drone_OnDataChange;
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

            if (!float.IsNaN(data.Gyro.Pitch) && !float.IsNaN(data.Gyro.Roll))
                artificialHorizon.SetAttitudeIndicatorParameters(data.Gyro.Pitch, -data.Gyro.Roll);
            if (!float.IsNaN(data.Gyro.Yaw))
                headingIndicator.SetHeadingIndicatorParameters((int)data.Gyro.Yaw);

            calibrateGyroButton.Enabled = data.State != DroneState.Armed && data.State != DroneState.Flying;
            calibrationRunningText.Visible = data.Gyro.InCalibration;

            orientationLabel.Text = string.Format("Roll: {0} Pitch: {1} Yaw: {2}",
                Formatting.FormatDecimal(data.Gyro.Roll, 2),
                Formatting.FormatDecimal(data.Gyro.Pitch, 2),
                Formatting.FormatDecimal(data.Gyro.Yaw, 2));

            rotationLabel.Text = string.Format("Rotation x: {0} y: {1} z: {2}",
                Formatting.FormatDecimal(data.Gyro.GyroX, 2),
                Formatting.FormatDecimal(data.Gyro.GyroY, 2),
                Formatting.FormatDecimal(data.Gyro.GyroZ, 2));

            float ax = data.Gyro.AccelerationX;
            float ay = data.Gyro.AccelerationY;
            float az = data.Gyro.AccelerationZ;

            float len = (float)Math.Sqrt(ax * ax + ay * ay + az * az);
            accelerationLabel.Text = string.Format("Acceleration x: {0} y: {1} z: {2} len: {3} g",
                Formatting.FormatDecimal(ax, 2),
                Formatting.FormatDecimal(ay, 2),
                Formatting.FormatDecimal(az, 2),
                Formatting.FormatDecimal(len, 2));

            float mx = data.Gyro.MagnetX;
            float my = data.Gyro.MagnetY;
            float mz = data.Gyro.MagnetZ;
            float magLen = (float)Math.Sqrt(mx * mx + my * my + mz * mz);
            magnetLabel.Text = string.Format("Magnet x: {0} y: {1} z: {2}{3}Magnet strengh: {4} µT",
                Formatting.FormatDecimal(mx, 2),
                Formatting.FormatDecimal(my, 2),
                Formatting.FormatDecimal(mz, 2),
                Environment.NewLine,
                Formatting.FormatDecimal(magLen, 2));

            temperatureLabel.Text = string.Format("Temperature: {0}°C",
                Formatting.FormatDecimal(data.Gyro.Temperature, 2));

            batteryVoltageLabel.Text = string.Format("Battery voltage: {0} V",
                Formatting.FormatDecimal(data.BatteryVoltage, 2));

            pressureLabel.Text = string.Format("Pressure: {0} hPa",
                Formatting.FormatDecimal(data.Baro.Pressure, 2, 4));

            humidityLabel.Text = string.Format("Humidity: {0} %RH",
                Formatting.FormatDecimal(data.Baro.Humidity, 2, 3));

            temperatureBaroLabel.Text = string.Format("Temperature: {0}°C",
                Formatting.FormatDecimal(data.Baro.Temperature, 2));

            altitudeLabel.Text = string.Format("Altitude: {0} m",
                Formatting.FormatDecimal(data.Baro.Altitude, 2, 4));

            ResumeLayout();
            Invalidate(true);
        }
    }
}
