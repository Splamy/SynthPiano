using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;

namespace NodeGen.ViewModels
{
	public class NGNodeOutputViewModel<T> : ValueNodeOutputViewModel<T>
	{
		static NGNodeOutputViewModel()
		{
			Splat.Locator.CurrentMutable.Register(() => new NodeOutputView(), typeof(IViewFor<NGNodeOutputViewModel<T>>));
		}

		public NGNodeOutputViewModel(PortType type)
		{
			Port = new NGPortViewModel { PortType = type };
			//if (type == PortType.Wave) PortPosition = PortPosition.Left;
		}
	}
}
