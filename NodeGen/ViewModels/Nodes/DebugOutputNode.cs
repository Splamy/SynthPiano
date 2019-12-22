using DynamicData;
using NodeGen.ViewModels.Editors;
using NodeGen.Views;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGen.ViewModels.Nodes
{
	public class DebugOutputNode : BaseNodeViewModel
	{
		static DebugOutputNode()
		{
			Splat.Locator.CurrentMutable.Register(() => new BaseNodeView(), typeof(IViewFor<DebugOutputNode>));
		}

		public ValueNodeInputViewModel<int?> DataInput { get; }

		public DebugOutputNode() : base(NodeType.Undef1)
		{
			Name = "Debug Output";

			DataInput = new ValueNodeInputViewModel<int?>
			{
				Name = "Value",
				Editor = new IntegerValueEditorViewModel()
			};
			Inputs.Add(DataInput);
		}
	}
}
