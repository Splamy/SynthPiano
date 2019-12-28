using NodeGen.ViewModels;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NodeGen.Views
{
	/// <summary>
	/// Interaction logic for BasePortView.xaml
	/// </summary>
	public partial class BasePortView : IViewFor<NGPortViewModel>
	{
		#region ViewModel
		public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
			typeof(NGPortViewModel), typeof(BasePortView), new PropertyMetadata(null));

		public NGPortViewModel ViewModel
		{
			get => (NGPortViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (NGPortViewModel)value;
		}
		#endregion

		#region Template Resource Keys
		public const string ExecutionPortTemplateKey = "ExecutionPortTemplate";
		public const string IntegerPortTemplateKey = "IntegerPortTemplate";
		public const string StringPortTemplateKey = "StringPortTemplate";
		#endregion

		public BasePortView()
		{
			InitializeComponent();

			this.WhenActivated(d =>
			{
				this.WhenAnyValue(v => v.ViewModel).BindTo(this, v => v.PortView.ViewModel).DisposeWith(d);

				this.OneWayBind(ViewModel, vm => vm.PortType, v => v.PortView.Template, GetTemplateFromPortType)
					.DisposeWith(d);

				this.OneWayBind(ViewModel, vm => vm.IsMirrored, v => v.PortView.RenderTransform,
					isMirrored => new ScaleTransform(isMirrored ? -1.0 : 1.0, 1.0))
					.DisposeWith(d);
			});
		}

		public ControlTemplate GetTemplateFromPortType(PortType type)
		{
			return type switch
			{
				PortType.Wave => (ControlTemplate)Resources[ExecutionPortTemplateKey],
				PortType.Int => (ControlTemplate)Resources[IntegerPortTemplateKey],
				PortType.Float => (ControlTemplate)Resources[StringPortTemplateKey],
				_ => throw new Exception("Unsupported port type"),
			};
		}
	}
}
