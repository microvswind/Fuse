using System.Collections.Generic;
using VL.Core;

namespace Fuse;

public class GetMember<TIn, TOut> : ResultNode<TOut>
{
	private readonly ShaderNode<TIn> _input;

	private readonly string _member;

	public GetMember(NodeContext nodeContext, ShaderNode<TIn> theInput, string theMember, ShaderNode<TOut> theDefault = null)
		: base(nodeContext, "GetMember", theDefault, true)
	{
		_member = theMember;
		_input = theInput;
		if (theInput != null && theMember != null)
		{
			Name = theInput.Name + ShaderNodesUtil.FirstLetterToUpper(theMember);
		}
		SetInputs(new List<AbstractShaderNode> { theInput });
	}

	protected override string ImplementationTemplate()
	{
		return ShaderNodesUtil.Evaluate("${input}.${member}", new Dictionary<string, string>
		{
			{
				"input",
				(_input == null) ? "" : _input.ID
			},
			{ "member", _member }
		});
	}
}
