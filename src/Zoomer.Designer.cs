namespace Zoomer
{
    partial class Zoomer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Zoomer));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_Server = new System.Windows.Forms.RadioButton();
            this.radioButton_Client = new System.Windows.Forms.RadioButton();
            this.button_Uninstall = new System.Windows.Forms.Button();
            this.label_ConnectionStatus = new System.Windows.Forms.Label();
            this.button_Install = new System.Windows.Forms.Button();
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
            this.button_Hotkeys = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "MAC Address";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_Hotkeys);
            this.groupBox1.Controls.Add(this.radioButton_Server);
            this.groupBox1.Controls.Add(this.radioButton_Client);
            this.groupBox1.Controls.Add(this.button_Uninstall);
            this.groupBox1.Controls.Add(this.label_ConnectionStatus);
            this.groupBox1.Controls.Add(this.button_Install);
            this.groupBox1.Controls.Add(this.textBox_Port);
            this.groupBox1.Controls.Add(this.textBox_IP);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(329, 137);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Config";
            // 
            // radioButton_Server
            // 
            this.radioButton_Server.AutoSize = true;
            this.radioButton_Server.Location = new System.Drawing.Point(66, 19);
            this.radioButton_Server.Name = "radioButton_Server";
            this.radioButton_Server.Size = new System.Drawing.Size(56, 17);
            this.radioButton_Server.TabIndex = 10;
            this.radioButton_Server.Text = "Server";
            this.radioButton_Server.UseVisualStyleBackColor = true;
            this.radioButton_Server.CheckedChanged += new System.EventHandler(this.radioButton_Server_CheckedChanged);
            // 
            // radioButton_Client
            // 
            this.radioButton_Client.AutoSize = true;
            this.radioButton_Client.Checked = true;
            this.radioButton_Client.Location = new System.Drawing.Point(9, 19);
            this.radioButton_Client.Name = "radioButton_Client";
            this.radioButton_Client.Size = new System.Drawing.Size(51, 17);
            this.radioButton_Client.TabIndex = 9;
            this.radioButton_Client.TabStop = true;
            this.radioButton_Client.Text = "Client";
            this.radioButton_Client.UseVisualStyleBackColor = true;
            this.radioButton_Client.CheckedChanged += new System.EventHandler(this.radioButton_Client_CheckedChanged);
            // 
            // button_Uninstall
            // 
            this.button_Uninstall.Enabled = false;
            this.button_Uninstall.Location = new System.Drawing.Point(252, 103);
            this.button_Uninstall.Name = "button_Uninstall";
            this.button_Uninstall.Size = new System.Drawing.Size(57, 23);
            this.button_Uninstall.TabIndex = 8;
            this.button_Uninstall.Text = "Uninstall";
            this.button_Uninstall.UseVisualStyleBackColor = true;
            this.button_Uninstall.Click += new System.EventHandler(this.button_Uninstall_Click);
            // 
            // label_ConnectionStatus
            // 
            this.label_ConnectionStatus.AutoSize = true;
            this.label_ConnectionStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ConnectionStatus.Location = new System.Drawing.Point(6, 113);
            this.label_ConnectionStatus.Name = "label_ConnectionStatus";
            this.label_ConnectionStatus.Size = new System.Drawing.Size(73, 13);
            this.label_ConnectionStatus.TabIndex = 7;
            this.label_ConnectionStatus.Text = "Not Loaded";
            // 
            // button_Install
            // 
            this.button_Install.Location = new System.Drawing.Point(253, 53);
            this.button_Install.Name = "button_Install";
            this.button_Install.Size = new System.Drawing.Size(56, 44);
            this.button_Install.TabIndex = 5;
            this.button_Install.Text = "Install";
            this.button_Install.UseVisualStyleBackColor = true;
            this.button_Install.Click += new System.EventHandler(this.button_Install_Click);
            // 
            // textBox_Port
            // 
            this.textBox_Port.Location = new System.Drawing.Point(70, 83);
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(60, 20);
            this.textBox_Port.TabIndex = 6;
            this.textBox_Port.Text = "0";
            this.textBox_Port.TextChanged += new System.EventHandler(this.textBox_Port_TextChanged);
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(70, 53);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(152, 20);
            this.textBox_IP.TabIndex = 5;
            this.textBox_IP.TextChanged += new System.EventHandler(this.textBox_IP_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_WOLSend);
            this.groupBox2.Controls.Add(this.textBox_MAC);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(10, 155);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(331, 100);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Wake On Lan";
            // 
            // button_WOLSend
            // 
            this.button_WOLSend.Enabled = false;
            this.button_WOLSend.Location = new System.Drawing.Point(253, 36);
            this.button_WOLSend.Name = "button_WOLSend";
            this.button_WOLSend.Size = new System.Drawing.Size(56, 44);
            this.button_WOLSend.TabIndex = 7;
            this.button_WOLSend.Text = "Send";
            this.button_WOLSend.UseVisualStyleBackColor = true;
            this.button_WOLSend.Click += new System.EventHandler(this.button_WOLSend_Click);
            // 
            // textBox_MAC
            // 
            this.textBox_MAC.Location = new System.Drawing.Point(83, 36);
            this.textBox_MAC.Name = "textBox_MAC";
            this.textBox_MAC.Size = new System.Drawing.Size(152, 20);
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
            this.wakeToolStripMenuItem.Enabled = false;
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
            // button_Hotkeys
            // 
            this.button_Hotkeys.Location = new System.Drawing.Point(137, 16);
            this.button_Hotkeys.Name = "button_Hotkeys";
            this.button_Hotkeys.Size = new System.Drawing.Size(75, 23);
            this.button_Hotkeys.TabIndex = 11;
            this.button_Hotkeys.Text = "Edit Hotkeys";
            this.button_Hotkeys.UseVisualStyleBackColor = true;
            this.button_Hotkeys.Click += new System.EventHandler(this.button_Hotkeys_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 262);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
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
        private System.Windows.Forms.Button button_Install;
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
        private System.Windows.Forms.Button button_Uninstall;
        private System.Windows.Forms.RadioButton radioButton_Server;
        private System.Windows.Forms.RadioButton radioButton_Client;
        private System.Windows.Forms.Button button_Hotkeys;
    }
}

