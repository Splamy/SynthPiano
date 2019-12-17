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

		public double Frequency { get; set; }
		public double FrequencyBase { get; }
		public int FreqPos { get; set; }
		public int FadePos { get; set; }

		public bool IsWaveFading { get; private set; }
		public bool IsWaveFinalizing { get; private set; }
		private double lastAbs;

		const int blackkHeight = 130;
		public const int blackkWidth = 20;

		const int whitekHeight = 150;
		public const int whitekWidth = 50;

		public PianoKey(int x, int y, double frequency, bool black)
		{
			Black = black;
			Frequency = frequency;
			FrequencyBase = frequency;
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

		public void Reset()
		{
			Frequency = FrequencyBase;
			IsWaveFading = false;
			IsWaveFinalizing = false;
			FreqPos = 0;
			FadePos = 0;
		}

		public void Fade()
		{
			FadePos = FreqPos;
			IsWaveFading = true;
		}

		public void FinalizeWave()
		{
			lastAbs = double.PositiveInfinity;
			IsWaveFinalizing = true;
		}

		public double GetFadeMul() => Math.Pow(Global.FadePower, (FreqPos - FadePos) / (double)Global.Bitrate * 10);

		public bool IsFinal(double val)
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
		public double CalcSquare()
		{
			double fperbit = (Global.Bitrate / Frequency);
			return (FreqPos++ % fperbit) > fperbit / 2 ? 1 : -1;
		}
		public double CalcTriangle()
		{
			double fperbit = (Global.Bitrate / Frequency);
			return (Math.Abs(((FreqPos++ % fperbit) / fperbit) - 0.5) * 4 - 1);
		}
		public double CalcSawtooth()
		{
			double fperbit = (Global.Bitrate / Frequency);
			return 1 - ((FreqPos++ % fperbit) / fperbit) * 2;
		}

		public double CalcWave()
		{
			return Parent.WaveForm switch
			{
				WaveForm.Sine => CalcSine(),
				WaveForm.Sqaure => CalcSquare(),
				WaveForm.Triangle => CalcTriangle(),
				WaveForm.Sawtooth => CalcSawtooth(),
				_ => 0,
			};
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
