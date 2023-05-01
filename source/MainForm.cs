namespace Zoomer
{
    public partial class MainForm : Form
    {
        public delegate void ServerStatus(string msg);
        private readonly Config _config;
        private readonly WakeOnLan _wol;
        private readonly Zoomer _zoomer;
        private readonly System.Timers.Timer _timer;
        private bool _running;
        private bool IsRunning
        {
            get
            {
                return _running;
            }
            set
            {
                _running = value;
                GuiUpdate(value);
                Win32API.PreventSleep(value);
            }
        }
        
        public MainForm(Config config) // GUI Constructor
        {
            _config = config;
            InitializeComponent();
            this.Resize += MainForm_Resize;
            this.Shown += MainForm_Shown;
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            _running = false;
            _wol = new WakeOnLan();
            var del = new ServerStatus(del_ServerStatus);
            _zoomer = new Zoomer(this.Handle, config, del);
            _timer = new System.Timers.Timer(1000);
            _timer.AutoReset = true;
            _timer.Elapsed += this.CheckClientConnection;
            _timer.Start();
            toolTip.SetToolTip(label_DestWindow, "Functionality may be limited, does not fully replicate keyboard input. " +
                "Also, modifiers (Ctrl,Alt,Shift) will not work.");
            GuiFirstSet(); // Set initial control values
        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            if (_config.Mode is OperatingMode.Server) // Attempt silent server startup
            {
                this.IsRunning = await _zoomer.Run(true);
                GuiShow(!this.IsRunning); // Hide window if running
            }
        }

        private void MainForm_Resize(object sender, EventArgs e) // Hide GUI when minimized
        {
            if (this.WindowState == FormWindowState.Minimized) GuiShow(false);
        }

        // NotifyIcon (Tray Icon)
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) // Tray icon double click - restore GUI
        {
            GuiShow(true);
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e) // Tray 'open' menu item click - restore GUI
        {
            GuiShow(true);
        }

        private async void wakeToolStripMenuItem_Click(object sender, EventArgs e) // Tray 'wake' menu item click - send WakeOnLan broadcast
        {
            try
            {
                await _wol.SendAsync(this.textBox_MAC.Text.Trim().ToUpper());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) // Tray 'exit' menu item click - Exit GUI/Program
        {
            this.Close();
        }

        // Button Controls
        private async void button_Run_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                switch (this.button_Run.Text)
                {
                    case "Run":
                        this.label_ConnectionStatus.Text = "Starting...";
                        this.IsRunning = await _zoomer.Run();
                        break;
                    case "Stop":
                        this.label_ConnectionStatus.Text = "Stopping...";
                        this.IsRunning = _zoomer.Stop();
                        break;
                }
            }
            finally
            {
                await Task.Delay(333);
                this.Enabled = true;
            }
        }
        private async void button_WOLSend_Click(object sender, EventArgs e) // GUI Wake On Lan 'Send' button click - send WakeOnLan broadcast
        {
            try
            {
                await _wol.SendAsync(this.textBox_MAC.Text.Trim().ToUpper());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button_Hotkeys_Click(object sender, EventArgs e) // Loads GUI for Hotkey Editor
        {
            try
            {
                this.Enabled = false; // Disable parent form
                var EditHotkeys = new HotkeyEditor(_config);
                var dialog = EditHotkeys.ShowDialog(); // Load dialog form
            }
            finally
            {
                this.Enabled = true; // Enable parent form upon exit
            }

        }
        private void button_Connections_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false; // Disable parent form
                var clients = new ClientList(_zoomer);
                clients.ShowDialog(); // Load dialog form
            }
            finally
            {
                this.Enabled = true; // Enable parent form upon exit
            }
        }

        // Textbox Controls
        private void textBox_IP_TextChanged(object sender, EventArgs e) // Control changed, re-enable Install button and change text to 'Update'
        {
            _config.IPAddress = this.textBox_IP.Text;
        }

        private void textBox_Port_TextChanged(object sender, EventArgs e) // Control changed, re-enable Install button and change text to 'Update'
        {
            if (int.TryParse(this.textBox_Port.Text, out var port))
            {
                _config.Port = port;
            }
            else this.textBox_Port.Text = "0";
        }

        private void textBox_Password_TextChanged(object sender, EventArgs e)
        {
            _config.Password = this.textBox_Password.Text;
        }
        private void textBox_DestWindow_TextChanged(object sender, EventArgs e)
        {
            _config.WindowTitle = this.textBox_DestWindow.Text;
        }

        private void textBox_MAC_TextChanged(object sender, EventArgs e) // Control changed, re-enable Install button and change text to 'Update'
        {
            _config.WOL_Mac = this.textBox_MAC.Text;
        }
        // Radio Button / Checkbox Controls
        private void radioButton_Client_CheckedChanged(object sender, EventArgs e) // Control changed, re-enable Install button and change text to 'Update'
        {
            if (this.radioButton_Client.Checked)
            {
                _config.Mode = OperatingMode.Client;
                this.textBox_IP.Enabled = true;
            }
        }

        private void radioButton_Server_CheckedChanged(object sender, EventArgs e) // Control changed, re-enable Install button and change text to 'Update'
        {
            if (this.radioButton_Server.Checked)
            {
                _config.Mode = OperatingMode.Server;
                this.textBox_IP.Enabled = false;
            }
        }

        private void checkBox_PwShow_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_PwShow.Checked)
            {
                this.textBox_Password.UseSystemPasswordChar = false;
            }
            else this.textBox_Password.UseSystemPasswordChar = true;
        }

        // Custom Methods & Delegates

        private void GuiShow(bool IsVisible) // Shows/Hides GUI Window & Tray Icon
        {
            if (IsVisible)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.BringToFront();
            }
            else
            {
                this.Hide();
                this.notifyIcon1.ShowBalloonTip(2000, "Zoomer", "Minimized to tray.", ToolTipIcon.Info);
            }
        }
        private void GuiFirstSet() // Set initial GUI Values
        {
            this.textBox_IP.Text = _config.IPAddress;
            this.textBox_Port.Text = _config.Port.ToString();
            this.textBox_Password.Text = _config.Password;
            this.textBox_DestWindow.Text = _config.WindowTitle;
            this.textBox_MAC.Text = _config.WOL_Mac;
            switch (_config.Mode)
            {
                case OperatingMode.Client:
                    this.radioButton_Client.Checked = true;
                    break;
                case OperatingMode.Server:
                    this.radioButton_Server.Checked = true;
                    break;
            }
        }
        private void GuiUpdate(bool isRunning) // Update GUI State
        {
            if (isRunning)
            {
                if (_config.Mode == OperatingMode.Client)
                {
                    this.label_ConnectionStatus.Text = $"Connected to {_zoomer.ServerAddress}";
                    this.button_Connections.Enabled = false;
                }
                else // Server
                {
                    this.label_ConnectionStatus.Text = "Server - Listening...";
                    this.button_Connections.Enabled = true;
                }
                this.button_Run.Text = "Stop";
                this.radioButton_Client.Enabled = false;
                this.radioButton_Server.Enabled = false;
                this.textBox_IP.Enabled = false;
                this.textBox_Port.Enabled = false;
                this.textBox_Password.Enabled = false;
                this.textBox_DestWindow.Enabled = false;
                this.button_Hotkeys.Enabled = false;
            }
            else
            {
                this.button_Connections.Enabled = false;
                this.label_ConnectionStatus.Text = "Stopped";
                this.button_Run.Text = "Run";
                this.radioButton_Client.Enabled = true;
                this.radioButton_Server.Enabled = true;
                if (_config.Mode == OperatingMode.Client) this.textBox_IP.Enabled = true;
                this.textBox_Port.Enabled = true;
                this.textBox_Password.Enabled = true;
                this.textBox_DestWindow.Enabled = true;
                this.button_Hotkeys.Enabled = true;
            }
        }

        private void CheckClientConnection(object sender, ElapsedEventArgs e) // Poll client socket to make sure a connection is there
        {
            if (this.IsRunning && _config.Mode is OperatingMode.Client && !_zoomer.ClientConnected)
            {
                _running = false;
                this.IsRunning = _zoomer.Stop();
                this.notifyIcon1.ShowBalloonTip(2000, $"Zoomer {_config.Mode}", "Disconnected!", ToolTipIcon.Error);
            }
        }

        private void del_ServerStatus(string msg)
        {
            this.BeginInvoke(new MethodInvoker(delegate
            {
                this.notifyIcon1.ShowBalloonTip(2000, "Zoomer", msg, ToolTipIcon.Info);
            }));
        }

        // Overrides

        protected override void WndProc(ref Message m) // Process Hotkey Press
        {
            try
            {
                if (m.Msg == Win32API.WM_HOTKEY)
                {
                    byte hotkey = (byte)m.WParam.ToInt32(); // Hotkey vkey
                    string window = textBox_DestWindow.Text;
                    if (window is not null && window.Trim() == string.Empty)
                        window = null;
                    _zoomer.Transmit(_config.Hotkeys[hotkey].Action, window); // Get Actionkey vkey (value) from Dictionary, using Hotkey (key).
                }
            }
            catch { }
            finally { base.WndProc(ref m); }
        }

        protected override void OnFormClosing(FormClosingEventArgs e) // Raised on Close()
        {
            try
            {
                _timer?.Stop();
                _wol?.Dispose();
                _zoomer?.Stop();
                Config.SaveConfig(_config);
                this.notifyIcon1.Visible = false;
            }
            finally { base.OnFormClosing(e); }
        }
    }
}
