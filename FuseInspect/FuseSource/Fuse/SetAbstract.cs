using System.Collections.Generic;
using Fuse.compute;
using Stride.Rendering.Materials;
using VL.Core;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse;

public class SetAbstract : ShaderNode<GpuVoid>, IComputeVoid, IComputeNode
{
	private readonly AbstractShaderNode _source;

	private readonly AbstractShaderNode _target;

	public SetAbstract(NodeContext nodeContext, AbstractShaderNode theTarget, AbstractShaderNode theSource)
		: base(nodeContext, "Assign", (ShaderNode<GpuVoid>)null, true)
	{
		_target = theTarget;
		_target.WriteCounter++;
		_source = theSource;
		SetInputs(new List<AbstractShaderNode> { _target, _source });
	}

	protected override string GenerateDefaultSource()
	{
		return "";
	}

	protected override string SourceTemplate()
	{
		if (_target == null || _source == null)
		{
			return "";
		}
		return ShaderNodesUtil.Evaluate("${target} = ${source};", new Dictionary<string, string>
		{
			{ "target", _target.ID },
			{ "source", _source.ID }
		});
	}
}
