namespace Zoomer
{
    public partial class HotkeyEditor : Form
    {
        private readonly Config _config;
        private readonly Dictionary<byte, ZoomerHotkey> _hotkeys; // Localized hotkey dict
        private ZoomerKey _selectedHotkey;
        private ZoomerKey _selectedAction;
        public static readonly IReadOnlyDictionary<byte, string> DefaultKeys = new Dictionary<byte, string>() // Pre-Defined Keylist
        {
            // https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
            [0x00] = "Custom",
            [0x70] = "F1",
            [0x71] = "F2",
            [0x72] = "F3",
            [0x73] = "F4",
            [0x74] = "F5",
            [0x75] = "F6",
            [0x76] = "F7",
            [0x77] = "F8",
            [0x78] = "F9",
            [0x79] = "F10",
            [0x7A] = "F11",
            [0x7B] = "F12", // Cannot be used as hotkey
            [0x08] = "BACKSPACE",
            [0x0D] = "ENTER",
            [0xBB] = "PLUS",
            [0xBD] = "MINUS",
            [0x09] = "TAB",
            [0x20] = "SPACE",
            [0x2D] = "INSERT",
            [0x2E] = "DEL",
            [0x21] = "PG UP",
            [0x22] = "PG DN",
            [0x23] = "END",
            [0x24] = "HOME",
            [0x25] = "LEFT",
            [0x26] = "UP",
            [0x27] = "RIGHT",
            [0x28] = "DOWN",
            [(byte)MouseScrollDirection.Down] = "Mouse Scr Down", // Cannot be used as hotkey
            [(byte)MouseScrollDirection.Up] = "Mouse Scr Up" // Cannot be used as hotkey
        };

        public HotkeyEditor(Config config)
        {
            InitializeComponent();
            _config = config;
            _hotkeys = new Dictionary<byte, ZoomerHotkey>();
            foreach (var entry in DefaultKeys) // Load dropdown menus with available keys
            {
                switch (entry.Key)
                {
                    case 0x7B: // F12 System Reserved
                        break;
                    case (byte)MouseScrollDirection.Down: // Reserved for action only, not valid for hotkey
                        break;
                    case (byte)MouseScrollDirection.Up: // Reserved for action only, not valid for hotkey
                        break;
                    default:
                        this.comboBox_Hotkey.Items.Add(new ZoomerKey() { Value = entry.Key }); // Hotkeys
                        break;
                }
                this.comboBox_Action.Items.Add(new ZoomerKey() { Value = entry.Key }); // Actions
            }
            foreach (var entry in config.Hotkeys) // Load existing hotkeys into local dict
            {
                this.listBox_Hotkeys.Items.Add(entry.Value);
                _hotkeys.Add(entry.Key, entry.Value);
            }
        }
        // Button Controls
        private void button_Save_Click(object sender, EventArgs e) // Save Hotkeys to Config
        {
            _config.Hotkeys = _hotkeys;
            this.DialogResult = DialogResult.OK;
        }
        private void button_Cancel_Click(object sender, EventArgs e) // No changes to Config
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button_Add_Click(object sender, EventArgs e) // Add local hotkey entry
        {
            try
            {
                if (_selectedHotkey is null || _selectedAction is null) return;
                ZoomerKey hotkey, action;
                if (_selectedHotkey.Value == 0x00) // Custom Hotkey
                {
                    if (TryConvertHexToByte(this.textBox_CustomHotkey.Text.Trim(), out byte key))
                    {
                        if (key == 0xFF || key == 0xFE || key == 0x7B) // Prevent reserved hotkeys from being used
                        {
                            MessageBox.Show("Invalid Hotkey Value - Reserved", "Zoomer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        hotkey = new ZoomerKey()
                        {
                            Value = key,
                            HasCtrl = checkBox_HkControl.Checked,
                            HasAlt = checkBox_HkAlt.Checked,
                            HasShift = checkBox_HkShift.Checked
                        };
                    }
                    else
                    {
                        MessageBox.Show("Invalid Virtual Key Hex Value", "Zoomer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else hotkey = new ZoomerKey()
                {
                    Value = _selectedHotkey.Value,
                    HasCtrl = checkBox_HkControl.Checked,
                    HasAlt = checkBox_HkAlt.Checked,
                    HasShift = checkBox_HkShift.Checked
                }; // Pre-Defined Hotkey
                if (_selectedAction.Value == 0x00) // Custom Action
                {
                    if (TryConvertHexToByte(this.textBox_CustomAction.Text.Trim(), out byte key)) action = new ZoomerKey()
                    {
                        Value = key,
                        HasCtrl = checkBox_AcCtrl.Checked,
                        HasAlt = checkBox_AcAlt.Checked,
                        HasShift = checkBox_AcShift.Checked
                    };
                    else
                    {
                        MessageBox.Show("Invalid Virtual Key Hex Value", "Zoomer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else action = new ZoomerKey()
                {
                    Value = _selectedAction.Value,
                    HasCtrl = checkBox_AcCtrl.Checked,
                    HasAlt = checkBox_AcAlt.Checked,
                    HasShift = checkBox_AcShift.Checked
                }; // Pre-Defined Action
                if (_hotkeys.ContainsKey(hotkey.Value)) // Make sure hotkey isn't used twice
                {
                    MessageBox.Show("This hotkey is already being used!", "Zoomer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var result = new ZoomerHotkey
                {
                    Hotkey = hotkey,
                    Action = action
                };
                this.listBox_Hotkeys.Items.Add(result); // Add to Listbox
                _hotkeys.Add(hotkey.Value, result); // Add to Local Dictionary
            }
            finally
            {
                ResetMenus();
            }
        }

        private void button_Remove_Click(object sender, EventArgs e) // Remove local hotkey entry
        {
            if (this.listBox_Hotkeys.SelectedItem is null) return;
            var selectedItem = (ZoomerHotkey)this.listBox_Hotkeys.SelectedItem;
            _hotkeys.Remove(selectedItem.Hotkey.Value);
            this.listBox_Hotkeys.Items.Remove(this.listBox_Hotkeys.SelectedItem);
        }
        // combo box controls
        private void comboBox_Hotkey_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox_Hotkey.SelectedItem is null)
            {
                _selectedHotkey = null;
                return;
            }
            _selectedHotkey = (ZoomerKey)this.comboBox_Hotkey.SelectedItem;
            if (_selectedHotkey.Value == 0x00) this.textBox_CustomHotkey.Enabled = true;
            else this.textBox_CustomHotkey.Enabled = false;
        }

        private void comboBox_Action_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox_Action.SelectedItem is null)
            {
                _selectedAction = null;
                return;
            }
            _selectedAction = (ZoomerKey)this.comboBox_Action.SelectedItem;
            if (_selectedAction.Value == 0x00) this.textBox_CustomAction.Enabled = true;
            else this.textBox_CustomAction.Enabled = false;
        }

        // helper methods
        private void ResetMenus()
        {
            this.comboBox_Action.SelectedIndex = -1;
            this.comboBox_Hotkey.SelectedIndex = -1;
            this.textBox_CustomHotkey.Text = "0x00";
            this.textBox_CustomAction.Text = "0x00";
            this.textBox_CustomHotkey.Enabled = false;
            this.textBox_CustomAction.Enabled = false;
            this.checkBox_AcAlt.Checked = false;
            this.checkBox_AcCtrl.Checked = false;
            this.checkBox_AcShift.Checked = false;
            this.checkBox_HkAlt.Checked = false;
            this.checkBox_HkControl.Checked = false;
            this.checkBox_HkShift.Checked = false;
        }

        private static bool TryConvertHexToByte(string hex, out byte result)
        {
            if (hex.StartsWith("0x", StringComparison.OrdinalIgnoreCase)) hex = hex.Remove(0, 2);
            if (byte.TryParse(String.Format("{0:X}", hex), System.Globalization.NumberStyles.HexNumber, null, out result)) return true;
            else return false;
        }
    }
}
