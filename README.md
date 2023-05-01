# Zoomer
Windows .NET application that transmits hotkey presses between Client/Server PCs, with additional WakeOnLan module.

## Info
Uses TLS 1.3 to establish a secure connection. Uses a 'Hotkey' keypress on the Client PC to execute an 'Action' keypress on the Server PC. Supports custom keys, use the hexadecimal values via https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes

**NOTE:** If the server is attempting to interact with an elevated process, you may need to Run-As-Admin for input to work in the elevated window.

## Demo
![Example1](https://user-images.githubusercontent.com/42287509/144729669-fa5f894a-f33d-4af7-b982-137b21e995d9.png)
![Example2](https://user-images.githubusercontent.com/42287509/144729671-75b9fc13-9266-4081-88a2-65015be7a5a4.png)
