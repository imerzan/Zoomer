# Zoomer
Windows .NET application that transmits hotkey presses between Client/Server PCs, with additional WakeOnLan module.

### Note: This project is no longer maintained, but should continue to function on future versions of Windows.

![Example](https://user-images.githubusercontent.com/42287509/115422824-e3a68300-a1c2-11eb-86f0-4a201921f768.jpg) ![Example2](https://user-images.githubusercontent.com/42287509/115422840-e73a0a00-a1c2-11eb-916e-1b784aeb3a2f.jpg)
## Instructions:
1. Run-As-Admin Zoomer.exe on the 'Server' computer that you would like 'Action' key presses to be executed on.
   - Select the 'Server' radio button.
   - Specify a port (NOTE: Highly recommend using ports from the range 49152 to 65535).
   - Press 'Install' to save configuration and run.
2. *(Optional)* If you will be communicating over the internet, make sure to setup Port Forwarding on your router (on the server-side).
   - **WARNING:** Do this at your own risk. There is basic encryption/authentication to prevent most attacks (including replay), but security is not guaranteed.
   - Uses UDP Protocol.
3. Run Zoomer.exe on the 'Client' computer that will use 'Hotkeys' to execute an 'Action' on the 'Server'.
   - Select the 'Client' radio button.
   - Specify IP Address & Port of the 'Server'
   - Press 'Install' to save configuration and run.
4. After Installing if you change the configuration or hotkeys, be sure to press the 'Update' button to update the running configuration.

**NOTE:** To use custom hotkeys, see: https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
Be sure to use the integer value of the values listed on this webpage (Example: 0x41 is the binary hex value for the 'a' key, which converts to an integer value of 65)
