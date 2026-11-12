using System.Collections.Generic;
using VL.Core;

namespace Fuse;

public class Output<T> : ShaderNode<T>
{
	private readonly AbstractShaderNode _input;

	public override string ID => _input.ID;

	public Output(NodeContext nodeContext, AbstractShaderNode theComputation, AbstractShaderNode theValue, ShaderNode<T> theDefault = null)
		: base(nodeContext, "AddOutput", theDefault, true)
	{
		_input = theValue;
		SetInputs(new List<AbstractShaderNode> { theComputation, theValue });
	}

	protected override string SourceTemplate()
	{
		return "";
	}

	protected override string GenerateDefaultSource()
	{
		return "";
	}
}
