using System.ComponentModel;

namespace DroneLibrary
{
    [TypeConverter(typeof(DroneSettingsTypeConverter))]
    public struct SensorCalibration
    {
        public CalibrationData MagnetData;
        public float MagneticFieldStrenght;
    }
}
