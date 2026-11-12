using System.Collections.Generic;
using Fuse.compute;
using Stride.Rendering.Materials;
using VL.Core;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse;

public class SetItem<T, TValue> : ShaderNode<GpuVoid>, IComputeVoid, IComputeNode where TValue : struct
{
	private readonly ShaderNode<int> _index;

	private readonly ShaderNode<T> _target;

	private readonly ShaderNode<TValue> _value;

	public SetItem(NodeContext nodeContext, ShaderNode<T> theTarget, ShaderNode<int> theIndex, ShaderNode<TValue> theValue)
		: base(nodeContext, "setItem", (ShaderNode<GpuVoid>)null, true)
	{
		_target = theTarget;
		_index = theIndex;
		_value = theValue;
		SetInputs(new List<AbstractShaderNode> { theTarget, theIndex, theValue });
	}

	protected override string SourceTemplate()
	{
		return ShaderNodesUtil.Evaluate("${target}[${index}] =  ${value};", new Dictionary<string, string>
		{
			{ "target", _target.ID },
			{ "index", _index.ID },
			{ "value", _value.ID }
		});
	}

	public ShaderNode<T> Value()
	{
		return _target;
	}
}
