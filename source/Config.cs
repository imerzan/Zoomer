namespace Zoomer
{
    public sealed class Config
    {
        [JsonIgnore]
        private static readonly string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Zoomer", "Config.json");
        [JsonIgnore]
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions()
        {
            WriteIndented = true
        };

        [JsonPropertyName("ipaddress")]
        public string IPAddress { get; set; } = string.Empty;
        [JsonPropertyName("port")]
        public int Port { get; set; } = 0;
        [JsonPropertyName("password")]
        [JsonInclude]
        public byte[] PasswordBytes { get; private set; } // Backing property
        [JsonIgnore]
        public string Password
        {
            get // Decrypt password
            {
                return Encoding.UTF8.GetString(ProtectedData.Unprotect(PasswordBytes, Program.RandomEntropy, DataProtectionScope.CurrentUser));
            }
            set // Encrypt password
            {
                PasswordBytes = ProtectedData.Protect(Encoding.UTF8.GetBytes(value), Program.RandomEntropy, DataProtectionScope.CurrentUser);
            }
        }
        [JsonPropertyName("windowTitle")]
        public string WindowTitle { get; set; } = string.Empty;
        [JsonPropertyName("wolmac")]
        public string WOL_Mac { get; set; } = string.Empty;
        [JsonPropertyName("mode")]
        public OperatingMode Mode { get; set; } = OperatingMode.Client;
        [JsonPropertyName("hotkeys")]
        public IReadOnlyDictionary<byte, ZoomerHotkey> Hotkeys { get; set; } = new Dictionary<byte, ZoomerHotkey>();

        public Config()
        {
            Password = string.Empty;
        }

        public static bool TryLoadConfig(out Config config)
        {
            try
            {
                var file = new FileInfo(_path);
                if (!file.Exists)
                {
                    config = null;
                    return false;
                }
                var json = File.ReadAllText(file.FullName);
                config = JsonSerializer.Deserialize<Config>(json, _jsonOptions);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR loading config: {ex}", "Zoomer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                config = null;
                return false;
            }
        }

        public static void SaveConfig(Config config)
        {
            try
            {
                var file = new FileInfo(_path);
                file!.Directory!.Create(); // Create config dir if does not exist
                var json = JsonSerializer.Serialize<Config>(config, _jsonOptions);
                File.WriteAllText(file.FullName, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR saving config: {ex}", "Zoomer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
