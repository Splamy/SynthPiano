using CSCore;
using DynamicData;
using NodeGen.Model.Nodes;
using System;
using System.Reactive.Linq;

namespace NodeGen.ViewModels.Nodes
{
	public class MergeNode : NGNodeViewModel
	{
		public WaveMerge waveMerge = new WaveMerge();

		public NGNodeListInputViewModel<ISampleSource> WavesIn { get; } = new NGNodeListInputViewModel<ISampleSource>(PortType.Wave) { };
		public NGNodeOutputViewModel<ISampleSource> WaveOut { get; } = new NGNodeOutputViewModel<ISampleSource>(PortType.Wave) { };

		public MergeNode() : base(NodeType.WaveProcessor)
		{
			Name = "Merge";

			WavesIn.Values.Connect().Subscribe(_ => waveMerge.Waves = WavesIn.Values.Items);
			Inputs.Add(WavesIn);

			WaveOut.Value = Observable.Return(waveMerge);
			Outputs.Add(WaveOut);
		}
	}
}
