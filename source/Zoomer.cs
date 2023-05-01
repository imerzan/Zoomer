namespace Zoomer
{
    public sealed class Zoomer
    {
        internal const string SubjectName = @"Zoomer-16fc27e9-a50c-409e-a972-940b1a3ed1d4";
        private readonly IntPtr _hwnd;
        private readonly Config _config;
        private readonly MainForm.ServerStatus _del;
        private ZoomerServer _server;
        private ConcurrentDictionary<ZoomerServerSession, int> _sessions = new();
        private TcpClient _client;
        private SslStream _clientStream;

        /// <summary>
        /// Active Server Sessions.
        /// </summary>
        public IReadOnlyDictionary<ZoomerServerSession, int> ServerSessions
        {
            get => _sessions;
        }
        /// <summary>
        /// True if client is connected.
        /// </summary>
        public bool ClientConnected
        {
            get
            {
                return _client?.Client.IsConnected() ?? false;
            }
        }
        /// <summary>
        /// Address of remote server.
        /// </summary>
        public string ServerAddress
        {
            get
            {
                var ep = _client?.Client.RemoteEndPoint as IPEndPoint;
                return ep?.Address.ToString() ?? "null";
            }
        }

        public Zoomer(IntPtr hwnd, Config config, MainForm.ServerStatus del)
        {
            _hwnd = hwnd;
            _config = config;
            _del = del;
        }

        public async Task<bool> Run(bool suppressExceptions = false)
        {
            try
            {
                Stop();
                if (_config.Port < 1024 || _config.Port > 65535) throw new ArgumentOutOfRangeException("port", null, "Must provide a port between 1024-65535.");
                if (_config.Password.Length < 4) throw new ArgumentOutOfRangeException("password", null, "Password must be at least 4 characters long.");
                switch (_config.Mode)
                {
                    case OperatingMode.Client:
                        _client = new TcpClient();
                        await _client.ConnectAsync(IPAddress.Parse(_config.IPAddress), _config.Port);
                        _clientStream = new SslStream(_client.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null, EncryptionPolicy.RequireEncryption);
                        _clientStream.WriteTimeout = 8000;
                        await _clientStream.AuthenticateAsClientAsync(Zoomer.SubjectName);
                        await Handshake(); // Further auth with server (password)
                        foreach (var entry in _config.Hotkeys)
                        {
                            entry.Value.Register(_hwnd); // Register Hotkey
                        }
                        break;
                    case OperatingMode.Server:
                        _sessions = new();
                        _server = new ZoomerServer(_config, _sessions, _del);
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                Stop();
                if (!suppressExceptions) MessageBox.Show(ex.ToString(), "Zoomer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private async Task Handshake() // Confirm password with Server
        {
            Memory<byte> buffer = new byte[256];
            _clientStream.ReadTimeout = 5000; // 5 sec timeout for auth
            int bytesRead = await _clientStream.ReadAsync(buffer); // STAGE 1
            var salt = buffer.Slice(0, bytesRead).ToArray();
            using (var hmac = new HMACSHA512(salt.CombineBytes(Program.RandomEntropy))) // Keyed hash digest
            {
                ReadOnlyMemory<byte> hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(_config.Password));
                await _clientStream.WriteAsync(hash); // STAGE 2
                bytesRead = await _clientStream.ReadAsync(buffer); // STAGE 3
                _clientStream.ReadTimeout = Timeout.Infinite;
                if (bytesRead != 1) throw new Exception("Did not receive a handshake response from Server.");
                switch (buffer.Span[0]) // auth check
                {
                    case 0x01:
                        break;
                    case 0xFF:
                        throw new Exception("Server rejected password! Closing down...");
                    default:
                        throw new Exception("Unhandled error during handshake! Closing down...");
                }
            }
        }

        public bool Stop()
        {
            try
            {
                _clientStream?.Dispose();
                _client?.Dispose();
                _server?.Dispose();
                foreach (var entry in _config.Hotkeys)
                {
                    entry.Value.Unregister(_hwnd); // Unregister Hotkey
                }
            } catch { }
            return false;
        }

        public void Transmit(ZoomerKey action, string window)
        {
            var payload = new ZoomerPayload()
            {
                Action = action,
                WindowTitle = window
            };
            _clientStream.Write(payload.ToPacket());
        }

        private static bool ValidateServerCertificate(
      object sender,
      X509Certificate certificate,
      X509Chain chain,
      SslPolicyErrors sslPolicyErrors)
        {
            return !sslPolicyErrors.HasFlag(SslPolicyErrors.RemoteCertificateNameMismatch);
        }
    }

}
