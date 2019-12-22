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

		public NGInputViewModel<int?> Input { get; }

		public NGOutputViewModel<int?> Output { get; }

		public DummyNode() : base(NodeType.WaveGenerator)
		{
			this.Name = "Dummy";

			Input = new NGInputViewModel<int?>(PortType.AudioSample)
			{
				Editor = InputValueEditor,
			};

			this.Inputs.Add(Input);

			Output = new NGOutputViewModel<int?>(PortType.AudioSample)
			{
				Editor = OutputValueEditor,
				Value = OutputValueEditor.ValueChanged
			};

			this.Outputs.Add(Output);
		}
	}
}
