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

        private DroneSettings originalData;
        private DroneSettings data;

        private bool bindingError = false;
        private List<Binding> bindings = new List<Binding>();

        public SettingsForm(Drone drone)
        {
            InitializeComponent();

            this.drone = drone;
            this.info = drone.Info;
            this.originalData = drone.Settings;
            this.data = drone.Settings;

            try
            {
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
                Bind(safeRollTextBox, "data.SafeRoll");
                Bind(safePitchTextBox, "data.SafePitch");

                Bind(rollKpTextBox, "data.RollPid.Kp");
                Bind(rollKiTextBox, "data.RollPid.Ki");
                Bind(rollKdTextBox, "data.RollPid.Kd");

                Bind(pitchKpTextBox, "data.PitchPid.Kp");
                Bind(pitchKiTextBox, "data.PitchPid.Ki");
                Bind(pitchKdTextBox, "data.PitchPid.Kd");

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
            catch(Exception e)
            {
                bindingError = true;

                Log.Error(e);
                MessageBox.Show("Could not load the settings.", "Error while loading settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            drone.OnSettingsChange -= Drone_OnSettingsChange;
            drone.OnInfoChange -= Drone_OnInfoChange;
            base.OnHandleDestroyed(e);
        }

        private void Drone_OnSettingsChange(object sender, SettingsChangedEventArgs e)
        {
            originalData = e.Settings;
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

            try
            {
                foreach (Binding binding in bindings)
                    binding.NotifyValueChanged();
            }
            catch(Exception e)
            {
                bindingError = true;

                Log.Error(e);
                if (MessageBox.Show("Could not load the settings.", "Error while loading settings", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    UpdateValues();
            }
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
        private void Apply(bool force)
        {
            // Only send config when settings were changed by user
            if (force || IsAnyBindingChanged())
            {
                drone.SendConfig(data);
                originalData = data;
            }

            ClearChangedByUser();
        }

        /// <summary>
        /// Reverts all settings to the original settings saved on the drone.
        /// </summary>
        private void Revert()
        {
            data = originalData;
            ClearChangedByUser();
            UpdateValues();
        }

        private bool IsAnyBindingChanged()
        {
            foreach (Binding binding in bindings)
                if (binding.ChangedByUser)
                    return true;
            return false;
        }

        private void ClearChangedByUser()
        {
            applyButton.ForeColor = Color.Black;
            foreach (Binding binding in bindings)
                binding.ClearChangedByUser();
        }

        private void updateFirmwareButton_Click(object sender, EventArgs e)
        {
            if (bindingError)
                return;

            new UpdateOTAForm(drone).Show(this);
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            if (bindingError)
                return;

            drone.SendReset();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (bindingError)
                return;

            if (CheckSettings())
                Apply(false);
        }

        private void calibrateButton_Click(object sender, EventArgs e)
        {
            if (bindingError)
                return;

            MessageBox.Show("Power off the drone and turn it back on. The motors will then start the calibration.");
            data.CalibrateServos = true;
            if (CheckSettings())
                Apply(true);
        }

        private void revertButton_Click(object sender, EventArgs e)
        {
            if (bindingError)
                return;

            if (MessageBox.Show("This will revert all settings to the original settings saved on the drone.", "Revert settings?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Revert();
        }

        private bool CheckSettings()
        {
            bool v = true;
            v = v && CheckSetting("Min Value", data.ServoMin, originalData.ServoMin);
            v = v && CheckSetting("Idle Value", data.ServoIdle, originalData.ServoIdle);
            v = v && CheckSetting("Max Value", data.ServoMax, originalData.ServoMax);

            v = v && CheckSetting("PID Roll Kp", data.RollPid.Kp, originalData.RollPid.Kp);
            v = v && CheckSetting("PID Roll Ki", data.RollPid.Ki, originalData.RollPid.Ki);
            v = v && CheckSetting("PID Roll Kd", data.RollPid.Kd, originalData.RollPid.Kd);

            v = v && CheckSetting("PID Pitch Kp", data.PitchPid.Kp, originalData.PitchPid.Kp);
            v = v && CheckSetting("PID Pitch Ki", data.PitchPid.Ki, originalData.PitchPid.Ki);
            v = v && CheckSetting("PID Pitch Kd", data.PitchPid.Kd, originalData.PitchPid.Kd);

            v = v && CheckSetting("PID Yaw Kp", data.YawPid.Kp, originalData.YawPid.Kp);
            v = v && CheckSetting("PID Yaw Ki", data.YawPid.Ki, originalData.YawPid.Ki);
            v = v && CheckSetting("PID Yaw Kd", data.YawPid.Kd, originalData.YawPid.Kd);

            v = v && CheckSetting("PID Angle Kp", data.AngleStabilization.Kp, originalData.AngleStabilization.Kp);
            v = v && CheckSetting("PID Angle Ki", data.AngleStabilization.Ki, originalData.AngleStabilization.Ki);
            v = v && CheckSetting("PID Angle Kd", data.AngleStabilization.Kd, originalData.AngleStabilization.Kd);
            return v;
        }

        private bool CheckSetting(string name, float newValue, float oldValue)
        {
            float percentage = Math.Abs(newValue - oldValue) / oldValue;

            if (percentage > 0.2f)
            {
                return MessageBox.Show(
                    string.Format("Setting {0} (value: {1}) differs from the old value ({2}). Check the value and confirm the change.", name, newValue, oldValue),
                    "Confirm the value", MessageBoxButtons.OKCancel) == DialogResult.OK;
            }
            return true;
        }
    }
}
