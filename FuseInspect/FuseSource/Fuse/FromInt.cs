using System.Collections.Generic;
using VL.Core;

namespace Fuse;

public class FromInt<T> : ResultNode<T> where T : struct
{
	private readonly ShaderNode<int> _x;

	public FromInt(NodeContext nodeContext, ShaderNode<int> x)
		: base(nodeContext, "fromInt", (ShaderNode<T>)null, true)
	{
		ShaderNode<int> shaderNode = new ConstantValue<int>(0);
		_x = x ?? shaderNode;
		SetInputs(new List<AbstractShaderNode> { _x });
	}

	protected override string ImplementationTemplate()
	{
		return ShaderNodesUtil.Evaluate(TypeHelpers.GetDimension(typeof(T)) switch
		{
			1 => "${x}", 
			2 => "int2(${x},${x})", 
			3 => "int3(${x},${x},${x})", 
			4 => "int4(${x},${x},${x},${x})", 
			_ => "", 
		}, new Dictionary<string, string> { { "x", _x.ID } });
	}
}
