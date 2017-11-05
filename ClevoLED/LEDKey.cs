using System.Windows.Media;
using ClevoWMIVB;

namespace ClevoLED
{
	class LEDKey
	{
		private float _opacity = 1;
		public float Opacity
		{
			get
			{
				return _opacity;
			}
			set
			{
				_opacity = value;

				if (lastUsedSection == Keyboard.All)
					clevoWMI.SetColorAll(ToClevoColor(lastColor));
				else
					clevoWMI.SetColor(ToKeyboardCode(lastUsedSection) + ToClevoColor(lastColor));
			}
		}

		ClevoWMI clevoWMI;
		Keyboard lastUsedSection = Keyboard.All;
		Color lastColor = Colors.White;

		public LEDKey()
		{
			clevoWMI = new ClevoWMI();
		}

		public void SetColor(Keyboard section, Color color)
		{
			if (color != lastColor || section != lastUsedSection)
			{
				lastUsedSection = section;
				lastColor = color;

				if (section == Keyboard.All)
					clevoWMI.SetColorAll(ToClevoColor(color));
				else
					clevoWMI.SetColor(ToKeyboardCode(section) + ToClevoColor(color));
			}
		}

		public void SetOpacity(Keyboard section, float opacity)
		{
			if (opacity != _opacity || section != lastUsedSection)
			{
				lastUsedSection = section;
				_opacity = opacity;

				if (section == Keyboard.All)
					clevoWMI.SetColorAll(ToClevoColor(lastColor));
				else
					clevoWMI.SetColor(ToKeyboardCode(section) + ToClevoColor(lastColor));
			}
		}

		public string Int2Hex(int fNum)
		{
			return fNum.ToString("X2");
		}

		public string ToClevoColor(Color color)
		{
			//return string.Format("B: {0} R: {1} G: {2}", Int2Hex(b), Int2Hex(r), Int2Hex(g));
			return string.Format("{0}{1}{2}", Int2Hex(color.B * (byte)(Opacity * 255) / 255),
											  Int2Hex(color.R * (byte)(Opacity * 255) / 255),
											  Int2Hex(color.G * (byte)(Opacity * 255) / 255));
		}

		public string ToKeyboardCode(Keyboard section)
		{
			if (section == Keyboard.Left)
				return "F0";
			else if (section == Keyboard.Center)
				return "F1";
			else if (section == Keyboard.Right)
				return "F2";

			return null;
		}

		public enum Keyboard
		{
			Left,
			Center,
			Right,
			All
		}
	}
}
