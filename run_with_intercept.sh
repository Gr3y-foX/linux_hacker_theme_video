#!/bin/bash

# Run hacker terminal with keyboard interception
#
# This script:
# 1. Checks for Python and required packages
# 2. Starts the keyboard interceptor in the background
# 3. Runs the hacker terminal with interception flag
# 4. Cleans up on exit

# ANSI color codes
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
BOLD='\033[1m'
NC='\033[0m' # No Color

# Get the directory of this script
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PYTHON_DIR="${SCRIPT_DIR}/python_scripts"
KEYBOARD_INTERCEPTOR="${PYTHON_DIR}/keyboard_interceptor.py"

# Print banner
echo -e "${GREEN}${BOLD}"
echo "╔═══════════════════════════════════════════════════╗"
echo "║       HACKER TERMINAL WITH KEYBOARD LOCKDOWN      ║"
echo "╚═══════════════════════════════════════════════════╝"
echo -e "${NC}"

# Check if Python is installed
echo -e "${BLUE}[*] Checking for Python...${NC}"
if command -v python3 &>/dev/null; then
    PYTHON_CMD="python3"
    echo -e "${GREEN}[+] Python 3 found!${NC}"
elif command -v python &>/dev/null; then
    PYTHON_CMD="python"
    echo -e "${GREEN}[+] Python found!${NC}"
else
    echo -e "${YELLOW}[!] Python not found. Running without keyboard interception.${NC}"
    echo -e "${YELLOW}[*] Run setup_keyboard_interceptor.sh first to enable full features.${NC}"
    
    # Run without keyboard interception
    echo -e "${BLUE}[*] Starting hacker terminal without keyboard interception...${NC}"
    cd "$SCRIPT_DIR"
    dotnet run
    exit 0
fi

# Check for pynput
echo -e "${BLUE}[*] Checking for pynput package...${NC}"
if ! $PYTHON_CMD -c "import pynput" &>/dev/null; then
    echo -e "${YELLOW}[!] The pynput package is required but not installed.${NC}"
    
    # Ask if we should try to install it
    read -p "Would you like to install pynput now? (y/n) " -n 1 -r
    echo
    if [[ $REPLY =~ ^[Yy]$ ]]; then
        echo -e "${BLUE}[*] Installing pynput...${NC}"
        $PYTHON_CMD -m pip install pynput
        
        # Check if installation succeeded
        if ! $PYTHON_CMD -c "import pynput" &>/dev/null; then
            echo -e "${RED}[!] Failed to install pynput. Running without keyboard interception.${NC}"
            cd "$SCRIPT_DIR"
            dotnet run
            exit 0
        fi
        echo -e "${GREEN}[+] Successfully installed pynput!${NC}"
    else
        echo -e "${YELLOW}[!] Running without keyboard interception.${NC}"
        cd "$SCRIPT_DIR"
        dotnet run
        exit 0
    fi
fi

# Check if keyboard interceptor script exists
if [ ! -f "$KEYBOARD_INTERCEPTOR" ]; then
    echo -e "${RED}[!] Keyboard interceptor script not found at: $KEYBOARD_INTERCEPTOR${NC}"
    echo -e "${YELLOW}[!] Run setup_keyboard_interceptor.sh first to set up the keyboard interceptor.${NC}"
    
    # Run without keyboard interception
    echo -e "${BLUE}[*] Starting hacker terminal without keyboard interception...${NC}"
    cd "$SCRIPT_DIR"
    dotnet run
    exit 0
fi

# Function to clean up processes on exit
cleanup() {
    echo -e "\n${BLUE}[*] Cleaning up...${NC}"
    
    # Kill the keyboard interceptor if it's running
    if [ -n "$INTERCEPTOR_PID" ]; then
        echo -e "${BLUE}[*] Stopping keyboard interceptor...${NC}"
        kill -9 "$INTERCEPTOR_PID" &>/dev/null
    fi
    
    echo -e "${GREEN}[+] Cleanup complete!${NC}"
    exit 0
}

# Register cleanup function to run on script exit
trap cleanup EXIT SIGINT SIGTERM

# Start keyboard interceptor in the background
echo -e "${BLUE}[*] Starting keyboard interceptor...${NC}"
"$KEYBOARD_INTERCEPTOR" &
INTERCEPTOR_PID=$!

# Wait a bit to ensure interceptor is running
sleep 1

# Check if interceptor is still running
if ! ps -p $INTERCEPTOR_PID &>/dev/null; then
    echo -e "${YELLOW}[!] Keyboard interceptor failed to start. Check permissions.${NC}"
    echo -e "${YELLOW}[*] Starting without keyboard interception...${NC}"
    cd "$SCRIPT_DIR"
    dotnet run
else
    echo -e "${GREEN}[+] Keyboard interceptor started with PID: $INTERCEPTOR_PID${NC}"
    echo -e "${YELLOW}[!] NOTE: To exit the application use Ctrl+Shift+X${NC}"
    
    # Run hacker terminal with the intercept flag
    echo -e "${BLUE}[*] Starting hacker terminal with keyboard interception...${NC}"
    cd "$SCRIPT_DIR"
    dotnet run -- --intercept
fi
