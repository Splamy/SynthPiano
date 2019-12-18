using NodeNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGen.ViewModels
{
	public class BaseNodeViewModel : NodeViewModel
	{
		public NodeType NodeType { get; }

		public BaseNodeViewModel(NodeType type)
		{
			NodeType = type;
		}
	}

	public enum NodeType
	{
		EventNode,
		Function,
		FlowControl,
		Literal
	}
}
