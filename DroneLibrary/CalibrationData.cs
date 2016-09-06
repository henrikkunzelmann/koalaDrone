using System.ComponentModel;

namespace DroneLibrary
{
    [TypeConverter(typeof(DroneSettingsTypeConverter))]
    public struct CalibrationData
    {
        public float MinX;
        public float MinY;
        public float MinZ;

        public float MaxX;
        public float MaxY;
        public float MaxZ;

        public float AverageX;
        public float AverageY;
        public float AverageZ;
    }
}
