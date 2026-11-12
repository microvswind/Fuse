using System.Collections.Generic;
using VL.Core;

namespace Fuse.function;

public class Invoke2In1Out<TIn0, TIn1, TOut> : Invoke<TOut>
{
	public Invoke2In1Out(NodeContext nodeContext, Delegate2In1Out<TIn0, TIn1, TOut> theDelegate, ShaderNode<TIn0> theArg0, ShaderNode<TIn1> theArg1, string theId = "Invoke", ShaderNode<TOut> theDefault = null)
		: base(nodeContext, (Delegate<TOut>)theDelegate, (IEnumerable<AbstractShaderNode>)new List<AbstractShaderNode> { theArg0, theArg1 }, theId, theDefault)
	{
	}
}
