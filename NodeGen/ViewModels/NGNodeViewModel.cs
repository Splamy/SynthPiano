using NodeNetwork.ViewModels;

namespace NodeGen.ViewModels
{
	public class NGNodeViewModel : NodeViewModel
	{
		public NodeType NodeType { get; }

		public NGNodeViewModel(NodeType type)
		{
			NodeType = type;
		}
	}

	public enum NodeType
	{
		WaveSource,
		WaveProcessor,
		WaveSink,
		Undef3,
	}
}
