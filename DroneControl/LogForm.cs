using DroneLibrary;
using System;
using System.Windows.Forms;

namespace DroneControl
{
    public partial class LogForm : Form
    {
        private Drone drone;

        private LogView log;
        private LogView droneLog;

        public LogForm(Drone drone)
        {
            this.drone = drone;

            InitializeComponent();

            log = Log.Storage.CreateView();
            droneLog = drone.DroneLog.CreateView();

            AppendLog();

            Log.Storage.OnAddedLines += LogBuffer_OnAddedLines;
            drone.DroneLog.OnAddedLines += LogBuffer_OnAddedLines;
        }

        private void AppendLog()
        {
            AppendLog(logTextBox, log);
            AppendLog(droneLogTextBox, droneLog);
        }

        private void AppendLog(TextBox box, LogView view)
        {
            if (!view.HasNewLines)
                return;

            box.AppendText(string.Join(Environment.NewLine, view.GetNewLines()));
            box.AppendText(Environment.NewLine);
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
                AppendLog();
        }
    

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Log.Storage.OnAddedLines -= LogBuffer_OnAddedLines;
            drone.DroneLog.OnAddedLines -= LogBuffer_OnAddedLines;

            base.OnFormClosing(e);
        }
    }
}
