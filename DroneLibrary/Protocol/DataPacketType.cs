namespace DroneLibrary.Protocol
{
    public enum DataPacketType : byte
    {
        Drone = 1,
        Log = 2,
        DebugOutput = 3,
        DebugProfiler = 4
    }
}
