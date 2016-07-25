using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
