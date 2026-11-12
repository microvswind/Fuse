using System.Collections.Generic;
using Fuse.function;
using VL.Core;

namespace Fuse;

public class PassThroughDelegate<T> : Delegate<T>
{
	public Delegate<T> Input { get; }

	public override string ID => Input.ID;

	public override string FunctionName
	{
		get
		{
			if (Input != null)
			{
				return Input.FunctionName;
			}
			return "null input";
		}
	}

	public PassThroughDelegate(NodeContext nodeContext, string theName, AbstractShaderNode theValue)
		: base(nodeContext, theValue, (List<IFunctionParameter>)null, theName, (ShaderNode<T>)null)
	{
		Input = theValue as Delegate<T>;
		SetInputs(new List<AbstractShaderNode> { Input });
	}

	protected override string SourceTemplate()
	{
		return "";
	}

	protected override string GenerateDefaultSource()
	{
		return "";
	}

	public override string TypeName()
	{
		if (Input == null)
		{
			return base.TypeName();
		}
		return Input.TypeName();
	}
}
