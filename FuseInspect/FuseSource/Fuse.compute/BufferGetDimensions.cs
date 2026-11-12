using System.Collections.Generic;
using VL.Core;

namespace Fuse.compute;

public class BufferGetDimensions<T> : ShaderNode<GpuVoid>
{
	private readonly BufferInput<T> _buffer;

	public BufferGetDimensions(NodeContext nodeContext, BufferInput<T> theBuffer)
		: base(nodeContext, "bufferGetDimension", (ShaderNode<GpuVoid>)null, true)
	{
		OptionalOutputs = new List<AbstractShaderNode>();
		_buffer = theBuffer;
		List<AbstractShaderNode> list = new List<AbstractShaderNode> { _buffer };
		DeclareValue<float> item = new DeclareValue<float>(new NodeSubContextFactory(NodeContext).NextSubContext());
		list.Add(item);
		OptionalOutputs.Add(item);
		SetInputs(list);
	}

	protected override string SourceTemplate()
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		if (_buffer != null)
		{
			dictionary["buffer"] = _buffer.ID;
		}
		return ShaderNodesUtil.Evaluate("${buffer}.GetDimensions(${arguments});", dictionary);
	}
}
