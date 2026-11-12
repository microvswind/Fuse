using VL.Core;

namespace Fuse;

public class InjectToRegion<T> : PassThroughNode<T>, IInjectToRegion
{
	public InjectToRegion(NodeContext nodeContext, ShaderNode<T> theNode)
		: base(nodeContext, (AbstractShaderNode)theNode, "InjectToRegion")
	{
	}
}
