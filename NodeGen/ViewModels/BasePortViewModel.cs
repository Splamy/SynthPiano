using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeNetwork.ViewModels;
using ReactiveUI;

namespace NodeGen.ViewModels
{
	public enum PortType
	{
		AudioSample,
		Undef1,
		Undef2,
	}

	public class BasePortViewModel : PortViewModel
	{
		static BasePortViewModel()
		{
			Splat.Locator.CurrentMutable.Register(() => new Views.BasePortView(), typeof(IViewFor<BasePortViewModel>));
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
