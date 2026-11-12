using System.Collections.Generic;
using Stride.Graphics;
using Stride.Rendering.Materials;
using VL.Core;
using VL.Stride.Shaders.ShaderFX;

namespace Fuse.compute;

public class TypedBufferSet<TIn> : AbstractTypedFunction<TIn, GpuVoid>, IComputeVoid, IComputeNode where TIn : unmanaged
{
	private readonly ShaderNode<Buffer<TIn>> _buffer;

	private readonly ShaderNode<int> _index;

	private readonly ShaderNode<TIn> _value;

	public TypedBufferSet(NodeContext nodeContext, ShaderNode<Buffer<TIn>> theBuffer, ShaderNode<int> theIndex, ShaderNode<TIn> theValue)
		: base(nodeContext, "setBuffer", (ShaderNode<GpuVoid>)null)
	{
		_buffer = theBuffer;
		_index = theIndex;
		_value = theValue;
		SetInputs(new List<AbstractShaderNode> { theBuffer, theIndex, theValue });
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
