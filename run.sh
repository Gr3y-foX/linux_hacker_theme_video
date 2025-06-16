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
    
    # Check if build succeeded
    if [ $? -ne 0 ]; then
        echo "Build failed. Attempting to build with verbose logging..."
        $DOTNET_PATH build -c Release -v detailed
        
        if [ $? -ne 0 ]; then
            echo "Build failed again. Trying to run without pre-building..."
            $DOTNET_PATH run -c Release
            exit $?
        fi
    fi
fi

# Run the application
echo "Starting Hacker Terminal application..."
$DOTNET_PATH run --project . -c Release