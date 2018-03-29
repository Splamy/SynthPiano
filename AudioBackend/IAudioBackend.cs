using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynthPiano.AudioBackend
{
	interface IAudioBackend : IDisposable
	{
		void Init();
		Func<byte[], int, int, int> Read { get; set; }

		IEnumerable<DeviceId> GetDevices();
		void SetDevice(int id);
	}

	class DeviceId
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
