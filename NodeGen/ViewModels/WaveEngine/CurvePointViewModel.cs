using NodeGen.Views.WaveEngine;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeGen.ViewModels.WaveEngine
{
	public class CurvePointViewModel : ReactiveObject
	{
		static CurvePointViewModel()
		{
			Splat.Locator.CurrentMutable.Register(() => new CurvePointView(), typeof(IViewFor<CurvePointViewModel>));
		}

		private WaveEngineViewModel? parent;
		public WaveEngineViewModel? Parent
		{
			get => parent;
			set => this.RaiseAndSetIfChanged(ref parent, value);
		}

		private Point position;
		public Point Position
		{
			get => position;
			set => this.RaiseAndSetIfChanged(ref position, value);
		}

		private bool isSelected;
		public bool IsSelected
		{
			get => isSelected;
			set => this.RaiseAndSetIfChanged(ref isSelected, value);
		}

		private Point handleLeft;
		public Point HandleLeft
		{
			get => handleLeft;
			set => this.RaiseAndSetIfChanged(ref handleLeft, value);
		}
		public Point HandleLeftView => handleleftView.Value;
		private ObservableAsPropertyHelper<Point> handleleftView;

		private Point handleRight;
		public Point HandleRight
		{
			get => handleRight;
			set => this.RaiseAndSetIfChanged(ref handleRight, value);
		}
		public Point HandleRightView => handleRightView.Value;
		private ObservableAsPropertyHelper<Point> handleRightView;

		public CurvePointViewModel()
		{
			this.WhenAnyValue(v => v.HandleRight, v => v.Position, (h, p) => new Point(h.X + p.X, h.Y + p.Y))
				.ToProperty(this, vm => vm.HandleRightView, out handleRightView);
			this.WhenAnyValue(v => v.HandleLeft, v => v.Position, (h, p) => new Point(h.X + p.X, h.Y + p.Y))
				.ToProperty(this, vm => vm.HandleLeftView, out handleleftView);
		}
	}
}
