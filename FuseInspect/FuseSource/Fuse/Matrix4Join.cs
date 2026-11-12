using System.Collections.Generic;
using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse;

public class Matrix4Join : ResultNode<Matrix>
{
	private readonly ShaderNode<Vector4> _w;

	private readonly ShaderNode<Vector4> _x;

	private readonly ShaderNode<Vector4> _y;

	private readonly ShaderNode<Vector4> _z;

	public Matrix4Join(NodeContext nodeContext, ShaderNode<Vector4> x, ShaderNode<Vector4> y, ShaderNode<Vector4> z, ShaderNode<Vector4> w)
		: base(nodeContext, "Matrix4Join", (ShaderNode<Matrix>)null, true)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		ShaderNode<Vector4> shaderNode = new ConstantValue<Vector4>(default);
		_x = x ?? shaderNode;
		_y = y ?? shaderNode;
		_z = z ?? shaderNode;
		_w = w ?? shaderNode;
		SetInputs(new List<AbstractShaderNode> { _x, _y, _z, _w });
	}

	protected override string ImplementationTemplate()
	{
		return ShaderNodesUtil.Evaluate("float4x4(${x},${y},${z},${w})", new Dictionary<string, string>
		{
			{ "x", _x.ID },
			{ "y", _y.ID },
			{ "z", _z.ID },
			{ "w", _w.ID }
		});
	}
}
