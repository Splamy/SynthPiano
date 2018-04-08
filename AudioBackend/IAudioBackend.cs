using System;
using System.Collections.Generic;

namespace SynthPiano.AudioBackend
{
	public delegate void AudioGenerate(Span<byte> buffer);

	public interface IAudioBackend : IDisposable
	{
		void Init();
		AudioGenerate Read { get; set; }

		IEnumerable<DeviceId> GetDevices();
		void SetDevice(int id);
	}

	public class DeviceId
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
