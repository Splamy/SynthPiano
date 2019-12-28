using NodeGen.Model;
using NodeGen.ViewModels.Editors;
using NodeGen.ViewModels.Nodes;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows;

namespace NodeGen.Views.Editors
{
	public partial class FloatValueEditorView : IViewFor<FloatValueEditorViewModel>
	{
		#region ViewModel
		public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
			typeof(FloatValueEditorViewModel), typeof(FloatValueEditorView), new PropertyMetadata(null));

		public FloatValueEditorViewModel ViewModel
		{
			get => (FloatValueEditorViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (FloatValueEditorViewModel)value;
		}
		#endregion

		public FloatValueEditorView()
		{
			InitializeComponent();

			this.WhenActivated(d => {
				this.Bind(ViewModel, vm => vm.Value, v => v.UpDown.Value, x => x.Value, x => new DataFloat() { Value = x ?? 0 });
			});
		}
	}
}