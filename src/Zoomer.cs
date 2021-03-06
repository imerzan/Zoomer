using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace Zoomer
{
    public partial class Zoomer : Form
    {
        private ZoomerConfig Config;
        private OperatingMode Mode = OperatingMode.Client; // GUI RadioButtons State (mode)
        private Dictionary<byte, Hotkey> EditorHotkeys = new Dictionary<byte, Hotkey>();
        private bool allowshowdisplay = false;

        protected override void SetVisibleCore(bool value) // Hide window by default on startup
        {
            base.SetVisibleCore(this.allowshowdisplay ? value : this.allowshowdisplay);
        }
        public Zoomer() // GUI Constructor
        {
            InitializeComponent();
            this.Resize += Form1_Resize;
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            // Form constructed >> Load Config
            this.Config = new ZoomerConfig(this.Handle);
            this.GuiUpdate(); // Update GUI with loaded config
            if (!this.Config.IsReady) this.GuiShow(true); // Show GUI if no config loaded (ex: first run)
        }

        protected override void WndProc(ref Message m) // Process Hotkey Press
        {
            try
            {
                if (m.Msg == Win32API.WM_HOTKEY)
                {
                    byte hotkey = (byte)m.WParam.ToInt32(); // Hotkey vkey
                    this.Config.Client.Transmit(Encoding.ASCII.GetBytes(this.Config.Hotkeys[hotkey].action.Value.ToString())); // Get Actionkey vkey (value) from Dictionary, using Hotkey (key).
                }
            }
            catch {}
            finally { base.WndProc(ref m); }
        }
        private void Form1_Resize(object sender, EventArgs e) // Hide GUI when minimized
        {
            if (this.WindowState == FormWindowState.Minimized) GuiShow(false);
        }
        // NotifyIcon
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) // Tray icon double click - restore GUI
        {
            this.GuiShow(true);
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e) // Tray 'open' menu item click - restore GUI
        {
            this.GuiShow(true);
        }

        private void wakeToolStripMenuItem_Click(object sender, EventArgs e) // Tray 'wake' menu item click - send WakeOnLan broadcast
        {
            this.Config.WOL.Send();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) // Tray 'exit' menu item click - Exit GUI/Program
        {
            this.Close();
        }

        // Button Controls
        private void button_Install_Click(object sender, EventArgs e) // GUI 'install' button click
        {
            if (this.button_Install.Text == "Update")
            {
                var dialog = MessageBox.Show("Are you sure you want to overwrite the existing configuration?", Globals.WindowTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog != DialogResult.Yes) return;
            }
            this.Config.Install(this.textBox_IP.Text, this.textBox_Port.Text, this.textBox_MAC.Text, this.Mode, this.EditorHotkeys);
            this.EditorHotkeys = new Dictionary<byte, Hotkey>(); // Clear editor hotkeys
            this.GuiUpdate();
        }
        private void button_Uninstall_Click(object sender, EventArgs e) // GUI 'uninstall' button click
        {
            var dialog = MessageBox.Show("Are you sure you want to uninstall?", Globals.WindowTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog != DialogResult.Yes) return;
            this.Config.Uninstall();
            this.EditorHotkeys = new Dictionary<byte, Hotkey>(); // Clear editor hotkeys
            this.GuiUpdate();
        }
        private void button_WOLSend_Click(object sender, EventArgs e) // GUI Wake On Lan 'Send' button click - send WakeOnLan broadcast
        {
            this.Config.WOL.Send();
        }
        private void button_Hotkeys_Click(object sender, EventArgs e) // Loads GUI for Hotkey Editor
        {
            HotkeyEditor EditHotkeys = new HotkeyEditor(this.Config.Hotkeys);
            this.Enabled = false; // Disable parent form
            var dialog = EditHotkeys.ShowDialog(); // Load dialog form
            this.Enabled = true; // Enable parent form upon exit
            if (dialog == DialogResult.OK)
            {
                this.EditorHotkeys = EditHotkeys.Hotkeys; // Get hotkeys from editor
                this.button_Install.Enabled = true;
                this.button_Install.Text = "Update";
                MessageBox.Show("Hotkeys set! Click Update to save configuration.", Globals.WindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        // Textbox Controls
        private void textBox_IP_TextChanged(object sender, EventArgs e) // Control changed, re-enable Install button and change text to 'Update'
        {
            if (!this.button_Install.Enabled)
            {
                this.button_Install.Enabled = true;
                this.button_Install.Text = "Update";
            }
        }

        private void textBox_Port_TextChanged(object sender, EventArgs e) // Control changed, re-enable Install button and change text to 'Update'
        {
            if (!this.button_Install.Enabled)
            {
                this.button_Install.Enabled = true;
                this.button_Install.Text = "Update";
            }
        }

        private void textBox_MAC_TextChanged(object sender, EventArgs e) // Control changed, re-enable Install button and change text to 'Update'
        {
            if (!this.button_Install.Enabled)
            {
                this.button_Install.Enabled = true;
                this.button_Install.Text = "Update";
            }
        }
        // Radio Button Controls
        private void radioButton_Client_CheckedChanged(object sender, EventArgs e) // Control changed, re-enable Install button and change text to 'Update'
        {
            if (this.radioButton_Client.Checked)
            {
                this.Mode = OperatingMode.Client;
                this.textBox_IP.Enabled = true;
                this.button_Hotkeys.Enabled = true;
                if (!this.button_Install.Enabled)
                {
                    this.button_Install.Enabled = true;
                    this.button_Install.Text = "Update";
                }
            }
        }

        private void radioButton_Server_CheckedChanged(object sender, EventArgs e) // Control changed, re-enable Install button and change text to 'Update'
        {
            if (this.radioButton_Server.Checked)
            {
                this.Mode = OperatingMode.Server;
                this.textBox_IP.Enabled = false;
                this.button_Hotkeys.Enabled = false;
                if (!this.button_Install.Enabled)
                {
                    this.button_Install.Enabled = true;
                    this.button_Install.Text = "Update";
                }
            }
        }

        // Custom Methods

        private void GuiShow(bool IsVisible) // Shows/Hides GUI Window & Tray Icon
        {
            if (IsVisible)
            {
                this.allowshowdisplay = true;
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.BringToFront();
                this.notifyIcon1.Visible = false;
            }
            else
            {
                this.Hide();
                this.notifyIcon1.Visible = true;
            }
        }
        private void GuiUpdate()
        {
            this.textBox_IP.Text = this.Config.IpAddress;
            this.textBox_Port.Text = this.Config.Port.ToString();
            this.textBox_MAC.Text = this.Config.WOL_MacAddress;
            switch (this.Config.Mode)
            {
                case OperatingMode.Client:
                    this.radioButton_Client.Checked = true;
                    break;
                case OperatingMode.Server:
                    this.radioButton_Server.Checked = true;
                    break;
            }
            if (this.Config.IsReady)
            {
                this.label_ConnectionStatus.Text = "Running: " + this.Config.Mode;
                this.button_WOLSend.Enabled = true;
                this.wakeToolStripMenuItem.Enabled = true;
                this.button_Install.Enabled = false;
                this.button_Uninstall.Enabled = true;
            }
            else
            {
                this.label_ConnectionStatus.Text = "Not running";
                this.button_WOLSend.Enabled = false;
                this.wakeToolStripMenuItem.Enabled = false;
                this.button_Install.Enabled = true;
                this.button_Uninstall.Enabled = false;
            }
            this.button_Install.Text = "Install";
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                this.Config?.Dispose();
                this.notifyIcon1.Visible = false;
            }
            catch { }
            finally { base.OnFormClosing(e); }
        }
    }
}
