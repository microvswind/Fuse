using System.Collections.Generic;
using Stride.Graphics;
using VL.Core;

namespace Fuse.compute;

public class TypedBufferAppend<T> : AbstractTypedFunction<T, GpuVoid> where T : unmanaged
{
	private readonly ShaderNode<Buffer<T>> _buffer;

	private readonly ShaderNode<T> _value;

	public TypedBufferAppend(NodeContext nodeContext, ShaderNode<Buffer<T>> theBuffer, ShaderNode<T> theValue)
		: base(nodeContext, "appendBuffer", (ShaderNode<GpuVoid>)null)
	{
		_buffer = theBuffer;
		_value = theValue;
		SetInputs(new List<AbstractShaderNode> { theBuffer, theValue });
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
