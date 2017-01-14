using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;

namespace DroneControl.Input.Remote
{
    public class FlightController : IDisposable
    {
        private SerialPort port;

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

        private Thread readThread;

        public FlightController(string comPort)
        {
            this.ComPort = comPort;

            port = new SerialPort(comPort, 115200, Parity.None, 8, StopBits.One);
            port.NewLine = "\r\n";
            port.Open();

            if (port.IsOpen)
            {
                readThread = new Thread(ReadData);
                readThread.Start();
            }
        }

        public void Dispose()
        {
            if (readThread != null)
                readThread.Abort();
            if (port != null)
                port.Close();
            SetDisconnectedState();
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
                while (port.IsOpen)
                {
                    string line = port.ReadLine();

                    if (line.StartsWith("&"))
                        continue;

                    if (!line.StartsWith("%"))
                    {
                        SetDisconnectedState();
                        continue;
                    }

                    string[] values = line.Substring(1).Trim().Split(',').Select(v => v.Trim()).ToArray();
                    if (values.Length == 0)
                    {
                        SetDisconnectedState();
                        continue;
                    }

                    bool ok;
                    if (values[0] == "OK")
                        ok = true;
                    else if (values[0] == "NOTOK")
                        ok = false;
                    else
                    {
                        SetDisconnectedState();
                        continue;
                    }

                    int[] data = new int[values.Length - 1];
                    for (int i = 0; i < data.Length; i++)
                    {
                        int v;
                        if (!int.TryParse(values[i + 1], out v))
                        {
                            data[i] = 1000;
                            ok = false;
                        }
                        else
                            data[i] = Clamp(v, 1000, 2000);
                    }

                    lock (readThread)
                    {
                        if (data.Length == ChannelCount)
                        {
                            this.data = data;
                            IsOK = ok;
                        }
                        else
                            IsOK = false;
                        IsConnected = true;
                    }
                }
            }
            catch (Exception)
            {

            }

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
