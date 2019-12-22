using DynamicData;
using NodeGen.Model;
using NodeGen.ViewModels.Editors;
using NodeGen.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGen.ViewModels.Nodes
{
	class DummyNode : BaseNodeViewModel
	{
		static DummyNode()
		{
			Splat.Locator.CurrentMutable.Register(() => new BaseNodeView(), typeof(IViewFor<DummyNode>));
		}

		public IntegerValueEditorViewModel InputValueEditor { get; } = new IntegerValueEditorViewModel();

		public IntegerValueEditorViewModel OutputValueEditor { get; } = new IntegerValueEditorViewModel();

		public NGInputViewModel<IInteger> Input { get; }

		public NGOutputViewModel<IInteger> Output { get; }

		public DummyNode() : base(NodeType.WaveGenerator)
		{
			this.Name = "Dummy";

			Output = new NGOutputViewModel<IInteger>(PortType.AudioSample)
			{
				Editor = OutputValueEditor,
				Value = OutputValueEditor.ValueChanged.Select(v => new AudioSample { Value = v })
			};

			this.Outputs.Add(Output);

			Input = new NGInputViewModel<IInteger>(PortType.AudioSample)
			{
				
			};

			this.Inputs.Add(Input);
		}
	}
}
