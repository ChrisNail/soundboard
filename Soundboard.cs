using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Soundboard {
    class Soundboard {

        private static string filename = null;
        private static List<int> deviceNumberList = new List<int> { -1 };
        private static List<float> volumeList = new List<float> { 1.0f };

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
            Console.WriteLine("\t-f : Audio file path. (REQUIRED)");
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
                    case "-f":
                        filename = args[i + 1];
                        break;
                    case "-d":
                        parseDeviceNumbers(args[i + 1]);
                        break;
                    case "-v":
                        parseVolumeInput(args[i + 1]);
                        break;
                }
            }

            if (filename == null) {
                throw new ArgumentException("No audio file supplied.", "audioFile");
            }

            for (int i = 0; i < deviceNumberList.Count; i++) {
                var caps = WaveOut.GetCapabilities(deviceNumberList[i]);
                float volume = volumeList.Count > i ? volumeList[i] * 100 : volumeList[0] * 100;
                Console.WriteLine("Playing on " + caps.ProductName + " at volume " + (int)volume);
            }
        }

        static void parseDeviceNumbers(string args) {
            string[] numberStringList = args.Split(',');
            deviceNumberList = new List<int>();
            foreach (string numberString in numberStringList) {
                try {
                    int number = int.Parse(numberString);
                    var caps = WaveOut.GetCapabilities(number);
                    Console.WriteLine("Selected Device: " + caps.ProductName);
                    deviceNumberList.Add(number);
                } catch (Exception e) {
                    throw new ArgumentException("Devices must be a comma-separated list of valid device indices.", "devices");
                }
            }
        }

        static void parseVolumeInput(string args) {
            string[] volumeStringList = args.Split(',');
            volumeList = new List<float>();
            foreach (string volumeString in volumeStringList) {
                try {
                    float volume = float.Parse(volumeString);
                    if (volume < 0.0f || volume > 1.0f) {
                        throw new ArgumentOutOfRangeException();
                    }
                    volumeList.Add(volume);
                } catch (Exception e) {
                    throw new ArgumentOutOfRangeException("volume", "Volumes must be between 0.0 and 1.0.");
                }
            }
        }

        static void playSounds() {
            using DisposableList<AudioFileReader> audioFileList = new DisposableList<AudioFileReader>();
            using DisposableList<WaveOutEvent> outputDeviceList = new DisposableList<WaveOutEvent>();
            for (int i = 0; i < deviceNumberList.Count; i++) {
                AudioFileReader audioFile = new AudioFileReader(@filename);
                audioFileList.Add(audioFile);
                WaveOutEvent outputDevice = new WaveOutEvent() { DeviceNumber = deviceNumberList[i] };
                if (volumeList.Count > i) {
                    outputDevice.Volume = volumeList[i];
                } else {
                    outputDevice.Volume = volumeList[0];
                }

                outputDeviceList.Add(outputDevice);
                outputDeviceList[i].Init(audioFileList[i]);
            }

            for (int i = 0; i < outputDeviceList.Count; i++) {
                outputDeviceList[i].Play();
            }

            bool playbackFinished = true;

            do {
                playbackFinished = true;
                foreach (WaveOutEvent outputEvent in outputDeviceList) {
                    Thread.Sleep(100);
                    if (outputEvent.PlaybackState == PlaybackState.Playing) {
                        playbackFinished = false;
                        break;
                    }
                }
            } while (!playbackFinished);
        }
    }
}
