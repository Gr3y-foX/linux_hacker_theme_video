using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace hacker_terminal.Utils
{
    public static class TerminalHelper
    {
        private static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        private static readonly bool IsLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        private static readonly bool IsMacOS = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        
        /// <summary>
        /// Safely clears the terminal screen
        /// </summary>
        public static void ClearScreen()
        {
            try
            {
                Console.Clear();
            }
            catch
            {
                // Fallback for environments where Clear doesn't work
                Console.Write("\u001b[2J\u001b[H");
            }
        }

        /// <summary>
        /// Safely sets cursor position with bounds checking
        /// </summary>
        public static void SetCursorPosition(int left, int top)
        {
            try
            {
                int safeLeft = Math.Max(0, Math.Min(left, Console.WindowWidth - 1));
                int safeTop = Math.Max(0, Math.Min(top, Console.WindowHeight - 1));
                Console.SetCursorPosition(safeLeft, safeTop);
            }
            catch
            {
                // Fallback for terminal positioning
                Console.Write($"\u001b[{top + 1};{left + 1}H");
            }
        }
        
        /// <summary>
        /// Safely writes a line with color support
        /// </summary>
        public static void WriteLine(string message, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
            }
            finally
            {
                Console.ForegroundColor = originalColor;
            }
        }

        /// <summary>
        /// Safely writes text with color support
        /// </summary>
        public static void Write(string message, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = color;
                Console.Write(message);
            }
            finally
            {
                Console.ForegroundColor = originalColor;
            }
        }
        
        /// <summary>
        /// Returns detection of current OS
        /// </summary>
        public static string GetOSPlatform()
        {
            if (IsWindows) return "Windows";
            if (IsLinux) return "Linux";
            if (IsMacOS) return "macOS";
            return "Unknown";
        }
        
        /// <summary>
        /// Gets the current terminal size
        /// </summary>
        public static (int width, int height) GetTerminalSize()
        {
            try
            {
                return (Console.WindowWidth, Console.WindowHeight);
            }
            catch
            {
                // Fallback for environments where WindowWidth/Height aren't available
                return (80, 24); // Standard terminal size
            }
        }
        
        /// <summary>
        /// Displays information about the terminal environment
        /// </summary>
        public static void ShowTerminalInfo()
        {
            var (width, height) = GetTerminalSize();
            WriteLine($"OS: {GetOSPlatform()}", ConsoleColor.Cyan);
            WriteLine($"Terminal Size: {width}x{height}", ConsoleColor.Cyan);
            WriteLine($"Time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}", ConsoleColor.Cyan);
        }
    }
}