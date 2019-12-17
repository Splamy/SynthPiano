using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynthTest
{
	public abstract class Wave
	{
		public double Frequency { get; set; }
		public int FreqPos { get; set; }
		public int FadePos { get; set; }
		public bool IsWaveFading { get; private set; }

		public virtual double Next()
		{
			var val = NextWaveRaw();
			if (IsWaveFading)
			{
				var fade = GetFadeMul();
				val *= fade;
			}
			FreqPos++;
			return val;
		}

		public virtual double GetFadeMul()
		{
			return Math.Pow(Global.FadePower, (FreqPos - FadePos) / (double)Global.Bitrate * 10);
		}

		public abstract double NextWaveRaw();
	}

	public class SineWave : Wave
	{
		public override double Next()
		{
			throw new NotImplementedException();
		}

		public override double NextWaveRaw()
		{
			return Math.Sin(FreqPos / (double)Global.Bitrate * Frequency * 2 * Math.PI);
		}
	}
}
