using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.Views;
using ReactiveUI;

namespace NodeGen.ViewModels
{
	public class NGNodeListInputViewModel<T> : ValueListNodeInputViewModel<T>
	{
		static NGNodeListInputViewModel()
		{
			Splat.Locator.CurrentMutable.Register(() => new NodeInputView(), typeof(IViewFor<NGNodeListInputViewModel<T>>));
		}

		public NGNodeListInputViewModel(PortType type)
		{
			Port = new NGPortViewModel { PortType = type };
		}
	}
}
