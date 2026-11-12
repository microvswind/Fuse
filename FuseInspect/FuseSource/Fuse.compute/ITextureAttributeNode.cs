using Fuse.ComputeSystem;
using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse.compute;

public abstract class ITextureAttributeNode<T> : Attribute<T>, ITextureInput, ITextureInputProvider
{
	protected ITextureAttributeNode(NodeContext nodeContext, string theName, AttributeType theType, AbstractShaderNode theValue = null)
		: base(nodeContext, theName, theType, theValue)
	{
	}

	public abstract string TextureID();

	public abstract Int3 TextureSize();

	public ITextureInput GetTextureInput()
	{
		return this;
	}
}
