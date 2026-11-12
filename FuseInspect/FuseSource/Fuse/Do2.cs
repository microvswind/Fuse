using System.Collections.Generic;
using VL.Core;

namespace Fuse;

public class Do2<T> : ShaderNode<T>
{
	private readonly ShaderNode<T> _input;

	public override string ID => _input.ID;

	public Do2(NodeContext nodeContext, ShaderNode<T> theOne, IEnumerable<AbstractShaderNode> theInputs, string name = "Do2")
		: base(nodeContext, name, (ShaderNode<T>)null, true)
	{
		NullInputMode = HandleNullInputMode.Remove;
		_input = theOne;
		List<AbstractShaderNode> list = new List<AbstractShaderNode>();
		foreach (AbstractShaderNode theInput in theInputs)
		{
			list.Add(theInput);
		}
		list.Add(theOne);
		SetInputs(list);
	}

	protected override string SourceTemplate()
	{
		return "";
	}
}
