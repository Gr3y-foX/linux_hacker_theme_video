#!/usr/bin/env python3
# Keyboard interception for Hacker Terminal
import os
import sys
import time
import random
import signal
import threading

try:
    from pynput import keyboard
    from pynput.keyboard import Key, KeyCode
except ImportError:
    print('Installing pynput package...')
    import subprocess
    subprocess.check_call([sys.executable, '-m', 'pip', 'install', 'pynput'])
    from pynput import keyboard
    from pynput.keyboard import Key, KeyCode

# ANSI colors
RED = '\033[91m'
GREEN = '\033[92m'
YELLOW = '\033[93m'
BLUE = '\033[94m'
MAGENTA = '\033[95m'
CYAN = '\033[96m'
RESET = '\033[0m'
BOLD = '\033[1m'
BLINK = '\033[5m'

# Track currently pressed keys
current_keys = set()

# Define combinations to intercept
COMBINATIONS = [
    {'name': 'ctrl+c', 'keys': {Key.ctrl, KeyCode.from_char('c')}},
    {'name': 'ctrl+z', 'keys': {Key.ctrl, KeyCode.from_char('z')}},
    {'name': 'ctrl+d', 'keys': {Key.ctrl, KeyCode.from_char('d')}},
    {'name': 'alt+tab', 'keys': {Key.alt, Key.tab}},
    {'name': 'alt+f4', 'keys': {Key.alt, Key.f4}},
    {'name': 'cmd+q', 'keys': {Key.cmd, KeyCode.from_char('q')}},
    {'name': 'cmd+w', 'keys': {Key.cmd, KeyCode.from_char('w')}},
    {'name': 'cmd+tab', 'keys': {Key.cmd, Key.tab}},
    {'name': 'cmd+c', 'keys': {Key.cmd, KeyCode.from_char('c')}},
    {'name': 'cmd+v', 'keys': {Key.cmd, KeyCode.from_char('v')}},
    {'name': 'cmd+m', 'keys': {Key.cmd, KeyCode.from_char('m')}},
    {'name': 'cmd+h', 'keys': {Key.cmd, KeyCode.from_char('h')}},
    {'name': 'cmd+space', 'keys': {Key.cmd, Key.space}},
    {'name': 'cmd+alt+esc', 'keys': {Key.cmd, Key.alt, Key.esc}},
    {'name': 'escape', 'keys': {Key.esc}},
]

# Add function keys
for i in range(1, 13):
    COMBINATIONS.append({
        'name': f'f{i}', 
        'keys': {getattr(Key, f'f{i}')}
    })

# Taunting messages
TAUNTING_MESSAGES = [
    'Huh, nice try)',
    'Oopsie doopsie kiddo, something went wrong?',
    'Nope, not today!',
    'You thought it would be that easy?',
    'Sorry, the exit is... elsewhere',
    'Those keys are disabled. Try harder!',
    'Clever, but not clever enough',
    'This lockdown isn\'t broken that easily',
    'Good luck getting out that way',
    'Access Denied: Unauthorized Escape Attempt',
    'Did you really think Cmd+Q would work? LOL',
    'That\'s not how this works, that\'s not how any of this works',
    'Error 403: Freedom Forbidden',
    'The universe laughs at your escape attempt'
]

# Exit combination - pressing this will stop the script
EXIT_COMBINATION = {Key.ctrl, Key.shift, KeyCode.from_char('x')}

# Special keys to release immediately to avoid key getting "stuck"
SPECIAL_RELEASE_KEYS = {Key.cmd, Key.alt, Key.shift}

# Define the skull animation frames
skull_frame_0 = [
    "                 _,.-------.,_",
    "             ,;~'             '~;,",
    "           ,;                     ;,",
    "          ;                         ;",
    "         ,'                         ',",
    "        ,;                           ;,",
    "        ; ;      .           .      ; ;",
    "        | ;   ______       ______   ; |",
    "        |  `/~\"     ~\" . \"~     \"~\\'  |",
    "        |  ~  ,-~~~^~, | ,~^~~~-,  ~  |",
    "         |   |        }:{        |   |",
    "         |   l       / | \\       !   |",
    "         .~  (__,.--\" .^. \"--.,__)  ~.",
    "         |     ---;' / | \\ `;---     |",
    "          \\__.       \\/^\\/       .__/",
    "           V| \\                 / |V",
    "            | |T~\\___!___!___/~T| |",
    "            | |`IIII_I_I_I_IIII'| |",
    "            |  \\,III I I I III,/  |",
    "             \\   `~~~~~~~~~~'    /",
    "               \\   .       .   /",
    "                 \\.    ^    ./",
    "                   ^~~~^~~~^"
]

skull_frame_1 = [
    "                 _,.-------.,_",
    "             ,;~'             '~;,",
    "           ,;        _          ;,",
    "          ;        (   )          ;",
    "         ,'        (_ )           ',",
    "        ,;           `             ;,",
    "        ; ;      .           .      ; ;",
    "        | ;   ______       ______   ; |",
    "        |  `/~\"    ~\" . \"~    \"~\\'  |",
    "        |  ~ ,-~~~^~, | ,~^~~~-, ~  |",
    "         |  /|       }:{       |\\  |",
    "         |   l      / | \\      !   |",
    "         .~ (__,.--\" .^. \"--.,__) ~.",
    "         |    ---;' / | \\ `;---    |",
    "          \\__.      \\/o\\/      .__/",
    "           V| \\                 / |V",
    "            | |T~\\___!___!___/~T| |",
    "            | |`IIII_I_I_I_IIII'| |",
    "            |  \\,III I I I III,/  |",
    "             \\   `~~~~~~~~~~'    /",
    "               \\   .  .  .    /",
    "                 \\.   -   ./",
    "                   -~~~-~~~-"
]

skull_frame_2 = [
    "                 _,.-------.,_",
    "             ,;~'             '~;,",
    "           ,;     \\     /       ;,",
    "          ;       (x)   (x)      ;",
    "         ,'       \\  ___  /       ',",
    "        ,;         \\|   |/         ;,",
    "        ; ;      .   \\ /   .      ; ;",
    "        | ;   ______\\_/_______   ; |",
    "        |  `/~\"     ~\" . \"~     \"~\\'  |",
    "        |  ~  ,-~~~^~, | ,~^~~~-, ~  |",
    "         |   |        }:{        |   |",
    "         |   l       / | \\       !   |",
    "         .~  (__,.--\" .^. \"--.,__) ~.",
    "         |     ---;' / | \\ `;---     |",
    "          \\__.       \\/^\\/       .__/",
    "           V| \\   *           * / |V",
    "            | |T~\\___!___!___/~T| |",
    "            | |`IIII_I_I_I_IIII'| |",
    "            |  \\,III I I I III,/  |",
    "             \\   `~~~~~~~~~~'    /",
    "               \\    \\___/     /",
    "                 \\.  \\_/  ./",
    "                   \\~~ ~~/"
]

skull_frame_3 = [
    "                 _,.-------.,_",
    "             ,;~'   .-.   .-. '~;,",
    "           ,;      (   ) (   )    ;,",
    "          ;         `-'   `-'       ;",
    "         ,'                         ',",
    "        ,;         .-\"\"\"\"-          ;,",
    "        ; ;      .           .      ; ;",
    "        | ;   ______  ___  ______   ; |",
    "        |  `/~\"    ~\"     \"~    \"~\\' |",
    "        |  ~  ,-~~~^~, | ,~^~~~-, ~  |",
    "         |   |  (◕)  }:{  (◕)  |   |",
    "         |   l       / | \\       !  |",
    "         .~  (__,.--\" .^. \"--.,__) ~.",
    "         |     ---;' / | \\ `;---     |",
    "          \\__.       \\/^\\/       .__/",
    "           V| \\   *           * / |V",
    "            | |T~\\___!___!___/~T| |",
    "            | |`IIII_I_I_I_IIII'| |",
    "            |  \\,III I I I III,/  |",
    "             \\   `~~~~~~~~~~'    /",
    "               \\   .       .   /",
    "                 \\.    ^    ./",
    "                   ^~~~^~~~^"
]

skull_frames = [skull_frame_0, skull_frame_1, skull_frame_2, skull_frame_3]

def display_taunt():
    """Returns a random taunting message"""
    return random.choice(TAUNTING_MESSAGES)

def clear_screen():
    """Clear the terminal screen"""
    os.system('clear' if os.name == 'posix' else 'cls')

def display_centered_text(text, color=RED, effect=""):
    """Display text centered in the terminal"""
    try:
        terminal_width = os.get_terminal_size().columns
    except:
        terminal_width = 80
    
    lines = text.split('\n')
    for line in lines:
        spaces = (terminal_width - len(line)) // 2
        print(" " * spaces + color + effect + line + RESET)

def launch_laugh_skull():
    """Launch the laughing skull animation in the terminal"""
    # Get a taunting message
    taunt = display_taunt()
    
    # Clear screen and show taunt
    clear_screen()
    print("\n" * 3)  # Add some space at the top
    display_centered_text(taunt, RED, BOLD)
    time.sleep(0.5)

    # Show the skull animation
    for _ in range(10):  # Show animation for about 5 seconds
        clear_screen()
        frame = random.choice(skull_frames)
        print("\n" * 2)  # Space at top
        for line in frame:
            display_centered_text(line, GREEN)
        print("\n")  # Space after skull
        display_centered_text(taunt, RED, BOLD if random.random() > 0.5 else BLINK)
        time.sleep(0.5)

    clear_screen()
    print("\n" * 3)
    display_centered_text("ACCESS DENIED", RED, BOLD)
    time.sleep(1)

def simulate_random_keypress():
    """Simulates a random alphanumeric key press"""
    random_char = random.choice('abcdefghijklmnopqrstuvwxyz0123456789')
    
    try:
        controller = keyboard.Controller()
        # First, release any special keys that might be held down
        for skey in SPECIAL_RELEASE_KEYS.intersection(current_keys):
            controller.release(skey)
        
        # Press and release the random character
        controller.press(random_char)
        time.sleep(0.05)  # Short delay
        controller.release(random_char)
        
        # Ensure all special keys are released
        for skey in list(current_keys):
            if skey in SPECIAL_RELEASE_KEYS:
                try:
                    controller.release(skey)
                    current_keys.discard(skey)
                except:
                    pass
    except Exception as e:
        print(f"Error simulating keypress: {e}")

def is_combination_pressed(combination_keys):
    """Check if a combination of keys is pressed"""
    return all(k in current_keys for k in combination_keys)

def check_accessibility_permissions():
    """
    Check if accessibility permissions are granted.
    Returns True if permissions seem to be in place.
    """
    try:
        # Just creating the controller object is enough to check for permissions
        controller = keyboard.Controller()
        # Try to listen for a key event - this is a more reliable test than pressing a key
        with keyboard.Listener(on_press=lambda k: None, on_release=lambda k: None) as listener:
            # Start and immediately stop - just to check if we can access the listener
            time.sleep(0.1)
            listener.stop()
        return True
    except Exception as e:
        print(f"Failed accessibility test: {e}")
        return False

def handle_key_combination():
    """
    Handle an intercepted key combination by launching the skull,
    showing a taunt message, and simulating a random key press.
    """
    # Launch laughing skull animation
    launch_laugh_skull()
    
    # Display taunting message
    taunt = display_taunt()
    print(f"\033[91m{taunt}\033[0m")  # Red color
    
    # Simulate random key press
    simulate_random_keypress()

def on_press(key):
    """Handler for key press events"""
    # Add to currently pressed keys
    current_keys.add(key)
    
    # Check for exit combination first
    if is_combination_pressed(EXIT_COMBINATION):
        print('\n' + GREEN + BOLD + 'Exit combination detected. Stopping keyboard interceptor.' + RESET)
        return False  # Stop the listener
    
    # Check for combinations to intercept
    for combo in COMBINATIONS:
        if is_combination_pressed(combo['keys']):
            print(f"\n{RED}{BOLD}Intercepted: {combo['name']}{RESET}")
            
            # Handle the key combination in a non-blocking way
            threading.Thread(target=handle_key_combination).start()
            
            # Important to release all keys in the combination to avoid "stuck keys"
            try:
                controller = keyboard.Controller()
                for k in combo['keys']:
                    if k in current_keys:
                        controller.release(k)
                        current_keys.discard(k)
            except Exception as e:
                print(f"Error releasing keys: {e}")
                    
            return False  # Critical - stop key from propagating further
    
    # Allow normal keys to pass through
    return True

def on_release(key):
    """Handler for key release events"""
    try:
        current_keys.remove(key)
    except KeyError:
        # Key might not be in the set if we intercepted its press event
        pass

def anti_stuck_keys_thread():
    """Periodically check for and release any keys that may be "stuck" down"""
    try:
        controller = keyboard.Controller()
        while True:
            try:
                # Check every 2 seconds
                time.sleep(2)
                # Look for special keys that might be stuck
                for skey in SPECIAL_RELEASE_KEYS.intersection(current_keys):
                    controller.release(skey)
                    current_keys.discard(skey)
            except:
                pass
    except Exception as e:
        print(f"Error in anti-stuck thread: {e}")

def main():
    """Main function to run the keyboard interceptor"""
    print(f"{BLUE}{BOLD}Starting keyboard interceptor...{RESET}")
    
    # Handle Ctrl+C to prevent the script from being terminated with a keyboard interrupt
    def handle_sigint(signum, frame):
        print(f"\n{RED}{display_taunt()}{RESET}")  # Display taunt when Ctrl+C is pressed
        threading.Thread(target=launch_laugh_skull).start()
        return
    
    # Register the custom signal handler for SIGINT (Ctrl+C)
    signal.signal(signal.SIGINT, handle_sigint)
    
    if not check_accessibility_permissions():
        print("\n\033[1;31m⚠️ ACCESSIBILITY PERMISSIONS REQUIRED ⚠️\033[0m")
        print("\033[1mThis script needs to monitor keyboard events to work properly.\033[0m")
        print("\n\033[1mFollow these steps to grant permissions:\033[0m")
        
        if sys.platform == 'darwin':  # macOS
            print("1. Open \033[1mSystem Settings > Privacy & Security > Accessibility\033[0m")
            print("2. Click the '+' button to add an application")
            print("3. Navigate to and select \033[1mTerminal\033[0m (or \033[1miTerm\033[0m if you're using that)")
            print("4. Make sure the checkbox next to Terminal is \033[1mchecked\033[0m")
            
            # Try to open the Accessibility settings directly
            try:
                subprocess.run(["open", "x-apple.systempreferences:com.apple.preference.security?Privacy_Accessibility"])
                print("\n\033[32mThe System Settings app should be opening now...\033[0m")
            except Exception:
                print("\nPlease open System Settings manually.")
        
        elif sys.platform.startswith('linux'):  # Linux
            print("Depending on your Linux distribution:")
            print("1. For Ubuntu/Debian: You may need to run with sudo or configure uinput permissions")
            print("2. For other distributions: Check input accessibility settings")
        
        print("\n\033[33mAfter granting permissions:\033[0m")
        print("1. Close and restart Terminal")
        print("2. Run this script again")
        
        # Ask if the user wants to try continuing anyway
        try:
            response = input("\n\033[1mWould you like to try continuing anyway? (y/n): \033[0m")
            if response.lower() != 'y':
                return
            print("\nAttempting to continue without confirmed permissions...")
        except KeyboardInterrupt:
            return
    
    print(f"\n{YELLOW}{BOLD}===== LOCKDOWN KEYBOARD INTERCEPTOR ACTIVE ====={RESET}")
    print(f"Intercepting: {', '.join(combo['name'] for combo in COMBINATIONS)}")
    print(f"{GREEN}Press Ctrl+Shift+X to exit.{RESET}")
    
    # Start anti-stuck keys thread
    threading.Thread(target=anti_stuck_keys_thread, daemon=True).start()
    
    try:
        # Start the keyboard listener in blocking mode to ensure all keys are captured
        with keyboard.Listener(on_press=on_press, on_release=on_release, suppress=True) as listener:
            listener.join()
    except Exception as e:
        print(f"\n{RED}Error in keyboard listener: {e}{RESET}")
    
    print(f"\n{GREEN}{BOLD}Keyboard interceptor stopped.{RESET}")

if __name__ == "__main__":
    main()
