using System;
using System.Threading;

namespace hacker_terminal.Animation
{
    public static class TextScroller
    {
        private static Random random = new Random();

        public static void ScrollText(string text, ConsoleColor color = ConsoleColor.Green)
        {
            int left = random.Next(Console.WindowWidth - text.Length - 5);
            int top = random.Next(1, Console.WindowHeight - 2);
            
            Console.ForegroundColor = color;
            Console.SetCursorPosition(left, top);
            Console.Write(text);
            Console.ResetColor();
        }
        
        public static void MatrixEffect(int duration = 1000)
        {
            int startTop = 0;
            string chars = "01";
            
            for (int i = 0; i < duration / 50; i++)
            {
                int left = random.Next(Console.WindowWidth);
                
                for (int j = 0; j < random.Next(5, 15); j++)
                {
                    if (startTop + j < Console.WindowHeight)
                    {
                        char c = chars[random.Next(chars.Length)];
                        Console.ForegroundColor = j == 0 ? ConsoleColor.White : ConsoleColor.DarkGreen;
                        Console.SetCursorPosition(left, startTop + j);
                        Console.Write(c);
                    }
                }
                
                Thread.Sleep(50);
            }
            
            Console.ResetColor();
        }
    }
}