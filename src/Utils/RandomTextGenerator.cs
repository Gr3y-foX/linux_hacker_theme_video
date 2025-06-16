using System;
using System.Collections.Generic;
using System.Text;

namespace hacker_terminal.Utils
{
    public static class RandomTextGenerator
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
            "Error: Unauthorized Access",
            "Bypassing Firewall",
            "Injecting Payload",
            "Exploiting Vulnerability",
            "Extracting Data",
            "Securing Connection",
            "Routing Through Proxy",
            "Brute Force Attack",
            "Malware Deployed",
            "Backdoor Installed",
            "Covering Tracks",
            "Exfiltrating Data",
            "Monitoring Target",
            "Keylogger Active",
            "Exploiting Zero-Day",
            "Cracking Password Hash",
            "DDoS Attack Launched",
            "Rootkit Installed",
            "Remote Access Granted",
            "Lateral Movement Enabled",
            "Privilege Escalation Successful",
            "Data Breach Complete",
            "Persistence Established"
        };
        
        private static readonly List<string> ipAddresses = new List<string>
        {
            "192.168.1.1",
            "10.0.0.1",
            "172.16.0.1",
            "8.8.8.8",
            "1.1.1.1",
            "127.0.0.1",
            "192.30.255.112",
            "104.244.42.65",
            "185.199.108.153",
            "140.82.121.4",
            "151.101.1.140",
            "31.13.72.36"
        };
        
        private static readonly List<string> hexCodes = new List<string>
        {
            "0xDEADBEEF",
            "0xC0FFEE",
            "0xBAADF00D",
            "0x8BADF00D",
            "0xFEEDFACE",
            "0xCAFEBABE",
            "0x1BADB002",
            "0xFACADE",
            "0xF00DBABE",
            "0xBADDCAFE"
        };
        
        private static readonly Random random = new Random();
        
        public static string GetRandomText()
        {
            return phrases[random.Next(phrases.Count)];
        }
        
        public static string GetRandomIP()
        {
            return ipAddresses[random.Next(ipAddresses.Count)];
        }
        
        public static string GetRandomHex()
        {
            return hexCodes[random.Next(hexCodes.Count)];
        }
        
        public static string GenerateRandomBinaryString(int length = 16)
        {
            StringBuilder sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                sb.Append(random.Next(2));
            }
            return sb.ToString();
        }
        
        public static string GenerateRandomHexString(int length = 16)
        {
            StringBuilder sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                sb.Append("0123456789ABCDEF"[random.Next(16)]);
            }
            return "0x" + sb.ToString();
        }
        
        public static string GenerateLogEntry()
        {
            string[] timestampFormats = { 
                "yyyy-MM-dd HH:mm:ss", 
                "MM/dd/yyyy HH:mm:ss",
                "dd-MM-yyyy HH:mm:ss" 
            };
            
            string timestamp = DateTime.Now.ToString(timestampFormats[random.Next(timestampFormats.Length)]);
            
            string[] logTypes = {
                "INFO", "WARNING", "ERROR", "DEBUG", "CRITICAL", "ALERT", "NOTICE"
            };
            
            string logType = logTypes[random.Next(logTypes.Length)];
            string message = GetRandomText();
            string ip = GetRandomIP();
            
            return $"[{timestamp}] [{logType}] {message} from {ip}";
        }
        
        public static string GenerateHashString()
        {
            string[] hashTypes = { "MD5", "SHA1", "SHA256", "SHA512" };
            string hashType = hashTypes[random.Next(hashTypes.Length)];
            
            StringBuilder sb = new StringBuilder();
            int length = hashType == "MD5" ? 32 : hashType == "SHA1" ? 40 : hashType == "SHA256" ? 64 : 128;
            
            for (int i = 0; i < length; i++)
            {
                sb.Append("0123456789abcdef"[random.Next(16)]);
            }
            
            return $"{hashType}: {sb.ToString()}";
        }
    }
}