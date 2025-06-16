// filepath: /Users/phenix/Projects/hacker_theme_linux/hacker-terminal/src/UI/ColorScheme.cs
using System;

namespace hacker_terminal.UI
{
    public static class ColorScheme
    {
        // Main color scheme
        public static ConsoleColor PrimaryColor { get; private set; } = ConsoleColor.Green;
        public static ConsoleColor SecondaryColor { get; private set; } = ConsoleColor.DarkGreen;
        public static ConsoleColor AccentColor { get; private set; } = ConsoleColor.Cyan;
        public static ConsoleColor HighlightColor { get; private set; } = ConsoleColor.Yellow;
        
        // Status colors
        public static ConsoleColor SuccessColor { get; private set; } = ConsoleColor.Green;
        public static ConsoleColor ErrorColor { get; private set; } = ConsoleColor.Red;
        public static ConsoleColor WarningColor { get; private set; } = ConsoleColor.Yellow;
        public static ConsoleColor InfoColor { get; private set; } = ConsoleColor.Cyan;
        public static ConsoleColor SystemColor { get; private set; } = ConsoleColor.Blue;
        
        // Animation colors
        public static ConsoleColor MatrixHeadColor { get; private set; } = ConsoleColor.White;
        public static ConsoleColor MatrixTailColor { get; private set; } = ConsoleColor.DarkGreen;
        public static ConsoleColor LoadingBarColor { get; private set; } = ConsoleColor.Green;
        public static ConsoleColor LoadingTextColor { get; private set; } = ConsoleColor.White;
        
        // Attack theme colors
        public static ConsoleColor AttackColor { get; private set; } = ConsoleColor.Red;
        public static ConsoleColor DefenseColor { get; private set; } = ConsoleColor.Blue;
        public static ConsoleColor CodeColor { get; private set; } = ConsoleColor.DarkYellow;
        public static ConsoleColor AlertColor { get; private set; } = ConsoleColor.Magenta;
        
        // Apply default color scheme
        public static void ApplyDefaultColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = PrimaryColor;
        }
        
        // Apply matrix color scheme (primarily green)
        public static void ApplyMatrixColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            PrimaryColor = ConsoleColor.Green;
            SecondaryColor = ConsoleColor.DarkGreen;
            AccentColor = ConsoleColor.White;
        }
        
        // Apply attack color scheme (primarily red)
        public static void ApplyAttackColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            PrimaryColor = ConsoleColor.Red;
            SecondaryColor = ConsoleColor.DarkRed;
            AccentColor = ConsoleColor.Yellow;
        }
        
        // Apply cyber blue color scheme
        public static void ApplyCyberBlueColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
            PrimaryColor = ConsoleColor.Cyan;
            SecondaryColor = ConsoleColor.DarkCyan;
            AccentColor = ConsoleColor.White;
        }
        
        // Get a random color from the bright console colors
        public static ConsoleColor GetRandomBrightColor()
        {
            ConsoleColor[] brightColors = { 
                ConsoleColor.White, 
                ConsoleColor.Yellow, 
                ConsoleColor.Magenta, 
                ConsoleColor.Red, 
                ConsoleColor.Cyan, 
                ConsoleColor.Green
            };
            
            return brightColors[new Random().Next(brightColors.Length)];
        }
    }
}
