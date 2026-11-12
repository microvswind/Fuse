using System.Collections.Generic;
using Stride.Rendering.Materials;
using VL.Core;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse;

public class AddToGraph<T> : ShaderNode<T>, IComputeVoid, IComputeNode
{
	private readonly ShaderNode<T> _input;

	public override string ID => _input.ID;

	public AddToGraph(NodeContext nodeContext, ShaderNode<T> theMain, IEnumerable<AbstractShaderNode> theInputs, string name = "AddToGraph")
		: base(nodeContext, name, (ShaderNode<T>)null, true)
	{
		NullInputMode = HandleNullInputMode.Remove;
		_input = theMain;
		List<AbstractShaderNode> list = new List<AbstractShaderNode> { theMain };
		list.AddRange(theInputs);
		SetInputs(list);
	}

	protected override string SourceTemplate()
	{
		return "";
	}

	protected override string GenerateDefaultSource()
	{
		return "";
	}
}
