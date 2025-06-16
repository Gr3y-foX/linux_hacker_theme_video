// filepath: /Users/phenix/Projects/hacker_theme_linux/hacker-terminal/src/Assets/AsciiArt.cs
using System;

namespace hacker_terminal.Assets
{
    public static class AsciiArt
    {
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

        public static string Matrix => @"
01001011001  010101010  110010110
10101  01010  01010  10101  01010
0010  110110  110110  110110  001
1101  101101  101101  101101  110
01  01010101  01010101  01010101  
10  10101010  10101010  10101010  
0  1010101010  1010101010  1010101
1  0101010101  0101010101  0101010
        ";

        public static string Hacker => @"
   _    _            _             
  | |  | |          | |            
  | |__| | __ _  ___| | _____ _ __ 
  |  __  |/ _` |/ __| |/ / _ \ '__|
  | |  | | (_| | (__|   <  __/ |   
  |_|  |_|\__,_|\___|_|\_\___|_|   
                                  
        ";

        public static string Error => @"
  _____                     
 | ____|_ __ _ __ ___  _ __ 
 |  _| | '__| '__/ _ \| '__|
 | |___| |  | | | (_) | |   
 |_____|_|  |_|  \___/|_|   
                           
        ";

        public static string GetArt(string artType)
        {
            return artType switch
            {
                "skull" => Skull,
                "matrix" => Matrix,
                "hacker" => Hacker,
                "error" => Error,
                _ => "Art not found."
            };
        }
    }
}
