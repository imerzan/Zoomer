using System;
using System.Threading;
using System.Windows.Forms;

namespace Zoomer
{
    static class Program
    {
        private static Mutex mutex;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew;
            mutex = new Mutex(true, "c7340164-ac54-4442-bca8-bfb2c2d95945", out createdNew); // Create new Mutex, only allow one instance of program to be running.
            if (!createdNew) // Program is already running! Abort startup
            {
                MessageBox.Show("The Application Is Already Running", Globals.WindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else // Program is not running, continue startup.
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Zoomer());
            }
        }
    }
}
