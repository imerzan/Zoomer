namespace Zoomer
{
    partial class HotkeyEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HotkeyEditor));
            this.comboBox_Hotkey = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_CustomAction = new System.Windows.Forms.TextBox();
            this.textBox_CustomHotkey = new System.Windows.Forms.TextBox();
            this.listBox_Hotkeys = new System.Windows.Forms.ListBox();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Remove = new System.Windows.Forms.Button();
            this.button_Add = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_Action = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_AcCtrl = new System.Windows.Forms.CheckBox();
            this.checkBox_AcAlt = new System.Windows.Forms.CheckBox();
            this.checkBox_AcShift = new System.Windows.Forms.CheckBox();
            this.checkBox_HkShift = new System.Windows.Forms.CheckBox();
            this.checkBox_HkAlt = new System.Windows.Forms.CheckBox();
            this.checkBox_HkControl = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_Hotkey
            // 
            this.comboBox_Hotkey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Hotkey.FormattingEnabled = true;
            this.comboBox_Hotkey.Location = new System.Drawing.Point(15, 37);
            this.comboBox_Hotkey.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBox_Hotkey.Name = "comboBox_Hotkey";
            this.comboBox_Hotkey.Size = new System.Drawing.Size(150, 23);
            this.comboBox_Hotkey.TabIndex = 1;
            this.comboBox_Hotkey.SelectedIndexChanged += new System.EventHandler(this.comboBox_Hotkey_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_HkShift);
            this.groupBox1.Controls.Add(this.checkBox_HkAlt);
            this.groupBox1.Controls.Add(this.checkBox_HkControl);
            this.groupBox1.Controls.Add(this.checkBox_AcShift);
            this.groupBox1.Controls.Add(this.checkBox_AcAlt);
            this.groupBox1.Controls.Add(this.checkBox_AcCtrl);
            this.groupBox1.Controls.Add(this.textBox_CustomAction);
            this.groupBox1.Controls.Add(this.textBox_CustomHotkey);
            this.groupBox1.Controls.Add(this.listBox_Hotkeys);
            this.groupBox1.Controls.Add(this.button_Cancel);
            this.groupBox1.Controls.Add(this.button_Save);
            this.groupBox1.Controls.Add(this.button_Remove);
            this.groupBox1.Controls.Add(this.button_Add);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox_Action);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox_Hotkey);
            this.groupBox1.Location = new System.Drawing.Point(14, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(535, 308);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Edit";
            // 
            // textBox_CustomAction
            // 
            this.textBox_CustomAction.Enabled = false;
            this.textBox_CustomAction.Location = new System.Drawing.Point(201, 89);
            this.textBox_CustomAction.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_CustomAction.MaxLength = 4;
            this.textBox_CustomAction.Name = "textBox_CustomAction";
            this.textBox_CustomAction.Size = new System.Drawing.Size(56, 23);
            this.textBox_CustomAction.TabIndex = 12;
            this.textBox_CustomAction.Text = "0x00";
            // 
            // textBox_CustomHotkey
            // 
            this.textBox_CustomHotkey.Enabled = false;
            this.textBox_CustomHotkey.Location = new System.Drawing.Point(15, 89);
            this.textBox_CustomHotkey.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_CustomHotkey.MaxLength = 4;
            this.textBox_CustomHotkey.Name = "textBox_CustomHotkey";
            this.textBox_CustomHotkey.Size = new System.Drawing.Size(56, 23);
            this.textBox_CustomHotkey.TabIndex = 11;
            this.textBox_CustomHotkey.Text = "0x00";
            // 
            // listBox_Hotkeys
            // 
            this.listBox_Hotkeys.FormattingEnabled = true;
            this.listBox_Hotkeys.ItemHeight = 15;
            this.listBox_Hotkeys.Location = new System.Drawing.Point(15, 119);
            this.listBox_Hotkeys.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.listBox_Hotkeys.Name = "listBox_Hotkeys";
            this.listBox_Hotkeys.Size = new System.Drawing.Size(336, 169);
            this.listBox_Hotkeys.TabIndex = 10;
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(453, 243);
            this.button_Cancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(65, 45);
            this.button_Cancel.TabIndex = 9;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(381, 243);
            this.button_Save.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(65, 45);
            this.button_Save.TabIndex = 8;
            this.button_Save.Text = "Save";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button_Remove
            // 
            this.button_Remove.Location = new System.Drawing.Point(445, 53);
            this.button_Remove.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_Remove.Name = "button_Remove";
            this.button_Remove.Size = new System.Drawing.Size(64, 27);
            this.button_Remove.TabIndex = 7;
            this.button_Remove.Text = "Remove";
            this.button_Remove.UseVisualStyleBackColor = true;
            this.button_Remove.Click += new System.EventHandler(this.button_Remove_Click);
            // 
            // button_Add
            // 
            this.button_Add.Location = new System.Drawing.Point(381, 53);
            this.button_Add.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(57, 27);
            this.button_Add.TabIndex = 6;
            this.button_Add.Text = "Add";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Action";
            // 
            // comboBox_Action
            // 
            this.comboBox_Action.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Action.FormattingEnabled = true;
            this.comboBox_Action.Location = new System.Drawing.Point(201, 37);
            this.comboBox_Action.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBox_Action.Name = "comboBox_Action";
            this.comboBox_Action.Size = new System.Drawing.Size(150, 23);
            this.comboBox_Action.TabIndex = 4;
            this.comboBox_Action.SelectedIndexChanged += new System.EventHandler(this.comboBox_Action_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Hotkey";
            // 
            // checkBox_AcCtrl
            // 
            this.checkBox_AcCtrl.AutoSize = true;
            this.checkBox_AcCtrl.Location = new System.Drawing.Point(201, 66);
            this.checkBox_AcCtrl.Name = "checkBox_AcCtrl";
            this.checkBox_AcCtrl.Size = new System.Drawing.Size(45, 19);
            this.checkBox_AcCtrl.TabIndex = 13;
            this.checkBox_AcCtrl.Text = "Ctrl";
            this.checkBox_AcCtrl.UseVisualStyleBackColor = true;
            // 
            // checkBox_AcAlt
            // 
            this.checkBox_AcAlt.AutoSize = true;
            this.checkBox_AcAlt.Location = new System.Drawing.Point(252, 66);
            this.checkBox_AcAlt.Name = "checkBox_AcAlt";
            this.checkBox_AcAlt.Size = new System.Drawing.Size(41, 19);
            this.checkBox_AcAlt.TabIndex = 14;
            this.checkBox_AcAlt.Text = "Alt";
            this.checkBox_AcAlt.UseVisualStyleBackColor = true;
            // 
            // checkBox_AcShift
            // 
            this.checkBox_AcShift.AutoSize = true;
            this.checkBox_AcShift.Location = new System.Drawing.Point(299, 66);
            this.checkBox_AcShift.Name = "checkBox_AcShift";
            this.checkBox_AcShift.Size = new System.Drawing.Size(50, 19);
            this.checkBox_AcShift.TabIndex = 15;
            this.checkBox_AcShift.Text = "Shift";
            this.checkBox_AcShift.UseVisualStyleBackColor = true;
            // 
            // checkBox_HkShift
            // 
            this.checkBox_HkShift.AutoSize = true;
            this.checkBox_HkShift.Location = new System.Drawing.Point(113, 66);
            this.checkBox_HkShift.Name = "checkBox_HkShift";
            this.checkBox_HkShift.Size = new System.Drawing.Size(50, 19);
            this.checkBox_HkShift.TabIndex = 18;
            this.checkBox_HkShift.Text = "Shift";
            this.checkBox_HkShift.UseVisualStyleBackColor = true;
            // 
            // checkBox_HkAlt
            // 
            this.checkBox_HkAlt.AutoSize = true;
            this.checkBox_HkAlt.Location = new System.Drawing.Point(66, 66);
            this.checkBox_HkAlt.Name = "checkBox_HkAlt";
            this.checkBox_HkAlt.Size = new System.Drawing.Size(41, 19);
            this.checkBox_HkAlt.TabIndex = 17;
            this.checkBox_HkAlt.Text = "Alt";
            this.checkBox_HkAlt.UseVisualStyleBackColor = true;
            // 
            // checkBox_HkControl
            // 
            this.checkBox_HkControl.AutoSize = true;
            this.checkBox_HkControl.Location = new System.Drawing.Point(15, 66);
            this.checkBox_HkControl.Name = "checkBox_HkControl";
            this.checkBox_HkControl.Size = new System.Drawing.Size(45, 19);
            this.checkBox_HkControl.TabIndex = 16;
            this.checkBox_HkControl.Text = "Ctrl";
            this.checkBox_HkControl.UseVisualStyleBackColor = true;
            // 
            // HotkeyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 330);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HotkeyEditor";
            this.Text = "Hotkey Editor";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox_Hotkey;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_Remove;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_Action;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.ListBox listBox_Hotkeys;
        private System.Windows.Forms.TextBox textBox_CustomAction;
        private System.Windows.Forms.TextBox textBox_CustomHotkey;
        private CheckBox checkBox_AcShift;
        private CheckBox checkBox_AcAlt;
        private CheckBox checkBox_AcCtrl;
        private CheckBox checkBox_HkShift;
        private CheckBox checkBox_HkAlt;
        private CheckBox checkBox_HkControl;
    }
}