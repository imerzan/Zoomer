namespace Zoomer
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_DestWindow = new System.Windows.Forms.Label();
            this.textBox_DestWindow = new System.Windows.Forms.TextBox();
            this.button_Connections = new System.Windows.Forms.Button();
            this.checkBox_PwShow = new System.Windows.Forms.CheckBox();
            this.button_Run = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_Password = new System.Windows.Forms.TextBox();
            this.button_Hotkeys = new System.Windows.Forms.Button();
            this.radioButton_Server = new System.Windows.Forms.RadioButton();
            this.radioButton_Client = new System.Windows.Forms.RadioButton();
            this.label_ConnectionStatus = new System.Windows.Forms.Label();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_WOLSend = new System.Windows.Forms.Button();
            this.textBox_MAC = new System.Windows.Forms.TextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wakeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Remote IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 125);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 60);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "MAC Address";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_DestWindow);
            this.groupBox1.Controls.Add(this.textBox_DestWindow);
            this.groupBox1.Controls.Add(this.button_Connections);
            this.groupBox1.Controls.Add(this.checkBox_PwShow);
            this.groupBox1.Controls.Add(this.button_Run);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_Password);
            this.groupBox1.Controls.Add(this.button_Hotkeys);
            this.groupBox1.Controls.Add(this.radioButton_Server);
            this.groupBox1.Controls.Add(this.radioButton_Client);
            this.groupBox1.Controls.Add(this.label_ConnectionStatus);
            this.groupBox1.Controls.Add(this.textBox_Port);
            this.groupBox1.Controls.Add(this.textBox_IP);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(14, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(433, 188);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Config";
            // 
            // label_DestWindow
            // 
            this.label_DestWindow.AutoSize = true;
            this.label_DestWindow.Location = new System.Drawing.Point(7, 159);
            this.label_DestWindow.Name = "label_DestWindow";
            this.label_DestWindow.Size = new System.Drawing.Size(82, 15);
            this.label_DestWindow.TabIndex = 18;
            this.label_DestWindow.Text = "Dest Window*";
            // 
            // textBox_DestWindow
            // 
            this.textBox_DestWindow.Location = new System.Drawing.Point(97, 156);
            this.textBox_DestWindow.MaxLength = 255;
            this.textBox_DestWindow.Name = "textBox_DestWindow";
            this.textBox_DestWindow.Size = new System.Drawing.Size(238, 23);
            this.textBox_DestWindow.TabIndex = 17;
            this.textBox_DestWindow.TextChanged += new System.EventHandler(this.textBox_DestWindow_TextChanged);
            // 
            // button_Connections
            // 
            this.button_Connections.Enabled = false;
            this.button_Connections.Location = new System.Drawing.Point(254, 18);
            this.button_Connections.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_Connections.Name = "button_Connections";
            this.button_Connections.Size = new System.Drawing.Size(88, 27);
            this.button_Connections.TabIndex = 16;
            this.button_Connections.Text = "Connections";
            this.button_Connections.UseVisualStyleBackColor = true;
            this.button_Connections.Click += new System.EventHandler(this.button_Connections_Click);
            // 
            // checkBox_PwShow
            // 
            this.checkBox_PwShow.AutoSize = true;
            this.checkBox_PwShow.Location = new System.Drawing.Point(281, 96);
            this.checkBox_PwShow.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkBox_PwShow.Name = "checkBox_PwShow";
            this.checkBox_PwShow.Size = new System.Drawing.Size(55, 19);
            this.checkBox_PwShow.TabIndex = 15;
            this.checkBox_PwShow.Text = "Show";
            this.checkBox_PwShow.UseVisualStyleBackColor = true;
            this.checkBox_PwShow.CheckedChanged += new System.EventHandler(this.checkBox_PwShow_CheckedChanged);
            // 
            // button_Run
            // 
            this.button_Run.Location = new System.Drawing.Point(350, 62);
            this.button_Run.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_Run.Name = "button_Run";
            this.button_Run.Size = new System.Drawing.Size(69, 50);
            this.button_Run.TabIndex = 14;
            this.button_Run.Text = "Run";
            this.button_Run.UseVisualStyleBackColor = true;
            this.button_Run.Click += new System.EventHandler(this.button_Run_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 95);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "Password";
            // 
            // textBox_Password
            // 
            this.textBox_Password.Location = new System.Drawing.Point(97, 92);
            this.textBox_Password.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_Password.MaxLength = 128;
            this.textBox_Password.Name = "textBox_Password";
            this.textBox_Password.Size = new System.Drawing.Size(177, 23);
            this.textBox_Password.TabIndex = 12;
            this.textBox_Password.UseSystemPasswordChar = true;
            this.textBox_Password.TextChanged += new System.EventHandler(this.textBox_Password_TextChanged);
            // 
            // button_Hotkeys
            // 
            this.button_Hotkeys.Location = new System.Drawing.Point(160, 18);
            this.button_Hotkeys.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_Hotkeys.Name = "button_Hotkeys";
            this.button_Hotkeys.Size = new System.Drawing.Size(88, 27);
            this.button_Hotkeys.TabIndex = 11;
            this.button_Hotkeys.Text = "Edit Hotkeys";
            this.button_Hotkeys.UseVisualStyleBackColor = true;
            this.button_Hotkeys.Click += new System.EventHandler(this.button_Hotkeys_Click);
            // 
            // radioButton_Server
            // 
            this.radioButton_Server.AutoSize = true;
            this.radioButton_Server.Location = new System.Drawing.Point(77, 22);
            this.radioButton_Server.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButton_Server.Name = "radioButton_Server";
            this.radioButton_Server.Size = new System.Drawing.Size(57, 19);
            this.radioButton_Server.TabIndex = 10;
            this.radioButton_Server.Text = "Server";
            this.radioButton_Server.UseVisualStyleBackColor = true;
            this.radioButton_Server.CheckedChanged += new System.EventHandler(this.radioButton_Server_CheckedChanged);
            // 
            // radioButton_Client
            // 
            this.radioButton_Client.AutoSize = true;
            this.radioButton_Client.Checked = true;
            this.radioButton_Client.Location = new System.Drawing.Point(10, 22);
            this.radioButton_Client.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioButton_Client.Name = "radioButton_Client";
            this.radioButton_Client.Size = new System.Drawing.Size(56, 19);
            this.radioButton_Client.TabIndex = 9;
            this.radioButton_Client.TabStop = true;
            this.radioButton_Client.Text = "Client";
            this.radioButton_Client.UseVisualStyleBackColor = true;
            this.radioButton_Client.CheckedChanged += new System.EventHandler(this.radioButton_Client_CheckedChanged);
            // 
            // label_ConnectionStatus
            // 
            this.label_ConnectionStatus.AutoSize = true;
            this.label_ConnectionStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label_ConnectionStatus.Location = new System.Drawing.Point(192, 128);
            this.label_ConnectionStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_ConnectionStatus.Name = "label_ConnectionStatus";
            this.label_ConnectionStatus.Size = new System.Drawing.Size(60, 15);
            this.label_ConnectionStatus.TabIndex = 7;
            this.label_ConnectionStatus.Text = "Stopped";
            // 
            // textBox_Port
            // 
            this.textBox_Port.Location = new System.Drawing.Point(97, 122);
            this.textBox_Port.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_Port.MaxLength = 5;
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(69, 23);
            this.textBox_Port.TabIndex = 6;
            this.textBox_Port.Text = "0";
            this.textBox_Port.TextChanged += new System.EventHandler(this.textBox_Port_TextChanged);
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(97, 62);
            this.textBox_IP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_IP.MaxLength = 20;
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(177, 23);
            this.textBox_IP.TabIndex = 5;
            this.textBox_IP.TextChanged += new System.EventHandler(this.textBox_IP_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_WOLSend);
            this.groupBox2.Controls.Add(this.textBox_MAC);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(14, 208);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(433, 115);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Wake On Lan";
            // 
            // button_WOLSend
            // 
            this.button_WOLSend.Location = new System.Drawing.Point(295, 42);
            this.button_WOLSend.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_WOLSend.Name = "button_WOLSend";
            this.button_WOLSend.Size = new System.Drawing.Size(65, 51);
            this.button_WOLSend.TabIndex = 7;
            this.button_WOLSend.Text = "Send";
            this.button_WOLSend.UseVisualStyleBackColor = true;
            this.button_WOLSend.Click += new System.EventHandler(this.button_WOLSend_Click);
            // 
            // textBox_MAC
            // 
            this.textBox_MAC.Location = new System.Drawing.Point(97, 57);
            this.textBox_MAC.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_MAC.MaxLength = 20;
            this.textBox_MAC.Name = "textBox_MAC";
            this.textBox_MAC.Size = new System.Drawing.Size(177, 23);
            this.textBox_MAC.TabIndex = 7;
            this.textBox_MAC.TextChanged += new System.EventHandler(this.textBox_MAC_TextChanged);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Zoomer";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.wakeToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(104, 76);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // wakeToolStripMenuItem
            // 
            this.wakeToolStripMenuItem.Name = "wakeToolStripMenuItem";
            this.wakeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.wakeToolStripMenuItem.Text = "Wake";
            this.wakeToolStripMenuItem.Click += new System.EventHandler(this.wakeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(100, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 331);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Zoomer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label_ConnectionStatus;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.Button button_WOLSend;
        private System.Windows.Forms.TextBox textBox_MAC;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wakeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioButton_Server;
        private System.Windows.Forms.RadioButton radioButton_Client;
        private System.Windows.Forms.Button button_Hotkeys;
        private System.Windows.Forms.Button button_Run;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_Password;
        private System.Windows.Forms.CheckBox checkBox_PwShow;
        private System.Windows.Forms.Button button_Connections;
        private Label label_DestWindow;
        private TextBox textBox_DestWindow;
        private ToolTip toolTip;
    }
}

