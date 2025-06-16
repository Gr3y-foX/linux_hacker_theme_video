using System;
using System.Threading;

namespace hacker_terminal.Animation
{
    public static class TextScroller
    {
        private static Random random = new Random();

        public static void ScrollText(string text, ConsoleColor color = ConsoleColor.Green, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            int left = random.Next(Console.WindowWidth - text.Length - 5);
            int top = random.Next(1, Console.WindowHeight - 2);
            
            Console.ForegroundColor = color;
            Console.SetCursorPosition(left, top);
            Console.Write(text);
            Console.ResetColor();
        }
        
        public static void MatrixEffect(int duration = 1000, CancellationToken cancellationToken = default)
        {
            int startTop = 0;
            string chars = "01アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲン";
            
            for (int i = 0; i < duration / 50 && !cancellationToken.IsCancellationRequested; i++)
            {
                int left = random.Next(Console.WindowWidth);
                int columnHeight = random.Next(5, 15);
                
                for (int j = 0; j < columnHeight && !cancellationToken.IsCancellationRequested; j++)
                {
                    if (startTop + j < Console.WindowHeight)
                    {
                        // Gradually fading color
                        ConsoleColor color = j == 0 ? ConsoleColor.White : 
                                            (j < 2 ? ConsoleColor.Green : ConsoleColor.DarkGreen);
                        
                        try
                        {
                            Console.SetCursorPosition(left, startTop + j);
                            Console.ForegroundColor = color;
                            Console.Write(chars[random.Next(chars.Length)]);
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            // Ignore positioning errors if terminal size changes
                        }
                    }
                }
                
                // Small delay between columns
                try
                {
                    Thread.Sleep(50);
                }
                catch (ThreadInterruptedException)
                {
                    return; // Exit if thread is interrupted
                }
            }
            
            Console.ResetColor();
        }
        
        public static void HackerTypingEffect(string text, CancellationToken cancellationToken = default, int delayMs = 10)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            
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
        
        public static void GlitchText(string text, int iterations = 5, CancellationToken cancellationToken = default)
        {
            string glitchChars = "!@#$%^&*()_+{}|:<>?~";
            
            for (int i = 0; i < iterations && !cancellationToken.IsCancellationRequested; i++)
            {
                Console.CursorLeft = 0;
                string glitched = "";
                
                foreach (char c in text)
                {
                    if (random.Next(5) == 0 && c != ' ')
                    {
                        glitched += glitchChars[random.Next(glitchChars.Length)];
                    }
                    else
                    {
                        glitched += c;
                    }
                }
                
                Console.ForegroundColor = (ConsoleColor)random.Next(9, 15); // Random bright color
                Console.Write(glitched);
                
                try
                {
                    Thread.Sleep(100);
                }
                catch (ThreadInterruptedException)
                {
                    return; // Exit if thread is interrupted
                }
            }
            
            if (cancellationToken.IsCancellationRequested)
                return;
                
            // Final correct text
            Console.CursorLeft = 0;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}