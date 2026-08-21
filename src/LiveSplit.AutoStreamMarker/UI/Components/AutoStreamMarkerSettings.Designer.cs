namespace LiveSplit.UI.Components
{
    partial class AutoStreamMarkerSettings
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
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                ObsTestClient?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkNotificationsEnabled = new System.Windows.Forms.CheckBox();
            this.chkMarkResets = new System.Windows.Forms.CheckBox();
            this.chkMarkEverySplit = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Connect = new System.Windows.Forms.Button();
            this.Avatar = new System.Windows.Forms.PictureBox();
            this.Username = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkObsEnabled = new System.Windows.Forms.CheckBox();
            this.lblObsUrl = new System.Windows.Forms.Label();
            this.txtObsUrl = new System.Windows.Forms.TextBox();
            this.lblObsPassword = new System.Windows.Forms.Label();
            this.txtObsPassword = new System.Windows.Forms.TextBox();
            this.btnObsTest = new System.Windows.Forms.Button();
            this.chkLogEnabled = new System.Windows.Forms.CheckBox();
            this.lblLogFolder = new System.Windows.Forms.Label();
            this.txtLogFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseLogFolder = new System.Windows.Forms.Button();
            this.lblLogHint = new System.Windows.Forms.Label();
            this.lblLogOffset = new System.Windows.Forms.Label();
            this.txtLogOffsetSeconds = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Avatar)).BeginInit();
            this.SuspendLayout();
            //
            // groupBox2
            //
            this.groupBox2.Controls.Add(this.chkNotificationsEnabled);
            this.groupBox2.Controls.Add(this.chkMarkResets);
            this.groupBox2.Controls.Add(this.chkMarkEverySplit);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 373);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(475, 86);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            //
            // chkMarkResets
            //
            this.chkMarkResets.AutoSize = true;
            this.chkMarkResets.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkMarkResets.Location = new System.Drawing.Point(3, 33);
            this.chkMarkResets.Name = "chkMarkResets";
            this.chkMarkResets.Size = new System.Drawing.Size(469, 17);
            this.chkMarkResets.TabIndex = 7;
            this.chkMarkResets.Text = "Mark resets in the VOD";
            this.chkMarkResets.UseVisualStyleBackColor = true;
            //
            // chkMarkEverySplit
            //
            this.chkMarkEverySplit.AutoSize = true;
            this.chkMarkEverySplit.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkMarkEverySplit.Location = new System.Drawing.Point(3, 16);
            this.chkMarkEverySplit.Name = "chkMarkEverySplit";
            this.chkMarkEverySplit.Size = new System.Drawing.Size(469, 17);
            this.chkMarkEverySplit.TabIndex = 8;
            this.chkMarkEverySplit.Text = "Mark every split in the VOD";
            this.chkMarkEverySplit.UseVisualStyleBackColor = true;
            //
            // chkNotificationsEnabled
            //
            this.chkNotificationsEnabled.AutoSize = true;
            this.chkNotificationsEnabled.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkNotificationsEnabled.Location = new System.Drawing.Point(3, 50);
            this.chkNotificationsEnabled.Name = "chkNotificationsEnabled";
            this.chkNotificationsEnabled.Size = new System.Drawing.Size(469, 17);
            this.chkNotificationsEnabled.TabIndex = 9;
            this.chkNotificationsEnabled.Text = "Show notifications for errors and status changes";
            this.chkNotificationsEnabled.UseVisualStyleBackColor = true;
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.Connect);
            this.groupBox1.Controls.Add(this.Avatar);
            this.groupBox1.Controls.Add(this.Username);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 128);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Twitch Account";
            //
            // Connect
            //
            this.Connect.Location = new System.Drawing.Point(116, 67);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(181, 30);
            this.Connect.TabIndex = 7;
            this.Connect.Text = "Connect/Reconnect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.ShowLogin);
            //
            // Avatar
            //
            this.Avatar.BackColor = System.Drawing.Color.LightGray;
            this.Avatar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Avatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Avatar.Location = new System.Drawing.Point(6, 19);
            this.Avatar.Name = "Avatar";
            this.Avatar.Size = new System.Drawing.Size(100, 100);
            this.Avatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Avatar.TabIndex = 5;
            this.Avatar.TabStop = false;
            //
            // Username
            //
            this.Username.AutoSize = true;
            this.Username.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Username.Location = new System.Drawing.Point(112, 40);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(184, 24);
            this.Username.TabIndex = 4;
            this.Username.Text = "User: Not Logged";
            //
            // groupBox3
            //
            this.groupBox3.Controls.Add(this.txtLogOffsetSeconds);
            this.groupBox3.Controls.Add(this.lblLogOffset);
            this.groupBox3.Controls.Add(this.lblLogHint);
            this.groupBox3.Controls.Add(this.btnBrowseLogFolder);
            this.groupBox3.Controls.Add(this.txtLogFolder);
            this.groupBox3.Controls.Add(this.lblLogFolder);
            this.groupBox3.Controls.Add(this.chkLogEnabled);
            this.groupBox3.Controls.Add(this.btnObsTest);
            this.groupBox3.Controls.Add(this.txtObsPassword);
            this.groupBox3.Controls.Add(this.lblObsPassword);
            this.groupBox3.Controls.Add(this.txtObsUrl);
            this.groupBox3.Controls.Add(this.lblObsUrl);
            this.groupBox3.Controls.Add(this.chkObsEnabled);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 128);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(475, 245);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "OBS WebSocket";
            //
            // chkObsEnabled
            //
            this.chkObsEnabled.AutoSize = true;
            this.chkObsEnabled.Location = new System.Drawing.Point(6, 19);
            this.chkObsEnabled.Name = "chkObsEnabled";
            this.chkObsEnabled.Size = new System.Drawing.Size(300, 17);
            this.chkObsEnabled.TabIndex = 0;
            this.chkObsEnabled.Text = "Create a chapter marker in OBS";
            this.chkObsEnabled.UseVisualStyleBackColor = true;
            //
            // lblObsUrl
            //
            this.lblObsUrl.AutoSize = true;
            this.lblObsUrl.Location = new System.Drawing.Point(6, 48);
            this.lblObsUrl.Name = "lblObsUrl";
            this.lblObsUrl.Size = new System.Drawing.Size(63, 13);
            this.lblObsUrl.TabIndex = 1;
            this.lblObsUrl.Text = "Server URL:";
            //
            // txtObsUrl
            //
            this.txtObsUrl.Location = new System.Drawing.Point(94, 45);
            this.txtObsUrl.Name = "txtObsUrl";
            this.txtObsUrl.Size = new System.Drawing.Size(280, 20);
            this.txtObsUrl.TabIndex = 2;
            //
            // lblObsPassword
            //
            this.lblObsPassword.AutoSize = true;
            this.lblObsPassword.Location = new System.Drawing.Point(6, 77);
            this.lblObsPassword.Name = "lblObsPassword";
            this.lblObsPassword.Size = new System.Drawing.Size(56, 13);
            this.lblObsPassword.TabIndex = 3;
            this.lblObsPassword.Text = "Password:";
            //
            // txtObsPassword
            //
            this.txtObsPassword.Location = new System.Drawing.Point(94, 74);
            this.txtObsPassword.Name = "txtObsPassword";
            this.txtObsPassword.Size = new System.Drawing.Size(280, 20);
            this.txtObsPassword.TabIndex = 4;
            this.txtObsPassword.UseSystemPasswordChar = true;
            //
            // btnObsTest
            //
            this.btnObsTest.Location = new System.Drawing.Point(94, 103);
            this.btnObsTest.Name = "btnObsTest";
            this.btnObsTest.Size = new System.Drawing.Size(140, 28);
            this.btnObsTest.TabIndex = 5;
            this.btnObsTest.Text = "Test Connection";
            this.btnObsTest.UseVisualStyleBackColor = true;
            this.btnObsTest.Click += new System.EventHandler(this.TestObsConnection);
            //
            // chkLogEnabled
            //
            this.chkLogEnabled.AutoSize = true;
            this.chkLogEnabled.Location = new System.Drawing.Point(6, 139);
            this.chkLogEnabled.Name = "chkLogEnabled";
            this.chkLogEnabled.Size = new System.Drawing.Size(300, 17);
            this.chkLogEnabled.TabIndex = 6;
            this.chkLogEnabled.Text = "Log stream sessions to file";
            this.chkLogEnabled.UseVisualStyleBackColor = true;
            //
            // lblLogFolder
            //
            this.lblLogFolder.AutoSize = true;
            this.lblLogFolder.Location = new System.Drawing.Point(6, 168);
            this.lblLogFolder.Name = "lblLogFolder";
            this.lblLogFolder.Size = new System.Drawing.Size(60, 13);
            this.lblLogFolder.TabIndex = 7;
            this.lblLogFolder.Text = "Log folder:";
            //
            // txtLogFolder
            //
            this.txtLogFolder.Location = new System.Drawing.Point(94, 165);
            this.txtLogFolder.Name = "txtLogFolder";
            this.txtLogFolder.Size = new System.Drawing.Size(220, 20);
            this.txtLogFolder.TabIndex = 8;
            //
            // btnBrowseLogFolder
            //
            this.btnBrowseLogFolder.Location = new System.Drawing.Point(320, 163);
            this.btnBrowseLogFolder.Name = "btnBrowseLogFolder";
            this.btnBrowseLogFolder.Size = new System.Drawing.Size(60, 24);
            this.btnBrowseLogFolder.TabIndex = 9;
            this.btnBrowseLogFolder.Text = "Browse...";
            this.btnBrowseLogFolder.UseVisualStyleBackColor = true;
            this.btnBrowseLogFolder.Click += new System.EventHandler(this.BrowseLogFolder);
            //
            // lblLogHint
            //
            this.lblLogHint.AutoSize = true;
            this.lblLogHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblLogHint.Location = new System.Drawing.Point(6, 192);
            this.lblLogHint.Name = "lblLogHint";
            this.lblLogHint.Size = new System.Drawing.Size(400, 13);
            this.lblLogHint.TabIndex = 10;
            this.lblLogHint.Text = "Format: H:MM:SS Description. Also used when OBS\'s recording format doesn\'t support chapters.";
            //
            // lblLogOffset
            //
            this.lblLogOffset.AutoSize = true;
            this.lblLogOffset.Location = new System.Drawing.Point(6, 213);
            this.lblLogOffset.Name = "lblLogOffset";
            this.lblLogOffset.Size = new System.Drawing.Size(150, 13);
            this.lblLogOffset.TabIndex = 11;
            this.lblLogOffset.Text = "Timestamp offset (seconds):";
            //
            // txtLogOffsetSeconds
            //
            this.txtLogOffsetSeconds.Location = new System.Drawing.Point(160, 210);
            this.txtLogOffsetSeconds.Name = "txtLogOffsetSeconds";
            this.txtLogOffsetSeconds.Size = new System.Drawing.Size(50, 20);
            this.txtLogOffsetSeconds.TabIndex = 12;
            //
            // AutoStreamMarkerSettings
            //
            this.AutoSize = true;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "AutoStreamMarkerSettings";
            this.Size = new System.Drawing.Size(475, 459);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Avatar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkMarkResets;
        private System.Windows.Forms.CheckBox chkMarkEverySplit;
        private System.Windows.Forms.CheckBox chkNotificationsEnabled;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox Avatar;
        private System.Windows.Forms.Label Username;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkObsEnabled;
        private System.Windows.Forms.Label lblObsUrl;
        private System.Windows.Forms.TextBox txtObsUrl;
        private System.Windows.Forms.Label lblObsPassword;
        private System.Windows.Forms.TextBox txtObsPassword;
        private System.Windows.Forms.Button btnObsTest;
        private System.Windows.Forms.CheckBox chkLogEnabled;
        private System.Windows.Forms.Label lblLogFolder;
        private System.Windows.Forms.TextBox txtLogFolder;
        private System.Windows.Forms.Button btnBrowseLogFolder;
        private System.Windows.Forms.Label lblLogHint;
        private System.Windows.Forms.Label lblLogOffset;
        private System.Windows.Forms.TextBox txtLogOffsetSeconds;
    }
}
