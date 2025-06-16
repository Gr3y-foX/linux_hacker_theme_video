#!/bin/bash

# Don't exit on error to allow fallbacks
set +e

echo "=== Hacker Terminal Runner ==="

# Function to find dotnet executable
find_dotnet() {
    # Check common locations for dotnet
    if command -v dotnet &> /dev/null; then
        echo "Using dotnet from PATH"
        DOTNET_PATH="dotnet"
        # Check the version
        DOTNET_VERSION=$(dotnet --version | cut -d. -f1)
        echo "Detected .NET SDK version: $DOTNET_VERSION"
        return 0
    elif [ -f "/usr/share/dotnet/dotnet" ]; then
        echo "Using .NET from /usr/share/dotnet/"
        DOTNET_PATH="/usr/share/dotnet/dotnet"
        return 0
    elif [ -f "/usr/bin/dotnet" ]; then
        echo "Using .NET from /usr/bin/"
        DOTNET_PATH="/usr/bin/dotnet"
        return 0
    elif [ -f "/usr/local/bin/dotnet" ]; then
        echo "Using .NET from /usr/local/bin/"
        DOTNET_PATH="/usr/local/bin/dotnet"
        return 0
    else
        return 1
    fi
}

# Function to try to install .NET if it's missing
try_install_dotnet() {
    echo "Attempting to install .NET automatically..."
    
    if [ -f "./install_offline.sh" ]; then
        echo "Found offline installation script. Running it..."
        chmod +x ./install_offline.sh
        ./install_offline.sh
        if find_dotnet; then
            return 0
        fi
    fi
    
    echo "Installation failed. .NET not found."
    return 1
}

# Find dotnet or attempt to install it
if ! find_dotnet; then
    echo ".NET not found in common locations."
    if ! try_install_dotnet; then
        echo "Error: .NET not found and installation failed. Please run install_offline.sh first."
        exit 1
    fi
fi

# Try to run directly first
echo "Attempting to run the application directly..."
$DOTNET_PATH run -c Release
RUN_RESULT=$?

# If direct run failed, try building first
if [ $RUN_RESULT -ne 0 ]; then
    echo "Direct run failed. Attempting to build first..."
    
    # Try building with different configurations
    $DOTNET_PATH build -c Release
    BUILD_RESULT=$?
    
    if [ $BUILD_RESULT -ne 0 ]; then
        echo "Release build failed. Trying Debug build..."
        $DOTNET_PATH build -c Debug
        BUILD_RESULT=$?
        
        if [ $BUILD_RESULT -ne 0 ]; then
            echo "Debug build failed. Trying with minimal verbosity..."
            $DOTNET_PATH build -c Release -v minimal
            BUILD_RESULT=$?
            
            if [ $BUILD_RESULT -ne 0 ]; then
                echo "All build attempts failed. Attempting to run in Debug mode..."
                $DOTNET_PATH run -c Debug
                exit $?
            else
                # Run the Release build that succeeded
                echo "Build succeeded. Running application..."
                $DOTNET_PATH run -c Release
                exit $?
            fi
        else
            # Run the Debug build that succeeded
            echo "Debug build succeeded. Running application..."
            $DOTNET_PATH run -c Debug
            exit $?
        fi
    else
        # Run the Release build that succeeded
        echo "Build succeeded. Running application..."
        $DOTNET_PATH run -c Release
        exit $?
    fi
fi

# If we got here, the direct run was successful
exit $RUN_RESULT