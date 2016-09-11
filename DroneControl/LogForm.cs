using DroneLibrary;
using System;
using System.Windows.Forms;

namespace DroneControl
{
    public partial class LogForm : Form
    {
        private Drone drone;
        private int lastDroneLogIndex;

        public LogForm(Drone drone)
        {
            InitializeComponent();

            // Log Klasse vorbereiten 
            Log.OnFlushBuffer += Log_OnFlushBuffer;
            Log.FlushBuffer();
            Log.AutomaticFlush = true;

            this.drone = drone;

            AppendDroneLog();
            drone.LogBuffer.OnAddedLines += LogBuffer_OnAddedLines;
        }

        private void AppendDroneLog()
        {
            string[] lines = drone.LogBuffer.GetLinesAfter(lastDroneLogIndex);
            lastDroneLogIndex += lines.Length;

            droneLogTextBox.AppendText(string.Join(Environment.NewLine, lines));
            droneLogTextBox.AppendText(Environment.NewLine);
        }

        private void Log_OnFlushBuffer(string obj)
        {
            if (logTextBox.InvokeRequired)
                logTextBox.BeginInvoke(new Action<string>(Log_OnFlushBuffer), obj);
            else
                logTextBox.AppendText(obj);
        }

        private void LogBuffer_OnAddedLines(object sender, EventArgs e)
        {
            if (droneLogTextBox.InvokeRequired)
                droneLogTextBox.Invoke(new EventHandler(LogBuffer_OnAddedLines), sender, e);
            else
                AppendDroneLog();
        }
    

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Log.OnFlushBuffer -= Log_OnFlushBuffer;
            Log.AutomaticFlush = false;

            drone.LogBuffer.OnAddedLines -= LogBuffer_OnAddedLines;

            base.OnFormClosing(e);
        }

        private void flushTimer_Tick(object sender, EventArgs e)
        {
            Log.FlushBuffer();
        }

        private void logCleanButton_Click(object sender, EventArgs e)
        {
            logTextBox.Clear();
        }

        private void clearDroneButton_Click(object sender, EventArgs e)
        {
            droneLogTextBox.Clear();
        }
    }
}
