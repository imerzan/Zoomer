namespace Zoomer
{
    public sealed class ZoomerServerSession : IDisposable
    {
        private readonly TcpClient _client;
        private readonly X509Certificate2 _cert;
        private readonly byte[] _pass;
        private readonly MainForm.ServerStatus _del;
        private readonly ConcurrentDictionary<ZoomerServerSession, int> _sessions;
        private SslStream _stream;
        public bool ClientConnected
        {
            get
            {
                if (_disposed) return false;
                else return _client?.Client.IsConnected() ?? false;
            }
        }

        public ZoomerServerSession(TcpClient client, 
            X509Certificate2 cert, byte[] pass, 
            CancellationToken ct, 
            MainForm.ServerStatus del,
            ConcurrentDictionary<ZoomerServerSession, int> sessions)
        {
            _client = client;
            _cert = cert;
            _pass = pass;
            _del = del;
            _sessions = sessions;
            Task.Run(() => Server(ct));
        }

        private async Task Server(CancellationToken ct)
        {
            try
            {
                using (ct.Register(() => Dispose(true)))
                {
                    _stream = new SslStream(_client.GetStream(), false, null, null, EncryptionPolicy.RequireEncryption);
                    _stream.WriteTimeout = 8000;
                    await _stream.AuthenticateAsServerAsync(_cert);
                    if (!await Handshake()) return; // Further auth (verify password)
                    Memory<byte> buffer = new byte[2048];
                    int bytesRead;
                    while (true)
                    {
                        try
                        {
                            if (!_client.Client.IsConnected())
                            {
                                _del($"{DateTime.Now.ToLongTimeString()}: Client {_client.Client.RemoteEndPoint} has disconnected!");
                                return; // Abort if client is no longer connected
                            }
                            bytesRead = await _stream.ReadAsync(buffer);
                            var received = buffer.Slice(0, bytesRead);
                            var payload = ZoomerPayload.FromPacket(received);
                            if (payload.WindowTitle is not null) // Window Action
                            {
                                var targetHwnd = Win32API.FindWindow(null, payload.WindowTitle);
                                if (targetHwnd != IntPtr.Zero)
                                {
                                    switch (payload.Action.Value)
                                    {
                                        case (byte)MouseScrollDirection.Down: // Not Supported
                                            break;
                                        case (byte)MouseScrollDirection.Up: // Not Supported
                                            break;
                                        default:
                                            Win32API.SendKey(payload.Action.Value, targetHwnd);
                                            break;
                                    }
                                }
                            }
                            else // Global Action
                            {
                                switch (payload.Action.Value)
                                {
                                    case (byte)MouseScrollDirection.Down: // Special Case
                                        Win32API.SendMouseGlobal(MouseScrollDirection.Down);
                                        break;
                                    case (byte)MouseScrollDirection.Up: // Special Case
                                        Win32API.SendMouseGlobal(MouseScrollDirection.Up);
                                        break;
                                    default:
                                        Win32API.SendKeyGlobal(payload.Action);
                                        break;
                                }
                            }
                        }
                        catch
                        {
                            if (ct.IsCancellationRequested || _disposed) return;
                        }
                    }
                }
            }
            catch
            {
                _del($"{DateTime.Now.ToLongTimeString()}: Client {_client.Client.RemoteEndPoint} failed to connect (unhandled socket exception)");
            }
            finally { Dispose(true); }
        }

        private async Task<bool> Handshake() // Verify password from Client
        {
            ReadOnlyMemory<byte> hash;
            var salt = RandomNumberGenerator.GetBytes(16);
            using (var hmac = new HMACSHA512(salt.CombineBytes(Program.RandomEntropy)))
            {
                hash = hmac.ComputeHash(_pass);
            }
            await _stream.WriteAsync(salt); // STAGE 1
            Memory<byte> buffer = new byte[256];
            _stream.ReadTimeout = 5000; // 5 sec timeout for auth
            int bytesRead = await _stream.ReadAsync(buffer); // STAGE 2
            _stream.ReadTimeout = Timeout.Infinite;
            if (!ValidateHash(hash.Span, buffer.Slice(0, bytesRead).Span))
            {
                _del($"{DateTime.Now.ToLongTimeString()}: Client {_client.Client.RemoteEndPoint} provided an invalid password!");
                await _stream.WriteAsync(new byte[1] { 0xFF }); // STAGE 3 (fail)
                return false;
            }
            else
            {
                _del($"{DateTime.Now.ToLongTimeString()}: Client {_client.Client.RemoteEndPoint} has connected!");
                await _stream.WriteAsync(new byte[1] { 0x01 }); // STAGE 3 (success)
                return true;
            }
        }

        public override string ToString()
        {
            return _client?.Client?.RemoteEndPoint?.ToString() ?? "NULL";
        }

        private static bool ValidateHash(ReadOnlySpan<byte> hash1, ReadOnlySpan<byte> hash2)
        {
            if (hash1.Length != hash2.Length) return false; // Should never happen, but checking for good measure
            for (int i = 0; i < hash1.Length; i++) // Compare hash values
            {
                if (hash1[i] != hash2[i]) return false;
            }
            return true;
        }

    // Public implementation of Dispose pattern callable by consumers.
    private bool _disposed = false;
        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                _stream?.Dispose();
                _client?.Dispose();
                _sessions.TryRemove(this, out int zz);
            }

            _disposed = true;
        }
    }
}
