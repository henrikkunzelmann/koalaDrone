namespace DroneLibrary
{
    public struct DroneData  {
        public DroneState State { get; }
        public QuadMotorValues MotorValues { get; }
        public GyroData Gyro { get; }
        public BaroData Baro { get; }
        public float BatteryVoltage { get; }
        public int WifiRssi { get; }

        public DroneData(DroneState state, QuadMotorValues motorValues, GyroData gyro, BaroData baro, float batteryVoltage, int wifiRssi) {
            this.State = state;
            this.MotorValues = motorValues;
            this.Gyro = gyro;
            this.Baro = baro;
            this.BatteryVoltage = batteryVoltage;
            this.WifiRssi = wifiRssi;
        }
    }
}
