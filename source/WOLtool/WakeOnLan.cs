using System;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace WOLtool // https://github.com/imerzan/WOLtool
{
    public class WakeOnLan : IDisposable
    {
        private readonly Socket _sock;
        private static readonly IPEndPoint[] _endpoints = new IPEndPoint[]
        {
            new IPEndPoint(IPAddress.Broadcast, 7), // echo
            new IPEndPoint(IPAddress.Broadcast, 9) // discard
        };

        public WakeOnLan()
        {
            _sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
            {
                EnableBroadcast = true // Enable broadcast, required for macOS compatibility
            };
        }

        public async Task SendAsync(string macAddress)
        {
            try
            {
                var macParse = PhysicalAddress.Parse(macAddress); // Parse string value
                var bmp = BuildMagicPacket(macParse);
                var magicPacket = new ArraySegment<byte>(bmp, 0, bmp.Length); // Get magic packet byte array based on MAC Address
                foreach (var ep in _endpoints) // Broadcast to *all* WOL Endpoints
                {
                    await _sock.SendToAsync(magicPacket, SocketFlags.None, ep); // Broadcast magic packet
                }
                System.Media.SystemSounds.Beep.Play(); // Success cue sound
            }
            catch (Exception ex)
            {
                throw new WakeOnLanException("ERROR broadcasting WakeOnLan Magic Packet.", ex);
            }
        }

        private static byte[] BuildMagicPacket(PhysicalAddress macAddress)
        {
            byte[] macBytes = macAddress.GetAddressBytes(); // Convert 48 bit MAC Address to array of bytes
            byte[] magicPacket = new byte[102];
            for (int i = 0; i < 6; i++) // 6 times 0xFF
            {
                magicPacket[i] = 0xFF;
            }
            for (int i = 6; i < 102; i += 6) // 16 times MAC Address
            {
                Buffer.BlockCopy(macBytes, 0, magicPacket, i, 6);
            }
            return magicPacket; // 102 Byte Magic Packet
        }

        // Public implementation of Dispose pattern callable by consumers.
        private bool _disposed = false;
        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                _sock?.Dispose();
            }

            _disposed = true;
        }
    }

    public class WakeOnLanException : Exception
    {
        public WakeOnLanException()
        {
        }

        public WakeOnLanException(string message)
            : base(message)
        {
        }

        public WakeOnLanException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}