using NodeGen.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NodeGen.Views
{
	/// <summary>
	/// Interaction logic for BasePortView.xaml
	/// </summary>
	public partial class BasePortView : IViewFor<BasePortViewModel>
	{
		#region ViewModel
		public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
			typeof(BasePortViewModel), typeof(BasePortView), new PropertyMetadata(null));

		public BasePortViewModel ViewModel
		{
			get => (BasePortViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (BasePortViewModel)value;
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
				PortType.Undef1 => (ControlTemplate)Resources[ExecutionPortTemplateKey],
				PortType.AudioSample => (ControlTemplate)Resources[IntegerPortTemplateKey],
				PortType.Undef2 => (ControlTemplate)Resources[StringPortTemplateKey],
				_ => throw new Exception("Unsupported port type"),
			};
		}
	}
}
