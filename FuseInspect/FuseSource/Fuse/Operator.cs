using System.Collections.Generic;
using System.Text;
using VL.Core;

namespace Fuse;

public class Operator<T> : ResultNode<T> where T : struct
{
	private readonly string _operator;

	public Operator(NodeContext nodeContext, IEnumerable<AbstractShaderNode> theInputs, ShaderNode<T> theDefault, string theOperator)
		: base(nodeContext, "result", theDefault, true)
	{
		_operator = theOperator;
		List<AbstractShaderNode> list = new List<AbstractShaderNode>();
		foreach (AbstractShaderNode theInput in theInputs)
		{
			list.Add(theInput ?? Default);
		}
		SetInputs(list);
	}

	protected override string ImplementationTemplate()
	{
		StringBuilder call = new StringBuilder();
		Ins.ForEach((AbstractShaderNode input) =>
		{
			call.Append(input.ID);
			call.Append(" " + _operator + " ");
		});
		if (call.Length > 3)
		{
			call.Remove(call.Length - 3, 3);
		}
		return ShaderNodesUtil.Evaluate("(${Call})", new Dictionary<string, string> { 
		{
			"Call",
			call.ToString()
		} });
	}
}
