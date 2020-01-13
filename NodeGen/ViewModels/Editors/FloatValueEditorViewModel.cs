using NodeGen.Model;
using NodeGen.Views.Editors;
using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;

namespace NodeGen.ViewModels.Editors
{
	public class FloatValueEditorViewModel : ValueEditorViewModel<float?>
	{
		static FloatValueEditorViewModel()
		{
			Splat.Locator.CurrentMutable.Register(() => new FloatValueEditorView(), typeof(IViewFor<FloatValueEditorViewModel>));
		}

		public FloatValueEditorViewModel(float def = 0)
		{
			Value = def;
		}

		private double knobRotation;
		public double KnobRotation
		{
			get => knobRotation;
			set => this.RaiseAndSetIfChanged(ref knobRotation, value);
		}
	}
}
