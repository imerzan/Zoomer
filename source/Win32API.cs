namespace Zoomer
{
    internal static class Win32API // Methods from the Win32 API
    {
        // Win32 Consts
        private const uint INPUT_MOUSE = 0;
        private const uint INPUT_KEYBOARD = 1;
        private const uint INPUT_HARDWARE = 2;
        private const uint MOUSEEVENTF_WHEEL = 0x0800;
        private const uint ES_CONTINUOUS = 0x80000000;
        private const uint ES_SYSTEM_REQUIRED = 0x00000001;
        private const int WHEEL_DELTA = 120;
        public const int WM_HOTKEY = 0x0312;
        public const int MOD_CONTROL = 0x0002;
        public const int MOD_ALT = 0x0001;
        public const int MOD_SHIFT = 0x0004;
        public const int VK_SHIFT = 0x10;
        public const int VK_CONTROL = 0x11;
        public const int VK_ALT = 0x12;
        public const uint WM_KEYDOWN = 0x0100;

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, long wParam, long lParam);
        [DllImport("user32.dll")] // RegisterHotKey() Pinvoke
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")] // UnregisterHotKey() Pinvoke
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll")]
        private static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize); // SendInput() Pinvoke
        /// <summary>
        /// Sends a keystroke to the foreground window.
        /// </summary>
        /// <param name="action">Action Key object.</param>
        public static void SendKeyGlobal(ZoomerKey action)
        {
            INPUT[] input = new INPUT[1];
            input[0].type = INPUT_KEYBOARD;
            if (action.HasCtrl)
            {
                input[0].U.ki.wVk = VK_CONTROL;
                SendInput((uint)input.Length, input, Marshal.SizeOf(typeof(INPUT)));
            }
            if (action.HasCtrl)
            {
                input[0].U.ki.wVk = VK_CONTROL;
                SendInput((uint)input.Length, input, Marshal.SizeOf(typeof(INPUT)));
            }
            if (action.HasCtrl)
            {
                input[0].U.ki.wVk = VK_CONTROL;
                SendInput((uint)input.Length, input, Marshal.SizeOf(typeof(INPUT)));
            }
            input[0].U.ki.wVk = action.Value;
            SendInput((uint)input.Length, input, Marshal.SizeOf(typeof(INPUT)));
        }
        /// <summary>
        /// Sends a global mousewheel scroll.
        /// </summary>
        /// <param name="dir">Direction to scroll.</param>
        public static void SendMouseGlobal(MouseScrollDirection dir)
        {
            INPUT[] input = new INPUT[1];
            input[0].type = INPUT_MOUSE;
            input[0].U.mi.dx = 0;
            input[0].U.mi.dy = 0;
            input[0].U.mi.dwFlags = MOUSEEVENTF_WHEEL;
            switch (dir)
            {
                case MouseScrollDirection.Down:
                    input[0].U.mi.mouseData = -WHEEL_DELTA;
                    break;
                case MouseScrollDirection.Up:
                    input[0].U.mi.mouseData = WHEEL_DELTA;
                    break;
            }
            SendInput((uint)input.Length, input, Marshal.SizeOf(typeof(INPUT)));
        }
        /// <summary>
        /// Send a keystroke to a window's message queue.
        /// May have different behavior for different applications.
        /// </summary>
        /// <param name="vk">Virtual key code.</param>
        /// <param name="hWnd">Window handle to send to.</param>
        public static void SendKey(byte vk, IntPtr hWnd)
        {
            PostMessage(hWnd, WM_KEYDOWN, vk, 0);
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)] // SetThreadExecutionState() Pinvoke
        private static extern uint SetThreadExecutionState(uint esFlags);
        public static void PreventSleep(bool preventSleep) // Wrapper method for SetThreadExecutionState() Pinvoke
        {
            if (preventSleep)
            {
                SetThreadExecutionState(ES_CONTINUOUS | ES_SYSTEM_REQUIRED); // Prevent sleep
            }
            else
            {
                SetThreadExecutionState(ES_CONTINUOUS); // Allow sleep
            }
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
        public MOUSEINPUT mi;
        [FieldOffset(0)]
        public KEYBDINPUT ki;
        [FieldOffset(0)]
        public HARDWAREINPUT hi;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public int mouseData;
        public uint dwFlags;
        public uint time;
        public UIntPtr dwExtraInfo;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public UIntPtr dwExtraInfo;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct HARDWAREINPUT
    {
        public uint uMsg;
        public ushort wParamL;
        public ushort wParamH;
    }

    public enum MouseScrollDirection : byte
    {
        Up = 0xFF,
        Down = 0xFE
    }
}
