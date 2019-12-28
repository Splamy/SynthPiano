using NodeGen.ViewModels.WaveEngine;
using ReactiveUI;
using System.Windows;
using System.Windows.Media;

namespace NodeGen.Views.WaveEngine
{
	/// <summary>
	/// Interaction logic for WaveEngineView.xaml
	/// </summary>
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
			InitializeComponent();
		}
	}
}
