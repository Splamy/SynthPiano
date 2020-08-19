using DynamicData;
using NodeGen.Views;
using NodeGen.Views.WaveEngine;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace NodeGen.ViewModels.WaveEngine
{
	public class WaveEngineViewModel : ReactiveObject
	{
		static WaveEngineViewModel()
		{
			Splat.Locator.CurrentMutable.Register(() => new WaveEngineView(), typeof(IViewFor<WaveEngineViewModel>));
		}

		public ISourceList<CurvePointViewModel> CurvePoints { get; set; } = new SourceList<CurvePointViewModel>();
		public ISourceList<CurveSegViewModel> CurveSegs { get; set; } = new SourceList<CurveSegViewModel>();

		public IObservableList<CurvePointViewModel> SelectedKnobs { get; set; }

		public WaveEngineViewModel()
		{
			// Setup parent relationship in nodes.
			CurvePoints.Connect().ActOnEveryObject(
				addedNode => addedNode.Parent = this,
				removedNode => removedNode.Parent = null
			);

			SelectedKnobs = CurvePoints.Connect()
				.AutoRefresh(node => node.IsSelected)
				.Filter(node => node.IsSelected)
				.AsObservableList();


		}

		public void ClearSelection()
		{
			foreach (var node in SelectedKnobs.Items)
			{
				node.IsSelected = false;
			}
		}
	}
}
