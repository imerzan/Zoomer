global using System;
global using System.Diagnostics;
global using System.Collections.Generic;
global using System.Collections.Concurrent;
global using System.IO;
global using System.Linq;
global using System.Net;
global using System.Net.Security;
global using System.Reflection;
global using System.Runtime.InteropServices;
global using System.Security;
global using System.Security.Cryptography;
global using System.Security.Cryptography.X509Certificates;
global using System.Threading;
global using System.Windows.Forms;
global using System.Net.Sockets;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Threading.Tasks;
global using System.Timers;
global using WOLtool;

namespace Zoomer
{
    static class Program
    {
        private static readonly Mutex _mutex;
        private static readonly Config _config;
        public static readonly byte[] RandomEntropy = new byte[] // Pepper
        {
            0x1F, 0x1B, 0x13, 0x49, 0x7, 0xDF, 0x70, 0xE7,
            0xAB, 0xC5, 0x3E, 0xC9, 0x71, 0xDF, 0x83, 0x2
        };

        static Program()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                throw new PlatformNotSupportedException();
            _mutex = new Mutex(true, "C7340164-AC54-4442-BCA8-BFB2C2D95945", out bool singleton);
            if (!singleton)
                throw new ApplicationException("The Application Is Already Running!");
            if (!Config.TryLoadConfig(out _config)) _config = new Config();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(_config));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Zoomer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
