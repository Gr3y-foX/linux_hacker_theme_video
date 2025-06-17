#!/bin/bash

# Keyboard interceptor setup for Hacker Terminal
# This script installs the necessary dependencies for the keyboard interceptor

# ANSI color codes
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
BOLD='\033[1m'
NC='\033[0m' # No Color

# Print banner
echo -e "${GREEN}${BOLD}"
echo "╔══════════════════════════════════════════════════════╗"
echo "║      KEYBOARD INTERCEPTOR SETUP - HACKER TERMINAL     ║"
echo "╚══════════════════════════════════════════════════════╝"
echo -e "${NC}"

# Check for Python installation
echo -e "${BLUE}[*] Checking for Python installation...${NC}"
if command -v python3 >/dev/null 2>&1; then
    PYTHON_CMD="python3"
    echo -e "${GREEN}[+] Python 3 found!${NC}"
elif command -v python >/dev/null 2>&1; then
    PYTHON_CMD="python"
    echo -e "${GREEN}[+] Python found!${NC}"
else
    echo -e "${RED}[!] Python not found. Installing Python 3...${NC}"
    if command -v apt >/dev/null 2>&1; then
        sudo apt update
        sudo apt install -y python3 python3-pip
        PYTHON_CMD="python3"
    elif command -v yum >/dev/null 2>&1; then
        sudo yum install -y python3 python3-pip
        PYTHON_CMD="python3"
    elif command -v dnf >/dev/null 2>&1; then
        sudo dnf install -y python3 python3-pip
        PYTHON_CMD="python3"
    elif command -v pacman >/dev/null 2>&1; then
        sudo pacman -S python python-pip
        PYTHON_CMD="python"
    elif command -v brew >/dev/null 2>&1; then
        brew install python
        PYTHON_CMD="python3"
    else
        echo -e "${RED}[!] Couldn't install Python. Please install Python 3 manually.${NC}"
        exit 1
    fi
    echo -e "${GREEN}[+] Python installed successfully!${NC}"
fi

# Check for pip and install if necessary
echo -e "${BLUE}[*] Checking for pip...${NC}"
if ! $PYTHON_CMD -m pip --version >/dev/null 2>&1; then
    echo -e "${YELLOW}[!] pip not found. Installing pip...${NC}"
    if command -v apt >/dev/null 2>&1; then
        sudo apt update
        sudo apt install -y python3-pip
    elif command -v yum >/dev/null 2>&1; then
        sudo yum install -y python3-pip
    elif command -v dnf >/dev/null 2>&1; then
        sudo dnf install -y python3-pip
    elif command -v pacman >/dev/null 2>&1; then
        sudo pacman -S python-pip
    elif command -v curl >/dev/null 2>&1; then
        curl https://bootstrap.pypa.io/get-pip.py -o get-pip.py
        $PYTHON_CMD get-pip.py
        rm get-pip.py
    else
        echo -e "${RED}[!] Couldn't install pip. Please install pip manually.${NC}"
        exit 1
    fi
    echo -e "${GREEN}[+] pip installed successfully!${NC}"
fi

# Install pynput for keyboard interception
echo -e "${BLUE}[*] Installing pynput package for keyboard interception...${NC}"
$PYTHON_CMD -m pip install pynput

# Check if it installed successfully
if $PYTHON_CMD -c "import pynput" >/dev/null 2>&1; then
    echo -e "${GREEN}[+] pynput package installed successfully!${NC}"
else
    echo -e "${RED}[!] Failed to install pynput package. Some features may not work correctly.${NC}"
fi

# Create keyboard interceptor Python script
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PYTHON_DIR="${SCRIPT_DIR}/python_scripts"
mkdir -p "$PYTHON_DIR"

echo -e "${BLUE}[*] Creating keyboard interceptor Python script...${NC}"

cat > "${PYTHON_DIR}/keyboard_interceptor.py" << 'EOF'
#!/usr/bin/env python3
# Keyboard interception for Hacker Terminal
import os
import sys
import time
import random
import signal
import threading

try:
    from pynput import keyboard
    from pynput.keyboard import Key, KeyCode
except ImportError:
    print('Installing pynput package...')
    import subprocess
    subprocess.check_call([sys.executable, '-m', 'pip', 'install', 'pynput'])
    from pynput import keyboard
    from pynput.keyboard import Key, KeyCode

# ANSI colors
RED = '\033[91m'
GREEN = '\033[92m'
RESET = '\033[0m'
BOLD = '\033[1m'

# Track currently pressed keys
current_keys = set()

# Define combinations to intercept
COMBINATIONS = [
    {'name': 'ctrl+c', 'keys': {Key.ctrl, KeyCode.from_char('c')}},
    {'name': 'ctrl+z', 'keys': {Key.ctrl, KeyCode.from_char('z')}},
    {'name': 'ctrl+d', 'keys': {Key.ctrl, KeyCode.from_char('d')}},
    {'name': 'alt+tab', 'keys': {Key.alt, Key.tab}},
    {'name': 'alt+f4', 'keys': {Key.alt, Key.f4}},
    {'name': 'cmd+q', 'keys': {Key.cmd, KeyCode.from_char('q')}},
    {'name': 'cmd+w', 'keys': {Key.cmd, KeyCode.from_char('w')}},
    {'name': 'cmd+tab', 'keys': {Key.cmd, Key.tab}},
]

# Add function keys
for i in range(1, 13):
    COMBINATIONS.append({
        'name': f'f{i}', 
        'keys': {getattr(Key, f'f{i}')}
    })

# Taunting messages
TAUNTING_MESSAGES = [
    'Huh, nice try)',
    'Oopsie doopsie kiddo, something went wrong?',
    'Nope, not today!',
    'You thought it would be that easy?',
    'Sorry, the exit is... elsewhere',
    'Those keys are disabled. Try harder!',
    'Clever, but not clever enough',
    'This lockdown isn\'t broken that easily',
    'Good luck getting out that way',
    'Access Denied: Unauthorized Escape Attempt'
]

# Exit combination - pressing this will stop the script
EXIT_COMBINATION = {Key.ctrl, Key.shift, KeyCode.from_char('x')}

def display_taunt():
    """Returns a random taunting message"""
    return random.choice(TAUNTING_MESSAGES)

def simulate_random_keypress():
    """Simulates a random alphanumeric key press"""
    random_char = random.choice('abcdefghijklmnopqrstuvwxyz0123456789')
    controller = keyboard.Controller()
    controller.press(random_char)
    time.sleep(0.05)
    controller.release(random_char)

def is_combination_pressed(combination_keys):
    """Check if a combination of keys is pressed"""
    return all(k in current_keys for k in combination_keys)

def on_press(key):
    """Handler for key press events"""
    # Add to currently pressed keys
    current_keys.add(key)
    
    # Check for exit combination first
    if is_combination_pressed(EXIT_COMBINATION):
        print('\n' + GREEN + BOLD + 'Exit combination detected. Stopping keyboard interceptor.' + RESET)
        return False  # Stop the listener
    
    # Check for combinations to intercept
    for combo in COMBINATIONS:
        if is_combination_pressed(combo['keys']):
            print(RED + display_taunt() + RESET)
            simulate_random_keypress()
            return False  # Stop this key from propagating further
    
    # Allow normal keys to pass through
    return True

def on_release(key):
    """Handler for key release events"""
    try:
        current_keys.remove(key)
    except KeyError:
        pass

def main():
    """Main function to run the keyboard interceptor"""
    print('Starting keyboard interceptor...')
    print('Press Alt+Q to exit interceptor.')
    
    # Handle Ctrl+C to prevent the script from being terminated
    def handle_sigint(signum, frame):
        print('\n' + RED + display_taunt() + RESET)
    
    signal.signal(signal.SIGINT, handle_sigint)
    
    with keyboard.Listener(on_press=on_press, on_release=on_release) as listener:
        listener.join()
    
    print('Keyboard interceptor stopped.')

if __name__ == '__main__':
    main()
EOF

# Make script executable
chmod +x "${PYTHON_DIR}/keyboard_interceptor.py"

# Create helper script to run the application with interceptor
cat > "${SCRIPT_DIR}/run_with_intercept.sh" << 'EOF'
#!/bin/bash
# Run hacker terminal with keyboard interception

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PYTHON_DIR="${SCRIPT_DIR}/python_scripts"

# Start keyboard interceptor in the background
echo "Starting keyboard interceptor..."
python3 "${PYTHON_DIR}/keyboard_interceptor.py" &
INTERCEPTOR_PID=$!

# Wait for a second to ensure interceptor is running
sleep 1

# Function to clean up on exit
cleanup() {
    echo "Stopping keyboard interceptor..."
    kill $INTERCEPTOR_PID 2>/dev/null
    exit 0
}

# Register cleanup function for Ctrl+C and other signals
trap cleanup INT TERM EXIT

# Run the main application
echo "Starting hacker terminal with interception..."
dotnet run --project "${SCRIPT_DIR}" -- --intercept

# Ensure interceptor is stopped before exiting
kill $INTERCEPTOR_PID 2>/dev/null
EOF

# Make helper script executable
chmod +x "${SCRIPT_DIR}/run_with_intercept.sh"

echo -e "${GREEN}${BOLD}[+] Keyboard interceptor setup complete!${NC}"
echo -e "${YELLOW}[*] To run the hacker terminal with keyboard interception, use:${NC}"
echo -e "${BLUE}    ./run_with_intercept.sh${NC}"
echo -e "${YELLOW}[*] When the application is running, press Ctrl+Shift+X to activate the kill switch${NC}"
echo ""
