using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroneLibrary.Diagnostics;
using DroneLibrary;
using System.Windows.Forms;

namespace DroneControl
{
    public static class ErrorHandler
    {
        public static void HandleException(Drone drone, Exception exception)
        {
            if (exception == null)
                Log.Error("ErrorHandler.HandleException(): Exception is null");
            else
            {
                Log.Error(exception);
                if (drone == null)
                    Log.Error("ErrorHandler.HandleException(): Drone is null");
                else if (drone.Data.State.AreMotorsRunning())
                    Log.Warning("ErrorHandler.HandleException(): Exception while motors are running");
                else
                    MessageBox.Show(exception.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
