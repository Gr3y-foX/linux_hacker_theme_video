#!/bin/bash

# Convert Flipper Zero script to ensure proper line endings
# Usage: ./convert_to_ducky.sh input_script.txt output_script.txt

if [ "$#" -ne 2 ]; then
    echo "Usage: $0 input_script.txt output_script.txt"
    exit 1
fi

INPUT_FILE=$1
OUTPUT_FILE=$2

if [ ! -f "$INPUT_FILE" ]; then
    echo "Error: Input file '$INPUT_FILE' not found"
    exit 1
fi

# Create output directory if it doesn't exist
OUTPUT_DIR=$(dirname "$OUTPUT_FILE")
mkdir -p "$OUTPUT_DIR"

# Process each line:
# 1. Replace any Windows-style CRLF with LF
# 2. Ensure there's a blank line after each DELAY command for Ducky compatibility
# 3. Remove any trailing whitespace

sed 's/\r$//' "$INPUT_FILE" | \
    sed 's/^DELAY \([0-9][0-9]*\)$/DELAY \1\n/' | \
    sed 's/ *$//' > "$OUTPUT_FILE"

echo "Conversion complete: $INPUT_FILE -> $OUTPUT_FILE"
echo "File is now ready for use with Flipper Zero BadUSB or other Rubber Ducky compatible devices."
