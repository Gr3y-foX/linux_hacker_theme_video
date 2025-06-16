// filepath: /Users/phenix/Projects/hacker_theme_linux/hacker-terminal/src/Assets/AsciiArt.cs
using System;
using System.Collections.Generic;

namespace hacker_terminal.Assets
{
    public static class AsciiArt
    {
        private static Random random = new Random();
        
        // Original skull design
        public static string Skull => @"
                 .ed"""" """"$$$$be.
              -""           ^""""**$$$e.
            .""                   '$$$c
           /                      ""4$$b
          d  3                     $$$$
          $  *                   .$$$$$$
         .$  ^c           $$$$$e$$$$$$$$.
         d$L  4.         4$$$$$$$$$$$$$$b
         $$$$b ^ceeeee.  4$$ECL.F*$$$$$$$
e$""""=.      $$$$P d$$$$F $ $$$$$$$$$- $$$$$$
z$$b. ^c     3$$$F ""$$$$b   $""$$$$$c  $$$$*""
4$$$$L   \   $$P""  ""$$b   .$ $$$$$...e$$
^*$$$$$c  %..   *c    ..    $$ 3$$$$$$$$$$
  ""**$$$ec   ""   %ce""""    $$$  $$$$$$$$P""
        ""*$b.  ""c  *$e.    *** d$$$$$""
          ^*$$c ^$c $$$      4J$$$$$""
             ""$$$$$$""'$=e....$*$$**""
               ""*$$$  *=%4.$ L$P""
                  ""$   ""%*ebJLzb""
                    %..      4$$$ec
                     $$$e   z$$$$$$""
        ";

        // Animation frames for skull
        public static string SkullFrame1 => @"
                 _,.-------.,_
             ,;~'             '~;,
           ,;        _          ;,
          ;        (   )          ;
         ,'        (_ )           ',
        ,;           `             ;,
        ; ;      .           .      ; ;
        | ;   ______       ______   ; |
        |  `/~""    ~"" . ""~    ""~\'  |
        |  ~ ,-~~~^~, | ,~^~~~-, ~  |
         |  /|       }:{       |\  |
         |   l       / | \       !   |
         .~  (__,.--"" .^. ""--.,__) ~.
         |    ---;' / | \ `;---     |
          \__.       \/^\/       .__/
           V| \                 / |V
            | |T~\___!___!___/~T| |
            | |`IIII_I_I_I_IIII'| |
            |  \,III I I I III,/  |
             \   `~~~~~~~~~~'    /
               \   .       .   /
                 \.    ^    ./
                   ^~~~^~~~^
        ";

        public static string SkullFrame2 => @"
                 _,.-------.,_
             ,;~'             '~;,
           ,;       \     /      ;,
          ;                        ;
         ,'                        ',
        ,;                          ;,
        ; ;      .           .      ; ;
        | ;   _______ _ _______   ; |
        |  `/~""       V      ""~\'  |
        |  ~  ,-~~~^~, | ,~^~~~-, ~  |
         |   |        }:{        |   |
         |   l       / | \       !   |
         .~  (__,.--"" .^. ""--.,__) ~.
         |     ---;' / | \ `;---     |
          \__.       \/^\/       .__/
           V| \                 / |V
            | |T~\___!___!___/~T| |
            | |`IIII_I_I_I_IIII'| |
            |  \,III I I I III,/  |
             \   `~~~~~~~~~~'    /
               \   .       .   /
                 \.    ^    ./
                   ^~~~^~~~^
        ";
        
        public static string SkullFrame3 => @"
                 _,.-------.,_
             ,;~'   .-.   .-. '~;,
           ,;      (   ) (   )    ;,
          ;         `-'   `-'       ;
         ,'                         ',
        ,;         .-""""-.,          ;,
        ; ;      .           .      ; ;
        | ;   ______  ___  ______   ; |
        |  `/~""     ~"" . ""~     ""~\'  |
        |  ~  ,-~~~^~, | ,~^~~~-,  ~  |
         |   |  (◕)  }:{  (◕)  |   |
         |   l       / | \       !  |
         .~  (__,.--"" .^. ""--.,__) ~.
         |     ---;' / | \ `;---     |
          \__.       \/^\/       .__/
           V| \   *           * / |V
            | |T~\___!___!___/~T| |
            | |`IIII_I_I_I_IIII'| |
            |  \,III I I I III,/  |
             \   `~~~~~~~~~~'    /
               \    \___/     /
                 \.  \_/  ./
                   \~~ ~~/
        ";

        // New skull designs from ascii.co.uk
        public static string SkullSimple => @"
     .-""``""-.
    /        \
   |  X    X  |
   |  O_/\_O  |
   /          \
  /            \
 /|   ______   |\
| |  |______|  | |
| |      |     | |
| |   ___|___  | |
 \|   \__/\__/ |/
  \         __/
   \_______/
        ";

        public static string SkullBones => @"
              ___
             /   \
            /     \
           |       |
           |  O O  |
           |   ∧   |
           |  \_/  |
            \     /
             \___/
         _____|||_____
        /             \
       /               \
      /                 \
     |    |||     |||    |
     |                   |
     |                   |
      \                 /
       \_______________/
          |         |
          |         |
          |         |
         / \       / \
        /   \     /   \
       /     \   /     \
      /       \ /       \
     /_________X_________\
        ";

        public static string SkullDetailed => @"
                        ,---.
                       /    |
                      /     |
                     /      |
                    /       |
                 __|        |__
                /  \        /  \
               |    \      /    |
               |     \____/     |
               |  \/\/    \/\/  |
               |  (o)      (o)  |
               |      /\        |
                \     \/       /
                 \            /
                  \          /
                   `--------'
                 /-|      |-\
                /  |      |  \
               /   |      |   \
              /   /        \   \
             /   /          \   \
            |___/            \___|
        ";

        public static string SkullCrossed => @"
                     ______
                  .-""      ""-.
                 /            \
                |              |
                |,  .-.  .-.  ,|
                | )(__/  \__)( |
                |/     /\     \|
      (@_       (_     ^^     _)
 _     ) \_______\__|IIIIII|__/__________________________
(_)@8@8{}<________|-\IIIIII/-|___________________________>
       )_/        \          /
      (@           `--------`
        ";

        public static string SkullFlaming => @"
             (    )
            ((((()))
            |o\ /o)|
            ( (  _')
             (._.  /\__
            ,\___,/ '  ')
          '.,__.'    /
             )       )
            /       /
           /       /
          /       /
         /       /
        /       /
       /       /
      /       /
     /       /
    /       /
   /       /
  /       /
  \      /
   \    /
    \  /
     \/
        ";

        public static string SkullDanger => @"
                      _.--""""""--._
                    .'            '.
                   /                \
                  ;                  ;
                  |                  |
                  |                  |
                  ;                  ;
                   \                /
                    '.            .'
                      '--......--'
                 .::::::::::::::::.
                .::::::::::::::::::.
               :::::::' .:::::::::::::
              :::::::.   .::::::::::::
             ::::::::     ::::::::::::
            :::::::::     ::::::::::::
           ::::::::::     ::::::::::::
           :::::::::::.   ::::::::::::
           ::::::::::::   ::::::::::::
           :::::::::::::.:::::::::::::
           :::::::::::::::::::::::::::
           :::::::::::::::::::::::::::
        ";
        
        // Adding missing skull designs
        public static string SkullTribal => @"
                 .x88888x.
                :8**888888X.
               :8°   `88888X
              :8°      ?8888>
              X88h.     8888>
              '8888     8888>
               `888     8888>
                 `*     8888>
                /~     (8888
               :8:     :8888
              :888:   :88888
             '<8888.  :88888
             8:8888 .:88888:
            '88 `8  `:88888:
            888  ~  '888888:
            `~      :888888:
                      88888X
                      88888X
                      88888X
                      88888X
        ";
        
        public static string SkullCreepy => @"
                 .-""""""""-.
                /          \
               |            |
               |  X      X  |
               |    /\/\    |
               |            |
               |    ____    |
                \          /
                 `.______.'
                 
          .------------------.
         /  (oo)      (oo)   \
        |      |      |       |
        |      V      V       |
        |                     |
         \     wwwwwww      /
          '---------------'
        ";
        
        public static string SkullPixelated => @"
           ██████████████
         ██              ██
       ██                  ██
     ██                      ██
   ██      ██        ██        ██
 ██        ███      ███        ██
 ██        ███      ███        ██
 ██        ███      ███        ██
 ██        ███████████        ██
 ██                          ██
   ██      ██      ██      ██
     ██    ████████    ██
       ██              ██
         ██          ██
           ██████████
        ";
        
        public static string SkullHexed => @"
             ,;;;;;;;;,
          ,;;;;;;;;;;;;,
        ,;;;;;;;;;;;;;;;;,
       ;;;;  ;;;;;;  ;;;;
      ;;;;;  ;;;;;;  ;;;;;
     ;;;;;;  ;;;;;;  ;;;;;;
     ;;;;;;  ;;;;;;  ;;;;;;
     ;;;;;;  ;;;;;;  ;;;;;;
     ;;;;;;&&&&&&&&&&&;;;;;
     ;;;;;;          ;;;;;;
     ;;;;;;          ;;;;;;
      ;;;;;  ;;;;;;  ;;;;;
       ;;;;  ;;;;;   ;;;;
        `;;;  ;;;   ;;;'
          `;;       ;;'
            `;;   ;;'
              `;;;'
                ;
        ";
        
        // Binary-coded skull
        public static string BinaryCodeBanner => @"
 01000100 01100101 01100001 01110100 01101000  
 01101001 01110011  01101001 01101110 01100101 
 01110110 01101001 01110100 01100001 01100010 
 01101100 01100101 00101110 00100000 01010011 
 01101111  01101001 01110011  01110100 01101000 
 01100101  01100100 01100001 01110100 01100001 
        ";
        
        // Other ASCII art
        public static string Computer => @"
         .---------.
        / .-------. \
       / /         \ \
       | |         | |
       | |         | |
       \ \         / /
        \ `-._____.-' /
         `-._______.-'
        ";

        public static string HackingBanner => @"
     _    _            _    
    | |  | |          | |   
    | |__| | __ _  ___| | __
    |  __  |/ _` |/ __| |/ /
    | |  | | (_| | (__|   < 
    |_|  |_|\__,_|\___|_|\_\
                            
        ";

        public static string MatrixBanner => @"
  __  __       _        _      
 |  \/  |     | |      (_)     
 | \  / | __ _| |_ _ __ ___  __
 | |\/| |/ _` | __| '__| \ \/ /
 | |  | | (_| | |_| |  | |>  < 
 |_|  |_|\__,_|\__|_|  |_/_/\_\
                               
        ";

        public static string Lock => @"
       .---.
      /   _  \.
      |  / \  |
      | (___) |
       \      /
        `---'
        ";

        public static string AccessDenied => @"
  _____                            _____             _          _ 
 |  _  |                          |  _  |           (_)        | |
 | | | | ___ ___ ___  ___ ___     | | | | ___ _ __  _  ___  __| |
 | | | |/ __/ __/ _ \/ __/ __|    | | | |/ _ \ '_ \| |/ _ \/ _` |
 | |/ /| (_| (_|  __/\__ \__ \    | |/ /|  __/ | | | |  __/ (_| |
 |___/  \___\___\___||___/___/    |___/ \___|_| |_|_|\___|\__,_|
                                                                 
        ";

        public static string AccessGranted => @"
  _____                            _____                 _           _ 
 |  _  |                          |  __ \               | |         | |
 | | | | ___ ___ ___  ___ ___     | |  \/_ __ __ _ _ __ | |_ ___  __| |
 | | | |/ __/ __/ _ \/ __/ __|    | | __| '__/ _` | '_ \| __/ _ \/ _` |
 | |/ /| (_| (_|  __/\__ \__ \    | |_\ \ | | (_| | | | | ||  __/ (_| |
 |___/  \___\___\___||___/___/     \____/_|  \__,_|_| |_|\__\___|\__,_|
                                                                        
        ";

        public static string SystemFailure => @"
  _____           _                   _____     _ _                
 /  ___|         | |                 |  ___|   (_) |               
 \ `--. _   _ ___| |_ ___ _ __ ___   | |_ __ _ _| |_   _ _ __ ___ 
  `--. \ | | / __| __/ _ \ '_ ` _ \  |  _/ _` | | | | | | '__/ _ \
 /\__/ / |_| \__ \ ||  __/ | | | | | | || (_| | | | |_| | | |  __/
 \____/ \__,_|\___|_|   \___|_| |_| |_| \__,_|_|_|\__,_|_|  \___|
         __/ |                                                     
        |___/                                                      
        ";

        public static string TerminalBanner => @"
 ______                    _             _ 
 |  ___|                  (_)           | |
 | |_ ___ _ __ _ __ ___    _ _ __   __ _| |
 |  _/ _ \ '__| '_ ` _ \  | | '_ \ / _` | |
 | ||  __/ |  | | | | | | | | | | | (_| | |
 \_| \___|_|  |_| |_| |_| |_|_| |_|\__,_|_|
                                           
        ";
        
        public static string WarningBanner => @"
 _    _                     _             
| |  | |                   (_)            
| |  | | __ _ _ __ _ __  _  _ _ __   __ _ 
| |/\| |/ _` | '__| '_ \| || | '_ \ / _` |
\  /\  / (_| | |  | | | | || | | | | (_| |
 \/  \/ \__,_|_|  |_| |_|_|| |_| |_|\__,_|
                          _/ |       __/ |
                         |__/       |___/ 
        ";
        
        public static string IntrustionBanner => @"
 _____       _                     _                 _____        _            _           _ 
|_   _|     | |                   (_)               |  _  |      | |          | |         | |
  | | _ __  | |_ _ __ _   _ ___    _  ___  _ __     | | | |_ __ | |_ ___  ___| |_ ___  __| |
  | || '_ \ | __| '__| | | / __|  | |/ _ \| '_ \    | | | | '_ \| __/ _ \/ __| __/ _ \/ _` |
 _| || | | || |_| |  | |_| \__ \  | | (_) | | | |   \ \_/ / | | | ||  __/ (__| ||  __/ (_| |
 \___/_| |_| \__|_|   \__,_|___/  |_|\___/|_| |_|    \___/|_| |_|\__\___|\___|\__\___|\__,_|
                                                                                             
        ";
        
        public static string SecurityBanner => @"
  _____                      _ _         _____ _                    _     
 /  ___|                    (_) |       |_   _| |                  | |    
 \ `--.  ___  ___ _   _ _ __ _| |_ _   _  | | | |__  _ __ ___  __ _| |_   
  `--. \/ _ \/ __| | | | '__| | __| | | | | | | '_ \| '__/ _ \/ _` | __|  
 /\__/ /  __/ (__| |_| | |  | | |_| |_| | | | | | | | |  __/ (_| | |_   
 \____/ \___|\___|\__,_|_|  |_|\__|\__, | \_/ |_| |_|_|  \___|\__,_|\__|  
                                    __/ |                                 
                                   |___/                                  
        ";

        public static string ExploitBanner => @"
 _____            _       _ _   
|  ___|          | |     (_) |  
| |____  ___ __  | | ___  _| |_ 
|  __\ \/ / '_ \ | |/ _ \| | __|
| |___>  <| |_) || | (_) | | |_ 
\____/_/\_\ .__(_)_|\___/|_|\__|
          | |                   
          |_|                   
        ";

        public static string HackedBanner => @"
 _   _            _            _ 
| | | |          | |          | |
| |_| | __ _  ___| | _____  __| |
|  _  |/ _` |/ __| |/ / _ \/ _` |
| | | | (_| | (__|   <  __/ (_| |
\_| |_/\__,_|\___|_|\_\___|\__,_|
                                 
        ";

        // NEW ADDITIONAL ASCII ART
        public static string AnonymousMask => @"
      .-""""""""-.
     /     __    \
    /     |  |    \
    |     |__|     |
    |      __      |
    \     |  |    /
     \    |__|   /
      `-........-'
       Anonymous
        ";

        public static string ProcessorChip => @"
       _________
      |         |
      | _______ |
      ||       ||
      || CPU   ||
      ||_______||
      |_________|
        ";

        public static string ServerRack => @"
    ┌───────────┐
    │ ▓▓▓▓▓▓▓▓▓ │
    │           │
    │ ▓▓▓▓▓▓▓▓▓ │
    │           │
    │ ▓▓▓▓▓▓▓▓▓ │
    │           │
    │ ▓▓▓▓▓▓▓▓▓ │
    └───────────┘
        ";

        public static string MalwareDetected => @"
  __  __       _                           _____       _            _ 
 |  \/  |     | |                         |  _  |     | |          | |
 | \  / | __ _| |_      ____ _ _ __ ___   | | | |_ __ | |_ ___  ___| |_ ___  __| |
 | |\/| |/ _` | __| '__| \/\ / _` | '__/ _ \  | | | | '_ \| __/ _ \/ __| __/ _ \/ _` |
 | |  | | (_| | || (_| |  \ V  V / (_| | | | |_| | | | | | ||  __/ (__| ||  __/ (_| |
 \_|  |_|\__,_|\__|\__,_|   \_/\_/ \__,_|_|_|\__,_|_| |_|\__\___|\___|\__\___|\__,_|
                                                                                  
        ";

        public static string FirewallBanner => @"
  _______ _                        _ _ 
 |  ___(_) |                      | | |
 | |_   _| |_ _____      ____ _ _ | | |
 |  _| | | __/ _ \ \ /\ / / _` | | | | |
 | |   | | ||  __/\ V  V / (_| | | | | |
 \_|   |_|\__\___| \_/\_/ \__,_|_|_|_|_|
                                        
        ";

        public static string EncryptionBanner => @"
  _____                           _   _             
 |  ___|                         | | (_)            
 | |__ _ __   ___ _ __ _   _ _ __| |_ _  ___  _ __  
 |  __| '_ \ / __| '__| | | | '_ \  _| |/ _ \| '_ \ 
 | |__| | | | (__| |  | |_| | |_) | |_| | (_) | | | |
 \____/_| |_|\___|_|   \__, | .__/ \__|_|\___/|_| |_|
                        __/ | |                      
                       |___/|_|                      
        ";

        public static string RansomwareBanner => @"
  _____                                                          
 |  __ \                                                         
 | |__) |__ _ _ __  ___  ___  _ __ _____      ____ _ _ __ ___   
 |  _  // _` | '_ \/ __|/ _` | '__| _ \ \ /\ / / _` | '__/ _ \  
 | | \ \ (_| | | | \__ \ (_| | |  |  __/\ V  V / (_| | | |  __/_ 
 |_|  \_\__,_|\__|_|___/\__,_|_|  \___| \_/\_/ \__,_|_|  \___(_)
                                                                
        ";

        public static string BitcoinBanner => @"
  ____  _ _            _       
 |  _ \(_) |          (_)      
 | |_) |_| |_ ___ ___  _ _ __  
 |  _ <| | __/ __/ _ \| | '_ \ 
 | |_) | | || (_| (_) | | | | |
 |____/|_|\__\___\___/|_|_| |_|
                               
        ";

        public static string TerminalPrompt => @"
 ┌──(user㉿kali)-[~/hacker]
 └─$ 
        ";

        public static string DataBreachBanner => @"
  _____        _         ____                      _     
 |  __ \      | |       |  _ \                    | |    
 | |  | | __ _| |_ __ _ | |_) |_ __ ___  __ _  ___| |__  
 | |  | |/ _` | __| _` ||  _ <| '__/ _ \/ _` |/ __| '_ \ 
 | |__| | (_| | || (_| || |_) | | |  __/ (_| | (__| | | |
 |_____/ \__,_|\__|\__,_||____/|_|  \___|\__,_|\___|_| |_|
                                                         
        ";

        public static string CybercrimeScene => @"
   _____      _                          _                _____                       
  / ____|    | |                        (_)              / ____|                      
 | |    _   _| |__   ___ _ __ ___ _ __  _ _ __ ___   ___| (___   ___ ___ _ __   ___  
 | |   | | | | '_ \ / _` | '__| | | | '_ \  _| |/ _ \ / _` | '__/ _ \  _` | '__| / _ \ 
 | |___| |_| | |_) |  __/ | | (__| | | | | | | | | |  __/____) | (_|  __/ | | |  __/ 
  \_____\__, |_.__/ \___|_|  \___|_| |_|_|_| |_| |_|\___|_____/ \___\___|_| |_|\___| 
         __/ |                                                                        
        |___/                                                                         
        ";

        public static string NetworkBanner => @"
  _   _      _                      _    
 | \ | |    | |                    | |   
 |  \| | ___| |___      _____  _ __| | __
 | . ` |/ _` | __\ \ /\ / / _` | '__| |/ /
 | |\  |  __/ |_ \ V  V / (_) | |  |   < 
 |_| \_|\___|\__| \_/\_/ \___/|_|  |_|\_\
                                         
        ";
        
        public static string DigitalSignatureBanner => @"
  _____  _       _ _        _    _____  _                 _                    
 |  __ \(_)     (_) |      | |  / ____|(_)               | |                   
 | |  | |_  __ _ _| |_ __ _| | | (___   _  __ _ _ __   __| |_   _ _ __ ___  
 | |  | | |/ _` | | __/ _` | |  \___ \ | |/ _` | '_ \ / _` | | | | '__/ _ \ 
 | |__| | | (_| | | || (_| | |  ____) || | (_| | | | | (_| | |_| | | |  __/
 |_____/|_|\__, |_|\__\__,_|_| |_____/ |_|\__, |_| |_|\__,_|\__,_|_|  \___|
            __/ |                          __/ |                            
           |___/                          |___/                             
        ";
        
        // Returns a random skull frame for animation
        public static string GetRandomSkullFrame()
        {
            int frame = random.Next(10);
            return frame switch
            {
                0 => Skull,
                1 => SkullFrame1,
                2 => SkullFrame2,
                3 => SkullFrame3,
                4 => SkullSimple,
                5 => SkullCrossed,
                6 => SkullFlaming,
                7 => SkullDanger,
                8 => SkullTribal,
                9 => SkullCreepy,
                _ => Skull
            };
        }
        
        // Returns random ASCII art
        public static string GetRandomArt()
        {
            int artIndex = random.Next(18);
            return artIndex switch
            {
                0 => Skull,
                1 => SkullFrame1,
                2 => SkullFrame2,
                3 => SkullFrame3,
                4 => SkullSimple,
                5 => SkullCrossed,
                6 => SkullFlaming, 
                7 => Computer,
                8 => HackingBanner,
                9 => MatrixBanner,
                10 => Lock,
                11 => SkullDanger,
                12 => SkullTribal,
                13 => SkullDetailed,
                14 => SkullHexed,
                15 => SkullCreepy,
                16 => SkullPixelated,
                17 => SkullBones,
                _ => Skull
            };
        }
        
        // Returns random banner art
        public static string GetRandomBanner()
        {
            int bannerIndex = random.Next(15);
            return bannerIndex switch
            {
                0 => HackingBanner,
                1 => MatrixBanner,
                2 => AccessDenied,
                3 => AccessGranted,
                4 => SystemFailure,
                5 => TerminalBanner,
                6 => WarningBanner,
                7 => SecurityBanner,
                8 => ExploitBanner,
                9 => FirewallBanner,
                10 => EncryptionBanner,
                11 => RansomwareBanner,
                12 => DataBreachBanner,
                13 => NetworkBanner,
                14 => BinaryCodeBanner,
                _ => HackedBanner
            };
        }
        
        // Returns random cybersecurity-themed banner art
        public static string GetRandomSecurityBanner()
        {
            int bannerIndex = random.Next(7);
            return bannerIndex switch
            {
                0 => SecurityBanner,
                1 => FirewallBanner,
                2 => EncryptionBanner,
                3 => DigitalSignatureBanner,
                4 => MalwareDetected,
                5 => IntrustionBanner,
                6 => CybercrimeScene,
                _ => WarningBanner
            };
        }
        
        // Returns a random frame for a specific animation sequence
        public static string GetAnimationFrame(string animationName, int frameNumber)
        {
            return animationName.ToLower() switch
            {
                "skull" => frameNumber switch
                {
                    0 => Skull,
                    1 => SkullFrame1,
                    2 => SkullFrame2,
                    3 => SkullFrame3,
                    _ => Skull
                },
                _ => GetRandomArt() // Default to random art if animation not found
            };
        }
        
        // Returns total frames for a specific animation
        public static int GetAnimationFrameCount(string animationName)
        {
            return animationName.ToLower() switch
            {
                "skull" => 4,
                _ => 1
            };
        }
    }
}
