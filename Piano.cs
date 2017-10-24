using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SynthTest
{
	public partial class Piano : UserControl
	{
		const int baseoctave = 7;
		static readonly double[] fulltable = new double[] { 4186.01, 4434.92, 4698.63, 4978.03, 5274.04, 5587.65, 5919.91, 6271.93, 6644.88, 7040.00, 7458.62, 7902.13 };
		static readonly bool[] keytable = new[] { false, true, false, true, false, false, true, false, true, false, true, false };
		static readonly int[] whitemul = new[] { 0, 0, 1, 1, 2, 3, 3, 4, 4, 5, 5, 6 };
		const int keymulmax = 7;
		static readonly int[] blackmul = new[] { 0, 1, 1, 2, 2, 2, 4, 4, 5, 5, 6, 6 };
		const int scaleLen = 12;

		bool isDown = false;
		PianoKey pressKey = null;

		public event Action<PianoKey> PianoKeyDown;
		public event Action<PianoKey> PianoKeyUp;

		private List<PianoKey> AllKeys = new List<PianoKey>();
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
			foreach (PianoKey k in AllKeys)
				k.Draw(g);
		}

		private void RecalcKeys()
		{
			WhiteKeys.Clear();
			BlackKeys.Clear();

			for (int i = 0, xpos = 0; xpos + PianoKey.whitekWidth < Width; i++)
			{
				bool black = keytable[i % scaleLen];
				int octave = (i / scaleLen);
				double octavemul = Math.Pow(2, baseoctave - Octave - octave);
				if (black)
				{
					xpos = (i / scaleLen) * keymulmax * PianoKey.whitekWidth
							+ blackmul[i % scaleLen] * PianoKey.whitekWidth
							 - PianoKey.blackkWidth / 2;
					BlackKeys.Add(
						new PianoKey(
							xpos,
							0,
							fulltable[i % scaleLen] / octavemul,
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
							fulltable[i % scaleLen] / octavemul,
							false));
				}
			}

			AllKeys.Clear();
			AllKeys.AddRange(WhiteKeys.ToArray());
			AllKeys.AddRange(BlackKeys.ToArray());
			AllKeys.ForEach(x => x.Parent = this);
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
			if(isDown)
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

	public enum WaveForm
	{
		Sine,
		FakeSine,
		Sqaure,
		Triangle,
		Sawtooth,
	}
}
