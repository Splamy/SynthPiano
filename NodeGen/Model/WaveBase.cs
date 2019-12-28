using CSCore;
using System;

namespace NodeGen.Model
{
	public abstract class WaveBase : ISampleSource
	{
		public static readonly WaveFormat Default = new WaveFormat(Global.SampleRate, 32, 1, AudioEncoding.IeeeFloat);

		public bool CanSeek => false;
		public WaveFormat WaveFormat => Default;
		public long Position { get; set; } = 0;
		public long Length => 0;

		public virtual void Dispose() { }
		public abstract int Read(float[] buffer, int offset, int count);
	}

	public abstract class WaveBaseSpan : WaveBase
	{
		public override sealed int Read(float[] buffer, int offset, int count)
		{
			Read(buffer.AsSpan(offset, count));
			return count;
		}

		public abstract void Read(Span<float> buffer);
	}
}
