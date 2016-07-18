namespace DroneControl
{
    partial class SensorControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.GroupBox sensorGroupBox;
            this.calibrationRunningText = new System.Windows.Forms.Label();
            this.magnetLabel = new System.Windows.Forms.Label();
            this.rotationLabel = new System.Windows.Forms.Label();
            this.batteryVoltageLabel = new System.Windows.Forms.Label();
            this.orientationLabel = new System.Windows.Forms.Label();
            this.temperatureLabel = new System.Windows.Forms.Label();
            this.accelerationLabel = new System.Windows.Forms.Label();
            this.headingIndicator = new DroneControl.Avionics.HeadingIndicatorInstrumentControl();
            this.calibrateGyroButton = new System.Windows.Forms.Button();
            this.artificialHorizon = new DroneControl.Avionics.AttitudeIndicatorInstrumentControl();
            this.pressureLabel = new System.Windows.Forms.Label();
            this.humidityLabel = new System.Windows.Forms.Label();
            this.altitudeLabel = new System.Windows.Forms.Label();
            this.temperatureBaroLabel = new System.Windows.Forms.Label();
            sensorGroupBox = new System.Windows.Forms.GroupBox();
            sensorGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // sensorGroupBox
            // 
            sensorGroupBox.Controls.Add(this.temperatureBaroLabel);
            sensorGroupBox.Controls.Add(this.altitudeLabel);
            sensorGroupBox.Controls.Add(this.humidityLabel);
            sensorGroupBox.Controls.Add(this.pressureLabel);
            sensorGroupBox.Controls.Add(this.calibrationRunningText);
            sensorGroupBox.Controls.Add(this.magnetLabel);
            sensorGroupBox.Controls.Add(this.rotationLabel);
            sensorGroupBox.Controls.Add(this.batteryVoltageLabel);
            sensorGroupBox.Controls.Add(this.orientationLabel);
            sensorGroupBox.Controls.Add(this.temperatureLabel);
            sensorGroupBox.Controls.Add(this.accelerationLabel);
            sensorGroupBox.Controls.Add(this.headingIndicator);
            sensorGroupBox.Controls.Add(this.calibrateGyroButton);
            sensorGroupBox.Controls.Add(this.artificialHorizon);
            sensorGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            sensorGroupBox.Location = new System.Drawing.Point(0, 0);
            sensorGroupBox.Name = "sensorGroupBox";
            sensorGroupBox.Size = new System.Drawing.Size(410, 312);
            sensorGroupBox.TabIndex = 0;
            sensorGroupBox.TabStop = false;
            sensorGroupBox.Text = "Sensors";
            // 
            // calibrationRunningText
            // 
            this.calibrationRunningText.AutoSize = true;
            this.calibrationRunningText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.calibrationRunningText.Location = new System.Drawing.Point(6, 240);
            this.calibrationRunningText.Name = "calibrationRunningText";
            this.calibrationRunningText.Size = new System.Drawing.Size(103, 13);
            this.calibrationRunningText.TabIndex = 23;
            this.calibrationRunningText.Text = "Calibration running...";
            // 
            // magnetLabel
            // 
            this.magnetLabel.AutoSize = true;
            this.magnetLabel.Location = new System.Drawing.Point(6, 227);
            this.magnetLabel.Name = "magnetLabel";
            this.magnetLabel.Size = new System.Drawing.Size(43, 13);
            this.magnetLabel.TabIndex = 22;
            this.magnetLabel.Text = "Magnet";
            // 
            // rotationLabel
            // 
            this.rotationLabel.AutoSize = true;
            this.rotationLabel.Location = new System.Drawing.Point(6, 214);
            this.rotationLabel.Name = "rotationLabel";
            this.rotationLabel.Size = new System.Drawing.Size(47, 13);
            this.rotationLabel.TabIndex = 21;
            this.rotationLabel.Text = "Rotation";
            // 
            // batteryVoltageLabel
            // 
            this.batteryVoltageLabel.AutoSize = true;
            this.batteryVoltageLabel.Location = new System.Drawing.Point(212, 214);
            this.batteryVoltageLabel.Name = "batteryVoltageLabel";
            this.batteryVoltageLabel.Size = new System.Drawing.Size(78, 13);
            this.batteryVoltageLabel.TabIndex = 20;
            this.batteryVoltageLabel.Text = "Battery voltage";
            // 
            // orientationLabel
            // 
            this.orientationLabel.AutoSize = true;
            this.orientationLabel.Location = new System.Drawing.Point(6, 201);
            this.orientationLabel.Name = "orientationLabel";
            this.orientationLabel.Size = new System.Drawing.Size(58, 13);
            this.orientationLabel.TabIndex = 19;
            this.orientationLabel.Text = "Orientation";
            // 
            // temperatureLabel
            // 
            this.temperatureLabel.AutoSize = true;
            this.temperatureLabel.Location = new System.Drawing.Point(211, 227);
            this.temperatureLabel.Name = "temperatureLabel";
            this.temperatureLabel.Size = new System.Drawing.Size(67, 13);
            this.temperatureLabel.TabIndex = 18;
            this.temperatureLabel.Text = "Temperature";
            // 
            // accelerationLabel
            // 
            this.accelerationLabel.AutoSize = true;
            this.accelerationLabel.Location = new System.Drawing.Point(212, 201);
            this.accelerationLabel.Name = "accelerationLabel";
            this.accelerationLabel.Size = new System.Drawing.Size(66, 13);
            this.accelerationLabel.TabIndex = 17;
            this.accelerationLabel.Text = "Acceleration";
            // 
            // headingIndicator
            // 
            this.headingIndicator.CausesValidation = false;
            this.headingIndicator.Location = new System.Drawing.Point(215, 18);
            this.headingIndicator.Name = "headingIndicator";
            this.headingIndicator.RotateAircraft = true;
            this.headingIndicator.Size = new System.Drawing.Size(175, 175);
            this.headingIndicator.TabIndex = 16;
            this.headingIndicator.Text = "headingIndicatorInstrumentControl1";
            // 
            // calibrateGyroButton
            // 
            this.calibrateGyroButton.Location = new System.Drawing.Point(186, 19);
            this.calibrateGyroButton.Name = "calibrateGyroButton";
            this.calibrateGyroButton.Size = new System.Drawing.Size(23, 23);
            this.calibrateGyroButton.TabIndex = 15;
            this.calibrateGyroButton.Text = "0";
            this.calibrateGyroButton.UseVisualStyleBackColor = true;
            this.calibrateGyroButton.Click += new System.EventHandler(this.calibrateGyroButton_Click);
            // 
            // artificialHorizon
            // 
            this.artificialHorizon.Location = new System.Drawing.Point(6, 19);
            this.artificialHorizon.Name = "artificialHorizon";
            this.artificialHorizon.Size = new System.Drawing.Size(175, 175);
            this.artificialHorizon.TabIndex = 14;
            this.artificialHorizon.Text = "attitudeIndicatorInstrumentControl1";
            // 
            // pressureLabel
            // 
            this.pressureLabel.AutoSize = true;
            this.pressureLabel.Location = new System.Drawing.Point(6, 267);
            this.pressureLabel.Name = "pressureLabel";
            this.pressureLabel.Size = new System.Drawing.Size(48, 13);
            this.pressureLabel.TabIndex = 24;
            this.pressureLabel.Text = "Pressure";
            // 
            // humidityLabel
            // 
            this.humidityLabel.AutoSize = true;
            this.humidityLabel.Location = new System.Drawing.Point(212, 267);
            this.humidityLabel.Name = "humidityLabel";
            this.humidityLabel.Size = new System.Drawing.Size(47, 13);
            this.humidityLabel.TabIndex = 25;
            this.humidityLabel.Text = "Humidity";
            // 
            // altitudeLabel
            // 
            this.altitudeLabel.AutoSize = true;
            this.altitudeLabel.Location = new System.Drawing.Point(6, 280);
            this.altitudeLabel.Name = "altitudeLabel";
            this.altitudeLabel.Size = new System.Drawing.Size(42, 13);
            this.altitudeLabel.TabIndex = 26;
            this.altitudeLabel.Text = "Altitude";
            // 
            // temperatureBaroLabel
            // 
            this.temperatureBaroLabel.AutoSize = true;
            this.temperatureBaroLabel.Location = new System.Drawing.Point(211, 280);
            this.temperatureBaroLabel.Name = "temperatureBaroLabel";
            this.temperatureBaroLabel.Size = new System.Drawing.Size(91, 13);
            this.temperatureBaroLabel.TabIndex = 27;
            this.temperatureBaroLabel.Text = "Temperature baro";
            // 
            // SensorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(sensorGroupBox);
            this.Name = "SensorControl";
            this.Size = new System.Drawing.Size(410, 312);
            sensorGroupBox.ResumeLayout(false);
            sensorGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label temperatureLabel;
        private System.Windows.Forms.Label accelerationLabel;
        private Avionics.HeadingIndicatorInstrumentControl headingIndicator;
        private System.Windows.Forms.Button calibrateGyroButton;
        private Avionics.AttitudeIndicatorInstrumentControl artificialHorizon;
        private System.Windows.Forms.Label orientationLabel;
        private System.Windows.Forms.Label batteryVoltageLabel;
        private System.Windows.Forms.Label rotationLabel;
        private System.Windows.Forms.Label magnetLabel;
        private System.Windows.Forms.Label calibrationRunningText;
        private System.Windows.Forms.Label altitudeLabel;
        private System.Windows.Forms.Label humidityLabel;
        private System.Windows.Forms.Label pressureLabel;
        private System.Windows.Forms.Label temperatureBaroLabel;
    }
}
