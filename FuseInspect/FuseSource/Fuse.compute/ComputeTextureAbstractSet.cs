using System.Collections.Generic;
using VL.Core;

namespace Fuse.compute;

public class ComputeTextureAbstractSet : ShaderNode<GpuVoid>
{
	private readonly AbstractShaderNode _index;

	private readonly ITextureInput _texture;

	private readonly AbstractShaderNode _value;

	public ComputeTextureAbstractSet(NodeContext nodeContext, ITextureInputProvider theTextureProvider, AbstractShaderNode theIndex, AbstractShaderNode theValue)
		: base(nodeContext, "setTextureValue", (ShaderNode<GpuVoid>)null, true)
	{
		_texture = theTextureProvider?.GetTextureInput();
		_index = theIndex;
		_value = theValue;
		SetInputs(new List<AbstractShaderNode>
		{
			(AbstractShaderNode)_texture,
			theIndex,
			theValue
		});
	}

	protected override Dictionary<string, string> CreateTemplateMap()
	{
		return new Dictionary<string, string>();
	}

	protected override string GenerateDefaultSource()
	{
		return "";
	}

	protected override string SourceTemplate()
	{
		return ShaderNodesUtil.Evaluate("${textureName}[${index}] = ${value};", new Dictionary<string, string>
		{
			{
				"textureName",
				_texture.TextureID()
			},
			{ "index", _index.ID },
			{ "value", _value.ID }
		});
	}
}
