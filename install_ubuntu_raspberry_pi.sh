#!/bin/bash

# Set script to exit on error
set -e

echo "======================================================"
echo "    Hacker Terminal Installation for Raspberry Pi 4"
echo "               with Ubuntu Server OS"
echo "======================================================"
echo "This script will install the .NET SDK and the Hacker Terminal application"
echo "on your Raspberry Pi 4 running Ubuntu Server."
echo ""

# Check if running on Raspberry Pi
if ! grep -q "Raspberry Pi" /proc/device-tree/model 2>/dev/null; then
    echo "WARNING: This doesn't appear to be a Raspberry Pi."
    echo "This script is optimized for Raspberry Pi 4 with Ubuntu Server."
    read -p "Do you want to continue anyway? (y/n): " answer
    if [[ "$answer" != "y" && "$answer" != "Y" ]]; then
        echo "Installation cancelled."
        exit 0
    fi
fi

# Get architecture and system information
ARCH=$(dpkg --print-architecture)
echo "Detected architecture: $ARCH"

# Load OS information
. /etc/os-release

echo "Detected OS: $PRETTY_NAME"
echo "===================================================="

# Check for sudo privileges
if [ "$(id -u)" -ne 0 ]; then
    echo "Checking for sudo privileges..."
    if ! sudo -v; then
        echo "ERROR: This script requires sudo privileges to install packages."
        exit 1
    fi
fi

# Install prerequisites
echo "Installing prerequisites..."
sudo apt-get update
sudo apt-get install -y curl libunwind8 gettext apt-transport-https \
                        wget ca-certificates software-properties-common \
                        libc6 libgcc1 libgssapi-krb5-2 libicu70 \
                        libssl3 libstdc++6 zlib1g

# Detect memory constraints and add swap if needed
TOTAL_MEM=$(free -m | awk '/^Mem:/{print $2}')
echo "Total available memory: ${TOTAL_MEM}MB"

if [ $TOTAL_MEM -lt 2048 ]; then
    echo "Memory is less than 2GB. Checking swap space..."
    SWAP=$(free -m | awk '/^Swap:/{print $2}')
    
    if [ $SWAP -lt 1024 ]; then
        echo "Adding additional swap space for the build process..."
        
        # Create a 1GB swap file if less than 1GB swap exists
        SWAP_FILE="/swapfile"
        
        # Check if swapfile already exists
        if [ -f $SWAP_FILE ]; then
            echo "Swap file already exists. Skipping creation."
        else
            sudo fallocate -l 1G $SWAP_FILE
            sudo chmod 600 $SWAP_FILE
            sudo mkswap $SWAP_FILE
            sudo swapon $SWAP_FILE
            
            # Make swap permanent
            if ! grep -q "$SWAP_FILE" /etc/fstab; then
                echo "$SWAP_FILE none swap sw 0 0" | sudo tee -a /etc/fstab
            fi
            
            echo "Added 1GB swap file to help with memory constraints."
        fi
    else
        echo "Sufficient swap space already exists."
    fi
fi

# Add Microsoft package repository
echo "Adding Microsoft package repository..."
wget -q https://packages.microsoft.com/config/ubuntu/$VERSION_ID/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

# Update package list
echo "Updating package lists..."
sudo apt-get update

# Install .NET SDK 9.0 (with fallback to 8.0 or 6.0)
echo "Installing .NET SDK..."
if sudo apt-get install -y dotnet-sdk-9.0; then
    echo "Successfully installed .NET SDK 9.0"
else
    echo "Failed to install .NET SDK 9.0. Trying .NET 8.0..."
    
    if sudo apt-get install -y dotnet-sdk-8.0; then
        echo "Successfully installed .NET SDK 8.0"
    else
        echo "Failed to install .NET SDK 8.0. Falling back to .NET 6.0 LTS..."
        sudo apt-get install -y dotnet-sdk-6.0
    fi
fi

# Verify installation
echo "Verifying .NET installation..."
dotnet --info

# Optimize Raspberry Pi settings for performance
echo "Optimizing Raspberry Pi settings for performance..."

# CPU Governor settings (if cpufrequtils is available)
if sudo apt-get install -y cpufrequtils; then
    echo "Setting CPU governor to performance mode..."
    sudo cpufreq-set -g performance
    
    # Make CPU governor setting persistent across reboots
    if ! grep -q "cpufreq" /etc/rc.local 2>/dev/null; then
        # Create rc.local if it doesn't exist
        if [ ! -f /etc/rc.local ]; then
            echo '#!/bin/bash' | sudo tee /etc/rc.local
            echo 'exit 0' | sudo tee -a /etc/rc.local
            sudo chmod +x /etc/rc.local
        fi
        
        # Add cpufreq command before exit
        sudo sed -i '/^exit 0/i cpufreq-set -g performance' /etc/rc.local
    fi
fi

# Clone the Hacker Terminal repository if this script is run standalone
if [ ! -f "hacker-terminal.csproj" ]; then
    echo "Hacker Terminal project not found in current directory."
    echo "Cloning the repository..."
    
    sudo apt-get install -y git
    git clone https://github.com/Gr3y-foX/linux_hacker_theme_video.git hacker-terminal
    cd hacker-terminal
fi

# Build the Hacker Terminal application
echo "Building Hacker Terminal application..."
dotnet build -c Release

# Make run script executable
chmod +x run.sh

# Create desktop shortcut if desktop environment is detected
if [ -d "$HOME/Desktop" ]; then
    echo "Creating desktop shortcut..."
    
    cat > "$HOME/Desktop/HackerTerminal.desktop" << EOF
[Desktop Entry]
Type=Application
Name=Hacker Terminal
Comment=Hacker-themed terminal visualization
Exec=$(pwd)/run.sh
Icon=utilities-terminal
Terminal=true
Categories=Utility;
EOF
    
    chmod +x "$HOME/Desktop/HackerTerminal.desktop"
fi

# Create a system-wide command for the application
LAUNCHER_SCRIPT="/usr/local/bin/hacker-terminal"
echo "Creating system-wide command 'hacker-terminal'..."

cat << EOF | sudo tee $LAUNCHER_SCRIPT
#!/bin/bash
cd $(pwd)
./run.sh
EOF

sudo chmod +x $LAUNCHER_SCRIPT

echo "======================================================"
echo "     Hacker Terminal Installation Complete!"
echo "======================================================"
echo ""
echo "Run the application using:"
echo "  - ./run.sh (from this directory)"
echo "  - hacker-terminal (from anywhere)"
echo ""
echo "Enjoy your hacker-themed terminal visualization!"
