using Stride.Graphics;

namespace Fuse;

public class BufferTypeTracker<T> : GpuTypeTracker<Buffer>
{
	private readonly ShaderNode<T> _type;

	public BufferType BufferType { get; set; }

	public BufferTypeTracker(ShaderNode<T> theType, BufferType theBufferType = BufferType.Normal)
	{
		_type = theType;
		BufferType = theBufferType;
	}

	protected override string DefineGpuType(Buffer value)
	{
		return TypeHelpers.BufferTypeName(value, (_type == null) ? TypeHelpers.GetGpuType<T>() : _type.TypeName(), BufferType);
	}

	protected override string DefineComputeGpuType(Buffer value)
	{
		return DefineGpuType(value);
	}
}
