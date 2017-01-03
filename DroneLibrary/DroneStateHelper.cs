using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public static class DroneStateHelper
    {
        public static bool IsIdle(this DroneState state)
        {
            return state == DroneState.Reset || state == DroneState.Stopped || state == DroneState.Idle;
        }

        public static bool AreMotorsRunning(this DroneState state)
        {
            return state == DroneState.Armed || state == DroneState.Flying;
        }
    }
}
