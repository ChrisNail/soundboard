using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Soundboard {
    class Soundboard {

        private static string filename = null;
        private static List<int> deviceNumberList = new List<int> { -1 };
        private static List<float> volumeList = new List<float> { 1.0f };

        private static InformationWindow window = null;

        static void Main(string[] args) {
            if (args.Length == 0) {
                displayInformation();
            } else {
                try {
                    parsePlayOptions(args);
                    playSounds();
                } catch (Exception e) {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        static void displayInformation() {
            window = new InformationWindow();
            Application.Run(window);
        }

        static void parsePlayOptions(string[] args) {
            for (int i = 0; i < args.Length; i++) {
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
                throw new ArgumentException("No audio file supplied.");
            }

            // Verify Devices
            for (int i = 0; i < deviceNumberList.Count; i++) {
                try {
                    WaveOut.GetCapabilities(deviceNumberList[i]);
                } catch (Exception e) {
                    throw new ArgumentException("Device ID " + deviceNumberList[i] + " is not a valid device.", e);
                }
            }
        }

        static void parseDeviceNumbers(string args) {
            string[] numberStringList = args.Split(',');
            deviceNumberList = new List<int>();
            foreach (string numberString in numberStringList) {
                try {
                    int number = int.Parse(numberString);
                    deviceNumberList.Add(number);
                } catch (Exception e) {
                    throw new ArgumentException("Device ID " + numberString + " is invalid.", e);
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
                        throw new ArgumentException("Volume " + volume + " is not between 0.0 and 1.0");
                    }
                    volumeList.Add(volume);
                } catch (Exception e) {
                    throw new ArgumentException("Volume " + volumeString + " is invalid.", e);
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
                outputDeviceList.Add(outputDevice);
                if (volumeList.Count > i) {
                    audioFileList[i].Volume = volumeList[i];
                } else {
                    audioFileList[i].Volume = volumeList[0];
                }

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
