using CSCore;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NodeGen.Model
{
	public class FillSource : IWaveSource
	{
		#region IWaveSource
		private static readonly WaveFormat Default = new WaveFormat(Global.SampleRate, 16, 1);

		public bool CanSeek => false;
		public WaveFormat WaveFormat => Default;
		public long Position { get; set; } = 0;
		public long Length => 0;
		#endregion

		public ISampleSource? Wave { get; set; }

		private float[] convBuffer = Array.Empty<float>();

		public int Read(byte[] buffer, int offset, int count)
		{
			var span = MemoryMarshal.Cast<byte, short>(buffer.AsSpan(offset, count));
			span.Clear();
			var wave = Wave;
			if (wave is null)
				return count;

			convBuffer = convBuffer.CheckBuffer(span.Length);
			wave.Read(convBuffer, 0, span.Length);

			var convSpan = convBuffer.AsSpan(0, span.Length);
			const int maxVal = short.MaxValue - 1;
			for (int i = 0; i < convSpan.Length; i++)
				span[i] = (short)(convSpan[i] * maxVal);

			Position += count;
			return count;
		}

		public void Dispose()
		{
			Wave?.Dispose();
		}
	}
}
