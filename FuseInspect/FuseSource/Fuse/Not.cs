using System.Collections.Generic;
using VL.Core;

namespace Fuse;

public class Not : ResultNode<bool>
{
	private readonly ShaderNode<bool> _in;

	public Not(NodeContext nodeContext, ShaderNode<bool> theIn)
		: base(nodeContext, "not", (ShaderNode<bool>)null, true)
	{
		_in = theIn ?? Default;
		SetInputs(new List<AbstractShaderNode> { _in });
	}

	protected override string ImplementationTemplate()
	{
		return ShaderNodesUtil.Evaluate("!${in}", new Dictionary<string, string> { { "in", _in.ID } });
	}
}
