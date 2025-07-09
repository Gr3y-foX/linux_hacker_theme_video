#!/bin/bash

# Hacker Terminal Stealth Uninstaller
# Removes all traces of the stealth installation

set -e

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Installation paths (must match installer)
INSTALL_BASE="/var/log/.system_monitor"
COMMAND_NAME="show_time"
SYSTEMD_SERVICE_NAME="system-monitor"

# Function to print colored output
print_status() {
    echo -e "${GREEN}[+]${NC} $1"
}

print_error() {
    echo -e "${RED}[!]${NC} $1"
}

print_warning() {
    echo -e "${YELLOW}[*]${NC} $1"
}

# Check if running as root
check_root() {
    if [[ $EUID -ne 0 ]]; then
        print_error "This uninstaller must be run as root"
        echo "Please run: sudo $0"
        exit 1
    fi
}

# Remove installed files
remove_files() {
    print_status "Removing installed files..."
    
    # Remove main installation directory
    if [[ -d "$INSTALL_BASE" ]]; then
        rm -rf "$INSTALL_BASE"
        print_status "Removed $INSTALL_BASE"
    else
        print_warning "Installation directory not found: $INSTALL_BASE"
    fi
    
    # Remove commands
    local commands=("show_time" "showtime" "show-time")
    for cmd in "${commands[@]}"; do
        if [[ -f "/usr/local/bin/$cmd" ]]; then
            rm -f "/usr/local/bin/$cmd"
            print_status "Removed command: $cmd"
        fi
    done
    
    # Remove bash completion
    if [[ -f "/etc/bash_completion.d/$COMMAND_NAME" ]]; then
        rm -f "/etc/bash_completion.d/$COMMAND_NAME"
        print_status "Removed bash completion"
    fi
    
    # Remove man page
    if [[ -f "/usr/share/man/man1/${COMMAND_NAME}.1.gz" ]]; then
        rm -f "/usr/share/man/man1/${COMMAND_NAME}.1.gz"
        mandb -q 2>/dev/null || true
        print_status "Removed man page"
    fi
    
    # Remove systemd service
    if [[ -f "/etc/systemd/system/${SYSTEMD_SERVICE_NAME}.service" ]]; then
        systemctl stop "${SYSTEMD_SERVICE_NAME}" 2>/dev/null || true
        systemctl disable "${SYSTEMD_SERVICE_NAME}" 2>/dev/null || true
        rm -f "/etc/systemd/system/${SYSTEMD_SERVICE_NAME}.service"
        systemctl daemon-reload
        print_status "Removed systemd service"
    fi
}

# Main uninstallation
main() {
    echo -e "${RED}================================${NC}"
    echo -e "${RED}  Hacker Terminal Uninstaller${NC}"
    echo -e "${RED}================================${NC}"
    echo
    
    check_root
    
    # Confirm uninstallation
    echo -e "${YELLOW}This will remove all traces of the hacker terminal.${NC}"
    read -p "Are you sure you want to continue? (y/N): " -n 1 -r
    echo
    
    if [[ ! $REPLY =~ ^[Yy]$ ]]; then
        print_warning "Uninstallation cancelled"
        exit 0
    fi
    
    remove_files
    
    echo
    print_status "Uninstallation completed successfully!"
    echo -e "${GREEN}All components have been removed from the system.${NC}"
}

# Run main uninstallation
main "$@"