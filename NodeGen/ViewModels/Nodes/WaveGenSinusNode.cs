using DynamicData;
using NodeGen.Model;
using NodeGen.ViewModels.Editors;
using NodeGen.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGen.ViewModels.Nodes
{
	public class WaveGenSinusNode : BaseNodeViewModel
	{
		static WaveGenSinusNode()
		{
			Splat.Locator.CurrentMutable.Register(() => new BaseNodeView(), typeof(IViewFor<WaveGenSinusNode>));
		}

		public IntegerValueEditorViewModel PhaseValueEditor { get; } = new IntegerValueEditorViewModel();
		public NGInputViewModel<IInteger> Phase { get; }
		public NGOutputViewModel<IInteger> SampleData { get; }

		public WaveGenSinusNode() : base(NodeType.WaveGenerator)
		{
			this.Name = "Sinus";

			Phase = new NGInputViewModel<IInteger>(PortType.Undef1)
			{
				Name = "Phase",
			};

			this.Inputs.Add(Phase);

			SampleData = new NGOutputViewModel<IInteger>(PortType.AudioSample)
			{
				Name = "Sample",
			};

			this.Outputs.Add(SampleData);

		}
	}
}
