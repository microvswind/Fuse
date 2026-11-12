using System.Collections.Generic;
using Fuse.function;
using VL.Core;

namespace Fuse;

public class IntrinsicFunction<T> : AbstractFunction<T>
{
	public IntrinsicFunction(NodeContext nodeContext, string theFunction, ShaderNode<T> theDefault, IEnumerable<AbstractShaderNode> theArguments, bool theIsGroupable = false, IEnumerable<InputModifier> theModifiers = null)
		: base(nodeContext, theFunction, theArguments, theModifiers, theDefault, theIsGroupable, 0)
	{
	}
}
