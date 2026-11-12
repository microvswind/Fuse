using System.Collections.Generic;
using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse;

public class Int4Join : ResultNode<Int4>
{
	private readonly ShaderNode<int> _w;

	private readonly ShaderNode<int> _x;

	private readonly ShaderNode<int> _y;

	private readonly ShaderNode<int> _z;

	public Int4Join(NodeContext nodeContext, ShaderNode<int> x, ShaderNode<int> y, ShaderNode<int> z, ShaderNode<int> w)
		: base(nodeContext, "Int4Join", (ShaderNode<Int4>)null, true)
	{
		ShaderNode<int> shaderNode = new ConstantValue<int>(0);
		_x = x ?? shaderNode;
		_y = y ?? shaderNode;
		_z = z ?? shaderNode;
		_w = w ?? shaderNode;
		SetInputs(new List<AbstractShaderNode> { _x, _y, _z, _w });
	}

	protected override string ImplementationTemplate()
	{
		return ShaderNodesUtil.Evaluate("int4(${x},${y},${z},${w})", new Dictionary<string, string>
		{
			{ "x", _x.ID },
			{ "y", _y.ID },
			{ "z", _z.ID },
			{ "w", _w.ID }
		});
	}
}
