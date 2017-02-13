using DroneLibrary;
using System;
using System.Net;
using System.Linq;
using System.Windows.Forms;

namespace DroneControl
{
    public partial class ConnectForm : Form
    {
        private DroneList droneList;

        public ConnectForm()
        {
            InitializeComponent();

            droneList = new DroneList(new Config());

            droneList.OnListChanged += DroneList_OnListChanged;

            searchTimer.Interval = 500; // Millisekunden
            searchTimer.Tick += (object sender, EventArgs args) =>
            {
                droneList.SendHello();
            };
            searchTimer.Start();

            droneList.TimeoutSeconds = 2; // Sekunden
            droneList.SendHello();
        }

        private void StopDroneList()
        {
            searchTimer.Stop();

            if (droneList != null)
            {
                droneList.OnListChanged -= DroneList_OnListChanged;
                droneList.Dispose();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopDroneList();
            base.OnFormClosing(e);
        }

        private void TryToConnect()
        {
            IPAddress address;
            if (!IPAddress.TryParse(ipAddressTextBox.Text, out address))
                return;

            Connect(address);
        }

        private void Connect(IPAddress address)
        {
            connectButton.Enabled = false;

            using (ConnectingForm form = new ConnectingForm(address))
            {
                // wenn wir verbunden sind (result == OK)
                if (!form.IsDisposed && form.ShowDialog() == DialogResult.OK)
                    OpenMainForm(form.Drone); // richtiges Fenster öffnen
            }

            connectButton.Enabled = true;
        }

        private void OpenMainForm(Drone drone)
        {
            StopDroneList();

            MainForm form = new MainForm(drone);
            form.Show();
            Hide();

            // wenn MainForm geschlossen wurde dann auch unser Fenster schließen
            form.FormClosed += (sender, e) => Close(); 
        }

        private void DroneList_OnListChanged(object sender, DroneListChangedEventArgs e)
        {
            if (InvokeRequired)
                Invoke(new EventHandler<DroneListChangedEventArgs>(DroneList_OnListChanged), sender, e);
            else
            {
                SuspendLayout();
                if (e.Entries.Length == 0)
                    searchStatus.Text = "Searching drones...";
                else if (e.Entries.Length == 1)
                    searchStatus.Text = string.Format("Found {0} drone...", e.Entries.Length);
                else
                    searchStatus.Text = string.Format("Found {0} drones...", e.Entries.Length);

                droneListBox.BeginUpdate();
                droneListBox.Items.Clear();
                foreach (DroneEntry entry in e.Entries)
                    droneListBox.Items.Add(entry);
                droneListBox.EndUpdate();
                ResumeLayout();
            }
        }

        private void ipAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            IPAddress address;
            connectButton.Enabled = IPAddress.TryParse(ipAddressTextBox.Text, out address);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            TryToConnect();
        }

        private void droneListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (droneListBox.SelectedItem == null)
                return;

            DroneEntry entry = (DroneEntry)droneListBox.SelectedItem;
            Connect(entry.Address);
        }

        private void ipAddressTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                e.SuppressKeyPress = true;
                OpenMainForm(new Drone(IPAddress.Loopback, new Config()));
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                TryToConnect();
            }
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            new InfoForm().ShowDialog();
        }
    }
}
