using System;

namespace HackerTerminal.UI
{
    public class ErrorDisplay
    {
        private readonly ColorScheme _colorScheme;

        public ErrorDisplay(ColorScheme colorScheme)
        {
            _colorScheme = colorScheme;
        }

        public void ShowError(string message)
        {
            Console.ForegroundColor = _colorScheme.ErrorColor;
            Console.WriteLine($"[ERROR] {message}");
            Console.ResetColor();
        }

        public void ShowCriticalError(string message)
        {
            Console.ForegroundColor = _colorScheme.CriticalErrorColor;
            Console.WriteLine($"[CRITICAL ERROR] {message}");
            Console.ResetColor();
        }
    }
}