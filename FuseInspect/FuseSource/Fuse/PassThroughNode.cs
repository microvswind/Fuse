using System.Collections.Generic;
using VL.Core;

namespace Fuse;

public class PassThroughNode<T> : ShaderNode<T>
{
	public ShaderNode<T> Input { get; set; }

	public override string ID => Input.ID;

	public PassThroughNode(NodeContext nodeContext, string theName = "PassThrough")
		: base(nodeContext, theName, (ShaderNode<T>)null, true)
	{
		Default = new ShaderNode<T>(new NodeSubContextFactory(NodeContext).NextSubContext(), "PassThroughDefault");
	}

	public PassThroughNode(NodeContext nodeContext, AbstractShaderNode theValue, string theName = "PassThrough")
		: base(nodeContext, theName, (ShaderNode<T>)null, true)
	{
		Default = new ShaderNode<T>(new NodeSubContextFactory(NodeContext).NextSubContext(), "PassThroughDefault");
		Input = theValue as ShaderNode<T>;
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
