#!/bin/bash

# Set script to exit on error
set -e

# Check if dotnet is available in the path
if ! command -v dotnet &> /dev/null; then
    if [ -f "/usr/share/dotnet/dotnet" ]; then
        echo "Using .NET from /usr/share/dotnet/"
        DOTNET_PATH="/usr/share/dotnet/dotnet"
    else
        echo "Error: .NET not found. Please run install_ubuntu.sh or install_raspberry_pi.sh first."
        exit 1
    fi
else
    DOTNET_PATH="dotnet"
fi

# Check if the build exists, if not build it
if [ ! -d "./bin/Release/net6.0" ]; then
    echo "Build not found. Building project..."
    $DOTNET_PATH build -c Release
fi

# Run the application
$DOTNET_PATH run --project . --no-build -c Release