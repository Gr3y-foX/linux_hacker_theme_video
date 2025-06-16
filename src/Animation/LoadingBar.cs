using System;
using System.Threading;
using System.Threading.Tasks;

namespace hacker_terminal.Animation
{
    public class LoadingBar
    {
        private const int BarLength = 40;
        private const char ProgressChar = '█';
        private const char EmptyChar = '░';

        public static async Task ShowAsync(string message, int percentage, ConsoleColor color = ConsoleColor.Green)
        {
            Console.WriteLine($"\n{message}");
            Console.ForegroundColor = color;
            Console.Write("[");
            
            int filledLength = (int)(BarLength * percentage / 100);
            
            for (int i = 0; i < BarLength; i++)
            {
                Console.Write(i < filledLength ? ProgressChar : EmptyChar);
                await Task.Delay(10); // Animate the loading bar
            }
            
            Console.Write($"] {percentage}%");
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[ERROR] {message}");
            Console.Write("[");
            
            for (int i = 0; i < BarLength; i++)
            {
                if (i % 2 == 0)
                {
                    Console.Write(ProgressChar);
                }
                else
                {
                    Console.Write(EmptyChar);
                }
            }
            
            Console.Write("] FAILED");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}