using NodeGen.Views.WaveEngine;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace NodeGen.ViewModels.WaveEngine
{
	public class CurveSegViewModel : ReactiveObject
	{
		static CurveSegViewModel()
		{
			Splat.Locator.CurrentMutable.Register(() => new CurveSegView(), typeof(IViewFor<CurveSegViewModel>));
		}

		private CurvePointViewModel? startPoint;
		public CurvePointViewModel? StartPoint
		{
			get => startPoint;
			set => this.RaiseAndSetIfChanged(ref startPoint, value);
		}

		private CurvePointViewModel? endPoint;
		public CurvePointViewModel? EndPoint
		{
			get => endPoint;
			set => this.RaiseAndSetIfChanged(ref endPoint, value);
		}
	}
}
