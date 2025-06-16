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
sudo apt-get install -y curl libunwind8 gettext apt-transport-https wget ca-certificates

# Choose correct .NET installation method based on architecture
if [ "$ARCH" = "armhf" ] || [ "$ARCH" = "arm64" ]; then
    echo "Installing .NET SDK for ARM architecture using Microsoft's repository..."
    
    # Remove any previous problematic installations
    echo "Checking for previous installations..."
    if [ -d "/usr/lib/dotnet" ] && [ ! -d "/usr/lib/dotnet/host/fxr" ]; then
        echo "Removing incomplete .NET installation..."
        sudo rm -rf /usr/lib/dotnet
    fi
    
    # Add Microsoft repository
    echo "Adding Microsoft package repository..."
    curl -sSL https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
    
    # Add repository based on OS version
    if [ -f /etc/os-release ]; then
        . /etc/os-release
        if [[ $ID == "debian" ]]; then
            echo "Detected Debian-based system: $VERSION_ID"
            if [[ $VERSION_ID == "11" ]]; then
                # Debian 11 (Bullseye)
                echo "deb [arch=$ARCH] https://packages.microsoft.com/debian/11/prod bullseye main" | sudo tee /etc/apt/sources.list.d/microsoft-prod.list
            elif [[ $VERSION_ID == "10" ]]; then
                # Debian 10 (Buster)
                echo "deb [arch=$ARCH] https://packages.microsoft.com/debian/10/prod buster main" | sudo tee /etc/apt/sources.list.d/microsoft-prod.list
            else
                # Default to latest Debian
                echo "deb [arch=$ARCH] https://packages.microsoft.com/debian/11/prod bullseye main" | sudo tee /etc/apt/sources.list.d/microsoft-prod.list
            fi
        elif [[ $ID == "raspbian" ]]; then
            echo "Detected Raspbian: $VERSION_ID"
            # Use Debian repositories for Raspbian
            if [[ $VERSION_ID == "11" ]]; then
                echo "deb [arch=$ARCH] https://packages.microsoft.com/debian/11/prod bullseye main" | sudo tee /etc/apt/sources.list.d/microsoft-prod.list
            elif [[ $VERSION_ID == "10" ]]; then
                echo "deb [arch=$ARCH] https://packages.microsoft.com/debian/10/prod buster main" | sudo tee /etc/apt/sources.list.d/microsoft-prod.list
            else
                echo "deb [arch=$ARCH] https://packages.microsoft.com/debian/11/prod bullseye main" | sudo tee /etc/apt/sources.list.d/microsoft-prod.list
            fi
        elif [[ $ID == "ubuntu" ]]; then
            echo "Detected Ubuntu: $VERSION_ID"
            echo "deb [arch=$ARCH] https://packages.microsoft.com/ubuntu/$VERSION_ID/prod $(lsb_release -cs) main" | sudo tee /etc/apt/sources.list.d/microsoft-prod.list
        else
            echo "Unknown OS: $ID. Using Debian 11 repository."
            echo "deb [arch=$ARCH] https://packages.microsoft.com/debian/11/prod bullseye main" | sudo tee /etc/apt/sources.list.d/microsoft-prod.list
        fi
    else
        echo "Could not determine OS version. Using Debian 11 repository."
        echo "deb [arch=$ARCH] https://packages.microsoft.com/debian/11/prod bullseye main" | sudo tee /etc/apt/sources.list.d/microsoft-prod.list
    fi
    
    # Update package list
    echo "Updating package list..."
    sudo apt-get update
    
    # Install .NET SDK
    echo "Installing .NET 9.0 SDK via apt..."
    if sudo apt-get install -y dotnet-sdk-9.0; then
        echo "Successfully installed .NET SDK 9.0."
    else
        echo "Failed to install .NET SDK 9.0. Falling back to .NET SDK 6.0..."
        sudo apt-get install -y dotnet-sdk-6.0
    fi
    
    # Verify installation
    echo "Verifying .NET installation..."
    dotnet --info
else
    echo "Unsupported architecture: $ARCH"
    echo "This script supports armhf (32-bit ARM) and arm64 (64-bit ARM) architectures."
    exit 1
fi

# If we get here, .NET should be installed
if ! command -v dotnet &> /dev/null; then
    echo "ERROR: .NET installation failed. Trying alternative method..."
    
    # Creating a simple test script to check if we can run simple .NET commands
    echo "Using direct apt method..."
    sudo apt-get update
    
    # Try installing .NET 9.0 first
    if sudo apt-get install -y dotnet-sdk-9.0; then
        echo "Successfully installed .NET SDK 9.0 using direct method."
    else
        echo "Failed to install .NET SDK 9.0. Trying .NET 6.0..."
        sudo apt-get install -y dotnet-runtime-6.0 aspnetcore-runtime-6.0 dotnet-sdk-6.0
    fi
    
    if ! command -v dotnet &> /dev/null; then
        echo "ERROR: Failed to install .NET using repository method."
        echo "Please try installing .NET manually following the Microsoft documentation."
        exit 1
    fi
fi

# Build the Hacker Terminal application
echo "Building Hacker Terminal application..."
dotnet build -c Release

# Make run script executable
chmod +x run.sh

echo "=== Installation Complete ==="
echo "To run the application, use: ./run.sh"