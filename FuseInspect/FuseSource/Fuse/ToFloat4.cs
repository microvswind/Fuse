using System.Collections.Generic;
using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse;

public class ToFloat4 : ResultNode<Vector4>
{
	private readonly AbstractShaderNode _x;

	public ToFloat4(NodeContext nodeContext, AbstractShaderNode x)
		: base(nodeContext, "ToFloat", (ShaderNode<Vector4>)null, true)
	{
		ShaderNode<float> shaderNode = new ConstantValue<float>(0f);
		_x = x ?? shaderNode;
		SetInputs(new List<AbstractShaderNode> { _x });
	}

	protected override string ImplementationTemplate()
	{
		return ShaderNodesUtil.Evaluate(_x.Dimension() switch
		{
			1 => "float4(${x},${x},${x},1.0)", 
			2 => "float4(${x}.xy,0.0,1.0)", 
			3 => "float4(${x}.xyz,1.0)", 
			4 => "${x}", 
			_ => "float4(1.0,1.0,0.0,1.0)", 
		}, new Dictionary<string, string> { { "x", _x.ID } });
	}
}
