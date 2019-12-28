using NodeNetwork.ViewModels;
using ReactiveUI;

namespace NodeGen.ViewModels
{
	public enum PortType
	{
		Wave,
		Int,
		Float,
	}

	public class NGPortViewModel : PortViewModel
	{
		static NGPortViewModel()
		{
			Splat.Locator.CurrentMutable.Register(() => new Views.BasePortView(), typeof(IViewFor<NGPortViewModel>));
		}

		#region PortType
		public PortType PortType
		{
			get => _portType;
			set => this.RaiseAndSetIfChanged(ref _portType, value);
		}
		private PortType _portType;
		#endregion
	}
}
