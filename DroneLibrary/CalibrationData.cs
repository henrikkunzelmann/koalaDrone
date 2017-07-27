using System.ComponentModel;

namespace DroneLibrary
{
    [TypeConverter(typeof(DroneSettingsTypeConverter))]
    public struct CalibrationData
    {
        public ulong Count;

        public float MinX;
        public float MinY;
        public float MinZ;

        public float MaxX;
        public float MaxY;
        public float MaxZ;

        public float SumX;
        public float SumY;
        public float SumZ;

        public float AverageX;
        public float AverageY;
        public float AverageZ;

        public float Length;
    }
}
