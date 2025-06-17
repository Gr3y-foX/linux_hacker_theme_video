#!/bin/bash

# Test script for keyboard interceptor on Raspberry Pi
# This script will help install and test the keyboard interceptor on Raspberry Pi

# ANSI color codes
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
BOLD='\033[1m'
NC='\033[0m' # No Color

echo -e "${GREEN}${BOLD}"
echo "╔══════════════════════════════════════════════════════╗"
echo "║      KEYBOARD INTERCEPTOR TEST - RASPBERRY PI         ║"
echo "╚══════════════════════════════════════════════════════╝"
echo -e "${NC}"

# Check if we're on Raspberry Pi
if grep -q "Raspberry Pi" /proc/device-tree/model 2>/dev/null; then
    echo -e "${GREEN}[+] Confirmed running on Raspberry Pi${NC}"
else
    echo -e "${YELLOW}[!] This doesn't appear to be a Raspberry Pi, but we'll continue anyway${NC}"
fi

# Check Python version
echo -e "${BLUE}[*] Checking Python version...${NC}"
python3 --version

# Check for system pynput package
echo -e "${BLUE}[*] Looking for system pynput package...${NC}"
if dpkg -l | grep -q python3-pynput; then
    echo -e "${GREEN}[+] System package python3-pynput is installed${NC}"
else
    echo -e "${YELLOW}[!] System package python3-pynput is not installed${NC}"
    echo -e "${BLUE}[*] Attempting to install it...${NC}"
    sudo apt update && sudo apt install -y python3-pynput
    if dpkg -l | grep -q python3-pynput; then
        echo -e "${GREEN}[+] System package python3-pynput installed successfully${NC}"
    else
        echo -e "${RED}[!] Failed to install system package python3-pynput${NC}"
    fi
fi

# Check for Python virtual environment tools
echo -e "${BLUE}[*] Checking for virtual environment tools...${NC}"
if dpkg -l | grep -q python3-venv; then
    echo -e "${GREEN}[+] python3-venv is installed${NC}"
else
    echo -e "${YELLOW}[!] python3-venv is not installed${NC}"
    echo -e "${BLUE}[*] Installing python3-venv...${NC}"
    sudo apt update && sudo apt install -y python3-venv python3-pip
    if dpkg -l | grep -q python3-venv; then
        echo -e "${GREEN}[+] python3-venv installed successfully${NC}"
    else
        echo -e "${RED}[!] Failed to install python3-venv${NC}"
    fi
fi

# Create a test virtual environment
echo -e "${BLUE}[*] Creating test virtual environment...${NC}"
python3 -m venv test_venv
if [ -d "test_venv" ]; then
    echo -e "${GREEN}[+] Virtual environment created successfully${NC}"
    echo -e "${BLUE}[*] Installing pynput in virtual environment...${NC}"
    ./test_venv/bin/pip install pynput
    if ./test_venv/bin/python -c "import pynput" &>/dev/null; then
        echo -e "${GREEN}[+] pynput installed successfully in virtual environment${NC}"
    else
        echo -e "${RED}[!] Failed to install pynput in virtual environment${NC}"
    fi
else
    echo -e "${RED}[!] Failed to create virtual environment${NC}"
fi

# Test Python keyboard interceptor
echo -e "${BLUE}[*] Testing keyboard interceptor (system Python)...${NC}"
if python3 -c "import pynput" &>/dev/null; then
    echo -e "${GREEN}[+] pynput is available in system Python${NC}"
    echo -e "${BLUE}[*] Testing keyboard interceptor with system Python...${NC}"
    echo -e "${YELLOW}[!] Press Ctrl+C to exit this test${NC}"
    python3 -c "
import time
from pynput import keyboard

print('Press Ctrl+C to exit test')
print('Testing keyboard monitoring...')

def on_press(key):
    try:
        print(f'Key pressed: {key.char}')
    except AttributeError:
        print(f'Special key pressed: {key}')

def on_release(key):
    print(f'Key released: {key}')
    if key == keyboard.Key.esc:
        # Stop listener
        return False

# Collect events for 5 seconds
with keyboard.Listener(on_press=on_press, on_release=on_release) as listener:
    try:
        time.sleep(5)
        print('\\nKeyboard monitoring test completed!')
    except KeyboardInterrupt:
        print('\\nTest interrupted by user')
    finally:
        listener.stop()
" || echo -e "${RED}[!] Failed to run keyboard test${NC}"
fi

# Test with virtual environment
if [ -d "test_venv" ]; then
    echo -e "${BLUE}[*] Testing keyboard interceptor (virtual environment)...${NC}"
    echo -e "${YELLOW}[!] Press Ctrl+C to exit this test${NC}"
    ./test_venv/bin/python -c "
import time
from pynput import keyboard

print('Press Ctrl+C to exit test')
print('Testing keyboard monitoring (virtual environment)...')

def on_press(key):
    try:
        print(f'Key pressed: {key.char}')
    except AttributeError:
        print(f'Special key pressed: {key}')

def on_release(key):
    print(f'Key released: {key}')
    if key == keyboard.Key.esc:
        # Stop listener
        return False

# Collect events for 5 seconds
with keyboard.Listener(on_press=on_press, on_release=on_release) as listener:
    try:
        time.sleep(5)
        print('\\nKeyboard monitoring test completed!')
    except KeyboardInterrupt:
        print('\\nTest interrupted by user')
    finally:
        listener.stop()
" || echo -e "${RED}[!] Failed to run keyboard test in virtual environment${NC}"
fi

# Cleanup
echo -e "${BLUE}[*] Cleaning up...${NC}"
rm -rf test_venv

echo -e "${GREEN}${BOLD}"
echo "╔══════════════════════════════════════════════════════╗"
echo "║              TEST COMPLETED                          ║"
echo "╚══════════════════════════════════════════════════════╝"
echo -e "${NC}"
echo -e "${YELLOW}[!] If keyboard monitoring tests were successful, then the keyboard interceptor should work${NC}"
echo -e "${BLUE}[*] Run the application with: ./run_with_intercept.sh${NC}"
