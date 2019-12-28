using CSCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NodeGen.Model.Nodes
{
	public class WaveMerge : WaveBaseSpan
	{
		public IEnumerable<ISampleSource> Waves { get; set; } = Enumerable.Empty<ISampleSource>();

		private float[] sumBuffer = Array.Empty<float>();

		public override void Read(Span<float> buffer)
		{
			try
			{
				sumBuffer = sumBuffer.CheckBuffer(buffer.Length);
				foreach (var wave in Waves)
				{
					sumBuffer.AsSpan().Clear();
					wave.Read(sumBuffer, 0, buffer.Length);

					for (int i = 0; i < buffer.Length; i++)
						buffer[i] += sumBuffer[i];
				}
			}
			catch { }
		}
	}
}
