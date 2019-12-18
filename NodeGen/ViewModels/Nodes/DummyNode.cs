using NodeGen.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public DummyNode() : base(NodeType.Literal)
		{
			this.Name = "Dummy";
		}
	}
}
