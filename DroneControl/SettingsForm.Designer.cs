namespace DroneControl
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label nameLabel;
            System.Windows.Forms.Label modelLabel;
            System.Windows.Forms.Label idLabel;
            System.Windows.Forms.GroupBox firmwareGroupBox;
            System.Windows.Forms.Label firmwareVersionLabel;
            System.Windows.Forms.Label buildDateLabel;
            System.Windows.Forms.TabPage quadrocopterPage;
            System.Windows.Forms.GroupBox hardwareGroupBox;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label magnetometerLabel;
            System.Windows.Forms.Label gyroSensorLabel;
            System.Windows.Forms.GroupBox motorsGroupBox;
            System.Windows.Forms.Label minValueLabel;
            System.Windows.Forms.Label maxValueLabel;
            System.Windows.Forms.Label idleValueLabel;
            System.Windows.Forms.GroupBox safetyGroupBox;
            System.Windows.Forms.Label safeMotorValueLabel;
            System.Windows.Forms.Label safeRollLabel;
            System.Windows.Forms.Label safeTemperatureLabel;
            System.Windows.Forms.Label safePitchLabel;
            System.Windows.Forms.GroupBox pidPitchGroupBox;
            System.Windows.Forms.Label pitchKpLabel;
            System.Windows.Forms.Label pitchKiLabel;
            System.Windows.Forms.Label pitchKdLabel;
            System.Windows.Forms.GroupBox pidRollGroupBox;
            System.Windows.Forms.Label rollKpLabel;
            System.Windows.Forms.Label rollKiLabel;
            System.Windows.Forms.Label rollKdLabel;
            System.Windows.Forms.GroupBox pidYawGroupBox;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.GroupBox groupBox3;
            System.Windows.Forms.Label label12;
            System.Windows.Forms.Label label13;
            System.Windows.Forms.Label label14;
            System.Windows.Forms.GroupBox groupBox4;
            System.Windows.Forms.Label label15;
            System.Windows.Forms.Label label16;
            System.Windows.Forms.Label label17;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.updateFirmwareButton = new System.Windows.Forms.Button();
            this.firmwareVersionTextBox = new System.Windows.Forms.TextBox();
            this.buildDateTextBox = new System.Windows.Forms.TextBox();
            this.saveConfigCheckBox = new System.Windows.Forms.CheckBox();
            this.baroSensorTextBox = new System.Windows.Forms.TextBox();
            this.restartButton = new System.Windows.Forms.Button();
            this.modelTextBox = new System.Windows.Forms.TextBox();
            this.magnetometerTextBox = new System.Windows.Forms.TextBox();
            this.gyroSensorTextBox = new System.Windows.Forms.TextBox();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.calibrateButton = new System.Windows.Forms.Button();
            this.minValueTextBox = new System.Windows.Forms.TextBox();
            this.idleValueTextBox = new System.Windows.Forms.TextBox();
            this.maxValueTextBox = new System.Windows.Forms.TextBox();
            this.safeMotorValueTextBox = new System.Windows.Forms.TextBox();
            this.safeRollTextBox = new System.Windows.Forms.TextBox();
            this.safePitchTextBox = new System.Windows.Forms.TextBox();
            this.safeTemperatureTextBox = new System.Windows.Forms.TextBox();
            this.pitchKdTextBox = new System.Windows.Forms.NumericUpDown();
            this.pitchKiTextBox = new System.Windows.Forms.NumericUpDown();
            this.pitchKpTextBox = new System.Windows.Forms.NumericUpDown();
            this.rollKdTextBox = new System.Windows.Forms.NumericUpDown();
            this.rollKiTextBox = new System.Windows.Forms.NumericUpDown();
            this.rollKpTextBox = new System.Windows.Forms.NumericUpDown();
            this.yawKdTextBox = new System.Windows.Forms.NumericUpDown();
            this.yawKiTextBox = new System.Windows.Forms.NumericUpDown();
            this.yawKpTextBox = new System.Windows.Forms.NumericUpDown();
            this.angleRollKdTextBox = new System.Windows.Forms.NumericUpDown();
            this.angleRollKiTextBox = new System.Windows.Forms.NumericUpDown();
            this.angleRollKpTextBox = new System.Windows.Forms.NumericUpDown();
            this.yawTrim = new System.Windows.Forms.NumericUpDown();
            this.pitchTrim = new System.Windows.Forms.NumericUpDown();
            this.rollTrim = new System.Windows.Forms.NumericUpDown();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.flyingPage = new System.Windows.Forms.TabPage();
            this.tuningButton = new System.Windows.Forms.Button();
            this.ignoreSafeOrientationCheckBox = new System.Windows.Forms.CheckBox();
            this.maxThrustForFlyingTextBox = new System.Windows.Forms.NumericUpDown();
            this.enableStabilizationCheckBox = new System.Windows.Forms.CheckBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.revertButton = new System.Windows.Forms.Button();
            this.anglePitchKdTextBox = new System.Windows.Forms.NumericUpDown();
            this.anglePitchKiTextBox = new System.Windows.Forms.NumericUpDown();
            this.anglePitchKpTextBox = new System.Windows.Forms.NumericUpDown();
            this.angleYawKdTextBox = new System.Windows.Forms.NumericUpDown();
            this.angleYawKiTextBox = new System.Windows.Forms.NumericUpDown();
            this.angleYawKpTextBox = new System.Windows.Forms.NumericUpDown();
            nameLabel = new System.Windows.Forms.Label();
            modelLabel = new System.Windows.Forms.Label();
            idLabel = new System.Windows.Forms.Label();
            firmwareGroupBox = new System.Windows.Forms.GroupBox();
            firmwareVersionLabel = new System.Windows.Forms.Label();
            buildDateLabel = new System.Windows.Forms.Label();
            quadrocopterPage = new System.Windows.Forms.TabPage();
            hardwareGroupBox = new System.Windows.Forms.GroupBox();
            label8 = new System.Windows.Forms.Label();
            magnetometerLabel = new System.Windows.Forms.Label();
            gyroSensorLabel = new System.Windows.Forms.Label();
            motorsGroupBox = new System.Windows.Forms.GroupBox();
            minValueLabel = new System.Windows.Forms.Label();
            maxValueLabel = new System.Windows.Forms.Label();
            idleValueLabel = new System.Windows.Forms.Label();
            safetyGroupBox = new System.Windows.Forms.GroupBox();
            safeMotorValueLabel = new System.Windows.Forms.Label();
            safeRollLabel = new System.Windows.Forms.Label();
            safeTemperatureLabel = new System.Windows.Forms.Label();
            safePitchLabel = new System.Windows.Forms.Label();
            pidPitchGroupBox = new System.Windows.Forms.GroupBox();
            pitchKpLabel = new System.Windows.Forms.Label();
            pitchKiLabel = new System.Windows.Forms.Label();
            pitchKdLabel = new System.Windows.Forms.Label();
            pidRollGroupBox = new System.Windows.Forms.GroupBox();
            rollKpLabel = new System.Windows.Forms.Label();
            rollKiLabel = new System.Windows.Forms.Label();
            rollKdLabel = new System.Windows.Forms.Label();
            pidYawGroupBox = new System.Windows.Forms.GroupBox();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            label9 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            groupBox3 = new System.Windows.Forms.GroupBox();
            label12 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            groupBox4 = new System.Windows.Forms.GroupBox();
            label15 = new System.Windows.Forms.Label();
            label16 = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            firmwareGroupBox.SuspendLayout();
            quadrocopterPage.SuspendLayout();
            hardwareGroupBox.SuspendLayout();
            motorsGroupBox.SuspendLayout();
            safetyGroupBox.SuspendLayout();
            pidPitchGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pitchKdTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pitchKiTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pitchKpTextBox)).BeginInit();
            pidRollGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rollKdTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rollKiTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rollKpTextBox)).BeginInit();
            pidYawGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yawKdTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yawKiTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yawKpTextBox)).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.angleRollKdTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleRollKiTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleRollKpTextBox)).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yawTrim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pitchTrim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rollTrim)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.flyingPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxThrustForFlyingTextBox)).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.anglePitchKdTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.anglePitchKiTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.anglePitchKpTextBox)).BeginInit();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.angleYawKdTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleYawKiTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleYawKpTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new System.Drawing.Point(20, 10);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new System.Drawing.Size(35, 13);
            nameLabel.TabIndex = 0;
            nameLabel.Text = "Name";
            // 
            // modelLabel
            // 
            modelLabel.AutoSize = true;
            modelLabel.Location = new System.Drawing.Point(8, 22);
            modelLabel.Name = "modelLabel";
            modelLabel.Size = new System.Drawing.Size(36, 13);
            modelLabel.TabIndex = 2;
            modelLabel.Text = "Model";
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new System.Drawing.Point(8, 48);
            idLabel.Name = "idLabel";
            idLabel.Size = new System.Drawing.Size(18, 13);
            idLabel.TabIndex = 8;
            idLabel.Text = "ID";
            // 
            // firmwareGroupBox
            // 
            firmwareGroupBox.Controls.Add(this.updateFirmwareButton);
            firmwareGroupBox.Controls.Add(firmwareVersionLabel);
            firmwareGroupBox.Controls.Add(this.firmwareVersionTextBox);
            firmwareGroupBox.Controls.Add(buildDateLabel);
            firmwareGroupBox.Controls.Add(this.buildDateTextBox);
            firmwareGroupBox.Location = new System.Drawing.Point(11, 33);
            firmwareGroupBox.Name = "firmwareGroupBox";
            firmwareGroupBox.Size = new System.Drawing.Size(258, 108);
            firmwareGroupBox.TabIndex = 11;
            firmwareGroupBox.TabStop = false;
            firmwareGroupBox.Text = "Firmware";
            // 
            // updateFirmwareButton
            // 
            this.updateFirmwareButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.updateFirmwareButton.Location = new System.Drawing.Point(144, 82);
            this.updateFirmwareButton.Name = "updateFirmwareButton";
            this.updateFirmwareButton.Size = new System.Drawing.Size(108, 20);
            this.updateFirmwareButton.TabIndex = 10;
            this.updateFirmwareButton.Text = "Update Firmware";
            this.updateFirmwareButton.UseVisualStyleBackColor = true;
            this.updateFirmwareButton.Click += new System.EventHandler(this.updateFirmwareButton_Click);
            // 
            // firmwareVersionLabel
            // 
            firmwareVersionLabel.AutoSize = true;
            firmwareVersionLabel.Location = new System.Drawing.Point(9, 18);
            firmwareVersionLabel.Name = "firmwareVersionLabel";
            firmwareVersionLabel.Size = new System.Drawing.Size(87, 13);
            firmwareVersionLabel.TabIndex = 4;
            firmwareVersionLabel.Text = "Firmware Version";
            // 
            // firmwareVersionTextBox
            // 
            this.firmwareVersionTextBox.Location = new System.Drawing.Point(110, 15);
            this.firmwareVersionTextBox.Name = "firmwareVersionTextBox";
            this.firmwareVersionTextBox.Size = new System.Drawing.Size(142, 20);
            this.firmwareVersionTextBox.TabIndex = 5;
            // 
            // buildDateLabel
            // 
            buildDateLabel.AutoSize = true;
            buildDateLabel.Location = new System.Drawing.Point(9, 44);
            buildDateLabel.Name = "buildDateLabel";
            buildDateLabel.Size = new System.Drawing.Size(56, 13);
            buildDateLabel.TabIndex = 6;
            buildDateLabel.Text = "Build Date";
            // 
            // buildDateTextBox
            // 
            this.buildDateTextBox.Location = new System.Drawing.Point(110, 41);
            this.buildDateTextBox.Name = "buildDateTextBox";
            this.buildDateTextBox.Size = new System.Drawing.Size(142, 20);
            this.buildDateTextBox.TabIndex = 7;
            // 
            // quadrocopterPage
            // 
            quadrocopterPage.Controls.Add(this.saveConfigCheckBox);
            quadrocopterPage.Controls.Add(hardwareGroupBox);
            quadrocopterPage.Controls.Add(firmwareGroupBox);
            quadrocopterPage.Controls.Add(this.nameTextBox);
            quadrocopterPage.Controls.Add(nameLabel);
            quadrocopterPage.Location = new System.Drawing.Point(4, 22);
            quadrocopterPage.Name = "quadrocopterPage";
            quadrocopterPage.Padding = new System.Windows.Forms.Padding(3);
            quadrocopterPage.Size = new System.Drawing.Size(698, 412);
            quadrocopterPage.TabIndex = 0;
            quadrocopterPage.Text = "Quadrocopter";
            // 
            // saveConfigCheckBox
            // 
            this.saveConfigCheckBox.AutoSize = true;
            this.saveConfigCheckBox.Location = new System.Drawing.Point(302, 10);
            this.saveConfigCheckBox.Name = "saveConfigCheckBox";
            this.saveConfigCheckBox.Size = new System.Drawing.Size(84, 17);
            this.saveConfigCheckBox.TabIndex = 17;
            this.saveConfigCheckBox.Text = "Save Config";
            this.saveConfigCheckBox.UseVisualStyleBackColor = true;
            // 
            // hardwareGroupBox
            // 
            hardwareGroupBox.Controls.Add(this.baroSensorTextBox);
            hardwareGroupBox.Controls.Add(label8);
            hardwareGroupBox.Controls.Add(this.restartButton);
            hardwareGroupBox.Controls.Add(this.modelTextBox);
            hardwareGroupBox.Controls.Add(this.magnetometerTextBox);
            hardwareGroupBox.Controls.Add(modelLabel);
            hardwareGroupBox.Controls.Add(magnetometerLabel);
            hardwareGroupBox.Controls.Add(idLabel);
            hardwareGroupBox.Controls.Add(this.gyroSensorTextBox);
            hardwareGroupBox.Controls.Add(this.idTextBox);
            hardwareGroupBox.Controls.Add(gyroSensorLabel);
            hardwareGroupBox.Location = new System.Drawing.Point(11, 156);
            hardwareGroupBox.Name = "hardwareGroupBox";
            hardwareGroupBox.Size = new System.Drawing.Size(258, 189);
            hardwareGroupBox.TabIndex = 16;
            hardwareGroupBox.TabStop = false;
            hardwareGroupBox.Text = "Hardware";
            // 
            // baroSensorTextBox
            // 
            this.baroSensorTextBox.Location = new System.Drawing.Point(110, 124);
            this.baroSensorTextBox.Name = "baroSensorTextBox";
            this.baroSensorTextBox.Size = new System.Drawing.Size(142, 20);
            this.baroSensorTextBox.TabIndex = 17;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(9, 127);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(65, 13);
            label8.TabIndex = 16;
            label8.Text = "Baro Sensor";
            // 
            // restartButton
            // 
            this.restartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.restartButton.Location = new System.Drawing.Point(144, 163);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(108, 20);
            this.restartButton.TabIndex = 11;
            this.restartButton.Text = "Restart";
            this.restartButton.UseVisualStyleBackColor = true;
            this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
            // 
            // modelTextBox
            // 
            this.modelTextBox.Location = new System.Drawing.Point(109, 19);
            this.modelTextBox.Name = "modelTextBox";
            this.modelTextBox.Size = new System.Drawing.Size(142, 20);
            this.modelTextBox.TabIndex = 3;
            // 
            // magnetometerTextBox
            // 
            this.magnetometerTextBox.Location = new System.Drawing.Point(109, 98);
            this.magnetometerTextBox.Name = "magnetometerTextBox";
            this.magnetometerTextBox.Size = new System.Drawing.Size(142, 20);
            this.magnetometerTextBox.TabIndex = 15;
            // 
            // magnetometerLabel
            // 
            magnetometerLabel.AutoSize = true;
            magnetometerLabel.Location = new System.Drawing.Point(8, 101);
            magnetometerLabel.Name = "magnetometerLabel";
            magnetometerLabel.Size = new System.Drawing.Size(75, 13);
            magnetometerLabel.TabIndex = 14;
            magnetometerLabel.Text = "Magnetometer";
            // 
            // gyroSensorTextBox
            // 
            this.gyroSensorTextBox.Location = new System.Drawing.Point(109, 71);
            this.gyroSensorTextBox.Name = "gyroSensorTextBox";
            this.gyroSensorTextBox.Size = new System.Drawing.Size(142, 20);
            this.gyroSensorTextBox.TabIndex = 13;
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(109, 45);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(142, 20);
            this.idTextBox.TabIndex = 9;
            // 
            // gyroSensorLabel
            // 
            gyroSensorLabel.AutoSize = true;
            gyroSensorLabel.Location = new System.Drawing.Point(8, 74);
            gyroSensorLabel.Name = "gyroSensorLabel";
            gyroSensorLabel.Size = new System.Drawing.Size(65, 13);
            gyroSensorLabel.TabIndex = 12;
            gyroSensorLabel.Text = "Gyro Sensor";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(121, 7);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(142, 20);
            this.nameTextBox.TabIndex = 1;
            // 
            // motorsGroupBox
            // 
            motorsGroupBox.Controls.Add(this.calibrateButton);
            motorsGroupBox.Controls.Add(this.minValueTextBox);
            motorsGroupBox.Controls.Add(minValueLabel);
            motorsGroupBox.Controls.Add(maxValueLabel);
            motorsGroupBox.Controls.Add(this.idleValueTextBox);
            motorsGroupBox.Controls.Add(this.maxValueTextBox);
            motorsGroupBox.Controls.Add(idleValueLabel);
            motorsGroupBox.Location = new System.Drawing.Point(8, 6);
            motorsGroupBox.Name = "motorsGroupBox";
            motorsGroupBox.Size = new System.Drawing.Size(258, 124);
            motorsGroupBox.TabIndex = 17;
            motorsGroupBox.TabStop = false;
            motorsGroupBox.Text = "Motors";
            // 
            // calibrateButton
            // 
            this.calibrateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.calibrateButton.Location = new System.Drawing.Point(143, 98);
            this.calibrateButton.Name = "calibrateButton";
            this.calibrateButton.Size = new System.Drawing.Size(108, 20);
            this.calibrateButton.TabIndex = 11;
            this.calibrateButton.Text = "Calibrate";
            this.calibrateButton.UseVisualStyleBackColor = true;
            this.calibrateButton.Click += new System.EventHandler(this.calibrateButton_Click);
            // 
            // minValueTextBox
            // 
            this.minValueTextBox.Location = new System.Drawing.Point(109, 19);
            this.minValueTextBox.Name = "minValueTextBox";
            this.minValueTextBox.Size = new System.Drawing.Size(142, 20);
            this.minValueTextBox.TabIndex = 3;
            // 
            // minValueLabel
            // 
            minValueLabel.AutoSize = true;
            minValueLabel.Location = new System.Drawing.Point(6, 22);
            minValueLabel.Name = "minValueLabel";
            minValueLabel.Size = new System.Drawing.Size(54, 13);
            minValueLabel.TabIndex = 2;
            minValueLabel.Text = "Min Value";
            // 
            // maxValueLabel
            // 
            maxValueLabel.AutoSize = true;
            maxValueLabel.Location = new System.Drawing.Point(6, 77);
            maxValueLabel.Name = "maxValueLabel";
            maxValueLabel.Size = new System.Drawing.Size(57, 13);
            maxValueLabel.TabIndex = 8;
            maxValueLabel.Text = "Max Value";
            // 
            // idleValueTextBox
            // 
            this.idleValueTextBox.Location = new System.Drawing.Point(109, 45);
            this.idleValueTextBox.Name = "idleValueTextBox";
            this.idleValueTextBox.Size = new System.Drawing.Size(143, 20);
            this.idleValueTextBox.TabIndex = 13;
            // 
            // maxValueTextBox
            // 
            this.maxValueTextBox.Location = new System.Drawing.Point(109, 74);
            this.maxValueTextBox.Name = "maxValueTextBox";
            this.maxValueTextBox.Size = new System.Drawing.Size(143, 20);
            this.maxValueTextBox.TabIndex = 9;
            // 
            // idleValueLabel
            // 
            idleValueLabel.AutoSize = true;
            idleValueLabel.Location = new System.Drawing.Point(6, 48);
            idleValueLabel.Name = "idleValueLabel";
            idleValueLabel.Size = new System.Drawing.Size(54, 13);
            idleValueLabel.TabIndex = 12;
            idleValueLabel.Text = "Idle Value";
            // 
            // safetyGroupBox
            // 
            safetyGroupBox.Controls.Add(this.safeMotorValueTextBox);
            safetyGroupBox.Controls.Add(this.safeRollTextBox);
            safetyGroupBox.Controls.Add(safeMotorValueLabel);
            safetyGroupBox.Controls.Add(this.ignoreSafeOrientationCheckBox);
            safetyGroupBox.Controls.Add(safeRollLabel);
            safetyGroupBox.Controls.Add(safeTemperatureLabel);
            safetyGroupBox.Controls.Add(this.safePitchTextBox);
            safetyGroupBox.Controls.Add(this.safeTemperatureTextBox);
            safetyGroupBox.Controls.Add(safePitchLabel);
            safetyGroupBox.Location = new System.Drawing.Point(8, 136);
            safetyGroupBox.Name = "safetyGroupBox";
            safetyGroupBox.Size = new System.Drawing.Size(258, 150);
            safetyGroupBox.TabIndex = 18;
            safetyGroupBox.TabStop = false;
            safetyGroupBox.Text = "Safety";
            // 
            // safeMotorValueTextBox
            // 
            this.safeMotorValueTextBox.Location = new System.Drawing.Point(109, 19);
            this.safeMotorValueTextBox.Name = "safeMotorValueTextBox";
            this.safeMotorValueTextBox.Size = new System.Drawing.Size(142, 20);
            this.safeMotorValueTextBox.TabIndex = 3;
            // 
            // safeRollTextBox
            // 
            this.safeRollTextBox.Location = new System.Drawing.Point(109, 92);
            this.safeRollTextBox.Name = "safeRollTextBox";
            this.safeRollTextBox.Size = new System.Drawing.Size(142, 20);
            this.safeRollTextBox.TabIndex = 15;
            // 
            // safeMotorValueLabel
            // 
            safeMotorValueLabel.AutoSize = true;
            safeMotorValueLabel.Location = new System.Drawing.Point(8, 22);
            safeMotorValueLabel.Name = "safeMotorValueLabel";
            safeMotorValueLabel.Size = new System.Drawing.Size(89, 13);
            safeMotorValueLabel.TabIndex = 2;
            safeMotorValueLabel.Text = "Safe Motor Value";
            // 
            // safeRollLabel
            // 
            safeRollLabel.AutoSize = true;
            safeRollLabel.Location = new System.Drawing.Point(8, 95);
            safeRollLabel.Name = "safeRollLabel";
            safeRollLabel.Size = new System.Drawing.Size(50, 13);
            safeRollLabel.TabIndex = 14;
            safeRollLabel.Text = "Safe Roll";
            // 
            // safeTemperatureLabel
            // 
            safeTemperatureLabel.AutoSize = true;
            safeTemperatureLabel.Location = new System.Drawing.Point(8, 48);
            safeTemperatureLabel.Name = "safeTemperatureLabel";
            safeTemperatureLabel.Size = new System.Drawing.Size(92, 13);
            safeTemperatureLabel.TabIndex = 8;
            safeTemperatureLabel.Text = "Safe Temperature";
            // 
            // safePitchTextBox
            // 
            this.safePitchTextBox.Location = new System.Drawing.Point(109, 119);
            this.safePitchTextBox.Name = "safePitchTextBox";
            this.safePitchTextBox.Size = new System.Drawing.Size(142, 20);
            this.safePitchTextBox.TabIndex = 13;
            // 
            // safeTemperatureTextBox
            // 
            this.safeTemperatureTextBox.Location = new System.Drawing.Point(109, 45);
            this.safeTemperatureTextBox.Name = "safeTemperatureTextBox";
            this.safeTemperatureTextBox.Size = new System.Drawing.Size(142, 20);
            this.safeTemperatureTextBox.TabIndex = 9;
            // 
            // safePitchLabel
            // 
            safePitchLabel.AutoSize = true;
            safePitchLabel.Location = new System.Drawing.Point(7, 122);
            safePitchLabel.Name = "safePitchLabel";
            safePitchLabel.Size = new System.Drawing.Size(56, 13);
            safePitchLabel.TabIndex = 12;
            safePitchLabel.Text = "Safe Pitch";
            // 
            // pidPitchGroupBox
            // 
            pidPitchGroupBox.Controls.Add(this.pitchKdTextBox);
            pidPitchGroupBox.Controls.Add(this.pitchKiTextBox);
            pidPitchGroupBox.Controls.Add(this.pitchKpTextBox);
            pidPitchGroupBox.Controls.Add(pitchKpLabel);
            pidPitchGroupBox.Controls.Add(pitchKiLabel);
            pidPitchGroupBox.Controls.Add(pitchKdLabel);
            pidPitchGroupBox.Location = new System.Drawing.Point(395, 6);
            pidPitchGroupBox.Name = "pidPitchGroupBox";
            pidPitchGroupBox.Size = new System.Drawing.Size(117, 101);
            pidPitchGroupBox.TabIndex = 19;
            pidPitchGroupBox.TabStop = false;
            pidPitchGroupBox.Text = "PID Pitch";
            // 
            // pitchKdTextBox
            // 
            this.pitchKdTextBox.DecimalPlaces = 5;
            this.pitchKdTextBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.pitchKdTextBox.Location = new System.Drawing.Point(34, 72);
            this.pitchKdTextBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.pitchKdTextBox.Name = "pitchKdTextBox";
            this.pitchKdTextBox.Size = new System.Drawing.Size(70, 20);
            this.pitchKdTextBox.TabIndex = 15;
            // 
            // pitchKiTextBox
            // 
            this.pitchKiTextBox.DecimalPlaces = 5;
            this.pitchKiTextBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.pitchKiTextBox.Location = new System.Drawing.Point(34, 46);
            this.pitchKiTextBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.pitchKiTextBox.Name = "pitchKiTextBox";
            this.pitchKiTextBox.Size = new System.Drawing.Size(70, 20);
            this.pitchKiTextBox.TabIndex = 14;
            // 
            // pitchKpTextBox
            // 
            this.pitchKpTextBox.DecimalPlaces = 2;
            this.pitchKpTextBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.pitchKpTextBox.Location = new System.Drawing.Point(34, 18);
            this.pitchKpTextBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.pitchKpTextBox.Name = "pitchKpTextBox";
            this.pitchKpTextBox.Size = new System.Drawing.Size(70, 20);
            this.pitchKpTextBox.TabIndex = 13;
            // 
            // pitchKpLabel
            // 
            pitchKpLabel.AutoSize = true;
            pitchKpLabel.Location = new System.Drawing.Point(8, 22);
            pitchKpLabel.Name = "pitchKpLabel";
            pitchKpLabel.Size = new System.Drawing.Size(20, 13);
            pitchKpLabel.TabIndex = 2;
            pitchKpLabel.Text = "Kp";
            // 
            // pitchKiLabel
            // 
            pitchKiLabel.AutoSize = true;
            pitchKiLabel.Location = new System.Drawing.Point(8, 48);
            pitchKiLabel.Name = "pitchKiLabel";
            pitchKiLabel.Size = new System.Drawing.Size(16, 13);
            pitchKiLabel.TabIndex = 8;
            pitchKiLabel.Text = "Ki";
            // 
            // pitchKdLabel
            // 
            pitchKdLabel.AutoSize = true;
            pitchKdLabel.Location = new System.Drawing.Point(8, 74);
            pitchKdLabel.Name = "pitchKdLabel";
            pitchKdLabel.Size = new System.Drawing.Size(20, 13);
            pitchKdLabel.TabIndex = 12;
            pitchKdLabel.Text = "Kd";
            // 
            // pidRollGroupBox
            // 
            pidRollGroupBox.Controls.Add(this.rollKdTextBox);
            pidRollGroupBox.Controls.Add(this.rollKiTextBox);
            pidRollGroupBox.Controls.Add(this.rollKpTextBox);
            pidRollGroupBox.Controls.Add(rollKpLabel);
            pidRollGroupBox.Controls.Add(rollKiLabel);
            pidRollGroupBox.Controls.Add(rollKdLabel);
            pidRollGroupBox.Location = new System.Drawing.Point(272, 6);
            pidRollGroupBox.Name = "pidRollGroupBox";
            pidRollGroupBox.Size = new System.Drawing.Size(117, 101);
            pidRollGroupBox.TabIndex = 20;
            pidRollGroupBox.TabStop = false;
            pidRollGroupBox.Text = "PID Roll";
            // 
            // rollKdTextBox
            // 
            this.rollKdTextBox.DecimalPlaces = 5;
            this.rollKdTextBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.rollKdTextBox.Location = new System.Drawing.Point(34, 72);
            this.rollKdTextBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.rollKdTextBox.Name = "rollKdTextBox";
            this.rollKdTextBox.Size = new System.Drawing.Size(70, 20);
            this.rollKdTextBox.TabIndex = 15;
            // 
            // rollKiTextBox
            // 
            this.rollKiTextBox.DecimalPlaces = 5;
            this.rollKiTextBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.rollKiTextBox.Location = new System.Drawing.Point(34, 46);
            this.rollKiTextBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.rollKiTextBox.Name = "rollKiTextBox";
            this.rollKiTextBox.Size = new System.Drawing.Size(70, 20);
            this.rollKiTextBox.TabIndex = 14;
            // 
            // rollKpTextBox
            // 
            this.rollKpTextBox.DecimalPlaces = 2;
            this.rollKpTextBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.rollKpTextBox.Location = new System.Drawing.Point(34, 18);
            this.rollKpTextBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.rollKpTextBox.Name = "rollKpTextBox";
            this.rollKpTextBox.Size = new System.Drawing.Size(70, 20);
            this.rollKpTextBox.TabIndex = 13;
            // 
            // rollKpLabel
            // 
            rollKpLabel.AutoSize = true;
            rollKpLabel.Location = new System.Drawing.Point(8, 22);
            rollKpLabel.Name = "rollKpLabel";
            rollKpLabel.Size = new System.Drawing.Size(20, 13);
            rollKpLabel.TabIndex = 2;
            rollKpLabel.Text = "Kp";
            // 
            // rollKiLabel
            // 
            rollKiLabel.AutoSize = true;
            rollKiLabel.Location = new System.Drawing.Point(8, 48);
            rollKiLabel.Name = "rollKiLabel";
            rollKiLabel.Size = new System.Drawing.Size(16, 13);
            rollKiLabel.TabIndex = 8;
            rollKiLabel.Text = "Ki";
            // 
            // rollKdLabel
            // 
            rollKdLabel.AutoSize = true;
            rollKdLabel.Location = new System.Drawing.Point(8, 74);
            rollKdLabel.Name = "rollKdLabel";
            rollKdLabel.Size = new System.Drawing.Size(20, 13);
            rollKdLabel.TabIndex = 12;
            rollKdLabel.Text = "Kd";
            // 
            // pidYawGroupBox
            // 
            pidYawGroupBox.Controls.Add(this.yawKdTextBox);
            pidYawGroupBox.Controls.Add(this.yawKiTextBox);
            pidYawGroupBox.Controls.Add(this.yawKpTextBox);
            pidYawGroupBox.Controls.Add(label4);
            pidYawGroupBox.Controls.Add(label5);
            pidYawGroupBox.Controls.Add(label6);
            pidYawGroupBox.Location = new System.Drawing.Point(518, 6);
            pidYawGroupBox.Name = "pidYawGroupBox";
            pidYawGroupBox.Size = new System.Drawing.Size(117, 101);
            pidYawGroupBox.TabIndex = 21;
            pidYawGroupBox.TabStop = false;
            pidYawGroupBox.Text = "PID Yaw";
            // 
            // yawKdTextBox
            // 
            this.yawKdTextBox.DecimalPlaces = 5;
            this.yawKdTextBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.yawKdTextBox.Location = new System.Drawing.Point(34, 72);
            this.yawKdTextBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.yawKdTextBox.Name = "yawKdTextBox";
            this.yawKdTextBox.Size = new System.Drawing.Size(70, 20);
            this.yawKdTextBox.TabIndex = 15;
            // 
            // yawKiTextBox
            // 
            this.yawKiTextBox.DecimalPlaces = 5;
            this.yawKiTextBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.yawKiTextBox.Location = new System.Drawing.Point(34, 46);
            this.yawKiTextBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.yawKiTextBox.Name = "yawKiTextBox";
            this.yawKiTextBox.Size = new System.Drawing.Size(70, 20);
            this.yawKiTextBox.TabIndex = 14;
            // 
            // yawKpTextBox
            // 
            this.yawKpTextBox.DecimalPlaces = 2;
            this.yawKpTextBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.yawKpTextBox.Location = new System.Drawing.Point(34, 18);
            this.yawKpTextBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.yawKpTextBox.Name = "yawKpTextBox";
            this.yawKpTextBox.Size = new System.Drawing.Size(70, 20);
            this.yawKpTextBox.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(8, 22);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(20, 13);
            label4.TabIndex = 2;
            label4.Text = "Kp";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(8, 48);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(16, 13);
            label5.TabIndex = 8;
            label5.Text = "Ki";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(8, 74);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(20, 13);
            label6.TabIndex = 12;
            label6.Text = "Kd";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 294);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(90, 13);
            label1.TabIndex = 25;
            label1.Text = "Max Thrust Flying";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.angleRollKdTextBox);
            groupBox1.Controls.Add(this.angleRollKiTextBox);
            groupBox1.Controls.Add(this.angleRollKpTextBox);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label7);
            groupBox1.Location = new System.Drawing.Point(273, 219);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(117, 101);
            groupBox1.TabIndex = 22;
            groupBox1.TabStop = false;
            groupBox1.Text = "PID Angle Roll";
            // 
            // angleRollKdTextBox
            // 
            this.angleRollKdTextBox.DecimalPlaces = 2;
            this.angleRollKdTextBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.angleRollKdTextBox.Location = new System.Drawing.Point(34, 72);
            this.angleRollKdTextBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.angleRollKdTextBox.Name = "angleRollKdTextBox";
            this.angleRollKdTextBox.Size = new System.Drawing.Size(70, 20);
            this.angleRollKdTextBox.TabIndex = 15;
            // 
            // angleRollKiTextBox
            // 
            this.angleRollKiTextBox.DecimalPlaces = 2;
            this.angleRollKiTextBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.angleRollKiTextBox.Location = new System.Drawing.Point(34, 46);
            this.angleRollKiTextBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.angleRollKiTextBox.Name = "angleRollKiTextBox";
            this.angleRollKiTextBox.Size = new System.Drawing.Size(70, 20);
            this.angleRollKiTextBox.TabIndex = 14;
            // 
            // angleRollKpTextBox
            // 
            this.angleRollKpTextBox.DecimalPlaces = 2;
            this.angleRollKpTextBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.angleRollKpTextBox.Location = new System.Drawing.Point(34, 18);
            this.angleRollKpTextBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.angleRollKpTextBox.Name = "angleRollKpTextBox";
            this.angleRollKpTextBox.Size = new System.Drawing.Size(70, 20);
            this.angleRollKpTextBox.TabIndex = 13;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(8, 22);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(20, 13);
            label2.TabIndex = 2;
            label2.Text = "Kp";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(8, 48);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(16, 13);
            label3.TabIndex = 8;
            label3.Text = "Ki";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(8, 74);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(20, 13);
            label7.TabIndex = 12;
            label7.Text = "Kd";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.yawTrim);
            groupBox2.Controls.Add(this.pitchTrim);
            groupBox2.Controls.Add(this.rollTrim);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(label11);
            groupBox2.Location = new System.Drawing.Point(518, 113);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(117, 101);
            groupBox2.TabIndex = 23;
            groupBox2.TabStop = false;
            groupBox2.Text = "Acc Trim";
            // 
            // yawTrim
            // 
            this.yawTrim.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.yawTrim.Location = new System.Drawing.Point(34, 72);
            this.yawTrim.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.yawTrim.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.yawTrim.Name = "yawTrim";
            this.yawTrim.Size = new System.Drawing.Size(70, 20);
            this.yawTrim.TabIndex = 15;
            // 
            // pitchTrim
            // 
            this.pitchTrim.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.pitchTrim.Location = new System.Drawing.Point(34, 46);
            this.pitchTrim.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.pitchTrim.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.pitchTrim.Name = "pitchTrim";
            this.pitchTrim.Size = new System.Drawing.Size(70, 20);
            this.pitchTrim.TabIndex = 14;
            // 
            // rollTrim
            // 
            this.rollTrim.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.rollTrim.Location = new System.Drawing.Point(34, 18);
            this.rollTrim.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.rollTrim.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.rollTrim.Name = "rollTrim";
            this.rollTrim.Size = new System.Drawing.Size(70, 20);
            this.rollTrim.TabIndex = 13;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(6, 22);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(25, 13);
            label9.TabIndex = 2;
            label9.Text = "Roll";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(5, 48);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(31, 13);
            label10.TabIndex = 8;
            label10.Text = "Pitch";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(5, 74);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(28, 13);
            label11.TabIndex = 12;
            label11.Text = "Yaw";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(quadrocopterPage);
            this.tabControl1.Controls.Add(this.flyingPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(706, 438);
            this.tabControl1.TabIndex = 0;
            // 
            // flyingPage
            // 
            this.flyingPage.Controls.Add(groupBox4);
            this.flyingPage.Controls.Add(groupBox3);
            this.flyingPage.Controls.Add(groupBox2);
            this.flyingPage.Controls.Add(this.tuningButton);
            this.flyingPage.Controls.Add(groupBox1);
            this.flyingPage.Controls.Add(this.maxThrustForFlyingTextBox);
            this.flyingPage.Controls.Add(label1);
            this.flyingPage.Controls.Add(this.enableStabilizationCheckBox);
            this.flyingPage.Controls.Add(pidYawGroupBox);
            this.flyingPage.Controls.Add(pidRollGroupBox);
            this.flyingPage.Controls.Add(pidPitchGroupBox);
            this.flyingPage.Controls.Add(safetyGroupBox);
            this.flyingPage.Controls.Add(motorsGroupBox);
            this.flyingPage.Location = new System.Drawing.Point(4, 22);
            this.flyingPage.Name = "flyingPage";
            this.flyingPage.Padding = new System.Windows.Forms.Padding(3);
            this.flyingPage.Size = new System.Drawing.Size(698, 412);
            this.flyingPage.TabIndex = 1;
            this.flyingPage.Text = "Flying";
            // 
            // tuningButton
            // 
            this.tuningButton.Location = new System.Drawing.Point(272, 113);
            this.tuningButton.Name = "tuningButton";
            this.tuningButton.Size = new System.Drawing.Size(75, 23);
            this.tuningButton.TabIndex = 28;
            this.tuningButton.Text = "Tuning";
            this.tuningButton.UseVisualStyleBackColor = true;
            this.tuningButton.Click += new System.EventHandler(this.tuningButton_Click);
            // 
            // ignoreSafeOrientationCheckBox
            // 
            this.ignoreSafeOrientationCheckBox.AutoSize = true;
            this.ignoreSafeOrientationCheckBox.Location = new System.Drawing.Point(11, 69);
            this.ignoreSafeOrientationCheckBox.Name = "ignoreSafeOrientationCheckBox";
            this.ignoreSafeOrientationCheckBox.Size = new System.Drawing.Size(185, 17);
            this.ignoreSafeOrientationCheckBox.TabIndex = 27;
            this.ignoreSafeOrientationCheckBox.Text = "Ignore safe orientation while flying";
            this.ignoreSafeOrientationCheckBox.UseVisualStyleBackColor = true;
            // 
            // maxThrustForFlyingTextBox
            // 
            this.maxThrustForFlyingTextBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.maxThrustForFlyingTextBox.Location = new System.Drawing.Point(117, 292);
            this.maxThrustForFlyingTextBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.maxThrustForFlyingTextBox.Name = "maxThrustForFlyingTextBox";
            this.maxThrustForFlyingTextBox.Size = new System.Drawing.Size(70, 20);
            this.maxThrustForFlyingTextBox.TabIndex = 26;
            // 
            // enableStabilizationCheckBox
            // 
            this.enableStabilizationCheckBox.AutoSize = true;
            this.enableStabilizationCheckBox.Location = new System.Drawing.Point(273, 196);
            this.enableStabilizationCheckBox.Name = "enableStabilizationCheckBox";
            this.enableStabilizationCheckBox.Size = new System.Drawing.Size(116, 17);
            this.enableStabilizationCheckBox.TabIndex = 22;
            this.enableStabilizationCheckBox.Text = "Enable stabilization";
            this.enableStabilizationCheckBox.UseVisualStyleBackColor = true;
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(645, -1);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(62, 25);
            this.applyButton.TabIndex = 1;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // revertButton
            // 
            this.revertButton.Location = new System.Drawing.Point(577, -1);
            this.revertButton.Name = "revertButton";
            this.revertButton.Size = new System.Drawing.Size(62, 25);
            this.revertButton.TabIndex = 2;
            this.revertButton.Text = "Revert";
            this.revertButton.UseVisualStyleBackColor = true;
            this.revertButton.Click += new System.EventHandler(this.revertButton_Click);
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(this.anglePitchKdTextBox);
            groupBox3.Controls.Add(this.anglePitchKiTextBox);
            groupBox3.Controls.Add(this.anglePitchKpTextBox);
            groupBox3.Controls.Add(label12);
            groupBox3.Controls.Add(label13);
            groupBox3.Controls.Add(label14);
            groupBox3.Location = new System.Drawing.Point(395, 219);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(117, 101);
            groupBox3.TabIndex = 23;
            groupBox3.TabStop = false;
            groupBox3.Text = "PID Angle Pitch";
            // 
            // anglePitchKdTextBox
            // 
            this.anglePitchKdTextBox.DecimalPlaces = 2;
            this.anglePitchKdTextBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.anglePitchKdTextBox.Location = new System.Drawing.Point(34, 72);
            this.anglePitchKdTextBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.anglePitchKdTextBox.Name = "anglePitchKdTextBox";
            this.anglePitchKdTextBox.Size = new System.Drawing.Size(70, 20);
            this.anglePitchKdTextBox.TabIndex = 15;
            // 
            // anglePitchKiTextBox
            // 
            this.anglePitchKiTextBox.DecimalPlaces = 2;
            this.anglePitchKiTextBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.anglePitchKiTextBox.Location = new System.Drawing.Point(34, 46);
            this.anglePitchKiTextBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.anglePitchKiTextBox.Name = "anglePitchKiTextBox";
            this.anglePitchKiTextBox.Size = new System.Drawing.Size(70, 20);
            this.anglePitchKiTextBox.TabIndex = 14;
            // 
            // anglePitchKpTextBox
            // 
            this.anglePitchKpTextBox.DecimalPlaces = 2;
            this.anglePitchKpTextBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.anglePitchKpTextBox.Location = new System.Drawing.Point(34, 18);
            this.anglePitchKpTextBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.anglePitchKpTextBox.Name = "anglePitchKpTextBox";
            this.anglePitchKpTextBox.Size = new System.Drawing.Size(70, 20);
            this.anglePitchKpTextBox.TabIndex = 13;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(8, 22);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(20, 13);
            label12.TabIndex = 2;
            label12.Text = "Kp";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(8, 48);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(16, 13);
            label13.TabIndex = 8;
            label13.Text = "Ki";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(8, 74);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(20, 13);
            label14.TabIndex = 12;
            label14.Text = "Kd";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(this.angleYawKdTextBox);
            groupBox4.Controls.Add(this.angleYawKiTextBox);
            groupBox4.Controls.Add(this.angleYawKpTextBox);
            groupBox4.Controls.Add(label15);
            groupBox4.Controls.Add(label16);
            groupBox4.Controls.Add(label17);
            groupBox4.Location = new System.Drawing.Point(518, 219);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(117, 101);
            groupBox4.TabIndex = 24;
            groupBox4.TabStop = false;
            groupBox4.Text = "PID Angle Yaw";
            // 
            // angleYawKdTextBox
            // 
            this.angleYawKdTextBox.DecimalPlaces = 2;
            this.angleYawKdTextBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.angleYawKdTextBox.Location = new System.Drawing.Point(34, 72);
            this.angleYawKdTextBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.angleYawKdTextBox.Name = "angleYawKdTextBox";
            this.angleYawKdTextBox.Size = new System.Drawing.Size(70, 20);
            this.angleYawKdTextBox.TabIndex = 15;
            // 
            // angleYawKiTextBox
            // 
            this.angleYawKiTextBox.DecimalPlaces = 2;
            this.angleYawKiTextBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.angleYawKiTextBox.Location = new System.Drawing.Point(34, 46);
            this.angleYawKiTextBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.angleYawKiTextBox.Name = "angleYawKiTextBox";
            this.angleYawKiTextBox.Size = new System.Drawing.Size(70, 20);
            this.angleYawKiTextBox.TabIndex = 14;
            // 
            // angleYawKpTextBox
            // 
            this.angleYawKpTextBox.DecimalPlaces = 2;
            this.angleYawKpTextBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.angleYawKpTextBox.Location = new System.Drawing.Point(34, 18);
            this.angleYawKpTextBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.angleYawKpTextBox.Name = "angleYawKpTextBox";
            this.angleYawKpTextBox.Size = new System.Drawing.Size(70, 20);
            this.angleYawKpTextBox.TabIndex = 13;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(8, 22);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(20, 13);
            label15.TabIndex = 2;
            label15.Text = "Kp";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new System.Drawing.Point(8, 48);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(16, 13);
            label16.TabIndex = 8;
            label16.Text = "Ki";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new System.Drawing.Point(8, 74);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(20, 13);
            label17.TabIndex = 12;
            label17.Text = "Kd";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 438);
            this.Controls.Add(this.revertButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            firmwareGroupBox.ResumeLayout(false);
            firmwareGroupBox.PerformLayout();
            quadrocopterPage.ResumeLayout(false);
            quadrocopterPage.PerformLayout();
            hardwareGroupBox.ResumeLayout(false);
            hardwareGroupBox.PerformLayout();
            motorsGroupBox.ResumeLayout(false);
            motorsGroupBox.PerformLayout();
            safetyGroupBox.ResumeLayout(false);
            safetyGroupBox.PerformLayout();
            pidPitchGroupBox.ResumeLayout(false);
            pidPitchGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pitchKdTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pitchKiTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pitchKpTextBox)).EndInit();
            pidRollGroupBox.ResumeLayout(false);
            pidRollGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rollKdTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rollKiTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rollKpTextBox)).EndInit();
            pidYawGroupBox.ResumeLayout(false);
            pidYawGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yawKdTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yawKiTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yawKpTextBox)).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.angleRollKdTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleRollKiTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleRollKpTextBox)).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yawTrim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pitchTrim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rollTrim)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.flyingPage.ResumeLayout(false);
            this.flyingPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxThrustForFlyingTextBox)).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.anglePitchKdTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.anglePitchKiTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.anglePitchKpTextBox)).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.angleYawKdTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleYawKiTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleYawKpTextBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage flyingPage;
        private System.Windows.Forms.Button restartButton;
        private System.Windows.Forms.TextBox modelTextBox;
        private System.Windows.Forms.TextBox magnetometerTextBox;
        private System.Windows.Forms.TextBox gyroSensorTextBox;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Button updateFirmwareButton;
        private System.Windows.Forms.TextBox firmwareVersionTextBox;
        private System.Windows.Forms.TextBox buildDateTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Button calibrateButton;
        private System.Windows.Forms.TextBox minValueTextBox;
        private System.Windows.Forms.TextBox idleValueTextBox;
        private System.Windows.Forms.TextBox maxValueTextBox;
        private System.Windows.Forms.TextBox safeMotorValueTextBox;
        private System.Windows.Forms.TextBox safeRollTextBox;
        private System.Windows.Forms.TextBox safePitchTextBox;
        private System.Windows.Forms.TextBox safeTemperatureTextBox;
        private System.Windows.Forms.NumericUpDown pitchKdTextBox;
        private System.Windows.Forms.NumericUpDown pitchKiTextBox;
        private System.Windows.Forms.NumericUpDown pitchKpTextBox;
        private System.Windows.Forms.NumericUpDown yawKdTextBox;
        private System.Windows.Forms.NumericUpDown yawKiTextBox;
        private System.Windows.Forms.NumericUpDown yawKpTextBox;
        private System.Windows.Forms.NumericUpDown rollKdTextBox;
        private System.Windows.Forms.NumericUpDown rollKiTextBox;
        private System.Windows.Forms.NumericUpDown rollKpTextBox;
        private System.Windows.Forms.CheckBox saveConfigCheckBox;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.CheckBox enableStabilizationCheckBox;
        private System.Windows.Forms.NumericUpDown maxThrustForFlyingTextBox;
        private System.Windows.Forms.CheckBox ignoreSafeOrientationCheckBox;
        private System.Windows.Forms.NumericUpDown angleRollKdTextBox;
        private System.Windows.Forms.NumericUpDown angleRollKiTextBox;
        private System.Windows.Forms.NumericUpDown angleRollKpTextBox;
        private System.Windows.Forms.TextBox baroSensorTextBox;
        private System.Windows.Forms.Button revertButton;
        private System.Windows.Forms.Button tuningButton;
        private System.Windows.Forms.NumericUpDown yawTrim;
        private System.Windows.Forms.NumericUpDown pitchTrim;
        private System.Windows.Forms.NumericUpDown rollTrim;
        private System.Windows.Forms.NumericUpDown angleYawKdTextBox;
        private System.Windows.Forms.NumericUpDown angleYawKiTextBox;
        private System.Windows.Forms.NumericUpDown angleYawKpTextBox;
        private System.Windows.Forms.NumericUpDown anglePitchKdTextBox;
        private System.Windows.Forms.NumericUpDown anglePitchKiTextBox;
        private System.Windows.Forms.NumericUpDown anglePitchKpTextBox;
    }
}