using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public enum GyroDLPF : byte
    {
        BW_256Hz = 0,
        BW_188Hz = 1,
        BW_98Hz = 2,
        BW_42Hz = 3,
        BW_20Hz = 4,
        BW_10Hz = 5,
        BW_5Hz = 6
    }
}
