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
	}
}
