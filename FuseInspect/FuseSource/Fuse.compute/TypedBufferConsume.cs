using System.Collections.Generic;
using Stride.Graphics;
using VL.Core;

namespace Fuse.compute;

public class TypedBufferConsume<T> : AbstractTypedFunction<T, T> where T : unmanaged
{
	private readonly ShaderNode<Buffer<T>> _buffer;

	public TypedBufferConsume(NodeContext nodeContext, ShaderNode<Buffer<T>> theBuffer, ShaderNode<T> theDefault)
		: base(nodeContext, "consumeBuffer", theDefault)
	{
		_buffer = theBuffer;
		SetInputs(new List<AbstractShaderNode> { theBuffer });
	}

	protected override string SourceTemplate()
	{
		return ShaderNodesUtil.Evaluate("${resultType} ${resultName} = ${bufferName}.Consume();", new Dictionary<string, string> { { "bufferName", _buffer.ID } });
	}
}
