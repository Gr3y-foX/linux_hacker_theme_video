#!/bin/bash

# Script to set up the Hacker Terminal using an existing Homebrew installation of .NET
# This is useful when .NET is already installed but not in the standard locations

echo "=== Hacker Terminal Setup Script for Homebrew .NET ==="

# Check if we have a Homebrew .NET installation
if [ -f "/home/linuxbrew/.linuxbrew/bin/dotnet" ]; then
    echo "Found .NET installed via Homebrew:"
    /home/linuxbrew/.linuxbrew/bin/dotnet --info
    
    # Create symbolic link for system-wide access
    echo "Creating symbolic link for system-wide access..."
    sudo ln -sf /home/linuxbrew/.linuxbrew/bin/dotnet /usr/local/bin/dotnet
    
    # Add to PATH if not already there
    if [[ ":$PATH:" != *":/home/linuxbrew/.linuxbrew/bin:"* ]]; then
        echo "Adding Homebrew bin directory to PATH for this session..."
        export PATH="/home/linuxbrew/.linuxbrew/bin:$PATH"
    fi
    
    # Build the application
    echo "Building Hacker Terminal application..."
    cd "$(dirname "$0")"  # Navigate to the script's directory
    /home/linuxbrew/.linuxbrew/bin/dotnet build -c Release
    
    # Make run script executable
    chmod +x run.sh
    
    echo "=== Setup Complete ==="
    echo "To run the application, use: ./run.sh"
    exit 0
else
    echo "Homebrew .NET installation not found at /home/linuxbrew/.linuxbrew/bin/dotnet"
    echo "This script is specifically for systems with Homebrew-installed .NET."
    echo "To find where your dotnet is installed, run: which dotnet"
    echo "If you see a path, try to create a symbolic link manually:"
    echo "sudo ln -sf [your_dotnet_path] /usr/local/bin/dotnet"
    exit 1
fi
