using NodeGen.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Media;

namespace NodeGen.Views
{
	public partial class BaseNodeView : IViewFor<BaseNodeViewModel>
	{
		#region ViewModel
		public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
			typeof(BaseNodeViewModel), typeof(BaseNodeView), new PropertyMetadata(null));

		public BaseNodeViewModel ViewModel
		{
			get => (BaseNodeViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (BaseNodeViewModel)value;
		}
		#endregion

		public BaseNodeView()
		{
			InitializeComponent();

			this.WhenActivated(d =>
			{
				NodeView.ViewModel = this.ViewModel;
				Disposable.Create(() => NodeView.ViewModel = null).DisposeWith(d);

				this.OneWayBind(ViewModel, vm => vm.NodeType, v => v.NodeView.Background, ConvertNodeTypeToBrush).DisposeWith(d);
			});
		}

		private Brush ConvertNodeTypeToBrush(NodeType type)
		{
			return type switch
			{
				NodeType.Undef1 => new SolidColorBrush(Color.FromRgb(0x9b, 0x00, 0x00)),
				NodeType.Undef2 => new SolidColorBrush(Color.FromRgb(0x49, 0x49, 0x49)),
				NodeType.Undef3 => new SolidColorBrush(Color.FromRgb(0x00, 0x39, 0xcb)),
				NodeType.WaveGenerator => new SolidColorBrush(Color.FromRgb(0x00, 0x60, 0x0f)),
				_ => throw new Exception("Unsupported node type"),
			};
		}
	}
}