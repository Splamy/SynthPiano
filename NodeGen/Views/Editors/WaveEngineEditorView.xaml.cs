using NodeGen.Model;
using NodeGen.ViewModels.Editors;
using NodeGen.ViewModels.Nodes;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows;

namespace NodeGen.Views.Editors
{
	public partial class WaveEngineEditorView : IViewFor<WaveEngineEditorViewModel>
	{
		#region ViewModel
		public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
			typeof(WaveEngineEditorViewModel), typeof(WaveEngineEditorView), new PropertyMetadata(null));

		public WaveEngineEditorViewModel ViewModel
		{
			get => (WaveEngineEditorViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (WaveEngineEditorViewModel)value;
		}
		#endregion

		public WaveEngineEditorView()
		{
			InitializeComponent();

		}
	}
}