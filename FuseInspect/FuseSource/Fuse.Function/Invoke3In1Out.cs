using System.Collections.Generic;
using VL.Core;

namespace Fuse.function;

public class Invoke3In1Out<TIn0, TIn1, TIn2, TOut> : Invoke<TOut>
{
	public Invoke3In1Out(NodeContext nodeContext, Delegate3In1Out<TIn0, TIn1, TIn2, TOut> theDelegate, ShaderNode<TIn0> theArg0, ShaderNode<TIn1> theArg1, ShaderNode<TIn2> theArg2, string theId = "Invoke", ShaderNode<TOut> theDefault = null)
		: base(nodeContext, (Delegate<TOut>)theDelegate, (IEnumerable<AbstractShaderNode>)new List<AbstractShaderNode> { theArg0, theArg1, theArg2 }, theId, theDefault)
	{
	}
}
