using System.Collections.Generic;
using Stride.Core.Mathematics;
using VL.Core;

namespace Fuse;

public class Matrix2JoinVector2 : ResultNode<Matrix2>
{
	private readonly ShaderNode<Vector2> _x;

	private readonly ShaderNode<Vector2> _y;

	public Matrix2JoinVector2(NodeContext nodeContext, ShaderNode<Vector2> x, ShaderNode<Vector2> y)
		: base(nodeContext, "Matrix2Join", (ShaderNode<Matrix2>)null, true)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		ShaderNode<Vector2> shaderNode = new ConstantValue<Vector2>(default);
		_x = x ?? shaderNode;
		_y = y ?? shaderNode;
		SetInputs(new List<AbstractShaderNode> { _x, _y });
	}

	protected override string ImplementationTemplate()
	{
		return ShaderNodesUtil.Evaluate("float2x2(${x},${y})", new Dictionary<string, string>
		{
			{ "x", _x.ID },
			{ "y", _y.ID }
		});
	}
}
