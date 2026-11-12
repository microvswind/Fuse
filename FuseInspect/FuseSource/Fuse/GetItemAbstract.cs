using System.Collections.Generic;
using VL.Core;

namespace Fuse;

public class GetItemAbstract<TOut> : ResultNode<TOut>
{
	private readonly ShaderNode<int> _index;

	private readonly AbstractShaderNode _input;

	public GetItemAbstract(NodeContext nodeContext, AbstractShaderNode theInput, ShaderNode<int> theIndex)
		: base(nodeContext, "GetItem", (ShaderNode<TOut>)null, true)
	{
		_input = theInput;
		_index = theIndex;
		SetInputs(new List<AbstractShaderNode> { theInput, theIndex });
	}

	protected override string ImplementationTemplate()
	{
		return ShaderNodesUtil.Evaluate("${inputName}[${index}]", new Dictionary<string, string>
		{
			{ "inputName", _input.ID },
			{ "index", _index.ID }
		});
	}
}
