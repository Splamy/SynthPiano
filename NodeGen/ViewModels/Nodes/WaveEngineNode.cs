using CSCore;
using DynamicData;
using NodeGen.ViewModels.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeGen.ViewModels.Nodes
{
	public class WaveEngineNode : NGNodeViewModel
	{
		public NGNodeOutputViewModel<ISampleSource?> Wave { get; } = new NGNodeOutputViewModel<ISampleSource?>(PortType.Wave) { Name = "Wave", };

		public WaveEngineNode() : base(NodeType.WaveSource)
		{
			Name = "Wave Engine";

			Wave.Editor = new WaveEngineEditorViewModel();
			Outputs.Add(Wave);
		}
	}
}
