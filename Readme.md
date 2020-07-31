# AR Drum Circle Unity Project
Note, this is currently for Android only devices, make sure you have an android device connected to build

## Testing Bluetooth Communication
At the moment to test bluetooth communication, you will first need to do some manual configuration.
- First, make sure you have the a device with the [Bluetooth MIDI server](https://github.com/atlas-acme-lab/AR-drum-circle-midi-connector) set up
- Then Open the Unity project
- Open the scene named `AndroidNativeBTTest` from the scenes folder
- Set the field `Device Name` on the game object *Bluetooth Connector* to match the name of the device running the Bluetooth Server
- Start the Bluetooth MIDI Server (at the moment this must alread be open, we will be adding Bluetooth discovery UI in the future)
- Connect your Android device to the computer with the Unity project, and build the project to the phone (File->Build and Run)
- Your device should automatically connect to the Bluetooth MIDI Server and play sound when you hit the drum pad