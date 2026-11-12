using System.Collections.Generic;
using VL.Core;

namespace Fuse.function;

public class Invoke0In1Out<TOut> : Invoke<TOut>
{
	public Invoke0In1Out(NodeContext nodeContext, Delegate0In1Out<TOut> theDelegate, string theId = "Invoke", ShaderNode<TOut> theDefault = null)
		: base(nodeContext, (Delegate<TOut>)theDelegate, (IEnumerable<AbstractShaderNode>)new List<AbstractShaderNode>(), theId, theDefault)
	{
	}
}
