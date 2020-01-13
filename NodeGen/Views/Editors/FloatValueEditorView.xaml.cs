using NodeGen.Model;
using NodeGen.ViewModels.Editors;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using static System.Math;

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

		private RotateTransform KnobTransform;

		public FloatValueEditorView()
		{
			InitializeComponent();

			this.WhenActivated(d =>
			{
				this.Bind(ViewModel, vm => vm.Value, v => v.UpDown.Value);

				var child = VisualTreeHelper.GetChild(TurnKnob, 0);
				var control = (Canvas)child;
				KnobTransform = ((RotateTransform)control.RenderTransform);
			});
		}

		private double valueAtDragStart;
		private double knobRotAtStart;

		private double KnobRotation
		{
			get => KnobTransform.Angle;
			set => KnobTransform.Angle = value;
		}

		private void OnDragKnob(object sender, DragDeltaEventArgs e)
		{
			e.Handled = true;

			var gran = Max(100, 100 + (2 * e.VerticalChange));
			var drag = e.HorizontalChange;

			const double minScale = -2;
			var minDelta = Pow(10, minScale);

			var diff = drag - valueAtDragStart;
			valueAtDragStart = drag;

			var vmVal = ViewModel.Value ?? 0;
			if (vmVal == 0)
			{
				ViewModel.Value = (float)(minDelta * Sign(drag));
			}
			else if (Abs(vmVal) < minDelta && Sign(vmVal) != Sign(drag))
			{
				ViewModel.Value = 0;
				KnobRotation = 0;
			}
			else
			{
				var scale = Max(Floor(Log10(Abs(vmVal))), minScale);
				var sc10 = Pow(10, scale);
				var add = ((sc10 * 9) / gran) * Sign(diff);
				var final = RoundEffective(vmVal + add, scale - 2);
				ViewModel.Value = (float)final;
				KnobRotation = 360 * ((final - sc10) / (sc10 * 9));
			}
		}

		static double RoundEffective(double val, double exp10)
		{
			var moved = val * Math.Pow(10, -exp10);
			var rounded = Math.Round(moved, 0);
			var bmoved = rounded * Math.Pow(10, exp10);
			return bmoved;
		}

		private void OnDragStartKnob(object sender, DragStartedEventArgs e)
		{
			valueAtDragStart = 0;
			knobRotAtStart = KnobRotation;
		}
	}
}