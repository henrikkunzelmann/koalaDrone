﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DroneLibrary;

namespace DroneControl
{
    public partial class GraphForm : Form
    {
        public Drone Drone { get; private set; }
        public FlightControl FlightControl { get; private set; }

        public GraphForm(Drone drone, FlightControl flightControl)
        {
            InitializeComponent();

            this.Drone = drone;
            this.Drone.OnDebugDataChange += Drone_OnDebugDataChange;

            this.FlightControl = flightControl;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (this.Drone != null)
                this.Drone.OnDebugDataChange -= Drone_OnDebugDataChange;
            base.OnFormClosed(e);
        }

        private void Drone_OnDebugDataChange(object sender, DebugDataChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<DebugDataChangedEventArgs>(Drone_OnDebugDataChange), sender, e);
                return;
            }

            if (Drone.Data.State == DroneState.Armed || Drone.Data.State == DroneState.Flying)
            {
                UpdateGraph(ratiosGraph, e.DebugData.Real);
                UpdateGraph(correctionGraph, e.DebugData.Correction);
            }
        }

        private void FlightControl_OnRatioChanged(object sender, MotorRatios e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<MotorRatios>(FlightControl_OnRatioChanged), sender, e);
                return;
            }

            // nur Werte zeigen, wenn die Drohne keinen echten Werte berechnet und ausführt
            if (Drone.Data.State != DroneState.Armed && Drone.Data.State != DroneState.Flying)
            {
                UpdateGraph(ratiosGraph, e);
                UpdateGraph(correctionGraph, new MotorRatios());
            }
        }

        private void UpdateGraph(QuadGraphControl graph, MotorRatios values)
        {
            graph.UpdateValues(values.FrontLeft, values.FrontRight, values.BackLeft, values.BackRight);
        }

        private void motorTabPage_Click(object sender, EventArgs e)
        {

        }
    }
}
