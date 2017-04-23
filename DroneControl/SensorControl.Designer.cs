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
            this.calibrateMagnetButton = new System.Windows.Forms.Button();
            this.altitudeLabel = new System.Windows.Forms.Label();
            this.humidityLabel = new System.Windows.Forms.Label();
            this.pressureLabel = new System.Windows.Forms.Label();
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
            this.altitudePositionLabel = new System.Windows.Forms.Label();
            this.longitudeLabel = new System.Windows.Forms.Label();
            this.latitudeLabel = new System.Windows.Forms.Label();
            this.velocityLabel = new System.Windows.Forms.Label();
            sensorGroupBox = new System.Windows.Forms.GroupBox();
            sensorGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // sensorGroupBox
            // 
            sensorGroupBox.Controls.Add(this.velocityLabel);
            sensorGroupBox.Controls.Add(this.altitudePositionLabel);
            sensorGroupBox.Controls.Add(this.longitudeLabel);
            sensorGroupBox.Controls.Add(this.latitudeLabel);
            sensorGroupBox.Controls.Add(this.calibrateMagnetButton);
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
            sensorGroupBox.Size = new System.Drawing.Size(410, 409);
            sensorGroupBox.TabIndex = 0;
            sensorGroupBox.TabStop = false;
            sensorGroupBox.Text = "Sensors";
            // 
            // calibrateMagnetButton
            // 
            this.calibrateMagnetButton.Location = new System.Drawing.Point(187, 48);
            this.calibrateMagnetButton.Name = "calibrateMagnetButton";
            this.calibrateMagnetButton.Size = new System.Drawing.Size(23, 23);
            this.calibrateMagnetButton.TabIndex = 28;
            this.calibrateMagnetButton.Text = "M";
            this.calibrateMagnetButton.UseVisualStyleBackColor = true;
            this.calibrateMagnetButton.Click += new System.EventHandler(this.calibrateMagnetButton_Click);
            // 
            // altitudeLabel
            // 
            this.altitudeLabel.AutoSize = true;
            this.altitudeLabel.Location = new System.Drawing.Point(5, 294);
            this.altitudeLabel.Name = "altitudeLabel";
            this.altitudeLabel.Size = new System.Drawing.Size(42, 13);
            this.altitudeLabel.TabIndex = 26;
            this.altitudeLabel.Text = "Altitude";
            // 
            // humidityLabel
            // 
            this.humidityLabel.AutoSize = true;
            this.humidityLabel.Location = new System.Drawing.Point(211, 281);
            this.humidityLabel.Name = "humidityLabel";
            this.humidityLabel.Size = new System.Drawing.Size(47, 13);
            this.humidityLabel.TabIndex = 25;
            this.humidityLabel.Text = "Humidity";
            // 
            // pressureLabel
            // 
            this.pressureLabel.AutoSize = true;
            this.pressureLabel.Location = new System.Drawing.Point(5, 281);
            this.pressureLabel.Name = "pressureLabel";
            this.pressureLabel.Size = new System.Drawing.Size(48, 13);
            this.pressureLabel.TabIndex = 24;
            this.pressureLabel.Text = "Pressure";
            // 
            // calibrationRunningText
            // 
            this.calibrationRunningText.AutoSize = true;
            this.calibrationRunningText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.calibrationRunningText.Location = new System.Drawing.Point(5, 254);
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
            this.headingIndicator.Location = new System.Drawing.Point(216, 19);
            this.headingIndicator.Name = "headingIndicator";
            this.headingIndicator.RotateAircraft = true;
            this.headingIndicator.Size = new System.Drawing.Size(175, 175);
            this.headingIndicator.TabIndex = 16;
            this.headingIndicator.Text = "headingIndicatorInstrumentControl1";
            // 
            // calibrateGyroButton
            // 
            this.calibrateGyroButton.Location = new System.Drawing.Point(187, 19);
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
            // altitudePositionLabel
            // 
            this.altitudePositionLabel.AutoSize = true;
            this.altitudePositionLabel.Location = new System.Drawing.Point(6, 335);
            this.altitudePositionLabel.Name = "altitudePositionLabel";
            this.altitudePositionLabel.Size = new System.Drawing.Size(42, 13);
            this.altitudePositionLabel.TabIndex = 31;
            this.altitudePositionLabel.Text = "Altitude";
            // 
            // longitudeLabel
            // 
            this.longitudeLabel.AutoSize = true;
            this.longitudeLabel.Location = new System.Drawing.Point(212, 322);
            this.longitudeLabel.Name = "longitudeLabel";
            this.longitudeLabel.Size = new System.Drawing.Size(54, 13);
            this.longitudeLabel.TabIndex = 30;
            this.longitudeLabel.Text = "Longitude";
            // 
            // latitudeLabel
            // 
            this.latitudeLabel.AutoSize = true;
            this.latitudeLabel.Location = new System.Drawing.Point(6, 322);
            this.latitudeLabel.Name = "latitudeLabel";
            this.latitudeLabel.Size = new System.Drawing.Size(45, 13);
            this.latitudeLabel.TabIndex = 29;
            this.latitudeLabel.Text = "Latitude";
            // 
            // velocityLabel
            // 
            this.velocityLabel.AutoSize = true;
            this.velocityLabel.Location = new System.Drawing.Point(211, 335);
            this.velocityLabel.Name = "velocityLabel";
            this.velocityLabel.Size = new System.Drawing.Size(44, 13);
            this.velocityLabel.TabIndex = 32;
            this.velocityLabel.Text = "Velocity";
            // 
            // SensorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(sensorGroupBox);
            this.Name = "SensorControl";
            this.Size = new System.Drawing.Size(410, 409);
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
        private System.Windows.Forms.Button calibrateMagnetButton;
        private System.Windows.Forms.Label altitudePositionLabel;
        private System.Windows.Forms.Label longitudeLabel;
        private System.Windows.Forms.Label latitudeLabel;
        private System.Windows.Forms.Label velocityLabel;
    }
}
