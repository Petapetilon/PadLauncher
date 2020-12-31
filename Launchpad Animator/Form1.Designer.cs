namespace Launchpad_Animator
{
    partial class LaunchpadSelectionLabel
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.selectLaunchpadDevice = new System.Windows.Forms.Label();
            this.SelectLaunchpadModelLabel = new System.Windows.Forms.Label();
            this.selectLaunchpadDevcieInputComboBox = new System.Windows.Forms.ComboBox();
            this.selectLaunchpadDeviceOut = new System.Windows.Forms.Label();
            this.selectLaunchpadDeviceOutputComboBox = new System.Windows.Forms.ComboBox();
            this.selectLaunchpadModelComboBox = new System.Windows.Forms.ComboBox();
            this.refreshMidiDevicesButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.debugpanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // selectLaunchpadDevice
            // 
            this.selectLaunchpadDevice.AutoSize = true;
            this.selectLaunchpadDevice.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.selectLaunchpadDevice.Location = new System.Drawing.Point(13, 9);
            this.selectLaunchpadDevice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.selectLaunchpadDevice.Name = "selectLaunchpadDevice";
            this.selectLaunchpadDevice.Size = new System.Drawing.Size(221, 21);
            this.selectLaunchpadDevice.TabIndex = 0;
            this.selectLaunchpadDevice.Text = "Select Launchpad Device Input";
            this.selectLaunchpadDevice.Click += new System.EventHandler(this.label1_Click);
            // 
            // SelectLaunchpadModelLabel
            // 
            this.SelectLaunchpadModelLabel.AutoSize = true;
            this.SelectLaunchpadModelLabel.Location = new System.Drawing.Point(11, 166);
            this.SelectLaunchpadModelLabel.Name = "SelectLaunchpadModelLabel";
            this.SelectLaunchpadModelLabel.Size = new System.Drawing.Size(179, 21);
            this.SelectLaunchpadModelLabel.TabIndex = 1;
            this.SelectLaunchpadModelLabel.Text = "Select Launchpad Model";
            this.SelectLaunchpadModelLabel.Click += new System.EventHandler(this.SelectLaunchpadModelLabel_Click);
            // 
            // selectLaunchpadDevcieInputComboBox
            // 
            this.selectLaunchpadDevcieInputComboBox.FormattingEnabled = true;
            this.selectLaunchpadDevcieInputComboBox.Location = new System.Drawing.Point(13, 34);
            this.selectLaunchpadDevcieInputComboBox.Name = "selectLaunchpadDevcieInputComboBox";
            this.selectLaunchpadDevcieInputComboBox.Size = new System.Drawing.Size(290, 29);
            this.selectLaunchpadDevcieInputComboBox.TabIndex = 2;
            this.selectLaunchpadDevcieInputComboBox.SelectedIndexChanged += new System.EventHandler(this.selectLaunchpadDeviceInputComboBox_SelectedIndexChanged);
            // 
            // selectLaunchpadDeviceOut
            // 
            this.selectLaunchpadDeviceOut.AutoSize = true;
            this.selectLaunchpadDeviceOut.Location = new System.Drawing.Point(12, 85);
            this.selectLaunchpadDeviceOut.Name = "selectLaunchpadDeviceOut";
            this.selectLaunchpadDeviceOut.Size = new System.Drawing.Size(234, 21);
            this.selectLaunchpadDeviceOut.TabIndex = 3;
            this.selectLaunchpadDeviceOut.Text = "Select Launchpad Device Output";
            this.selectLaunchpadDeviceOut.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // selectLaunchpadDeviceOutputComboBox
            // 
            this.selectLaunchpadDeviceOutputComboBox.FormattingEnabled = true;
            this.selectLaunchpadDeviceOutputComboBox.Location = new System.Drawing.Point(11, 109);
            this.selectLaunchpadDeviceOutputComboBox.Name = "selectLaunchpadDeviceOutputComboBox";
            this.selectLaunchpadDeviceOutputComboBox.Size = new System.Drawing.Size(291, 29);
            this.selectLaunchpadDeviceOutputComboBox.TabIndex = 4;
            this.selectLaunchpadDeviceOutputComboBox.SelectedIndexChanged += new System.EventHandler(this.selectLaunchpadDeviceOutputComboBox_SelectedIndexChanged);
            // 
            // selectLaunchpadModelComboBox
            // 
            this.selectLaunchpadModelComboBox.FormattingEnabled = true;
            this.selectLaunchpadModelComboBox.Location = new System.Drawing.Point(13, 191);
            this.selectLaunchpadModelComboBox.Name = "selectLaunchpadModelComboBox";
            this.selectLaunchpadModelComboBox.Size = new System.Drawing.Size(290, 29);
            this.selectLaunchpadModelComboBox.TabIndex = 5;
            this.selectLaunchpadModelComboBox.SelectedIndexChanged += new System.EventHandler(this.selectLaunchpadModelComboBox_SelectedIndexChanged);
            // 
            // refreshMidiDevicesButton
            // 
            this.refreshMidiDevicesButton.Location = new System.Drawing.Point(10, 239);
            this.refreshMidiDevicesButton.Name = "refreshMidiDevicesButton";
            this.refreshMidiDevicesButton.Size = new System.Drawing.Size(292, 32);
            this.refreshMidiDevicesButton.TabIndex = 6;
            this.refreshMidiDevicesButton.Text = "Refresh Device List";
            this.refreshMidiDevicesButton.UseVisualStyleBackColor = true;
            this.refreshMidiDevicesButton.Click += new System.EventHandler(this.refreshMidiDevicesButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(10, 328);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(143, 62);
            this.startButton.TabIndex = 7;
            this.startButton.Text = "Start it!";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // debugpanel
            // 
            this.debugpanel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.debugpanel.Location = new System.Drawing.Point(574, 191);
            this.debugpanel.Name = "debugpanel";
            this.debugpanel.Size = new System.Drawing.Size(304, 173);
            this.debugpanel.TabIndex = 8;
            // 
            // LaunchpadSelectionLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 630);
            this.Controls.Add(this.debugpanel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.refreshMidiDevicesButton);
            this.Controls.Add(this.selectLaunchpadModelComboBox);
            this.Controls.Add(this.selectLaunchpadDeviceOutputComboBox);
            this.Controls.Add(this.selectLaunchpadDeviceOut);
            this.Controls.Add(this.selectLaunchpadDevcieInputComboBox);
            this.Controls.Add(this.SelectLaunchpadModelLabel);
            this.Controls.Add(this.selectLaunchpadDevice);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LaunchpadSelectionLabel";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Device;
        private System.Windows.Forms.Label SelectLaunchpadModelLabel;
        private System.Windows.Forms.ComboBox selectLaunchpadDevcieComboBox;
        private System.Windows.Forms.Label selectLaunchpadDevice;
        private System.Windows.Forms.Label selectLaunchpadDeviceOut;
        private System.Windows.Forms.ComboBox selectLaunchpadDevcieInputComboBox;
        private System.Windows.Forms.ComboBox selectLaunchpadDeviceOutputComboBox;
        private System.Windows.Forms.ComboBox selectLaunchpadModelComboBox;
        private System.Windows.Forms.Button refreshMidiDevicesButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Panel debugpanel;
    }
}

