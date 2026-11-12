using System.Collections.Generic;
using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse;

public class Float3Join : ResultNode<Vector3>
{
	private readonly ShaderNode<float> _x;

	private readonly ShaderNode<float> _y;

	private readonly ShaderNode<float> _z;

	public Float3Join(NodeContext nodeContext, ShaderNode<float> x, ShaderNode<float> y, ShaderNode<float> z)
		: base(nodeContext, "Float3Join", (ShaderNode<Vector3>)null, true)
	{
		ShaderNode<float> shaderNode = new ConstantValue<float>(0f);
		_x = x ?? shaderNode;
		_y = y ?? shaderNode;
		_z = z ?? shaderNode;
		SetInputs(new List<AbstractShaderNode> { _x, _y, _z });
	}

	protected override string ImplementationTemplate()
	{
		return ShaderNodesUtil.Evaluate("float3(${x},${y},${z})", new Dictionary<string, string>
		{
			{ "x", _x.ID },
			{ "y", _y.ID },
			{ "z", _z.ID }
		});
	}
}
