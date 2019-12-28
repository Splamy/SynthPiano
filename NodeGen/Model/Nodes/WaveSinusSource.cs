using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGen.Model.Nodes
{
	public class WaveSinusSource : WaveBaseSpan
	{
		public IFloat? Phase { get; set; }
		public IFloat? Frequency { get; set; }

		public override void Read(Span<float> data)
		{
			// Check
			if (Frequency is null)
				return;
			// Read
			var phase = Phase?.Value ?? 0;
			var frequency = Frequency?.Value ?? 0;
			if (frequency == 0)
				return;
			// Process
			var fperbit = frequency / Global.SampleRate;
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = (float)Math.Sin(phase * 2 * Math.PI);
				phase = (phase + fperbit) % 1f;
			}
			// WriteBack
			if (Phase != null)
				Phase.Value = phase;
		}
	}
}
