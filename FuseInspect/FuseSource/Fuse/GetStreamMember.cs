using System.Collections.Generic;
using VL.Core;

namespace Fuse;

public class GetStreamMember<T> : ShaderNode<T>
{
	private readonly ShaderNode<int> _index;

	private readonly string _member;

	public GetStreamMember(NodeContext nodeContext, ShaderNode<int> theIndex, string theMember)
		: base(nodeContext, "getStream", (ShaderNode<T>)null, true)
	{
		_index = theIndex;
		_member = theMember;
		SetInputs(new List<AbstractShaderNode> { theIndex });
	}

	protected override string SourceTemplate()
	{
		return ShaderNodesUtil.Evaluate("${resultType} ${resultName} = input[${index}].${member};", new Dictionary<string, string>
		{
			{ "index", _index.ID },
			{ "member", _member }
		});
	}
}
