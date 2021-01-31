using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Zoomer
{
    internal static class Win32API
    {
        private const uint KEYEVENTF_KEYUP = 0x0002;
        private const uint ES_CONTINUOUS = 0x80000000;
        private const uint ES_SYSTEM_REQUIRED = 0x00000001;
        public const int WM_HOTKEY = 0x0312;

        [DllImport("user32.dll")] // RegisterHotKey() Pinvoke
        public static extern int RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")] // UnregisterHotKey() Pinvoke
        public static extern int UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo); // keybd_event() Pinvoke
        public static async void SendKey(byte key, int delay) // Wrapper function for keybd_event() Pinvoke
        {
            keybd_event(key, 0, 0, 0); // Keydown
            await Task.Delay(delay); // Keydown delay in ms
            keybd_event(key, 0, KEYEVENTF_KEYUP, 0); // Keyup
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)] // SetThreadExecutionState() Pinvoke
        private static extern uint SetThreadExecutionState(uint esFlags);
        public static void PreventSleep()
        {
            SetThreadExecutionState(ES_CONTINUOUS | ES_SYSTEM_REQUIRED);
        }
    }
}
