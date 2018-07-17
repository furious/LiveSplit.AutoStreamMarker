namespace LiveSplit.UI.Components
{
    partial class TwitchHighlighterSettings
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
            this.tabc_th = new System.Windows.Forms.TabControl();
            this.tab_th_settings = new System.Windows.Forms.TabPage();
            this.chkNotify = new System.Windows.Forms.CheckBox();
            this.chkMarkEverySplit = new System.Windows.Forms.CheckBox();
            this.chkMarkResets = new System.Windows.Forms.CheckBox();
            this.Username = new System.Windows.Forms.Label();
            this.tab_th_login = new System.Windows.Forms.TabPage();
            this.Browser = new System.Windows.Forms.WebBrowser();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Avatar = new System.Windows.Forms.PictureBox();
            this.Channels = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabc_th.SuspendLayout();
            this.tab_th_settings.SuspendLayout();
            this.tab_th_login.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Avatar)).BeginInit();
            this.SuspendLayout();
            // 
            // tabc_th
            // 
            this.tabc_th.Controls.Add(this.tab_th_settings);
            this.tabc_th.Controls.Add(this.tab_th_login);
            this.tabc_th.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabc_th.Location = new System.Drawing.Point(0, 0);
            this.tabc_th.Name = "tabc_th";
            this.tabc_th.SelectedIndex = 0;
            this.tabc_th.Size = new System.Drawing.Size(475, 530);
            this.tabc_th.TabIndex = 4;
            this.tabc_th.SelectedIndexChanged += new System.EventHandler(this.ShowLogin);
            // 
            // tab_th_settings
            // 
            this.tab_th_settings.Controls.Add(this.label1);
            this.tab_th_settings.Controls.Add(this.groupBox2);
            this.tab_th_settings.Controls.Add(this.groupBox1);
            this.tab_th_settings.Location = new System.Drawing.Point(4, 22);
            this.tab_th_settings.Name = "tab_th_settings";
            this.tab_th_settings.Padding = new System.Windows.Forms.Padding(3);
            this.tab_th_settings.Size = new System.Drawing.Size(467, 504);
            this.tab_th_settings.TabIndex = 0;
            this.tab_th_settings.Text = "Settings";
            this.tab_th_settings.UseVisualStyleBackColor = true;
            // 
            // chkNotify
            // 
            this.chkNotify.AutoSize = true;
            this.chkNotify.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkNotify.Location = new System.Drawing.Point(3, 16);
            this.chkNotify.Name = "chkNotify";
            this.chkNotify.Size = new System.Drawing.Size(455, 17);
            this.chkNotify.TabIndex = 9;
            this.chkNotify.Text = "Show notifications in the TrayIcon";
            this.chkNotify.UseVisualStyleBackColor = true;
            // 
            // chkMarkEverySplit
            // 
            this.chkMarkEverySplit.AutoSize = true;
            this.chkMarkEverySplit.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkMarkEverySplit.Location = new System.Drawing.Point(3, 33);
            this.chkMarkEverySplit.Name = "chkMarkEverySplit";
            this.chkMarkEverySplit.Size = new System.Drawing.Size(455, 17);
            this.chkMarkEverySplit.TabIndex = 8;
            this.chkMarkEverySplit.Text = "Mark every split in the VOD";
            this.chkMarkEverySplit.UseVisualStyleBackColor = true;
            // 
            // chkMarkResets
            // 
            this.chkMarkResets.AutoSize = true;
            this.chkMarkResets.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkMarkResets.Location = new System.Drawing.Point(3, 50);
            this.chkMarkResets.Name = "chkMarkResets";
            this.chkMarkResets.Size = new System.Drawing.Size(455, 17);
            this.chkMarkResets.TabIndex = 7;
            this.chkMarkResets.Text = "Mark resets in the VOD";
            this.chkMarkResets.UseVisualStyleBackColor = true;
            // 
            // Username
            // 
            this.Username.AutoSize = true;
            this.Username.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Username.Location = new System.Drawing.Point(62, 33);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(150, 20);
            this.Username.TabIndex = 4;
            this.Username.Text = "User: Not Logged";
            // 
            // tab_th_login
            // 
            this.tab_th_login.Controls.Add(this.richTextBox1);
            this.tab_th_login.Controls.Add(this.Browser);
            this.tab_th_login.Location = new System.Drawing.Point(4, 22);
            this.tab_th_login.Name = "tab_th_login";
            this.tab_th_login.Padding = new System.Windows.Forms.Padding(3);
            this.tab_th_login.Size = new System.Drawing.Size(467, 504);
            this.tab_th_login.TabIndex = 1;
            this.tab_th_login.Text = "Login";
            this.tab_th_login.UseVisualStyleBackColor = true;
            // 
            // Browser
            // 
            this.Browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Browser.Location = new System.Drawing.Point(3, 3);
            this.Browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.Browser.Name = "Browser";
            this.Browser.ScriptErrorsSuppressed = true;
            this.Browser.ScrollBarsEnabled = false;
            this.Browser.Size = new System.Drawing.Size(461, 498);
            this.Browser.TabIndex = 2;
            this.Browser.TabStop = false;
            this.Browser.Url = new System.Uri("", System.UriKind.Relative);
            this.Browser.Visible = false;
            this.Browser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.Navigating);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Channels);
            this.groupBox1.Controls.Add(this.Avatar);
            this.groupBox1.Controls.Add(this.Username);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 79);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Account";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkMarkResets);
            this.groupBox2.Controls.Add(this.chkMarkEverySplit);
            this.groupBox2.Controls.Add(this.chkNotify);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(461, 69);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // Avatar
            // 
            this.Avatar.BackColor = System.Drawing.Color.LightGray;
            this.Avatar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Avatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Avatar.Location = new System.Drawing.Point(6, 19);
            this.Avatar.Name = "Avatar";
            this.Avatar.Size = new System.Drawing.Size(50, 50);
            this.Avatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Avatar.TabIndex = 5;
            this.Avatar.TabStop = false;
            // 
            // Channels
            // 
            this.Channels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Channels.Enabled = false;
            this.Channels.FormattingEnabled = true;
            this.Channels.Location = new System.Drawing.Point(274, 48);
            this.Channels.Name = "Channels";
            this.Channels.Size = new System.Drawing.Size(181, 21);
            this.Channels.TabIndex = 6;
            this.Channels.Visible = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(461, 15);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "We need a first-party token, that\'s why we\'re getting Twitch Dev Portal credentia" +
    "ls";
            // 
            // label1
            // 
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(3, 488);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(461, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "This plugin was made by FURiOUS, if you like, follow/sub: http://twitch.tv/furiou" +
    "s";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Click += new System.EventHandler(this.TwitchChannel);
            // 
            // TwitchHighlighterSettings
            // 
            this.AutoSize = true;
            this.Controls.Add(this.tabc_th);
            this.Name = "TwitchHighlighterSettings";
            this.Size = new System.Drawing.Size(475, 530);
            this.tabc_th.ResumeLayout(false);
            this.tab_th_settings.ResumeLayout(false);
            this.tab_th_login.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Avatar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabc_th;
        private System.Windows.Forms.TabPage tab_th_settings;
        private System.Windows.Forms.TabPage tab_th_login;
        private System.Windows.Forms.WebBrowser Browser;
        private System.Windows.Forms.Label Username;
        private System.Windows.Forms.CheckBox chkNotify;
        private System.Windows.Forms.CheckBox chkMarkEverySplit;
        private System.Windows.Forms.CheckBox chkMarkResets;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox Avatar;
        private System.Windows.Forms.ComboBox Channels;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
    }
}
