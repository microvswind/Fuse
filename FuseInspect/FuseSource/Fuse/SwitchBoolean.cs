using System.Collections.Generic;
using VL.Core;

namespace Fuse;

public class SwitchBoolean<T> : ResultNode<T>
{
	private readonly ShaderNode<bool> _inCheck;

	private readonly ShaderNode<T> _inFalse;

	private readonly ShaderNode<T> _inTrue;

	public SwitchBoolean(NodeContext nodeContext, ShaderNode<bool> inCheck, ShaderNode<T> inFalse, ShaderNode<T> inTrue, ShaderNode<T> theDefault = null)
		: base(nodeContext, "expression", theDefault, true)
	{
		List<AbstractShaderNode> list = new List<AbstractShaderNode> { inCheck };
		_inCheck = inCheck;
		_inFalse = inFalse;
		_inTrue = inTrue;
		if (_inFalse != null)
		{
			TypeOverride = _inFalse.TypeName();
			list.Add(_inFalse);
		}
		if (_inTrue != null)
		{
			TypeOverride = _inTrue.TypeName();
			list.Add(_inTrue);
		}
		if (_inFalse == null && _inTrue == null)
		{
			list.Add(null);
		}
		SetInputs(list);
	}

	protected override string ImplementationTemplate()
	{
		return ShaderNodesUtil.Evaluate("${check} ? ${inTrue} : ${inFalse}", new Dictionary<string, string>
		{
			{ "check", _inCheck.ID },
			{ "inFalse", _inFalse.ID },
			{ "inTrue", _inTrue.ID }
		});
	}
}
