using VL.Core;

namespace Fuse;

public class CrossLink<T> : PassThroughNode<T>
{
	public CrossLink(NodeContext nodeContext)
		: base(nodeContext, "CrossLink")
	{
	}
}
