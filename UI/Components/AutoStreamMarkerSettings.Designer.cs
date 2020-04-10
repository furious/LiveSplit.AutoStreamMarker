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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.chkMarkResets = new System.Windows.Forms.CheckBox();
            this.chkMarkEverySplit = new System.Windows.Forms.CheckBox();
            this.chkNotify = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Connect = new System.Windows.Forms.Button();
            this.Channels = new System.Windows.Forms.ComboBox();
            this.Avatar = new System.Windows.Forms.PictureBox();
            this.Username = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Avatar)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkMarkResets);
            this.groupBox2.Controls.Add(this.chkMarkEverySplit);
            this.groupBox2.Controls.Add(this.chkNotify);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(475, 69);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // chkMarkResets
            // 
            this.chkMarkResets.AutoSize = true;
            this.chkMarkResets.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkMarkResets.Location = new System.Drawing.Point(3, 50);
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
            this.chkMarkEverySplit.Location = new System.Drawing.Point(3, 33);
            this.chkMarkEverySplit.Name = "chkMarkEverySplit";
            this.chkMarkEverySplit.Size = new System.Drawing.Size(469, 17);
            this.chkMarkEverySplit.TabIndex = 8;
            this.chkMarkEverySplit.Text = "Mark every split in the VOD";
            this.chkMarkEverySplit.UseVisualStyleBackColor = true;
            // 
            // chkNotify
            // 
            this.chkNotify.AutoSize = true;
            this.chkNotify.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkNotify.Location = new System.Drawing.Point(3, 16);
            this.chkNotify.Name = "chkNotify";
            this.chkNotify.Size = new System.Drawing.Size(469, 17);
            this.chkNotify.TabIndex = 9;
            this.chkNotify.Text = "Show notifications in the TrayIcon";
            this.chkNotify.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Connect);
            this.groupBox1.Controls.Add(this.Channels);
            this.groupBox1.Controls.Add(this.Avatar);
            this.groupBox1.Controls.Add(this.Username);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 128);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Account";
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(116, 53);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(181, 30);
            this.Connect.TabIndex = 7;
            this.Connect.Text = "Connect/Reconnect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.ShowLogin);
            // 
            // Channels
            // 
            this.Channels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Channels.Enabled = false;
            this.Channels.FormattingEnabled = true;
            this.Channels.Location = new System.Drawing.Point(115, 89);
            this.Channels.Name = "Channels";
            this.Channels.Size = new System.Drawing.Size(181, 21);
            this.Channels.TabIndex = 6;
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
            this.Username.Location = new System.Drawing.Point(112, 26);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(184, 24);
            this.Username.TabIndex = 4;
            this.Username.Text = "User: Not Logged";
            // 
            // AutoStreamMarkerSettings
            // 
            this.AutoSize = true;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "AutoStreamMarkerSettings";
            this.Size = new System.Drawing.Size(475, 530);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Avatar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkMarkResets;
        private System.Windows.Forms.CheckBox chkMarkEverySplit;
        private System.Windows.Forms.CheckBox chkNotify;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox Channels;
        private System.Windows.Forms.PictureBox Avatar;
        private System.Windows.Forms.Label Username;
        private System.Windows.Forms.Button Connect;
    }
}
