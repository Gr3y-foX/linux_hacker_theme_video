using System;

namespace HackerTerminal.UI
{
    public class TerminalRenderer
    {
        public void ClearScreen()
        {
            Console.Clear();
        }

        public void WriteText(string text, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }

        public void RenderAsciiArt(string asciiArt)
        {
            Console.WriteLine(asciiArt);
        }
    }
}