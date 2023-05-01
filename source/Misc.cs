namespace Zoomer
{
    public sealed class ZoomerHotkey // Define Hotkey for displaying in ListBox Control
    {
        [JsonIgnore]
        private volatile bool _set = false;

        /// <summary>
        /// 'Hotkey' keypress that is registered on the Local System.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("hotkey")]
        public ZoomerKey Hotkey { get; init; }
        /// <summary>
        /// 'Action' keypress that is registered on the Remote System.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("action")]
        public ZoomerKey Action { get; init; }

        /// <summary>
        /// Register Global Hotkey.
        /// </summary>
        /// <param name="handle">Window handle to process hotkey messages.</param>
        public void Register(IntPtr handle)
        {
            if (_set) return;
            try
            {
                int mod = 0;
                if (Hotkey.HasCtrl && !Hotkey.HasAlt && !Hotkey.HasShift)
                    mod = Win32API.MOD_CONTROL;
                else if (!Hotkey.HasCtrl && Hotkey.HasAlt && !Hotkey.HasShift)
                    mod = Win32API.MOD_ALT;
                else if (!Hotkey.HasCtrl && !Hotkey.HasAlt && Hotkey.HasShift)
                    mod = Win32API.MOD_SHIFT;
                else if (Hotkey.HasCtrl && Hotkey.HasAlt && !Hotkey.HasShift)
                    mod = Win32API.MOD_CONTROL | Win32API.MOD_ALT;
                else if (Hotkey.HasCtrl && !Hotkey.HasAlt && Hotkey.HasShift)
                    mod = Win32API.MOD_CONTROL | Win32API.MOD_SHIFT;
                else if (!Hotkey.HasCtrl && Hotkey.HasAlt && Hotkey.HasShift)
                    mod = Win32API.VK_ALT | Win32API.MOD_SHIFT;
                else if (Hotkey.HasCtrl && Hotkey.HasAlt && Hotkey.HasShift)
                    mod = Win32API.MOD_CONTROL | Win32API.MOD_ALT | Win32API.MOD_SHIFT;
                _set = Win32API.RegisterHotKey(handle, (int)Hotkey.Value, mod, (int)Hotkey.Value);
            }
            catch { }
        }
        /// <summary>
        /// UnRegister Global Hotkey.
        /// </summary>
        /// <param name="handle">Window handle that processes this hotkey's messages.</param>
        public void Unregister(IntPtr handle)
        {
            if (!_set) return;
            try
            {
                _set = !Win32API.UnregisterHotKey(handle, (int)Hotkey.Value);
            }
            catch { }
        }
        public override string ToString()
        {
            return this.Hotkey.Name + " >> " + this.Action.Name;
        }
    }
    public sealed class ZoomerKey // Define a single Vkey
    {
        [JsonInclude]
        [JsonPropertyName("value")]
        public byte Value { get; init; }
        [JsonInclude]
        [JsonPropertyName("hasCtrl")]
        public bool HasCtrl { get; init; }
        [JsonInclude]
        [JsonPropertyName("hasAlt")]
        public bool HasAlt { get; init; }
        [JsonInclude]
        [JsonPropertyName("hasShift")]
        public bool HasShift { get; init; }
        [JsonIgnore]
        public string Name
        {
            get
            {
                string mods = "";
                if (HasCtrl)
                    mods += "Ctrl";
                if (HasAlt)
                    mods += "Alt";
                if (HasShift)
                    mods += "Shift";
                if (HotkeyEditor.DefaultKeys.ContainsKey(Value))
                    return $"{mods} {HotkeyEditor.DefaultKeys[Value]}";
                else
                    return $"{mods} 0x{Value:X}"; // Hex Value
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public enum OperatingMode : int
    {
        Client = 0,
        Server = 1
    }

    public static class Extensions
    {
        public static bool IsConnected(this Socket socket)
        {
            try
            {
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException) { return false; }
        }

        public static byte[] CombineBytes(this byte[] source, byte[] toConcat) // returns new byte array
        {
            byte[] newArr = new byte[source.Length + toConcat.Length];
            var span = newArr.AsSpan();
            source.CopyTo(span.Slice(0, source.Length));
            toConcat.CopyTo(span.Slice(source.Length, toConcat.Length));
            return newArr;
        }
    }

    /// <summary>
    /// Defines a Zoomer Payload to be sent / received over the network.
    /// </summary>
    public readonly struct ZoomerPayload
    {
        /// <summary>
        /// Title of the window to execute the action in.
        /// If NULL, the action will be global on the system.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("windowTitle")]
        public readonly string WindowTitle { get; init; }
        /// <summary>
        /// Action to execute on the server.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("actionKey")]
        public readonly ZoomerKey Action { get; init; }

        /// <summary>
        /// Convert this structure to a network transmissible packet.
        /// </summary>
        public readonly ReadOnlySpan<byte> ToPacket()
        {
            var json = JsonSerializer.Serialize(this);
            return Encoding.UTF8.GetBytes(json);
        }

        /// <summary>
        /// Decode a Zoomer network transmissible packet.
        /// </summary>
        public static ZoomerPayload FromPacket(ReadOnlyMemory<byte> packet)
        {
            var json = Encoding.UTF8.GetString(packet.Span);
            return JsonSerializer.Deserialize<ZoomerPayload>(json);
        }
    }
}
