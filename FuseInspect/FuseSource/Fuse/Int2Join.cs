using System.Collections.Generic;
using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse;

public class Int2Join : ResultNode<Int2>
{
	private readonly ShaderNode<int> _x;

	private readonly ShaderNode<int> _y;

	public Int2Join(NodeContext nodeContext, ShaderNode<int> x, ShaderNode<int> y)
		: base(nodeContext, "int2Join", (ShaderNode<Int2>)null, true)
	{
		ShaderNode<int> shaderNode = new ConstantValue<int>(0);
		_x = x ?? shaderNode;
		_y = y ?? shaderNode;
		SetInputs(new List<AbstractShaderNode> { _x, _y });
	}

	protected override string ImplementationTemplate()
	{
		return ShaderNodesUtil.Evaluate("int2(${x},${y})", new Dictionary<string, string>
		{
			{ "x", _x.ID },
			{ "y", _y.ID }
		});
	}
}
