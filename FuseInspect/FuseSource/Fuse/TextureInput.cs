using Stride.Core.Mathematics;
using Stride.Graphics;
using VL.Core;

namespace Fuse;

public class TextureInput : ChangeableObjectInput<Texture>, ITextureInput, ITextureInputProvider
{
	public TextureTypeTracker TextureTypeTracker { get; }

	public TextureInput(NodeContext nodeContext, TextureTypeTracker theTypeTracker)
		: base(nodeContext, (GpuTypeTracker<Texture>)theTypeTracker, "TextureInput")
	{
		TextureTypeTracker = theTypeTracker;
	}

	public string TextureID()
	{
		return ID;
	}

	public Int3 TextureSize()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (Value != null)
		{
			return new Int3(Value.Width, Value.Height, Value.Depth);
		}
		return new Int3(1, 1, 1);
	}

	public ITextureInput GetTextureInput()
	{
		return this;
	}

	public void SetTexture(bool theUseRW, Texture theTexture)
	{
		TextureTypeTracker.SetUseRw(theUseRW);
		TextureTypeTracker.CheckDeclaration(theTexture);
		OnUpdateName();
		Value = theTexture;
	}
}
