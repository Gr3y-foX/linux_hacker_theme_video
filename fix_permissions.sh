#!/bin/bash

# This script fixes permission issues with the hacker-terminal project
# It ensures that all directories and files have the proper permissions

# ANSI color codes
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
BOLD='\033[1m'
NC='\033[0m' # No Color

echo -e "${GREEN}${BOLD}"
echo "╔══════════════════════════════════════════════════════╗"
echo "║       FIXING PERMISSIONS FOR HACKER TERMINAL          ║"
echo "╚══════════════════════════════════════════════════════╝"
echo -e "${NC}"

# Get the directory of this script
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo -e "${BLUE}[*] Fixing permissions for directories...${NC}"
# Set directories to 755 (rwxr-xr-x)
find "$SCRIPT_DIR" -type d -exec chmod 755 {} \;

echo -e "${BLUE}[*] Fixing permissions for shell scripts...${NC}"
# Make all shell scripts executable
find "$SCRIPT_DIR" -name "*.sh" -exec chmod +x {} \;

echo -e "${BLUE}[*] Fixing permissions for Python scripts...${NC}"
# Make Python scripts executable
find "$SCRIPT_DIR" -name "*.py" -exec chmod +x {} \;

echo -e "${BLUE}[*] Setting permissions for project files...${NC}"
# Set regular files to 644 (rw-r--r--)
find "$SCRIPT_DIR" -type f -not -name "*.sh" -not -name "*.py" -exec chmod 644 {} \;

echo -e "${BLUE}[*] Ensuring obj and bin directories are writable...${NC}"
# Make obj and bin directories writable by everyone (for development purposes)
# This is needed for dotnet restore/build to work properly
if [ -d "$SCRIPT_DIR/obj" ]; then
    chmod -R 777 "$SCRIPT_DIR/obj"
fi

if [ -d "$SCRIPT_DIR/bin" ]; then
    chmod -R 777 "$SCRIPT_DIR/bin"
fi

# Create obj directory if it doesn't exist
if [ ! -d "$SCRIPT_DIR/obj" ]; then
    echo -e "${BLUE}[*] Creating obj directory...${NC}"
    mkdir -p "$SCRIPT_DIR/obj"
    chmod 777 "$SCRIPT_DIR/obj"
fi

echo -e "${GREEN}[✓] Permissions fixed successfully!${NC}"
echo ""
echo -e "${YELLOW}[!] Now you can run the application with:${NC}"
echo -e "${BLUE}    dotnet restore${NC}"
echo -e "${BLUE}    dotnet build${NC}"
echo -e "${BLUE}    dotnet run${NC}"
echo ""
echo -e "${YELLOW}[!] Or use the run script:${NC}"
echo -e "${BLUE}    ./run.sh${NC}"
echo ""
echo -e "${YELLOW}[!] Or with keyboard interceptor:${NC}"
echo -e "${BLUE}    ./run_with_intercept.sh${NC}"
