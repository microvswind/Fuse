using System.Collections.Generic;
using System.Linq;
using VL.Core;

namespace Fuse;

public class TextureFunction<T> : ShaderNode<T>
{
	private readonly List<AbstractShaderNode> _arguments;

	private readonly string _functionName;

	private readonly ITextureInput _texture;

	public TextureFunction(NodeContext nodeContext, string theFunction, ITextureInputProvider theTextureProvider, IEnumerable<AbstractShaderNode> theArguments, ShaderNode<T> theDefault)
		: base(nodeContext, "textureNode", theDefault, true)
	{
		_functionName = theFunction;
		_texture = theTextureProvider?.GetTextureInput();
		_arguments = theArguments.ToList();
		List<AbstractShaderNode> list = new List<AbstractShaderNode> { _texture as AbstractShaderNode };
		list.AddRange(_arguments);
		SetInputs(list);
	}

	protected override string SourceTemplate()
	{
		if (ShaderNodesUtil.HasNullValue(_arguments) || _texture == null)
		{
			return GenerateDefaultSource();
		}
		return ShaderNodesUtil.Evaluate("${resultType} ${resultName} = ${texture}.${function}(${arguments});", new Dictionary<string, string> { 
		{
			"texture",
			_texture.TextureID()
		} });
	}

	protected override Dictionary<string, string> CreateTemplateMap()
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>
		{
			{ "function", _functionName },
			{ "resultName", ID },
			{
				"resultType",
				TypeName()
			},
			{
				"arguments",
				ShaderNodesUtil.BuildArguments(_arguments)
			},
			{
				"default",
				(Default == null) ? "" : Default.ID
			}
		};
		if (_texture != null)
		{
			dictionary["texture"] = _texture.TextureID();
		}
		return dictionary;
	}
}
