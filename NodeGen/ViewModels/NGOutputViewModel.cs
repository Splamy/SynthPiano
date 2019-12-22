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
	public class NGOutputViewModel<T> : ValueNodeOutputViewModel<T>
	{
        static NGOutputViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeOutputView(), typeof(IViewFor<NGOutputViewModel<T>>));
        }

        public NGOutputViewModel(PortType type)
        {
            this.Port = new BasePortViewModel { PortType = type };
        }
    }
}
