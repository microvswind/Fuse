using System.Collections.Generic;
using Fuse.compute;
using Stride.Rendering.Materials;
using VL.Core;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse;

public class Group : ShaderNode<GpuVoid>, IComputeVoid, IComputeNode
{
	public Group(NodeContext nodeContext, IEnumerable<AbstractShaderNode> theInputs, string name = "Group2")
		: base(nodeContext, name, (ShaderNode<GpuVoid>)null, true)
	{
		NullInputMode = HandleNullInputMode.Remove;
		SetInputs(theInputs);
	}

	protected override string SourceTemplate()
	{
		return "";
	}
}
