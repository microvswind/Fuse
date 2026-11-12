using System.Collections.Generic;
using Stride.Rendering;
using Stride.Rendering.Materials;
using VL.Core;

namespace Fuse;

public class DefaultInput<T> : ShaderNode<T>, IGpuInput, IComputeNode
{
	public override string ID => Name;

	public DefaultInput(NodeContext nodeContext, string theName)
		: base(nodeContext, theName, (ShaderNode<T>)null, true)
	{
		SetInputs(new List<AbstractShaderNode>());
	}

	public void SetParameterValue(ParameterCollection theCollection)
	{
	}

	public void OnUpdateName()
	{
	}

	public bool CheckDeclaration()
	{
		return false;
	}

	protected override string SourceTemplate()
	{
		return "";
	}
}
