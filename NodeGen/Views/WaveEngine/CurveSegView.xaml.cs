using NodeGen.ViewModels.WaveEngine;
using ReactiveUI;
using System.Windows;

namespace NodeGen.Views.WaveEngine
{
	public partial class CurveSegView : IViewFor<CurveSegViewModel>
	{
		#region ViewModel
		public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
			typeof(CurveSegViewModel), typeof(CurveSegView), new PropertyMetadata(null));

		public CurveSegViewModel ViewModel
		{
			get => (CurveSegViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (CurveSegViewModel)value;
		}
		#endregion

		public CurveSegView()
		{
			InitializeComponent();
		}
	}
}
