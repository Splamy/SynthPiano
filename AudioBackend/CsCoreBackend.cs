using System;
using System.Collections.Generic;
using System.Linq;
using CSCore;
using CSCore.SoundOut;
using CSCore.CoreAudioAPI;

namespace SynthPiano.AudioBackend
{
	class CsCoreBackend : IAudioBackend, IWaveSource
	{
		public AudioGenerate Read { get; set; }

		private List<MMDevice> devices = new List<MMDevice>();
		private ISoundOut waveOut;

		public bool CanSeek => false;
		public long Length => -1;
		private long shortPos = 0;
		public long Position { get => shortPos * 2; set => shortPos = value / 2; }

		public WaveFormat WaveFormat { get; } = new WaveFormat(Global.Bitrate, Global.Bits, Global.Channel);

		public void Init()
		{
			var dev = GetDevices().First();
			SetDevice(dev.Id);
		}

		private void SetDeviceInternal(MMDevice device)
		{
			waveOut?.Dispose();

			var ws = new WasapiOut(true, AudioClientShareMode.Shared, 1) { Device = device };
			waveOut = ws;
			waveOut.Initialize(this);
			waveOut.Play();
		}

		public void Dispose()
		{
			waveOut?.Dispose();
			waveOut = null;
		}

		int IReadableAudioSource<byte>.Read(byte[] buffer, int offset, int count)
		{
			Read(buffer.AsSpan().Slice(offset, count));
			shortPos += count;
			return count;
		}

		public IEnumerable<DeviceId> GetDevices()
		{
			devices.Clear();

			using (var mmdeviceEnumerator = new MMDeviceEnumerator())
			using (var mmdeviceCollection = mmdeviceEnumerator.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active))
				devices.AddRange(mmdeviceCollection);

			return devices.Select((x, i) => new DeviceId { Id = i, Name = x.FriendlyName });
		}

		public void SetDevice(int id)
		{
			SetDeviceInternal(devices[id]);
		}
	}
}
