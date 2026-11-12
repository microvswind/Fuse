using System.Collections.Generic;
using Fuse.function;
using VL.Core;

namespace Fuse.Function;

public class Delegate0In0OutInvoke<TOut> : Invoke<TOut>
{
	public Delegate0In0OutInvoke(NodeContext nodeContext, Delegate0In0Out<TOut> theDelegate)
		: base(nodeContext, theDelegate?.Delegate, (IEnumerable<AbstractShaderNode>)new List<AbstractShaderNode>(), "Invoke", (ShaderNode<TOut>)null)
	{
	}
}
