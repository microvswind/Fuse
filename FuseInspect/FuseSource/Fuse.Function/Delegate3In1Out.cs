using System.Collections.Generic;
using VL.Core;

namespace Fuse.function;

public class Delegate3In1Out<TIn0, TIn1, TIn2, TOut> : Delegate<TOut>
{
	public Delegate3In1Out(NodeContext nodeContext, ShaderNode<TOut> theDelegate, FunctionParameter<TIn0> theParam0, FunctionParameter<TIn1> theParam1, FunctionParameter<TIn2> theParam2, string theId = "Function", ShaderNode<TOut> theDefault = null)
		: base(nodeContext, (AbstractShaderNode)theDelegate, new List<IFunctionParameter> { theParam0, theParam1, theParam2 }, theId, theDefault)
	{
	}
}
