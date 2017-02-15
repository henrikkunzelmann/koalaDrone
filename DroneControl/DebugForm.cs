using DroneControl.Input;
using DroneLibrary;
using DroneLibrary.Data;
using System;
using System.Text;
using System.Windows.Forms;

namespace DroneControl
{
    public partial class DebugForm : Form
    {
        public Drone Drone { get; private set; }
        public InputManager InputManager { get; private set; }

        public DebugForm(Drone drone, InputManager inputManager)
        {
            if (drone == null)
                throw new ArgumentNullException(nameof(drone));

            InitializeComponent();

            this.Drone = drone;
            this.Drone.OnDebugDataChanged += Drone_OnDebugDataChange;
            this.InputManager = inputManager;

            UpdateDebugData();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Drone.OnDebugDataChanged -= Drone_OnDebugDataChange;
            base.OnFormClosing(e);
        }

        private void UpdateDebugData()
        {
            ProfilerData data = Drone.DebugProfilerData;

            StringBuilder profilerString = new StringBuilder();

            profilerString.AppendFormat("Free heap: {0}", Formatting.FormatDataSize(data.FreeHeapBytes));
            profilerString.AppendLine();

            if (data.Entries != null)
            {
                for (int i = 0; i < data.Entries.Length; i++)
                {
                    ProfilerData.Entry entry = data.Entries[i];
                    profilerString.AppendFormat("{0} {1}ms ({2}ms)",
                        entry.Name.PadLeft(25),
                        Formatting.FormatDecimal(entry.Time.TotalMilliseconds, 1, 8),
                        Formatting.FormatDecimal(entry.TimeMax.TotalMilliseconds, 1, 8));
                    profilerString.AppendLine();
                }

                profilerData.Text = profilerString.ToString();
            }
        }

        private void Drone_OnDebugDataChange(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<EventArgs>(Drone_OnDebugDataChange), sender, e);
                return;
            }

            UpdateDebugData();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            if (!Drone.Data.State.IsIdle())
                return;
            Drone.SendReset();
        }

        private void blinkButton_Click(object sender, EventArgs e)
        {
            Drone.SendBlink();
        }

        private void recorderButton_Click(object sender, EventArgs e)
        {
            new RecordForm(Drone, InputManager).Show();
        }
    }
}
