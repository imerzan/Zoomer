using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zoomer
{
    public class ZoomerConfig
    {
        public bool IsReady { get; private set; }
        public string IpAddress { get; private set; }
        public int Port { get; private set; }
        public string WOL_MacAddress { get; private set; }
        public OperatingMode Mode { get; private set; }
        public ZoomerClient Client { get; private set; }
        private ZoomerServer Server { get; set; }
        public WakeOnLan WOL { get; private set; }
        public Dictionary<byte, Hotkey> Hotkeys { get; private set; } // Active Hotkey List
        private IntPtr Handle; // Main window handle passed from Zoomer.cs
        private List<int> HotkeyIds; // Hotkey IDs currently bound by WIN32 API
        public ZoomerConfig(IntPtr hwnd)
        {
            this.IsReady = false;
            this.Handle = hwnd;
            this.Port = 0;
            this.Mode = OperatingMode.Client;
            this.LoadConfig();
        }
        private void LoadConfig() // Read existing configuration (if exists) from Registry.
        {
            try
            {
                using (RegistryKey read = Registry.CurrentUser.OpenSubKey(Globals.RegPath)) // Get Config
                {
                    if (read == null) return;
                    this.IpAddress = (string)read.GetValue("ipaddress");
                    this.Port = (int)read.GetValue("port");
                    this.WOL_MacAddress = (string)read.GetValue("macaddress");
                    this.Mode = (OperatingMode)read.GetValue("mode");
                    read.Close();
                }
                using (RegistryKey hotkeys = Registry.CurrentUser.OpenSubKey(Globals.RegPath + @"\Hotkeys")) // Get hotkey config
                {
                    if (hotkeys != null)
                    {
                        this.Hotkeys = new Dictionary<byte, Hotkey>();
                        string[] allvalues = hotkeys.GetValueNames();
                        foreach (string value in allvalues)
                        {
                            string read = (string)hotkeys.GetValue(value);
                            string[] split = read.Split(','); // Delimit Hotkey & Action by comma
                            if (split.Length == 2) // Make sure within bounds
                            {
                                int hotkey, action;
                                if (!int.TryParse(split[0], out hotkey)) break; // Invalid, exit if
                                if (!int.TryParse(split[1], out action)) break; // Invalid, exit if
                                this.Hotkeys.Add((byte)hotkey, new Hotkey(new Key((byte)hotkey), new Key((byte)action))); // Valid, Add to Main Hotkeys Dict
                            }
                        }
                        hotkeys.Close();
                    }
                }
                this.IsReady = true;
            }
            catch (Exception ex) // Handle general exceptions, show exception info to user.
            {
                this.IsReady = false;
                MessageBox.Show(ex.ToString(), Globals.WindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.SetState();
            }
        }
        private void SetHotkeys(bool HotkeysEnabled) // Set Hotkeys
        {
            try
            {
                if (this.HotkeyIds?.Count > 0) // Unreg old hotkeys
                {
                    foreach (int id in this.HotkeyIds)
                    {
                        Win32API.UnregisterHotKey(this.Handle, id);
                    }
                }
                this.HotkeyIds = new List<int>(); // Empty hotkey IDs List
                switch (HotkeysEnabled)
                {
                    case true: // register hotkeys
                        if (this.Hotkeys != null) foreach (KeyValuePair<byte, Hotkey> entry in this.Hotkeys)
                        {
                            Win32API.RegisterHotKey(this.Handle, (int)entry.Key, 0, (int)entry.Key);
                            this.HotkeyIds.Add((int)entry.Key);
                        }
                        break;
                    case false: // hotkeys already unreg'd , do nothing
                        break;
                }
            }
            catch (Exception ex) // Handle general exceptions, show exception info to user.
            {
                this.IsReady = false;
                MessageBox.Show(ex.ToString(), Globals.WindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetState() // Sets Client/Server State
        {
            try
            {
                this.Server?.Stop(); // Stop existing server (if any)
                if (!this.IsReady) this.SetHotkeys(false); // Un-reg hotkeys if not ready
                else // Is Ready
                {
                    this.WOL = new WakeOnLan(this.WOL_MacAddress); // WOL enabled for both modes
                    switch (this.Mode)
                    {
                        case OperatingMode.Client:
                            this.SetHotkeys(true);
                            this.Client = new ZoomerClient(this.IpAddress, this.Port);
                            break;
                        case OperatingMode.Server:
                            this.SetHotkeys(false);
                            this.Server = new ZoomerServer(this.Port);
                            break;
                    }
                }
            }
            catch (Exception ex) // Handle general exceptions, show exception info to user.
            {
                this.IsReady = false;
                MessageBox.Show(ex.ToString(), Globals.WindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public async Task Install(string ip, string port, string mac, OperatingMode mode, Dictionary<byte, Hotkey> editorhotkeys) // Apply current configuration and write to Registry.
        {
            try
            {
                this.IpAddress = ip;
                int tryport; if (!int.TryParse(port, out tryport)) throw new Exception("Invalid port specified!");
                else this.Port = tryport;
                this.WOL_MacAddress = mac;
                this.Mode = mode;
                using (RegistryKey write = Registry.CurrentUser.CreateSubKey(Globals.RegPath))
                {
                    write.SetValue("ipaddress", this.IpAddress);
                    write.SetValue("port", this.Port);
                    write.SetValue("macaddress", this.WOL_MacAddress);
                    write.SetValue("mode", (int)this.Mode);
                    write.Close();
                }
                using (RegistryKey hotkeys = Registry.CurrentUser.CreateSubKey(Globals.RegPath + @"\Hotkeys")) // Save hotkey configuration
                {
                    string[] allvalues = hotkeys.GetValueNames();
                    foreach (string value in allvalues) // Delete old hotkeys
                    {
                        hotkeys.DeleteValue(value);
                    }
                    if (editorhotkeys?.Count > 0) // Editor list has new entries, push these first
                    {
                        this.Hotkeys = new Dictionary<byte, Hotkey>();
                        foreach (KeyValuePair<byte, Hotkey> entry in editorhotkeys) this.Hotkeys.Add(entry.Key, entry.Value); // Push entries to main hotkey dict
                    }
                    int i = 1;
                    if (this.Hotkeys != null) foreach (KeyValuePair<byte, Hotkey> entry in this.Hotkeys) // Write new hotkeys
                    {
                        hotkeys.SetValue("hotkey" + i.ToString(), entry.Key.ToString() + "," + entry.Value.action.Value.ToString()); // Delimit Hotkey & Action by comma
                        i++;
                    }
                    hotkeys.Close();
                }
                this.IsReady = true;
            }
            catch (Exception ex) // Handle general exceptions, show exception info to user.
            {
                this.IsReady = false;
                MessageBox.Show(ex.ToString(), Globals.WindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.SetState();
            }
        }
        public async Task Uninstall() // Remove current configuration, and delete from Registry.
        {
            try
            {
                using (RegistryKey delete = Registry.CurrentUser)
                {
                    delete.DeleteSubKey(Globals.RegPath + @"\Hotkeys"); // Remove hotkeys subkey first, cannot recursively delete
                    delete.DeleteSubKey(Globals.RegPath);
                    delete.Close();
                }
                this.IpAddress = null;
                this.Port = 0;
                this.WOL_MacAddress = null;
                this.Mode = OperatingMode.Client;
                this.Hotkeys = new Dictionary<byte, Hotkey>();
                this.IsReady = false;
            }
            catch (Exception ex) // Handle general exceptions, show exception info to user.
            {
                this.IsReady = false;
                MessageBox.Show(ex.ToString(), Globals.WindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.SetState();
            }
        }
    }
    public class ZoomerClient
    {
        private IPEndPoint Endpoint;
        public ZoomerClient(string ip, int port)
        {
            this.Endpoint = new IPEndPoint(IPAddress.Parse(ip), port);
        }
        public async Task Transmit(byte[] actionkey) // Client Mode
        {
            try
            {
                using (Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
                {
                    client.SendTo(actionkey, this.Endpoint); // Transmits ActionKey byte array to specified IP Address & Port via UDP Datagram.
                    client.Close();
                }
            }
            catch { }
        }
    }
    public class ZoomerServer
    {
        private UdpClient Server;
        private IPEndPoint Endpoint;
        private Thread Worker;
        public ZoomerServer(int port)
        {
            this.Endpoint = new IPEndPoint(IPAddress.Any, port);
            this.Server = new UdpClient(port);
            this.Worker = new Thread(this.Listen);
            this.Worker.IsBackground = true;
            this.Worker.Start();
        }
        private void Listen() // Server Mode
        {
            try
            {
                Win32API.PreventSleep(); // Prevent windows from going to sleep while the server thread is active.
                byte[] buffer; // UDP Receive Buffer
                int key; // Key Buffer
                while (true)
                {
                    buffer = this.Server.Receive(ref this.Endpoint); // Listen on bound port for UDP Datagrams
                    if (int.TryParse(Encoding.ASCII.GetString(buffer), out key)) // Make sure received data is valid ActionKey
                    {
                        Win32API.SendKey((byte)key, 50); // Send ActionKey keypress as requested by the client, 50ms key delay
                    }
                }
            }
            catch (ThreadAbortException) { return; } // Abort Thread
            catch { }
        }
        public void Stop()
        {
            this.Worker?.Abort();
            this.Server?.Close();
        }
    }
    public class WakeOnLan
    {
        private string MacAddress;
        public WakeOnLan(string mac)
        {
            this.MacAddress = mac;
        }
        public async Task Send() // Broadcasts Magic Packet based on provided MAC Address to UDP Port 9 to wake computer(s) from sleep.
        {
            try
            {
                using (Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
                {
                    sock.EnableBroadcast = true; // Enable broadcast
                    IPEndPoint ep = new IPEndPoint(IPAddress.Broadcast, 9); // Port 9 most common WOL port
                    byte[] mp = this.BuildMagicPacket(this.MacAddress); // Get magic packet byte array based on MAC Address
                    if (mp == null) throw new NullReferenceException("Magic Packet is null. Verify MAC Address is entered correctly.");
                    sock.SendTo(mp, ep); // Transmit Magic Packet on Port 9
                    sock.Close(); // Close socket
                }
            }
            catch (Exception ex) // Handle general exceptions, show exception info to user.
            {
                MessageBox.Show(ex.ToString(), Globals.WindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private byte[] BuildMagicPacket(string macaddress) // MacAddress in any standard HEX format
        {
            try
            {
                macaddress = Regex.Replace(macaddress, "[. : -]", "");
                byte[] macBytes = new byte[6];
                for (int i = 0; i < 6; i++)
                {
                    macBytes[i] = Convert.ToByte(macaddress.Substring(i * 2, 2), 16);
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    using (BinaryWriter bw = new BinaryWriter(ms))
                    {
                        for (int i = 0; i < 6; i++)  //First 6 times 0xff
                        {
                            bw.Write((byte)0xff);
                        }
                        for (int i = 0; i < 16; i++) // then 16 times MacAddress
                        {
                            bw.Write(macBytes);
                        }
                    }
                    return ms.ToArray(); // return 102 bytes magic packet
                }
            }
            catch
            {
                return null; // Return null on exception (handled in calling function)
            }
        }
    }
    public class Hotkey // Define Hotkey for displaying in ListBox Control
    {
        public Key hotkey { get; private set; }
        public Key action { get; private set; }
        public Hotkey(Key in_hotkey, Key in_action)
        {
            this.hotkey = in_hotkey;
            this.action = in_action;
        }
        public override string ToString()
        {
            return this.hotkey.Name + " >> " + this.action.Name;
        }
    }
    public class Key // Define a single Vkey
    {
        public byte Value { get; private set; }
        public string Name { get; private set; }
        public Key(byte key)
        {
            this.Value = key;
            if (Globals.VirtualKeys.ContainsKey(key)) this.Name = Globals.VirtualKeys[key];
            else this.Name = "custom(" + key.ToString() + ")";
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
