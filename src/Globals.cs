using System;
using System.Collections.Generic;

namespace Zoomer
{
    internal static class Globals
    {
        public const string WindowTitle = "Zoomer";
        public const string RegPath = @"SOFTWARE\Zoomer";
        public static readonly Dictionary<byte, string> VirtualKeys = new Dictionary<byte, string>() // Pre-Defined Keylist
        {
            // https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
            [0x00] = "Custom", // Uses custom user input in HotkeyEditor.cs
            [0x70] = "F1",
            [0x71] = "F2",
            [0x72] = "F3",
            [0x73] = "F4",
            [0x74] = "F5",
            [0x75] = "F6",
            [0x76] = "F7",
            [0x77] = "F8",
            [0x78] = "F9",
            [0x79] = "F10",
            [0x7A] = "F11",
            [0x7B] = "F12", // Cannot be used as hotkey
            [0x08] = "BACKSPACE",
            [0x0D] = "ENTER",
            [0xBB] = "PLUS",
            [0xBD] = "MINUS",
            [0x09] = "TAB",
            [0x20] = "SPACE",
            [0x2D] = "INSERT",
            [0x2E] = "DEL",
            [0x21] = "PG UP",
            [0x22] = "PG DN",
            [0x23] = "END",
            [0x24] = "HOME",
            [0x25] = "LEFT",
            [0x26] = "UP",
            [0x27] = "RIGHT",
            [0x28] = "DOWN",
        };
    }
    public enum OperatingMode : int
    {
        Client = 0,
        Server = 1
    }
}
