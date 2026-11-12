using System.Collections.Generic;
using VL.Core;

namespace Fuse.function;

public class Delegate0In1Out<TOut> : Delegate<TOut>
{
	public Delegate0In1Out(NodeContext nodeContext, ShaderNode<TOut> theDelegate, string theId = "Function", ShaderNode<TOut> theDefault = null)
		: base(nodeContext, (AbstractShaderNode)theDelegate, new List<IFunctionParameter>(), theId, theDefault)
	{
	}
}
