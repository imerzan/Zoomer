
namespace Zoomer
{
    partial class ClientList
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
            this.listBox_Clients = new System.Windows.Forms.ListBox();
            this.button_Kill = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox_Clients
            // 
            this.listBox_Clients.FormattingEnabled = true;
            this.listBox_Clients.Location = new System.Drawing.Point(12, 12);
            this.listBox_Clients.Name = "listBox_Clients";
            this.listBox_Clients.Size = new System.Drawing.Size(230, 173);
            this.listBox_Clients.TabIndex = 0;
            this.listBox_Clients.SelectedIndexChanged += new System.EventHandler(this.listBox_Clients_SelectedIndexChanged);
            // 
            // button_Kill
            // 
            this.button_Kill.Enabled = false;
            this.button_Kill.Location = new System.Drawing.Point(91, 191);
            this.button_Kill.Name = "button_Kill";
            this.button_Kill.Size = new System.Drawing.Size(68, 40);
            this.button_Kill.TabIndex = 1;
            this.button_Kill.Text = "Kill";
            this.button_Kill.UseVisualStyleBackColor = true;
            this.button_Kill.Click += new System.EventHandler(this.button_Kill_Click);
            // 
            // ClientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 237);
            this.Controls.Add(this.button_Kill);
            this.Controls.Add(this.listBox_Clients);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientList";
            this.Text = "Client List";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_Clients;
        private System.Windows.Forms.Button button_Kill;
    }
}