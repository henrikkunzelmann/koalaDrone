namespace DroneControl.Input
{
    public struct BatteryInfo
    {
        public readonly bool HasBattery;
        public readonly BatteryLevel Level;

        public BatteryInfo(bool hasBattery, BatteryLevel level)
        {
            this.HasBattery = hasBattery;
            this.Level = level;
        }
    }
}
