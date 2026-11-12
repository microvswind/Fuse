using System.Collections.Generic;
using VL.Core;

namespace Fuse.function;

public class Invoke1In1Out<TIn, TOut> : Invoke<TOut>
{
	public Invoke1In1Out(NodeContext nodeContext, Delegate1In1Out<TIn, TOut> theDelegate, ShaderNode<TIn> theArg, string theId = "Invoke", ShaderNode<TOut> theDefault = null)
		: base(nodeContext, (Delegate<TOut>)theDelegate, (IEnumerable<AbstractShaderNode>)new List<AbstractShaderNode> { theArg }, theId, theDefault)
	{
	}
}
