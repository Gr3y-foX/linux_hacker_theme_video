using System;

namespace HackerTerminal.Utils
{
    public static class TerminalHelper
    {
        public static void ClearScreen()
        {
            Console.Clear();
        }

        public static void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }

        public static void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public static void Write(string message)
        {
            Console.Write(message);
        }
    }
}