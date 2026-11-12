using System.Collections.Generic;
using VL.Core;

namespace Fuse.compute;

public class ComputeTextureGet<TIndex, T> : ShaderNode<T> where T : struct
{
	private readonly ShaderNode<TIndex> _index;

	private readonly ITextureInput _texture;

	public ComputeTextureGet(NodeContext nodeContext, ITextureInputProvider theTextureProvider, ShaderNode<TIndex> theIndex, ShaderNode<T> theDefault = null)
		: base(nodeContext, "getTextureValue", theDefault, true)
	{
		_texture = theTextureProvider?.GetTextureInput();
		_index = theIndex;
		SetInputs(new List<AbstractShaderNode>
		{
			_texture as AbstractShaderNode,
			theIndex
		});
	}

	protected override string SourceTemplate()
	{
		return ShaderNodesUtil.Evaluate("${resultType} ${resultName} = ${textureName}[${index}];", new Dictionary<string, string>
		{
			{
				"textureName",
				_texture.TextureID()
			},
			{ "index", _index.ID }
		});
	}
}
