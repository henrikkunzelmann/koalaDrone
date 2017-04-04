using DroneControl.Input;
using DroneLibrary;
using DroneLibrary.Diagnostics;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DroneControl
{
    public partial class FlightControl : UserControl
    {
        private Drone drone;
        public InputManager InputManager { get; private set; }

        public FlightControl()
        {
            InitializeComponent();
        }

        public void Init(Drone drone)
        {
            if (drone == null)
                throw new ArgumentNullException(nameof(drone));

            this.drone = drone;
            this.InputManager = new InputManager(drone);
            this.InputManager.OnDeviceInfoChanged += InputManager_OnDeviceInfoChanged;
            this.InputManager.OnTargetDataChanged += InputManager_OnTargetDataChanged;

            this.drone.OnDebugDataChanged += Drone_OnDebugDataChange;

            SearchInputDevices(true);
            UpdateTargetData();
            UpdateDeviceInfo();
            UpdateInputConfig();
        }

        private void Drone_OnDebugDataChange(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<EventArgs>(Drone_OnDebugDataChange), sender, e);
                return;
            }

            if (drone.Data.State == DroneState.Flying)
            {
                StringBuilder pidData = new StringBuilder();
                pidData.AppendFormat("Roll:  {0} (stab: {1})",
                    Formatting.FormatDecimal(drone.DebugOutputData.RollOutput, 2, 3),
                    Formatting.FormatDecimal(drone.DebugOutputData.AngleRollOutput, 2, 3));
                pidData.AppendLine();
                pidData.AppendFormat("Pitch: {0} (stab: {1})", 
                    Formatting.FormatDecimal(drone.DebugOutputData.PitchOutput, 2, 3),
                    Formatting.FormatDecimal(drone.DebugOutputData.AnglePitchOutput, 2, 3));
                pidData.AppendLine();
                pidData.AppendFormat("Yaw:   {0}", Formatting.FormatDecimal(drone.DebugOutputData.YawOutput, 2, 3));

                pidDataLabel.Text = pidData.ToString();
                pidDataLabel.Visible = true;
            }
            else
                pidDataLabel.Visible = false;
        }

        private void InputManager_OnTargetDataChanged(object sender, EventArgs e)
        {
            UpdateTargetData();
        }

        private void InputManager_OnDeviceInfoChanged(object sender, EventArgs e)
        {
            UpdateDeviceInfo();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (InputManager != null)
                InputManager.Dispose();
            base.OnHandleDestroyed(e);
        }

        private void searchTimer_Tick(object sender, EventArgs e)
        {
            if (DesignMode)
                return;
            SearchInputDevices(false);
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            if (InputManager != null)
                InputManager.Update();
        }

        private void searchDeviceButton_Click(object sender, EventArgs e)
        {
            SearchInputDevices(false);
        }

        private void inputDeviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // "None" ist String, daher gibt "as" bei "None" null zurück
            InputManager.CurrentDevice = inputDeviceComboBox.SelectedItem as IInputDevice;
            UpdateDeviceInfo();
        }


        private void OnInputConfigChange(object sender, EventArgs e)
        {
            UpdateInputConfig();
        }

        private void SearchInputDevices(bool firstSearch)
        {
            bool changed;
            IInputDevice[] devices = InputManager.FindDevices(out changed);

            // schauen ob die Geräte sich nicht verändert haben und die Liste schon erstellt wurde
            if (!changed && inputDeviceComboBox.Items.Count > 0)
                return;

            // Liste erstellen
            inputDeviceComboBox.BeginUpdate();
            inputDeviceComboBox.Items.Clear();
            inputDeviceComboBox.Items.Add("\nNone"); // \n damit der Eintrag ganz am Anfang kommt
            inputDeviceComboBox.Items.AddRange(devices);
            inputDeviceComboBox.EndUpdate();

            // wenn nur ein Gerät gefunden und keins ausgewählt, dann neues auswählen
            if (firstSearch && devices.Length == 1 && InputManager.CurrentDevice == null)
                InputManager.CurrentDevice = devices[0];


            // Gerät auswählen
            if (InputManager.CurrentDevice == null)
                inputDeviceComboBox.SelectedIndex = 0;
            else
                inputDeviceComboBox.SelectedItem = InputManager.CurrentDevice;

            if (InputManager.CurrentDevice == null || !InputManager.CurrentDevice.IsConnected)
                searchTimer.Interval = 2500;
            else
                searchTimer.Interval = 5000;
        }


        private void UpdateDeviceInfo()
        {
            SuspendLayout();

            if (InputManager.CurrentDevice == null)
            {
                calibrateButton.Enabled = false;

                deviceConnectionLabel.Text = "No device selected";
                deviceConnectionLabel.ForeColor = SystemColors.ControlText;
                deviceBatteryLabel.Visible = false;
                ResumeLayout();
                return;
            }

            if (InputManager.CurrentDevice.IsConnected)
            {
                calibrateButton.Enabled = InputManager.CurrentDevice.CanCalibrate;

                deviceConnectionLabel.Text = "Device connected";
                deviceConnectionLabel.ForeColor = Color.Green;

                if (InputManager.CurrentDevice.Battery.HasBattery)
                {
                    deviceBatteryLabel.Visible = true;
                    deviceBatteryLabel.Text = string.Format("Battery: {0}", InputManager.CurrentDevice.Battery.Level);

                    switch (InputManager.CurrentDevice.Battery.Level)
                    {
                        case BatteryLevel.Empty:
                            deviceBatteryLabel.ForeColor = Color.DarkRed;
                            break;
                        case BatteryLevel.Low:
                            deviceBatteryLabel.ForeColor = Color.Red;
                            break;
                        case BatteryLevel.Medium:
                            deviceBatteryLabel.ForeColor = Color.Orange;
                            break;
                        case BatteryLevel.Full:
                            deviceBatteryLabel.ForeColor = Color.Green;
                            break;
                    }

                }
                else
                    deviceBatteryLabel.Visible = false;
            }
            else
            {
                calibrateButton.Enabled = false;

                deviceConnectionLabel.Text = "Device disconnected";
                deviceConnectionLabel.ForeColor = Color.Red;

                deviceBatteryLabel.Visible = false;
            }
            ResumeLayout();
        }

        private void UpdateTargetData()
        {
            SuspendLayout();

            bool deviceConnected = InputManager.CurrentDevice != null && InputManager.CurrentDevice.IsConnected;
            rollLabel.Visible = deviceConnected;
            pitchLabel.Visible = deviceConnected;
            yawLabel.Visible = deviceConnected;
            thrustLabel.Visible = deviceConnected;

            if (deviceConnected)
            {
                rollLabel.Text = string.Format("Roll: {0}",
                    Formatting.FormatDecimal(InputManager.TargetData.Roll, 0, 4));
                pitchLabel.Text = string.Format("Pitch: {0}",
                    Formatting.FormatDecimal(InputManager.TargetData.Pitch, 0, 4));
                yawLabel.Text = string.Format("Yaw: {0}",
                    Formatting.FormatDecimal(InputManager.TargetData.Yaw, 0, 4));
                thrustLabel.Text = string.Format("Thrust: {0}",
                    Formatting.FormatDecimal(InputManager.TargetData.Thrust, 0, 4));

                HighlightInputGraph(InputManager.RawTargetData);
            }

            ResumeLayout();
        }

        private void UpdateInputConfig()
        {
            InputManager.DeadZone = deadZoneCheckBox.Checked;
            InputManager.EnableStopButton = enableStop.Checked;
            InputManager.EnableClearButton = enableClear.Checked;

            InputManager.RollExp = (float)rollExpTextBox.Value;
            InputManager.PitchExp = (float)pitchExpTextBox.Value;
            InputManager.YawExp = (float)yawExpTextBox.Value;
            InputManager.ThrustExp = (float)thrustExpTextBox.Value;
            InputManager.ThrustBase = (float)thrustBaseTextBox.Value;

            CreateInputGraph(inputCurves.LeftTop.History, InputManager.RollExp);
            CreateInputGraph(inputCurves.RightTop.History, InputManager.PitchExp);
            CreateInputGraph(inputCurves.LeftBottom.History, InputManager.YawExp);

            inputCurves.RightBottom.ValueMin = 0;
            inputCurves.RightBottom.ValueMax = InputManager.ThrustMax;
            inputCurves.RightBottom.ShowHalfScaling = true;
            inputCurves.RightBottom.BaseLine = InputManager.ThrustMax / 2;
            CreateThrustGraph(inputCurves.RightBottom.History);

            inputCurves.Invalidate();
        }

        private void HighlightInputGraph(TargetData data)
        {
            HighlightInputGraph(inputCurves.LeftTop, data.Roll);
            HighlightInputGraph(inputCurves.RightTop, data.Pitch);
            HighlightInputGraph(inputCurves.LeftBottom, data.Yaw);
            HighlightInputGraph(inputCurves.RightBottom, data.Thrust * 2 - 1f);
            inputCurves.Invalidate();
        }

        private void HighlightInputGraph(Graph graph, float value)
        {
            value += 1;
            value /= 2;

            graph.HighlightX = (int)(value * (graph.History.ValueCount - 1));
        }

        private void CreateInputGraph(DataHistory data, float exp)
        {
            data.Clear();

            float scale = 1000.0f / (data.MaxLength - 1);
            for (float x = -500; x <= 500.0f; x += scale)
                data.UpdateValue(500 * InputManager.MapInputOneToOne(x / 500.0f, 0.5, exp));
        }

        private void CreateThrustGraph(DataHistory data)
        {
            data.Clear();
            float scale = 1000.0f / (data.MaxLength - 1);
            for (float x = 0; x <= 1000.0f; x += scale)
                data.UpdateValue(InputManager.ThrustMax * InputManager.MapThrust(x / 1000.0f));
        }

        

        private void calibrateButton_Click(object sender, EventArgs e)
        {
            if (InputManager.CurrentDevice != null)
                InputManager.CurrentDevice.Calibrate();
        }
    }
}
