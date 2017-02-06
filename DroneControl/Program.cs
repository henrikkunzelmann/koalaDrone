using DroneLibrary;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace DroneControl
{
    public static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            Log.Info("DroneControl booting up... Version: {0}", version);
            Log.LogSystemInfo();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ConnectForm());
        }
    }
}
