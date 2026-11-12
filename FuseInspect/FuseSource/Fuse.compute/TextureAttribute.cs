using Fuse.ComputeSystem;
using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse.compute;

public class TextureAttribute<T> : ITextureAttributeNode<T>, ITextureAttribute, IAttribute where T : struct
{
	public TextureInput TextureInput { get; set; }

	public ShaderNode<Int3> Index { get; set; }

	public bool DoubleBuffered { get; }

	public override Int3 Resolution
	{
		get
		{
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			if (TextureInput?.Value != null)
			{
				return new Int3(TextureInput.Value.Width, TextureInput.Value.Height, TextureInput.Value.Depth);
			}
			return new Int3(1);
		}
	}

	public TextureAttribute(NodeContext nodeContext, string theName, bool theIsDoubleBuffered, ShaderNode<T> theDefault = null)
		: base(nodeContext, theName, AttributeType.Texture, (AbstractShaderNode)null)
	{
		DoubleBuffered = theIsDoubleBuffered;
		Default = theDefault ?? ConstantHelper.FromFloat<T>(0f);
		SetProperty("ComputeSystemAttribute", this);
	}

	public void OverRideByIndex(ShaderNode<Int3> theIndex)
	{
		Index = theIndex;
		SetProperty("ComputeSystemAttribute", this);
	}

	public void OverRideByTexture(ITextureInputProvider theInstance, AbstractShaderNode theIndex)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		if (theInstance == null)
		{
			SetProperty("ComputeSystemAttribute", this);
			return;
		}
		NodeSubContextFactory nodeSubContextFactory = new NodeSubContextFactory(NodeContext, 1);
		SetInput(TypeHelpers.GetDimensionFromInt3(theInstance.GetTextureInput().TextureSize()) switch
		{
			1 => (AbstractShaderNode)new ComputeTextureGet<int, T>(nodeSubContextFactory.NextSubContext(), theInstance, (ShaderNode<int>)theIndex), 
			2 => new ComputeTextureGet<Int2, T>(nodeSubContextFactory.NextSubContext(), theInstance, (ShaderNode<Int2>)theIndex), 
			3 => new ComputeTextureGet<Int3, T>(nodeSubContextFactory.NextSubContext(), theInstance, (ShaderNode<Int3>)theIndex), 
			_ => null, 
		});
		RemoveProperty("ComputeSystemAttribute");
	}

	public override string TextureID()
	{
		return TextureInput?.ID;
	}

	public override Int3 TextureSize()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return Resolution;
	}
}
