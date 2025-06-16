# Hacker Terminal

## Overview
Hacker Terminal is a visually impressive application designed for Linux terminal environments, featuring an immersive hacker theme. The application utilizes ASCII art, dynamic text effects, advanced animations, and realistic visual effects to create an authentic hacker terminal experience.

## Features
- **Extensive ASCII Art Collection**: 
  - Multiple themed ASCII images including skulls, computers, and hacker graphics
  - Animated ASCII art with frame transitions
  - Big Money-NW font style banners for dramatic visual impact
  - Cybersecurity-themed visual elements (firewalls, encryption, etc.)
- **Advanced Visual Effects**:
  - Matrix Effect: Authentic Matrix-style falling character animation with color gradients
  - Digital rain with centered ASCII art
  - Glitching artifacts and visual distortion
  - Scanning effect with highlighted lines
  - Banner randomization transitions
- **Dynamic Text Effects**: 
  - Scrolling text with randomized positioning
  - Typing effect that simulates human typing
  - Glitch text effects that simulate data corruption
- **Advanced Loading Animations**:
  - Progress bars with customizable styles
  - Pulsing loading indicators
  - Incremental loading with percentage indicators
- **Visual Error Handling**: 
  - Flashing error signals with random messages and recovery simulation
  - Multiple error levels with appropriate visual feedback
  - Warning system alerts with styled boxes
- **Terminal Effects**:
  - Static noise simulation
  - Centered banners with borders
  - Color-coded information
  - Simulated terminal command input
- **Controls**:
  - Press `Esc` to exit gracefully
  - Press `Ctrl+C` to activate kill switch and terminate with visual feedback

## Installation

### Ubuntu Server
```bash
# Run the installation script
chmod +x install_ubuntu.sh
./install_ubuntu.sh

# Run the application
./run.sh
```

### Raspberry Pi
```bash
# Run the Raspberry Pi installation script
chmod +x install_raspberry_pi.sh
./install_raspberry_pi.sh

# Run the application
./run.sh
```

### Offline Installation
If you have all the dependencies downloaded already:
```bash
chmod +x install_offline.sh
./install_offline.sh
```

### macOS via Homebrew (for development)
```bash
chmod +x setup_homebrew_dotnet.sh
./setup_homebrew_dotnet.sh
```

## Usage
Once running, the application will display a series of animations and text effects. The application runs in a continuous loop, showing various hacker-themed visuals and animations. Press `ESC` key to exit normally or use `Ctrl+C` for emergency kill switch with visual feedback.

## Project Structure
```
hacker-terminal
├── src
│   ├── Program.cs               # Main application entry point
│   ├── Animation
│   │   ├── AsciiAnimator.cs     # ASCII art animation system
│   │   ├── LoadingBar.cs        # Loading bar animations
│   │   └── TextScroller.cs      # Text scrolling and Matrix effects
│   ├── Assets
│   │   ├── AsciiArt.cs          # Extensive ASCII art collection
│   │   └── TextResources.cs     # Text content and phrases
│   ├── UI
│   │   ├── ColorScheme.cs       # Color configurations
│   │   ├── ErrorDisplay.cs      # Error visualization
│   │   └── TerminalRenderer.cs  # Terminal drawing utilities
│   └── Utils
│       ├── RandomTextGenerator.cs # Text generation
│       └── TerminalHelper.cs      # Terminal utilities
├── .gitignore
├── hacker-terminal.csproj
├── install_ubuntu.sh           # Ubuntu installation script
├── install_raspberry_pi.sh     # Raspberry Pi installation script
├── install_offline.sh          # Offline installation script
├── setup_homebrew_dotnet.sh    # macOS development setup
├── fix_namespaces.sh           # Namespace consistency tool
└── run.sh                      # Run script
```

## Visual Effects
The application implements several visual effects:

1. **Skull Animation**: Animated skull frames with transitions
2. **Matrix Rain**: Classic falling character effect
3. **Banner Transitions**: Randomized banner transitions
4. **Scanning Effect**: Red scanning line passing over ASCII art
5. **Error Flashing**: Flashing screens for error warnings
6. **Terminal Glitching**: Simulated terminal glitches and artifacts
7. **ASCII Art in Matrix Rain**: Digital rain with focused ASCII art in the center

## Requirements
- .NET 9.0 SDK or later
- Terminal with ANSI color support
- Minimum terminal size: 80x24 characters
- Recommended terminal size: 120x40 characters for best visual experience

## GitHub Repository
https://github.com/Gr3y-foX/linux_hacker_theme_video.git

## License
MIT License