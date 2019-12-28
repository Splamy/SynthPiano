using NodeGen.Model;
using NodeGen.Views.Editors;
using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;

namespace NodeGen.ViewModels.Editors
{
	public class WaveEngineEditorViewModel : ValueEditorViewModel<object?>
	{
		static WaveEngineEditorViewModel()
		{
			Splat.Locator.CurrentMutable.Register(() => new WaveEngineEditorView(), typeof(IViewFor<WaveEngineEditorViewModel>));
		}

		public WaveEngineEditorViewModel()
		{
			Value = null;
		}
	}
}
