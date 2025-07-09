#!/bin/bash

# Test script for stealth installation
# This script verifies that the stealth installation is working correctly

set -e

# Colors
GREEN='\033[0;32m'
RED='\033[0;31m'
YELLOW='\033[1;33m'
NC='\033[0m'

echo -e "${YELLOW}=== Hacker Terminal Stealth Installation Test ===${NC}"
echo

# Function to check if a file/directory exists
check_exists() {
    if [[ -e "$1" ]]; then
        echo -e "${GREEN}✓${NC} $2"
        return 0
    else
        echo -e "${RED}✗${NC} $2"
        return 1
    fi
}

# Function to check if a command exists
check_command() {
    if command -v "$1" &> /dev/null; then
        echo -e "${GREEN}✓${NC} Command '$1' is available"
        return 0
    else
        echo -e "${RED}✗${NC} Command '$1' not found"
        return 1
    fi
}

# Test installation paths
echo "Checking installation directories..."
check_exists "/var/log/.system_monitor" "Hidden directory exists"
check_exists "/var/log/.system_monitor/bin/sysmon" "Binary wrapper exists"
check_exists "/var/log/.system_monitor/lib/hacker-terminal.dll" "Main application exists"
check_exists "/var/log/.system_monitor/config/effects_config.yaml" "Configuration file exists"
check_exists "/var/log/.system_monitor/service.log" "Decoy log file exists"
echo

# Test system commands
echo "Checking system commands..."
check_command "show_time"
check_command "showtime"
check_command "show-time"
echo

# Test documentation
echo "Checking documentation..."
check_exists "/usr/share/man/man1/show_time.1.gz" "Man page exists"
check_exists "/etc/bash_completion.d/show_time" "Bash completion exists"
echo

# Test command functionality
echo "Testing command functionality..."
if show_time --version &> /dev/null; then
    echo -e "${GREEN}✓${NC} Version flag works"
    show_time --version
else
    echo -e "${RED}✗${NC} Version flag failed"
fi

if show_time --help &> /dev/null; then
    echo -e "${GREEN}✓${NC} Help flag works"
else
    echo -e "${RED}✗${NC} Help flag failed"
fi
echo

# Test systemd service
echo "Checking systemd service..."
if systemctl list-unit-files | grep -q "system-monitor.service"; then
    echo -e "${GREEN}✓${NC} Systemd service file exists"
    systemctl status system-monitor.service --no-pager 2>/dev/null || echo "(Service is inactive as expected)"
else
    echo -e "${RED}✗${NC} Systemd service file not found"
fi
echo

# Test man page
echo "Testing man page..."
if man show_time &> /dev/null; then
    echo -e "${GREEN}✓${NC} Man page is accessible"
else
    echo -e "${RED}✗${NC} Man page not accessible"
fi
echo

# Summary
echo -e "${YELLOW}=== Test Summary ===${NC}"
echo "If all checks passed, the stealth installation is working correctly."
echo "You can now use 'show_time' command from anywhere in the system."
echo
echo "To test the actual application (this will launch the hacker terminal):"
echo "  show_time --help     # View help"
echo "  show_time            # Run the application"
echo "  show_time --enhanced # Run with keyboard interceptor"