using System.Collections.Generic;
using VL.Core;

namespace Fuse.function;

public class Delegate1In1Out<TIn, TOut> : Delegate<TOut>
{
	public Delegate1In1Out(NodeContext nodeContext, ShaderNode<TOut> theDelegate, FunctionParameter<TIn> theParam0, string theId = "Function", ShaderNode<TOut> theDefault = null)
		: base(nodeContext, (AbstractShaderNode)theDelegate, new List<IFunctionParameter> { theParam0 }, theId, theDefault)
	{
	}
}
