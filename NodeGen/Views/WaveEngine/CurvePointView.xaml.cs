using NodeGen.ViewModels.WaveEngine;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NodeGen.Views.WaveEngine
{
	/// <summary>
	/// Interaction logic for CurvePointView.xaml
	/// </summary>
	public partial class CurvePointView : IViewFor<CurvePointViewModel>
	{
		#region ViewModel
		public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
			typeof(CurvePointViewModel), typeof(CurvePointView), new PropertyMetadata(null));

		public CurvePointViewModel ViewModel
		{
			get => (CurvePointViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (CurvePointViewModel)value;
		}
		#endregion

		public CurvePointView()
		{
			InitializeComponent();

			this.WhenActivated(d =>
			{
				this.OneWayBind(ViewModel, vm => vm.IsSelected, v => v.Knob.Fill, EnabledColor).DisposeWith(d);
			});

			SetupEvents();
		}

		public static Brush EnabledColor(bool enabled)
		{
			return enabled
				? new SolidColorBrush(Colors.MistyRose)
				: new SolidColorBrush(Colors.DarkRed);
		}

		private void OnDragLHKnob(object sender, DragDeltaEventArgs e)
		{
			e.Handled = true;
			ViewModel.HandleLeft += new Vector(e.HorizontalChange, e.VerticalChange);
		}

		private void OnDragRHKnob(object sender, DragDeltaEventArgs e)
		{
			e.Handled = true;
			ViewModel.HandleRight += new Vector(e.HorizontalChange, e.VerticalChange);
		}

		private void SetupEvents()
		{
			this.MouseLeftButtonDown += (sender, args) =>
			{
				this.Focus();

				if (ViewModel == null)
				{
					return;
				}

				if (ViewModel.IsSelected)
				{
					return;
				}

				if (ViewModel.Parent != null && !Keyboard.IsKeyDown(Key.LeftCtrl) && !Keyboard.IsKeyDown(Key.RightCtrl))
				{
					ViewModel.Parent.ClearSelection();
				}

				ViewModel.IsSelected = true;
			};
		}
	}

	[ValueConversion(typeof(bool), typeof(Visibility))]
	public sealed class BoolToVisibilityConverter : IValueConverter
	{
		public Visibility TrueValue { get; set; }
		public Visibility FalseValue { get; set; }

		public BoolToVisibilityConverter()
		{
			// set defaults
			TrueValue = Visibility.Visible;
			FalseValue = Visibility.Hidden;
		}

		public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool))
				return null;
			return (bool)value ? TrueValue : FalseValue;
		}

		public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (Equals(value, TrueValue))
				return true;
			if (Equals(value, FalseValue))
				return false;
			return null;
		}
	}
}
