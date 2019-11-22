using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Soundboard {
    class Soundboard {

        private static string filename = null;
        private static List<int> deviceNumbers = new List<int> { -1 };
        private static int mainDeviceNumber = -1;
        private static int auxDeviceNumber = -1;
        private static float volume = 1.0f;

        static void Main(string[] args) {
            if (args.Length == 0 || args[0].ToLower().Contains("help")) {
                printHelpMessage();
            } else if (args[0].ToLower().Contains("scan")) {
                scanDevices();
            } else if (args[0].ToLower().Contains("play")) {
                parsePlayOptions(args);
                playSounds();
            } else {
                throw new ArgumentException("Use 'help' for a list of valid commands.", "command");
            }
        }

        static void printHelpMessage() {
            Console.WriteLine("Usage: soundboard.exe command [options]");
            Console.WriteLine("Commands:");
            Console.WriteLine("\thelp : Display this help text.");
            Console.WriteLine("\tscan : Scan for device numbers.");
            Console.WriteLine("\tplay : Play an audio file.");
            Console.WriteLine("Options:");
            Console.WriteLine("\t-a : Audio file path. (REQUIRED)");
            Console.WriteLine("\t-d : Output device numbers (comma-separated)");
            Console.WriteLine("\t-v : Output volume");
            Console.Write("Press any key to exit...");
            Console.Read();
        }

        static void scanDevices() {
            Console.WriteLine("Available output devices:");
            for (int i = -1; i < WaveOut.DeviceCount; i++) {
                var caps = WaveOut.GetCapabilities(i);
                Console.WriteLine(i + ": " + caps.ProductName);
            }
        }

        static void parsePlayOptions(string[] args) {
            for (int i = 1; i < args.Length; i++) {
                switch (args[i]) {
                    case "-a":
                        filename = args[i + 1];
                        break;
                    case "-d":
                        parseDeviceNumbers(args[i + 1]);
                        break;
                    case "v":
                        parseVolumeInput(args[i + 1]);
                        break;
                }
            }

            if (filename == null) {
                throw new ArgumentException("No audio file supplied.", "audioFile");
            }
        }

        static void parseDeviceNumbers(string args) {
            string[] numberStrings = args.Split(',');
            deviceNumbers = new List<int>();
            foreach(string number in numberStrings) {
                try {
                    deviceNumbers.Add(int.Parse(number));
                } catch (Exception e) {
                    throw new ArgumentException("Devices must be a comma-separated list of integers.", "devices");
                }
            }
        }

        static void parseVolumeInput(string vol) {
            try {
                volume = float.Parse(vol);
            } catch (Exception e) {
                throw new ArgumentException("Volume must be between 0.0 and 1.0.", "volume");
            }
        }

        static void playSounds() {
            using var mainAudioFile = new AudioFileReader(filename);
            using var auxAudioFile = new AudioFileReader(filename);
            using WaveOutEvent mainOutputDevice = new WaveOutEvent() { DeviceNumber = mainDeviceNumber, Volume = volume };
            using WaveOutEvent auxOutputDevice = new WaveOutEvent() { DeviceNumber = auxDeviceNumber, Volume = volume };

            var caps = WaveOut.GetCapabilities(mainDeviceNumber);
            Console.WriteLine("Playing on " + caps.ProductName);
            mainOutputDevice.Init(mainAudioFile);

            if (mainDeviceNumber != auxDeviceNumber) {
                caps = WaveOut.GetCapabilities(auxDeviceNumber);
                Console.WriteLine("Playing on " + caps.ProductName);
                auxOutputDevice.Init(auxAudioFile);
            }
            
            mainOutputDevice.Play();

            if (mainDeviceNumber != auxDeviceNumber) {
                auxOutputDevice.Play();
            }

            while (mainOutputDevice.PlaybackState == PlaybackState.Playing || auxOutputDevice.PlaybackState == PlaybackState.Playing) {
                Thread.Sleep(1000);
            }
        }
    }
}
