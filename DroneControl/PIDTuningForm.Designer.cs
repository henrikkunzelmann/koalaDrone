namespace DroneControl
{
    partial class PIDTuningForm
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
            System.Windows.Forms.Label tuneAmountLabel;
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.GroupBox groupBox3;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PIDTuningForm));
            this.rollDValue = new System.Windows.Forms.Label();
            this.rollDIncrement = new System.Windows.Forms.Button();
            this.rollDDecrement = new System.Windows.Forms.Button();
            this.rollIValue = new System.Windows.Forms.Label();
            this.rollIIncrement = new System.Windows.Forms.Button();
            this.rollIDecrement = new System.Windows.Forms.Button();
            this.rollPValue = new System.Windows.Forms.Label();
            this.rollPIncrement = new System.Windows.Forms.Button();
            this.rollPDecrement = new System.Windows.Forms.Button();
            this.pitchDValue = new System.Windows.Forms.Label();
            this.pitchDIncrement = new System.Windows.Forms.Button();
            this.pitchDDecrement = new System.Windows.Forms.Button();
            this.pitchIValue = new System.Windows.Forms.Label();
            this.pitchIIncrement = new System.Windows.Forms.Button();
            this.pitchIDecrement = new System.Windows.Forms.Button();
            this.pitchPValue = new System.Windows.Forms.Label();
            this.pitchPIncrement = new System.Windows.Forms.Button();
            this.pitchPDecrement = new System.Windows.Forms.Button();
            this.yawDValue = new System.Windows.Forms.Label();
            this.yawDIncrement = new System.Windows.Forms.Button();
            this.yawDDecrement = new System.Windows.Forms.Button();
            this.yawIValue = new System.Windows.Forms.Label();
            this.yawIIncrement = new System.Windows.Forms.Button();
            this.yawIDecrement = new System.Windows.Forms.Button();
            this.yawPValue = new System.Windows.Forms.Label();
            this.yawPIncrement = new System.Windows.Forms.Button();
            this.yawPDecrement = new System.Windows.Forms.Button();
            this.tuneAmount = new System.Windows.Forms.NumericUpDown();
            tuneAmountLabel = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            groupBox3 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tuneAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // tuneAmountLabel
            // 
            tuneAmountLabel.AutoSize = true;
            tuneAmountLabel.Location = new System.Drawing.Point(9, 14);
            tuneAmountLabel.Name = "tuneAmountLabel";
            tuneAmountLabel.Size = new System.Drawing.Size(71, 13);
            tuneAmountLabel.TabIndex = 1;
            tuneAmountLabel.Text = "Tune Amount";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.rollDValue);
            groupBox1.Controls.Add(this.rollDIncrement);
            groupBox1.Controls.Add(this.rollDDecrement);
            groupBox1.Controls.Add(this.rollIValue);
            groupBox1.Controls.Add(this.rollIIncrement);
            groupBox1.Controls.Add(this.rollIDecrement);
            groupBox1.Controls.Add(this.rollPValue);
            groupBox1.Controls.Add(this.rollPIncrement);
            groupBox1.Controls.Add(this.rollPDecrement);
            groupBox1.Location = new System.Drawing.Point(12, 56);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(214, 113);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "PID Roll";
            // 
            // rollDValue
            // 
            this.rollDValue.AutoSize = true;
            this.rollDValue.Location = new System.Drawing.Point(88, 83);
            this.rollDValue.Name = "rollDValue";
            this.rollDValue.Size = new System.Drawing.Size(34, 13);
            this.rollDValue.TabIndex = 8;
            this.rollDValue.Text = "1,234";
            // 
            // rollDIncrement
            // 
            this.rollDIncrement.Location = new System.Drawing.Point(128, 78);
            this.rollDIncrement.Name = "rollDIncrement";
            this.rollDIncrement.Size = new System.Drawing.Size(75, 23);
            this.rollDIncrement.TabIndex = 7;
            this.rollDIncrement.Text = "D +";
            this.rollDIncrement.UseVisualStyleBackColor = true;
            this.rollDIncrement.Click += new System.EventHandler(this.rollDIncrement_Click);
            // 
            // rollDDecrement
            // 
            this.rollDDecrement.Location = new System.Drawing.Point(7, 78);
            this.rollDDecrement.Name = "rollDDecrement";
            this.rollDDecrement.Size = new System.Drawing.Size(75, 23);
            this.rollDDecrement.TabIndex = 6;
            this.rollDDecrement.Text = "D -";
            this.rollDDecrement.UseVisualStyleBackColor = true;
            this.rollDDecrement.Click += new System.EventHandler(this.rollDDecrement_Click);
            // 
            // rollIValue
            // 
            this.rollIValue.AutoSize = true;
            this.rollIValue.Location = new System.Drawing.Point(88, 54);
            this.rollIValue.Name = "rollIValue";
            this.rollIValue.Size = new System.Drawing.Size(34, 13);
            this.rollIValue.TabIndex = 5;
            this.rollIValue.Text = "1,234";
            // 
            // rollIIncrement
            // 
            this.rollIIncrement.Location = new System.Drawing.Point(128, 49);
            this.rollIIncrement.Name = "rollIIncrement";
            this.rollIIncrement.Size = new System.Drawing.Size(75, 23);
            this.rollIIncrement.TabIndex = 4;
            this.rollIIncrement.Text = "I +";
            this.rollIIncrement.UseVisualStyleBackColor = true;
            this.rollIIncrement.Click += new System.EventHandler(this.rollIIncrement_Click);
            // 
            // rollIDecrement
            // 
            this.rollIDecrement.Location = new System.Drawing.Point(7, 49);
            this.rollIDecrement.Name = "rollIDecrement";
            this.rollIDecrement.Size = new System.Drawing.Size(75, 23);
            this.rollIDecrement.TabIndex = 3;
            this.rollIDecrement.Text = "I -";
            this.rollIDecrement.UseVisualStyleBackColor = true;
            this.rollIDecrement.Click += new System.EventHandler(this.rollIDecrement_Click);
            // 
            // rollPValue
            // 
            this.rollPValue.AutoSize = true;
            this.rollPValue.Location = new System.Drawing.Point(88, 25);
            this.rollPValue.Name = "rollPValue";
            this.rollPValue.Size = new System.Drawing.Size(34, 13);
            this.rollPValue.TabIndex = 2;
            this.rollPValue.Text = "1,234";
            // 
            // rollPIncrement
            // 
            this.rollPIncrement.Location = new System.Drawing.Point(128, 20);
            this.rollPIncrement.Name = "rollPIncrement";
            this.rollPIncrement.Size = new System.Drawing.Size(75, 23);
            this.rollPIncrement.TabIndex = 1;
            this.rollPIncrement.Text = "P +";
            this.rollPIncrement.UseVisualStyleBackColor = true;
            this.rollPIncrement.Click += new System.EventHandler(this.rollPIncrement_Click);
            // 
            // rollPDecrement
            // 
            this.rollPDecrement.Location = new System.Drawing.Point(7, 20);
            this.rollPDecrement.Name = "rollPDecrement";
            this.rollPDecrement.Size = new System.Drawing.Size(75, 23);
            this.rollPDecrement.TabIndex = 0;
            this.rollPDecrement.Text = "P -";
            this.rollPDecrement.UseVisualStyleBackColor = true;
            this.rollPDecrement.Click += new System.EventHandler(this.rollPDecrement_Click);
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.pitchDValue);
            groupBox2.Controls.Add(this.pitchDIncrement);
            groupBox2.Controls.Add(this.pitchDDecrement);
            groupBox2.Controls.Add(this.pitchIValue);
            groupBox2.Controls.Add(this.pitchIIncrement);
            groupBox2.Controls.Add(this.pitchIDecrement);
            groupBox2.Controls.Add(this.pitchPValue);
            groupBox2.Controls.Add(this.pitchPIncrement);
            groupBox2.Controls.Add(this.pitchPDecrement);
            groupBox2.Location = new System.Drawing.Point(232, 58);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(214, 113);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "PID Pitch";
            // 
            // pitchDValue
            // 
            this.pitchDValue.AutoSize = true;
            this.pitchDValue.Location = new System.Drawing.Point(88, 83);
            this.pitchDValue.Name = "pitchDValue";
            this.pitchDValue.Size = new System.Drawing.Size(34, 13);
            this.pitchDValue.TabIndex = 8;
            this.pitchDValue.Text = "1,234";
            // 
            // pitchDIncrement
            // 
            this.pitchDIncrement.Location = new System.Drawing.Point(128, 78);
            this.pitchDIncrement.Name = "pitchDIncrement";
            this.pitchDIncrement.Size = new System.Drawing.Size(75, 23);
            this.pitchDIncrement.TabIndex = 7;
            this.pitchDIncrement.Text = "D +";
            this.pitchDIncrement.UseVisualStyleBackColor = true;
            this.pitchDIncrement.Click += new System.EventHandler(this.pitchDIncrement_Click);
            // 
            // pitchDDecrement
            // 
            this.pitchDDecrement.Location = new System.Drawing.Point(7, 78);
            this.pitchDDecrement.Name = "pitchDDecrement";
            this.pitchDDecrement.Size = new System.Drawing.Size(75, 23);
            this.pitchDDecrement.TabIndex = 6;
            this.pitchDDecrement.Text = "D -";
            this.pitchDDecrement.UseVisualStyleBackColor = true;
            this.pitchDDecrement.Click += new System.EventHandler(this.pitchDDecrement_Click);
            // 
            // pitchIValue
            // 
            this.pitchIValue.AutoSize = true;
            this.pitchIValue.Location = new System.Drawing.Point(88, 54);
            this.pitchIValue.Name = "pitchIValue";
            this.pitchIValue.Size = new System.Drawing.Size(34, 13);
            this.pitchIValue.TabIndex = 5;
            this.pitchIValue.Text = "1,234";
            // 
            // pitchIIncrement
            // 
            this.pitchIIncrement.Location = new System.Drawing.Point(128, 49);
            this.pitchIIncrement.Name = "pitchIIncrement";
            this.pitchIIncrement.Size = new System.Drawing.Size(75, 23);
            this.pitchIIncrement.TabIndex = 4;
            this.pitchIIncrement.Text = "I +";
            this.pitchIIncrement.UseVisualStyleBackColor = true;
            this.pitchIIncrement.Click += new System.EventHandler(this.pitchIIncrement_Click);
            // 
            // pitchIDecrement
            // 
            this.pitchIDecrement.Location = new System.Drawing.Point(7, 49);
            this.pitchIDecrement.Name = "pitchIDecrement";
            this.pitchIDecrement.Size = new System.Drawing.Size(75, 23);
            this.pitchIDecrement.TabIndex = 3;
            this.pitchIDecrement.Text = "I -";
            this.pitchIDecrement.UseVisualStyleBackColor = true;
            this.pitchIDecrement.Click += new System.EventHandler(this.pitchIDecrement_Click);
            // 
            // pitchPValue
            // 
            this.pitchPValue.AutoSize = true;
            this.pitchPValue.Location = new System.Drawing.Point(88, 25);
            this.pitchPValue.Name = "pitchPValue";
            this.pitchPValue.Size = new System.Drawing.Size(34, 13);
            this.pitchPValue.TabIndex = 2;
            this.pitchPValue.Text = "1,234";
            // 
            // pitchPIncrement
            // 
            this.pitchPIncrement.Location = new System.Drawing.Point(128, 20);
            this.pitchPIncrement.Name = "pitchPIncrement";
            this.pitchPIncrement.Size = new System.Drawing.Size(75, 23);
            this.pitchPIncrement.TabIndex = 1;
            this.pitchPIncrement.Text = "P +";
            this.pitchPIncrement.UseVisualStyleBackColor = true;
            this.pitchPIncrement.Click += new System.EventHandler(this.pitchPIncrement_Click);
            // 
            // pitchPDecrement
            // 
            this.pitchPDecrement.Location = new System.Drawing.Point(7, 20);
            this.pitchPDecrement.Name = "pitchPDecrement";
            this.pitchPDecrement.Size = new System.Drawing.Size(75, 23);
            this.pitchPDecrement.TabIndex = 0;
            this.pitchPDecrement.Text = "P -";
            this.pitchPDecrement.UseVisualStyleBackColor = true;
            this.pitchPDecrement.Click += new System.EventHandler(this.pitchPDecrement_Click);
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(this.yawDValue);
            groupBox3.Controls.Add(this.yawDIncrement);
            groupBox3.Controls.Add(this.yawDDecrement);
            groupBox3.Controls.Add(this.yawIValue);
            groupBox3.Controls.Add(this.yawIIncrement);
            groupBox3.Controls.Add(this.yawIDecrement);
            groupBox3.Controls.Add(this.yawPValue);
            groupBox3.Controls.Add(this.yawPIncrement);
            groupBox3.Controls.Add(this.yawPDecrement);
            groupBox3.Location = new System.Drawing.Point(452, 58);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(214, 113);
            groupBox3.TabIndex = 10;
            groupBox3.TabStop = false;
            groupBox3.Text = "PID Yaw";
            // 
            // yawDValue
            // 
            this.yawDValue.AutoSize = true;
            this.yawDValue.Location = new System.Drawing.Point(88, 83);
            this.yawDValue.Name = "yawDValue";
            this.yawDValue.Size = new System.Drawing.Size(34, 13);
            this.yawDValue.TabIndex = 8;
            this.yawDValue.Text = "1,234";
            // 
            // yawDIncrement
            // 
            this.yawDIncrement.Location = new System.Drawing.Point(128, 78);
            this.yawDIncrement.Name = "yawDIncrement";
            this.yawDIncrement.Size = new System.Drawing.Size(75, 23);
            this.yawDIncrement.TabIndex = 7;
            this.yawDIncrement.Text = "D +";
            this.yawDIncrement.UseVisualStyleBackColor = true;
            this.yawDIncrement.Click += new System.EventHandler(this.yawDIncrement_Click);
            // 
            // yawDDecrement
            // 
            this.yawDDecrement.Location = new System.Drawing.Point(7, 78);
            this.yawDDecrement.Name = "yawDDecrement";
            this.yawDDecrement.Size = new System.Drawing.Size(75, 23);
            this.yawDDecrement.TabIndex = 6;
            this.yawDDecrement.Text = "D -";
            this.yawDDecrement.UseVisualStyleBackColor = true;
            this.yawDDecrement.Click += new System.EventHandler(this.yawDDecrement_Click);
            // 
            // yawIValue
            // 
            this.yawIValue.AutoSize = true;
            this.yawIValue.Location = new System.Drawing.Point(88, 54);
            this.yawIValue.Name = "yawIValue";
            this.yawIValue.Size = new System.Drawing.Size(34, 13);
            this.yawIValue.TabIndex = 5;
            this.yawIValue.Text = "1,234";
            // 
            // yawIIncrement
            // 
            this.yawIIncrement.Location = new System.Drawing.Point(128, 49);
            this.yawIIncrement.Name = "yawIIncrement";
            this.yawIIncrement.Size = new System.Drawing.Size(75, 23);
            this.yawIIncrement.TabIndex = 4;
            this.yawIIncrement.Text = "I +";
            this.yawIIncrement.UseVisualStyleBackColor = true;
            this.yawIIncrement.Click += new System.EventHandler(this.yawIIncrement_Click);
            // 
            // yawIDecrement
            // 
            this.yawIDecrement.Location = new System.Drawing.Point(7, 49);
            this.yawIDecrement.Name = "yawIDecrement";
            this.yawIDecrement.Size = new System.Drawing.Size(75, 23);
            this.yawIDecrement.TabIndex = 3;
            this.yawIDecrement.Text = "I -";
            this.yawIDecrement.UseVisualStyleBackColor = true;
            this.yawIDecrement.Click += new System.EventHandler(this.yawIDecrement_Click);
            // 
            // yawPValue
            // 
            this.yawPValue.AutoSize = true;
            this.yawPValue.Location = new System.Drawing.Point(88, 25);
            this.yawPValue.Name = "yawPValue";
            this.yawPValue.Size = new System.Drawing.Size(34, 13);
            this.yawPValue.TabIndex = 2;
            this.yawPValue.Text = "1,234";
            // 
            // yawPIncrement
            // 
            this.yawPIncrement.Location = new System.Drawing.Point(128, 20);
            this.yawPIncrement.Name = "yawPIncrement";
            this.yawPIncrement.Size = new System.Drawing.Size(75, 23);
            this.yawPIncrement.TabIndex = 1;
            this.yawPIncrement.Text = "P +";
            this.yawPIncrement.UseVisualStyleBackColor = true;
            this.yawPIncrement.Click += new System.EventHandler(this.yawPIncrement_Click);
            // 
            // yawPDecrement
            // 
            this.yawPDecrement.Location = new System.Drawing.Point(7, 20);
            this.yawPDecrement.Name = "yawPDecrement";
            this.yawPDecrement.Size = new System.Drawing.Size(75, 23);
            this.yawPDecrement.TabIndex = 0;
            this.yawPDecrement.Text = "P -";
            this.yawPDecrement.UseVisualStyleBackColor = true;
            this.yawPDecrement.Click += new System.EventHandler(this.yawPDecrement_Click);
            // 
            // tuneAmount
            // 
            this.tuneAmount.DecimalPlaces = 3;
            this.tuneAmount.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.tuneAmount.Location = new System.Drawing.Point(106, 12);
            this.tuneAmount.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tuneAmount.Name = "tuneAmount";
            this.tuneAmount.Size = new System.Drawing.Size(120, 20);
            this.tuneAmount.TabIndex = 0;
            this.tuneAmount.Value = new decimal(new int[] {
            2,
            0,
            0,
            131072});
            // 
            // PIDTuningForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 183);
            this.Controls.Add(groupBox3);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox1);
            this.Controls.Add(tuneAmountLabel);
            this.Controls.Add(this.tuneAmount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PIDTuningForm";
            this.Text = "PID Tuning";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tuneAmount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown tuneAmount;
        private System.Windows.Forms.Label rollDValue;
        private System.Windows.Forms.Button rollDIncrement;
        private System.Windows.Forms.Button rollDDecrement;
        private System.Windows.Forms.Label rollIValue;
        private System.Windows.Forms.Button rollIIncrement;
        private System.Windows.Forms.Button rollIDecrement;
        private System.Windows.Forms.Label rollPValue;
        private System.Windows.Forms.Button rollPIncrement;
        private System.Windows.Forms.Button rollPDecrement;
        private System.Windows.Forms.Label pitchDValue;
        private System.Windows.Forms.Button pitchDIncrement;
        private System.Windows.Forms.Button pitchDDecrement;
        private System.Windows.Forms.Label pitchIValue;
        private System.Windows.Forms.Button pitchIIncrement;
        private System.Windows.Forms.Button pitchIDecrement;
        private System.Windows.Forms.Label pitchPValue;
        private System.Windows.Forms.Button pitchPIncrement;
        private System.Windows.Forms.Button pitchPDecrement;
        private System.Windows.Forms.Label yawDValue;
        private System.Windows.Forms.Button yawDIncrement;
        private System.Windows.Forms.Button yawDDecrement;
        private System.Windows.Forms.Label yawIValue;
        private System.Windows.Forms.Button yawIIncrement;
        private System.Windows.Forms.Button yawIDecrement;
        private System.Windows.Forms.Label yawPValue;
        private System.Windows.Forms.Button yawPIncrement;
        private System.Windows.Forms.Button yawPDecrement;
    }
}