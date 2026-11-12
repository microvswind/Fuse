using System.Collections.Generic;
using VL.Core;

namespace Fuse;

public class AssignOperator<T> : ShaderNode<T> where T : struct
{
	private readonly AbstractShaderNode _inputA;

	private readonly AbstractShaderNode _inputB;

	private readonly string _operator;

	public AssignOperator(NodeContext nodeContext, AbstractShaderNode theInputA, AbstractShaderNode theInputB, ShaderNode<T> theDefault, string theOperator)
		: base(nodeContext, "result", theDefault, true)
	{
		_operator = theOperator;
		_inputA = theInputA;
		_inputB = theInputB;
		SetInputs(new List<AbstractShaderNode> { theInputA, theInputB });
	}

	protected override string SourceTemplate()
	{
		return ShaderNodesUtil.Evaluate("${inputA} ${operator}= ${inputB};", new Dictionary<string, string>
		{
			{ "inputA", _inputA.ID },
			{ "operator", _operator },
			{ "inputB", _inputB.ID }
		});
	}
}
