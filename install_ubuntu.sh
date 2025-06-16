#!/bin/bash

# Set script to exit on error
set -e

echo "=== Hacker Terminal Installation Script ==="
echo "This script will install the .NET SDK and the Hacker Terminal application"

# Check if .NET SDK is installed
if ! command -v dotnet &> /dev/null; then
    echo "Installing .NET SDK..."
    # Install the Microsoft package repository
    wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
    sudo dpkg -i packages-microsoft-prod.deb
    rm packages-microsoft-prod.deb

    # Update package list and install .NET SDK
    sudo apt-get update
    sudo apt-get install -y apt-transport-https
    sudo apt-get update
    
    # Try to install .NET 9.0 SDK first
    echo "Trying to install .NET 9.0 SDK..."
    if sudo apt-get install -y dotnet-sdk-9.0; then
        echo "Successfully installed .NET 9.0 SDK"
    else
        # Fall back to .NET 6.0 SDK
        echo "Failed to install .NET 9.0 SDK. Falling back to .NET 6.0 SDK..."
        sudo apt-get install -y dotnet-sdk-6.0
    fi
else
    echo ".NET SDK is already installed"
fi

# Build the Hacker Terminal application
echo "Building Hacker Terminal application..."
dotnet build -c Release

# Make run script executable
chmod +x run.sh

echo "=== Installation Complete ==="
echo "To run the application, use: ./run.sh"