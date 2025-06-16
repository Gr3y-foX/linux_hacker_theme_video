using System;
using System.Threading;
using System.Threading.Tasks;
using hacker_terminal.Animation;
using hacker_terminal.Assets;
using hacker_terminal.UI;
using hacker_terminal.Utils;

namespace hacker_terminal
{
    class Program
    {
        static readonly Random random = new Random();
        
        // Flag to signal that the application should exit
        private static bool shouldExit = false;
        
        // CancellationTokenSource for stopping tasks when Ctrl+C is pressed
        private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        
        static async Task Main(string[] args)
        {
            // Setup Ctrl+C handler
            Console.CancelKeyPress += HandleCancelKeyPress;
            
            // Setup
            Console.Clear();
            Console.CursorVisible = false;
            ColorScheme.ApplyDefaultColors();
            
            try
            {
                // Intro sequence
                await RunIntroSequence();
                
                if (!shouldExit)
                {
                    // Main animation loop
                    await RunMainLoop();
                }
                
                if (!shouldExit)
                {
                    // Exit sequence
                    await RunExitSequence();
                }
            }
            catch (OperationCanceledException)
            {
                // Handle cancellation gracefully
                await CleanupAndExit();
            }
            catch (Exception ex)
            {
                Console.ResetColor();
                Console.CursorVisible = true;
                Console.WriteLine($"The application encountered an error: {ex.Message}");
            }
            finally
            {
                Console.ResetColor();
                Console.CursorVisible = true;
                
                // Unregister the event handler to allow the process to exit
                Console.CancelKeyPress -= HandleCancelKeyPress;
            }
        }
        
        /// <summary>
        /// Handles the Ctrl+C key press to gracefully exit the application
        /// </summary>
        private static void HandleCancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        {
            // Prevent the default Ctrl+C behavior which terminates immediately
            e.Cancel = true;
            
            // Set flag to exit gracefully
            shouldExit = true;
            
            // Signal cancellation to all operations
            cancellationTokenSource.Cancel();
            
            // Display exit message
            Task.Run(async () => await CleanupAndExit());
        }
        
        /// <summary>
        /// Performs cleanup and shows exit message when Ctrl+C is pressed
        /// </summary>
        private static async Task CleanupAndExit()
        {
            TerminalRenderer.ClearScreen();
            TerminalRenderer.PrintBanner(TextResources.KillSwitchMessage, ConsoleColor.Red);
            await LoadingBar.ShowIncrementalAsync("Terminating processes...", 800);
            TerminalRenderer.PrintWithTypingEffect("\nConnection terminated by user. Goodbye.", 20, ConsoleColor.Yellow);
            Thread.Sleep(500);
        }
        
        private static async Task RunIntroSequence()
        {
            // Display skull animation for a dramatic intro
            await AsciiAnimator.PlayAnimationAsync("skull", ConsoleColor.Green, 250, 2, cancellationTokenSource.Token);
            
            if (shouldExit) return;
            
            // Randomize then show the hacking banner
            await AsciiAnimator.RandomizeThenShowBannerAsync(AsciiArt.HackingBanner, 8, cancellationTokenSource.Token);
            
            if (shouldExit) return;
            
            // Typing effect for welcome message
            TerminalRenderer.PrintWithTypingEffect(TextResources.WelcomeMessage, 50, ConsoleColor.Cyan);
            Thread.Sleep(500);
            
            // Check if should exit after each significant operation
            if (shouldExit) return;

            // Simulate connection sequence
            TerminalRenderer.PrintWithColor("Establishing secure connection...", ConsoleColor.Yellow);
            await LoadingBar.ShowPulseLoadingBarAsync("Bypassing security protocols...", 2, cancellationTokenSource.Token);
            
            if (shouldExit) return;
            
            // Matrix effect
            TextScroller.MatrixEffect(1000, cancellationTokenSource.Token);
            
            if (shouldExit) return;
            
            // Show scanning effect over security banner
            await AsciiAnimator.ScanEffectAsync(AsciiArt.SecurityBanner, ConsoleColor.Red, cancellationTokenSource.Token);
            
            if (shouldExit) return;
            
            // Banner
            TerminalRenderer.PrintBanner("SYSTEM COMPROMISED", ConsoleColor.Red);
            Thread.Sleep(200);
            
            if (shouldExit) return;
            
            // Initial loading
            await LoadingBar.ShowIncrementalAsync("Initializing hacker terminal...", 1500, cancellationTokenSource.Token);
            
            if (shouldExit) return;
            
            // Static noise for effect
            TerminalRenderer.SimulateStaticNoise(300);
            
            if (shouldExit) return;
            
            // Ready message
            TextScroller.GlitchText("TERMINAL READY", 3);
            Thread.Sleep(500);
        }
        
        private static async Task RunMainLoop()
        {
            int counter = 0;
            
            while (!shouldExit)
            {
                // Exit on ESC key press
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    break;
                }
                
                counter++;
                
                // Check if cancellation was requested
                if (cancellationTokenSource.Token.IsCancellationRequested)
                {
                    break;
                }
                
                // Main visual effects
                switch (counter % 10) // Expanded to include new effects
                {
                    case 0:
                        // Show random hacker phrases with typing effect
                        TextScroller.HackerTypingEffect(TextResources.GetRandomHackerPhrase(), cancellationTokenSource.Token);
                        if (shouldExit) return;
                        Thread.Sleep(100);
                        break;
                        
                    case 1:
                        // Scroll random text
                        for (int i = 0; i < 3 && !shouldExit; i++)
                        {
                            TextScroller.ScrollText(RandomTextGenerator.GetRandomText(), 
                                (ConsoleColor)random.Next(9, 15), cancellationTokenSource.Token); // Use bright colors
                            Thread.Sleep(50);
                        }
                        break;
                        
                    case 2:
                        if (shouldExit) return;
                        // Show code fragment
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"> {TextResources.GetRandomCodeFragment()}");
                        Console.ResetColor();
                        Thread.Sleep(200);
                        break;
                        
                    case 3:
                        // Mini matrix effect
                        TextScroller.MatrixEffect(500, cancellationTokenSource.Token);
                        break;
                        
                    case 4:
                        if (shouldExit) return;
                        // Loading indicator for some operation
                        await LoadingBar.ShowAsync(
                            TextResources.GetRandomHackerPhrase(), 
                            random.Next(75, 100), 
                            ConsoleColor.Cyan,
                            cancellationTokenSource.Token);
                        break;
                        
                    case 5:
                        if (shouldExit) return;
                        // Show random error occasionally
                        if (random.Next(3) == 0)
                        {
                            ErrorDisplay.ShowRandomError();
                        }
                        break;
                    
                    case 6:
                        if (shouldExit) return;
                        // Show random banner with glitch effect
                        await AsciiAnimator.GlitchAsciiArtAsync(AsciiArt.GetRandomBanner(), 1500, cancellationTokenSource.Token);
                        break;
                        
                    case 7:
                        if (shouldExit) return;
                        // Matrix rain with ascii art in center
                        string centerArt = random.Next(2) == 0 ? AsciiArt.Lock : AsciiArt.AnonymousMask;
                        await AsciiAnimator.MatrixRainWithArtAsync(centerArt, 2000, cancellationTokenSource.Token);
                        break;
                        
                    case 8:
                        if (shouldExit) return;
                        // Show security-themed banner
                        TerminalRenderer.PrintWithColor(AsciiArt.GetRandomSecurityBanner(), 
                            ColorScheme.GetRandomBrightColor());
                        Thread.Sleep(1000);
                        break;
                        
                    case 9:
                        if (shouldExit) return;
                        // Show terminal prompt with command
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(AsciiArt.TerminalPrompt);
                        TerminalRenderer.PrintWithTypingEffect(TextResources.GetRandomCodeFragment(), 10, ConsoleColor.White);
                        Thread.Sleep(200);
                        break;
                }
                
                if (shouldExit) return;
                
                // Occasional ASCII art
                if (counter % 25 == 0)
                {
                    TerminalRenderer.PrintWithColor(AsciiArt.GetRandomArt(), 
                        (ConsoleColor)random.Next(9, 15)); // Random bright color
                }
                
                Thread.Sleep(random.Next(100, 300));
            }
        }
        
        private static async Task RunExitSequence()
        {
            // Glitch text effect for exit message
            TextScroller.GlitchText(TextResources.ExitMessage, 3);
            
            // Show terminal banner with scanning effect
            await AsciiAnimator.ScanEffectAsync(AsciiArt.TerminalBanner, ConsoleColor.Yellow, cancellationTokenSource.Token);
            
            // Clearing traces loading bar
            await LoadingBar.ShowIncrementalAsync("Clearing traces...", 1000, cancellationTokenSource.Token);
            
            // Final banner
            TerminalRenderer.PrintBanner("CONNECTION TERMINATED", ConsoleColor.Yellow);
            
            // Static noise effect
            TerminalRenderer.SimulateStaticNoise(500);
            
            // Final pause
            Thread.Sleep(300);
        }
    }
}