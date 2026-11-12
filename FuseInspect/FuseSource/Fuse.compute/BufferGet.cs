using System.Collections.Generic;
using VL.Core;

namespace Fuse.compute;

public class BufferGet<T> : ShaderNode<T>
{
	private readonly IBufferInput<T> _buffer;

	private readonly ShaderNode<int> _index;

	public BufferGet(NodeContext nodeContext, IBufferInput<T> theBuffer, ShaderNode<int> theIndex, ShaderNode<T> theType)
		: base(nodeContext, "getBuffer", (ShaderNode<T>)null, true)
	{
		_buffer = theBuffer;
		_index = theIndex;
		TypeOverride = ((theBuffer == null) ? TypeHelpers.GetGpuType<T>() : theBuffer.TypeName());
		List<AbstractShaderNode> list = new List<AbstractShaderNode>
		{
			theBuffer as AbstractShaderNode,
			theIndex
		};
		if (theType != null)
		{
			list.Add(theType);
		}
		SetInputs(list);
	}

	protected override string SourceTemplate()
	{
		return ShaderNodesUtil.Evaluate("${bufferType} ${resultName} = ${bufferName}[${index}];", new Dictionary<string, string>
		{
			{
				"bufferType",
				_buffer.TypeName()
			},
			{ "bufferName", _buffer.ID },
			{ "index", _index.ID }
		});
	}
}
