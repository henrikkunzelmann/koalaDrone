using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DroneLibrary;

namespace DroneControl
{
    public partial class PIDTuningForm : Form
    {
        private Drone drone;
        private DroneSettings settings;

        private float TuneAmount
        {
            get { return (float)tuneAmount.Value; }
        }

        public PIDTuningForm(Drone drone)
        {
            this.drone = drone;
            InitializeComponent();

            UpdateValues(drone.Settings);
            drone.OnSettingsChange += Drone_OnSettingsChange;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            drone.OnSettingsChange -= Drone_OnSettingsChange;
            base.OnFormClosing(e);
        }

        private void Drone_OnSettingsChange(object sender, SettingsChangedEventArgs e)
        {
            Invoke(new Action<DroneSettings>(UpdateValues), e.Settings);
        }

        private void UpdateValues(DroneSettings settings)
        {
            try
            {
                this.settings = settings;

                SuspendLayout();

                rollPValue.Text = Formatting.FormatDecimal(settings.RollPid.Kp, 3);
                rollIValue.Text = Formatting.FormatDecimal(settings.RollPid.Ki, 3);
                rollDValue.Text = Formatting.FormatDecimal(settings.RollPid.Kd, 3);

                pitchPValue.Text = Formatting.FormatDecimal(settings.PitchPid.Kp, 3);
                pitchIValue.Text = Formatting.FormatDecimal(settings.PitchPid.Ki, 3);
                pitchDValue.Text = Formatting.FormatDecimal(settings.PitchPid.Kd, 3);

                yawPValue.Text = Formatting.FormatDecimal(settings.YawPid.Kp, 3);
                yawIValue.Text = Formatting.FormatDecimal(settings.YawPid.Ki, 3);
                yawDValue.Text = Formatting.FormatDecimal(settings.YawPid.Kd, 3);

                ResumeLayout();
            }
            catch(Exception e)
            {
                ErrorHandler.HandleException(drone, e);
            }
        }

        private void UploadSettings()
        {
            drone.SendConfig(settings);
        }

        // Roll
        // =============

        private void rollPDecrement_Click(object sender, EventArgs e)
        {
            settings.RollPid.Kp = Math.Max(0, settings.RollPid.Kp - TuneAmount);
            UploadSettings();
        }

        private void rollPIncrement_Click(object sender, EventArgs e)
        {
            settings.RollPid.Kp = Math.Max(0, settings.RollPid.Kp + TuneAmount);
            UploadSettings();
        }

        private void rollIDecrement_Click(object sender, EventArgs e)
        {
            settings.RollPid.Ki = Math.Max(0, settings.RollPid.Ki - TuneAmount);
            UploadSettings();
        }

        private void rollIIncrement_Click(object sender, EventArgs e)
        {
            settings.RollPid.Ki = Math.Max(0, settings.RollPid.Ki + TuneAmount);
            UploadSettings();
        }

        private void rollDDecrement_Click(object sender, EventArgs e)
        {
            settings.RollPid.Kd = Math.Max(0, settings.RollPid.Kd - TuneAmount);
            UploadSettings();
        }

        private void rollDIncrement_Click(object sender, EventArgs e)
        {
            settings.RollPid.Kd = Math.Max(0, settings.RollPid.Kd + TuneAmount);
            UploadSettings();
        }

        private void pitchPDecrement_Click(object sender, EventArgs e)
        {
            settings.PitchPid.Kp = Math.Max(0, settings.PitchPid.Kp - TuneAmount);
            UploadSettings();
        }

        // Pitch
        // =============

        private void pitchPIncrement_Click(object sender, EventArgs e)
        {
            settings.PitchPid.Kp = Math.Max(0, settings.PitchPid.Kp + TuneAmount);
            UploadSettings();
        }

        private void pitchIDecrement_Click(object sender, EventArgs e)
        {
            settings.PitchPid.Ki = Math.Max(0, settings.PitchPid.Ki - TuneAmount);
            UploadSettings();
        }

        private void pitchIIncrement_Click(object sender, EventArgs e)
        {
            settings.PitchPid.Ki = Math.Max(0, settings.PitchPid.Ki + TuneAmount);
            UploadSettings();
        }

        private void pitchDDecrement_Click(object sender, EventArgs e)
        {
            settings.PitchPid.Kd = Math.Max(0, settings.PitchPid.Kd - TuneAmount);
            UploadSettings();
        }

        private void pitchDIncrement_Click(object sender, EventArgs e)
        {
            settings.PitchPid.Kd = Math.Max(0, settings.PitchPid.Kd + TuneAmount);
            UploadSettings();
        }

        // Yaw
        // =============

        private void yawPDecrement_Click(object sender, EventArgs e)
        {
            settings.YawPid.Kp = Math.Max(0, settings.YawPid.Kp - TuneAmount);
            UploadSettings();
        }

        private void yawPIncrement_Click(object sender, EventArgs e)
        {
            settings.YawPid.Kp = Math.Max(0, settings.YawPid.Kp + TuneAmount);
            UploadSettings();
        }

        private void yawIDecrement_Click(object sender, EventArgs e)
        {
            settings.YawPid.Ki = Math.Max(0, settings.YawPid.Ki - TuneAmount);
            UploadSettings();
        }

        private void yawIIncrement_Click(object sender, EventArgs e)
        {
            settings.YawPid.Ki = Math.Max(0, settings.YawPid.Ki + TuneAmount);
            UploadSettings();
        }

        private void yawDDecrement_Click(object sender, EventArgs e)
        {
            settings.YawPid.Kd = Math.Max(0, settings.YawPid.Kd - TuneAmount);
            UploadSettings();
        }

        private void yawDIncrement_Click(object sender, EventArgs e)
        {
            settings.YawPid.Kd = Math.Max(0, settings.YawPid.Kd + TuneAmount);
            UploadSettings();
        }
    }
}
