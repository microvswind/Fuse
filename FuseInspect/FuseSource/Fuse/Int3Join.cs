using System.Collections.Generic;
using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse;

public class Int3Join : ResultNode<Int3>
{
	private readonly ShaderNode<int> _x;

	private readonly ShaderNode<int> _y;

	private readonly ShaderNode<int> _z;

	public Int3Join(NodeContext nodeContext, ShaderNode<int> x, ShaderNode<int> y, ShaderNode<int> z)
		: base(nodeContext, "Int3Join", (ShaderNode<Int3>)null, true)
	{
		ShaderNode<int> shaderNode = new ConstantValue<int>(0);
		_x = x ?? shaderNode;
		_y = y ?? shaderNode;
		_z = z ?? shaderNode;
		SetInputs(new List<AbstractShaderNode> { _x, _y, _z });
	}

	protected override string ImplementationTemplate()
	{
		return ShaderNodesUtil.Evaluate("int3(${x},${y},${z})", new Dictionary<string, string>
		{
			{ "x", _x.ID },
			{ "y", _y.ID },
			{ "z", _z.ID }
		});
	}
}
