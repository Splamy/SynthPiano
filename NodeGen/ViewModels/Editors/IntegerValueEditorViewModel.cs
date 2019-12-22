using NodeGen.Model;
using NodeGen.Views.Editors;
using NodeNetwork.Toolkit.ValueNode;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGen.ViewModels.Editors
{
    public class IntegerValueEditorViewModel : ValueEditorViewModel<int>
    {
        static IntegerValueEditorViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new IntegerValueEditorView(), typeof(IViewFor<IntegerValueEditorViewModel>));
        }

        public IntegerValueEditorViewModel()
        {
            Value = 0;
        }
    }
}
