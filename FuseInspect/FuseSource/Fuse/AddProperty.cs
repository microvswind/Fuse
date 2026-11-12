using System.Collections;
using VL.Core;

namespace Fuse;

public class AddProperty<T> : PassThroughNode<T>
{
	private readonly string _propertyId;

	public AddProperty(NodeContext nodeContext, ShaderNode<T> theIn, string thePropertyId, IList theResources)
		: base(nodeContext, (AbstractShaderNode)theIn, "AddProperty")
	{
		_propertyId = thePropertyId;
		AddProperties(_propertyId, theResources);
	}
}
