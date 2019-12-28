using NodeGen.Model;
using NodeGen.Views.Editors;
using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;

namespace NodeGen.ViewModels.Editors
{
	public class IntegerValueEditorViewModel : ValueEditorViewModel<IInteger>
	{
		static IntegerValueEditorViewModel()
		{
			Splat.Locator.CurrentMutable.Register(() => new IntegerValueEditorView(), typeof(IViewFor<IntegerValueEditorViewModel>));
		}

		public IntegerValueEditorViewModel()
		{
			Value ??= new DataInteger();
		}
	}
}
