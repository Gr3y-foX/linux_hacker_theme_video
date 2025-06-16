// filepath: /Users/phenix/Projects/hacker_theme_linux/hacker-terminal/src/UI/ColorScheme.cs
using System;

namespace hacker_terminal.UI
{
    public static class ColorScheme
    {
        public static readonly ConsoleColor DefaultBackground = ConsoleColor.Black;
        public static readonly ConsoleColor DefaultForeground = ConsoleColor.Green;
        public static readonly ConsoleColor ErrorColor = ConsoleColor.Red;
        public static readonly ConsoleColor WarningColor = ConsoleColor.Yellow;
        public static readonly ConsoleColor SuccessColor = ConsoleColor.Green;
        public static readonly ConsoleColor InfoColor = ConsoleColor.Cyan;
        public static readonly ConsoleColor[] RainbowColors = new ConsoleColor[]
        {
            ConsoleColor.Red,
            ConsoleColor.Yellow,
            ConsoleColor.Green,
            ConsoleColor.Cyan,
            ConsoleColor.Blue,
            ConsoleColor.Magenta
        };

        public static void ApplyDefaultColors()
        {
            Console.BackgroundColor = DefaultBackground;
            Console.ForegroundColor = DefaultForeground;
            Console.Clear();
        }

        public static void ApplyErrorColor()
        {
            Console.ForegroundColor = ErrorColor;
        }

        public static void ApplySuccessColor()
        {
            Console.ForegroundColor = SuccessColor;
        }

        public static void ApplyWarningColor()
        {
            Console.ForegroundColor = WarningColor;
        }

        public static void ApplyInfoColor()
        {
            Console.ForegroundColor = InfoColor;
        }

        public static void ResetColor()
        {
            Console.ForegroundColor = DefaultForeground;
        }
    }
}
