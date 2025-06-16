#!/bin/bash

# Script to install .NET on Raspberry Pi with multiple fallback methods
# This script handles common network issues and provides alternative installation methods

echo "=== Hacker Terminal Offline Installation Script ==="
echo "This script will attempt to install .NET using multiple methods"

# Detect architecture
ARCH=$(dpkg --print-architecture)
echo "Detected architecture: $ARCH"

# Function to check internet connectivity
check_internet() {
    echo "Checking internet connectivity..."
    if ping -c 1 -W 5 8.8.8.8 &> /dev/null; then
        echo "Internet connection available."
        return 0
    else
        echo "Warning: Internet connection seems unavailable or unstable."
        return 1
    fi
}

# Function to try installing using apt
try_apt_install() {
    echo "Attempting to install .NET using apt..."
    
    # Try to update package lists
    sudo apt-get update || echo "Warning: apt-get update failed, continuing anyway..."
    
    # Try to install .NET SDK 9.0 first
    echo "Trying to install .NET SDK 9.0..."
    if sudo apt-get install -y dotnet-sdk-9.0; then
        echo "Successfully installed .NET SDK 9.0 using apt."
        return 0
    else
        echo "Failed to install .NET SDK 9.0. Trying .NET SDK 6.0..."
        # Try to install .NET SDK 6.0 as fallback
        if sudo apt-get install -y dotnet-sdk-6.0; then
            echo "Successfully installed .NET SDK 6.0 using apt."
            return 0
        else
            echo "Failed to install .NET SDK 6.0."
            return 1
        fi
    fi
}

# Function to try installing using the Microsoft repository
try_ms_repo_install() {
    echo "Attempting to install .NET using Microsoft repository..."
    
    # Add Microsoft package repository
    curl -sSL https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
    
    # Add repository based on OS version
    if [ -f /etc/os-release ]; then
        . /etc/os-release
        if [[ $ID == "debian" || $ID == "raspbian" ]]; then
            if [[ $VERSION_ID == "11" ]]; then
                # Debian/Raspbian 11 (Bullseye)
                echo "deb [arch=$ARCH] https://packages.microsoft.com/debian/11/prod bullseye main" | sudo tee /etc/apt/sources.list.d/microsoft-prod.list
            elif [[ $VERSION_ID == "10" ]]; then
                # Debian/Raspbian 10 (Buster)
                echo "deb [arch=$ARCH] https://packages.microsoft.com/debian/10/prod buster main" | sudo tee /etc/apt/sources.list.d/microsoft-prod.list
            else
                # Default to Debian 11
                echo "deb [arch=$ARCH] https://packages.microsoft.com/debian/11/prod bullseye main" | sudo tee /etc/apt/sources.list.d/microsoft-prod.list
            fi
        else
            # Default to Debian 11
            echo "Unknown OS: $ID. Using Debian 11 repository."
            echo "deb [arch=$ARCH] https://packages.microsoft.com/debian/11/prod bullseye main" | sudo tee /etc/apt/sources.list.d/microsoft-prod.list
        fi
    fi
    
    # Update and install
    sudo apt-get update
    
    # Try .NET SDK 9.0 first
    echo "Trying to install .NET SDK 9.0 from Microsoft repository..."
    if sudo apt-get install -y dotnet-sdk-9.0; then
        echo "Successfully installed .NET SDK 9.0 from Microsoft repository."
        return 0
    else
        # Fall back to .NET SDK 6.0
        echo "Failed to install .NET SDK 9.0. Trying .NET SDK 6.0..."
        if sudo apt-get install -y dotnet-sdk-6.0; then
            echo "Successfully installed .NET SDK 6.0 from Microsoft repository."
            return 0
        else
            echo "Failed to install .NET SDK 6.0 from Microsoft repository."
            return 1
        fi
    fi
}

# Function to try downloading and installing the binary directly
try_binary_install() {
    echo "Attempting to install .NET using direct binary download..."
    
    # Create temporary directory
    TEMP_DIR=$(mktemp -d)
    cd $TEMP_DIR
    
    # Set URLs for the .NET binaries
    if [ "$ARCH" = "armhf" ]; then
        URL="https://download.visualstudio.microsoft.com/download/pr/8a1fd3c6-84ce-45e7-85a7-84c5e779bce0/a9260f6167fe021e5591730395f515e8/dotnet-sdk-6.0.418-linux-arm.tar.gz"
    elif [ "$ARCH" = "arm64" ]; then
        URL="https://download.visualstudio.microsoft.com/download/pr/f301d951-3ced-4d57-b8dd-cd382cef8425/b64233ee84acd5919d89b4dc983d6ccf/dotnet-sdk-6.0.418-linux-arm64.tar.gz"
    else
        echo "Unsupported architecture for direct binary installation: $ARCH"
        return 1
    fi
    
    # Try different download methods
    echo "Downloading .NET SDK..."
    if command -v curl &> /dev/null; then
        if ! curl -L -o dotnet-sdk.tar.gz $URL; then
            echo "curl download failed, trying wget..."
            if ! wget -O dotnet-sdk.tar.gz $URL; then
                echo "wget download failed too."
                return 1
            fi
        fi
    elif command -v wget &> /dev/null; then
        if ! wget -O dotnet-sdk.tar.gz $URL; then
            echo "wget download failed."
            return 1
        fi
    else
        echo "Neither curl nor wget is available. Cannot download .NET SDK."
        return 1
    fi
    
    # Create installation directory
    sudo mkdir -p /usr/share/dotnet
    
    # Extract the SDK
    echo "Extracting .NET SDK..."
    sudo tar -zxf dotnet-sdk.tar.gz -C /usr/share/dotnet
    
    # Create symbolic links
    echo "Setting up symbolic links..."
    sudo ln -sf /usr/share/dotnet/dotnet /usr/bin/dotnet
    
    # Clean up
    cd - > /dev/null
    rm -rf $TEMP_DIR
    
    # Verify installation
    if dotnet --list-sdks; then
        echo "Successfully installed .NET SDK using direct binary download."
        return 0
    else
        echo "Failed to install .NET SDK using direct binary download."
        return 1
    fi
}

# Function to check if .NET is already installed
check_dotnet_installed() {
    if command -v dotnet &> /dev/null; then
        echo ".NET is already installed:"
        dotnet --info
        return 0
    else
        echo ".NET is not installed or not in PATH."
        return 1
    fi
}

# Main installation logic
main() {
    # Check if .NET is already installed
    if check_dotnet_installed; then
        echo "Using existing .NET installation."
        return 0
    fi
    
    # Install prerequisites
    echo "Installing prerequisites..."
    sudo apt-get update || true
    sudo apt-get install -y curl wget apt-transport-https libunwind8 gettext libssl1.1 liblttng-ust0 libcurl4 libkrb5-3 zlib1g libicu* || true
    
    # Try the direct command first
    echo "Attempting to install .NET SDK 9.0 using direct command..."
    if sudo apt-get update && sudo apt-get install -y dotnet-sdk-9.0; then
        echo "Successfully installed .NET SDK 9.0 using direct command."
    else
        echo "Direct install command failed, trying other methods..."
        
        # Try each installation method
        if ! try_apt_install; then
            echo "APT installation failed, trying Microsoft repository..."
            if ! try_ms_repo_install; then
                echo "Microsoft repository installation failed, trying direct binary download..."
                if ! try_binary_install; then
                    echo "All installation methods failed!"
                    return 1
                fi
            fi
        fi
    fi
    
    # Build the Hacker Terminal application
    echo "Building Hacker Terminal application..."
    cd ~/linux_hacker_theme_video/hacker-terminal
    dotnet build -c Release
    
    # Make run script executable
    chmod +x run.sh
    
    echo "=== Installation Complete ==="
    echo "To run the application, use: ./run.sh"
    return 0
}

# Call the main function
main
exit $?
