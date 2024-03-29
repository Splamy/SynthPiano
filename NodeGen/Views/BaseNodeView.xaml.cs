﻿using NodeGen.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Media;

namespace NodeGen.Views
{
	public partial class BaseNodeView : IViewFor<NGNodeViewModel>
	{
		#region ViewModel
		public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel),
			typeof(NGNodeViewModel), typeof(BaseNodeView), new PropertyMetadata(null));

		public NGNodeViewModel ViewModel
		{
			get => (NGNodeViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}

		object IViewFor.ViewModel
		{
			get => ViewModel;
			set => ViewModel = (NGNodeViewModel)value;
		}
		#endregion

		public BaseNodeView()
		{
			InitializeComponent();

			this.WhenActivated(d =>
			{
				NodeView.ViewModel = ViewModel;
				Disposable.Create(() => NodeView.ViewModel = null).DisposeWith(d);

				this.OneWayBind(ViewModel, vm => vm.NodeType, v => v.NodeView.Background, ConvertNodeTypeToBrush).DisposeWith(d);
			});
		}

		public static Brush ConvertNodeTypeToBrush(NodeType type)
		{
			return type switch
			{
				NodeType.WaveSource => new SolidColorBrush(Color.FromRgb(0x00, 0x60, 0x0f)),
				NodeType.WaveProcessor => new SolidColorBrush(Color.FromRgb(0x9b, 0x00, 0x00)),
				NodeType.WaveSink => new SolidColorBrush(Color.FromRgb(0x49, 0x49, 0x49)),
				NodeType.Undef3 => new SolidColorBrush(Color.FromRgb(0x00, 0x39, 0xcb)),
				_ => throw new Exception("Unsupported node type"),
			};
		}
	}
}