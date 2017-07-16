using DroneControl.Input;
using DroneLibrary;
using DroneLibrary.Data;
using DroneLibrary.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Threading;

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

        private Thread thread;
        private AutoResetEvent resetEvent;

        private Stopwatch time;

        private StreamWriter dataStream;
        private StreamWriter logStream;

        private LogView log;
        private LogView droneLog;
        private DroneState lastState;

        public RecordForm(Drone drone, InputManager inputManager)
        {
            this.drone = drone;
            this.inputManager = inputManager;

            resetEvent = new AutoResetEvent(false);

            lastState = drone.Data.State;
            drone.OnDataChange += Drone_OnDataChange;

            InitializeComponent();
        }

        private void Drone_OnDataChange(object sender, DataChangedEventArgs e)
        {
            // wake up Thread
            resetEvent.Set();

            if (InvokeRequired)
                Invoke(new EventHandler<DataChangedEventArgs>(Drone_OnDataChange), sender, e);
            else
            {
                // Check if we should auto start or auto stop
                if (!autoStartCheckBox.Checked)
                    return;

                if (e.Data.State != lastState)
                {
                    if (e.Data.State == DroneState.Flying)
                    {
                        if (!running)
                        {
                            Start();
                            Log.Info("Starting automatic recording... state changed: {0} --> {1}", lastState, e.Data.State);
                        }
                    }
                    else if (running)
                    {
                        Log.Info("Stopping recording... state changed: {0} --> {1}", lastState, e.Data.State);
                        Stop();
                    }

                    lastState = e.Data.State;
                }
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            drone.OnDataChange -= Drone_OnDataChange;
            Stop();
            base.OnFormClosed(e);
        }

        public void Start()
        {
            if (running)
                return;

            string recordingName = "recording__" + DateTime.Now.ToString("yyyy_MM_dd___HH_mm_ss");
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

            Log.LogSystemInfo();

            Log.Debug("Drone info:");
            Log.WriteProperties(LogLevel.Debug, drone.Info);

            Log.Debug("Drone settings:");
            Log.WriteFields(LogLevel.Debug, drone.Settings);

            // Start thread
            resetEvent.Reset();
            thread = new Thread(Run);
            thread.Start();
            resetEvent.Set();
        }

        public void Stop()
        {
            if (!running)
                return;

            EmitData();

            running = false;

            // signal thread that it should stop
            resetEvent.Set();

            // Update ui
            Log.Info("Stopped recording");

            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.Value = progressBar.Maximum;

            statusLabel.Text = "Stopped";
            startButton.Text = "Start";

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

                string dataTag = string.Format("koalaDrone data (version {0}, date {1})", version, now);
                logStream.WriteLine("koalaDrone log (version {0}, date {1})", version, now);

                IEnumerable<string> columnsText;
                if (useQuotes)
                    columnsText = columns.Select(s => '"' + s + '"');
                else
                    columnsText = columns.AsEnumerable();

                // DataTag schreiben
                columnsText = columnsText.Skip(1);
                dataStream.Write(dataTag);
                dataStream.Write(csvSeperator);
                dataStream.Write(' ');

                dataStream.WriteLine(string.Join(csvSeperator + " ", columnsText));
                return true;
            }
            catch (Exception e)
            {
                ErrorHandler.HandleException(drone, e);
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
                ErrorHandler.HandleException(drone, e);
            }
        }

        private void Run()
        {
            try
            {
                while(running)
                {
                    EmitData();

                    // wait till new data
                    resetEvent.WaitOne();
                }
            }
            catch (Exception e)
            {
                ErrorHandler.HandleException(drone, e);
            }
        }

        private void UpdateUI()
        {
            try
            {
                if (!running)
                    return;

                long fileSize = dataStream.BaseStream.Length + logStream.BaseStream.Length;
                if (fileSize < 1024 * 1024)
                    statusLabel.Text = string.Format("Running ({0}kbyte)", fileSize / 1024);
                else
                    statusLabel.Text = string.Format("Running ({0}mbyte)", fileSize / (1024 * 1024));
            }
            catch (Exception e)
            {
                ErrorHandler.HandleException(drone, e);
            }
        }

        private static string[] columns = new string[]
        {
            "Time ms",

            "Ping ms",

            "State", "RSSI", "BatteryV", "FreeHeap",
            "Roll", "Pitch", "Yaw",

            "GyroX", "GyroY", "GyroZ",
            "AccX", "AccY", "AccZ",
            "MagX", "MagY", "MagZ",

            "Pressure", "Altitude",
            "Humidity",

            "Temps",

            "Target Roll",
            "Target Pitch",
            "Target Yaw",

            "Thrust",
            "PID Roll",
            "PID Pitch",
            "PID Yaw",
            "PID Stab Roll",
            "PID Stab Pitch",
            "PID Stab Yaw",
            "FL", "FR", "BL", "BR"
        };

        private StringBuilder line = new StringBuilder();

        private void EmitData()
        {
            try
            {
                EmitLine();
                EmitLog();

                Invoke(new Action(UpdateUI));
            }
            catch (Exception e)
            {
                ErrorHandler.HandleException(drone, e);
            }
        }

        private void EmitLine()
        {
            DroneData data = drone.Data;
            OutputData outputData = drone.DebugOutputData;

            object[] values = new object[]
                {
                    time.ElapsedMilliseconds,

                    drone.IsConnected ? drone.Ping.ToString() : "DISCONNECTED",

                    data.State,
                    data.WifiRssi,
                    data.BatteryVoltage,
                    drone.DebugProfilerData.FreeHeapBytes,

                    data.Sensor.Roll,
                    data.Sensor.Pitch,
                    data.Sensor.Yaw,
                    
                    data.Sensor.Gyro.X,
                    data.Sensor.Gyro.Y,
                    data.Sensor.Gyro.Z,
                    
                    data.Sensor.Acceleration.X,
                    data.Sensor.Acceleration.Y,
                    data.Sensor.Acceleration.Z,
                    
                    data.Sensor.Magnet.X,
                    data.Sensor.Magnet.Y,
                    data.Sensor.Magnet.Z,
                    
                    data.Sensor.Baro.Pressure,
                    data.Sensor.Baro.Altitude,
                    data.Sensor.Baro.Humidity,

                    string.Join(", ", drone.Data.Sensor.Temperatures),

                    inputManager.TargetData.Roll,
                    inputManager.TargetData.Pitch,
                    inputManager.TargetData.Yaw,
                    inputManager.TargetData.Thrust,

                    outputData.RollOutput,
                    outputData.PitchOutput,
                    outputData.YawOutput,
                    
                    outputData.AngleRollOutput,
                    outputData.AnglePitchOutput,
                    outputData.AngleYawOutput,

                    data.MotorValues.FrontLeft,
                    data.MotorValues.FrontRight,
                    data.MotorValues.BackLeft,
                    data.MotorValues.BackRight
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

        private void startButton_Click(object sender, EventArgs e)
        {
            if (running)
                Stop();
            else
                Start();
        }
    }
}
