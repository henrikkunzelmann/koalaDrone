using DroneControl.Input;
using DroneLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DroneControl
{
    public partial class RecordForm : Form
    {
        private const char csvSeperator = ';';
        private bool useQuotes = false;

        private Drone drone;
        private InputManager inputManager;

        private bool running;
        private string path;

        private Stopwatch time;

        private StreamWriter dataStream;
        private StreamWriter logStream;

        public RecordForm(Drone drone, InputManager inputManager)
        {
            this.drone = drone;
            this.inputManager = inputManager;

            InitializeComponent();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (running)
                Stop();
            base.OnFormClosed(e);
        }

        public void Start()
        {
            if (running)
                throw new InvalidOperationException("Already running");

            string recordingName = "recording_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            fileLabel.Text = string.Format("Recording to \"{0}\"", recordingName);

            path = Path.Combine(Environment.CurrentDirectory, "recordings", recordingName);

            if (!OpenFiles())
            {
                statusLabel.Text = "Error";
                return;
            }

            running = true;

            Log.Info("Starting recording: {0}", recordingName);

            progressBar.Style = ProgressBarStyle.Marquee;
            statusLabel.Text = "Running";
            startButton.Text = "Stop";

            time = new Stopwatch();
            time.Start();

            log = Log.Storage.CreateView(true);
            droneLog = drone.DroneLog.CreateView(true);

            Log.Debug("Drone info:");
            Log.WriteProperties(LogLevel.Debug, drone.Info);

            Log.Debug("Drone settings:");
            Log.WriteFields(LogLevel.Debug, drone.Settings);

            EmitData();

            timer.Enabled = true;
        }

        public void Stop()
        {
            if (!running)
                throw new InvalidOperationException("Alread stopped");

            running = false;

            Log.Info("Stopped recording");

            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.Value = progressBar.Maximum;

            statusLabel.Text = "Stopped";
            startButton.Text = "Start";

            timer.Enabled = false;
            CloseFiles();
        }

        private bool OpenFiles()
        {
            try
            {
                Directory.CreateDirectory(path);

                dataStream = File.CreateText(Path.Combine(path, "data.csv"));
                logStream = File.CreateText(Path.Combine(path, "log.txt"));

                Version version = Assembly.GetExecutingAssembly().GetName().Version;
                DateTime now = DateTime.Now;
                dataStream.WriteLine("koalaDrone data (version {0}, date {1})", version, now);
                logStream.WriteLine("koalaDrone log (version {0}, date {1})", version, now);

                IEnumerable<string> columnsText;
                if (useQuotes)
                    columnsText = columns.Select(s => '"' + s + '"');
                else
                    columnsText = columns.AsEnumerable();

                dataStream.WriteLine(string.Join(csvSeperator + " ", columnsText));
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            return false;
        }

        private void CloseFiles()
        {
            try
            {
                if (dataStream != null)
                    dataStream.Close();
                if (logStream != null)
                    logStream.Close();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private static string[] columns = new string[]
        {
            "Time ms",

            "State", "RSSI", "BatteryV", "FreeHeap",
            "GyroTemp", "Pitch", "Roll", "Yaw",

            "GyroX", "GyroY", "GyroZ",
            "AccX", "AccY", "AccZ",
            "MagX", "MagY", "MagZ",

            "Pressure", "Altitude",
            "Humidity", "BaroTemp",

            "Target Pitch",
            "Target Roll",
            "Target Yaw",

            "Thrust",
            "PID Pitch",
            "PID Roll",
            "PID Yaw",
            "FL", "FR", "BL", "BR"
        };

        private StringBuilder line = new StringBuilder();
        private LogView log;
        private LogView droneLog;

        private void EmitData()
        {
            try
            {
                EmitLine();
                EmitLog();

                long fileSize = dataStream.BaseStream.Length + logStream.BaseStream.Length;
                if (fileSize < 1024 * 1024)
                    statusLabel.Text = string.Format("Running ({0}kbyte)", fileSize / 1024);
                else
                    statusLabel.Text = string.Format("Running ({0}mkbyte)", fileSize / (1024 * 1024));
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private void EmitLine()
        {
            object[] values = new object[]
                {
                    time.ElapsedMilliseconds,

                    drone.Data.State,
                    drone.Data.WifiRssi,
                    drone.Data.BatteryVoltage,
                    drone.DebugData.FreeHeapBytes,

                    drone.Data.Gyro.Temperature,
                    drone.Data.Gyro.Pitch,
                    drone.Data.Gyro.Roll,
                    drone.Data.Gyro.Yaw,

                    drone.Data.Gyro.GyroX,
                    drone.Data.Gyro.GyroY,
                    drone.Data.Gyro.GyroZ,

                    drone.Data.Gyro.AccelerationX,
                    drone.Data.Gyro.AccelerationY,
                    drone.Data.Gyro.AccelerationZ,

                    drone.Data.Gyro.MagnetX,
                    drone.Data.Gyro.MagnetY,
                    drone.Data.Gyro.MagnetZ,

                    drone.Data.Baro.Pressure,
                    drone.Data.Baro.Altitude,
                    drone.Data.Baro.Humidity,
                    drone.Data.Baro.Temperature,

                    inputManager.TargetData.Pitch,
                    inputManager.TargetData.Roll,
                    inputManager.TargetData.Yaw,
                    inputManager.TargetData.Thrust,

                    drone.DebugData.PitchOutput,
                    drone.DebugData.RollOutput,
                    drone.DebugData.YawOutput,

                    drone.Data.MotorValues.FrontLeft,
                    drone.Data.MotorValues.FrontRight,
                    drone.Data.MotorValues.BackLeft,
                    drone.Data.MotorValues.BackRight
                };

            line.Clear();
            for (int i = 0; i < values.Length; i++)
            {
                if (i > 0)
                {
                    line.Append(csvSeperator);
                    line.Append(' ');
                }


                if (useQuotes)
                    line.Append('"');

                object value = values[i];
                if (value is float)
                    line.Append(((float)value).ToString("F"));
                else
                    line.Append(values[i].ToString());

                if (useQuotes)
                    line.Append('"');
            }

            dataStream.WriteLine(line.ToString());
        }

        private void EmitLog()
        {
            foreach (string line in log.GetNewLines())
                logStream.WriteLine(line);
            foreach (string line in droneLog.GetNewLines())
                logStream.WriteLine(line);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            EmitData();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (running)
                Stop();
            else
                Start();
        }
    }
}
