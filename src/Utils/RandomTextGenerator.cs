using System;
using System.Collections.Generic;

namespace HackerTerminal.Utils
{
    public class RandomTextGenerator
    {
        private static readonly List<string> phrases = new List<string>
        {
            "Access Granted",
            "Access Denied",
            "Hacking in Progress...",
            "Loading...",
            "System Breach Detected!",
            "Welcome to the Terminal",
            "Encrypting Data...",
            "Decrypting Files...",
            "Connection Established",
            "Error: Unauthorized Access"
        };

        private static Random random = new Random();

        public static string GenerateRandomText()
        {
            int index = random.Next(phrases.Count);
            return phrases[index];
        }

        public static void GenerateTextLoop(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(GenerateRandomText());
                System.Threading.Thread.Sleep(1000); // Wait for 1 second before generating the next text
            }
        }
    }
}