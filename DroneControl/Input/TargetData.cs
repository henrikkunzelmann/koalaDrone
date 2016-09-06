namespace DroneControl.Input
{
    public struct TargetData
    {
        public float Roll;
        public float Pitch;
        public float Yaw;
        public float Thrust;

        public TargetData(float roll, float pitch, float yaw, float thrust)
        {
            this.Roll = roll;
            this.Pitch = pitch;
            this.Yaw = yaw;
            this.Thrust = thrust;
        }
    }
}
