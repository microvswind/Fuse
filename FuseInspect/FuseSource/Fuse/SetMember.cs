using System.Collections.Generic;
using Fuse.compute;
using Stride.Rendering.Materials;
using VL.Core;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse;

public class SetMember<T> : ShaderNode<GpuVoid>, IComputeVoid, IComputeNode
{
	private readonly string _member;

	private readonly ShaderNode<T> _object;

	private readonly AbstractShaderNode _value;

	public SetMember(NodeContext nodeContext, ShaderNode<T> theObject, string theMember, AbstractShaderNode theValue)
		: base(nodeContext, "SetMember", (ShaderNode<GpuVoid>)null, true)
	{
		_object = theObject;
		_member = theMember;
		_value = theValue;
		SetInputs(new List<AbstractShaderNode> { theObject, theValue });
	}

	protected override string SourceTemplate()
	{
		return ShaderNodesUtil.Evaluate("${object}.${member} =  ${value};", new Dictionary<string, string>
		{
			{
				"object",
				(_object == null) ? "" : _object.ID
			},
			{ "value", _value.ID },
			{ "member", _member }
		});
	}

	public ShaderNode<T> Object()
	{
		return _object;
	}
}
