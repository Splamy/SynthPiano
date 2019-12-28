using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;

namespace NodeGen.ViewModels
{
	public class NGNodeInputViewModel<T> : ValueNodeInputViewModel<T>
	{
		static NGNodeInputViewModel()
		{
			Splat.Locator.CurrentMutable.Register(() => new NodeInputView(), typeof(IViewFor<NGNodeInputViewModel<T>>));
		}

		public NGNodeInputViewModel(PortType type)
		{
			Port = new NGPortViewModel { PortType = type };
			// if (type == PortType.Wave) PortPosition = PortPosition.Right;
		}
	}
}
