using System.Windows.Forms;
using NAudio.Wave;

namespace Soundboard
{
    public partial class InformationWindow : Form
    {
        public InformationWindow()
        {
            InitializeComponent();
            populateDeviceData();
        }

        protected void populateDeviceData() {
            for (int i = -1; i < WaveOut.DeviceCount; i++) {
                var caps = WaveOut.GetCapabilities(i);
                deviceGrid.Rows.Add(new string[] { "" + i, caps.ProductName });
            }
        }
    }
}
