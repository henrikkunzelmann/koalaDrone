namespace DroneLibrary
{
    public struct DebugData
    {
        public readonly int FreeHeapBytes;
        public readonly DebugProfiler Profiler;
        public readonly float PitchOutput;
        public readonly float RollOutput;
        public readonly float YawOutput;

        public DebugData(PacketBuffer buffer)
        {
            FreeHeapBytes = buffer.ReadInt();
            Profiler = new DebugProfiler(buffer);
            PitchOutput = buffer.ReadFloat();
            RollOutput = buffer.ReadFloat();
            YawOutput = buffer.ReadFloat();
        }
    }
}
