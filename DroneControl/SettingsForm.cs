using DroneLibrary;
using DroneLibrary.Diagnostics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace DroneControl
{
    public partial class SettingsForm : Form
    {
        private Drone drone;
        private DroneInfo info;
        private DroneSettings data;

        private List<Binding> bindings = new List<Binding>();

        public SettingsForm(Drone drone)
        {
            InitializeComponent();

            this.drone = drone;
            this.info = drone.Info;
            this.data = drone.Settings;

            Bind(nameTextBox, "data.DroneName");
            Bind(saveConfigCheckBox, "data.SaveConfig");

            Bind(firmwareVersionTextBox, "info.BuildVersion");
            Bind(buildDateTextBox, "info.BuildName");

            Bind(modelTextBox, "info.ModelName");
            Bind(idTextBox, "info.SerialCode");
            Bind(gyroSensorTextBox, "info.GyroSensor");
            Bind(magnetometerTextBox, "info.Magnetometer");
            Bind(baroSensorTextBox, "info.BaroSensor");

            Bind(minValueTextBox, "data.ServoMin");
            Bind(idleValueTextBox, "data.ServoIdle");
            Bind(maxValueTextBox, "data.ServoMax");

            Bind(safeMotorValueTextBox, "data.SafeServoValue");
            Bind(safeTemperatureTextBox, "data.MaxTemperature");
            Bind(safePitchTextBox, "data.SafePitch");
            Bind(safeRollTextBox, "data.SafeRoll");

            Bind(pitchKpTextBox, "data.PitchPid.Kp");
            Bind(pitchKiTextBox, "data.PitchPid.Ki");
            Bind(pitchKdTextBox, "data.PitchPid.Kd");

            Bind(rollKpTextBox, "data.RollPid.Kp");
            Bind(rollKiTextBox, "data.RollPid.Ki");
            Bind(rollKdTextBox, "data.RollPid.Kd");

            Bind(yawKpTextBox, "data.YawPid.Kp");
            Bind(yawKiTextBox, "data.YawPid.Ki");
            Bind(yawKdTextBox, "data.YawPid.Kd");

            Bind(enableStabilizationCheckBox, "data.EnableStabilization");
            Bind(negativeMixingCheckBox, "data.NegativeMixing");

            Bind(maxThrustForFlyingTextBox, "data.MaxThrustForFlying");
            Bind(onlyArmWhenStillCheckBox, "data.OnlyArmWhenStill");

            Bind(angleKpTextBox, "data.AngleStabilization.Kp");
            Bind(angleKiTextBox, "data.AngleStabilization.Ki");
            Bind(angleKdTextBox, "data.AngleStabilization.Kd");

            drone.OnSettingsChange += Drone_OnSettingsChange;
            drone.OnInfoChange += Drone_OnInfoChange;
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            drone.OnSettingsChange -= Drone_OnSettingsChange;
            drone.OnInfoChange -= Drone_OnInfoChange;
            base.OnHandleDestroyed(e);
        }

        private void Drone_OnSettingsChange(object sender, SettingsChangedEventArgs e)
        {
            data = e.Settings;
            UpdateValues();
        }

        private void Drone_OnInfoChange(object sender, InfoChangedEventArgs e)
        {
            info = e.Info;
            UpdateValues();
        }

        private void UpdateValues()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateValues));
                return;
            }

            foreach (Binding binding in bindings)
                binding.NotifyValueChanged();
        }

        private void Bind(Control control, string member)
        {
            Binding binding = new Binding(this, control, member);
            binding.OnChangedByUser += Binding_OnChangedByUser;
            bindings.Add(binding);
        }

        private void Binding_OnChangedByUser(object sender, EventArgs e)
        {
            applyButton.ForeColor = Color.DarkGreen;
        }

        /// <summary>
        /// Applys the settings to the connected drone.
        /// </summary>
        private void Apply()
        {
            drone.SendConfig(data);
            
            applyButton.ForeColor = Color.Black;
            foreach (Binding binding in bindings)
                binding.ClearChangedByUser();

        }

        private void updateFirmwareButton_Click(object sender, EventArgs e)
        {
            new UpdateOTAForm(drone).Show(this);
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            drone.SendReset();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            Apply();
        }

        private void calibrateButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Power off the drone and turn it back on. The motors will then start the calibration.");
            data.CalibrateServos = true;
            Apply();
        }
    }
}
