using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;
using System.Runtime.InteropServices;

namespace SynthPiano.AudioBackend
{
	class BassBackend : IAudioBackend
	{
		private const BASSFlag sampleTypeFlag = BASSFlag.BASS_DEFAULT;
		private int bassStream;
		private STREAMPROC soundCreator;

		public Func<byte[], int, int, int> Read { get; set; }

		public void Init()
		{
			// Todo read global values and set values here correspondingly
			Bass.BASS_Init(-1, Global.Bitrate, BASSInit.BASS_DEVICE_DEFAULT | BASSInit.BASS_DEVICE_LATENCY, IntPtr.Zero);
			var info = Bass.BASS_GetInfo();
			Console.WriteLine($@"Minimal buffer size: {info.minbuf}, letency: {info.latency}");
			Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_BUFFER, info.minbuf + 6);
			Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATEPERIOD, 5);
			Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_NET_BUFFER, info.minbuf / 2);

			var pointer = new IntPtr();
			soundCreator = GetSoundBytes;
			bassStream = Bass.BASS_StreamCreate(Global.Bitrate, 1, sampleTypeFlag, soundCreator, pointer);
			if (bassStream == 0) throw new Exception("Stream creation failed.");

			// play
			Bass.BASS_ChannelPlay(bassStream, false);
		}

		private int GetSoundBytes(int handle, IntPtr buffer, int length, IntPtr user)
		{
			byte[] dataArray = new byte[length];
			int readBytes = Read(dataArray, 0, length);
			Marshal.Copy(dataArray, 0, buffer, readBytes);
			return readBytes;
		}

		public void Dispose()
		{
			Bass.BASS_StreamFree(bassStream);
			Bass.BASS_Free();
		}

		public IEnumerable<DeviceId> GetDevices()
		{
			return Enumerable.Empty<DeviceId>();
		}

		public void SetDevice(int id)
		{
			throw new NotImplementedException();
		}
	}
}
