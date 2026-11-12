using System.Collections.Generic;
using Stride.Rendering.Materials;
using VL.Core;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse.ShaderFX;

public class CompositionInput<T> : ShaderNode<T>
{
	protected const string DeclarationTemplate = "\r\n        compose Compute${compositionType} ${compositionName};";

	public CompositionInput(NodeContext nodeContext, string theId, IComputeValue<T> theComposition)
		: base(nodeContext, theId, (ShaderNode<T>)null, true)
	{
		AddProperty("Compositions", this);
	}

	protected override Dictionary<string, string> CreateTemplateMap()
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>
		{
			{
				"compositionType",
				TypeHelpers.GetSignature<T>()
			},
			{ "compositionName", ID }
		};
		foreach (KeyValuePair<string, string> item in base.CreateTemplateMap())
		{
			dictionary.Add(item.Key, item.Value);
		}
		return dictionary;
	}

	protected override string SourceTemplate()
	{
		return "${resultType} ${resultName} = ${compositionName}.Compute();";
	}

	public override void OnPassContext(ShaderGeneratorContext theContext)
	{
		ShaderNodesUtil.Evaluate("\r\n        compose Compute${compositionType} ${compositionName};", CreateTemplateMap());
	}
}
