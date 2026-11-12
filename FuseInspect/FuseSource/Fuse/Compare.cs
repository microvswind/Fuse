using System.Collections.Generic;
using VL.Core;

namespace Fuse;

public class Compare<T> : ShaderNode<bool>
{
	private readonly ShaderNode<T> _a;

	private readonly ShaderNode<T> _b;

	private readonly bool _combine;

	private readonly string _operator;

	public Compare(NodeContext nodeContext, string theOperator, ShaderNode<T> theA, ShaderNode<T> theB, bool theAll = true)
		: base(nodeContext, "Assign", (ShaderNode<bool>)null, true)
	{
		_operator = theOperator;
		_a = theA;
		_b = theB;
		_combine = theAll;
		SetInputs(new List<AbstractShaderNode> { _a, _b });
	}

	protected override string GenerateDefaultSource()
	{
		return "";
	}

	protected override string SourceTemplate()
	{
		if (TypeHelpers.GetDimension<T>() == 1)
		{
			return ShaderNodesUtil.Evaluate("${resultType} ${resultName} = ${a}${operator}${b};", new Dictionary<string, string>
			{
				{ "a", _a.ID },
				{ "operator", _operator },
				{ "b", _b.ID }
			});
		}
		return ShaderNodesUtil.Evaluate("${resultType} ${resultName} = ${combine}(${a}${operator}${b});", new Dictionary<string, string>
		{
			{ "a", _a.ID },
			{ "operator", _operator },
			{ "b", _b.ID },
			{
				"combine",
				_combine ? "all" : "any"
			}
		});
	}
}
