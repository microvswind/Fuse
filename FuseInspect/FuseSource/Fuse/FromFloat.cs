using System.Collections.Generic;
using VL.Core;

namespace Fuse;

public class FromFloat<T> : ResultNode<T>
{
	private readonly ShaderNode<float> _x;

	public FromFloat(NodeContext nodeContext, ShaderNode<float> x)
		: base(nodeContext, "fromFloat", (ShaderNode<T>)null, true)
	{
		ShaderNode<float> shaderNode = new ConstantValue<float>(0f);
		_x = x ?? shaderNode;
		SetInputs(new List<AbstractShaderNode> { _x });
	}

	protected override string ImplementationTemplate()
	{
		return ShaderNodesUtil.Evaluate(TypeHelpers.GetDimension(typeof(T)) switch
		{
			1 => "${x}", 
			2 => "float2(${x},${x})", 
			3 => "float3(${x},${x},${x})", 
			4 => "float4(${x},${x},${x},${x})", 
			_ => "", 
		}, new Dictionary<string, string> { { "x", _x.ID } });
	}
}
