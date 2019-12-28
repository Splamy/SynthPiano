using NodeGen.ViewModels;
using ReactiveUI;
using System.Windows;


namespace NodeGen.Views
{
	public partial class EmptyView : IViewFor<EmptyViewModel>
	{
		#region ViewModel
		public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
			typeof(EmptyViewModel), typeof(EmptyView), new PropertyMetadata(null));

		public EmptyViewModel ViewModel
		{
			get => (EmptyViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (EmptyViewModel)value;
		}
		#endregion

		public EmptyView()
		{
			InitializeComponent();
		}
	}
}