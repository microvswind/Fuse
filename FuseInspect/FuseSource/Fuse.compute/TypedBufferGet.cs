using System.Collections.Generic;
using Stride.Graphics;
using VL.Core;

namespace Fuse.compute;

public class TypedBufferGet<T> : AbstractTypedFunction<T, T> where T : unmanaged
{
	private readonly ShaderNode<Buffer<T>> _buffer;

	private readonly ShaderNode<int> _index;

	public TypedBufferGet(NodeContext nodeContext, ShaderNode<Buffer<T>> theBuffer, ShaderNode<int> theIndex, ShaderNode<T> theDefault)
		: base(nodeContext, "getBuffer", theDefault)
	{
		_buffer = theBuffer;
		_index = theIndex;
		SetInputs(new List<AbstractShaderNode> { theBuffer, theIndex });
	}

	protected override string SourceTemplate()
	{
		return ShaderNodesUtil.Evaluate("${resultType} ${resultName} = ${bufferName}[${index}];", new Dictionary<string, string>
		{
			{ "bufferName", _buffer.ID },
			{ "index", _index.ID }
		});
	}
}
