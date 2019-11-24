# Soundboard
A Windows soundboard command to play audio files through multiple output devices

## Source Requirements
- Target .NET Framework 3.5
- NAudio package

## Usage
Selecting the application or running from command line with no arguments will display the available audio device information and option information.

### Command Line Arguments
- -f : Audio file path (REQUIRED)
- -d : Device index list (comma-separated)
- -v : Volume selection list (float, comma-separated)
