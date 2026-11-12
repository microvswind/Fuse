using System.Collections.Generic;
using Fuse.compute;
using VL.Core;

namespace Fuse;

public class Do<T> : PassThroughNode<T>
{
	public Do(NodeContext nodeContext, ShaderNode<T> theIn, ShaderNode<GpuVoid> theCommand)
		: base(nodeContext, (AbstractShaderNode)theIn, "Do")
	{
		SetInputs(new List<AbstractShaderNode> { theCommand, Input });
	}
}
