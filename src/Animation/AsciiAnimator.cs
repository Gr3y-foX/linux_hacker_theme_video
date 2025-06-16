using System;
using System.Threading;
using System.Threading.Tasks;
using hacker_terminal.Assets;
using hacker_terminal.UI;
using System.Collections.Generic;

namespace hacker_terminal.Animation
{
    public static class AsciiAnimator
    {
        private static Random random = new Random();
        
        /// <summary>
        /// Displays a simple animation by iterating through frames
        /// </summary>
        /// <param name="animationName">Name of the animation to display</param>
        /// <param name="color">Color to display the animation in</param>
        /// <param name="frameDelay">Delay between frames in milliseconds</param>
        /// <param name="cycles">Number of cycles to repeat the animation</param>
        /// <param name="cancellationToken">Token to allow cancelling the animation</param>
        public static async Task PlayAnimationAsync(
            string animationName,
            ConsoleColor color = ConsoleColor.Green,
            int frameDelay = 200,
            int cycles = 1,
            CancellationToken cancellationToken = default)
        {
            try
            {
                int frameCount = AsciiArt.GetAnimationFrameCount(animationName);
                
                for (int cycle = 0; cycle < cycles; cycle++)
                {
                    if (cancellationToken.IsCancellationRequested) break;
                    
                    for (int frame = 0; frame < frameCount; frame++)
                    {
                        if (cancellationToken.IsCancellationRequested) break;
                        
                        // Clear previous frame (same position)
                        Console.Clear();
                        
                        // Display current frame
                        string frameContent = AsciiArt.GetAnimationFrame(animationName, frame);
                        TerminalRenderer.PrintWithColor(frameContent, color);
                        
                        // Delay between frames
                        await Task.Delay(frameDelay, cancellationToken);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Animation was cancelled, cleanup
                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// Displays a glitching effect with ASCII art
        /// </summary>
        /// <param name="artContent">ASCII art content to display</param>
        /// <param name="duration">Total duration of the effect in milliseconds</param>
        /// <param name="cancellationToken">Token to allow cancellation</param>
        public static async Task GlitchAsciiArtAsync(
            string artContent,
            int duration = 2000,
            CancellationToken cancellationToken = default)
        {
            try
            {
                int iterations = duration / 100; // Number of glitch iterations
                ConsoleColor[] glitchColors = new ConsoleColor[]
                {
                    ConsoleColor.Green,
                    ConsoleColor.Red,
                    ConsoleColor.Cyan,
                    ConsoleColor.White,
                    ConsoleColor.Yellow,
                    ConsoleColor.DarkCyan
                };
                
                Console.Clear();
                
                for (int i = 0; i < iterations; i++)
                {
                    if (cancellationToken.IsCancellationRequested) break;
                    
                    // Pick a random color for this iteration
                    ConsoleColor color = glitchColors[random.Next(glitchColors.Length)];
                    
                    // Sometimes use the correct art, sometimes use random art
                    string displayArt = (random.Next(10) < 7) ? artContent : AsciiArt.GetRandomArt();
                    
                    // Clear and display
                    Console.Clear();
                    TerminalRenderer.PrintWithColor(displayArt, color);
                    
                    // Random delay between frames to enhance glitching effect
                    await Task.Delay(random.Next(50, 150), cancellationToken);
                }
                
                // End on the correct art in the primary color
                Console.Clear();
                TerminalRenderer.PrintWithColor(artContent, ColorScheme.PrimaryColor);
            }
            catch (OperationCanceledException)
            {
                // Effect was cancelled, cleanup
                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// Displays randomized banner for a dramatic effect
        /// </summary>
        /// <param name="finalBanner">Final banner to display</param>
        /// <param name="cycleCount">Number of randomization cycles</param>
        /// <param name="cancellationToken">Token to allow cancellation</param>
        public static async Task RandomizeThenShowBannerAsync(
            string finalBanner,
            int cycleCount = 10,
            CancellationToken cancellationToken = default)
        {
            try
            {
                for (int i = 0; i < cycleCount; i++)
                {
                    if (cancellationToken.IsCancellationRequested) break;
                    
                    Console.Clear();
                    
                    // Show random banner with decreasing frequency as we approach the end
                    string banner = (i < cycleCount - 3) ? AsciiArt.GetRandomBanner() : finalBanner;
                    ConsoleColor color = (i % 2 == 0) ? ColorScheme.HighlightColor : ColorScheme.PrimaryColor;
                    
                    TerminalRenderer.PrintWithColor(banner, color);
                    
                    // Decrease delay as we approach the final banner
                    int delay = 200 - (i * 15);
                    if (delay < 50) delay = 50;
                    
                    await Task.Delay(delay, cancellationToken);
                }
                
                // Final banner with highlight color
                Console.Clear();
                TerminalRenderer.PrintWithColor(finalBanner, ColorScheme.HighlightColor);
            }
            catch (OperationCanceledException)
            {
                // Animation was cancelled, cleanup
                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// Creates a digital rain effect with a focus on ASCII art in the middle
        /// </summary>
        /// <param name="centerArt">ASCII art to display in the center</param>
        /// <param name="duration">Duration in milliseconds</param>
        /// <param name="cancellationToken">Token to allow cancellation</param>
        public static async Task MatrixRainWithArtAsync(
            string centerArt,
            int duration = 3000,
            CancellationToken cancellationToken = default)
        {
            try
            {
                // Setup matrix columns
                int width = Console.WindowWidth;
                int height = Console.WindowHeight;
                
                // Create arrays for column tracking
                int[] drops = new int[width];
                for (int i = 0; i < width; i++)
                {
                    drops[i] = random.Next(-20, 0); // Start above screen at random positions
                }
                
                // Split the center art into lines
                string[] artLines = centerArt.Split(
                    new[] { "\r\n", "\n" }, 
                    StringSplitOptions.None
                );
                
                // Calculate center position for the art
                int centerY = height / 2 - artLines.Length / 2;
                int centerX = width / 2 - (artLines.Length > 0 ? artLines[0].Length / 2 : 0);
                
                // Create a mask for where the art will be displayed
                bool[,] artMask = new bool[width, height];
                
                // Mark where the art should be
                for (int y = 0; y < artLines.Length; y++)
                {
                    if (centerY + y >= 0 && centerY + y < height)
                    {
                        string line = artLines[y];
                        for (int x = 0; x < line.Length; x++)
                        {
                            if (centerX + x >= 0 && centerX + x < width && line[x] != ' ')
                            {
                                artMask[centerX + x, centerY + y] = true;
                            }
                        }
                    }
                }
                
                // Run the matrix effect
                DateTime startTime = DateTime.Now;
                
                // Characters to use for the matrix effect
                char[] matrixChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789$%#@!*&^".ToCharArray();
                
                while ((DateTime.Now - startTime).TotalMilliseconds < duration)
                {
                    if (cancellationToken.IsCancellationRequested) break;
                    
                    Console.Clear();
                    
                    // Draw the matrix rain
                    for (int i = 0; i < width; i++)
                    {
                        // Update drop position
                        if (random.Next(100) > 90) drops[i]++;
                        
                        // Draw drops
                        int dropPosition = drops[i];
                        if (dropPosition >= 0 && dropPosition < height)
                        {
                            // Don't draw over art mask positions
                            if (!artMask[i, dropPosition])
                            {
                                Console.SetCursorPosition(i, dropPosition);
                                Console.ForegroundColor = ColorScheme.MatrixHeadColor;
                                Console.Write(matrixChars[random.Next(matrixChars.Length)]);
                            }
                            
                            // Draw trailing characters with fading color
                            for (int j = 1; j < 8; j++)
                            {
                                int trailPos = dropPosition - j;
                                if (trailPos >= 0 && trailPos < height)
                                {
                                    // Skip positions that are part of the art
                                    if (!artMask[i, trailPos])
                                    {
                                        Console.SetCursorPosition(i, trailPos);
                                        
                                        // Fade from bright to dark
                                        Console.ForegroundColor = j < 3 ? 
                                            ColorScheme.PrimaryColor : 
                                            ColorScheme.SecondaryColor;
                                        
                                        Console.Write(matrixChars[random.Next(matrixChars.Length)]);
                                    }
                                }
                            }
                        }
                        
                        // Reset drops that go offscreen
                        if (dropPosition > height + 10 || random.Next(100) > 99)
                        {
                            drops[i] = random.Next(-20, 0);
                        }
                    }
                    
                    // Draw the center art
                    for (int y = 0; y < artLines.Length; y++)
                    {
                        if (centerY + y >= 0 && centerY + y < height)
                        {
                            Console.SetCursorPosition(centerX, centerY + y);
                            Console.ForegroundColor = ColorScheme.HighlightColor;
                            Console.Write(artLines[y]);
                        }
                    }
                    
                    await Task.Delay(50, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                // Animation was cancelled, cleanup
                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// Creates a scanning effect over an ASCII art
        /// </summary>
        /// <param name="art">ASCII art to scan</param>
        /// <param name="scanColor">Color of the scanning line</param>
        /// <param name="cancellationToken">Token to allow cancellation</param>
        public static async Task ScanEffectAsync(
            string art,
            ConsoleColor scanColor = ConsoleColor.Red,
            CancellationToken cancellationToken = default)
        {
            try
            {
                string[] lines = art.Split('\n');
                int maxLength = 0;
                
                // Find the max length of a line in the art
                foreach (string line in lines)
                {
                    if (line.Length > maxLength) maxLength = line.Length;
                }
                
                // Calculate starting positions
                int startY = Console.CursorTop;
                
                // Display the art initially
                Console.ForegroundColor = ColorScheme.PrimaryColor;
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
                
                // Return cursor to starting position
                Console.SetCursorPosition(0, startY);
                
                // Perform up to down scan
                for (int scan = 0; scan < lines.Length; scan++)
                {
                    if (cancellationToken.IsCancellationRequested) break;
                    
                    // Display scan line
                    Console.SetCursorPosition(0, startY + scan);
                    Console.ForegroundColor = scanColor;
                    Console.Write(new string('█', maxLength));
                    
                    await Task.Delay(100, cancellationToken);
                    
                    // Restore original line
                    Console.SetCursorPosition(0, startY + scan);
                    Console.ForegroundColor = ColorScheme.PrimaryColor;
                    Console.Write(lines[scan]);
                }
                
                // Perform left to right scan
                for (int x = 0; x < maxLength; x++)
                {
                    if (cancellationToken.IsCancellationRequested) break;
                    
                    for (int y = 0; y < lines.Length; y++)
                    {
                        if (y < lines.Length && x < lines[y].Length)
                        {
                            Console.SetCursorPosition(x, startY + y);
                            Console.ForegroundColor = scanColor;
                            Console.Write('█');
                        }
                    }
                    
                    await Task.Delay(50, cancellationToken);
                    
                    // Restore original characters
                    for (int y = 0; y < lines.Length; y++)
                    {
                        if (y < lines.Length && x < lines[y].Length)
                        {
                            Console.SetCursorPosition(x, startY + y);
                            Console.ForegroundColor = ColorScheme.PrimaryColor;
                            Console.Write(lines[y][x]);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Animation was cancelled
                Console.WriteLine();
            }
        }
    }
}
