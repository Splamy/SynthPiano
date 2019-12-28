using CSCore;
using DynamicData;
using NodeGen.Audio;
using NodeGen.Model;
using NodeGen.Views;
using ReactiveUI;
using System;

namespace NodeGen.ViewModels.Nodes
{
	public class OutputNode : NGNodeViewModel
	{
		public NGNodeInputViewModel<ISampleSource> WaveIn { get; } = new NGNodeInputViewModel<ISampleSource>(PortType.Wave) { };

		public OutputNode(Engine engine) : base(NodeType.WaveSink)
		{
			Name = "Output";

			WaveIn.ValueChanged.Subscribe(wav => engine.Wave = wav);
			Inputs.Add(WaveIn);

			//DebugView.Name = "Output";
			//DebugView.Editor = new DebugOutputEditorViewModel();
			//DebugView.Port.IsVisible = false;
			//Outputs.Add(DebugView);
		}
	}
}
