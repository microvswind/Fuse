using Stride.Graphics;
using VL.Core;

namespace Fuse;

internal sealed class DelegatingBufferInput<T> : ShaderNode<Buffer> where T : unmanaged
{
	private readonly NodeContext _nodeContext;

	private readonly BufferTypeTracker<T> _typeTracker;

	private BufferInput<T> _bufferInput;

	public DelegatingBufferInput(NodeContext nodeContext)
		: base(nodeContext, "DelegatingBufferInput", (ShaderNode<Buffer>)null, false)
	{
		_nodeContext = nodeContext;
		_typeTracker = new BufferTypeTracker<T>(null);
	}

	protected override ShaderNode<Buffer> SetValue(Buffer value)
	{
		if (_typeTracker.CheckDeclaration(value) || _bufferInput == null)
		{
			_bufferInput = new BufferInput<T>(_nodeContext, _typeTracker, null);
		}
		_bufferInput.Value = value;
		return _bufferInput;
	}
}
