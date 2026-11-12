using System.Collections.Generic;
using VL.Core;

namespace Fuse.function;

public class Delegate5In1Out<TIn0, TIn1, TIn2, TIn3, TIn4, TOut> : Delegate<TOut>
{
	public Delegate5In1Out(NodeContext nodeContext, ShaderNode<TOut> theDelegate, FunctionParameter<TIn0> theParam0, FunctionParameter<TIn1> theParam1, FunctionParameter<TIn2> theParam2, FunctionParameter<TIn3> theParam3, FunctionParameter<TIn4> theParam4, string theId = "Function", ShaderNode<TOut> theDefault = null)
		: base(nodeContext, (AbstractShaderNode)theDelegate, new List<IFunctionParameter> { theParam0, theParam1, theParam2, theParam3, theParam4 }, theId, theDefault)
	{
	}
}
