#!/bin/bash

# This script helps install the hacker-terminal on Raspberry Pi by using 
# a pre-compiled version instead of building it locally

# ANSI color codes
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
BOLD='\033[1m'
NC='\033[0m' # No Color

echo -e "${GREEN}${BOLD}"
echo "╔══════════════════════════════════════════════════════╗"
echo "║       QUICK INSTALL FOR HACKER TERMINAL               ║"
echo "╚══════════════════════════════════════════════════════╝"
echo -e "${NC}"

# Get the directory of this script
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

# Check for .NET runtime
echo -e "${BLUE}[*] Checking for .NET runtime...${NC}"
if ! command -v dotnet &>/dev/null; then
    echo -e "${YELLOW}[!] .NET runtime not found. Installing...${NC}"
    
    # Check which package manager we have
    if command -v apt &>/dev/null; then
        # Ubuntu/Debian/Raspberry Pi OS
        wget https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
        sudo dpkg -i packages-microsoft-prod.deb
        rm packages-microsoft-prod.deb
        
        sudo apt-get update
        sudo apt-get install -y apt-transport-https
        sudo apt-get update
        sudo apt-get install -y dotnet-runtime-9.0
    else
        echo -e "${RED}[!] Unsupported package manager. Please install .NET runtime manually.${NC}"
        echo -e "${YELLOW}[*] Visit: https://dotnet.microsoft.com/download/dotnet/9.0${NC}"
        exit 1
    fi
fi

# Create a temporary directory for downloads
TEMP_DIR=$(mktemp -d)
echo -e "${BLUE}[*] Created temporary directory: $TEMP_DIR${NC}"

# Download the pre-built version
echo -e "${BLUE}[*] Downloading pre-built version...${NC}"
wget -O "$TEMP_DIR/hacker-terminal-prebuilt.zip" https://github.com/Gr3y-foX/linux_hacker_theme_video/releases/download/v1.0.0/hacker-terminal-prebuilt.zip || {
    echo -e "${RED}[!] Download failed. Creating a GitHub release...${NC}"
    
    # If the download fails, we need to build it here and create a release
    echo -e "${BLUE}[*] Building a prebuilt package...${NC}"
    
    # Make sure we have dotnet SDK
    if ! dotnet --list-sdks | grep -q "9.0"; then
        echo -e "${YELLOW}[!] .NET SDK 9.0 not found. Some features might not work correctly.${NC}"
    fi
    
    # Build the application
    echo -e "${BLUE}[*] Building the application...${NC}"
    dotnet publish "$SCRIPT_DIR" -c Release -o "$TEMP_DIR/publish"
    
    # Create a zip file
    echo -e "${BLUE}[*] Creating a zip file...${NC}"
    cd "$TEMP_DIR/publish" && zip -r "$TEMP_DIR/hacker-terminal-prebuilt.zip" . && cd -
    
    echo -e "${YELLOW}[!] Created a prebuilt package. Please upload it manually to a release.${NC}"
    echo -e "${BLUE}[*] Prebuilt package location: $TEMP_DIR/hacker-terminal-prebuilt.zip${NC}"
    
    # Copy it to the current directory
    cp "$TEMP_DIR/hacker-terminal-prebuilt.zip" "$SCRIPT_DIR/"
}

# Extract the pre-built version
echo -e "${BLUE}[*] Extracting pre-built version...${NC}"
mkdir -p "$SCRIPT_DIR/bin/Release/net9.0"
unzip -o "$TEMP_DIR/hacker-terminal-prebuilt.zip" -d "$SCRIPT_DIR/bin/Release/net9.0" || {
    echo -e "${RED}[!] Failed to extract the pre-built version.${NC}"
    echo -e "${YELLOW}[!] Trying to create and use a local build...${NC}"
    
    # Copy from the build we did earlier if extraction fails
    if [ -d "$TEMP_DIR/publish" ]; then
        echo -e "${BLUE}[*] Using the local build...${NC}"
        cp -R "$TEMP_DIR/publish/"* "$SCRIPT_DIR/bin/Release/net9.0/"
    else
        echo -e "${RED}[!] No local build available. Trying to build...${NC}"
        dotnet publish "$SCRIPT_DIR" -c Release
    fi
}

# Fixing permissions
echo -e "${BLUE}[*] Fixing permissions...${NC}"
find "$SCRIPT_DIR/bin" -type d -exec chmod 755 {} \;
find "$SCRIPT_DIR/bin" -type f -exec chmod 644 {} \;
find "$SCRIPT_DIR/bin" -name "*.sh" -exec chmod +x {} \;
find "$SCRIPT_DIR/bin" -name "*.py" -exec chmod +x {} \;
find "$SCRIPT_DIR/bin" -name "hacker-terminal" -exec chmod +x {} \;

# Run keyboard interceptor setup
echo -e "${BLUE}[*] Setting up keyboard interceptor...${NC}"
chmod +x "$SCRIPT_DIR/setup_keyboard_interceptor.sh"
"$SCRIPT_DIR/setup_keyboard_interceptor.sh"

# Make run scripts executable
echo -e "${BLUE}[*] Making run scripts executable...${NC}"
chmod +x "$SCRIPT_DIR/run.sh"
chmod +x "$SCRIPT_DIR/run_with_intercept.sh"

# Clean up
echo -e "${BLUE}[*] Cleaning up...${NC}"
rm -rf "$TEMP_DIR"

echo -e "${GREEN}[✓] Installation complete!${NC}"
echo ""
echo -e "${YELLOW}[!] You can now run the application with:${NC}"
echo -e "${BLUE}    ./run.sh${NC}"
echo ""
echo -e "${YELLOW}[!] Or with keyboard interceptor:${NC}"
echo -e "${BLUE}    ./run_with_intercept.sh${NC}"
echo ""
echo -e "${YELLOW}[!] Remember: Use Ctrl+Shift+X to activate the kill switch when keyboard interceptor is active${NC}"
