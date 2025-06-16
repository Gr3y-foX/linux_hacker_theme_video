using System;
using System.Threading;

namespace hacker_terminal.UI
{
    public static class TerminalRenderer
    {
        private static Random random = new Random();
        
        public static void ClearScreen()
        {
            Console.Clear();
        }

        public static void WriteText(string text, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(text);
            }
            catch (ArgumentOutOfRangeException)
            {
                // Safely handle terminal resize issues
            }
        }

        public static void PrintWithColor(string text, ConsoleColor color = ConsoleColor.Green)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void PrintCentered(string text, ConsoleColor color = ConsoleColor.Green)
        {
            int left = (Console.WindowWidth - text.Length) / 2;
            int top = Console.CursorTop;
            
            Console.ForegroundColor = color;
            WriteText(text, Math.Max(0, left), top);
            Console.WriteLine();
            Console.ResetColor();
        }
        
        public static void PrintWithTypingEffect(string text, int delayMs = 10, ConsoleColor color = ConsoleColor.Green, CancellationToken cancellationToken = default)
        {
            Console.ForegroundColor = color;
            
            foreach (char c in text)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;
                
                Console.Write(c);
                try 
                {
                    Thread.Sleep(delayMs);
                }
                catch (ThreadInterruptedException)
                {
                    return; // Exit if thread is interrupted
                }
            }
            
            Console.WriteLine();
            Console.ResetColor();
        }
        
        public static void PrintBanner(string text, ConsoleColor color = ConsoleColor.Green)
        {
            int width = Math.Min(Console.WindowWidth - 4, 80); // Limit width to prevent overflow
            int padding = (width - text.Length) / 2;
            
            // Draw top border
            Console.ForegroundColor = color;
            Console.Write("╔");
            for (int i = 0; i < width; i++) Console.Write("═");
            Console.WriteLine("╗");
            
            // Draw content with padding
            Console.Write("║");
            for (int i = 0; i < padding; i++) Console.Write(" ");
            Console.Write(text);
            for (int i = 0; i < padding; i++) Console.Write(" ");
            // Adjust for odd lengths
            if ((width - text.Length) % 2 != 0) Console.Write(" ");
            Console.WriteLine("║");
            
            // Draw bottom border
            Console.Write("╚");
            for (int i = 0; i < width; i++) Console.Write("═");
            Console.WriteLine("╝");
            Console.ResetColor();
        }
        
        public static void SimulateStaticNoise(int durationMs = 500, CancellationToken cancellationToken = default)
        {
            int height = Console.WindowHeight;
            int width = Console.WindowWidth;
            
            DateTime end = DateTime.Now.AddMilliseconds(durationMs);
            
            while (DateTime.Now < end && !cancellationToken.IsCancellationRequested)
            {
                for (int i = 0; i < 10 && !cancellationToken.IsCancellationRequested; i++)
                {
                    int x = random.Next(width);
                    int y = random.Next(height);
                    
                    try
                    {
                        Console.SetCursorPosition(x, y);
                        char randomChar = (char)random.Next(33, 126); // ASCII printable chars
                        Console.Write(randomChar);
                    }
                    catch
                    {
                        // Ignore positioning errors
                    }
                }
                
                try
                {
                    Thread.Sleep(50);
                }
                catch (ThreadInterruptedException)
                {
                    return; // Exit if thread is interrupted
                }
            }
        }
    }
}