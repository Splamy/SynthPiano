using NodeGen.ViewModels.Nodes;
using NodeNetwork.Views;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Media;


namespace NodeGen.Views
{
	public partial class DebugOutputView : IViewFor<DebugOutputNode>
	{
		#region ViewModel
		public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
			typeof(DebugOutputNode), typeof(DebugOutputView), new PropertyMetadata(null));

		public DebugOutputNode ViewModel
		{
			get => (DebugOutputNode)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (DebugOutputNode)value;
		}
		#endregion

		public DebugOutputView()
		{
			InitializeComponent();
		}
	}
}