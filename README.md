# Hacker Terminal

## Overview
Hacker Terminal is a small visual application designed for the Ubuntu OS server terminal, featuring a hacker theme. The application utilizes ASCII art, auto-generated text, and dynamic loading animations to create an engaging terminal experience.

## Features
- **ASCII Art**: Displays themed ASCII images, including skulls.
- **Text Scrolling**: Auto-generates and scrolls text in a loop for a dynamic feel.
- **Loading Bar**: Visual representation of loading processes.
- **Color Error Signals**: Displays error messages with color-coded signals for better visibility.

## Project Structure
```
hacker-terminal
├── src
│   ├── Program.cs
│   ├── Animation
│   │   ├── LoadingBar.cs
│   │   └── TextScroller.cs
│   ├── Assets
│   │   ├── AsciiArt.cs
│   │   └── TextResources.cs
│   ├── UI
│   │   ├── ColorScheme.cs
│   │   ├── ErrorDisplay.cs
│   │   └── TerminalRenderer.cs
│   └── Utils
│       ├── RandomTextGenerator.cs
│       └── TerminalHelper.cs
├── .gitignore
├── hacker-terminal.csproj
└── README.md
```

## Setup Instructions
1. Clone the repository:
   ```
   git clone https://github.com/Gr3y-foX/linux_hacker_theme_video.git
   ```
2. Navigate to the project directory:
   ```
   cd hacker-terminal
   ```
3. Build the project:
   ```
   dotnet build
   ```
4. Run the application:
   ```
   dotnet run
   ```

## Usage Guidelines
- The application is designed to run in a terminal environment. Ensure your terminal supports ANSI color codes for the best experience.
- Customize the ASCII art and text resources by modifying the `AsciiArt.cs` and `TextResources.cs` files in the `src/Assets` directory.
- Adjust the color scheme by editing the `ColorScheme.cs` file in the `src/UI` directory.

## Installation on Raspberry Pi
For Raspberry Pi users, we provide a specialized installation script:
```bash
# Clone the repository
git clone https://github.com/Gr3y-foX/linux_hacker_theme_video.git

# Navigate to the project directory
cd linux_hacker_theme_video/hacker-terminal

# Run the Raspberry Pi installation script
chmod +x install_raspberry_pi.sh
./install_raspberry_pi.sh

# Run the application
./run.sh
```

## Troubleshooting
If you encounter issues during installation or running the application:

### .NET Installation Issues
1. **Manual .NET Installation on Raspberry Pi**:
   ```bash
   # Remove any problematic installations
   sudo rm -rf /usr/lib/dotnet
   
   # Install .NET Runtime and SDK
   sudo apt-get update
   sudo apt-get install -y dotnet-runtime-6.0 dotnet-sdk-6.0
   ```

2. **Verify .NET Installation**:
   ```bash
   dotnet --info
   ```

3. **Direct Build and Run**:
   ```bash
   dotnet build -c Release -v detailed
   dotnet run -c Release
   ```

### Common Errors
- `[/usr/lib/dotnet/host/fxr] does not contain any version-numbered child folders`: This indicates an incomplete .NET installation. Use the installation script or follow the manual installation steps above.
- Download errors: Check your internet connection and try again.
- Build failures: Make sure you have enough disk space and RAM available.

## Contributing
Contributions are welcome! Please submit a pull request or open an issue for any suggestions or improvements.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.