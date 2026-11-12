using System.Collections.Generic;
using VL.Core;

namespace Fuse;

public class Matrix2Join : ResultNode<Matrix2>
{
	private readonly ShaderNode<float> _00;

	private readonly ShaderNode<float> _01;

	private readonly ShaderNode<float> _10;

	private readonly ShaderNode<float> _11;

	public Matrix2Join(NodeContext nodeContext, ShaderNode<float> the00, ShaderNode<float> the10, ShaderNode<float> the01, ShaderNode<float> the11)
		: base(nodeContext, "Matrix2Join", (ShaderNode<Matrix2>)null, true)
	{
		ShaderNode<float> shaderNode = new ConstantValue<float>(0f);
		_00 = the00 ?? shaderNode;
		_10 = the10 ?? shaderNode;
		_01 = the01 ?? shaderNode;
		_11 = the11 ?? shaderNode;
		SetInputs(new List<AbstractShaderNode> { _00, _10, _01, _11 });
	}

	protected override string ImplementationTemplate()
	{
		return ShaderNodesUtil.Evaluate("float2x2(${m00},${m10},${m01},${m11})", new Dictionary<string, string>
		{
			{ "m00", _00.ID },
			{ "m10", _10.ID },
			{ "m01", _01.ID },
			{ "m11", _11.ID }
		});
	}
}
