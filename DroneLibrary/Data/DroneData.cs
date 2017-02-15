namespace DroneLibrary.Data
{
    public struct DroneData
    {
        public DroneState State { get; }
        public QuadMotorValues MotorValues { get; }
        public SensorData Sensor { get; }

        public float BatteryVoltage { get; }
        public int WifiRssi { get; }

        public DroneData(DroneState state, QuadMotorValues motorValues, SensorData sensor, float batteryVoltage, int wifiRssi)
        {
            this.State = state;
            this.MotorValues = motorValues;
            this.Sensor = sensor;
            this.BatteryVoltage = batteryVoltage;
            this.WifiRssi = wifiRssi;
        }
    }
}
