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
        public string Mode { get; private set; }
        public ZoomerClient Client { get; private set; }
        private ZoomerServer Server { get; set; }
        public WakeOnLan WOL { get; private set; }
        public Dictionary<byte, Hotkey> Hotkeys { get; private set; } // Active Hotkey List
        public static List<Hotkey> HotkeyEditorList; // Static , accessible between classes.
        private IntPtr Hwnd; // Main window handle
        private List<int> HotkeyIds; // Hotkey IDs currently bound by WIN32 API
        public ZoomerConfig(IntPtr handle)
        {
            this.IsReady = false;
            this.Hwnd = handle;
            this.Port = 0;
            this.Mode = "CLIENT";
            this.Hotkeys = new Dictionary<byte, Hotkey>();
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
                    this.Mode = (string)read.GetValue("lastmode");
                    read.Close();
                }
                using (RegistryKey hotkeys = Registry.CurrentUser.OpenSubKey(Globals.RegPath + "\\hotkeys")) // Get hotkey config
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
                                if (!int.TryParse(split[0], out hotkey)) break;
                                if (!int.TryParse(split[1], out action)) break;
                                Hotkeys.Add((byte)hotkey, new Hotkey(new Key((byte)hotkey), new Key((byte)action))); // Add to Zoomer.Hotkeys config
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
                this.Ready();
            }
        }
        private void SetHotkeys(bool IsEnabled) // Set Hotkeys
        {
            try
            {
                if (this.HotkeyIds?.Count > 0) // Unreg old hotkeys
                {
                    foreach (int id in this.HotkeyIds)
                    {
                        Win32API.UnregisterHotKey(this.Hwnd, id);
                    }
                    this.HotkeyIds = new List<int>();
                }
                switch (IsEnabled)
                {
                    case true:
                        this.HotkeyIds = new List<int>();
                        foreach (KeyValuePair<byte, Hotkey> entry in this.Hotkeys)
                        {
                            Win32API.RegisterHotKey(this.Hwnd, (int)entry.Key, 0, (int)entry.Key);
                            this.HotkeyIds.Add((int)entry.Key);
                        }
                        break;
                    case false:
                        break;
                }
            }
            catch (Exception ex) // Handle general exceptions, show exception info to user.
            {
                this.IsReady = false;
                MessageBox.Show(ex.ToString(), Globals.WindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Ready() // Sets Client/Server State
        {
            try
            {
                this.WOL = new WakeOnLan(this.WOL_MacAddress); // WOL enabled for both modes
                this.Server?.Stop(); // Stop existing server (if any)
                switch (this.Mode)
                {
                    case "CLIENT":
                        if (this.IsReady)
                        {
                            
                            this.SetHotkeys(true);
                            this.Client = new ZoomerClient(this.IpAddress, this.Port);
                        }
                        else
                        {
                            this.SetHotkeys(false);
                        }
                        break;
                    case "SERVER":
                        if (this.IsReady)
                        {
                            this.SetHotkeys(false);
                            this.Server = new ZoomerServer(this.Port);
                        }
                        else
                        {
                            this.SetHotkeys(false);
                        }
                        break;
                }
            }
            catch (Exception ex) // Handle general exceptions, show exception info to user.
            {
                this.IsReady = false;
                MessageBox.Show(ex.ToString(), Globals.WindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public async Task Install(string ip, string port, string mac, string mode) // Apply current configuration and write to Registry.
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
                    write.SetValue("lastmode", this.Mode);
                    write.Close();
                }
                using (RegistryKey hotkeys = Registry.CurrentUser.CreateSubKey(Globals.RegPath + "\\hotkeys")) // Save hotkey configuration
                {
                    string[] allvalues = hotkeys.GetValueNames();
                    foreach (string value in allvalues) // Delete old hotkeys
                    {
                        hotkeys.DeleteValue(value);
                    }
                    int i = 1;
                    if (ZoomerConfig.HotkeyEditorList?.Count > 0)
                    {
                        this.Hotkeys = new Dictionary<byte, Hotkey>();
                        foreach (Hotkey hotkey in ZoomerConfig.HotkeyEditorList) this.Hotkeys.Add(hotkey.hotkey.Value, hotkey);
                        ZoomerConfig.HotkeyEditorList = new List<Hotkey>();
                    }
                    if (this.Hotkeys != null) foreach (KeyValuePair<byte, Hotkey> entry in Hotkeys) // Write new hotkeys
                        {
                            hotkeys.SetValue(i.ToString(), entry.Key.ToString() + "," + entry.Value.action.Value.ToString()); // Delimit Hotkey & Action by comma
                            i += 1;
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
                this.Ready();
            }
        }
        public async Task Uninstall() // Remove current configuration, and delete from Registry.
        {
            try
            {
                using (RegistryKey delete = Registry.CurrentUser)
                {
                    delete.DeleteSubKey(Globals.RegPath + "\\hotkeys"); // Remove hotkeys subkey first, cannot recursively delete
                    delete.DeleteSubKey(Globals.RegPath);
                    delete.Close();
                }
                this.IpAddress = null;
                this.Port = 0;
                this.WOL_MacAddress = null;
                this.Mode = "CLIENT";
                this.Hotkeys = new Dictionary<byte, Hotkey>();
                ZoomerConfig.HotkeyEditorList = new List<Hotkey>();
                this.IsReady = false;
            }
            catch (Exception ex) // Handle general exceptions, show exception info to user.
            {
                this.IsReady = false;
                MessageBox.Show(ex.ToString(), Globals.WindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Ready();
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
                        Win32API.SendKey((byte)key, 50); // Send ActionKey keypress as requested by the client
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
        public void Send() // Broadcasts Magic Packet based on provided MAC Address to UDP Port 9 to wake computer(s) from sleep.
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
