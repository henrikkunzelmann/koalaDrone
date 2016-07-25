using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    [TypeConverter(typeof(DroneSettingsTypeConverter))]
    public struct SensorCalibration
    {
        public CalibrationData MagnetData;
        public float MagneticFieldStrenght;
    }
}
