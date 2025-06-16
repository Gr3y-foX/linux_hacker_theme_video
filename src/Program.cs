using System;
using System.Threading;
using hacker_terminal.Animation;
using hacker_terminal.Assets;
using hacker_terminal.UI;

namespace hacker_terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            TerminalRenderer.RenderAsciiArt(AsciiArt.GetSkull());
            ColorScheme.ApplyDefaultColors();

            Console.WriteLine(TextResources.WelcomeMessage);
            LoadingBar loadingBar = new LoadingBar();
            loadingBar.Start();

            // Main loop for displaying animations and text
            while (true)
            {
                TextScroller textScroller = new TextScroller();
                textScroller.StartScrolling(TextResources.DynamicText);
                Thread.Sleep(5000); // Display scrolling text for 5 seconds
                textScroller.StopScrolling();

                // Simulate some processing
                Thread.Sleep(2000);
                Console.Clear();
            }
        }
    }
}