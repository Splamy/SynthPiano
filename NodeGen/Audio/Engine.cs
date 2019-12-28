using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.SoundOut;
using NodeGen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NodeGen.Audio
{
	public class Engine : IDisposable
	{
		public ISampleSource? Wave { get => fillSource.Wave; set => fillSource.Wave = value; }
		private readonly FillSource fillSource = new FillSource();
		private ISoundOut? waveOut = null;

		public void AutoPlay()
		{
			using var mmdeviceEnumerator = new MMDeviceEnumerator();
			using var mmdeviceCollection = mmdeviceEnumerator.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active);
			var mmdeviceList = mmdeviceCollection.ToList();
			var device = mmdeviceList.First();
			SelectDevice(device);
		}

		public void SelectDevice(MMDevice device)
		{
			CloseDevice();

			var ws = new WasapiOut(true, AudioClientShareMode.Shared, 1) { Device = device };

			waveOut = ws;
			waveOut.Initialize(fillSource);
			waveOut.Play();
		}

		public void CloseDevice()
		{
			waveOut?.Dispose();
		}

		public void Dispose()
		{
			CloseDevice();
		}
	}
}
