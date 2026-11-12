using System.Collections.Generic;
using Fuse.compute;
using Stride.Rendering.Materials;
using VL.Core;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse;

public class SimpleKeyword : ShaderNode<GpuVoid>, IComputeVoid, IComputeNode
{
	private readonly string _keyword;

	protected SimpleKeyword(NodeContext nodeContext, string theKeyword)
		: base(nodeContext, "void", (ShaderNode<GpuVoid>)null, true)
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
