using Fuse.function;
using VL.Core;

namespace Fuse;

public class InjectToRegionDelegate<T> : PassThroughDelegate<T>, IInjectToRegion
{
	public InjectToRegionDelegate(NodeContext nodeContext, Delegate<T> theDelegate)
		: base(nodeContext, "InjectToRegion", (AbstractShaderNode)theDelegate)
	{
	}
}
