using CSCore;
using DynamicData;
using NodeGen.Model;
using NodeGen.Model.Nodes;
using NodeGen.ViewModels.Editors;
using NodeGen.Views;
using ReactiveUI;
using System;
using System.Reactive.Linq;

namespace NodeGen.ViewModels.Nodes
{
	public class WaveSinusNode : NGNodeViewModel
	{
		private readonly WaveSinusSource source = new WaveSinusSource();

		public IntegerValueEditorViewModel PhaseValueEditor { get; } = new IntegerValueEditorViewModel();
		public NGNodeInputViewModel<IFloat?> Phase { get; } = new NGNodeInputViewModel<IFloat?>(PortType.Float) { Name = "Phase", };
		public NGNodeInputViewModel<IFloat?> Frequency { get; } = new NGNodeInputViewModel<IFloat?>(PortType.Float) { Name = "Frequency", };
		public NGNodeOutputViewModel<ISampleSource?> Wave { get; } = new NGNodeOutputViewModel<ISampleSource?>(PortType.Wave) { Name = "Wave", };

		public WaveSinusNode() : base(NodeType.WaveSource)
		{
			Name = "Sinus";

			Phase.Editor = new FloatValueEditorViewModel();
			Phase.ValueChanged.Subscribe(val => source.Phase = val);
			Inputs.Add(Phase);

			Frequency.Editor = new FloatValueEditorViewModel();
			Frequency.ValueChanged.Subscribe(val => source.Frequency = val);
			Inputs.Add(Frequency);

			Wave.Value = Observable.Return(source);
			Outputs.Add(Wave);
		}
	}
}
