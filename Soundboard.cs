using NAudio.Wave;
using System;
using System.Threading;

namespace Soundboard {
    class Soundboard {

        private static string filename = null;
        private static int mainDeviceNumber = -1;
        private static int auxDeviceNumber = -1;
        private static float volume = 1.0f;

        static void Main(string[] args) {
            processInput(args);
            try {
                playSounds();
            } catch(Exception e) {
                exitWithMessage(e.ToString());
            }
            
        }

        static void processInput(string[] args) {
            if (args.Length < 2) {
                Console.WriteLine("Available output devices:");
                for (int i = 0; i < WaveOut.DeviceCount; i++) {
                    var caps = WaveOut.GetCapabilities(i);
                    Console.WriteLine(i + ": " + caps.ProductName);
                }

                exitWithMessage("Usage: {filename} {main output device number} {aux output device number} {volume}");
            }

            filename = args[0];

            try {
                if (args.Length > 1) {
                    mainDeviceNumber = Int32.Parse(args[1]);
                }

                if (args.Length > 2) {
                    auxDeviceNumber = Int32.Parse(args[2]);
                } else {
                    auxDeviceNumber = mainDeviceNumber;
                }

                if (args.Length > 3) {
                    volume = float.Parse(args[3]);
                }
            } catch(Exception e) {
                exitWithMessage(e.ToString());
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

        static void exitWithMessage(string message) {
            Console.WriteLine(message);
            Console.Write("Press any key to exit...");
            Console.Read();
            Environment.Exit(0);
        }
    }
}
