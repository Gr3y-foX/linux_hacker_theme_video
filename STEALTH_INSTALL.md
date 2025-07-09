# Stealth Installation Guide

## Overview

The stealth installation method installs the Hacker Terminal in a way that makes it blend seamlessly into the Linux system, appearing as a legitimate system monitoring tool.

## Features

### 1. Hidden Installation Location
- Files are installed to `/var/log/.system_monitor/` - a hidden directory in the system logs
- The main binary is named `sysmon` to appear as a system monitoring service
- Configuration files are stored in a standard Linux directory structure

### 2. System Integration
- Creates a system-wide command `show_time` available from any directory
- Includes bash completion for professional feel
- Provides a proper man page (`man show_time`)
- Creates alternative command names: `showtime`, `show-time`

### 3. Professional Appearance
- Dummy systemd service file (inactive) to appear in service listings
- Decoy log files that look like legitimate system monitoring logs
- Help and version flags work like standard Linux commands
- Proper error messages and terminal compatibility checks

### 4. Covert Features
- Automatically detects SSH sessions and disables keyboard interceptor
- Can be run with `--enhanced` flag for full effect
- Blends in with other system commands

## Installation

### Prerequisites
- Root access (sudo)
- Git installed
- Internet connection (for .NET SDK download)

### Install Command
```bash
sudo chmod +x install_stealth.sh
sudo ./install_stealth.sh
```

### What Gets Installed

1. **Hidden Directory Structure:**
   ```
   /var/log/.system_monitor/
   ├── bin/
   │   └── sysmon              # Main wrapper script
   ├── lib/
   │   ├── hacker-terminal.dll # Compiled application
   │   └── *.dll               # Dependencies
   ├── config/
   │   └── effects_config.yaml # Effect configuration
   └── logs/
       └── service.log         # Decoy log file
   ```

2. **System Commands:**
   - `/usr/local/bin/show_time`
   - `/usr/local/bin/showtime` (symlink)
   - `/usr/local/bin/show-time` (symlink)

3. **Documentation:**
   - `/usr/share/man/man1/show_time.1.gz` - Man page
   - `/etc/bash_completion.d/show_time` - Bash completion

4. **Service File:**
   - `/etc/systemd/system/system-monitor.service` - Dummy service (inactive)

## Usage

### Basic Usage
```bash
show_time                    # Run with default settings
show_time --enhanced         # Run with keyboard interceptor
show_time --help            # Display help
show_time --version         # Display version
man show_time               # View manual page
```

### Command Options
- `--help`, `-h` - Display help information
- `--version`, `-v` - Display version information
- `--enhanced` - Enable keyboard interceptor
- `--no-intercept` - Explicitly disable keyboard interceptor

### Exit Methods
- **ESC** - Normal exit (when interceptor disabled)
- **Ctrl+C** - Exit with kill switch animation
- **Ctrl+Shift+X** - Force exit (when interceptor enabled)

## Uninstallation

To completely remove all traces:

```bash
sudo chmod +x uninstall_stealth.sh
sudo ./uninstall_stealth.sh
```

This will remove:
- All installed files and directories
- System commands
- Man page and bash completion
- Systemd service file

## Security Notes

1. **Sudo Required**: Installation requires root access to write to system directories
2. **Keyboard Interceptor**: Only active when explicitly enabled or in local sessions
3. **No Persistence**: Does not auto-start or modify system startup
4. **Clean Uninstall**: Uninstaller removes all traces

## Troubleshooting

### Command Not Found
If `show_time` is not found after installation:
1. Check if `/usr/local/bin` is in your PATH
2. Try using the full path: `/usr/local/bin/show_time`
3. Restart your shell or run: `source ~/.bashrc`

### Keyboard Interceptor Issues
- Requires Python 3 with pynput module
- Automatically disabled in SSH sessions
- Use `--no-intercept` to explicitly disable

### Build Errors
- Ensure .NET SDK 9.0 is properly installed
- Check internet connection for package downloads
- Review build output in installation log

## Advanced Usage

### Custom Effects Configuration
Edit `/var/log/.system_monitor/config/effects_config.yaml` to customize the effect sequence.

### Integration with Scripts
```bash
# Check if installed
if command -v show_time &> /dev/null; then
    show_time --enhanced
fi
```

### Flipper Zero Deployment
Use `flipper_zero_scripts/linux_stealth_prank.txt` for automated deployment via BadUSB.