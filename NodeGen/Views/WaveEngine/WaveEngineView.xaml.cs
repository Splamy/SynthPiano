using DynamicData;
using NodeGen.ViewModels.WaveEngine;
using NodeNetwork.Utilities;
using ReactiveUI;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace NodeGen.Views.WaveEngine
{
	public partial class WaveEngineView : IViewFor<WaveEngineViewModel>
	{
		#region ViewModel
		public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
			typeof(WaveEngineViewModel), typeof(WaveEngineView), new PropertyMetadata(null));

		public WaveEngineViewModel ViewModel
		{
			get => (WaveEngineViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (WaveEngineViewModel)value;
		}
		#endregion

		public WaveEngineView()
		{
			ViewModel = new WaveEngineViewModel();

			InitializeComponent();

			this.BindList(ViewModel, vm => vm.CurvePoints, v => v.knobList.ItemsSource);
			this.BindList(ViewModel, vm => vm.CurveSegs, v => v.segList.ItemsSource);

			ViewModel.CurvePoints.Add(new CurvePointViewModel() { Position = new Point(50, 50), IsSelected = false });
			ViewModel.CurvePoints.Add(new CurvePointViewModel() { Position = new Point(60, 60), IsSelected = false });

			ViewModel.CurveSegs.Add(new CurveSegViewModel() { 
				StartPoint = ViewModel.CurvePoints.Items.First(),
				EndPoint = ViewModel.CurvePoints.Items.Last(),
			});
		}

		private void OnDragKnob(object sender, DragDeltaEventArgs e)
		{
			e.Handled = true;
			foreach (var node in ViewModel.SelectedKnobs.Items)
			{
				node.Position += new Vector(e.HorizontalChange, e.VerticalChange);
			}
		}

		private void OnClickCanvas(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
			ViewModel.ClearSelection();
		}
	}
}
