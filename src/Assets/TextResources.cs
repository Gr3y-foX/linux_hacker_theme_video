// filepath: /Users/phenix/Projects/hacker_theme_linux/hacker-terminal/src/Assets/TextResources.cs
using System;
using System.Collections.Generic;

namespace hacker_terminal.Assets
{
    public static class TextResources
    {
        public const string WelcomeMessage = "WELCOME TO THE MATRIX";
        public const string ErrorMessage = "ERROR: SYSTEM BREACH DETECTED";
        public const string LoadingMessage = "INITIALIZING SYSTEM HACK...";
        public const string ExitMessage = "CONNECTION TERMINATED";
        
        public static readonly string[] HackerPhrases = new string[]
        {
            "Bypassing security protocols...",
            "Cracking encryption keys...",
            "Accessing mainframe...",
            "Injecting exploit payload...",
            "Compromising system integrity...",
            "Extracting sensitive data...",
            "Disabling security countermeasures...",
            "Establishing backdoor access...",
            "Escalating privileges...",
            "Installing keylogger...",
            "Brute forcing admin credentials...",
            "Deploying ransomware payload...",
            "Scanning for vulnerabilities...",
            "SSH connection established...",
            "Exploiting zero-day vulnerability...",
            "Buffer overflow attack initiated...",
            "Data exfiltration in progress...",
            "Covering tracks...",
            "Erasing system logs...",
            "Installing rootkit...",
            "Deploying persistent threat actor..."
        };
        
        public static readonly string[] ErrorMessages = new string[]
        {
            "ERROR: Access denied",
            "WARNING: Firewall detected",
            "ALERT: Intrusion detection triggered",
            "CRITICAL: Connection compromised",
            "ERROR: Buffer overflow",
            "WARNING: Memory corruption",
            "FATAL: System crash imminent",
            "ERROR: Stack trace overflow",
            "ALERT: Runtime exception",
            "CRITICAL: Kernel panic"
        };

        public static string GetRandomHackerPhrase(Random random)
        {
            return HackerPhrases[random.Next(HackerPhrases.Length)];
        }

        public static string GetRandomErrorMessage(Random random)
        {
            return ErrorMessages[random.Next(ErrorMessages.Length)];
        }
    }
}
