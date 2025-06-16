using System;
using System.Threading;
using hacker_terminal.Assets;

namespace hacker_terminal.UI
{
    public static class ErrorDisplay
    {
        private static readonly Random random = new Random();
        
        public static void ShowError(string message)
        {
            Console.ForegroundColor = ColorScheme.ErrorColor;
            Console.WriteLine($"[ERROR] {message}");
            Console.ResetColor();
        }

        public static void ShowCriticalError(string message)
        {
            // Save current background color
            ConsoleColor originalBg = Console.BackgroundColor;
            
            // Flash effect for critical error
            for (int i = 0; i < 3; i++)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"[CRITICAL ERROR] {message}");
                Thread.Sleep(200);
                
                Console.BackgroundColor = originalBg;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[CRITICAL ERROR] {message}");
                Thread.Sleep(200);
            }
            
            // Reset colors
            Console.BackgroundColor = originalBg;
            Console.ResetColor();
        }
        
        public static void ShowWarning(string message)
        {
            Console.ForegroundColor = ColorScheme.WarningColor;
            Console.WriteLine($"[WARNING] {message}");
            Console.ResetColor();
        }
        
        public static void ShowRandomError()
        {
            string errorMessage = TextResources.GetRandomErrorMessage();
            
            // Determine error type randomly
            int errorType = random.Next(10);
            
            if (errorType < 2)
            {
                ShowCriticalError(errorMessage);
            }
            else if (errorType < 5)
            {
                ShowWarning(errorMessage);
            }
            else
            {
                ShowError(errorMessage);
            }
        }
        
        public static void ShowSystemAlert(string message)
        {
            // System alert with box
            string line = new string('═', message.Length + 4);
            
            Console.ForegroundColor = ColorScheme.SystemColor;
            Console.WriteLine($"╔{line}╗");
            Console.WriteLine($"║  {message}  ║");
            Console.WriteLine($"╚{line}╝");
            Console.ResetColor();
        }
        
        public static void ShowRandomSystemAlert()
        {
            ShowSystemAlert(TextResources.GetRandomSystemResponse());
        }
    }
}