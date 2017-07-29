using DroneLibrary;
using DroneLibrary.Data;
using System;
using System.Windows.Forms;

namespace DroneControl
{
    public partial class GraphForm : Form
    {
        public Drone Drone { get; private set; }
        public FlightControl FlightControl { get; private set; }

        public GraphForm(Drone drone, FlightControl flightControl)
        {
            InitializeComponent();

            this.Drone = drone;
            this.Drone.OnSettingsChange += Drone_OnSettingsChange;
            this.Drone.OnDataChange += Drone_OnDataChange;

            this.FlightControl = flightControl;

            UpdateSettings(Drone.Settings);

            orientationGraphList.ValueMinimums = new double[] { -180, -180, 0 };
            orientationGraphList.ValueMaximums = new double[] { 180, 180, 360 };

            const double rotationRange = 500;
            rotationGraphList.ValueMinimums = new double[] { -rotationRange, -rotationRange, -rotationRange };
            rotationGraphList.ValueMaximums = new double[] { rotationRange, rotationRange, rotationRange };

            const double accelerationRange = 3;
            accelerationGraphList.ValueMinimums = new double[] { -accelerationRange, -accelerationRange, -accelerationRange };
            accelerationGraphList.ValueMaximums = new double[] { accelerationRange, accelerationRange, accelerationRange };
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (this.Drone != null)
            {
                this.Drone.OnSettingsChange -= Drone_OnSettingsChange;
                this.Drone.OnDataChange -= Drone_OnDataChange;
            }
            base.OnFormClosed(e);
        }

        private void Drone_OnSettingsChange(object sender, SettingsChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<SettingsChangedEventArgs>(Drone_OnSettingsChange), sender, e);
                return;
            }

            UpdateSettings(e.Settings);
        }

        private void UpdateSettings(DroneSettings settings)
        {
            servoGraph.BaseLine = settings.ServoIdle;
            servoGraph.ValueMin = 0;
            servoGraph.ValueMax = settings.ServoMax;
        }

        private void Drone_OnDataChange(object sender, DataChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<DataChangedEventArgs>(Drone_OnDataChange), sender, e);
                return;
            }

            UpdateGraph(servoGraph, e.Data.MotorValues);

            orientationGraphList.UpdateValue(e.Data.Sensor.Roll, e.Data.Sensor.Pitch, e.Data.Sensor.Yaw);
            rotationGraphList.UpdateValue(e.Data.Sensor.Gyro.X, e.Data.Sensor.Gyro.Y, e.Data.Sensor.Gyro.Z);
            accelerationGraphList.UpdateValue(e.Data.Sensor.Acceleration.X, e.Data.Sensor.Acceleration.Y, e.Data.Sensor.Acceleration.Z);
        }

        private void UpdateGraph(QuadGraphControl graph, QuadMotorValues values)
        {
            graph.UpdateValues(values.FrontLeft, values.FrontRight, values.BackLeft, values.BackRight);
        }
    }
}
