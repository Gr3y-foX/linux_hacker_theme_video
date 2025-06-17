# Setting up Hacker Terminal on Raspberry Pi

These instructions will help you set up and test the Hacker Terminal with keyboard interceptor on a Raspberry Pi.

## Prerequisites

- Raspberry Pi 3 or 4 with Raspberry Pi OS (32-bit or 64-bit) or Ubuntu Server installed
- Internet connection
- Terminal access (either directly or via SSH)

## Option 1: Recommended - Quick Install without Building

```bash
# Clone the repository
git clone https://github.com/Gr3y-foX/linux_hacker_theme_video.git

# Navigate to the project directory
cd linux_hacker_theme_video

# Fix permissions (solves common issues)
chmod +x fix_permissions.sh
./fix_permissions.sh

# Use the quick install script (no building required)
chmod +x quick_install.sh
./quick_install.sh

# Run with keyboard interceptor
./run_with_intercept.sh
```

## Option 2: Standard Install with Script

```bash
# Clone the repository
git clone https://github.com/Gr3y-foX/linux_hacker_theme_video.git

# Navigate to the project directory
cd linux_hacker_theme_video

# Make the installation script executable
chmod +x install_raspberry_pi.sh

# Run the installation script
./install_raspberry_pi.sh

# Make keyboard interceptor scripts executable
chmod +x setup_keyboard_interceptor.sh
chmod +x run_with_intercept.sh

# Install Python dependencies for keyboard interceptor
sudo apt-get install -y python3-pip
pip3 install pynput

# Run with keyboard interceptor
./run_with_intercept.sh
```

## Option 2: For Ubuntu Server on Raspberry Pi

```bash
# Clone the repository
git clone https://github.com/Gr3y-foX/linux_hacker_theme_video.git

# Navigate to the project directory
cd linux_hacker_theme_video

# Make the installation script executable
chmod +x install_ubuntu_raspberry_pi.sh

# Run the installation script
./install_ubuntu_raspberry_pi.sh

# Make keyboard interceptor scripts executable
chmod +x setup_keyboard_interceptor.sh
chmod +x run_with_intercept.sh

# Install Python dependencies for keyboard interceptor
sudo apt-get install -y python3-pip
pip3 install pynput

# Run with keyboard interceptor
./run_with_intercept.sh
```

## Using the Application

- When running with the keyboard interceptor, common keyboard shortcuts will be blocked
- To exit, use the special kill switch: `Ctrl+Shift+X`
- If you need to force quit, you may need to use `killall python3` and `killall dotnet` from another terminal session

## Troubleshooting

### If the keyboard interceptor doesn't work:

1. Make sure you have the necessary permissions:
   ```bash
   sudo apt-get install -y python3-pip python3-dev
   sudo pip3 install pynput
   ```

2. Run the setup script explicitly:
   ```bash
   ./setup_keyboard_interceptor.sh
   ```

3. Check for errors:
   ```bash
   python3 ./python_scripts/keyboard_interceptor.py
   ```

### If the application doesn't build:

1. Check your .NET SDK installation:
   ```bash
   dotnet --version
   ```

2. Try rebuilding:
   ```bash
   dotnet build
   dotnet run
   ```

### For display issues:

1. Make sure you're running in a full-screen terminal
2. Try adjusting your terminal font size

### Permission issues:

If you encounter errors like "Access to the path is denied" or "Permission denied":

```bash
# Run the permission fix script
./fix_permissions.sh

# Then try again
./run_with_intercept.sh
```

### Building issues on Raspberry Pi:

If you're having trouble building the project due to memory limitations or timeout issues:

```bash
# Use the quick install script instead of building
./quick_install.sh
```

### If nothing else works:

If all else fails, you can try these steps:

1. Create a clean clone of the repository:
   ```bash
   # Move to your home directory
   cd ~
   
   # Clone a fresh copy
   git clone https://github.com/Gr3y-foX/linux_hacker_theme_video.git hacker-terminal-fresh
   
   # Navigate to the fresh copy
   cd hacker-terminal-fresh
   
   # Fix permissions and use quick install
   chmod +x fix_permissions.sh quick_install.sh
   ./fix_permissions.sh
   ./quick_install.sh
   ```

2. If that doesn't work, please open an issue on GitHub with details about your Raspberry Pi model, OS version, and the specific errors you're encountering.
