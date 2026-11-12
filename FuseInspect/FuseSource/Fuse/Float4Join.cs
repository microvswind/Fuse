using System.Collections.Generic;
using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse;

public class Float4Join : ResultNode<Vector4>
{
	private readonly ShaderNode<float> _w;

	private readonly ShaderNode<float> _x;

	private readonly ShaderNode<float> _y;

	private readonly ShaderNode<float> _z;

	public Float4Join(NodeContext nodeContext, ShaderNode<float> x, ShaderNode<float> y, ShaderNode<float> z, ShaderNode<float> w)
		: base(nodeContext, "Float4Join", (ShaderNode<Vector4>)null, true)
	{
		ShaderNode<float> shaderNode = new ConstantValue<float>(0f);
		_x = x ?? shaderNode;
		_y = y ?? shaderNode;
		_z = z ?? shaderNode;
		_w = w ?? shaderNode;
		SetInputs(new List<AbstractShaderNode> { _x, _y, _z, _w });
	}

	protected override string ImplementationTemplate()
	{
		return ShaderNodesUtil.Evaluate("float4(${x},${y},${z},${w})", new Dictionary<string, string>
		{
			{ "x", _x.ID },
			{ "y", _y.ID },
			{ "z", _z.ID },
			{ "w", _w.ID }
		});
	}
}
