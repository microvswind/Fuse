using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse.ComputeSystem;

public class TemporaryAttribute<T> : Attribute<T>
{
	public override Int3 Resolution
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return new Int3(1);
		}
	}

	public TemporaryAttribute(NodeContext nodeContext, string theName)
		: base(nodeContext, theName, AttributeType.Temporary, (AbstractShaderNode)null)
	{
		AddProperty("ComputeSystemAttribute", this);
	}
}
