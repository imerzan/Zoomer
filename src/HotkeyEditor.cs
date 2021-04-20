using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Zoomer
{
    public partial class HotkeyEditor : Form
    {
        public Dictionary<byte, Hotkey> Hotkeys { get; private set; } // Localized hotkey dict
        public HotkeyEditor(Dictionary<byte, Hotkey> in_hotkeys)
        {
            InitializeComponent();
            this.Hotkeys = new Dictionary<byte, Hotkey>();
            foreach (KeyValuePair<byte, string> entry in Globals.VirtualKeys) // Load dropdown menus with available keys
            {
                switch (entry.Key)
                {
                    case 0x7B: // F12 Reserved
                        break;
                    default:
                        this.comboBox_Hotkey.Items.Add(new Key(entry.Key)); // Hotkeys
                        break;
                }
                this.comboBox_Action.Items.Add(new Key(entry.Key)); // Actions
            }
            if (in_hotkeys != null) foreach (KeyValuePair<byte, Hotkey> entry in in_hotkeys) // Load existing hotkeys into local dict
            {
                this.listBox_Hotkeys.Items.Add(entry.Value);
                this.Hotkeys.Add(entry.Key, entry.Value);
            }
        }
        // Button Controls
        private void button_Save_Click(object sender, EventArgs e) // Save Hotkeys
        {
            this.DialogResult = DialogResult.OK;
        }
        private void button_Cancel_Click(object sender, EventArgs e) // No changes to global Zoomer.Hotkeys
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button_Add_Click(object sender, EventArgs e) // Add local hotkey entry
        {
            if (this.comboBox_Hotkey.SelectedItem == null | this.comboBox_Action.SelectedItem == null) return;
            var selectedHotkey = (Key)this.comboBox_Hotkey.SelectedItem; // Get selected Hotkey from dropdown
            var selectedAction = (Key)this.comboBox_Action.SelectedItem; // Get selected Action from dropdown
            Key hotkey, action; // declare locals
            int key; // declare local
            if (selectedHotkey.Value == 0x00) // Custom Hotkey - Uses custom user input (int) -> (byte)
            {
                if (!int.TryParse(this.textBox_CustomHotkey.Text, out key)) // Parse string value to integer
                {
                    MessageBox.Show("Invalid integer representation of character byte.", Globals.WindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else hotkey = new Key((byte)key);
            }
            else hotkey = new Key(selectedHotkey.Value); // Pre-Defined Hotkey
            if (selectedAction.Value == 0x00) // Custom Action - Uses custom user input (int) -> (byte)
            {
                if (!int.TryParse(this.textBox_CustomAction.Text, out key)) // Parse string value to integer
                {
                    MessageBox.Show("Invalid integer representation of character byte.", Globals.WindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else action = new Key((byte)key);
            }
            else action = new Key(selectedAction.Value); // Pre-Defined Action
            // All Ready! Add hotkey
            if (Hotkeys.ContainsKey(hotkey.Value)) // Make sure hotkey isn't used twice
            {
                MessageBox.Show("This hotkey is already being used!", Globals.WindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.comboBox_Action.SelectedIndex = -1;
                this.comboBox_Hotkey.SelectedIndex = -1;
                this.textBox_CustomHotkey.Text = "0";
                this.textBox_CustomAction.Text = "0";
                return;
            }
            this.listBox_Hotkeys.Items.Add(new Hotkey(hotkey, action));
            Hotkeys.Add(hotkey.Value, new Hotkey(hotkey, action));
            this.comboBox_Action.SelectedIndex = -1;
            this.comboBox_Hotkey.SelectedIndex = -1;
            this.textBox_CustomHotkey.Text = "0";
            this.textBox_CustomAction.Text = "0";
            this.textBox_CustomHotkey.Enabled = false;
            this.textBox_CustomAction.Enabled = false;
        }

        private void button_Remove_Click(object sender, EventArgs e) // Remove local hotkey entry
        {
            if (this.listBox_Hotkeys.SelectedItem == null) return;
            var selectedItem = (Hotkey)this.listBox_Hotkeys.SelectedItem;
            Hotkeys.Remove(selectedItem.hotkey.Value);
            this.listBox_Hotkeys.Items.Remove(this.listBox_Hotkeys.SelectedItem);
        }
        // combo box controls
        private void comboBox_Hotkey_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox_Hotkey.SelectedItem == null) return;
            var selectedItem = (Key)this.comboBox_Hotkey.SelectedItem;
            if (selectedItem.Value == 0x00) this.textBox_CustomHotkey.Enabled = true;
            else this.textBox_CustomHotkey.Enabled = false;
        }

        private void comboBox_Action_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox_Action.SelectedItem == null) return;
            var selectedItem = (Key)this.comboBox_Action.SelectedItem;
            if (selectedItem.Value == 0x00) this.textBox_CustomAction.Enabled = true;
            else this.textBox_CustomAction.Enabled = false;
        }
    }
}
