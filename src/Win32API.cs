using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Zoomer
{
    internal static class Win32API // Methods from the Win32 API
    {
        // Win32 Consts
        private const uint INPUT_MOUSE = 0;
        private const uint INPUT_KEYBOARD = 1;
        private const uint INPUT_HARDWARE = 2;
        private const uint ES_CONTINUOUS = 0x80000000;
        private const uint ES_SYSTEM_REQUIRED = 0x00000001;
        public const int WM_HOTKEY = 0x0312;

        [DllImport("user32.dll")] // RegisterHotKey() Pinvoke
        public static extern int RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")] // UnregisterHotKey() Pinvoke
        public static extern int UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll")]
        private static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize); // SendInput() Pinvoke
        public static void SendKey(byte key)
        {
            INPUT[] input = new INPUT[1];
            input[0].type = INPUT_KEYBOARD;
            input[0].U.ki.wVk = key;
            SendInput((uint)input.Length, input, Marshal.SizeOf(input.GetType().GetElementType()) * input.Length);
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)] // SetThreadExecutionState() Pinvoke
        private static extern uint SetThreadExecutionState(uint esFlags);
        public static void PreventSleep() // Wrapper method for SetThreadExecutionState() Pinvoke
        {
            SetThreadExecutionState(ES_CONTINUOUS | ES_SYSTEM_REQUIRED);
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct INPUT
    {
        public uint type;
        public InputUnion U;
    }
    [StructLayout(LayoutKind.Explicit)]
    internal struct InputUnion
    {
        [FieldOffset(0)]
        internal MOUSEINPUT mi;
        [FieldOffset(0)]
        internal KEYBDINPUT ki;
        [FieldOffset(0)]
        internal HARDWAREINPUT hi;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public uint dwFlags;
        public uint time;
        public UIntPtr dwExtraInfo;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct KEYBDINPUT
    {
        public short wVk;
        public short wScan;
        public uint dwFlags;
        public uint time;
        public UIntPtr dwExtraInfo;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct HARDWAREINPUT
    {
        public uint uMsg;
        public short wParamL;
        public short wParamH;
    }
}
