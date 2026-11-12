using System.Collections.Generic;
using Stride.Rendering.Materials;
using VL.Core;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse.compute;

public class ComputeTextureSet<TIndex, T> : ShaderNode<GpuVoid>, IComputeVoid, IComputeNode where T : struct
{
	private readonly AbstractShaderNode _index;

	private readonly ITextureInput _texture;

	private readonly ShaderNode<T> _value;

	public ComputeTextureSet(NodeContext nodeContext)
		: base(nodeContext, "setTextureValue", (ShaderNode<GpuVoid>)null, true)
	{
	}

	public ComputeTextureSet(NodeContext nodeContext, ITextureInputProvider theTextureProvider, ShaderNode<TIndex> theIndex, ShaderNode<T> theValue)
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
		if (_texture == null)
		{
			return GenerateDefaultSource();
		}
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
