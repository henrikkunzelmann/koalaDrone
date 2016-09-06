namespace DroneLibrary
{
    public enum ResetReason
    {
        Default = 0,
        Watchdog = 1,
        Exception = 2,
        SoftWatchdog = 3,
        SoftRestart = 4,
        DeepSleepSwake = 5,
        ExtSys = 6
    }
}
