using Fuse.ComputeSystem;
using Stride.Core.Mathematics;
using Stride.Graphics;
using VL.Core;

namespace Fuse.compute;

public abstract class AbstractStructuredBufferAttribute<T> : Attribute<T>, IStructureBufferAttribute, IAttribute
{
	public Buffer Buffer { get; set; }

	public int ArrayCount { get; }

	public override Int3 Resolution
	{
		get
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			return new Int3(Buffer.ElementCount, 1, 1);
		}
	}

	protected AbstractStructuredBufferAttribute(NodeContext nodeContext, string theName, AttributeType theType, int theArrayCount = 0)
		: base(nodeContext, theName, theType, (AbstractShaderNode)null)
	{
		ArrayCount = theArrayCount;
	}
}
