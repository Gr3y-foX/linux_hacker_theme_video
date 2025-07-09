using System;
using System.Threading;
using System.Threading.Tasks;
using hacker_terminal.Animation;
using hacker_terminal.Assets;
using hacker_terminal.UI;
using hacker_terminal.Utils;
using System.IO;
using System.Collections.Generic;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace hacker_terminal
{
    class Program
    {
        static readonly Random random = new Random();
        
        // Version information
        private const string VERSION = "2.3.1";
        private const string APP_NAME = "System Monitor (Hacker Terminal)";
        
        // Flag to signal that the application should exit
        private static bool shouldExit = false;
        
        // CancellationTokenSource for stopping tasks when Ctrl+C is pressed
        private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        
        // Flag to control keyboard interception
        private static bool keyboardInterceptorActive = false;
        
        // Config class for YAML
        public class EffectConfig
        {
            public List<EffectItem> Effects { get; set; } = new List<EffectItem>();
        }
        public class EffectItem
        {
            public string Name { get; set; } = string.Empty;
            public int Duration { get; set; } = 1000;
        }

        static Dictionary<string, Func<CancellationToken, Task>> effectMap;
        static EffectConfig? loadedConfig;
        
        static async Task Main(string[] args)
        {
            // Parse command line arguments
            if (ParseCommandLineArgs(args))
                return;
            
            // Setup Ctrl+C handler
            Console.CancelKeyPress += HandleCancelKeyPress;
            
            // Setup
            Console.Clear();
            Console.CursorVisible = false;
            ColorScheme.ApplyDefaultColors();
            
            try
            {
                // Start keyboard interceptor if requested and not explicitly disabled
                if (keyboardInterceptorActive)
                {
                    Utils.KeyboardInterceptor.Start();
                    TerminalRenderer.PrintWithTypingEffect("Keyboard interceptor activated. Use Ctrl+Shift+X to activate kill switch.", 20, ConsoleColor.Yellow);
                    Thread.Sleep(1000);
                }
                
                // Initialize effect map
                InitializeEffectMap();
                
                // Load effect configuration
                loadedConfig = LoadEffectConfig("effects_config.yaml");
                
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
                // Stop keyboard interceptor if it was started
                if (keyboardInterceptorActive)
                {
                    Utils.KeyboardInterceptor.Stop();
                }
                
                Console.ResetColor();
                Console.CursorVisible = true;
                
                // Unregister the event handler to allow the process to exit
                Console.CancelKeyPress -= HandleCancelKeyPress;
            }
        }
        
        /// <summary>
        /// Parse command line arguments and handle special commands
        /// </summary>
        /// <returns>True if the program should exit after handling args</returns>
        private static bool ParseCommandLineArgs(string[] args)
        {
            bool shouldExitAfterArgs = false;
            
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "--help":
                    case "-h":
                    case "/?":
                        ShowHelp();
                        shouldExitAfterArgs = true;
                        break;
                        
                    case "--version":
                    case "-v":
                        ShowVersion();
                        shouldExitAfterArgs = true;
                        break;
                        
                    case "--intercept":
                    case "--enhanced":
                        keyboardInterceptorActive = true;
                        break;
                        
                    case "--no-intercept":
                        keyboardInterceptorActive = false;
                        break;
                        
                    default:
                        if (args[i].StartsWith("-"))
                        {
                            Console.WriteLine($"Unknown option: {args[i]}");
                            Console.WriteLine("Use --help for usage information.");
                            shouldExitAfterArgs = true;
                        }
                        break;
                }
            }
            
            return shouldExitAfterArgs;
        }
        
        /// <summary>
        /// Display help information
        /// </summary>
        private static void ShowHelp()
        {
            Console.WriteLine($"{APP_NAME} v{VERSION}");
            Console.WriteLine();
            Console.WriteLine("Usage: show_time [OPTIONS]");
            Console.WriteLine();
            Console.WriteLine("Display system resource information with enhanced visual effects.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  --help, -h          Display this help message");
            Console.WriteLine("  --version, -v       Display version information");
            Console.WriteLine("  --enhanced          Enable enhanced mode with keyboard interceptor");
            Console.WriteLine("  --intercept         Same as --enhanced");
            Console.WriteLine("  --no-intercept      Disable keyboard interceptor (for remote sessions)");
            Console.WriteLine();
            Console.WriteLine("Exit Controls:");
            Console.WriteLine("  ESC                 Exit normally (when interceptor is disabled)");
            Console.WriteLine("  Ctrl+C              Exit with kill switch animation");
            Console.WriteLine("  Ctrl+Shift+X        Force exit (when interceptor is enabled)");
            Console.WriteLine();
            Console.WriteLine("For more information, see: man show_time");
        }
        
        /// <summary>
        /// Display version information
        /// </summary>
        private static void ShowVersion()
        {
            Console.WriteLine($"{APP_NAME} version {VERSION}");
            Console.WriteLine("Copyright (c) 2024 System Monitor Team");
            Console.WriteLine("License: MIT");
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
        
        private static void InitializeEffectMap()
        {
            effectMap = new Dictionary<string, Func<CancellationToken, Task>>
            {
                ["hacker_typing"] = async (token) => {
                    var phrase = TextResources.HackerPhrases[random.Next(TextResources.HackerPhrases.Length)];
                    TerminalRenderer.PrintWithTypingEffect(phrase, random.Next(30, 80), ConsoleColor.Green);
                    await Task.Delay(500, token);
                },
                ["scroll_text"] = async (token) => {
                    await TextScroller.ScrollTextAsync(TextResources.RandomCodeFragment(), ConsoleColor.Cyan, token);
                },
                ["code_fragment"] = async (token) => {
                    var fragment = RandomTextGenerator.GenerateCodeFragment();
                    TerminalRenderer.PrintWithColor(fragment, ConsoleColor.Yellow);
                    await Task.Delay(300, token);
                },
                ["mini_matrix"] = async (token) => {
                    TextScroller.MatrixEffect(random.Next(300, 800), token);
                },
                ["loading_bar"] = async (token) => {
                    var loadingText = TextResources.LoadingMessages[random.Next(TextResources.LoadingMessages.Length)];
                    await LoadingBar.ShowIncrementalAsync(loadingText, random.Next(500, 1200), token);
                },
                ["random_error"] = async (token) => {
                    if (random.Next(100) < 20) // 20% chance
                    {
                        await ErrorDisplay.ShowRandomErrorAsync(token);
                    }
                },
                ["glitch_banner"] = async (token) => {
                    var banners = new[] { "ACCESSING MAINFRAME", "BYPASSING FIREWALL", "CRACKING ENCRYPTION", "SYSTEM INFILTRATION" };
                    TextScroller.GlitchText(banners[random.Next(banners.Length)], random.Next(2, 5));
                    await Task.Delay(300, token);
                },
                ["matrix_rain_with_art"] = async (token) => {
                    await TextScroller.MatrixRainWithAsciiArtAsync(AsciiArt.SmallSkull, ConsoleColor.Green, 1500, token);
                },
                ["security_banner"] = async (token) => {
                    await AsciiAnimator.ScanEffectAsync(AsciiArt.SecurityBanner, ConsoleColor.Red, token);
                },
                ["terminal_prompt"] = async (token) => {
                    TerminalRenderer.SimulateTerminalPrompt();
                    await Task.Delay(200, token);
                },
                ["jackpot_attack"] = async (token) => {
                    TerminalRenderer.PrintBanner("JACKPOT PROTOCOL INITIATED", ConsoleColor.Yellow);
                    await LoadingBar.ShowPulseLoadingBarAsync("Extracting data...", 1, token);
                },
                ["system_defense"] = async (token) => {
                    ErrorDisplay.ShowWarning("INTRUSION DETECTED - COUNTERMEASURES ACTIVE");
                    await Task.Delay(500, token);
                    TerminalRenderer.SimulateStaticNoise(200);
                },
                ["skull_animation"] = async (token) => {
                    await AsciiAnimator.PlayAnimationAsync("skull", ConsoleColor.Green, 150, 1, token);
                },
                ["user_joke"] = async (token) => {
                    var jokes = new[] { 
                        "rm -rf --no-preserve-root /",
                        ":(){ :|:& };:",
                        "sudo make me a sandwich",
                        "cat /dev/random > /dev/audio"
                    };
                    TerminalRenderer.PrintWithTypingEffect($"root@target:~# {jokes[random.Next(jokes.Length)]}", 50, ConsoleColor.Red);
                    await Task.Delay(300, token);
                }
            };
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
        
        private static EffectConfig LoadEffectConfig(string path)
        {
            if (!File.Exists(path))
            {
                // Fallback: default config
                return new EffectConfig
                {
                    Effects = new List<EffectItem>
                    {
                        new EffectItem { Name = "hacker_typing", Duration = 1000 },
                        new EffectItem { Name = "scroll_text", Duration = 500 },
                        new EffectItem { Name = "code_fragment", Duration = 700 },
                        new EffectItem { Name = "mini_matrix", Duration = 500 },
                        new EffectItem { Name = "loading_bar", Duration = 800 },
                        new EffectItem { Name = "random_error", Duration = 600 },
                        new EffectItem { Name = "glitch_banner", Duration = 1500 },
                        new EffectItem { Name = "matrix_rain_with_art", Duration = 2000 },
                        new EffectItem { Name = "security_banner", Duration = 1000 },
                        new EffectItem { Name = "terminal_prompt", Duration = 700 },
                    }
                };
            }
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            using var reader = new StreamReader(path);
            return deserializer.Deserialize<EffectConfig>(reader);
        }
        
        private static async Task RunMainLoop()
        {
            int counter = 0;
            var effects = loadedConfig?.Effects ?? new List<EffectItem>();
            while (!shouldExit)
            {
                // Exit on ESC key press (if keyboard interceptor is not active)
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyboardInterceptorActive && Utils.KeyboardInterceptor.ShouldInterceptKey(keyInfo))
                        continue;
                    if (keyInfo.Key == ConsoleKey.Escape)
                        break;
                }
                if (cancellationTokenSource.Token.IsCancellationRequested)
                    break;
                if (effects.Count == 0)
                {
                    await Task.Delay(200, cancellationTokenSource.Token);
                    continue;
                }
                var effect = effects[counter % effects.Count];
                if (effectMap.TryGetValue(effect.Name, out var action))
                {
                    await action(cancellationTokenSource.Token);
                    await Task.Delay(effect.Duration, cancellationTokenSource.Token);
                }
                else
                {
                    // Unknown effect, skip
                    await Task.Delay(200, cancellationTokenSource.Token);
                }
                counter++;
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