using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGen.ViewModels
{
	public class NGInputViewModel<T> : ValueNodeInputViewModel<T>
    {
        static NGInputViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeInputView(), typeof(IViewFor<NGInputViewModel<T>>));
        }

        public NGInputViewModel(PortType type)
        {
            this.Port = new BasePortViewModel { PortType = type };
        }
    }
}
