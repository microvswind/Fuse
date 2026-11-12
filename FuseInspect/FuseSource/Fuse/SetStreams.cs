using System.Collections.Generic;
using Fuse.compute;
using Stride.Rendering.Materials;
using VL.Core;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse;

public class SetStreams : ShaderNode<GpuVoid>, IComputeVoid, IComputeNode
{
	private readonly ShaderNode<int> _index;

	public SetStreams(NodeContext nodeContext, ShaderNode<int> theIndex)
		: base(nodeContext, "getStream", (ShaderNode<GpuVoid>)null, true)
	{
		_index = theIndex;
		SetInputs(new List<AbstractShaderNode> { theIndex });
	}

	protected override string SourceTemplate()
	{
		return ShaderNodesUtil.Evaluate("streams = input[${index}];", new Dictionary<string, string> { { "index", _index.ID } });
	}
}
