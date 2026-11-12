using System.Collections.Generic;
using Fuse.function;
using VL.Core;

namespace Fuse;

public class MixinFunction<T> : AbstractFunction<T>
{
	public MixinFunction(NodeContext nodeContext, string theFunction, ShaderNode<T> theDefault, string theMixin, IEnumerable<AbstractShaderNode> theArguments, bool theIsGroupable = false, int theGroupOptions = 0, IEnumerable<InputModifier> theModifiers = null)
		: base(nodeContext, theFunction, theArguments, theModifiers, theDefault, theIsGroupable, theGroupOptions)
	{
		AddProperty("Mixins", theMixin);
	}
}
