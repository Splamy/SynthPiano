using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SynthTest
{
	public partial class Piano : UserControl
	{
		private const double freq_a_4 = 440;
		private static readonly double a_pow = Math.Pow(2, 1 / 12d);
		static readonly bool[] keytable = new[] { false, true, false, true, false, false, true, false, true, false, true, false };
		static readonly int[] whitemul = new[] { 0, 0, 1, 1, 2, 3, 3, 4, 4, 5, 5, 6 };
		const int keymulmax = 7;
		static readonly int[] blackmul = new[] { 0, 1, 1, 2, 2, 2, 4, 4, 5, 5, 6, 6 };
		const int scaleLen = 12;

		bool isDown = false;
		PianoKey pressKey = null;

		public event Action<PianoKey> PianoKeyDown;
		public event Action<PianoKey> PianoKeyUp;

		public List<PianoKey> AllKeys = new List<PianoKey>();
		public List<PianoKey> WhiteKeys { get; } = new List<PianoKey>();
		public List<PianoKey> BlackKeys { get; } = new List<PianoKey>();

		private int octave = 0;
		public int Octave { get { return octave; } set { octave = value; RecalcKeys(); } }
		public WaveForm WaveForm { get; set; } = WaveForm.Sine;

		public Piano()
		{
			InitializeComponent();
		}

		private void Piano_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			foreach (PianoKey k in WhiteKeys)
				k.Draw(g);
			foreach (PianoKey k in BlackKeys)
				k.Draw(g);
		}

		private static double GetFreq(int index) => freq_a_4 * Math.Pow(a_pow, index - (8 + 12 * 4));
		private static double GetFreq(int octave, int note) => GetFreq(octave * 12 + note);

		private void RecalcKeys()
		{
			WhiteKeys.Clear();
			BlackKeys.Clear();

			for (int i = 0, xpos = 0; xpos + PianoKey.whitekWidth < Width; i++)
			{
				bool black = keytable[i % scaleLen];
				int octave = (i / scaleLen);
				double freq = GetFreq(Octave, i);
				if (black)
				{
					xpos = (i / scaleLen) * keymulmax * PianoKey.whitekWidth
							+ blackmul[i % scaleLen] * PianoKey.whitekWidth
							 - PianoKey.blackkWidth / 2;
					BlackKeys.Add(
						new PianoKey(
							xpos,
							0,
							freq,
							true));
				}
				else
				{
					xpos = (i / scaleLen) * keymulmax * PianoKey.whitekWidth
							+ whitemul[i % scaleLen] * PianoKey.whitekWidth;
					WhiteKeys.Add(
						new PianoKey(
							xpos,
							0,
							freq,
							false));
				}
			}

			AllKeys.Clear();
			AllKeys.AddRange(WhiteKeys.ToArray());
			AllKeys.AddRange(BlackKeys.ToArray());
			AllKeys.ForEach(x => x.Parent = this);
			AllKeys.Sort((a, b) => Math.Sign(a.Frequency - b.Frequency));
		}

		private void Piano_Resize(object sender, EventArgs e)
		{
			RecalcKeys();
			Invalidate();
		}

		private void Piano_MouseDown(object sender, MouseEventArgs e)
		{
			var key = FindFrequency(e.Location);
			InvokeDown(key);
		}

		private void Piano_MouseUp(object sender, MouseEventArgs e)
		{
			InvokeDown(null);
		}

		private PianoKey FindFrequency(Point p)
			=> AllKeys.Where(k => k.IsContained(p)).OrderByDescending(k => k.Black).FirstOrDefault();

		private void Piano_MouseMove(object sender, MouseEventArgs e)
		{
			if (isDown)
			{
				var key = FindFrequency(e.Location);
				InvokeDown(key);
			}
		}

		private void InvokeDown(PianoKey key)
		{
			if (pressKey != key)
			{
				if (pressKey != null)
				{
					PianoKeyUp?.Invoke(pressKey);
					//pressKey.FreqPos = 0;
					pressKey = null;
					isDown = false;
				}
				if (key != null)
				{
					pressKey = key;
					PianoKeyDown?.Invoke(key);
					isDown = true;
				}
			}
		}
	}

	[Serializable]
	public enum WaveForm
	{
		Sine,
		Sqaure,
		Triangle,
		Sawtooth,
	}
}
