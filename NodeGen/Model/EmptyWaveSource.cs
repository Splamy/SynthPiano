using System;

namespace NodeGen.Model
{
	public class EmptyWaveSource : WaveBaseSpan
	{
		public static readonly EmptyWaveSource Instance = new EmptyWaveSource();

		public override void Read(Span<float> buffer) => buffer.Clear();

		private EmptyWaveSource() { }
	}
}
