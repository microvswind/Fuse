using System.Collections.Generic;
using VL.Core;

namespace Fuse;

public class ConstantKeyword : ShaderNode<float>
{
	private readonly string _keyword;

	protected ConstantKeyword(NodeContext nodeContext, string theKeyword)
		: base(nodeContext, "constant keyword", (ShaderNode<float>)null, true)
	{
		_keyword = theKeyword;
		SetInputs(new List<AbstractShaderNode> { null });
		HasFixedName = true;
	}

	protected override Dictionary<string, string> CreateTemplateMap()
	{
		return new Dictionary<string, string>();
	}

	protected override string GenerateDefaultSource()
	{
		return _keyword;
	}

	protected override string SourceTemplate()
	{
		return _keyword;
	}
}
