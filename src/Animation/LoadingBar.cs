using System;
using System.Threading;
using System.Threading.Tasks;

namespace hacker_terminal.Animation
{
    public class LoadingBar
    {
        private const int DefaultBarLength = 40;
        private const char DefaultProgressChar = '█';
        private const char DefaultEmptyChar = '░';
        private static readonly Random random = new Random();

        public static async Task ShowAsync(string message, int percentage, ConsoleColor color = ConsoleColor.Green, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"\n{message}");
            Console.ForegroundColor = color;
            Console.Write("[");
            
            int filledLength = (int)(DefaultBarLength * percentage / 100);
            
            for (int i = 0; i < DefaultBarLength; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;
                    
                Console.Write(i < filledLength ? DefaultProgressChar : DefaultEmptyChar);
                await Task.Delay(10, cancellationToken); // Animate the loading bar
            }
            
            Console.Write($"] {percentage}%");
            Console.ResetColor();
            Console.WriteLine();
        }
        
        public static async Task ShowIncrementalAsync(string message, int durationMs = 2000, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"\n{message}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[");
            
            int steps = DefaultBarLength;
            int delayPerStep = durationMs / steps;
            
            for (int i = 0; i < DefaultBarLength; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;
                    
                Console.Write(DefaultProgressChar);
                await Task.Delay(delayPerStep, cancellationToken);
                
                // Calculate percentage based on steps completed
                int percentage = (int)((i + 1) / (float)DefaultBarLength * 100);
                
                // Update progress percentage
                Console.Write($"\r{message}\n[");
                for (int j = 0; j <= i; j++) Console.Write(DefaultProgressChar);
                for (int j = i + 1; j < DefaultBarLength; j++) Console.Write(DefaultEmptyChar);
                Console.Write($"] {percentage}% ");
            }
            
            Console.ResetColor();
            Console.WriteLine();
        }
        
        public static async Task ShowPulseLoadingBarAsync(string message, int pulseCount = 3, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"\n{message}");
            
            for (int pulse = 0; pulse < pulseCount; pulse++)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;
                
                // Forward animation
                for (int i = 0; i <= DefaultBarLength; i++)
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;
                        
                    Console.ForegroundColor = GetPulseColor(i, DefaultBarLength);
                    Console.Write("\r[");
                    
                    for (int j = 0; j < DefaultBarLength; j++)
                    {
                        if (j == i)
                            Console.Write("O"); // Pulse character
                        else
                            Console.Write("·"); // Track character
                    }
                    
                    Console.Write("]");
                    await Task.Delay(30, cancellationToken);
                }
                
                // Backward animation
                for (int i = DefaultBarLength; i >= 0; i--)
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;
                        
                    Console.ForegroundColor = GetPulseColor(i, DefaultBarLength);
                    Console.Write("\r[");
                    
                    for (int j = 0; j < DefaultBarLength; j++)
                    {
                        if (j == i)
                            Console.Write("O"); // Pulse character
                        else
                            Console.Write("·"); // Track character
                    }
                    
                    Console.Write("]");
                    await Task.Delay(30, cancellationToken);
                }
            }
            
            Console.ResetColor();
            Console.WriteLine();
        }
        
        // Helper method to get a color based on position for pulse effect
        private static ConsoleColor GetPulseColor(int position, int maxPosition)
        {
            var colors = new[] { 
                ConsoleColor.Red, 
                ConsoleColor.Yellow, 
                ConsoleColor.Green, 
                ConsoleColor.Cyan, 
                ConsoleColor.Blue, 
                ConsoleColor.Magenta
            };
            
            return colors[(position / (maxPosition / colors.Length)) % colors.Length];
        }
    }
}