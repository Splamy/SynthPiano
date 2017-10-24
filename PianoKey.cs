// This Code was created by Microgold Software Inc. for educational purposes
// Copyright Microgold Software Inc. Saturday, January 25, 2003

using System;
using System.Drawing;

namespace SynthTest
{
	/// <summary>
	/// Summary description for PianoKey.
	/// </summary>
	public class PianoKey
	{
		public Piano Parent { get; set; }
		public Rectangle Bounds { get; }
		public bool Selected { get; set; }
		public bool Black { get; }

		public double Frequency { get; }
		public int FreqPos { get; set; }

		private bool waveFin;
		public bool WaveFin { get { return waveFin; } set { waveFin = value; if (waveFin) lastAbs = double.PositiveInfinity; } }
		private double lastAbs;

		const int blackkHeight = 130;
		public const int blackkWidth = 20;

		const int whitekHeight = 150;
		public const int whitekWidth = 50;

		public PianoKey(int x, int y, double frequency, bool black)
		{
			Black = black;
			Frequency = frequency;
			Bounds = black
				  ? new Rectangle(x, y, blackkWidth, blackkHeight)
				  : new Rectangle(x, y, whitekWidth, whitekHeight);
			FreqPos = 0;
			Selected = false;
		}

		public void Draw(Graphics g)
		{
			if (Black)
				g.FillRectangle(Selected ? Brushes.DarkBlue : Brushes.Black, Bounds);
			else
			{
				g.FillRectangle(Selected ? Brushes.SkyBlue : Brushes.White, Bounds);
				g.DrawRectangle(Pens.Black, Bounds);
			}
		}

		public bool IsContained(Point p)
		{
			return Bounds.Contains(p);
		}

		public bool IsFin(double val)
		{
			var absval = Math.Abs(val);
			if (absval < lastAbs)
			{
				lastAbs = absval;
				return false;
			}
			else
			{
				return true;
			}
		}

		public double CalcSine() => Math.Sin(FreqPos++ / (double)Global.Bitrate * Frequency * 2 * Math.PI);
		public double CalcFakeSine() => Math.Sin(FreqPos++ / (double)Global.Bitrate * Frequency);
		public double CalcSquare()
		{
			// Math.Sin(key.FreqPos++ / (double)bitrate * key.Frequency) > 0 ? 1 : -1
			double fperbit = (Global.Bitrate / Frequency);
			return (FreqPos++ % fperbit) > fperbit / 2 ? 1 : -1;
		}
		public double CalcTriangle()
		{
			double fperbit = (Global.Bitrate / Frequency) * 2;
			return (Math.Abs(((FreqPos++ % fperbit) / fperbit) - 0.5) * 4 - 1); // (FreqPos++ % fperbit) > fperbit / 2 ? linwave : 2 - linwave;
		}
		public double CalcSawtooth()
		{
			double fperbit = (Global.Bitrate / Frequency);
			return 1 - ((FreqPos++ % fperbit) / fperbit) * 2;
		}

		public double CalcWave()
		{
			switch (Parent.WaveForm)
			{
			case WaveForm.Sine: return CalcSine();
			case WaveForm.FakeSine: return CalcFakeSine();
			case WaveForm.Sqaure: return CalcSquare();
			case WaveForm.Triangle: return CalcTriangle();
			case WaveForm.Sawtooth: return CalcSawtooth();
			default: return 0;
			}
		}

		public PianoKey Clone()
		{
			return new PianoKey(Bounds.X, Bounds.Y, Frequency, Black)
			{
				Parent = Parent,
				FreqPos = 0,
			};
		}
	}
}
