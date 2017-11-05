using NAudio.CoreAudioApi;
using System;
using System.Text;
using System.Windows.Media;

namespace ClevoLED
{
	class Program
	{
		static void Main(string[] args)
		{
			LEDKey ledKey = new LEDKey();
			ledKey.SetColor(LEDKey.Keyboard.All, Colors.DarkRed);

			MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
			MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

			while (true)
			{
				System.Threading.Thread.Sleep(20);
				Console.Clear();
				var volume = defaultDevice.AudioMeterInformation.MasterPeakValue;
				var volumes = defaultDevice.AudioMeterInformation.PeakValues;
				
				ledKey.SetOpacity(LEDKey.Keyboard.Left, volumes[0]);
				ledKey.SetOpacity(LEDKey.Keyboard.Center, volume);
				ledKey.SetOpacity(LEDKey.Keyboard.Right, volumes[1]);
				
				var scale = (int)Math.Floor(volumes[0] * 79);
				var sb = new StringBuilder();
				sb.Append('-', scale);
				sb.Append(' ', 79 - scale);
				Console.Write(sb.ToString() + "\r\n");

				scale = (int)Math.Floor(volume * 79);
				sb = new StringBuilder();
				sb.Append('-', scale);
				sb.Append(' ', 79 - scale);
				Console.Write(sb.ToString() + "\r\n");

				scale = (int)Math.Floor(volumes[1] * 79);
				sb = new StringBuilder();
				sb.Append('-', scale);
				sb.Append(' ', 79 - scale);
				Console.Write(sb.ToString() + "\r\n");
				
			}
		}
	}
}
