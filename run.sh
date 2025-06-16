#!/bin/bash

# Set script to exit on error
set -e

# Check if the build exists, if not build it
if [ ! -d "./bin/Release/net6.0" ]; then
    echo "Build not found. Building project..."
    dotnet build -c Release
fi

# Run the application
dotnet run --project . --no-build -c Release