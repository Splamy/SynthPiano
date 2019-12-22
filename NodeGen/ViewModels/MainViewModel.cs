using DynamicData;
using NodeGen.ViewModels.Nodes;
using NodeNetwork.Toolkit.NodeList;
using NodeNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGen.ViewModels
{
	public class MainViewModel
	{
		public NetworkViewModel Network { get; } = new NetworkViewModel();
		public NodeListViewModel NodeList { get; } = new NodeListViewModel();

		public MainViewModel()
		{
			var eventNode = new DummyNode { };
			Network.Nodes.Add(eventNode);

			NodeList.AddNodeType(() => new DummyNode());
			NodeList.AddNodeType(() => new WaveGenSinusNode());
			NodeList.AddNodeType(() => new DebugOutputNode());
		}
	}
}
