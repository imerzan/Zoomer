namespace Zoomer
{
    public sealed class ZoomerServer : IDisposable
    {
        private readonly CancellationTokenSource _cts;
        private readonly X509Certificate2 _cert;
        private readonly byte[] _pass;
        private readonly TcpListener _listener;
        private readonly ConcurrentDictionary<ZoomerServerSession, int> _sessions;
        private readonly MainForm.ServerStatus _del;

        public ZoomerServer(Config config,
            ConcurrentDictionary<ZoomerServerSession, int> sessions, 
            MainForm.ServerStatus del)
        {
            _cts = new CancellationTokenSource();
            _pass = Encoding.UTF8.GetBytes(config.Password);
            // Generate ephemeral key/certificate
            using var key = ECDsa.Create(ECCurve.NamedCurves.nistP384);
            var certReq = new CertificateRequest($"CN={Zoomer.SubjectName}", key, HashAlgorithmName.SHA512);
            using var ephemeral = certReq.CreateSelfSigned(DateTime.UtcNow - TimeSpan.FromDays(1),
                DateTime.UtcNow + TimeSpan.FromDays(1));
            // SSLStream on Windows throws with ephemeral key sets
            // workaround from https://github.com/dotnet/runtime/issues/23749#issuecomment-388231655
            _cert = new X509Certificate2(ephemeral.Export(X509ContentType.Pkcs12));
            _sessions = sessions;
            _del = del;
            _listener = new TcpListener(IPAddress.Any, config.Port);
            _listener.Start();
            Task.Run(() => Listener(_cts.Token));
        }

        private async Task Listener(CancellationToken ct)
        {
            using (ct.Register(() => _listener.Stop()))
            {
                while (true)
                {
                    try
                    {
                        var client = await _listener.AcceptTcpClientAsync();
                        var session = new ZoomerServerSession(client, _cert, _pass, ct, _del, _sessions);
                        _sessions.TryAdd(session, session.GetHashCode());
                    }
                    catch { if (ct.IsCancellationRequested) return; }
                }
            }
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
                _cts?.Cancel();
                _cts?.Dispose();
                _cert?.Dispose();
            }

            _disposed = true;
        }
    }
}
