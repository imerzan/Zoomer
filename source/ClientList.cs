namespace Zoomer
{
    public partial class ClientList : Form
    {
        public ClientList(Zoomer zoomer)
        {
            InitializeComponent();
            foreach (var client in zoomer.ServerSessions)
            {
                listBox_Clients.Items.Add(client.Key);
            }
        }

        private void listBox_Clients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_Clients.SelectedIndex == -1) this.button_Kill.Enabled = false;
            else this.button_Kill.Enabled = true;
        }

        private void button_Kill_Click(object sender, EventArgs e)
        {
            var selectedItem = (ZoomerServerSession)this.listBox_Clients.SelectedItem;
            this.listBox_Clients.Items.Remove(this.listBox_Clients.SelectedItem);
            selectedItem.Dispose();
        }
    }
}
