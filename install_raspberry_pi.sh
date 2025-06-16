#!/bin/bash

# Set script to exit on error
set -e

echo "=== Hacker Terminal Installation Script for Raspberry Pi ==="
echo "This script will install the .NET SDK and the Hacker Terminal application"

# Get architecture
ARCH=$(dpkg --print-architecture)
echo "Detected architecture: $ARCH"

# Install prerequisites
echo "Installing prerequisites..."
sudo apt-get update
sudo apt-get install -y curl libunwind8 gettext apt-transport-https

# Choose correct .NET installation method based on architecture
if [ "$ARCH" = "armhf" ] || [ "$ARCH" = "arm64" ]; then
    echo "Installing .NET SDK for ARM architecture..."
    
    # Remove any previous installations
    echo "Checking for previous installations..."
    if [ -d "/usr/share/dotnet" ]; then
        echo "Removing previous .NET installation..."
        sudo rm -rf /usr/share/dotnet
    fi
    if [ -d "/usr/lib/dotnet" ]; then
        echo "Removing previous .NET libraries..."
        sudo rm -rf /usr/lib/dotnet
    fi
    
    # Download and install .NET 6.0
    echo "Downloading .NET 6.0..."
    
    if [ "$ARCH" = "armhf" ]; then
        # 32-bit ARM (Raspberry Pi OS 32-bit)
        wget https://download.visualstudio.microsoft.com/download/pr/40d4eada-74cc-410a-9121-f5322c8c0494/246cf0ed83e7f54cabf50e28a1e5ac43/dotnet-sdk-6.0.416-linux-arm.tar.gz -O dotnet-sdk.tar.gz
    elif [ "$ARCH" = "arm64" ]; then
        # 64-bit ARM (Raspberry Pi OS 64-bit)
        wget https://download.visualstudio.microsoft.com/download/pr/ec1ec366-7a90-404f-9a47-ce9a99d21059/36b21e0a75ffd98abee43eafbaf152ff/dotnet-sdk-6.0.416-linux-arm64.tar.gz -O dotnet-sdk.tar.gz
    fi
    
    echo "Creating installation directory..."
    sudo mkdir -p /usr/share/dotnet
    
    echo "Extracting .NET SDK..."
    sudo tar -zxf dotnet-sdk.tar.gz -C /usr/share/dotnet
    
    echo "Setting up environment..."
    sudo ln -sf /usr/share/dotnet/dotnet /usr/bin/dotnet
    
    # Remove the downloaded file
    rm dotnet-sdk.tar.gz
    
    # Verify installation
    echo "Verifying .NET installation..."
    dotnet --info
else
    echo "Unsupported architecture: $ARCH"
    echo "This script supports armhf (32-bit ARM) and arm64 (64-bit ARM) architectures."
    exit 1
fi

# Build the Hacker Terminal application
echo "Building Hacker Terminal application..."
dotnet build -c Release

# Make run script executable
chmod +x run.sh

echo "=== Installation Complete ==="
echo "To run the application, use: ./run.sh"