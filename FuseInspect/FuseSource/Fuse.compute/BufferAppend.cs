using System.Collections.Generic;
using VL.Core;

namespace Fuse.compute;

public class BufferAppend<T> : ShaderNode<GpuVoid>
{
	private readonly IBufferInput<T> _buffer;

	private readonly ShaderNode<T> _value;

	public BufferAppend(NodeContext nodeContext, IBufferInput<T> theBuffer, ShaderNode<T> theValue)
		: base(nodeContext, "appendBuffer", (ShaderNode<GpuVoid>)null, true)
	{
		_buffer = theBuffer;
		_buffer.BufferType = BufferType.Append;
		_value = theValue;
		SetInputs(new List<AbstractShaderNode>
		{
			theBuffer as AbstractShaderNode,
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
		return ShaderNodesUtil.Evaluate("${bufferName}.Append(${value});", new Dictionary<string, string>
		{
			{ "bufferName", _buffer.ID },
			{ "value", _value.ID }
		});
	}
}
