using System.Collections.Generic;
using Fuse.function;
using VL.Core;

namespace Fuse.Function;

public class Delegate2In2OutInvoke<TIn1, TIn2, TOut1, TOut2> : Invoke<TOut1>
{
	public Delegate2In2OutInvoke(NodeContext nodeContext, Delegate2In2Out<TIn1, TIn2, TOut1, TOut2> theDelegate, ShaderNode<TIn1> theInput1, ShaderNode<TIn2> theInput2)
		: base(nodeContext, theDelegate?.Delegate, (IEnumerable<AbstractShaderNode>)new List<AbstractShaderNode> { theInput1, theInput2 }, "Invoke", (ShaderNode<TOut1>)null)
	{
	}
}
