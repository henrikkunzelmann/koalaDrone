using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Globalization;
using DroneLibrary.Diagnostics;

namespace DroneControl.Input.Remote
{
    public class FlightController : IDisposable
    {
        private SerialPort port;
        private Stopwatch notOkWatch = new Stopwatch();

        public const int ChannelCount = 6;

        public string ComPort { get; private set; }

        public bool IsConnected { get; private set; }
        public bool IsOK { get; private set; }

        private int[] data;

        public int[] Data
        {
            get
            {
                lock (readThread)
                {
                    return data;
                }
            }
        }

        public TimeSpan UpdateInterval { get; private set; }

        private Thread readThread;

        public FlightController(string comPort)
        {
            this.ComPort = comPort;
            Connect();
        }

        private void Connect()
        {
            port = new SerialPort(ComPort, 115200, Parity.None, 8, StopBits.One);
            port.NewLine = "\r\n";
            port.Open();

            if (port.IsOpen)
            {
                readThread = new Thread(ReadData);
                readThread.Start();
            }
        }

        public void Reconnect()
        {
            if (IsConnected)
                return;

            if (!port.IsOpen)
            {
                Dispose();
                Connect();
            }
        }

        public void Dispose()
        {
            if (readThread != null)
                readThread.Abort();
            if (port != null)
            {
                try
                {
                    port.Close();
                }
                catch (Exception)
                {

                }
                port = null;
            }
            SetDisconnectedState();
        }

        private void SetOK(bool ok, bool becauseWrongData)
        {
            IsOK = ok;

            if (ok || becauseWrongData)
            {
                IsConnected = true;
                notOkWatch.Stop();
            }
            else
            {
                if (!notOkWatch.IsRunning)
                    notOkWatch.Restart();

                if (notOkWatch.Elapsed.TotalSeconds > 5)
                    SetDisconnectedState();
            }
        }

        private void SetDisconnectedState()
        {
            IsConnected = false;
            IsOK = false;
        }

        private void ReadData()
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (port.IsOpen)
                {
                    string line = port.ReadLine();

                    bool ok;
                    if (line.StartsWith("%O"))
                        ok = true;
                    else if (line.StartsWith("%N"))
                        ok = false;
                    else
                    {
                        SetOK(false, false);
                        continue;
                    }

                    string[] values = line.Substring(2).Trim().Split(',').Select(v => v.Trim()).ToArray();
                    if (values.Length == 0)
                    {
                        SetOK(false, false);
                        continue;
                    }

                    int[] data = new int[values.Length - 1];

                    int valueSum = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        int v;
                        if (!int.TryParse(values[i], NumberStyles.AllowHexSpecifier, null, out v))
                        {
                            data[i] = 1000;
                            ok = false;
                        }
                        else
                            data[i] = Clamp(v, 1000, 2000);
                        valueSum += data[i];
                    }

                    int sentValueSum;
                    if (!int.TryParse(values.Last(), NumberStyles.AllowHexSpecifier, null, out sentValueSum) || sentValueSum != valueSum)
                        SetOK(false, false);


                    lock (readThread)
                    {
                        if (data.Length == ChannelCount)
                        {
                            this.data = data;
                            SetOK(ok, true);
                        }
                        else
                            SetOK(false, false);

                        UpdateInterval = sw.Elapsed;
                        sw.Restart();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            Log.Warning("FlightController.ReadData() no longer running");
            SetDisconnectedState();
        }

        public int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }
    }
}
