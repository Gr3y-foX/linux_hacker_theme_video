#!/bin/bash

# Script to fix namespace inconsistencies in the project
# Sometimes files can have different namespaces (HackerTerminal vs hacker_terminal)
# This script unifies them

echo "=== Fixing namespace inconsistencies ==="

# Navigate to script directory
cd "$(dirname "$0")"

# Fix namespaces in all C# files
find src -name "*.cs" -type f -exec sed -i 's/namespace HackerTerminal/namespace hacker_terminal/g' {} \;
find src -name "*.cs" -type f -exec sed -i 's/HackerTerminal\./hacker_terminal./g' {} \;

echo "Namespace fixes applied to all C# files."
echo "You may need to rebuild the project."

# Ensure run.sh is executable
chmod +x run.sh

echo "=== Done! ===="
echo "Try running the application again with: ./run.sh"
