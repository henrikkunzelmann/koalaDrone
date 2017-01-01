using DroneLibrary;
using System;
using System.Net;
using System.Windows.Forms;

namespace DroneControl
{
    public partial class ConnectingForm : Form
    {
        private IPAddress ipAddress;

        public Drone Drone { get; private set; }

        public ConnectingForm(IPAddress ipAddress)
        {
            if (ipAddress == null)
                throw new ArgumentNullException(nameof(ipAddress));

            this.ipAddress = ipAddress;

            InitializeComponent();

            connectStatus.Text = string.Format(connectStatus.Text, ipAddress);

            timeoutTimer.Interval = 10 * 1000; // Timeout von 10 Sekunden
            timeoutTimer.Tick += (object sender, EventArgs args) =>
            {
                StopTimers();

                if (MessageBox.Show("Error while connecting: timeout.", "Connection Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                {
                    DisposeDrone(false);

                    DialogResult = DialogResult.Cancel;
                    Close();
                }
                else
                    StartTimers();
            };

            pingTimer.Interval = 200;
            pingTimer.Tick += (object sender, EventArgs args) =>
            {
                Drone.SendPing();
            };

            Connect();
        }

        private void DisposeDrone(bool onlyIfNotConnected)
        {
            StopTimers();
            if (Drone != null)
            {
                Drone.OnConnected -= OnDroneConnected;
                if (!onlyIfNotConnected || !Drone.IsConnected)
                    Drone.Dispose();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            DisposeDrone(true);
            base.OnFormClosing(e);
        }

        private void StartTimers()
        {
            timeoutTimer.Start();
            pingTimer.Start();
        }

        private void StopTimers()
        {
            timeoutTimer.Stop();
            pingTimer.Stop();
        }

        /// <summary>
        /// Versucht eine Verbindung zur IP-Adresse aufzubauen.
        /// </summary>
        private void Connect()
        {
            DisposeDrone(false);

            try
            {
                Drone = new Drone(ipAddress, new Config());
                Drone.OnConnected += OnDroneConnected;

                // TODO: drone.Connect() einbauen, damit das Event schon gesetzt ist bevor wir verbinden
                if (Drone.IsConnected) // schauen ob wir schon verbunden wurden, als wir das Event gesetzt haben
                    OnDroneConnected(this, EventArgs.Empty);

                StartTimers();
            }
            catch(Exception e)
            {
                Log.Error(e);
                MessageBox.Show("Error while connecting: exception." + Environment.NewLine + e.ToString(), "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                DisposeDrone(false);

                DialogResult = DialogResult.Abort;
                Close();
            }
        }

        private void OnDroneConnected(object sender, EventArgs args)
        {
            if (InvokeRequired)
                Invoke(new EventHandler(OnDroneConnected), sender, args);
            else
            {
                StopTimers();

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void abortButton_Click(object sender, EventArgs e)
        {
            DisposeDrone(false);

            DialogResult = DialogResult.Abort;
            Close();
        }
    }
}
