using System.Collections.Generic;
using Stride.Rendering.Materials;
using VL.Core;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse.compute;

public class BufferSet<T> : ShaderNode<GpuVoid>, IComputeVoid, IComputeNode
{
	private readonly IBufferInput<T> _buffer;

	private readonly ShaderNode<int> _index;

	private readonly ShaderNode<T> _value;

	public BufferSet(NodeContext nodeContext, IBufferInput<T> theBuffer, ShaderNode<int> theIndex, ShaderNode<T> theValue)
		: base(nodeContext, "setBuffer", (ShaderNode<GpuVoid>)null, true)
	{
		_buffer = theBuffer;
		_buffer.BufferType = BufferType.RW;
		_index = theIndex;
		_value = theValue;
		SetInputs(new List<AbstractShaderNode>
		{
			theBuffer as AbstractShaderNode,
			theIndex,
			theValue
		});
	}

	protected override Dictionary<string, string> CreateTemplateMap()
	{
		return new Dictionary<string, string>();
	}

	protected override string GenerateDefaultSource()
	{
		return "";
	}

	protected override string SourceTemplate()
	{
		return ShaderNodesUtil.Evaluate("${bufferName}[${index}] = ${value};", new Dictionary<string, string>
		{
			{ "bufferName", _buffer.ID },
			{ "index", _index.ID },
			{ "value", _value.ID }
		});
	}
}
