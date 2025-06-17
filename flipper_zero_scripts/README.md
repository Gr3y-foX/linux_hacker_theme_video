# Hacker Terminal Prank for Flipper Zero

This directory contains BadUSB/Rubber Ducky scripts for deploying the Hacker Terminal as a harmless prank using a Flipper Zero device.

## Available Scripts

1. `macos_prank.txt` - Specifically for macOS systems
2. `linux_prank.txt` - Specifically for Linux systems
3. `universal_prank.txt` - Auto-detects the OS and runs the appropriate commands (recommended)
4. `convert_to_ducky.sh` - Utility script to ensure proper formatting for Flipper Zero/Ducky Script

## How to Use with Flipper Zero

1. Use the conversion script to ensure proper formatting:
   ```bash
   ./convert_to_ducky.sh universal_prank.txt ready_to_use.txt
   ```

2. Copy the formatted `.txt` script file to your Flipper Zero's SD card in the `badusb` directory.
3. Connect your Flipper Zero to the target computer.
4. On the Flipper Zero, navigate to BadUSB app.
5. Select the script file you copied.
6. Run the script.

## What the Script Does

1. Opens a terminal window
2. Clones the Hacker Terminal repository
3. Installs necessary dependencies (including .NET if needed)
4. Runs the Hacker Terminal application with dramatic sound effects

## Requirements

- Target system must have internet access to download the repository
- Git must be installed on the target system
- For automatic .NET installation:
  - macOS: Homebrew will be installed if needed
  - Linux: apt/dnf/pacman package manager must be available

## Safety Notice

This is designed as a harmless prank - the application displays flashy visual effects but does not harm the system. However, please note:

- It installs software (.NET if not present) which requires admin permissions
- It creates temporary files in `/tmp/hacker_prank`
- Always get permission before running this on someone else's computer

## Customization

You can modify the delay times in the script to match the speed of the target system:
- Increase `DELAY` values for slower computers
- Decrease `DELAY` values for faster computers

## Cleanup

To remove all traces after the prank:
```bash
rm -rf /tmp/hacker_prank
```
