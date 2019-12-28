using DynamicData;
using NodeGen.Audio;
using NodeGen.ViewModels.Nodes;
using NodeGen.ViewModels.WaveEngine;
using NodeGen.Views;
using NodeNetwork.Toolkit.NodeList;
using NodeNetwork.ViewModels;
using ReactiveUI;
using System;
using System.Linq;

namespace NodeGen.ViewModels
{
	public class MainViewModel
	{
		static MainViewModel()
		{
			var vmNodeTypes = typeof(NGNodeViewModel).Assembly.GetTypes()
				.Where(t => typeof(NGNodeViewModel).IsAssignableFrom(t))
				.Where(t => !t.IsAbstract)
				.Where(t => t != typeof(WaveEngineViewModel));
			var splat = Splat.Locator.CurrentMutable;

			foreach (var nodeType in vmNodeTypes)
			{
				var nodeViewType = typeof(IViewFor<>).MakeGenericType(nodeType);
				splat.Register(() => new BaseNodeView(), nodeViewType);
			}
		}

		public NetworkViewModel Network { get; } = new NetworkViewModel();
		public NodeListViewModel NodeList { get; } = new NodeListViewModel();
		public Engine Engine { get; } = new Engine();

		public MainViewModel()
		{
			var eventNode = new OutputNode(Engine) { CanBeRemovedByUser = false };
			Network.Nodes.Add(eventNode);

			NodeList.AddNodeType(() => new WaveSinusNode());
			NodeList.AddNodeType(() => new WaveEngineNode());
			NodeList.AddNodeType(() => new MergeNode());
			NodeList.AddNodeType(() => new VolumeNode());

			Engine.AutoPlay();
		}
	}
}
