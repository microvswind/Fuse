using System.Collections.Generic;
using VL.Core;

namespace Fuse.compute;

public class BufferConsume<T> : ShaderNode<T>
{
	private readonly IBufferInput<T> _buffer;

	public BufferConsume(NodeContext nodeContext, IBufferInput<T> theBuffer, ShaderNode<T> theDefault)
		: base(nodeContext, "consumeBuffer", theDefault, true)
	{
		_buffer = theBuffer;
		_buffer.BufferType = BufferType.Consume;
		SetInputs(new List<AbstractShaderNode> { theBuffer as AbstractShaderNode });
	}

	protected override string SourceTemplate()
	{
		return ShaderNodesUtil.Evaluate("${structType} ${resultName} = ${bufferName}.Consume();", new Dictionary<string, string>
		{
			{
				"structType",
				TypeName()
			},
			{ "bufferName", _buffer.ID }
		});
	}
}
