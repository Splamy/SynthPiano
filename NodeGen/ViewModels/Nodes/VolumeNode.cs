using CSCore.Streams;
using DynamicData;
using NodeGen.Model;
using NodeGen.ViewModels.Editors;
using System;
using System.Reactive.Linq;

namespace NodeGen.ViewModels.Nodes
{
	public class VolumeNode : PipeTemplate
	{
		private readonly VolumeSource volumeSource = new VolumeSource(EmptyWaveSource.Instance);

		public NGNodeInputViewModel<IFloat?> Volume { get; } = new NGNodeInputViewModel<IFloat?>(PortType.Float) { Name = "Volume", };

		public VolumeNode()
		{
			Name = "Volume";

			Volume.Editor = new FloatValueEditorViewModel(1);
			Volume.ValueChanged.Subscribe(v => volumeSource.Volume = Math.Max(Math.Min(v?.Value ?? 0, 1), 0));
			Inputs.Add(Volume);

			WaveIn.ValueChanged.Subscribe(ss => volumeSource.BaseSource = ss ?? EmptyWaveSource.Instance);
			WaveOut.Value = Observable.Return(volumeSource);
		}
	}
}
