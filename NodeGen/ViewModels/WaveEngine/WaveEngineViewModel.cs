using NodeGen.Views;
using NodeGen.Views.WaveEngine;
using ReactiveUI;

namespace NodeGen.ViewModels.WaveEngine
{
	public class WaveEngineViewModel
	{
		static WaveEngineViewModel()
		{
			Splat.Locator.CurrentMutable.Register(() => new WaveEngineView(), typeof(IViewFor<WaveEngineViewModel>));
		}
	}
}
