using System.Collections.Generic;
using Fuse.function;
using VL.Core;

namespace Fuse.Function;

public class Delegate1In2OutInvoke<TIn, TOut1, TOut2> : Invoke<TOut1>
{
	public Delegate1In2OutInvoke(NodeContext nodeContext, Delegate1In2Out<TIn, TOut1, TOut2> theDelegate, ShaderNode<TIn> theInput)
		: base(nodeContext, theDelegate?.Delegate, (IEnumerable<AbstractShaderNode>)new List<AbstractShaderNode> { theInput }, "Invoke", (ShaderNode<TOut1>)null)
	{
	}
}
