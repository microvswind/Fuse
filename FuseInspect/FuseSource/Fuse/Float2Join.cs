using System.Collections.Generic;
using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse;

public class Float2Join : ResultNode<Vector2>
{
	private readonly ShaderNode<float> _x;

	private readonly ShaderNode<float> _y;

	public Float2Join(NodeContext nodeContext, ShaderNode<float> x, ShaderNode<float> y)
		: base(nodeContext, "float2Join", (ShaderNode<Vector2>)null, true)
	{
		ShaderNode<float> shaderNode = new ConstantValue<float>(0f);
		_x = x ?? shaderNode;
		_y = y ?? shaderNode;
		SetInputs(new List<AbstractShaderNode> { _x, _y });
	}

	protected override string ImplementationTemplate()
	{
		return ShaderNodesUtil.Evaluate("float2(${x},${y})", new Dictionary<string, string>
		{
			{ "x", _x.ID },
			{ "y", _y.ID }
		});
	}
}
