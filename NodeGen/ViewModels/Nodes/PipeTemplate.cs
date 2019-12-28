using CSCore;
using DynamicData;
using System;

namespace NodeGen.ViewModels.Nodes
{
	public abstract class PipeTemplate : NGNodeViewModel
	{
		public NGNodeInputViewModel<ISampleSource?> WaveIn { get; } = new NGNodeInputViewModel<ISampleSource?>(PortType.Wave) { Name = "WaveIn" };
		public NGNodeOutputViewModel<ISampleSource?> WaveOut { get; } = new NGNodeOutputViewModel<ISampleSource?>(PortType.Wave) { Name = "WaveOut" };

		public PipeTemplate() : base(NodeType.WaveProcessor)
		{
			Inputs.Add(WaveIn);
			Outputs.Add(WaveOut);
		}
	}
}
