using System.ComponentModel;

namespace DroneLibrary
{
    [TypeConverter(typeof(DroneSettingsTypeConverter))]
    public struct SensorCalibration
    {
        public CalibrationData MagnetCalibration;
    }
}
