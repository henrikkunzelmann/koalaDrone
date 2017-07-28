using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace DroneLibrary
{
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
    [TypeConverter(typeof(DroneSettingsTypeConverter))]
    public unsafe struct DroneSettings 
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        [Category("Drone")]
        [Description("User-friendly name for the drone")]
        public string DroneName;

        [MarshalAs(UnmanagedType.U1)]
        [Category("Drone")]
        public bool SaveConfig;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
        [Category("Network")]
        [Description("The SIID of the WiFi network")]
        public string NetworkSSID;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        [Category("Network")]
        [Description("The password of the WiFi network")]
        public string NetworkPassword;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        [Category("Network")]
        [Description("The password of the WiFi access point")]
        public string AccessPointPassword;

        [Category("Network")]
        [Description("The UDP port for the hello packets")]
        public ushort NetworkHelloPort;

        [Category("Network")]
        [Description("The UDP port for the control packets")]
        public ushort NetworkControlPort;

        [Category("Network")]
        [Description("The UDP port for the data packets")]
        public ushort NetworkDataPort;
        
        [Category("Network")]
        [Description("The size of the buffer for incoming packets")]
        public ushort NetworkPacketBufferSize;

        
        [Category("Network")]
        [Description("Time before the drone stops without any action sent")]
        public uint MaximumNetworkTimeout;

        [MarshalAs(UnmanagedType.U1)]
        [Category("Debug")]
        [Description("Enables or disables the output of log messages to the serial port")]
        public bool VerboseSerialLog;

        [Category("Drone")]
        [Description("The temperature, at which the drone starts to decent on turn off")]
        public float MaxTemperature;

        [Category("Motors")]
        [Description("Minimum value for the servo motors")]
        public ushort ServoMin;

        [Category("Motors")]
        [Description("Maximum value for the servo motors")]
        public ushort ServoMax;

        [Category("Motors")]
        [Description("Value when the servo motors begin to start")]
        public ushort ServoIdle;

        [Category("PID Flying")]
        public PidSettings RollPid;

        [Category("PID Flying")]
        public PidSettings PitchPid;

        [Category("PID Flying")]
        public PidSettings YawPid;

        [Category("Flying")]
        public float InputScale;

        [Category("Flying")]
        public float SafePitch;

        [Category("Flying")]
        public float SafeRoll;

        [Category("Flying")]
        public int SafeServoValue;

        [MarshalAs(UnmanagedType.U1)]
        [Category("Flying")]
        public bool EnableStabilization;

        [MarshalAs(UnmanagedType.U1)]
        [Category("Flying")]
        public bool NegativeMixing;

        [Category("Flying")]
        public int MaxThrustForFlying;

        [MarshalAs(UnmanagedType.U1)]
        [Category("Flying")]
        public bool OnlyArmWhenStill;

        [Category("Flying")]
        public float RollTrim;

        [Category("Flying")]
        public float PitchTrim;

        [Category("Flying")]
        public float YawTrim;

        [Category("PID Flying")]
        public PidSettings AngleStabilization;

        [MarshalAs(UnmanagedType.U1)]
        [Category("Debug")]
        public bool EnableImuAcc;

        [MarshalAs(UnmanagedType.U1)]
        [Category("Debug")]
        public bool EnableImuMag;

        [Category("Debug")]
        public float GyroFilter;

        [Category("Debug")]
        public float AccFilter;

        [MarshalAs(UnmanagedType.U1)]
        [Category("Debug")]
        public bool StabOnlyHelp;

        [Category("Debug")]
        public float StabInputScale;

        [Category("Debug")]
        public float YawCorrectionFactor;

        [Category("Debug")]
        public float YawMaxCorrection;

        [MarshalAs(UnmanagedType.U1)]
        [Category("Debug")]
        public bool PassThroughSerialNMEA;

        [MarshalAs(UnmanagedType.U1)]
        [Category("Debug")]
        public bool IgnoreGyroSelfTest;

        [MarshalAs(UnmanagedType.U1)]
        [Category("Motors")]
        public bool CalibrateServos;

        [Category("Sensor")]
        public SensorCalibration CalibrationData;


        public static DroneSettings Read(PacketBuffer packetBuffer)
        {
            if (packetBuffer == null)
                throw new ArgumentNullException(nameof(packetBuffer));

            int size = Marshal.SizeOf(typeof(DroneSettings));

            byte[] buffer = new byte[size];
            packetBuffer.Read(buffer, 0, buffer.Length);

            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            DroneSettings settings = (DroneSettings)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(DroneSettings));  
            handle.Free();

            return settings;
        }

        public void Write(PacketBuffer packetBuffer)
        {
            if (packetBuffer == null)
                throw new ArgumentNullException(nameof(packetBuffer));

            int size = Marshal.SizeOf(typeof(DroneSettings));

            byte[] buffer = new byte[size];
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            Marshal.StructureToPtr(this, handle.AddrOfPinnedObject(), false);
            handle.Free();

            packetBuffer.Write(buffer, 0, buffer.Length);
        }
    }
}
