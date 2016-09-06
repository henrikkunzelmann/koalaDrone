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
        }

        private void Drone_OnDataChange(object sender, DataChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<DataChangedEventArgs>(Drone_OnDataChange), sender, e);
                return;
            }


            SuspendLayout();

            if (!float.IsNaN(e.Data.Gyro.Pitch) && !float.IsNaN(e.Data.Gyro.Roll))
                artificialHorizon.SetAttitudeIndicatorParameters(e.Data.Gyro.Pitch, -e.Data.Gyro.Roll);
            if (!float.IsNaN(e.Data.Gyro.Yaw))
                headingIndicator.SetHeadingIndicatorParameters((int)e.Data.Gyro.Yaw);

            calibrateGyroButton.Enabled = e.Data.State != DroneState.Armed && e.Data.State != DroneState.Flying;
            calibrationRunningText.Visible = e.Data.Gyro.InCalibration;

            orientationLabel.Text = string.Format("Roll: {0} Pitch: {1} Yaw: {2}",
                Formatting.FormatDecimal(e.Data.Gyro.Roll, 2),
                Formatting.FormatDecimal(e.Data.Gyro.Pitch, 2),
                Formatting.FormatDecimal(e.Data.Gyro.Yaw, 2));

            rotationLabel.Text = string.Format("Rotation x: {0} y: {1} z: {2}",
                Formatting.FormatDecimal(e.Data.Gyro.GyroX, 2),
                Formatting.FormatDecimal(e.Data.Gyro.GyroY, 2),
                Formatting.FormatDecimal(e.Data.Gyro.GyroZ, 2));

            float ax = e.Data.Gyro.AccelerationX;
            float ay = e.Data.Gyro.AccelerationY;
            float az = e.Data.Gyro.AccelerationZ;

            float len = (float)Math.Sqrt(ax * ax + ay * ay + az * az);
            accelerationLabel.Text = string.Format("Acceleration x: {0} y: {1} z: {2} len: {3} g",
                Formatting.FormatDecimal(ax, 2),
                Formatting.FormatDecimal(ay, 2),
                Formatting.FormatDecimal(az, 2),
                Formatting.FormatDecimal(len, 2));

            float mx = e.Data.Gyro.MagnetX;
            float my = e.Data.Gyro.MagnetY;
            float mz = e.Data.Gyro.MagnetZ;
            float magLen = (float)Math.Sqrt(mx * mx + my * my + mz * mz);
            magnetLabel.Text = string.Format("Magnet x: {0} y: {1} z: {2}{3}Magnet strengh: {4} µT",
                Formatting.FormatDecimal(mx, 2),
                Formatting.FormatDecimal(my, 2),
                Formatting.FormatDecimal(mz, 2),
                Environment.NewLine,
                Formatting.FormatDecimal(magLen, 2));

            temperatureLabel.Text = string.Format("Temperature: {0}°C",
                Formatting.FormatDecimal(e.Data.Gyro.Temperature, 2));

            batteryVoltageLabel.Text = string.Format("Battery voltage: {0} V",
                Formatting.FormatDecimal(e.Data.BatteryVoltage, 2));

            pressureLabel.Text = string.Format("Pressure: {0} hPa",
                Formatting.FormatDecimal(e.Data.Baro.Pressure, 2, 4));

            humidityLabel.Text = string.Format("Humidity: {0} %RH",
                Formatting.FormatDecimal(e.Data.Baro.Humidity, 2, 3));

            temperatureBaroLabel.Text = string.Format("Temperature: {0}°C",
                Formatting.FormatDecimal(e.Data.Baro.Temperature, 2));

            altitudeLabel.Text = string.Format("Altitude: {0} m",
                Formatting.FormatDecimal(e.Data.Baro.Altitude, 2, 4));

            ResumeLayout();
        }

        private void calibrateGyroButton_Click(object sender, EventArgs e)
        {
            Drone.SendPacket(new PacketCalibrateGyro(false), true);
        }

        private void calibrateMagnetButton_Click(object sender, EventArgs e)
        {
            Drone.SendPacket(new PacketCalibrateGyro(true), true);
        }
    }
}
