using System.Collections.Generic;
using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse;

public class Matrix3Join : ResultNode<Matrix3>
{
	private readonly ShaderNode<Vector3> _x;

	private readonly ShaderNode<Vector3> _y;

	private readonly ShaderNode<Vector3> _z;

	public Matrix3Join(NodeContext nodeContext, ShaderNode<Vector3> x, ShaderNode<Vector3> y, ShaderNode<Vector3> z)
		: base(nodeContext, "Matrix3Join", (ShaderNode<Matrix3>)null, true)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		ShaderNode<Vector3> shaderNode = new ConstantValue<Vector3>(default);
		_x = x ?? shaderNode;
		_y = y ?? shaderNode;
		_z = z ?? shaderNode;
		SetInputs(new List<AbstractShaderNode> { _x, _y, _z });
	}

	protected override string ImplementationTemplate()
	{
		return ShaderNodesUtil.Evaluate("float3x3(${x},${y},${z})", new Dictionary<string, string>
		{
			{ "x", _x.ID },
			{ "y", _y.ID },
			{ "z", _z.ID }
		});
	}
}
